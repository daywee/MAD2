using System;
using System.Linq;

namespace Lesson08.Graph.Clustering
{
    public class KCoreClusterer
    {
        public void Cluster(Graph graph, int k)
        {
            var clone = graph.Clone();

            while (clone.Vertices.Any(e => e.Degree < k))
            {
                clone.Vertices.Where(e => e.Degree < k).ToList().ForEach(vertex =>
                {
                    vertex.Neighbors.ToList().ForEach(vertex.RemoveNeighborBiDirection);
                    clone.Vertices.Remove(vertex);
                });
            }

            Console.WriteLine($"Nodes for {k}-core: {string.Join(",", clone.Vertices.OrderBy(e => e.Id).Select(e => e.Id + 1))}");
        }
    }
}
