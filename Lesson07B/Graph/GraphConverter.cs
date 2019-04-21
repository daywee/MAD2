using System.Collections.Generic;
using System.Linq;

namespace Lesson07B.Graph
{
    public class GraphConverter
    {
        public Graph ConvertUsingERadius(List<Row> rows, double epsilon)
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

            return new Graph(vertices);
        }

        public Graph ConvertUsingKnn(List<Row> rows, int k)
        {
            var similarityMatrix = GetSimilarityMatrix(rows.Select(e => e.ToVector()).ToList());
            var vertices = rows.Select((_, id) => new Vertex(id)).ToList();

            for (int i = 0; i < rows.Count; i++)
            {
                var nearest = similarityMatrix.GetKNearest(i, k);
                foreach (var item in nearest)
                {
                    var v1 = vertices[i];
                    var v2 = vertices[item];
                    v1.Neighbors.Add(v2);
                    v2.Neighbors.Add(v1);
                    v1.Neighbors = v1.Neighbors.Distinct().ToList();
                    v2.Neighbors = v2.Neighbors.Distinct().ToList();
                }
            }

            vertices.ForEach(e => e.Neighbors = e.Neighbors.Distinct().ToList());

            return new Graph(vertices);
        }

        public Graph ConvertUsingERadiusWithKnn(List<Row> rows, double epsilon, int k)
        {
            var similarityMatrix = GetSimilarityMatrix(rows.Select(e => e.ToVector()).ToList());
            var vertices = rows.Select((_, id) => new Vertex(id)).ToList();

            for (int i = 0; i < similarityMatrix.Dimension; i++)
            {
                var verticesToAdd = new List<(Vertex, Vertex)>();
                for (int j = 0; j < similarityMatrix.Dimension; j++)
                {
                    if (i == j)
                        continue;

                    double similarity = similarityMatrix[i, j];
                    if (similarity > epsilon)
                    {
                        var v1 = vertices[i];
                        var v2 = vertices[j];
                        verticesToAdd.Add((v1, v2));
                    }
                }

                if (verticesToAdd.Count > k)
                {
                    // use KNN if more edges then K would be added
                    var nearest = similarityMatrix.GetKNearest(i, k);
                    foreach (var item in nearest)
                    {
                        var v1 = vertices[i];
                        var v2 = vertices[item];
                        v1.Neighbors.Add(v2);
                        v2.Neighbors.Add(v1);
                    }
                }
                else
                {
                    // otherwise use e-radius
                    foreach (var (v1, v2) in verticesToAdd)
                    {
                        v1.Neighbors.Add(v2);
                        v2.Neighbors.Add(v1);
                    }
                }
            }

            vertices.ForEach(e => e.Neighbors = e.Neighbors.Distinct().ToList());

            return new Graph(vertices);
        }

        private Matrix.Matrix GetSimilarityMatrix(List<Vector> vectors)
        {
            var similarityMatrix = new Matrix.Matrix(vectors.Count);
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
