using System;
using System.IO;
using System.Linq;

namespace Lesson05.Graph
{
    public class GraphLoader
    {
        /// <summary>
        /// Loads graph from csv containing edges between nodes
        /// </summary>
        /// <param name="csv">In-memory csv representation</param>
        /// <returns></returns>
        public Graph LoadFromCsv(string csv)
        {
            var data = csv.Split('\n')
                .Select(line => line.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries))
                .Where(e => e.Length > 0)
                .Select(e => new[] { int.Parse(e[0]), int.Parse(e[1]) })
                .ToList();

            var maxIndex = data.SelectMany(e => e, (ints, i) => i).Max();

            var vertices = Enumerable.Range(1, maxIndex)
                .Select(e => new Vertex(e))
                .ToDictionary(e => e.Id);

            foreach (var row in data)
            {
                var a = vertices[row[0]];
                var b = vertices[row[1]];

                a.Neighbors.Add(b);
                b.Neighbors.Add(a);
            }

            return new Graph(vertices.Values.ToList());
        }

        public Graph LoadFromCsvFile(string path)
        {
            string csv = File.ReadAllText(path);

            var data = csv.Split('\n')
                .Select(line => line.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries))
                .Where(e => e.Length > 0)
                .Select(e => new[] { int.Parse(e[0]), int.Parse(e[1]) })
                .ToList();

            var maxIndex = data.SelectMany(e => e, (ints, i) => i).Max();

            var vertices = Enumerable.Range(1, maxIndex)
                .Select(e => new Vertex(e))
                .ToDictionary(e => e.Id);

            foreach (var row in data)
            {
                var a = vertices[row[0]];
                var b = vertices[row[1]];

                a.Neighbors.Add(b);
                b.Neighbors.Add(a);
            }

            return new Graph(vertices.Values.ToList());
        }
    }
}
