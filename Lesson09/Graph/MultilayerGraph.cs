using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson09.Graph
{
    public class MultilayerGraph
    {
        public IEnumerable<string> ActorNames => _actors.Keys;
        public IEnumerable<string> LayerNames { get; }

        private readonly Dictionary<string, Actor> _actors; // (name, Actor)

        public MultilayerGraph(List<string> layers, List<string> actors, List<(string a1, string a2, string layer)> edges)
        {
            _actors = actors.ToDictionary(e => e, e => new Actor(e, layers));
            LayerNames = layers;

            foreach (var edge in edges)
            {
                var a1 = _actors[edge.a1];
                var a2 = _actors[edge.a2];
                a1.AddActorBiDirection(a2, edge.layer);
            }
        }

        public void PrintInfoAboutActor(string actorName)
        {
            if (!_actors.ContainsKey(actorName))
                throw new Exception($"Actor '{actorName}' not found");

            var actor = _actors[actorName];
            Console.WriteLine("=======================================================");
            Console.WriteLine($"Name: {actor.Name}");
            Console.WriteLine($"Degree: {actor.Degree}");
            Console.WriteLine($"Neighbors: {string.Join(", ", actor.NeighborNames)}");
            Console.WriteLine($"Neighborhood: {actor.Neighborhood}");
        }
    }
}
