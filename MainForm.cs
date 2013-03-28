using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphLib;
using NeuralNetwork.src;

namespace NeuralNetwork
{
    public partial class MainForm : Form
    {
        private NNetwork network;
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display.DataSources.Clear();
            display.SetDisplayRangeX(0, 400);
            display.DataSources.Add(new DataSource());
            int j = 0;
            display.DataSources[j].Name = "Graph " + (j + 1);
            display.DataSources[j].OnRenderXAxisLabel += RenderXLabel;
            this.Text = "Normal Graph";
            display.DataSources[j].Length = 5800;
            display.PanelLayout = PlotterGraphPaneEx.LayoutMode.NORMAL;
            display.DataSources[j].AutoScaleY = false;
            display.DataSources[j].SetDisplayRangeY(-300, 300);
            display.DataSources[j].SetGridDistanceY(100);
            display.DataSources[j].OnRenderYAxisLabel = RenderYLabel;

            display.DataSources[j].Samples[0].x = 1;
            display.DataSources[j].Samples[0].y = 2;
            display.DataSources[j].Samples[1].x = 2;
            display.DataSources[j].Samples[1].y = 200;
            display.DataSources[j].Samples[1].x = 100;
            display.DataSources[j].Samples[1].y = 200;
            display.DataSources[j].GraphColor = Color.Crimson;
            display.Refresh();
        }

        private String RenderXLabel(DataSource s, int idx)
        {
            if (s.AutoScaleX)
            {
                int Value = (int)(s.Samples[idx].x);
                return "" + Value;
            }
            else
            {
                int Value = (int)(s.Samples[idx].x / 200);
                String Label = "" + Value + "\"";
                return Label;
            }
        }

        private String RenderYLabel(DataSource s, float value)
        {
            return String.Format("{0:0.0}", value);
        }

        private void buttonCreateNetwork_Click(object sender, EventArgs e)
        {

        }
    }
}
