using System.Collections.Generic;
using FinalProject.NetworkAnalysis;
using FinalProject.NetworkAnalysis.CommunityDetection;

namespace FinalProject.Services.MainForm
{
    public class NetworkWrapper
    {
        public string Name { get; set; }
        public Network Network { get; set; }
        public IncidenceMatrix IncidenceMatrix { get; set; }
        public DistanceMatrix DistanceMatrix { get; set; }
        public NetworkStats Stats { get; set; } = new NetworkStats();
        public List<Community> Communities { get; set; } = new List<Community>();

        public NetworkWrapper(string name, Network network, IncidenceMatrix incidenceMatrix)
        {
            Name = name;
            Network = network;
            IncidenceMatrix = incidenceMatrix;
        }
    }
}
