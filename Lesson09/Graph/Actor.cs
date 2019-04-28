using System.Collections.Generic;
using System.Linq;

namespace Lesson09.Graph
{
    public class Actor
    {
        public string Name { get; set; }
        public Dictionary<string, List<Actor>> NeighborActors { get; set; } // (layer, neighboring actors)

        /// <summary>
        /// Degree centrality of a actor
        /// </summary>
        public int Degree => NeighborActors.Select(e => e.Value.Count).Sum();
        public IEnumerable<Actor> Neighbors => NeighborActors.SelectMany(e => e.Value, (pair, actor) => actor).Distinct();
        public IEnumerable<string> NeighborNames => Neighbors.Select(e => e.Name);

        /// <summary>
        /// Neighborhood centrality of a actor
        /// </summary>
        public int Neighborhood => Neighbors.Count();

        public Actor(string name, IEnumerable<string> layers)
        {
            Name = name;
            NeighborActors = layers.ToDictionary(e => e, _ => new List<Actor>());
        }

        public void AddActorBiDirection(Actor actor, string layer)
        {
            AddActor(actor, layer);
            actor.AddActor(this, layer);
        }

        public void AddActor(Actor actor, string layer)
        {
            if (!NeighborActors[layer].Contains(actor))
                NeighborActors[layer].Add(actor);
        }
    }
}