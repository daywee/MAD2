using System;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class MainForm : Form
    {
        private readonly MainFormController _controller;
        private const string DoubleFormat = "0.00";

        public MainForm()
        {
            InitializeComponent();
            _controller = new MainFormController();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _controller.OnNetworkAdd += HandleNetworkAdd;
            _controller.OnNetworkRemove += HandleNetworkRemove;
            _controller.OnNetworkStatsUpdate += HandleNetworkStatsUpdate;
        }

        private void HandleNetworkAdd(NetworkWrapper network)
        {
            listNetworks.Items.Add(network.Name);
            listNetworks.SelectedIndex = listNetworks.Items.Count - 1;
        }

        private void HandleNetworkRemove(NetworkWrapper network)
        {
            listNetworks.Items.Remove(network.Name);
        }

        private void HandleNetworkStatsUpdate(NetworkWrapper network)
        {
            listViewStats.Invoke((MethodInvoker)delegate
            {
                var i1 = new ListViewItem("Vertices");
                i1.SubItems.Add(network.Stats.Nodes.ToString());

                var i2 = new ListViewItem("Edges");
                i2.SubItems.Add(network.Stats.Edges.ToString());

                var i3 = new ListViewItem("Degree centrality");
                i3.SubItems.Add(network.Stats.DegreeCentrality.ToString(DoubleFormat));

                var i4 = new ListViewItem("Closeness centrality");
                i4.SubItems.Add(network.Stats.ClosenessCentrality.ToString(DoubleFormat));

                var i5 = new ListViewItem("Mean distance");
                i5.SubItems.Add(network.Stats.MeanDistance.ToString(DoubleFormat));

                var i6 = new ListViewItem("Clustering coefficient");
                i6.SubItems.Add(network.Stats.ClusteringCoefficient.ToString(DoubleFormat));

                listViewStats.Items.AddRange(new[] {i1, i2, i3, i4, i5, i6 });
            });
        }

        private void buttonLoadNetwork_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _controller.LoadNetwork(dialog.FileName);
            }
        }
    }
}
