using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Csv;
using FinalProject.NetworkAnalysis.CommunityDetection;

namespace FinalProject.NetworkAnalysis
{
    public class NetworkFileLoader
    {
        public (Network network, IncidenceMatrix incidenceMatrix) LoadHeroes(string path, int rowsToSkip = 0)
        {
            string csv = File.ReadAllText(path);

            var lines = CsvReader.ReadFromText(csv, new CsvOptions { RowsToSkip = rowsToSkip }).ToList();
            var nameNodeDict = new Dictionary<string, Node>();

            int id = 0;
            foreach (var line in lines)
            {
                for (int i = 0; i < 2; i++)
                {
                    string name = line.Values[i];
                    if (!nameNodeDict.ContainsKey(name))
                    {
                        nameNodeDict.Add(name, new Node(id++) { Name = name });
                    }
                }
            }

            var incidenceMatrix = new int[id, id];
            foreach (var line in lines)
            {
                // construct network
                var n1 = nameNodeDict[line.Values[0]];
                var n2 = nameNodeDict[line.Values[1]];
                n1.AddNeighborBiDirection(n2);

                // construct incidence matrix
                int x = n1.Id;
                int y = n2.Id;
                if (incidenceMatrix[x, y] == 0)
                {
                    incidenceMatrix[x, y] = 1;
                    incidenceMatrix[y, x] = 1;
                }
            }

            return (new Network(nameNodeDict.Values.ToList()), new IncidenceMatrix(incidenceMatrix));
        }

        public Network LoadFromCsvFile(string path, int firstId = 1, int rowsToSkip = 0)
        {
            string csv = File.ReadAllText(path);

            var data = csv.Split('\n')
                .Skip(rowsToSkip)
                .Select(line => line.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries))
                .Where(e => e.Length > 0)
                .Select(e => new[] { int.Parse(e[0]), int.Parse(e[1]) })
                .ToList();

            var allIds = data.SelectMany(e => e, (ints, i) => i).ToList();
            int minId = allIds.Min();
            int maxId = allIds.Max();

            var vertices = Enumerable.Range(minId, maxId + 1)
                .Select(e => new Node(e))
                .ToDictionary(e => e.Id);

            foreach (var row in data)
            {
                var a = vertices[row[0]];
                var b = vertices[row[1]];

                a.Neighbors.Add(b);
                b.Neighbors.Add(a);
            }

            if (minId != firstId)
            {
                int difference = minId - firstId;
                var verticesList = vertices.Values.ToList();
                foreach (var vertex in verticesList)
                {
                    vertex.Id -= difference;
                }
            }

            return new Network(vertices.Values.ToList());
        }

        public void ExportToREdgelist(string path, Network network)
        {
            var sb = new StringBuilder();
            var usedNodes = new List<Node>();

            foreach (var n1 in network.Nodes)
            {
                usedNodes.Add(n1);
                foreach (var n2 in n1.Neighbors.Except(usedNodes))
                {
                    sb.AppendLine($"{n1.Id - 1} {n2.Id - 1}");
                }
            }

            File.WriteAllText(path, sb.ToString());
        }

        public void ExportToRScript(string path, Network network, IEnumerable<Community> communities)
        {
            var sb = new StringBuilder();
            var leftSideNodes = new List<int>();
            var rightSideNodes = new List<int>();
            var groups = communities.Select(e => string.Join(",", e.Nodes.Select(d => d.Id)));

            var usedNodes = new List<Node>();
            foreach (var n1 in network.Nodes)
            {
                usedNodes.Add(n1);
                foreach (var n2 in n1.Neighbors.Except(usedNodes))
                {
                    leftSideNodes.Add(n1.Id);
                    rightSideNodes.Add(n2.Id);
                }
            }

            var groupsAsCode = groups.Select(e => $"c({e})");

            sb.AppendLine("library(igraph)");
            sb.AppendLine($"e = cbind(c({string.Join(",", leftSideNodes)}),c({string.Join(",", rightSideNodes)}))");
            sb.AppendLine("network = graph_from_edgelist(e, directed=F)");
            sb.AppendLine($"groups = list({string.Join(",", groupsAsCode)})");
            sb.AppendLine();
            sb.AppendLine("plot(network, mark.groups=groups)");

            File.WriteAllText(path, sb.ToString());
        }
    }
}
