using Math;
using NUnit.Framework;

namespace Test
{
    public class DistributionTest
    {
        [Test]
        public void TestZNormal()
        {
            Assert.AreEqual(0.5, Distribution.ZNormal(0.0), 0.0);
            Assert.AreEqual(0.69146, Distribution.ZNormal(0.5), 0.00001);
            Assert.AreEqual(0.84134, Distribution.ZNormal(1.0), 0.00001);
            Assert.AreEqual(0.93319, Distribution.ZNormal(1.5), 0.00001);
            Assert.AreEqual(0.97725, Distribution.ZNormal(2.0), 0.00001);
            Assert.AreEqual(0.99379, Distribution.ZNormal(2.5), 0.00001);
            Assert.AreEqual(0.99865, Distribution.ZNormal(3.0), 0.00001);
            Assert.AreEqual(0.99977, Distribution.ZNormal(3.5), 0.00001);
            Assert.AreEqual(1 - Distribution.ZNormal(0.5), Distribution.ZNormal(-0.5), 0.00001);
            Assert.AreEqual(1 - Distribution.ZNormal(1.0), Distribution.ZNormal(-1.0), 0.00001);
            Assert.AreEqual(1 - Distribution.ZNormal(1.5), Distribution.ZNormal(-1.5), 0.00001);
            Assert.AreEqual(1 - Distribution.ZNormal(2.0), Distribution.ZNormal(-2.0), 0.00001);
            Assert.AreEqual(1 - Distribution.ZNormal(2.5), Distribution.ZNormal(-2.5), 0.00001);
            Assert.AreEqual(1 - Distribution.ZNormal(3.0), Distribution.ZNormal(-3.0), 0.00001);
            Assert.AreEqual(1 - Distribution.ZNormal(3.5), Distribution.ZNormal(-3.5), 0.00001);
        }

        [Test]
        public void TestZInverse()
        {
            Assert.AreEqual(0.0, Distribution.ZInverse(0.5), 0.00001);
            Assert.AreEqual(0.841621, Distribution.ZInverse(0.8), 0.00001);
            Assert.AreEqual(1.281552, Distribution.ZInverse(0.9), 0.00001);
            Assert.AreEqual(1.644854, Distribution.ZInverse(0.95), 0.00001);
            Assert.AreEqual(2.053749, Distribution.ZInverse(0.98), 0.00001);
            Assert.AreEqual(2.326348, Distribution.ZInverse(0.99), 0.00001);
            Assert.AreEqual(2.575829, Distribution.ZInverse(0.995), 0.00001);
            Assert.AreEqual(2.878162, Distribution.ZInverse(0.998), 0.00001);
            Assert.AreEqual(3.090232, Distribution.ZInverse(0.999), 0.00001);
        }

        [Test]
        public void TestChiSquare()
        {
            Assert.AreEqual(0.05, Distribution.ChiSquare(3.841, 1), 0.0001);
            Assert.AreEqual(0.005, Distribution.ChiSquare(7.879, 1), 0.0001);
            Assert.AreEqual(0.95, Distribution.ChiSquare(3.940, 10), 0.0001);
            Assert.AreEqual(0.05, Distribution.ChiSquare(18.307, 10), 0.0001);
            Assert.AreEqual(0.995, Distribution.ChiSquare(2.156, 10), 0.0001);
            Assert.AreEqual(0.005, Distribution.ChiSquare(25.188, 10), 0.0001);
            Assert.AreEqual(0.95, Distribution.ChiSquare(77.929, 100), 0.0001);
            Assert.AreEqual(0.05, Distribution.ChiSquare(124.342, 100), 0.0001);
            Assert.AreEqual(0.995, Distribution.ChiSquare(67.328, 100), 0.0001);
            Assert.AreEqual(0.005, Distribution.ChiSquare(140.169, 100), 0.0001);
        }

        [Test]
        public void TestChiSquareInverse()
        {
            Assert.AreEqual(2.706, Distribution.ChiSquareInverse(0.1, 1), 0.001);
            Assert.AreEqual(6.635, Distribution.ChiSquareInverse(0.01, 1), 0.001);
            Assert.AreEqual(4.865, Distribution.ChiSquareInverse(0.9, 10), 0.001);
            Assert.AreEqual(15.987, Distribution.ChiSquareInverse(0.1, 10), 0.001);
            Assert.AreEqual(2.558, Distribution.ChiSquareInverse(0.99, 10), 0.001);
            Assert.AreEqual(23.209, Distribution.ChiSquareInverse(0.01, 10), 0.001);
            Assert.AreEqual(82.358, Distribution.ChiSquareInverse(0.9, 100), 0.001);
            Assert.AreEqual(118.498, Distribution.ChiSquareInverse(0.1, 100), 0.001);
            Assert.AreEqual(70.065, Distribution.ChiSquareInverse(0.99, 100), 0.001);
            Assert.AreEqual(135.807, Distribution.ChiSquareInverse(0.01, 100), 0.001);
        }

