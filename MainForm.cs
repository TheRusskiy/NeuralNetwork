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
using System.Diagnostics;

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
            bool two_steps = network.OutputCount() >= 2;
            bool three_steps = network.OutputCount() >= 3;
            checkTrain2.Enabled = two_steps;
            checkTest2.Enabled = two_steps;
            checkTrain3.Enabled = three_steps;
            checkTest3.Enabled = three_steps;
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
            groupPlotting.Enabled = false;
            trainer = new NetworkTrainer(network);
            double lambda = double.Parse(textLambda.Text);
            double alpha = double.Parse(textAlpha.Text);
            AssignData();
            double error = 1;
            double delta = 1;
            double required_error = double.Parse(textErrorStop.Text);
            double required_delta = double.Parse(textDeltaStop.Text);
            int time_limit = int.Parse(textStopTime.Text)*1000;
            int j = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
//            while(error > required_error && (delta >= required_delta) || j == 1)
            while (error > required_error && (delta >= 0) || j == 1)
            {
                trainer.TrainPrediction(train_data, lambda, alpha);
                double new_error = Math.Abs(trainer.GetError());
                delta = error - new_error;
                error = new_error;
                j++;
                if (stopwatch.ElapsedMilliseconds > time_limit) break;
            }
            textError.Text = Math.Round(error, 5).ToString();
            textTimes.Text = j.ToString();
            groupPlotting.Enabled = true;
        }


        private void buttonPlot_Click(object sender, EventArgs e)
        {
            DataSource prediction_source = PredictionSource(1);
            DataSource initial_source = InitialSource();
            DataSource test_source = TestSource(1);
            display.DataSources.Clear();
            int y_resolution = int.Parse(textYResolution.Text);
            display.SetDisplayRangeX(0, y_resolution);
            display.PanelLayout = PlotterGraphPaneEx.LayoutMode.NORMAL;

            if (checkNetwork.Checked) AddDataSource(initial_source, Color.Crimson);
            if (checkTrain.Checked) AddDataSource(prediction_source, Color.RoyalBlue);
            if (checkTest.Checked) AddDataSource(test_source, Color.SeaGreen);
            if (network.OutputCount() >= 2)
            {
                if (checkTrain2.Checked) AddDataSource(PredictionSource(2), Color.Blue);
                if (checkTest2.Checked) AddDataSource(TestSource(2), Color.SeaGreen);
            }
            if (network.OutputCount() >= 3)
            {
                if (checkTrain3.Checked) AddDataSource(PredictionSource(3), Color.Cyan);
                if (checkTest3.Checked) AddDataSource(TestSource(3), Color.SeaGreen);
            }

            display.Refresh();
            display.PerformAutoScale();
        }

        private void AddDataSource(DataSource initial_source, Color color)
        {
            float min_y = 0;
            float max_y = 0;
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
            display.DataSources.Add(initial_source);
            initial_source.AutoScaleY = false;
            initial_source.SetGridDistanceY(100);
            initial_source.OnRenderYAxisLabel = RenderYLabel;
            initial_source.GraphColor = color;
//            initial_source.AutoScaleY = true;
            initial_source.SetDisplayRangeY(min_y, max_y);
            initial_source.XAutoScaleOffset = 0;
        }

        private DataSource PredictionSource(int step)
        {
            double[] prediction = trainer.GetPrediction(train_data, step);
            DataSource prediction_source = new DataSource();
            prediction_source.Length = data.Length;

            float acc = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (i < network.InputCount() + step - 1)
                {
//                    prediction_source.Samples[i].x = i;
//                    prediction_source.Samples[i].y = 0.5f;
                    prediction_source.Samples[i].x = 0;
                    prediction_source.Samples[i].y = 0;
                }
                else if (i < network.InputCount() + prediction.Length + step - 1)
                {
                    prediction_source.Samples[i].x = i;
                    prediction_source.Samples[i].y = (float) prediction[i - network.InputCount() - step + 1];
//                    acc += (float) normalizer.Denormalize(prediction[i - network.InputCount()]);
//                    prediction_source.Samples[i].y = acc;
                }
                else
                {
                    prediction_source.Samples[i].x = 0;
                    prediction_source.Samples[i].y = 0;
                }
            }
            return prediction_source;
        }

        private DataSource TestSource(int step)
        {
            double[] prediction_on_test = trainer.GetPrediction(test_data, step);
            DataSource prediction_source = new DataSource();
            prediction_source.Length = data.Length;

            float acc = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (i < network.InputCount() + train_data.Length + step - 1)
                {
                    prediction_source.Samples[i].x = 0;
                    prediction_source.Samples[i].y = 0;
                }
                else
                {
                    prediction_source.Samples[i].x = i;
                    prediction_source.Samples[i].y = (float)prediction_on_test[i - network.InputCount() - train_data.Length - step + 1];
                }
            }
            return prediction_source;
        }

        private DataSource InitialSource()
        {
            DataSource initial_source;
            initial_source = new DataSource();
            initial_source.Length = data.Length;
            for (int i = 0; i < data.Length; i++)
            {
                initial_source.Samples[i].x = i;
                initial_source.Samples[i].y = (float) data[i];
            }
            return initial_source;
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
