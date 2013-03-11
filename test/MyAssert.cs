using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NeuralNetwork.test
{
    class MyAssert
    {
        public delegate void TestDelegate(); //duplicates NUnit declaration
        public static long MeasureMethod(TestDelegate method)//Delegate method, object[] args=null)
        {
            // Uses the second Core or Processor for the Test:
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);
            // Prevents "Normal" processes from interrupting Threads:
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            // Prevents "Normal" Threads from interrupting this thread
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            Stopwatch stopwatch = new Stopwatch();
            WarmUp(stopwatch);
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < 100; i++)
            {
                method.Invoke();
            }
            stopwatch.Stop();
            long time = stopwatch.ElapsedTicks;
            return time;
        }

        public static void CloseTo(double arg1, double arg2, double by = 0.0001)
        {
            Assert.Less(Math.Abs(arg1 - arg2), by);
        }

        private static void WarmUp(Stopwatch stopwatch, int warm_up_time_in_ms=100)
        {
            stopwatch.Reset();
            stopwatch.Start();
            // A Warmup of 1000-1500 mS stabilizes the CPU cache and pipeline:
            int junk = 0;
            while (stopwatch.ElapsedMilliseconds < warm_up_time_in_ms)
            {
                junk++; // Warmup
            }
            stopwatch.Stop();
        }
    }
}
