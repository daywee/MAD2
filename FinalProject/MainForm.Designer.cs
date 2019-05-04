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
            this.listCommunities = new System.Windows.Forms.ListBox();
            this.listViewStats = new System.Windows.Forms.ListView();
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxNetworks.SuspendLayout();
            this.groupBoxNetworkStats.SuspendLayout();
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
            this.buttonLoadNetwork.Location = new System.Drawing.Point(6, 263);
            this.buttonLoadNetwork.Name = "buttonLoadNetwork";
            this.buttonLoadNetwork.Size = new System.Drawing.Size(101, 23);
            this.buttonLoadNetwork.TabIndex = 1;
            this.buttonLoadNetwork.Text = "Load";
            this.buttonLoadNetwork.UseVisualStyleBackColor = true;
            this.buttonLoadNetwork.Click += new System.EventHandler(this.buttonLoadNetwork_Click);
            // 
            // groupBoxNetworks
            // 
            this.groupBoxNetworks.Controls.Add(this.buttonDeleteNetwork);
            this.groupBoxNetworks.Controls.Add(this.listNetworks);
            this.groupBoxNetworks.Controls.Add(this.buttonLoadNetwork);
            this.groupBoxNetworks.Location = new System.Drawing.Point(12, 12);
            this.groupBoxNetworks.Name = "groupBoxNetworks";
            this.groupBoxNetworks.Size = new System.Drawing.Size(221, 293);
            this.groupBoxNetworks.TabIndex = 2;
            this.groupBoxNetworks.TabStop = false;
            this.groupBoxNetworks.Text = "Networks";
            // 
            // buttonDeleteNetwork
            // 
            this.buttonDeleteNetwork.Location = new System.Drawing.Point(113, 263);
            this.buttonDeleteNetwork.Name = "buttonDeleteNetwork";
            this.buttonDeleteNetwork.Size = new System.Drawing.Size(101, 23);
            this.buttonDeleteNetwork.TabIndex = 2;
            this.buttonDeleteNetwork.Text = "Delete";
            this.buttonDeleteNetwork.UseVisualStyleBackColor = true;
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
            // listCommunities
            // 
            this.listCommunities.FormattingEnabled = true;
            this.listCommunities.Location = new System.Drawing.Point(583, 12);
            this.listCommunities.Name = "listCommunities";
            this.listCommunities.Size = new System.Drawing.Size(208, 238);
            this.listCommunities.TabIndex = 3;
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
            this.value.Width = 123;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1352, 687);
            this.Controls.Add(this.listCommunities);
            this.Controls.Add(this.groupBoxNetworkStats);
            this.Controls.Add(this.groupBoxNetworks);
            this.Name = "MainForm";
            this.Text = "Network Analysis";
            this.groupBoxNetworks.ResumeLayout(false);
            this.groupBoxNetworkStats.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listNetworks;
        private System.Windows.Forms.Button buttonLoadNetwork;
        private System.Windows.Forms.GroupBox groupBoxNetworks;
        private System.Windows.Forms.Button buttonDeleteNetwork;
        private System.Windows.Forms.GroupBox groupBoxNetworkStats;
        private System.Windows.Forms.ListBox listCommunities;
        private System.Windows.Forms.ListView listViewStats;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader value;
    }
}

