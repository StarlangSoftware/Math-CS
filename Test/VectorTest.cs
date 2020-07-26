using Math;
using NUnit.Framework;

namespace Test
{
    public class VectorTest
    {
        Vector smallVector1, smallVector2;
        Vector largeVector1, largeVector2;
        double[] data1 = {2, 3, 4, 5, 6};

        [SetUp]
        public void Setup()
        {
            double[] data2 = {8, 7, 6, 5, 4};
            smallVector1 = new Vector(data1);
            smallVector2 = new Vector(data2);
            var largeData1 = new double[1000];
            for (var i = 1; i <= 1000; i++)
            {
                largeData1[i - 1] = i;
            }

            largeVector1 = new Vector(largeData1);
            var largeData2 = new double[1000];
            for (var i = 1; i <= 1000; i++)
            {
                largeData2[i - 1] = 1000 - i + 1;
            }

            largeVector2 = new Vector(largeData2);
        }

        [Test]
        public void TestBiased()
        {
            Vector biased = smallVector1.Biased();
            Assert.AreEqual(1, biased.GetValue(0), 0.0);
            Assert.AreEqual(smallVector1.Size() + 1, biased.Size());
        }

        [Test]
        public void TestElementAdd()
        {
            smallVector1.Add(7);
            Assert.AreEqual(7, smallVector1.GetValue(5), 0.0);
            Assert.AreEqual(6, smallVector1.Size());
            smallVector1.Remove(5);
        }

        [Test]
        public void TestInsert()
        {
            smallVector1.Insert(3, 6);
            Assert.AreEqual(6, smallVector1.GetValue(3), 0.0);
            Assert.AreEqual(6, smallVector1.Size());
            smallVector1.Remove(3);
        }

        [Test]
        public void TestRemove()
        {
            smallVector1.Remove(2);
            Assert.AreEqual(2, smallVector1.GetValue(0), 0.0);
            Assert.AreEqual(4, smallVector1.Size());
            smallVector1.Insert(2, 4);
        }

        [Test]
        public void TestSumOfElementsSmall()
        {
            Assert.AreEqual(20, smallVector1.SumOfElements(), 0.0);
            Assert.AreEqual(30, smallVector2.SumOfElements(), 0.0);
        }

        [Test]
        public void TestSumOfElementsLarge()
        {
            Assert.AreEqual(20, smallVector1.SumOfElements(), 0.0);
            Assert.AreEqual(30, smallVector2.SumOfElements(), 0.0);
            Assert.AreEqual(500500, largeVector1.SumOfElements(), 0.0);
            Assert.AreEqual(500500, largeVector2.SumOfElements(), 0.0);
        }

        [Test]
        public void TestMaxIndex()
        {
            Assert.AreEqual(4, smallVector1.MaxIndex());
            Assert.AreEqual(0, smallVector2.MaxIndex());
        }

        [Test]
        public void TestSigmoid()
        {
            Vector smallVector3 = new Vector(data1);
            smallVector3.Sigmoid();
            Assert.AreEqual(0.8807971, smallVector3.GetValue(0), 0.000001);
            Assert.AreEqual(0.9975274, smallVector3.GetValue(4), 0.000001);
        }

        [Test]
        public void TestSkipVectorSmall()
        {
            var smallVector3 = smallVector1.SkipVector(2, 0);
            Assert.AreEqual(2, smallVector3.GetValue(0), 0);
            Assert.AreEqual(6, smallVector3.GetValue(2), 0);
            smallVector3 = smallVector1.SkipVector(3, 1);
            Assert.AreEqual(3, smallVector3.GetValue(0), 0);
            Assert.AreEqual(6, smallVector3.GetValue(1), 0);
        }

        [Test]
        public void TestSkipVectorLarge()
        {
            var largeVector3 = largeVector1.SkipVector(2, 0);
            Assert.AreEqual(250000, largeVector3.SumOfElements(), 0);
            largeVector3 = largeVector1.SkipVector(5, 3);
            Assert.AreEqual(100300, largeVector3.SumOfElements(), 0);
        }

        [Test]
        public void TestVectorAddSmall()
        {
            smallVector1.Add(smallVector2);
            Assert.AreEqual(50, smallVector1.SumOfElements(), 0.0);
            smallVector1.Subtract(smallVector2);
        }

