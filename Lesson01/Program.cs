using System.Linq;
using Lesson01.Graph;

namespace Lesson01
{
    class Program
    {
        static void Main(string[] args)
        {
            var dl = new DataLoader();
            var data = dl.LoadData("../../../Datasets/Iris/iris.data");
            //var data = dl.LoadData("../../../Datasets/Iris/iris.data").Take(10).ToList();
            var converter = new GraphConverter();
            var graph = converter.ERadiusConvert(data, 0.75);
            var exporter = new GraphExporter();
            //exporter.ExportToPajak(graph, "../../../Datasets/Iris/epsilonIris.net");
            exporter.ExportToCsv(graph, "../../../Datasets/Iris/epsilonIris.csv");
        }
    }
}
