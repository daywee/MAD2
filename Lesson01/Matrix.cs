using System;

namespace Lesson01
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
    }
}
