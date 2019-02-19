namespace Lesson01
{
    public class Row
    {
        public int Id { get; set; }
        public double SepalLength { get; set; }
        public double SepalWidth { get; set; }
        public double PetalLength { get; set; }
        public double PetalWidth { get; set; }
        public IrisType IrisType { get; set; }
    }

    public enum IrisType
    {
        Setosa,
        Versicolor,
        Virginica
    }
}
