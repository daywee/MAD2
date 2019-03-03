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
            var graphER = converter.ConvertUsingERadius(data, 0.75);
            var graphKnn = converter.ConvertUsingKnn(data, 5);
            var graphERKnn = converter.ConvertUsingERadiusWithKnn(data, 0.75, 5);
            var exporter = new GraphExporter();
            //exporter.ExportToPajak(graph, "../../../Datasets/Iris/epsilonIris.net");
            exporter.ExportToCsv(graphER, "../../../Datasets/Iris/epsilonIris.csv");
            exporter.ExportToCsv(graphKnn, "../../../Datasets/Iris/knnIris.csv");
            exporter.ExportToCsv(graphERKnn, "../../../Datasets/Iris/epsilonKnnIris.csv");
        }
    }
}
