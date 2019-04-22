using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson08.Graph
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
        /// Generate graph by Barabási–Albert model with aging
        /// </summary>
        /// <param name="n">Number of vertices</param>
        /// <param name="m0">Number of fundamental nodes</param>
        /// <param name="m">Number of edges for new created vertices</param>
        /// <param name="v">Tunable parameter governing the dependence of the attachment probability on the node’s age.
        /// For big negative v, nodes prefer to attach oldest node. For big positive v, nodes prefer to attach newest node.</param>
        /// <returns></returns>
        public Graph GenerateBarabasiAlbertModelWithAging(int n, int m0, int m, double v)
        {
            int ChooseIntervalIndex(double randomNumber, double[] intervals)
            {
                for (int i = 0; i < intervals.Length; i++)
                    if (randomNumber <= intervals[i])
                        return i;

                throw new InvalidOperationException();
            }

            var vertices = Enumerable.Range(0, m0).Select(id => new Vertex(id)).ToList();
            var nodesAge = vertices.ToDictionary(e => e.Id, e => e.Id); // (id, age)

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

            // add new nodes
            for (int currentAge = m0; currentAge < n; currentAge++)
            {
                var probabilities = vertices.Select(e => e.Degree * Math.Pow(currentAge - nodesAge[e.Id], -v)).ToList();
                var probabilityIntervals = Enumerable.Range(0, probabilities.Count)
                    .Select(i => probabilities.Take(i + 1).Sum())
                    .ToArray();

                var vertex = new Vertex(currentAge);

                var usedNodeIds = new List<int>();
                for (int j = 0; j < m; j++)
                {
                    int randomIndex = ChooseIntervalIndex(_random.NextDouble() * probabilityIntervals.Last(), probabilityIntervals);
                    while (usedNodeIds.Contains(randomIndex))
                        randomIndex = ChooseIntervalIndex(_random.NextDouble() * probabilityIntervals.Last(), probabilityIntervals);

                    var newNeighbor = vertices.Single(e => e.Id == randomIndex);
                    newNeighbor.AddNeighborBiDirection(vertex);
                    usedNodeIds.Add(randomIndex);
                }


                vertices.Add(vertex);
                nodesAge[vertex.Id] = currentAge;
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
                    var neighborhood = randomNode.Neighbors.Except(new[] { randomNode }).ToList();
                    if (neighborhood.Count == 0)
                    {
                        var randomNeighbor2 = vertices.Except(new[] { randomNode }).ToList()[_random.Next(vertices.Count - 1)];
                        randomNeighbor2.AddNeighborBiDirection(newVertex);
                    }
                    var randomNeighbor = neighborhood[_random.Next(randomNode.Degree - 1)];
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">Number of vertices</param>
        /// <returns></returns>
        public Graph GenerateLinkSelectionModel(int n)
        {
            var vertices = new List<Vertex> { new Vertex(0), new Vertex(1) };
            var edges = new List<(Vertex, Vertex)> { (vertices[0], vertices[1]) };
            vertices[0].AddNeighborBiDirection(vertices[1]);

            for (int i = 2; i < n; i++)
            {
                var newVertex = new Vertex(i);

                var randomEdge = edges[_random.Next(edges.Count - 1)];

                if (_random.NextDouble() > 0.5)
                {
                    newVertex.AddNeighborBiDirection(randomEdge.Item1);
                    edges.Add((newVertex, randomEdge.Item1));
                }
                else
                {
                    newVertex.AddNeighborBiDirection(randomEdge.Item2);
                    edges.Add((newVertex, randomEdge.Item2));
                }

                vertices.Add(newVertex);
            }

            return new Graph(vertices);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">Number of vertices</param>
        /// <param name="p">Probability of connecting a new vertex X with randomly chosen vertex V. 1 - p probability of connecting with neighbor V</param>
        /// <returns></returns>
        public Graph GenerateCopyingModel(int n, double p)
        {
            var vertices = new List<Vertex> { new Vertex(0), new Vertex(1) };
            vertices[0].AddNeighborBiDirection(vertices[1]);

            for (int i = 2; i < n; i++)
            {
                var newVertex = new Vertex(i);

                var randomConnection = vertices[_random.Next(vertices.Count - 1)];

                if (_random.NextDouble() < p)
                {
                    randomConnection.AddNeighborBiDirection(newVertex);
                }
                else
                {
                    var randomNeighborOfRandomConnection = randomConnection.Neighbors[_random.Next(randomConnection.Degree - 1)];
                    randomNeighborOfRandomConnection.AddNeighborBiDirection(newVertex);
                }

                vertices.Add(newVertex);
            }

            return new Graph(vertices);
        }
    }
}
