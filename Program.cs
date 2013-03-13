using System;
using System.Collections.Generic;
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
        public static void Main()
        {
            M3();
        }

        public static void M4()
        {
            NNetwork n = new NNetwork(new int[] { 2, 4, 4, 1 });
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
            double[] ests = n.Estimation(1);
            for (int i = 0; i < ders.Length; i++)
            {
                Console.Out.WriteLine(ders[i] + " = " + ests[i]);
            }
        }

        public static void M3()
        {
            NNetwork n = new NNetwork(new int[] { 2, 4, 4, 1 });
            n.RandomizeWeights(-1, 10);
            Random random = new Random();
            double x;
            double y;
            double z;
            for (int j = 0; j < 100; j++ )
            {
                for (int i = 0; i < 100; i++)
                {
                    x = random.NextDouble();
                    y = random.NextDouble();
                    z = some_function(x, y);
                    n.SetInput(new double[] { x, y });
                    n.SetAnswers(new double[] { z });
                    n.BackPropagate();
                }
                n.ApplyTraining(0.01, 0.0001);
            }
            for (int i = 0; i < 10; i++)
            {
                x = random.NextDouble();
                y = random.NextDouble();
                n.SetInput(new double[] { x, y });
                z = n.GetOutput()[0];
                int digits = 3;
                Console.Out.WriteLine(Math.Round(z, digits)+" = "+Math.Round(some_function(x, y), digits));
            }
        }

        public static double some_function(double x, double y)
        {
//            return 1/(1+x*x+y*y);
            return x*y;
        }

        public static void M2()
        {
            NNetwork n = new NNetwork(new int[] { 2, 2, 1 });
            double[] from_l1 = new double[] { -30, 20, 20, 10, -20, -20 };
            double[] from_l2 = new double[] { -10, 20, 20 };
            double[][] weights = new double[][]
                {
                    from_l1,
                    from_l2
                };
            n.SetWeightMatrix(weights);
            n.RandomizeWeights(1, 2);

            var inputs_1 = new double[] { 0, 0 };
            var answers_1 = new double[] { 1 };
            var inputs_2 = new double[] { 0, 1 };
            var answers_2 = new double[] { 0 };
            var inputs_3 = new double[] { 1, 0 };
            var answers_3 = new double[] { 0 };
            var inputs_4 = new double[] { 1, 1 };
            var answers_4 = new double[] { 1 };
            for (int j = 0; j < 100; j++)
            {
                for (int i = 0; i < 100; i++)
                {
                    int k2 = 0;
                    n.SetInput(inputs_1);
                    n.SetAnswers(answers_1);
                    n.BackPropagate();

                    n.SetInput(inputs_2);
                    n.SetAnswers(answers_2);
                    n.BackPropagate();

                    n.SetInput(inputs_3);
                    n.SetAnswers(answers_3);
                    n.BackPropagate();

                    n.SetInput(inputs_4);
                    n.SetAnswers(answers_4);
                    n.BackPropagate();
                }
                int k1 = 0;
                n.ApplyTraining(0.001, 0.01);
                n.SetInput(inputs_1);
                Console.Out.WriteLine(inputs_1[0] + "" + inputs_1[1] + " = " + n.GetOutput()[0]);
                n.SetInput(inputs_2);
                Console.Out.WriteLine(inputs_2[0] + "" + inputs_2[1] + " = " + n.GetOutput()[0]);
                n.SetInput(inputs_3);
                Console.Out.WriteLine(inputs_3[0] + "" + inputs_3[1] + " = " + n.GetOutput()[0]);
                n.SetInput(inputs_4);
                Console.Out.WriteLine(inputs_4[0] + "" + inputs_4[1] + " = " + n.GetOutput()[0]);
            }
            n.SetInput(inputs_1);
            Console.Out.WriteLine(inputs_1[0] + "" + inputs_1[1] + " = " + n.GetOutput()[0]);
            n.SetInput(inputs_2);
            Console.Out.WriteLine(inputs_2[0] + "" + inputs_2[1] + " = " + n.GetOutput()[0]);
            n.SetInput(inputs_3);
            Console.Out.WriteLine(inputs_3[0] + "" + inputs_3[1] + " = " + n.GetOutput()[0]);
            n.SetInput(inputs_4);
            Console.Out.WriteLine(inputs_4[0] + "" + inputs_4[1] + " = " + n.GetOutput()[0]);
        }

        public static void M1()
        {
            NNetwork n = new NNetwork(new int[] { 2, 2, 1 });
            n.RandomizeWeights(2, 20);
            var inputs_1 = new double[] { 0, 0 };
            var answers_1 = new double[] { 1 };
            var inputs_2 = new double[] { 0, 1 };
            var answers_2 = new double[] { 0 };
            var inputs_3 = new double[] { 1, 0 };
            var answers_3 = new double[] { 0 };
            var inputs_4 = new double[] { 1, 1 };
            var answers_4 = new double[] { 1 };
            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < 100; i++)
                {
                    int k = 0;
                    n.SetInput(inputs_1);
                    n.SetAnswers(answers_1);
                    n.BackPropagate();
                    //                n.ApplyTraining();
                    //                Console.Out.WriteLine(inputs_1[0] + "" + inputs_1[1] + " = " + n.GetOutput()[0]);               

                    n.SetInput(inputs_2);
                    n.SetAnswers(answers_2);
                    n.BackPropagate();
                    //                n.ApplyTraining();
                    //                Console.Out.WriteLine(inputs_2[0] + "" + inputs_2[1] + " = " + n.GetOutput()[0]);

                    n.SetInput(inputs_3);
                    n.SetAnswers(answers_3);
                    n.BackPropagate();
                    //                n.ApplyTraining();
                    //                Console.Out.WriteLine(inputs_3[0] + "" + inputs_3[1] + " = " + n.GetOutput()[0]);

                    n.SetInput(inputs_4);
                    n.SetAnswers(answers_4);
                    n.BackPropagate();


                    //                Console.Out.WriteLine(inputs_4[0] + "" + inputs_4[1] + " = " + n.GetOutput()[0]);

                    //                n.ApplyTraining(0, 1);
                    //                Console.Out.WriteLine(inputs_1[0] + "" + inputs_1[1] + " = " + n.GetOutput()[0]);
                    //                Console.Out.WriteLine(inputs_2[0] + "" + inputs_2[1] + " = " + n.GetOutput()[0]);
                    //                Console.Out.WriteLine(inputs_3[0] + "" + inputs_3[1] + " = " + n.GetOutput()[0]);
                    //                Console.Out.WriteLine(inputs_4[0] + "" + inputs_4[1] + " = " + n.GetOutput()[0]);
                }
                n.ApplyTraining(0.001, 0.1);
            }
            //            n.ApplyTraining(0, 0.01);
            //            n.ApplyTraining(0, 1);
            n.SetInput(inputs_1);
            Console.Out.WriteLine(inputs_1[0] + "" + inputs_1[1] + " = " + n.GetOutput()[0]);
            n.SetInput(inputs_2);
            Console.Out.WriteLine(inputs_2[0] + "" + inputs_2[1] + " = " + n.GetOutput()[0]);
            n.SetInput(inputs_3);
            Console.Out.WriteLine(inputs_3[0] + "" + inputs_3[1] + " = " + n.GetOutput()[0]);
            n.SetInput(inputs_4);
            Console.Out.WriteLine(inputs_4[0] + "" + inputs_4[1] + " = " + n.GetOutput()[0]);
        }
    }
}
