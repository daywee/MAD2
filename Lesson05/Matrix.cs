using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson05
{
    public class Matrix
    {
        public int Dimension { get; }

        private readonly double[,] _matrix;

        public Matrix(int dimension)
        {
            Dimension = dimension;
            _matrix = new double[dimension, dimension];
        }

        public double this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Dimension)
                    throw new IndexOutOfRangeException();

                if (y < 0 || y >= Dimension)
                    throw new IndexOutOfRangeException();

                return _matrix[x, y];
            }
            set
            {
                if (x < 0 || x >= Dimension)
                    throw new IndexOutOfRangeException();

                if (y < 0 || y >= Dimension)
                    throw new IndexOutOfRangeException();

                _matrix[x, y] = value;
                _matrix[y, x] = value;
            }
        }

        public IEnumerable<int> GetKNearest(int x, int k)
        {
            var values = new List<(int, double)>();
            for (int i = 0; i < Dimension; i++)
            {
                if (i == x)
                    continue;
                values.Add((i, this[x, i]));
            }

            return values
                .OrderByDescending(e => e.Item2)
                .Take(k)
                .Select(e => e.Item1);
        }

        public Matrix GetCosineSimilarityMatrix()
        {
            var similarityMatrix = new Matrix(Dimension);
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
        // todo: split matrix functions by interfaces
        public void Cluster()
        {
            (int I, int J) FindMostSimilarNodes()
            {
                double bestSimilarity = this[0, 0];
                int bestI = 0;
                int bestJ = 0;

                for (int i = 0; i < Dimension; i++)
                {
                    for (int j = 0; j < Dimension; j++)
                    {
                        if (i == j)
                            continue;
                        if (this[i, j] > bestSimilarity)
                        {
                            bestSimilarity = this[i, j];
                            bestI = i;
                            bestJ = j;
                        }
                    }
                }

                return (bestI, bestJ);
            }
        }

        // todo: Clustering class, bude mit adjacency matrix, k tomu si vytvori similarity, na tom najde most similar nody, pridam je do clusteru a upravim adjacency, opakuju
    }
}
