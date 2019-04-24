using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lesson09.Graph
{
    class GraphExporter
    {
        // .net file extension
        public void ExportToPajak(Graph graph, string path)
        {
            var sb = new StringBuilder();

            sb.Append("*vertices ");
            sb.AppendLine(graph.Vertices.Count.ToString());

            foreach (var vertex in graph.Vertices)
            {
                sb.Append(vertex.Id);
                sb.Append(" ");
                sb.AppendLine(vertex.Name);
            }

            sb.AppendLine("*Arcs");

            sb.AppendLine("*Edges");
            var usedVertices = new List<Vertex>();

            foreach (var v1 in graph.Vertices)
            {
                usedVertices.Add(v1);
                foreach (var v2 in v1.Neighbors.Except(usedVertices))
                {
                    sb.Append(v1.Id);
                    sb.Append(" ");
                    sb.Append(v2.Id);
                    sb.AppendLine(" 1");
                }
            }

            File.WriteAllText(path, sb.ToString());
        }

        public void ExportToCsv(Graph graph, string path)
        {
            var sb = new StringBuilder();
            var usedVertices = new List<Vertex>();
            sb.AppendLine($"Source,Target");
            foreach (var v1 in graph.Vertices)
            {
                usedVertices.Add(v1);
                foreach (var v2 in v1.Neighbors.Except(usedVertices))
                {
                    sb.AppendLine($"{v1.Id},{v2.Id}");
                }
            }

            File.WriteAllText(path, sb.ToString());
        }

        public void ExportDegreeToCsv(Graph graph, string path)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Id,Degree");
            foreach (var v in graph.Vertices)
            {
                sb.AppendLine($"{v.Id},{v.Degree}");
            }

            File.WriteAllText(path, sb.ToString());
        }
    }
}
