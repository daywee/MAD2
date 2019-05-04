using System.Collections.Generic;
using System.Linq;

namespace FinalProject.NetworkAnalysis
{
    public class Network
    {
        public IList<Node> Nodes { get; }

        public Network(IList<Node> nodes)
        {
            Nodes = nodes;
        }

        public float GetGlobalClusteringCoefficient()
        {
            var sum = Nodes.Select(e => e.ClusteringCoefficient).Sum();
            return sum / Nodes.Count;
        }

        public float GetAverageDegree()
        {
            return (float)Nodes.Sum(e => e.Degree) / Nodes.Count;
        }

        public Network Clone()
        {
            var newVertices = Nodes.Select(e => new Node(e)).ToDictionary(e => e.Id);

            foreach (var vertex in Nodes)
            {
                var newVertex = newVertices[vertex.Id];
                foreach (var neighbor in vertex.Neighbors)
                {
                    var newNeighbor = newVertices[neighbor.Id];
                    newVertex.AddNeighborBiDirection(newNeighbor);
                }
            }

            return new Network(newVertices.Values.ToList());
        }
    }
}
