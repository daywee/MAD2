namespace Lesson03
{
    public class Row : IVectorConvertible
    {
        public int Id { get; set; }
        public double SepalLength { get; set; }
        public double SepalWidth { get; set; }
        public double PetalLength { get; set; }
        public double PetalWidth { get; set; }
        public IrisType IrisType { get; set; }
        public Vector ToVector()
        {
            return new Vector(new[] { SepalLength, SepalWidth, PetalLength, PetalWidth });
        }
    }

    public enum IrisType
    {
        Setosa,
        Versicolor,
        Virginica
    }
}
