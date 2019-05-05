namespace FinalProject
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listNetworks = new System.Windows.Forms.ListBox();
            this.buttonLoadNetwork = new System.Windows.Forms.Button();
            this.groupBoxNetworks = new System.Windows.Forms.GroupBox();
            this.buttonDeleteNetwork = new System.Windows.Forms.Button();
            this.groupBoxNetworkStats = new System.Windows.Forms.GroupBox();
            this.listViewStats = new System.Windows.Forms.ListView();
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxCommunities = new System.Windows.Forms.GroupBox();
            this.listViewCommunities = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.vertices = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.checkBoxSkipFirstLine = new System.Windows.Forms.CheckBox();
            this.groupBoxNetworks.SuspendLayout();
            this.groupBoxNetworkStats.SuspendLayout();
            this.groupBoxCommunities.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listNetworks
            // 
            this.listNetworks.FormattingEnabled = true;
            this.listNetworks.Location = new System.Drawing.Point(6, 19);
            this.listNetworks.Name = "listNetworks";
            this.listNetworks.Size = new System.Drawing.Size(208, 238);
            this.listNetworks.TabIndex = 0;
            // 
            // buttonLoadNetwork
            // 
            this.buttonLoadNetwork.Location = new System.Drawing.Point(0, 293);
            this.buttonLoadNetwork.Name = "buttonLoadNetwork";
            this.buttonLoadNetwork.Size = new System.Drawing.Size(101, 23);
            this.buttonLoadNetwork.TabIndex = 1;
            this.buttonLoadNetwork.Text = "Load";
            this.buttonLoadNetwork.UseVisualStyleBackColor = true;
            this.buttonLoadNetwork.Click += new System.EventHandler(this.buttonLoadNetwork_Click);
            // 
            // groupBoxNetworks
            // 
            this.groupBoxNetworks.Controls.Add(this.checkBoxSkipFirstLine);
            this.groupBoxNetworks.Controls.Add(this.buttonDeleteNetwork);
            this.groupBoxNetworks.Controls.Add(this.listNetworks);
            this.groupBoxNetworks.Controls.Add(this.buttonLoadNetwork);
            this.groupBoxNetworks.Location = new System.Drawing.Point(12, 12);
            this.groupBoxNetworks.Name = "groupBoxNetworks";
            this.groupBoxNetworks.Size = new System.Drawing.Size(221, 322);
            this.groupBoxNetworks.TabIndex = 2;
            this.groupBoxNetworks.TabStop = false;
            this.groupBoxNetworks.Text = "Networks";
            // 
            // buttonDeleteNetwork
            // 
            this.buttonDeleteNetwork.Location = new System.Drawing.Point(113, 293);
            this.buttonDeleteNetwork.Name = "buttonDeleteNetwork";
            this.buttonDeleteNetwork.Size = new System.Drawing.Size(101, 23);
            this.buttonDeleteNetwork.TabIndex = 2;
            this.buttonDeleteNetwork.Text = "Delete";
            this.buttonDeleteNetwork.UseVisualStyleBackColor = true;
            this.buttonDeleteNetwork.Click += new System.EventHandler(this.buttonDeleteNetwork_Click);
            // 
            // groupBoxNetworkStats
            // 
            this.groupBoxNetworkStats.Controls.Add(this.listViewStats);
            this.groupBoxNetworkStats.Location = new System.Drawing.Point(239, 12);
            this.groupBoxNetworkStats.Name = "groupBoxNetworkStats";
            this.groupBoxNetworkStats.Size = new System.Drawing.Size(338, 293);
            this.groupBoxNetworkStats.TabIndex = 3;
            this.groupBoxNetworkStats.TabStop = false;
            this.groupBoxNetworkStats.Text = "Network statistics";
            // 
            // listViewStats
            // 
            this.listViewStats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.type,
            this.value});
            this.listViewStats.GridLines = true;
            this.listViewStats.Location = new System.Drawing.Point(6, 19);
            this.listViewStats.Name = "listViewStats";
            this.listViewStats.Size = new System.Drawing.Size(326, 267);
            this.listViewStats.TabIndex = 4;
            this.listViewStats.UseCompatibleStateImageBehavior = false;
            this.listViewStats.View = System.Windows.Forms.View.Details;
            // 
            // type
            // 
            this.type.Text = "Type";
            this.type.Width = 150;
            // 
            // value
            // 
            this.value.Text = "Value";
            this.value.Width = 155;
            // 
            // groupBoxCommunities
            // 
            this.groupBoxCommunities.Controls.Add(this.button1);
            this.groupBoxCommunities.Controls.Add(this.listViewCommunities);
            this.groupBoxCommunities.Location = new System.Drawing.Point(583, 12);
            this.groupBoxCommunities.Name = "groupBoxCommunities";
            this.groupBoxCommunities.Size = new System.Drawing.Size(298, 293);
            this.groupBoxCommunities.TabIndex = 4;
            this.groupBoxCommunities.TabStop = false;
            this.groupBoxCommunities.Text = "Communities";
            // 
            // listViewCommunities
            // 
            this.listViewCommunities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.vertices});
            this.listViewCommunities.Location = new System.Drawing.Point(6, 60);
            this.listViewCommunities.Name = "listViewCommunities";
            this.listViewCommunities.Size = new System.Drawing.Size(286, 226);
            this.listViewCommunities.TabIndex = 5;
            this.listViewCommunities.UseCompatibleStateImageBehavior = false;
            this.listViewCommunities.View = System.Windows.Forms.View.Details;
            // 
            // id
            // 
            this.id.Text = "Id";
            // 
            // vertices
            // 
            this.vertices.Text = "Vertices";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 665);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1352, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // checkBoxSkipFirstLine
            // 
            this.checkBoxSkipFirstLine.AutoSize = true;
            this.checkBoxSkipFirstLine.Location = new System.Drawing.Point(6, 270);
            this.checkBoxSkipFirstLine.Name = "checkBoxSkipFirstLine";
            this.checkBoxSkipFirstLine.Size = new System.Drawing.Size(85, 17);
            this.checkBoxSkipFirstLine.TabIndex = 6;
            this.checkBoxSkipFirstLine.Text = "Skip first line";
            this.checkBoxSkipFirstLine.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1352, 687);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxCommunities);
            this.Controls.Add(this.groupBoxNetworkStats);
            this.Controls.Add(this.groupBoxNetworks);
            this.Name = "MainForm";
            this.Text = "Network Analysis";
            this.groupBoxNetworks.ResumeLayout(false);
            this.groupBoxNetworks.PerformLayout();
            this.groupBoxNetworkStats.ResumeLayout(false);
            this.groupBoxCommunities.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listNetworks;
        private System.Windows.Forms.Button buttonLoadNetwork;
        private System.Windows.Forms.GroupBox groupBoxNetworks;
        private System.Windows.Forms.Button buttonDeleteNetwork;
        private System.Windows.Forms.GroupBox groupBoxNetworkStats;
        private System.Windows.Forms.ListView listViewStats;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader value;
        private System.Windows.Forms.GroupBox groupBoxCommunities;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listViewCommunities;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader vertices;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.CheckBox checkBoxSkipFirstLine;
    }
}