        [Test]
        public void TestVectorAddLarge()
        {
            largeVector1.Add(largeVector2);
            Assert.AreEqual(1001000, largeVector1.SumOfElements(), 0.0);
            largeVector1.Subtract(largeVector2);
        }

        [Test]
        public void TestSubtractSmall()
        {
            smallVector1.Subtract(smallVector2);
            Assert.AreEqual(-10, smallVector1.SumOfElements(), 0.0);
            smallVector1.Add(smallVector2);
        }

        [Test]
        public void TestSubtractLarge()
        {
            largeVector1.Subtract(largeVector2);
            Assert.AreEqual(0, largeVector1.SumOfElements(), 0.0);
            largeVector1.Add(largeVector2);
        }

        [Test]
        public void TestDifferenceSmall()
        {
            Vector smallVector3 = smallVector1.Difference(smallVector2);
            Assert.AreEqual(-10, smallVector3.SumOfElements(), 0.0);
        }

        [Test]
        public void TestDifferenceLarge()
        {
            Vector largeVector3 = largeVector1.Difference(largeVector2);
            Assert.AreEqual(0, largeVector3.SumOfElements(), 0.0);
        }

        [Test]
        public void TestDotProductWithVectorSmall()
        {
            double dotProduct = smallVector1.DotProduct(smallVector2);
            Assert.AreEqual(110, dotProduct, 0.0);
        }

        [Test]
        public void TestDotProductWithVectorLarge()
        {
            double dotProduct = largeVector1.DotProduct(largeVector2);
            Assert.AreEqual(167167000, dotProduct, 0.0);
        }

        [Test]
        public void TestDotProductWithItselfSmall()
        {
            double dotProduct = smallVector1.DotProduct();
            Assert.AreEqual(90, dotProduct, 0.0);
        }

        [Test]
        public void TestDotProductWithItselfLarge()
        {
            double dotProduct = largeVector1.DotProduct();
            Assert.AreEqual(333833500, dotProduct, 0.0);
        }

        [Test]
        public void TestElementProductSmall()
        {
            Vector smallVector3 = smallVector1.ElementProduct(smallVector2);
            Assert.AreEqual(110, smallVector3.SumOfElements(), 0.0);
        }

        [Test]
        public void TestElementProductLarge()
        {
            Vector largeVector3 = largeVector1.ElementProduct(largeVector2);
            Assert.AreEqual(167167000, largeVector3.SumOfElements(), 0.0);
        }

        [Test]
        public void TestDivide()
        {
            smallVector1.Divide(10.0);
            Assert.AreEqual(2, smallVector1.SumOfElements(), 0.0);
            smallVector1.Multiply(10.0);
        }

        [Test]
        public void TestMultiply()
        {
            smallVector1.Multiply(10.0);
            Assert.AreEqual(200, smallVector1.SumOfElements(), 0.0);
            smallVector1.Divide(10.0);
        }

        [Test]
        public void TestProduct()
        {
            Vector smallVector3 = smallVector1.Product(7.0);
            Assert.AreEqual(140, smallVector3.SumOfElements(), 0.0);
        }

        [Test]
        public void TestL1NormalizeSmall()
        {
            smallVector1.L1Normalize();
            Assert.AreEqual(1.0, smallVector1.SumOfElements(), 0.0);
            smallVector1.Multiply(20);
        }

        [Test]
        public void TestL1NormalizeLarge()
        {
            largeVector1.L1Normalize();
            Assert.AreEqual(1.0, largeVector1.SumOfElements(), 0.0);
            largeVector1.Multiply(500500);
        }

        [Test]
        public void TestL2NormSmall()
        {
            double norm = smallVector1.L2Norm();
            Assert.AreEqual(norm, System.Math.Sqrt(90), 0.0);
        }

        [Test]
        public void TestL2NormLarge()
        {
            double norm = largeVector1.L2Norm();
            Assert.AreEqual(norm, System.Math.Sqrt(333833500), 0.0);
        }

        [Test]
        public void TestCosineSimilaritySmall()
        {
            double similarity = smallVector1.CosineSimilarity(smallVector2);
            Assert.AreEqual(0.8411910, similarity, 0.000001);
        }

        [Test]
        public void TestCosineSimilarityLarge()
        {
            double similarity = largeVector1.CosineSimilarity(largeVector2);
            Assert.AreEqual(0.5007497, similarity, 0.000001);
        }
    }
}