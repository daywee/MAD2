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
        }
    }
}
