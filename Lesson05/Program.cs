using Lesson05.Graph;

namespace Lesson05
{
    class Program
    {
        static void Main(string[] args)
        {
            var gl = new GraphLoader();
            var graph = gl.LoadFromCsvFile("../../../Datasets/KarateClub/KarateClub.csv");
            graph.NormalizeIds();
            var adjacencyMatrix = graph.GetAdjacencyMatrix();
            var cosineSimilarityMatrix = adjacencyMatrix.GetCosineSimilarityMatrix();

            var exporter = new GraphExporter();
            //exporter.ExportToCsv(basic, "../../../Datasets/basicGraph.csv");
            //exporter.ExportToCsv(holmeKim, "../../../Datasets/holmeKim.csv");
        }
    }
}
