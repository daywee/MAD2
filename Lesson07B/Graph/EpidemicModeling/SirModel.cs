using System;
using System.Collections;
using System.Collections.Generic;
using MoreLinq;
using System.Linq;

namespace Lesson07B.Graph.EpidemicModeling
{
    public class SirModel
    {
        private readonly Random _random = new Random();

        /// <summary>
        /// Simulates Susceptible-Infected-Recovered (SIR) Model
        /// </summary>
        /// <param name="graph">Graph used for simulation</param>
        /// <param name="beta">Probability of transmitting infection in a unit of time</param>
        /// <param name="mu">Number of time units to recover infected individual</param>
        /// <param name="iterations">Number of iterations in simulation</param>
        public List<(int SusceptibleCount, int InfectedCount, int RecoveredCount)> Simulate(Graph graph, double beta, int mu, int iterations)
        {
            var models = graph.Vertices.Select(e => new SirModelWrapper(e, SirState.Susceptible)).ToList();
            models[0].State = SirState.Infected;
            var sirDistribution = new List<(int, int, int)>(); // S-I-R vertex counts

            for (int iteration = 0; iteration < iterations; iteration++)
            {
                models
                    .Where(e => e.State == SirState.Infected)
                    .ForEach(e => e.InfectedDuration++);

                models
                    .Where(e => e.State == SirState.Infected && e.InfectedDuration >= mu)
                    .ForEach(e =>
                    {
                        e.InfectedDuration = 0;
                        e.State = SirState.Recovered;
                    });

                var preservedModels = models.Select(e => new SirModelWrapper(e)).ToList();
                models
                    .Where(e => e.State == SirState.Susceptible)
                    .ForEach(e =>
                    {
                        var neighbors = e.Vertex.Neighbors.Select(n => preservedModels.Single(p => p.Vertex.Id == n.Id));
                        int infectedNeighborsCount = neighbors.Count(n => n.State == SirState.Infected);
                        for (int i = 0; i < infectedNeighborsCount; i++)
                        {
                            if (beta > _random.NextDouble())
                            {
                                e.State = SirState.Infected;
                                break;
                            }
                        }
                    });

                var susceptible = models.Where(e => e.State == SirState.Susceptible).Select(e => e.Vertex.Id).Count();
                var infected = models.Where(e => e.State == SirState.Infected).Select(e => e.Vertex.Id).Count();
                var recovered = models.Where(e => e.State == SirState.Recovered).Select(e => e.Vertex.Id).Count();

                sirDistribution.Add((susceptible, infected, recovered));
                //PrintSimulationInfo(models, iteration);
            }

            return sirDistribution;
        }

        private void PrintSimulationInfo(IList<SirModelWrapper> models, int iteration)
        {
            var susceptible = models.Where(e => e.State == SirState.Susceptible).Select(e => e.Vertex.Id);
            var infected = models.Where(e => e.State == SirState.Infected).Select(e => e.Vertex.Id);
            var recovered = models.Where(e => e.State == SirState.Recovered).Select(e => e.Vertex.Id);

            Console.WriteLine($"Iteration: {iteration}");
            Console.WriteLine($"Susceptible: {string.Join(", ", susceptible)}");
            Console.WriteLine($"Infected: {string.Join(", ", infected)}");
            Console.WriteLine($"Recovered: {string.Join(", ", recovered)}");
        }

        private class SirModelWrapper
        {
            public Vertex Vertex { get; }
            public SirState State { get; set; }
            public int InfectedDuration { get; set; }

            public SirModelWrapper(Vertex vertex, SirState state)
            {
                Vertex = vertex;
                State = state;
            }

            public SirModelWrapper(SirModelWrapper wrapper)
            {
                Vertex = wrapper.Vertex;
                State = wrapper.State;
                InfectedDuration = wrapper.InfectedDuration;
            }
        }

        private enum SirState
        {
            Susceptible,
            Infected,
            Recovered,
        }
    }
}
