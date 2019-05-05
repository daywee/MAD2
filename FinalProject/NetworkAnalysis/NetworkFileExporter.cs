using FinalProject.NetworkAnalysis.CommunityDetection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FinalProject.NetworkAnalysis
{
    public static class NetworkFileExporter
    {
        public static void ExportToRScript(string path, Network network, IEnumerable<Community> communities)
        {
            var sb = new StringBuilder();
            var leftSideNodes = new List<int>();
            var rightSideNodes = new List<int>();
            var groups = communities.Select(e => string.Join(",", e.Nodes.Select(d => d.Id + 1)));
            var labels = network.Nodes.Select(e => $"'{e.Name ?? e.Id.ToString()}'");

            var usedNodes = new List<Node>();
            foreach (var n1 in network.Nodes)
            {
                usedNodes.Add(n1);
                foreach (var n2 in n1.Neighbors.Except(usedNodes))
                {
                    leftSideNodes.Add(n1.Id + 1);
                    rightSideNodes.Add(n2.Id + 1);
                }
            }

            var groupsAsCode = groups.Select(e => $"c({e})");

            sb.AppendLine("library(igraph)");
            sb.AppendLine($"e = cbind(c({string.Join(",", leftSideNodes)}),c({string.Join(",", rightSideNodes)}))");
            sb.AppendLine("network = graph_from_edgelist(e, directed=F)");
            sb.AppendLine($"groups = list({string.Join(",", groupsAsCode)})");
            sb.AppendLine($"labels = c({string.Join(",", labels)})");
            sb.AppendLine();
            sb.AppendLine("plot(network, mark.groups=groups, vertex.label=labels)");

            File.WriteAllText(path, sb.ToString());
        }
    }
}
