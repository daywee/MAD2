using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.NetworkAnalysis
{
    public class Network
    {
        public IList<Node> Nodes { get; }
        public int Edges => Nodes.Select(e => e.Neighbors.Count).Sum() / 2;

        public Network(IList<Node> nodes)
        {
            Nodes = nodes;
        }

        public int CountComponents()
        {
            var visited = new List<Node>();
            var unvisited = Nodes.ToList();

            void VisitNode(Node node)
            {
                visited.Add(node);
                unvisited.Remove(node);
                foreach (var neighbor in node.Neighbors)
                {
                    if (!visited.Contains(neighbor))
                        VisitNode(neighbor);
                }
            }

            int noComponents = 0;
            while (unvisited.Any())
            {
                noComponents++;
                VisitNode(unvisited.First());
            }

            return noComponents;
        }

        public float GetGlobalClusteringCoefficient()
        {
            var sum = Nodes.Select(e => e.ClusteringCoefficient).Sum();
            return sum / Nodes.Count;
        }

        public void ComputeClusteringCoefficient()
        {
            foreach (var node in Nodes)
            {
                var neighborsNetwork = node.Neighbors.Except(new[] { node }).ToList();

                int n = node.Degree;
                int m = CountEdges(neighborsNetwork);

                if (node.Degree == 0 || node.Degree == 1)
                    node.ClusteringCoefficient = 0;
                else
                    node.ClusteringCoefficient = 2 * m / (float)(n * (n - 1));
            }
        }

        private int CountEdges(IList<Node> vertices)
        {
            int edges = vertices
                .Select(e => e.Neighbors.Intersect(vertices).Count()) // only count edges in subgraph
                .Sum();

            return edges / 2; // divide by two because a graph is unoriented
        }

        public float GetAverageDegree()
        {
            return (float)Nodes.Sum(e => e.Degree) / Nodes.Count;
        }

        public IncidenceMatrix GetIncidenceMatrix()
        {
            var incidenceMatrix = new int[Nodes.Count, Nodes.Count];

            foreach (var n1 in Nodes)
            {
                foreach (var n2 in n1.Neighbors)
                {
                    int x = n1.Id;
                    int y = n2.Id;

                    if (incidenceMatrix[x, y] == 0)
                    {
                        incidenceMatrix[x, y] = 1;
                        incidenceMatrix[y, x] = 1;
                    }
                }
            }

            return new IncidenceMatrix(incidenceMatrix);
        }

        public Network Clone()
        {
            var newVertices = Nodes.Select(e => new Node(e)).ToDictionary(e => e.Id);

            foreach (var vertex in Nodes)
            {
                var newVertex = newVertices[vertex.Id];
                foreach (var neighbor in vertex.Neighbors)
                {
                    if (newVertices.ContainsKey(neighbor.Id))
                    {
                        var newNeighbor = newVertices[neighbor.Id];
                        newVertex.AddNeighborBiDirection(newNeighbor);
                    }
                }
            }

            newVertices.Values.ForEach((e, i) => e.Id = i);

            return new Network(newVertices.Values.ToList());
        }
    }
}
