using Lesson07B.Graph;
using Lesson07B.Graph.EpidemicModeling;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MoreLinq;

namespace Lesson07B
{
    class Program
    {
        static void Main(string[] args)
        {
            var gl = new GraphLoader();
            var karate = gl.LoadFromCsvFile("../../../Datasets/KarateClub/KarateClub.csv");
            var erdos = gl.LoadFromEdgeListFile("../../../Datasets/Lesson07B/erdos_export");
            var barabasi = gl.LoadFromEdgeListFile("../../../Datasets/Lesson07B/barabasi_export");
            var airports = gl.LoadFromEdgeListFile("../../../Datasets/Lesson07B/airports_export");

            var sm = new SirModel();
            double beta = 0.3;
            int mu = 5;
            int iterations = 30;
            var karateSirDistribution = sm.Simulate(karate, beta, mu, iterations);
            var erdosSirDistribution = sm.Simulate(erdos, beta, mu, iterations);
            var barabasiSirDistribution = sm.Simulate(barabasi, beta, mu, iterations);
            var airportsSirDistribution = sm.Simulate(airports, beta, mu, iterations);

            ExportDistributionToCsv(karateSirDistribution, "../../../Exports/KarateClubSirDistribution.csv");
            ExportDistributionToCsv(erdosSirDistribution, "../../../Exports/ErdosSirDistribution.csv");
            ExportDistributionToCsv(barabasiSirDistribution, "../../../Exports/BarabasiSirDistribution.csv");
            ExportDistributionToCsv(airportsSirDistribution, "../../../Exports/AirportsSirDistribution.csv");
        }

        static void ExportDistributionToCsv(IEnumerable<(int s, int i, int r)> distribution, string path)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Iteration,Susceptible,Infected,Recovered");
            distribution.ForEach((d, iteration) => sb.AppendLine($"{iteration},{d.s},{d.i},{d.r}"));

            File.WriteAllText(path, sb.ToString());
        }
    }
}
