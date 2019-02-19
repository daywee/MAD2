using Lesson01.Graph;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lesson01
{
    public class GraphConverter
    {
        public Graph.Graph ERadiusConvert(List<Row> rows, double epsilon)
        {
            var rowsHandled = new List<Row>();
            var vertices = rows.Select(e => new Vertex(e.Id)).ToList();

            foreach (var r1 in rows)
            {
                foreach (var r2 in rows)
                {
                    if (r1 == r2)
                        continue;
                    if (rowsHandled.Contains(r2))
                        continue;

                    var similarity = Utils.GaussianKernel(r1, r2);
                    if (similarity > epsilon)
                    {
                        var v1 = vertices.Single(e => e.Id == r1.Id);
                        var v2 = vertices.Single(e => e.Id == r2.Id);
                        if (v1.Neighbors.Contains(v2))
                            Debugger.Break();
                        v1.Neighbors.Add(v2);
                        v2.Neighbors.Add(v1);
                    }

                    //Console.WriteLine(distance);
                }

                rowsHandled.Add(r1);
            }

            var graph = new Graph.Graph(vertices);
            return graph;
        }
    }
}
