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
            for (int j = 0; j < 100; j++ )
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
                n.ApplyTraining(0, 0.001);
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
