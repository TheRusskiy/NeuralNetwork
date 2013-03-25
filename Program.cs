using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeuralNetwork.src;
using NeuralNetwork.test;

namespace NeuralNetwork
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static String path_cola = Path.GetFullPath(@"./../../data/ko.csv");

//        public static void Main()
//        {
//            TrainPrediction();
////            Sinus();
////            TestTanhLearningOnSinus();
////            TestTanhDerivative();
//            
//        }

        public static void TrainPrediction()
        {
            NNetwork network = NNetwork.SigmoidNetwork(new int[] { 5, 1 });
            network.RandomizeWeights(-1, 20);
            NetworkTrainer trainer = new NetworkTrainer(network);
            List<double> tr = new List<double>();
            for (double i = 0; i <= 1; i=i+0.05)
            {
                tr.Add(i);
            }
            double[] train_set = tr.ToArray();//new double[] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
            double error = 1;
            double delta = 1;
            int j = 0;
            for (; error > 0.01 && !(delta <= 0.00001) || j == 1; j++)
            {
                trainer.TrainPrediction(train_set, 0.0001, 0.2);
                double new_cost = trainer.GetError();
                delta = error - new_cost;
                error = new_cost;
            }
            Console.Out.WriteLine(j+": "+error);
            for (double i = 0; i <= 0.5; i=i+0.05)
            {
                network.SetInput(new double[] { i + 0.0, i + 0.1, i + 0.2, i + 0.3, i + 0.4 });
                Show(new double[]
                    {
                        i+0.5,
                        network.GetOutput()[0],
//                        network.GetOutput()[1]
                    });
            }
        }

        public static void TestTanhDerivative()
        {
            NNetwork n = NNetwork.HyperbolicNetwork(new int[] { 2, 2, 1 });
            n.RandomizeWeights(-1, 10);
            Random random = new Random();
            double x;
            double y;
            double z;
            x = random.NextDouble();
            y = random.NextDouble();
            z = some_function(x, y);
            n.SetInput(new double[] { x, y });
            n.SetAnswers(new double[] { z });
            n.BackPropagate();
            double[] ders = n.Derivatives();
            double[] ests = n.Estimation(0.0001);
            for (int i = 0; i < ders.Length; i++)
            {
                Show(new[]{ders[i], ests[i], ests[i]/ders[i]});
            }
        }

        public static void TestTanhLearningOnSinus()
        {
            NNetwork network = NNetwork.HyperbolicNetwork(new int[] { 1, 2, 1 });
            network.RandomizeWeights(1, 2);
            NetworkTrainer trainer = new NetworkTrainer(network);
            double[][] inputs = SinusTrainSet()[0];
            double[][] outputs = SinusTrainSet()[1];
            double error = 1;
            double delta = 1;
            int j = 0;
            for (; error > 0.01 && !(delta <= 0.000001) || j == 1; j++)
            {
                trainer.TrainClassification(inputs, outputs);
                double new_cost = trainer.GetError();
                delta = error - new_cost;
                error = new_cost;
            }
            double[][] input_test = SinusTrainSet(20)[0];
            double[][] output_test = SinusTrainSet(20)[1];
            trainer.IsLearning = false;
            trainer.TrainClassification(input_test, output_test);
            error = trainer.GetError();
            Console.Out.WriteLine(error);
            for (int i = 0; i < input_test.Length; i++ )
            {
                network.SetInput(input_test[i]);
                Show(new []{input_test[i][0], network.GetOutput()[0], Math.Sin(input_test[i][0])});
            }
        }

