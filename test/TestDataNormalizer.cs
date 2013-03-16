using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NeuralNetwork.src;

namespace NeuralNetwork.test
{
    class TestDataNormalizer
    {
        private DataNormalizer normalizer;
        private double[] initial_values;

        [SetUp]
        public void SetUp()
        {
            initial_values = new double[]
                {
                    -1, 0, 1, 2, 3
                };
            normalizer = new DataNormalizer(initial_values);
        }

        [Test]
        public void MaxMinValues()
        {
            double min = normalizer.GetMin();
            double max = normalizer.GetMax();
            Assert.AreEqual(-1, min / DataNormalizer.SAFE_KOEFF);
            Assert.AreEqual(3, max / DataNormalizer.SAFE_KOEFF);
        }

        [Test]
        public void ThrowsIsEmptyOrNull()
        {
            Assert.Throws(typeof (EmptyDataSetException), () => { new DataNormalizer(null); });
            Assert.Throws(typeof(EmptyDataSetException), () => { new DataNormalizer( new double[]{}); });
        }

        [Test]
        public void Normalize()
        {
            double[] values = normalizer.GetValues();
            foreach (var value in values)
            {
                Assert.GreaterOrEqual(value, 0);
                Assert.LessOrEqual(value, 1);
            }
        }

        [Test]
        public void Denormalize()
        {
            double[] values = normalizer.GetValues();
            for (int i = 0; i < values.Length; i++)
            {
                MyAssert.CloseTo(initial_values[i], normalizer.Denormalize(values[i]));
            }
        }

        [Test]
        public void DenormalizeThrowsIfGreaterThanOneOrLessThanZero()
        {
            Assert.Throws(typeof (ValueOutOfBoundsException), ()=>normalizer.Denormalize(1.1));
            Assert.Throws(typeof(ValueOutOfBoundsException), () => normalizer.Denormalize(-0.1));
        }
    }
}
