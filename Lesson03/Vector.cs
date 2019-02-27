using System;

namespace Lesson03
{
    public class Vector
    {
        public int Dimension { get; set; }

        private readonly double[] _x;

        public Vector(double[] vector)
        {
            if (vector.Length <= 0)
                throw new InvalidOperationException("Dimension cannot be 0 or negative");

            Dimension = vector.Length;
            _x = vector;
        }

        public Vector(int dimension)
        {
            if (dimension <= 0)
                throw new InvalidOperationException("Dimension cannot be 0 or negative");

            Dimension = dimension;
            _x = new double[dimension];
        }

        public double this[int index]
        {
            get => _x[index];
            set => _x[index] = value;
        }

        public double GetGaussianKernelSimilarityTo(Vector vector, double sigma = 1)
        {
            if (Dimension != vector.Dimension)
                throw new InvalidOperationException("Vectors must have same dimension");

            double distance = 0;
            for (int i = 0; i < Dimension; i++)
                distance += Math.Pow(this[i] - vector[i], 2);

            double result = Math.Exp(-distance / 2 * sigma);
            return result;
        }
    }
}
