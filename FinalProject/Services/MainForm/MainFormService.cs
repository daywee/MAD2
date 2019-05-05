using FinalProject.NetworkAnalysis;
using FinalProject.NetworkAnalysis.CommunityDetection;
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
        public event Action<NetworkWrapper> OnNetworkCommunitiesUpdate; 

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
                var (network, incidenceMatrix) = _networkFileLoader.LoadHeroes(path, rowsToSkip);
                //var network = _networkFileLoader.LoadFromCsvFile(path, firstId: 0, skip: 0);
                string fileName = Path.GetFileNameWithoutExtension(path);

                var wrapper = new NetworkWrapper(fileName, network, incidenceMatrix);
                Networks.Add(wrapper);
                OnNetworkAdd?.Invoke(wrapper);
            });
        }

        public void ExportNetworkToR(string path, string networkName)
        {
            var network = GetNetwork(networkName);
            NetworkFileExporter.ExportToRScript(path, network.Network, network.Communities);
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

        public void FindCommunities(string name)
        {
            var network = Networks.SingleOrDefault(e => e.Name == name);
            if (network != null)
            {
                RunWithProgressBar(() =>
                {
                    var communities = IterativeLocalExpansion.FindAllCommunities(network.Network, network.Network.Nodes.First());
                    network.Communities = communities;
                    OnNetworkCommunitiesUpdate?.Invoke(network);
                });
            }
        }

        public void CreateNetworkFromCommunity(string networkName, string communityName)
        {
            var network = GetNetwork(networkName);
            var community = network.Communities.Single(e => e.Id.ToString() == communityName);

            // cloning because new nodes must be used
            var newNetwork = new Network(community.Nodes).Clone();

            var newNetworkWrapper = new NetworkWrapper($"{networkName} - {communityName}", newNetwork, newNetwork.GetIncidenceMatrix());
            Networks.Add(newNetworkWrapper);
            OnNetworkAdd?.Invoke(newNetworkWrapper);
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
                network.DistanceMatrix = network.IncidenceMatrix.GetDistanceMatrix();
                network.Stats.MeanDistance = network.DistanceMatrix.GetMeanDistance();
                network.Stats.ClosenessCentrality = network.DistanceMatrix.GetClosenessCentralityVector().Average;

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
