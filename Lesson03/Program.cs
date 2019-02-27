using Lesson03.Graph;
using Lesson03.Graph.Clustering;

namespace Lesson03
{
    class Program
    {
        static void Main(string[] args)
        {
            var gl = new GraphLoader();
            var graph = gl.LoadFromCsvFile("../../../Datasets/KarateClub/KarateClub.csv");
            var kl = new KernighanLin();
            kl.Cluster(graph);
        }
    }
}
