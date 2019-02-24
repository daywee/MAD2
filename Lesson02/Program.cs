using Lesson02.Graph;

namespace Lesson02
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new GraphGenerator();
            var ba = generator.GenerateBarabasiAlbertModel(1000, 10, 2);
            var er = generator.GenerateErdosRenyiModel(1000, 0.00398);

            var baRnsSample = ba.CreateRnsSample(0.15);
            var baRdnSample = ba.CreateRdnSample(0.15);

            var erRnsSample = er.CreateRnsSample(0.15);
            var erRdnSample = er.CreateRdnSample(0.15);

            var exporter = new GraphExporter();
            exporter.ExportDegreeToCsv(ba, "../../../Exports/ba.csv");
            exporter.ExportDegreeToCsv(er, "../../../Exports/er.csv");
            exporter.ExportDegreeToCsv(baRnsSample, "../../../Exports/baRnsSample.csv");
            exporter.ExportDegreeToCsv(baRdnSample, "../../../Exports/baRdnSample.csv");
            exporter.ExportDegreeToCsv(erRnsSample, "../../../Exports/erRnsSample.csv");
            exporter.ExportDegreeToCsv(erRdnSample, "../../../Exports/erRdnSample.csv");
        }
    }
}
