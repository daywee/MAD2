using FinalProject.Services.MainForm;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class MainForm : Form
    {
        private readonly MainFormService _service;
        private const string DoubleFormat = "0.000";

        public MainForm()
        {
            InitializeComponent();
            _service = new MainFormService();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            toolStripProgressBar.ProgressBar.Style = ProgressBarStyle.Marquee;
            toolStripProgressBar.ProgressBar.MarqueeAnimationSpeed = 20;
            toolStripProgressBar.ProgressBar.Visible = false;

            _service.OnNetworkAdd += HandleNetworkAdd;
            _service.OnNetworkRemove += HandleNetworkRemove;
            _service.OnNetworkStatsUpdate += HandleNetworkStatsUpdate;
            _service.ProgressBarService.OnProgressBarStart += HandleProgressBarStart;
            _service.ProgressBarService.OnProgressBarStop += HandleProgressBarStop;
            _service.OnNetworkCommunitiesUpdate += HandleNetworkCommunitiesUpdate;
            _service.OnNetworkPlotUpdate += HandleNetworkPlotUpdate;
        }

        #region Controller event handlers

        private void HandleProgressBarStart()
        {
            toolStripProgressBar.ProgressBar.Invoke((MethodInvoker)delegate
            {
                toolStripProgressBar.ProgressBar.Visible = true;
            });
        }

        private void HandleProgressBarStop()
        {
            toolStripProgressBar.ProgressBar.Invoke((MethodInvoker)delegate
            {
                toolStripProgressBar.ProgressBar.Visible = false;
            });
        }

        private void HandleNetworkAdd(NetworkWrapper network)
        {
            listNetworks.Invoke((MethodInvoker)delegate
            {
                listNetworks.Items.Add(network.Name);
                listNetworks.SelectedIndex = listNetworks.Items.Count - 1;
            });
        }

        private void HandleNetworkRemove(NetworkWrapper network)
        {
            listNetworks.Items.Remove(network.Name);
        }

        private void HandleNetworkStatsUpdate(NetworkWrapper network)
        {
            void Update()
            {
                listViewStats.Items.Clear();

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

                var i7 = new ListViewItem("Diameter");
                i7.SubItems.Add(network.Stats.Diameter.ToString());

                listViewStats.Items.AddRange(new[] { i1, i2, i3, i4, i5, i6, i7 });
            }

            if (listViewStats.InvokeRequired)
                listViewStats.Invoke((MethodInvoker)Update);
            else
                Update();
        }

        private void HandleNetworkCommunitiesUpdate(NetworkWrapper network)
        {
            void Update()
            {
                listViewCommunities.Items.Clear();

                var items = network.Communities
                    .OrderByDescending(e => e.Nodes.Count)
                    .Select(e => new ListViewItem(e.Id.ToString()) { SubItems = { e.Nodes.Count.ToString() } })
                    .ToArray();

                listViewCommunities.Items.AddRange(items);
            }

            if (listViewCommunities.InvokeRequired)
                listViewCommunities.Invoke((MethodInvoker)Update);
            else
                Update();
        }

        private void HandleNetworkPlotUpdate(NetworkWrapper network)
        {
            if (network.Plot == null)
                return;

            if (pictureBoxNetworkPlot.InvokeRequired)
            {
                pictureBoxNetworkPlot.Invoke((MethodInvoker)delegate
                {
                    pictureBoxNetworkPlot.Image = network.Plot;
                });
            }
            else
            {
                pictureBoxNetworkPlot.Image = network.Plot;
            }
        }

        #endregion

        #region Form event handlers

        private void buttonLoadNetwork_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                bool skip = checkBoxSkipFirstLine.Checked;
                _service.LoadNetwork(dialog.FileName, skip ? 1 : 0);
            }
        }

        private void buttonDeleteNetwork_Click(object sender, EventArgs e)
        {
            _service.RemoveNetwork((string)listNetworks.SelectedItem);
        }

        private void listNetworks_SelectedIndexChanged(object sender, EventArgs e)
        {
            var network = _service.GetNetwork((string)listNetworks.SelectedItem);
            HandleNetworkStatsUpdate(network);
            HandleNetworkCommunitiesUpdate(network);
            HandleNetworkPlotUpdate(network);
        }

        #endregion

        private void buttonRunIterativeSearch_Click(object sender, EventArgs e)
        {
            _service.FindCommunities((string)listNetworks.SelectedItem);
        }

        private void buttonCreateNetworkFromCommunity_Click(object sender, EventArgs e)
        {
            string network = (string)listNetworks.SelectedItem;
            if (listViewCommunities.SelectedItems.Count > 0)
            {
                string community = listViewCommunities.SelectedItems[0].Text;
                _service.CreateNetworkFromCommunity(network, community);
            }
        }

        private void buttonExportNetworkToR_Click(object sender, EventArgs e)
        {
            if (listNetworks.SelectedItem != null)
            {
                var dialog = new SaveFileDialog { Filter = "R Script|*.R", DefaultExt = "R", AddExtension = true };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _service.ExportNetworkToR(dialog.FileName, (string)listNetworks.SelectedItem);
                }
            }
        }

        private void buttonExportToCsv_Click(object sender, EventArgs e)
        {
            if (listNetworks.SelectedItem != null)
            {
                var dialog = new SaveFileDialog { Filter = "CSV|*.csv", DefaultExt = "csv", AddExtension = true };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _service.ExportNetworkToCsv(dialog.FileName, (string)listNetworks.SelectedItem);
                }
            }
        }

        private void buttonGenerateNetwork_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(numericNoNodes.Value);
            int m0 = Convert.ToInt32(numericNoFundamentalNodes.Value);
            int m = Convert.ToInt32(numericNoCreatedEdges.Value);
            double v = Convert.ToDouble(numericV.Value);
            _service.GenerateBAModelWithAging(n, m0, m, v);
        }
    }
}
