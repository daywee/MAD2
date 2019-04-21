using System;

namespace Lesson07B
{
    public static class Utils
    {
        public static double GaussianKernel(Row r1, Row r2, double sigma = 1)
        {
            var upper = (EuclidDistance(r1, r2));
            var lower = 2 * sigma;
            var result = Math.Exp(-(upper / lower));
            return result;
        }

        public static double Scalar(Row x, Row y)
        {
            double sum = 0;
            sum += x.SepalWidth * y.SepalWidth;
            sum += x.SepalLength * y.SepalLength;
            sum += x.PetalWidth * y.PetalWidth;
            sum += x.PetalLength * y.PetalLength;
            return sum;
        }

        public static double EuclidDistance(Row x, Row y)
        {
            double sum = 0;
            sum += Math.Pow(x.SepalWidth - y.SepalWidth, 2);
            sum += Math.Pow(x.SepalLength - y.SepalLength, 2);
            sum += Math.Pow(x.PetalWidth - y.PetalWidth, 2);
            sum += Math.Pow(x.PetalLength - y.PetalLength, 2);

            return sum;
        }
    }
}
