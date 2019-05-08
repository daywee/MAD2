using Csv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FinalProject.NetworkAnalysis
{
    public class NetworkFileLoader
    {
        public (Network network, IncidenceMatrix incidenceMatrix) LoadNetwork(string path, int rowsToSkip = 0)
        {
            string data = File.ReadAllText(path);

            List<string[]> lines;
            if (Path.GetExtension(path) == ".csv")
            {
                lines = CsvReader.ReadFromText(data, new CsvOptions { RowsToSkip = rowsToSkip })
                    .Select(e => new[] { e.Values[0], e.Values[1] })
                    .ToList();
            }
            else
            {
                // split on whitespace
                lines = data.Split('\n')
                    .Skip(rowsToSkip)
                    .Select(line => line.Split(new char[0] , StringSplitOptions.RemoveEmptyEntries))
                    .Where(e => e.Length > 0)
                    .ToList();
            }
            
            var nameNodeDict = new Dictionary<string, Node>();

            int id = 0;
            foreach (var line in lines)
            {
                for (int i = 0; i < 2; i++)
                {
                    string name = line[i];
                    if (!nameNodeDict.ContainsKey(name))
                    {
                        nameNodeDict.Add(name, new Node(id++, name));
                    }
                }
            }

            var incidenceMatrix = new int[id, id];
            foreach (var line in lines)
            {
                // construct network
                var n1 = nameNodeDict[line[0]];
                var n2 = nameNodeDict[line[1]];
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
    }
}
