using Lesson04.Graph;

namespace Lesson04
{
    class Program
    {
        static void Main(string[] args)
        {
            var gg = new GraphGenerator();
            //var basic = gg.GenerateBasicModel(1000, 10, 2, 0.97);
            var holmeKim = gg.GenerateHolmeKimModel(300, 10, 2, 0.9);

            var exporter = new GraphExporter();
            //exporter.ExportToCsv(basic, "../../../Datasets/basicGraph.csv");
            exporter.ExportToCsv(holmeKim, "../../../Datasets/holmeKim.csv");
        }
    }
}
