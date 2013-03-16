using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeuralNetwork.src;

namespace NeuralNetwork
{
    static class Program
    {
//        /// <summary>
//        /// The main entry point for the application.
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);
//            Application.Run(new Form1());
//        }

        private static String path_cola = Path.GetFullPath(@"./../../data/ko.csv");

        public static void Main()
        {
            Sinus();
        }

        public static void M1()
        {
            GoogleParser parser = new GoogleParser();
            double[] initial_values = parser.GetDeltas(parser.ParseFile(path_cola));
            DataNormalizer normalizer = new DataNormalizer(initial_values);
            double[] normalized_values = normalizer.GetValues();
            NNetwork network = new NNetwork(new int[]{8, 8, 8, 3});
//            for(int i=0; )
        }

        public static void Cosinus()
        {
            NNetwork network = new NNetwork(new int[] { 1, 2, 1 });
            network.RandomizeWeights(0, 10);
            double[] inputs = SinusTrainSet()[0];
            double[] outputs = SinusTrainSet()[1];
            DataNormalizer input_normalizer = new DataNormalizer(inputs);
            DataNormalizer output_normalizer = new DataNormalizer(outputs);
            double[] n_inputs = input_normalizer.GetValues();
            double[] n_outputs = output_normalizer.GetValues();
            int max_j = 10000;
            for (int j = 0; j < max_j; j++)
            {
                for (int i = 0; i < n_inputs.Length; i++)
                {
                    int k = i;
                    network.SetInput(new double[] { n_inputs[k] });
                    network.SetAnswers(new double[] { n_outputs[k] });
                    network.BackPropagate();
                    if (j == 0)
                    {
                        Show(n_inputs[i], n_outputs[i]);
                    }
                }
                network.ApplyTraining(0.0000, 0.01);
                if (j < 10 || j%(max_j/10) == 0)
                {
                    Console.Out.WriteLine(network.AccumulatedCost());
                }
                network.ResetCost();
                
            }
            Console.Out.WriteLine("AFTER");
            for (int i = 0; i < n_inputs.Length; i++)
            {
                network.SetInput(new double[] { n_inputs[i] });
                Show(n_inputs[i], network.GetOutput()[0]);
            }
        }

        public static void Sinus()
        {
            NNetwork network = new NNetwork(new int[] { 1, 4, 1 });
            network.RandomizeWeights(-1, 2);
            double[] inputs = SinusTrainSet()[0];
            double[] outputs = SinusTrainSet()[1];
            DataNormalizer input_normalizer = new DataNormalizer(inputs);
            DataNormalizer output_normalizer = new DataNormalizer(outputs);
            double[] n_inputs = inputs;// input_normalizer.GetValues();
            double[] n_outputs = outputs;// output_normalizer.GetValues();
            int max_j = 10000;
            double error = 1;
            double delta = 1;
            int j = 0;
            for (; error > 0.01 && !(delta <= 0.0000001)||j==1; j++)
//            for (int j = 0; j<max_j; j++)
            {
                for (int i = 0; i < n_inputs.Length; i++)
                {
                    int k = i;
                    network.SetInput(new double[] {n_inputs[k]});
                    network.SetAnswers(new double[] {n_outputs[k]});
                    network.BackPropagate();
                    if (j == 0)
                    {
                        Show(n_inputs[i], n_outputs[i]);
                    }
                }
                if (j%(max_j/10) == 0)
                {
//                    Console.Out.WriteLine(network.AccumulatedCost());
                }
                double new_cost = network.AccumulatedCost();
                delta = error - new_cost;
                error = new_cost;
                network.ResetCost();
                network.ApplyTraining(0.0000, 1);
            }
            Console.Out.WriteLine(j+", Error: "+error+", delta: "+delta);
            Console.Out.WriteLine("AFTER");
            for (int i = 0; i < n_inputs.Length; i++)
            {
                network.SetInput(new double[] { n_inputs[i] });
                Show(n_inputs[i], network.GetOutput()[0]);
            }
        }

        private static void Show(double x1, double x2)
        {
            Console.Out.WriteLine(
            Math.Round(x1, 3) +
            ": " +
            Math.Round(x2, 3)
            );
        }

        private static double[][] SinusTrainSet()
        {
            int n = 9;
            double[] inputs = new double[n];
            double[] outputs = new double[n];
            double pi = Math.PI;
            for (int i = 0; i < n; i++)
            {
                var value = -pi / 2 + i * pi / 8;
                inputs[i] = value;
                outputs[i] = Math.Sin(value);
            }
            return new double[][] { inputs, outputs };
        }

        private static double[][] CosinusTrainSet()
        {
            int n = 9;
            double[] inputs = new double[n];
            double[] outputs = new double[n];
            double pi = Math.PI;
            for (int i = 0; i < n; i++)
            {
                var value = -pi / 2 + i * pi / 8;
                inputs[i] = value;
                outputs[i] = Math.Cos(value);
            }
            return new double[][] { inputs, outputs };
        }
    }
}
