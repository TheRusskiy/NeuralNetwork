using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphLib;
using NeuralNetwork.src;

namespace NeuralNetwork
{
    public partial class MainForm : Form
    {
        private NNetwork network;
        private GoogleParser parser = new GoogleParser();
        private DataNormalizer normalizer;
        private NetworkTrainer trainer;
        private double[] data;
        private double[] train_data;
        private double[] test_data;
        private bool is_hyperbolic;
        private bool is_sigmoid;
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
            String[] layers_string = textLayers.Text.Split(";".ToCharArray());
            int[] layers = new int[layers_string.Length];
            for (int i = 0; i < layers_string.Length; i++)
            {
                layers[i] = int.Parse(layers_string[i]);
            }
            if (radioHyperbolic.Checked)
            {
                network = NNetwork.HyperbolicNetwork(layers);
                is_hyperbolic = true;
                is_sigmoid = false;
            }
            if (radioSigmoid.Checked)
            {
                network = NNetwork.SigmoidNetwork(layers);
                is_hyperbolic = false;
                is_sigmoid = true;
            }
            groupWeights.Enabled = true;
        }

        private void buttonRandomize_Click(object sender, EventArgs e)
        {
            int seed = int.Parse(textSeed.Text);
            network.RandomizeWeights(seed);
            groupData.Enabled = true;
        }

        private void buttonLoadData_Click(object sender, EventArgs e)
        {
            openDataFile.ShowDialog();
            data = parser.GetDeltas(
                parser.ParseFile(openDataFile.FileName)
                );
            normalizer = is_sigmoid ? DataNormalizer.SigmoidNormalizer(data) : DataNormalizer.HyperbolicNormalizer(data);
            data = normalizer.GetValues();
            groupTraining.Enabled = true;
        }

        private void buttonTrain_Click(object sender, EventArgs e)
        {
            trainer = new NetworkTrainer(network);
            double lambda = double.Parse(textLambda.Text);
            double alpha = double.Parse(textAlpha.Text);
            AssignData();
            double error = 1;
            double delta = 1;
            double required_error = double.Parse(textErrorStop.Text);
            double required_delta = double.Parse(textDeltaStop.Text);
            int j = 0;
            while(error > required_error && !(delta <= required_delta) || j == 1)
            {
                trainer.TrainPrediction(train_data, lambda, alpha);
                double new_cost = trainer.GetError();
                delta = error - new_cost;
                error = new_cost;
                j++;
            }
        }


        private void buttonPlot_Click(object sender, EventArgs e)
        {
            float min_y=0;
            float max_y=0;
            if (is_sigmoid)
            {
                min_y = 0;
                max_y = 1;
            }
            if (is_hyperbolic)
            {
                min_y = -1;
                max_y = 1;
            }
            double[] prediction = trainer.GetPrediction(train_data, 1);
            DataSource prediction_source = new DataSource();
            prediction_source.Length = data.Length;

            for (int i = 0; i < data.Length; i++)
            {
                if (i < network.InputCount())
                {
                    prediction_source.Samples[i].x = i;
                    prediction_source.Samples[i].y = 0.5f;
                }
                else if (i < network.InputCount()+prediction.Length)
                {
                    prediction_source.Samples[i].x = i;
                    prediction_source.Samples[i].y = (float)prediction[i-network.InputCount()];
                }
                else
                {
                    prediction_source.Samples[i].x = i;
                    prediction_source.Samples[i].y = 0.5f;
                }
            }

            DataSource initial_source = new DataSource();
            initial_source.Length = data.Length;
            for (int i = 0; i < data.Length; i++)
            {
                initial_source.Samples[i].x = i;
                initial_source.Samples[i].y = (float)data[i];
            }
            display.DataSources.Clear();
            display.SetDisplayRangeX(0, 400);
            display.PanelLayout = PlotterGraphPaneEx.LayoutMode.NORMAL;

            display.DataSources.Add(initial_source);
            initial_source.AutoScaleY = false;
            initial_source.SetGridDistanceY(100);
            initial_source.OnRenderYAxisLabel = RenderYLabel;
            initial_source.GraphColor = Color.Crimson;
//            initial_source.AutoScaleY = true;
            initial_source.SetDisplayRangeY(min_y, max_y);
            initial_source.XAutoScaleOffset = 0;

            display.DataSources.Add(prediction_source);
            prediction_source.AutoScaleY = false;
            prediction_source.SetGridDistanceY(100);
            prediction_source.OnRenderYAxisLabel = RenderYLabel;
            prediction_source.GraphColor = Color.RoyalBlue;
//            prediction_source.AutoScaleY = true;
            prediction_source.SetDisplayRangeY(min_y, max_y);
            prediction_source.XAutoScaleOffset = 0;


            display.Refresh();
            display.PerformAutoScale();
        }

        private void AssignData()
        {
            int trainset_count = data.Length*(int) numericTrainPercent.Value/100;
            train_data = new double[trainset_count];
            test_data = new double[data.Length - trainset_count];
            for (int i = 0; i < trainset_count; i++)
            {
                train_data[i] = data[i];
            }
            for (int i = trainset_count; i < data.Length; i++)
            {
                test_data[i - trainset_count] = data[i];
            }
        }
    }
}
