using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson03.Graph.Clustering
{
    public class KernighanLin
    {
        private readonly Random _random = new Random(0);

        public void Cluster(Graph graph)
        {
            var shuffled = graph.Vertices.OrderBy(e => Guid.NewGuid()).ToList();

            var group1 = shuffled.Take(shuffled.Count / 2).ToList();
            var group2 = shuffled.Skip(shuffled.Count / 2).ToList();

            int initialCutSize = GetCutSize(group1, group2);
            int bestCutSize = int.MaxValue;
            List<Vertex> bestNewGroup1 = null;
            List<Vertex> bestNewGroup2 = null;
            Vertex bestSwappedFromGroup1 = null;
            Vertex bestSwappedFromGroup2 = null;
            var swappedVertices = new List<Vertex>();


            foreach (var v1 in group1)
            {
                foreach (var v2 in group2)
                {
                    var newGroup1 = group1.Except(new[] { v1 }).Concat(new[] { v2 }).ToList();
                    var newGroup2 = group1.Except(new[] { v2 }).Concat(new[] { v1 }).ToList();
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

            group1 = bestNewGroup1;
            group2 = bestNewGroup2;
            swappedVertices.Add(bestSwappedFromGroup1);
            swappedVertices.Add(bestSwappedFromGroup2);

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
    }
}
