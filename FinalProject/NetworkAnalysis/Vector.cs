using System.Linq;
using System.Text;

namespace FinalProject.NetworkAnalysis
{
    public class Vector
    {
        private readonly double[] vector;

        public Vector(double[] vector)
        {
            this.vector = vector;
        }

        public double Average => vector.Average();

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < vector.Length; i++)
            {
                sb.Append("Vertex ");
                sb.Append(i + 1);
                sb.Append(": ");
                sb.AppendLine(vector[i].ToString("0.#######'"));
            }
            return sb.ToString();
        }
    }
}
