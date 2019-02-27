using System.Collections.Generic;

namespace Lesson03.Graph
{
    public class Vertex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float ClusteringCoefficient { get; set; }

        public List<Vertex> Neighbors { get; set; } = new List<Vertex>();
        public int Degree => Neighbors.Count;

        public Vertex(int id)
        {
            Id = id;
        }

        public Vertex(Vertex vertex)
        {
            Id = vertex.Id;
            Name = vertex.Name;
        }

        public void AddNeighborBiDirection(Vertex vertex)
        {
            if (!Neighbors.Contains(vertex))
                Neighbors.Add(vertex);
            if (!vertex.Neighbors.Contains(this))
                vertex.Neighbors.Add(this);
        }

        public void AddNeighbor(Vertex vertex)
        {
            if (!Neighbors.Contains(vertex))
                Neighbors.Add(vertex);
        }

        public override string ToString()
        {
            return $"Vertex {Id}: {Name} (degree: {Degree})";
        }
    }
}
