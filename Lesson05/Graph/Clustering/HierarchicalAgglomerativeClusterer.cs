using Lesson05.Matrix;
using MoreLinq;
using System;
using System.Linq;

namespace Lesson05.Graph.Clustering
{
    public class HierarchicalAgglomerativeClusterer
    {
        public void Cluster(Graph graph)
        {
            var clone = graph.Clone();
            foreach (var vertex in clone.Vertices)
                vertex.IdsBeforeMerge.Add(vertex.Id);

            while (clone.Vertices.Count > 1)
            {
                var adjacencyMatrix = clone.GetAdjacencyMatrix();
                var cosineSimilarityMatrix = adjacencyMatrix.GetCosineSimilarityMatrix();

                var similar = FindMostSimilarNodes(cosineSimilarityMatrix);
                clone.MergeVertices(similar.I, similar.J);
                clone.NormalizeIds();

                Console.WriteLine("=========================================================");
                Console.WriteLine($"Iteration: {graph.Vertices.Count - clone.Vertices.Count}");
                PrintClusters(clone);
            }
        }

        private void PrintClusters(Graph graph)
        {
            graph.Vertices.ForEach((vertex, i) =>
            {
                Console.WriteLine($"Cluster {i}: {string.Join(",", vertex.IdsBeforeMerge.Select(e => e + 1).OrderBy(e => e))}");
            });
        }

        private (int I, int J) FindMostSimilarNodes(SimilarityMatrix matrix)
        {
            double bestSimilarity = matrix[0, 1];
            int bestI = 0;
            int bestJ = 1;

            for (int i = 0; i < matrix.Dimension; i++)
            {
                for (int j = 0; j < matrix.Dimension; j++)
                {
                    if (i == j)
                        continue;
                    if (matrix[i, j] > bestSimilarity)
                    {
                        bestSimilarity = matrix[i, j];
                        bestI = i;
                        bestJ = j;
                    }
                }
            }

            return (bestI, bestJ);
        }
    }
}
