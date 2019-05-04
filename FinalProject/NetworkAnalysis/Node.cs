using System.Collections.Generic;

namespace FinalProject.NetworkAnalysis
{
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float ClusteringCoefficient { get; set; }

        public List<Node> Neighbors { get; set; } = new List<Node>();
        public int Degree => Neighbors.Count;

        public Node(int id)
        {
            Id = id;
        }

        public Node(Node node)
        {
            Id = node.Id;
            Name = node.Name;
        }

        public bool AddNeighbor(Node node)
        {
            if (!Neighbors.Contains(node))
            {
                Neighbors.Add(node);
                return true;
            }

            return false;
        }

        public bool AddNeighborBiDirection(Node node)
        {
            return AddNeighbor(node) && node.AddNeighbor(this);
        }

        public void RemoveNeighbor(Node node)
        {
            Neighbors.Remove(node);
        }

        public void RemoveNeighborBiDirection(Node node)
        {
            RemoveNeighbor(node);
            node.RemoveNeighbor(this);
        }

        public override string ToString()
        {
            return $"Node {Id}: {Name} (degree: {Degree})";
        }
    }
}
