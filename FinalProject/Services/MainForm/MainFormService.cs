using FinalProject.NetworkAnalysis;
using FinalProject.Services.ProgressBar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Services.MainForm
{
    public class MainFormService
    {
        public List<NetworkWrapper> Networks { get; set; } = new List<NetworkWrapper>();
        public ProgressBarService ProgressBarService { get; set; } = new ProgressBarService();

        public event Action<NetworkWrapper> OnNetworkAdd;
        public event Action<NetworkWrapper> OnNetworkRemove;
        public event Action<NetworkWrapper> OnNetworkStatsUpdate;

        private readonly NetworkFileLoader _networkFileLoader = new NetworkFileLoader();

        public MainFormService()
        {
            OnNetworkAdd += LoadStats;
        }

        public NetworkWrapper GetNetwork(string name)
        {
            return Networks.Single(e => e.Name == name);
        }

        public void LoadNetwork(string path, int rowsToSkip)
        {
            RunWithProgressBar(() =>
            {
                var network = _networkFileLoader.LoadHeroes(path, rowsToSkip);
                //var network = _networkFileLoader.LoadFromCsvFile(path, firstId: 0, skip: 0);
                string fileName = Path.GetFileName(path);

                var wrapper = new NetworkWrapper(fileName, network);
                Networks.Add(wrapper);
                OnNetworkAdd?.Invoke(wrapper);
            });
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
            RunWithProgressBar(() =>
            {
                network.Network.ComputeClusteringCoefficient();
                network.Stats.ClusteringCoefficient = network.Network.GetGlobalClusteringCoefficient();

                OnNetworkStatsUpdate?.Invoke(network);
            });

            RunWithProgressBar(() =>
            {
                network.Stats.DegreeCentrality = network.Network.GetAverageDegree();
                network.Stats.Edges = network.Network.Edges;
                network.Stats.Nodes = network.Network.Nodes.Count;

                OnNetworkStatsUpdate?.Invoke(network);
            });
        }

        private void RunWithProgressBar(Action action)
        {
            Task.Run(() =>
            {
                var id = Guid.NewGuid();
                ProgressBarService.Start(id);
                action?.Invoke();
                ProgressBarService.Stop(id);
            });
        }
    }
}
