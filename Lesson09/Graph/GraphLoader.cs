using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lesson09.Graph
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

        /// <summary>
        /// Loads multilayer graph from multiplex (.mpx) format
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns></returns>
        public MultilayerGraph LoadMultilayerGraph(string path)
        {
            const string layersMark = "#LAYERS";
            const string actorsMark = "#ACTORS";
            const string edgesMark = "#EDGES";

            var mpx = File.ReadAllText(path).Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();

            IEnumerable<string> GetSection(string mark)
            {
                int index = mpx.IndexOf(mark);
                while (!string.IsNullOrWhiteSpace(mpx[++index]))
                {
                    yield return mpx[index];
                }
            }

            var layers = GetSection(layersMark)
                .Select(e => e.Split(',')[0])
                .ToList();

            var actors = GetSection(actorsMark)
                .Select(e => e.Split(',')[0])
                .ToList();

            var edges = GetSection(edgesMark)
                .Select(e => e.Split(','))
                .Select(e => (e[0], e[1], e[2]))
                .ToList();

            return new MultilayerGraph(layers, actors, edges);
        }

        /// <summary>
        /// Loads temporal graph from .tsv format
        /// Each line has the form (t i j), where i and j are vertex ids and t is the interval during which this edge was active
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns></returns>
        public TemporalGraph LoadTemporalGraph(string path)
        {
            var tsv = File.ReadAllText(path)
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var edgeList = tsv
                .Select(e => e.Split('\t'))
                .Select(e => (int.Parse(e[0]), int.Parse(e[1]), int.Parse(e[2])))
                .ToList();

            return new TemporalGraph(edgeList);
        }
    }
}
