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
        private DataNormalizer sigm_normalizer;
        private DataNormalizer hyper_normalizer;
        private double[] initial_values;

        [SetUp]
        public void SetUp()
        {
            initial_values = new double[]
                {
                    -2, -1, 0, 1, 2, 3
                };
            sigm_normalizer = DataNormalizer.SigmoidNormalizer(initial_values);
            hyper_normalizer = DataNormalizer.HyperbolicNormalizer(initial_values);
        }

        [Test]
        public void MaxMinValues()
        {
            double min = sigm_normalizer.GetMin();
            double max = sigm_normalizer.GetMax();
            Assert.AreEqual(-2, min / DataNormalizer.SAFE_KOEFF);
            Assert.AreEqual(3, max / DataNormalizer.SAFE_KOEFF);
        }

        [Test]
        public void ThrowsIsEmptyOrNull()
        {
            Assert.Throws(typeof (EmptyDataSetException), () => { DataNormalizer.SigmoidNormalizer(null); });
            Assert.Throws(typeof(EmptyDataSetException), () => { DataNormalizer.SigmoidNormalizer(new double[]{}); });
            Assert.Throws(typeof(EmptyDataSetException), () => { DataNormalizer.HyperbolicNormalizer(null); });
            Assert.Throws(typeof(EmptyDataSetException), () => { DataNormalizer.HyperbolicNormalizer(new double[] { }); });
        }

        [Test]
        public void SigmNormalize()
        {
            double[] values = sigm_normalizer.GetValues();
            foreach (var value in values)
            {
                Assert.GreaterOrEqual(value, 0);
                Assert.LessOrEqual(value, 1);
            }
        }

        [Test]
        public void HyperNormalize()
        {
            double[] values = hyper_normalizer.GetValues();
            double min = values[0];
            double max = values[0];
            foreach (var value in values)
            {
                Assert.GreaterOrEqual(value, -1);
                Assert.LessOrEqual(value, 1);
                if (min > value) min = value;
                if (max < value) max = value;
            }
            MyAssert.CloseTo(min * DataNormalizer.SAFE_KOEFF, -1, 0.025);
            MyAssert.CloseTo(max * DataNormalizer.SAFE_KOEFF, 1, 0.025);
        }

        [Test]
        public void SigmDenormalize()
        {
            double[] values = sigm_normalizer.GetValues();
            for (int i = 0; i < values.Length; i++)
            {
                MyAssert.CloseTo(initial_values[i], sigm_normalizer.Denormalize(values[i]));
            }
        }

        [Test]
        public void HyperDenormalize()
        {
            double[] values = hyper_normalizer.GetValues();
            for (int i = 0; i < values.Length; i++)
            {
                MyAssert.CloseTo(initial_values[i], hyper_normalizer.Denormalize(values[i]));
            }
        }

        [Test]
        public void DenormalizeThrowsIfGreaterThanOneOrLessThanZero()
        {
            Assert.Throws(typeof (ValueOutOfBoundsException), ()=>sigm_normalizer.Denormalize(1.1));
            Assert.Throws(typeof(ValueOutOfBoundsException), () => sigm_normalizer.Denormalize(-0.1));
            Assert.Throws(typeof(ValueOutOfBoundsException), () => hyper_normalizer.Denormalize(1.1));
            Assert.Throws(typeof(ValueOutOfBoundsException), () => hyper_normalizer.Denormalize(-1.1));
        }
    }
}
