using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NeuralNetwork.Properties;
using NeuralNetwork.src;

namespace NeuralNetwork.test
{
    class TestDataParser
    {
        private GoogleParser parser;
        private String path;
        private String path_cola = Path.GetFullPath(@"./../../data/ko.csv");
        private String path_epam = Path.GetFullPath(@"./../../data/epam.csv");
        private String path_google = Path.GetFullPath(@"./../../data/goog.csv");
        
        [SetUp]
        public void InitializeParser()
        {
            parser = new GoogleParser();
            path = @"./../../data/ko.csv";
            path = Path.GetFullPath(path);
            Assert.NotNull(parser);
        }

        [Test]
        public void SourceExists()
        {
            Assert.True(File.Exists(path));
            Assert.True(File.Exists(path_cola));
            Assert.True(File.Exists(path_google));
            Assert.True(File.Exists(path_epam));
        }

        [Test]
        public void TestParseLine()
        {
            string line = "4-Oct-12,38.46,38.55,38.19,38.33,9654464";
            double entry = parser.ParseLine(line);
            MyAssert.CloseTo(38.33, entry);
        }

        [Test]
        public void TestParseFile()
        {
            double[] entries = parser.ParseFile(path_cola);
            Assert.AreEqual(1272, entries.Length);
            MyAssert.CloseTo(29.33, entries[entries.Length-1]);
        }

        [Test]
        public void TestDeltas()
        {
            double[] absolutes = new double[]{1, 3, 4, 2};
            double[] deltas = parser.GetDeltas(absolutes);
            Assert.AreEqual(absolutes.Length-1, deltas.Length);
            Assert.AreEqual(deltas, new double[]{2, 1, -2});
        }
    }
}
