using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.IsMdiContainer =true;
        }

        private void scottPlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Demos.ScottPlotDemoFrm o = new Demos.ScottPlotDemoFrm();
            o.MdiParent = this;
            o.Show();
        }

        private void oxyPlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Demos.OxyPlotDemoFrm o = new Demos.OxyPlotDemoFrm();
            o.MdiParent = this;
            o.Show();
        }
    }
}