        [Test]
        public void TestFDistribution()
        {
            Assert.AreEqual(0.1, Distribution.FDistribution(39.86346, 1, 1), 0.00001);
            Assert.AreEqual(0.1, Distribution.FDistribution(2.32260, 10, 10), 0.00001);
            Assert.AreEqual(0.1, Distribution.FDistribution(1.79384, 20, 20), 0.00001);
            Assert.AreEqual(0.1, Distribution.FDistribution(1.60648, 30, 30), 0.00001);
            Assert.AreEqual(0.05, Distribution.FDistribution(161.4476, 1, 1), 0.00001);
            Assert.AreEqual(0.05, Distribution.FDistribution(2.9782, 10, 10), 0.00001);
            Assert.AreEqual(0.05, Distribution.FDistribution(2.1242, 20, 20), 0.00001);
            Assert.AreEqual(0.05, Distribution.FDistribution(1.8409, 30, 30), 0.00001);
            Assert.AreEqual(0.01, Distribution.FDistribution(4052.181, 1, 1), 0.00001);
            Assert.AreEqual(0.01, Distribution.FDistribution(4.849, 10, 10), 0.00001);
            Assert.AreEqual(0.01, Distribution.FDistribution(2.938, 20, 20), 0.00001);
            Assert.AreEqual(0.01, Distribution.FDistribution(2.386, 30, 30), 0.00001);
        }

        [Test]
        public void TestFDistributionInverse()
        {
            Assert.AreEqual(3.818, Distribution.FDistributionInverse(0.01, 5, 26), 0.001);
            Assert.AreEqual(15.1010, Distribution.FDistributionInverse(0.025, 4, 3), 0.001);
            Assert.AreEqual(2.19535, Distribution.FDistributionInverse(0.1, 8, 13), 0.001);
            Assert.AreEqual(2.29871, Distribution.FDistributionInverse(0.1, 3, 27), 0.001);
            Assert.AreEqual(3.4381, Distribution.FDistributionInverse(0.05, 8, 8), 0.001);
            Assert.AreEqual(2.6283, Distribution.FDistributionInverse(0.05, 6, 19), 0.001);
            Assert.AreEqual(3.3120, Distribution.FDistributionInverse(0.025, 9, 13), 0.001);
            Assert.AreEqual(3.7505, Distribution.FDistributionInverse(0.025, 3, 23), 0.001);
            Assert.AreEqual(4.155, Distribution.FDistributionInverse(0.01, 12, 12), 0.001);
            Assert.AreEqual(6.851, Distribution.FDistributionInverse(0.01, 1, 120), 0.001);
        }

        [Test]
        public void TestTDistribution()
        {
            Assert.AreEqual(0.05, Distribution.TDistribution(6.314, 1), 0.0001);
            Assert.AreEqual(0.005, Distribution.TDistribution(63.656, 1), 0.0001);
            Assert.AreEqual(0.05, Distribution.TDistribution(1.812, 10), 0.0001);
            Assert.AreEqual(0.01, Distribution.TDistribution(2.764, 10), 0.0001);
            Assert.AreEqual(0.005, Distribution.TDistribution(3.169, 10), 0.0001);
            Assert.AreEqual(0.001, Distribution.TDistribution(4.144, 10), 0.0001);
            Assert.AreEqual(0.05, Distribution.TDistribution(1.725, 20), 0.0001);
            Assert.AreEqual(0.01, Distribution.TDistribution(2.528, 20), 0.0001);
            Assert.AreEqual(0.005, Distribution.TDistribution(2.845, 20), 0.0001);
            Assert.AreEqual(0.001, Distribution.TDistribution(3.552, 20), 0.0001);
        }

        [Test]
        public void TestTDistributionInverse()
        {
            Assert.AreEqual(2.947, Distribution.TDistributionInverse(0.005, 15), 0.001);
            Assert.AreEqual(1.717, Distribution.TDistributionInverse(0.05, 22), 0.001);
            Assert.AreEqual(3.365, Distribution.TDistributionInverse(0.01, 5), 0.001);
            Assert.AreEqual(3.922, Distribution.TDistributionInverse(0.0005, 18), 0.001);
            Assert.AreEqual(3.467, Distribution.TDistributionInverse(0.001, 24), 0.001);
            Assert.AreEqual(6.314, Distribution.TDistributionInverse(0.05, 1), 0.001);
            Assert.AreEqual(2.306, Distribution.TDistributionInverse(0.025, 8), 0.001);
            Assert.AreEqual(3.646, Distribution.TDistributionInverse(0.001, 17), 0.001);
            Assert.AreEqual(3.373, Distribution.TDistributionInverse(0.0005, 120), 0.001);
        }
    }
}