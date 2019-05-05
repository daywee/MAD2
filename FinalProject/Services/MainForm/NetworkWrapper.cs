using FinalProject.NetworkAnalysis;

namespace FinalProject.Services.MainForm
{
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
}
