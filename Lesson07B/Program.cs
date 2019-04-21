using Lesson07B.Graph;
using Lesson07B.Graph.EpidemicModeling;

namespace Lesson07B
{
    class Program
    {
        static void Main(string[] args)
        {
            var gl = new GraphLoader();
            var graph = gl.LoadFromCsvFile("../../../Datasets/KarateClub/KarateClub.csv");
            var sm = new SirModel();
            sm.Simulate(graph, 0.5, 3, 10);
        }
    }
}
