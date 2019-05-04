using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class MainForm : Form
    {
        private readonly MainFormController _controller;

        public MainForm()
        {
            InitializeComponent();
            _controller = new MainFormController();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            listNetworks.Items.Add("aa");
            listNetworks.Items.Add("b");


        }
    }
}
