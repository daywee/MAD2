using Lesson09.Graph;

namespace Lesson09
{
    class Program
    {
        static void Main(string[] args)
        {
            var gg = new GraphGenerator();
            var lsm = gg.GenerateLinkSelectionModel(500);
            var cm = gg.GenerateCopyingModel(500, 0.5);

            var exporter = new GraphExporter();
            exporter.ExportToCsv(lsm, "../../../Datasets/lsm.csv");
            exporter.ExportToCsv(cm, "../../../Datasets/cm.csv");

            var agingPositive = gg.GenerateBarabasiAlbertModelWithAging(10, 2, 1, 10);
            var agingNegative = gg.GenerateBarabasiAlbertModelWithAging(10, 2, 1, 10);
            exporter.ExportToCsv(agingPositive, "../../../Exports/barabasiAgingPositive.csv");
            exporter.ExportToCsv(agingNegative, "../../../Exports/barabasiAgingNegative.csv");
        }
    }
}
