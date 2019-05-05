using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.NetworkAnalysis
{
    public class NetworkGenerator
    {
        private readonly Random _random = new Random();

        /// <summary>
        /// Generate graph by Barabási–Albert model with aging
        /// </summary>
        /// <param name="n">Number of vertices</param>
        /// <param name="m0">Number of fundamental nodes</param>
        /// <param name="m">Number of edges for new created vertices</param>
        /// <param name="v">Tunable parameter governing the dependence of the attachment probability on the node’s age.
        /// For big negative v, nodes prefer to attach oldest node. For big positive v, nodes prefer to attach newest node.</param>
        /// <returns></returns>
        public Network GenerateBarabasiAlbertModelWithAging(int n, int m0, int m, double v)
        {
            int ChooseIntervalIndex(double randomNumber, double[] intervals)
            {
                for (int i = 0; i < intervals.Length; i++)
                    if (randomNumber <= intervals[i])
                        return i;

                throw new InvalidOperationException();
            }

            var vertices = Enumerable.Range(0, m0).Select(id => new Node(id, id.ToString())).ToList();
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

                var vertex = new Node(currentAge, currentAge.ToString());

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

            return new Network(vertices);
        }
    }
}
