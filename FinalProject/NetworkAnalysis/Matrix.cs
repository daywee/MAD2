using System;
using System.Text;

namespace FinalProject.NetworkAnalysis
{
    public class Matrix
    {
        protected const int MaxInt = 99999; // custom max int which will not overflow

        protected readonly int[,] matrix;

        public Matrix(int[,] matrix)
        {
            this.matrix = matrix;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sb.Append(matrix[i, j]);
                    sb.Append(" ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    public class DistanceMatrix : Matrix
    {
        public DistanceMatrix(int[,] matrix)
            : base(matrix)
        {
        }

        // průměrná vzdálenost
        public Vector GetMeanDistanceVector()
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new InvalidOperationException("Matrix dimensions must have same length");

            int length = matrix.GetLength(0);
            var distanceVector = new double[length];

            for (int i = 0; i < length; i++)
            {
                double sum = 0;
                for (int j = 0; j < length; j++)
                {
                    if (i != j)
                        sum += matrix[i, j];
                }

                distanceVector[i] = sum / length;
            }

            return new Vector(distanceVector);
        }

        public double GetMeanDistance()
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new InvalidOperationException("Matrix dimensions must have same length");

            int length = matrix.GetLength(0);

            double sum = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (i != j)
                        sum += matrix[i, j];
                }

            }

            return sum / (length * (length - 1));
        }

        public int GetDiameter()
        {
            int length = matrix.GetLength(0);

            int max = 0;
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                    if (i != j && matrix[i, j] > max)
                        max = matrix[i, j];

            return max;
        }

        public Vector GetClosenessCentralityVector()
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new InvalidOperationException("Matrix dimensions must have same length");

            int length = matrix.GetLength(0);
            var closenessCentralityVector = new double[length];

            for (int i = 0; i < length; i++)
            {
                double sum = 0;
                for (int j = 0; j < length; j++)
                {
                    if (i != j)
                        sum += matrix[i, j];
                }

                closenessCentralityVector[i] = length / sum;
            }

            return new Vector(closenessCentralityVector);
        }
    }

    public class IncidenceMatrix : Matrix
    {
        public IncidenceMatrix(int[,] matrix)
            : base(matrix)
        {
        }

        public DistanceMatrix GetDistanceMatrix()
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new InvalidOperationException("Matrix dimensions must have same length");

            int matrixLength = matrix.GetLength(0);
            var distanceMatrix = new int[matrixLength, matrixLength];

            // prepare matrix for Floyd-Marshall
            for (int i = 0; i < matrixLength; i++)
                for (int j = 0; j < matrixLength; j++)
                    if (matrix[i, j] != 0)
                        distanceMatrix[i, j] = matrix[i, j];
                    else
                        distanceMatrix[i, j] = i == j ? 0 : MaxInt; // all diagonal vertices are set to 0, vertices which does not have an edge between them are set to infinity

            // Floyd-Marshall algorithm
            for (int k = 0; k < matrixLength; k++)
                for (int i = 0; i < matrixLength; i++)
                    for (int j = 0; j < matrixLength; j++)
                        if (distanceMatrix[i, j] > distanceMatrix[i, k] + distanceMatrix[k, j])
                            distanceMatrix[i, j] = distanceMatrix[i, k] + distanceMatrix[k, j];

            return new DistanceMatrix(distanceMatrix);
        }
    }
}
