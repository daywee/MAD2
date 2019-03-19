using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson05.Graph.Clustering
{
    public class KernighanLin
    {
        private readonly Random _random = new Random(0);

        public Graph Cluster(Graph graph)
        {
            var clone = graph.Clone();

            // divide vertices into two random groups
            var shuffled = clone.Vertices.OrderBy(e => _random.Next()).ToList();
            var group1 = shuffled.Take(shuffled.Count / 2).ToList();
            var group2 = shuffled.Skip(shuffled.Count / 2).ToList();

            int initialCutSize = GetCutSize(group1, group2);
            int bestRun = initialCutSize;
            int currentRun = bestRun;

            do
            {
                bestRun = currentRun;
                var allStates = new Dictionary<int, (List<Vertex>, List<Vertex>)>();
                var swappedVertices = new List<Vertex>();

                // steps
                while (swappedVertices.Count < shuffled.Count)
                {
                    int bestCutSize = int.MaxValue;
                    Vertex bestSwappedFromGroup1 = null;
                    Vertex bestSwappedFromGroup2 = null;
                    List<Vertex> bestNewGroup1 = null;
                    List<Vertex> bestNewGroup2 = null;

                    foreach (var v1 in group1)
                    {
                        if (swappedVertices.Contains(v1))
                            continue;
                        foreach (var v2 in group2)
                        {
                            if (swappedVertices.Contains(v2))
                                continue;

                            var newGroup1 = group1.Except(new[] { v1 }).Concat(new[] { v2 }).ToList();
                            var newGroup2 = group2.Except(new[] { v2 }).Concat(new[] { v1 }).ToList();
                            int newCutSize = GetCutSize(newGroup1, newGroup2);
                            if (newCutSize < bestCutSize)
                            {
                                bestCutSize = newCutSize;
                                bestNewGroup1 = newGroup1;
                                bestNewGroup2 = newGroup2;
                                bestSwappedFromGroup1 = v1;
                                bestSwappedFromGroup2 = v2;
                            }
                        }
                    }

                    allStates[bestCutSize] = (bestNewGroup1, bestNewGroup2);
                    group1 = bestNewGroup1;
                    group2 = bestNewGroup2;
                    swappedVertices.Add(bestSwappedFromGroup1);
                    swappedVertices.Add(bestSwappedFromGroup2);
                }

                var bestState = allStates.OrderBy(e => e.Key).First();
                currentRun = bestState.Key;
                var (g1, g2) = bestState.Value;
                group1 = g1;
                group2 = g2;
            } while (currentRun < bestRun);

            RemoveClusteredEdges(group1, group2);

            return new Graph(group1.Concat(group2).ToList());
        }

        private int GetCutSize(List<Vertex> firstGroup, List<Vertex> secondGroup)
        {
            int cutSize = 0;
            foreach (var v1 in firstGroup)
            {
                foreach (var neighbor in v1.Neighbors)
                {
                    if (secondGroup.Contains(neighbor))
                        cutSize++;
                }
            }

            return cutSize;
        }

        private void RemoveClusteredEdges(List<Vertex> group1, List<Vertex> group2)
        {
            foreach (var vertex in group1)
            {
                var mutualVertices = vertex.Neighbors.Intersect(group2).ToList();
                foreach (var v in mutualVertices)
                {
                    vertex.RemoveNeighborBiDirection(v);
                }
            }
        }
    }
}
