using Lesson09.Graph;
using MoreLinq;

namespace Lesson09
{
    class Program
    {
        static void Main(string[] args)
        {
            var gl = new GraphLoader();
            var florentine = gl.LoadMultilayerGraph("../../../Datasets/Florentine/florentine.mpx");

            florentine.ActorNames.ForEach(e => florentine.PrintInfoAboutActor(e));

            var gc = new GraphConverter();
            var acmTemporal = gl.LoadTemporalGraph("../../../Datasets/ACM Hypertex 2009/ht09_contact_list.tsv");
            var acmMultilayer = gc.ConvertTemporalToMultilayerGraph(acmTemporal, 5);

            acmMultilayer.ActorNames.ForEach(e => acmMultilayer.PrintInfoAboutActor(e));
        }
    }
}
