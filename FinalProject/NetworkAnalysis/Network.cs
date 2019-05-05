using System.Collections.Generic;
using System.Linq;

namespace FinalProject.NetworkAnalysis
{
    public class Network
    {
        public IList<Node> Nodes { get; }
        public int Edges => Nodes.Select(e => e.Neighbors.Count).Sum() / 2;

        public Network(IList<Node> nodes)
        {
            Nodes = nodes;
        }

        public float GetGlobalClusteringCoefficient()
        {
            var sum = Nodes.Select(e => e.ClusteringCoefficient).Sum();
            return sum / Nodes.Count;
        }

        public void ComputeClusteringCoefficient()
        {
            foreach (var node in Nodes)
            {
                var neighborsNetwork = node.Neighbors.Except(new[] { node }).ToList();

                int n = node.Degree;
                int m = CountEdges(neighborsNetwork);

                if (node.Degree == 0 || node.Degree == 1)
                    node.ClusteringCoefficient = 0;
                else
                    node.ClusteringCoefficient = 2 * m / (float)(n * (n - 1));
            }
        }

        private int CountEdges(IList<Node> vertices)
        {
            int edges = vertices
                .Select(e => e.Neighbors.Intersect(vertices).Count()) // only count edges in subgraph
                .Sum();

            return edges / 2; // divide by two because a graph is unoriented
        }

        public float GetAverageDegree()
        {
            return (float)Nodes.Sum(e => e.Degree) / Nodes.Count;
        }

        public Network Clone()
        {
            var newVertices = Nodes.Select(e => new Node(e)).ToDictionary(e => e.Id);

            foreach (var vertex in Nodes)
            {
                var newVertex = newVertices[vertex.Id];
                foreach (var neighbor in vertex.Neighbors)
                {
                    var newNeighbor = newVertices[neighbor.Id];
                    newVertex.AddNeighborBiDirection(newNeighbor);
                }
            }

            return new Network(newVertices.Values.ToList());
        }
    }
}
