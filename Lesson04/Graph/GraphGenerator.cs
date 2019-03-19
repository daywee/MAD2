using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson04.Graph
{
    public class GraphGenerator
    {
        private readonly Random _random = new Random(0);

        /// <summary>
        /// Generate graph by Barabási–Albert model
        /// </summary>
        /// <param name="n">Number of vertices</param>
        /// <param name="m0">Number of fundamental nodes</param>
        /// <param name="m">Number of edges for new created vertices</param>
        /// <returns></returns>
        public Graph GenerateBarabasiAlbertModel(int n, int m0, int m)
        {
            var vertices = Enumerable.Range(0, m0).Select(id => new Vertex(id)).ToList();

            // create base graph (complete graph)
            for (int i = 0; i < m0; i++)
            {
                for (int j = 0; j < m0; j++)
                {
                    if (i == j)
                        continue;
                    vertices[i].AddNeighborBiDirection(vertices[j]);
                }
            }

            var vertexDegreeDistribution = new List<int>();
            foreach (var vertex in vertices)
            {
                for (int i = 0; i < vertex.Degree; i++)
                {
                    vertexDegreeDistribution.Add(vertex.Id);
                }
            }

            // add new nodes
            for (int i = 0; i < n; i++)
            {
                var vertex = new Vertex(m0 + i + 1);
                var usedNodeIds = new List<int>();

                for (int j = 0; j < m; j++)
                {
                    int randomIndex = _random.Next(vertexDegreeDistribution.Count - 1);
                    while (usedNodeIds.Contains(vertexDegreeDistribution[randomIndex]))
                        randomIndex = _random.Next(vertexDegreeDistribution.Count - 1);

                    var newNeighbor = vertices.Single(e => e.Id == vertexDegreeDistribution[randomIndex]);
                    newNeighbor.AddNeighborBiDirection(vertex);
                    usedNodeIds.Add(vertexDegreeDistribution[randomIndex]);
                }

                for (int j = 0; j < m; j++)
                {
                    vertexDegreeDistribution.Add(vertex.Id);
                    vertexDegreeDistribution.Add(vertex.Neighbors[j].Id);
                }

                vertices.Add(vertex);
            }

            return new Graph(vertices);
        }

        /// <summary>
        /// Generate graph by Erdős–Rényi model
        /// </summary>
        /// <param name="n">Number of vertices</param>
        /// <param name="p">Probability</param>
        /// <returns></returns>
        public Graph GenerateErdosRenyiModel(int n, double p)
        {
            var vertices = Enumerable.Range(0, n).Select(id => new Vertex(id)).ToList();

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    var v1 = vertices[i];
                    var v2 = vertices[j];

                    if (v1.Neighbors.Contains(v2))
                        continue;
                    if (v1 == v2)
                        continue;
                    if (_random.NextDouble() < p)
                    {
                        v1.Neighbors.Add(v2);
                        v2.Neighbors.Add(v1);
                    }
                }
            }

            return new Graph(vertices);
        }

        /// <summary>
        /// Generate graph by Holme & Kim model
        /// </summary>
        /// <param name="n">Number of vertices</param>
        /// <param name="m0">Number of fundamental nodes</param>
        /// <param name="m">Number of edges for new created vertices</param>
        /// <para name="p">Probability of making a triad formation</para>
        /// <returns></returns>
        public Graph GenerateHolmeKimModel(int n, int m0, int m, double p)
        {
            var vertices = Enumerable.Range(0, m0).Select(id => new Vertex(id)).ToList();

            // create base graph (complete graph)
            for (int i = 0; i < m0; i++)
            {
                for (int j = 0; j < m0; j++)
                {
                    if (i == j)
                        continue;
                    vertices[i].AddNeighborBiDirection(vertices[j]);
                }
            }

            var vertexDegreeDistribution = new List<int>();
            foreach (var vertex in vertices)
            {
                for (int i = 0; i < vertex.Degree; i++)
                {
                    vertexDegreeDistribution.Add(vertex.Id);
                }
            }

            // add new nodes
            for (int i = m0; i < n; i++)
            {
                var vertex = new Vertex(m0 + i + 1);
                var usedNodeIds = new List<int>();

                Vertex newNeighbor = null;
                for (int j = 0; j < m; j++)
                {
                    if (j == 0 || p < _random.NextDouble())
                    {
                        int randomIndex = _random.Next(vertexDegreeDistribution.Count - 1);
                        while (usedNodeIds.Contains(vertexDegreeDistribution[randomIndex]))
                            randomIndex = _random.Next(vertexDegreeDistribution.Count - 1);

                         newNeighbor = vertices.Single(e => e.Id == vertexDegreeDistribution[randomIndex]);
                        bool isAdded = newNeighbor.AddNeighborBiDirection(vertex);
                        usedNodeIds.Add(vertexDegreeDistribution[randomIndex]);
                    }
                    else
                    {
                        if (newNeighbor == null) throw new Exception();
                        var possibleNeighborsToAdd = newNeighbor.Neighbors.Except(new[] { vertex }).ToList();
                        var otherNeighbor = possibleNeighborsToAdd[_random.Next(possibleNeighborsToAdd.Count)];
                        vertex.AddNeighborBiDirection(otherNeighbor);
                    }
                }

                for (int j = 0; j < m; j++)
                {
                    vertexDegreeDistribution.Add(vertex.Id);
                    vertexDegreeDistribution.Add(vertex.Neighbors[j].Id);
                }

                vertices.Add(vertex);
            }

            return new Graph(vertices);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">Number of vertices</param>
        /// <param name="m0">Number of fundamental nodes</param>
        /// <param name="m">Number of edges for new created vertices</param>
        /// <para name="p">Probability of making a triad formation</para>
        /// <returns></returns>
        public Graph GenerateBasicModel(int n, int m0, int m, double p)
        {
            var vertices = Enumerable.Range(0, m0).Select(id => new Vertex(id)).ToList();

            // create base graph (complete graph)
            for (int i = 0; i < m0; i++)
            {
                for (int j = 0; j < m0; j++)
                {
                    if (i == j)
                        continue;
                    vertices[i].AddNeighborBiDirection(vertices[j]);
                }
            }

            for (int i = m0; i < n; i++)
            {
                var newVertex = new Vertex(i);
                var randomNode = vertices[_random.Next(vertices.Count)];
                randomNode.AddNeighborBiDirection(newVertex);
                Vertex r1, r2;
                if (_random.NextDouble() < p)
                {
                    var neighborhood = randomNode.Neighbors.Except(new[] {randomNode}).ToList();
                    if (neighborhood.Count == 0)
                    {
                        var randomNeighbor2 = vertices.Except(new[] { randomNode }).ToList()[_random.Next(vertices.Count - 1)];
                        randomNeighbor2.AddNeighborBiDirection(newVertex);
                    }
                    var randomNeighbor = neighborhood[_random.Next(randomNode.Degree-1)];
                    randomNeighbor.AddNeighborBiDirection(newVertex);
                    r1 = randomNeighbor;
                }
                else
                {
                    var randomNeighbor = vertices.Except(new[] { randomNode }).ToList()[_random.Next(vertices.Count - 1)];
                    randomNeighbor.AddNeighborBiDirection(newVertex);
                    r2 = randomNeighbor;
                }

                vertices.Add(newVertex);
            }

            return new Graph(vertices);
        }
    }
}
