using FinalProject.NetworkAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject
{
    public class MainFormController
    {
        public List<NetworkWrapper> Networks { get; set; } = new List<NetworkWrapper>();

        public event Action<NetworkWrapper> OnNetworkAdd;
        public event Action<NetworkWrapper> OnNetworkRemove;
        public event Action<NetworkWrapper> OnNetworkStatsUpdate;

        private readonly NetworkFileLoader _networkFileLoader = new NetworkFileLoader();

        public void LoadNetwork(string path)
        {
            var network = _networkFileLoader.LoadFromCsvFile(path, firstId: 0, skip: 0);
            string fileName = Path.GetFileName(path);

            var wrapper = new NetworkWrapper(fileName, network);
            Networks.Add(wrapper);
            OnNetworkAdd?.Invoke(wrapper);
            LoadStats(wrapper);
        }

        public void RemoveNetwork(string name)
        {
            var network = Networks.SingleOrDefault(e => e.Name == name);
            if (network != null)
            {
                Networks.Remove(network);
                OnNetworkRemove?.Invoke(network);
            }
        }

        private void LoadStats(NetworkWrapper network)
        {
            Task.Run(() =>
            {
                network.Stats.DegreeCentrality = network.Network.GetAverageDegree();
                network.Stats.ClusteringCoefficient = network.Network.GetGlobalClusteringCoefficient();
                network.Stats.Edges = network.Network.Edges;
                network.Stats.Nodes = network.Network.Nodes.Count;

                OnNetworkStatsUpdate?.Invoke(network);
            });
        }
    }

    public class NetworkWrapper
    {
        public string Name { get; set; }
        public Network Network { get; set; }
        public NetworkStats Stats { get; set; } = new NetworkStats();

        public NetworkWrapper(string name, Network network)
        {
            Name = name;
            Network = network;
        }
    }

    public class NetworkStats
    {
        public int Nodes { get; set; }
        public int Edges { get; set; }
        public double DegreeCentrality { get; set; }
        public double ClosenessCentrality { get; set; }
        public double MeanDistance { get; set; }
        public double ClusteringCoefficient { get; set; }
    }
}
