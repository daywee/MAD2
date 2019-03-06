using Lesson04.Graph;
using Lesson04.Graph.Clustering;

namespace Lesson04
{
    class Program
    {
        static void Main(string[] args)
        {
            var gl = new GraphLoader();
            var graph = gl.LoadFromCsvFile("../../../Datasets/KarateClub/KarateClub.csv");
            var kl = new KernighanLin();
            var clusteredGraph = kl.Cluster(graph);
            var exporter = new GraphExporter();
            exporter.ExportToCsv(clusteredGraph, "../../../Datasets/KarateClub/KarateClubClustered.csv");
        }
    }
}
