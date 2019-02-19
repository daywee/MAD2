using MoreLinq.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lesson01.Graph
{
    public class Graph
    {
        public IList<Vertex> Vertices { get; }

        public Graph(IList<Vertex> vertices)
        {
            Vertices = vertices;
        }



        public void NormalizeIds()
        {
            Vertices
                .OrderBy(e => e.Id)
                .ForEach((e, i) =>
                {
                    e.Name = e.Name + $"({e.Id})";
                    e.Id = i + 1;
                });
        }

        public void ComputeClusteringCoefficient()
        {
            foreach (var vertex in Vertices)
            {
                Debug.WriteLine("Counting id: " + vertex.Id);
                var neighborsSubgraph = vertex.Neighbors.Except(new[] { vertex }).ToList();

                int n = vertex.Degree;
                int m = CountEdges(neighborsSubgraph);

                if (vertex.Degree == 0 || vertex.Degree == 1)
                    vertex.ClusteringCoefficient = 0;
                else
                    vertex.ClusteringCoefficient = 2 * m / (float)(n * (n - 1));
            }
        }

        private int CountEdges(IList<Vertex> vertices)
        {
            int edges = vertices
                .Select(e => e.Neighbors.Intersect(vertices).Count()) // only count edges in subgraph
                .Sum();
            return edges / 2; // divide by two because a graph is unoriented
        }

        public float GetGlobalClusteringCoefficient()
        {
            var sum = Vertices.Select(e => e.ClusteringCoefficient).Sum();
            return sum / Vertices.Count;
        }

        public float GetAverageDegree()
        {
            return (float)Vertices.Sum(e => e.Degree) / Vertices.Count;
        }
    }
}
