using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson03.Graph
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

            var vertexIds = new List<int>();
            foreach (var vertex in vertices)
            {
                for (int i = 0; i < vertex.Degree; i++)
                {
                    vertexIds.Add(vertex.Id);
                }
            }

            // add new nodes
            for (int i = 0; i < n; i++)
            {
                var vertex = new Vertex(m0 + i + 1);
                var usedNodeIds = new List<int>();

                for (int j = 0; j < m; j++)
                {
                    int randomIndex = _random.Next(vertexIds.Count - 1);
                    while (usedNodeIds.Contains(vertexIds[randomIndex]))
                        randomIndex = _random.Next(vertexIds.Count - 1);

                    var newNeighbor = vertices.Single(e => e.Id == vertexIds[randomIndex]);
                    newNeighbor.AddNeighborBiDirection(vertex);
                    usedNodeIds.Add(vertexIds[randomIndex]);
                }

                for (int j = 0; j < m; j++)
                {
                    vertexIds.Add(vertex.Id);
                    vertexIds.Add(vertex.Neighbors[j].Id);
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
    }
}
