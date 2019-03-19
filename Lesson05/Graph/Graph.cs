using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lesson05.Graph
{
    public class Graph
    {
        public IList<Vertex> Vertices { get; }

        public Graph(IList<Vertex> vertices)
        {
            Vertices = vertices;
        }

        /// <summary>
        /// Random node sampling (uniform node sampling)
        /// </summary>
        /// <param name="p">Probability of vertex being sampled</param>
        /// <returns></returns>
        public Graph CreateRnsSample(double p)
        {
            if (p <= 0 || p >= 1)
                throw new ArgumentOutOfRangeException(nameof(p), "Must be in between 0-1");

            var random = new Random(0);
            var newVertices = Vertices.Select(e => new Vertex(e.Id)).ToList();
            var newGraphVertices = new List<Vertex>();

            foreach (var vertex in Vertices)
            {
                if (random.NextDouble() < p)
                {
                    var v1 = newVertices.Single(e => e.Id == vertex.Id);
                    if (!newGraphVertices.Contains(v1))
                        newGraphVertices.Add(v1);
                    foreach (var neighbor in vertex.Neighbors)
                    {
                        var v2 = newVertices.Single(e => e.Id == neighbor.Id);
                        if (!newGraphVertices.Contains(v2))
                            newGraphVertices.Add(v2);
                        v1.Neighbors.Add(v2);
                        v2.Neighbors.Add(v1);
                    }
                }
            }

            newGraphVertices.ForEach(e => e.Neighbors = e.Neighbors.Distinct().ToList());

            return new Graph(newGraphVertices);
        }

        /// <summary>
        /// Degree-based sampling
        /// </summary>
        /// <param name="p">Probability of vertex being sampled</param>
        /// <returns></returns>
        public Graph CreateRdnSample(double p)
        {
            if (p <= 0 || p >= 1)
                throw new ArgumentOutOfRangeException(nameof(p), "Must be in between 0-1");

            var random = new Random(0);
            var newVertices = Vertices.Select(e => new Vertex(e.Id)).ToList();
            var newGraphVertices = new List<Vertex>();

            foreach (var vertex in Vertices)
            {
                if (random.NextDouble() < (p / vertex.Degree))
                {
                    var v1 = newVertices.Single(e => e.Id == vertex.Id);
                    if (!newGraphVertices.Contains(v1))
                        newGraphVertices.Add(v1);
                    foreach (var neighbor in vertex.Neighbors)
                    {
                        var v2 = newVertices.Single(e => e.Id == neighbor.Id);
                        if (!newGraphVertices.Contains(v2))
                            newGraphVertices.Add(v2);
                        v1.Neighbors.Add(v2);
                        v2.Neighbors.Add(v1);
                    }
                }
            }

            newGraphVertices.ForEach(e => e.Neighbors = e.Neighbors.Distinct().ToList());

            return new Graph(newGraphVertices);
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

        public void NormalizeIds()
        {
            Vertices.ForEach((e, i) => e.Id = i);
        }

        public Matrix.AdjacencyMatrix GetAdjacencyMatrix()
        {
            var matrix = new Matrix.AdjacencyMatrix(Vertices.Count);
            foreach (var v1 in Vertices)
            {
                foreach (var v2 in Vertices)
                {
                    if (v1.Neighbors.Contains(v2))
                        matrix[v1.Id, v2.Id] = 1;
                    else
                        matrix[v1.Id, v2.Id] = 0;
                }
            }

            return matrix;
        }

        public Graph Clone()
        {
            var newVertices = Vertices.Select(e => new Vertex(e)).ToDictionary(e => e.Id);

            foreach (var vertex in Vertices)
            {
                var newVertex = newVertices[vertex.Id];
                foreach (var neighbor in vertex.Neighbors)
                {
                    var newNeighbor = newVertices[neighbor.Id];
                    newVertex.AddNeighborBiDirection(newNeighbor);
                }
            }

            return new Graph(newVertices.Values.ToList());
        }

        public Vertex MergeVertices(int id1, int id2)
        {
            var v1 = Vertices.SingleOrDefault(e => e.Id == id1);
            var v2 = Vertices.SingleOrDefault(e => e.Id == id2);

            if (v1 == null)
                throw new Exception($"Vertex '{id1}' not found");
            if (v2 == null)
                throw new Exception($"Vertex '{id2}' not found");

            int maxId = Vertices.Select(e => e.Id).Max();
            var merged = new Vertex(maxId + 1);

            // add all neighbors
            foreach (var neighbor in v1.Neighbors.ToList())
                merged.AddNeighborBiDirection(neighbor);
            foreach (var neighbor in v2.Neighbors.ToList())
                merged.AddNeighborBiDirection(neighbor);

            // remove old neighbor references from graph
            foreach (var neighbor in v1.Neighbors.ToList())
                v1.RemoveNeighborBiDirection(neighbor);
            foreach (var neighbor in v2.Neighbors.ToList())
                v2.RemoveNeighborBiDirection(neighbor);

            merged.IdsBeforeMerge.AddRange(v1.IdsBeforeMerge);
            merged.IdsBeforeMerge.AddRange(v2.IdsBeforeMerge);

            Vertices.Remove(v1);
            Vertices.Remove(v2);
            Vertices.Add(merged);

            return merged;
        }
    }
}