//        public static void M1()
//        {
//            GoogleParser parser = new GoogleParser();
//            double[] initial_values = parser.GetDeltas(parser.ParseFile(path_cola));
//            DataNormalizer normalizer = new DataNormalizer(initial_values);
//            double[] normalized_values = normalizer.GetValues();
//            NNetwork network = new NNetwork(new int[]{8, 8, 8, 3});
////            for(int i=0; )
//        }
//
//        public static void Cosinus()
//        {
//            NNetwork network = new NNetwork(new int[] { 1, 2, 1 });
//            network.RandomizeWeights(0, 10);
//            double[] inputs = SinusTrainSet()[0];
//            double[] outputs = SinusTrainSet()[1];
//            DataNormalizer input_normalizer = new DataNormalizer(inputs);
//            DataNormalizer output_normalizer = new DataNormalizer(outputs);
//            double[] n_inputs = input_normalizer.GetValues();
//            double[] n_outputs = output_normalizer.GetValues();
//            int max_j = 10000;
//            for (int j = 0; j < max_j; j++)
//            {
//                for (int i = 0; i < n_inputs.Length; i++)
//                {
//                    int k = i;
//                    network.SetInput(new double[] { n_inputs[k] });
//                    network.SetAnswers(new double[] { n_outputs[k] });
//                    network.BackPropagate();
//                    if (j == 0)
//                    {
//                        Show(n_inputs[i], n_outputs[i]);
//                    }
//                }
//                network.ApplyTraining(0.0000, 0.01);
//                if (j < 10 || j%(max_j/10) == 0)
//                {
//                    Console.Out.WriteLine(network.AccumulatedCost());
//                }
//                network.ResetCost();
//                
//            }
//            Console.Out.WriteLine("AFTER");
//            for (int i = 0; i < n_inputs.Length; i++)
//            {
//                network.SetInput(new double[] { n_inputs[i] });
//                Show(n_inputs[i], network.GetOutput()[0]);
//            }
//        }
//
//        public static void Sinus()
//        {
//            NNetwork network = new NNetwork(new int[] { 1, 2, 1 });
//            network.RandomizeWeights(3, 2);
//            double[] inputs = SinusTrainSet()[0];
//            double[] outputs = SinusTrainSet()[1];
//            DataNormalizer input_normalizer = new DataNormalizer(inputs);
//            DataNormalizer output_normalizer = new DataNormalizer(outputs);
//            double[] n_inputs = inputs;// input_normalizer.GetValues();
//            double[] n_outputs = outputs;// output_normalizer.GetValues();
//            int max_j = 10000;
//            double error = 1;
//            double delta = 1;
//            int j = 0;
//            for (; error > 0.01 && !(delta <= 0.0000001)||j==1; j++)
////            for (int j = 0; j<max_j; j++)
//            {
//                for (int i = 0; i < n_inputs.Length; i++)
//                {
//                    int k = i;
//                    network.SetInput(new double[] {n_inputs[k]});
//                    network.SetAnswers(new double[] {n_outputs[k]});
//                    network.BackPropagate();
//                    if (j == 0)
//                    {
//                        Show(n_inputs[i], n_outputs[i]);
//                    }
//                }
//                if (j%(max_j/10) == 0)
//                {
////                    Console.Out.WriteLine(network.AccumulatedCost());
//                }
//                double new_cost = network.AccumulatedCost();
//                delta = error - new_cost;
//                error = new_cost;
//                network.ResetCost();
//                network.ApplyTraining(0.001, 2);
//            }
//            Console.Out.WriteLine(j+", Error: "+error+", delta: "+delta);
//            Console.Out.WriteLine("AFTER");
////            for (int i = 0; i < n_inputs.Length; i++)
//            for (double i = -Math.PI/2.0; i < Math.PI/2.0;i=i+0.2)
//            {
////                network.SetInput(new double[] { n_inputs[i] });
//                network.SetInput(new double[] { i });
//                Show(i, network.GetOutput()[0]);
//            }
//        }

        private static void Show(double[] values, int digits = 4)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] >= 0)
                {
                    Console.Out.Write(" {0,-" + (digits + 2) + "} : ", Math.Round(values[i], digits));
                }
                else
                {
                    Console.Out.Write("{0,-" + (digits + 3) + "} : ", Math.Round(values[i], digits));
                }
                
            }
            Console.Out.WriteLine("");
        }

        private static double[][][] SinusTrainSet(int size=9)
        {
            double[][] inputs = new double[size][];
            double[][] outputs = new double[size][];
            double pi = Math.PI;
            double step = pi/(double)size;
            for (int i = 0; i < size; i++)
            {
                var value = -pi / 2 + i * step;
                inputs[i] = new double[]{value};
                outputs[i] = new double[]{Math.Sin(value)};
            }
            return new double[][][] { inputs, outputs };
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

        public static double some_function(double x, double y)
        {
            return x * x * y + y * y;
        }
    }
}
