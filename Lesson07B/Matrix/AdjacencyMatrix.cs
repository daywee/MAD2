using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson07B.Matrix
{
    public class AdjacencyMatrix : Matrix
    {
        public AdjacencyMatrix(int dimension) 
            : base(dimension)
        {
        }

        public SimilarityMatrix GetCosineSimilarityMatrix()
        {
            var similarityMatrix = new SimilarityMatrix(Dimension);
            int GetDegree(int id)
            {
                int degree = 0;
                for (int i = 0; i < Dimension; i++)
                    degree += Convert.ToInt32(this[id, i]);
                return degree;
            }

            List<int> GetNeighborIndexes(int i)
            {
                var indexes = new List<int>();
                for (int d = 0; d < Dimension; d++)
                {
                    if (Convert.ToInt32(this[i, d]) == 1)
                        indexes.Add(d);
                }

                return indexes;
            }

            int GetNumberOfMutualNeighbors(int i, int j)
            {
                var neighborsI = GetNeighborIndexes(i);
                var neighborsJ = GetNeighborIndexes(j);

                return neighborsI.Intersect(neighborsJ).Count();
            }

            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    double n = GetNumberOfMutualNeighbors(i, j);
                    double d = Math.Sqrt(GetDegree(i) * GetDegree(j));
                    similarityMatrix[i, j] = n / d;
                }
            }

            return similarityMatrix;
        }
    }
}
