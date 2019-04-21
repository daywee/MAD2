using Lesson07B.Graph;

namespace Lesson07B
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
        }
    }
}
