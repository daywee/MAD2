using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson03
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
    }
}
