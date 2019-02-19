using Lesson01.Graph;
using System.Collections.Generic;
using System.Linq;

namespace Lesson01
{
    public class GraphConverter
    {
        public Graph.Graph ERadiusConvert(List<Row> rows, double epsilon)
        {
            var similarityMatrix = GetSimilarityMatrix(rows.Select(e => e.ToVector()).ToList());
            var vertices = rows.Select((_, id) => new Vertex(id)).ToList();

            for (int i = 0; i < similarityMatrix.Dimension; i++)
            {
                for (int j = i + 1; j < similarityMatrix.Dimension; j++)
                {
                    double similarity = similarityMatrix[i, j];
                    if (similarity > epsilon)
                    {
                        var v1 = vertices[i];
                        var v2 = vertices[j];
                        v1.Neighbors.Add(v2);
                        v2.Neighbors.Add(v1);
                    }
                }
            }

            return new Graph.Graph(vertices);
        }

        private Matrix GetSimilarityMatrix(List<Vector> vectors)
        {
            var similarityMatrix = new Matrix(vectors.Count);
            for (int i = 0; i < vectors.Count; i++)
            {
                for (int j = i + 1; j < vectors.Count; j++)
                {
                    double similarity = vectors[i].GetGaussianKernelSimilarityTo(vectors[j]);
                    similarityMatrix[i, j] = similarity;
                }
            }

            return similarityMatrix;
        }
    }
}
