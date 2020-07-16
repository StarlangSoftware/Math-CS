using System;
using System.Collections.Generic;
using Math;
using NUnit.Framework;

namespace Test
{
    public class MatrixTest
    {
        Matrix small;
        Matrix medium;
        Vector v;
        Matrix large;
        Vector V;
        Vector vr;
        Matrix random;
        Matrix identity;
        double originalSum;

        [SetUp]
        public void Setup()
        {
            small = new Matrix(3, 3);
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    small.SetValue(i, j, 1.0);
                }
            }

            v = new Vector(3, 1.0);
            large = new Matrix(1000, 1000);
            for (var i = 0; i < 1000; i++)
            {
                for (var j = 0; j < 1000; j++)
                {
                    large.SetValue(i, j, 1.0);
                }
            }

            medium = new Matrix(100, 100);
            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    medium.SetValue(i, j, 1.0);
                }
            }

            V = new Vector(1000, 1.0);
            vr = new Vector(100, 1.0);
            random = new Matrix(100, 100, 1, 10, new Random());
            originalSum = random.SumOfElements();
            identity = new Matrix(100);
        }

        [Test]

        public void TestColumnWiseNormalize()
        {
            Matrix mClone = (Matrix) small.Clone();
            mClone.ColumnWiseNormalize();
            Assert.AreEqual(3, mClone.SumOfElements(), 0.0);
            Matrix MClone = (Matrix) large.Clone();
            MClone.ColumnWiseNormalize();
            Assert.AreEqual(1000, MClone.SumOfElements(), 0.001);
            identity.ColumnWiseNormalize();
            Assert.AreEqual(100, identity.SumOfElements(), 0.0);
        }

        [Test]

        public void TestMultiplyWithConstant()
        {
            small.MultiplyWithConstant(4);
            Assert.AreEqual(36, small.SumOfElements(), 0.0);
            small.DivideByConstant(4);
            large.MultiplyWithConstant(1.001);
            Assert.AreEqual(1001000, large.SumOfElements(), 0.001);
            large.DivideByConstant(1.001);
            random.MultiplyWithConstant(3.6);
            Assert.AreEqual(originalSum * 3.6, random.SumOfElements(), 0.0001);
            random.DivideByConstant(3.6);
        }

        [Test]

        public void TestDivideByConstant()
        {
            small.DivideByConstant(4);
            Assert.AreEqual(2.25, small.SumOfElements(), 0.0);
            small.MultiplyWithConstant(4);
            large.DivideByConstant(10);
            Assert.AreEqual(100000, large.SumOfElements(), 0.001);
            large.MultiplyWithConstant(10);
            random.DivideByConstant(3.6);
            Assert.AreEqual(originalSum / 3.6, random.SumOfElements(), 0.0001);
            random.MultiplyWithConstant(3.6);
        }

        [Test]

        public void TestAdd()
        {
            random.Add(identity);
            Assert.AreEqual(originalSum + 100, random.SumOfElements(), 0.0001);
            random.Subtract(identity);
        }

        [Test]

        public void TestAddVector()
        {
            large.Add(4, V);
            Assert.AreEqual(1001000, large.SumOfElements(), 0.0);
            V.Multiply(-1.0);
            large.Add(4, V);
            V.Multiply(-1.0);
        }

        [Test]

        public void TestSubtract()
        {
            random.Subtract(identity);
            Assert.AreEqual(originalSum - 100, random.SumOfElements(), 0.0001);
            random.Add(identity);
        }

        [Test]

        public void TestMultiplyWithVectorFromLeft()
        {
            Vector result = small.MultiplyWithVectorFromLeft(v);
            Assert.AreEqual(9, result.SumOfElements(), 0.0);
            result = large.MultiplyWithVectorFromLeft(V);
            Assert.AreEqual(1000000, result.SumOfElements(), 0.0);
            result = random.MultiplyWithVectorFromLeft(vr);
            Assert.AreEqual(originalSum, result.SumOfElements(), 0.0001);
        }

        [Test]

        public void TestMultiplyWithVectorFromRight()
        {
            Vector result = small.MultiplyWithVectorFromRight(v);
            Assert.AreEqual(9, result.SumOfElements(), 0.0);
            result = large.MultiplyWithVectorFromRight(V);
            Assert.AreEqual(1000000, result.SumOfElements(), 0.0);
            result = random.MultiplyWithVectorFromRight(vr);
            Assert.AreEqual(originalSum, result.SumOfElements(), 0.0001);
        }

        [Test]

        public void TestColumnSum()
        {
            Random rand = new Random();
            Assert.AreEqual(3, small.ColumnSum(rand.Next(3)), 0.0);
            Assert.AreEqual(1000, large.ColumnSum(rand.Next(1000)), 0.0);
            Assert.AreEqual(1, identity.ColumnSum(rand.Next(100)), 0.0);
        }

        [Test]

        public void TestSumOfRows()
        {
            Assert.AreEqual(9, small.SumOfRows().SumOfElements(), 0.0);
            Assert.AreEqual(1000000, large.SumOfRows().SumOfElements(), 0.0);
            Assert.AreEqual(100, identity.SumOfRows().SumOfElements(), 0.0);
            Assert.AreEqual(originalSum, random.SumOfRows().SumOfElements(), 0.001);
        }

        [Test]

        public void TestRowSum()
        {
            Random rand = new Random();
            Assert.AreEqual(3, small.RowSum(rand.Next(3)), 0.0);
            Assert.AreEqual(1000, large.RowSum(rand.Next(1000)), 0.0);
            Assert.AreEqual(1, identity.RowSum(rand.Next(100)), 0.0);
        }

        [Test]

        public void TestMultiply()
        {
            Matrix result = small.Multiply(small);
            Assert.AreEqual(27, result.SumOfElements(), 0.0);
            result = large.Multiply(large);
            Assert.AreEqual(1000000000.0, result.SumOfElements(), 0.0);
            result = random.Multiply(identity);
            Assert.AreEqual(originalSum, result.SumOfElements(), 0.0);
            result = identity.Multiply(random);
            Assert.AreEqual(originalSum, result.SumOfElements(), 0.0);
        }

        [Test]

        public void TestElementProduct()
        {
            Matrix result = small.ElementProduct(small);
            Assert.AreEqual(9, result.SumOfElements(), 0.0);
            result = large.ElementProduct(large);
            Assert.AreEqual(1000000, result.SumOfElements(), 0.0);
            result = random.ElementProduct(identity);
            Assert.AreEqual(result.Trace(), result.SumOfElements(), 0.0);
        }

        [Test]

        public void TestSumOfElements()
        {
            Assert.AreEqual(9, small.SumOfElements(), 0.0);
            Assert.AreEqual(1000000, large.SumOfElements(), 0.0);
            Assert.AreEqual(100, identity.SumOfElements(), 0.0);
            Assert.AreEqual(originalSum, random.SumOfElements(), 0.0);
        }

        [Test]

        public void TestTrace()
        {
            Assert.AreEqual(3, small.Trace(), 0.0);
            Assert.AreEqual(1000, large.Trace(), 0.0);
            Assert.AreEqual(100, identity.Trace(), 0.0);
        }

        [Test]

        public void TestTranspose()
        {
            Assert.AreEqual(9, small.Transpose().SumOfElements(), 0.0);
            Assert.AreEqual(1000000, large.Transpose().SumOfElements(), 0.0);
            Assert.AreEqual(100, identity.Transpose().SumOfElements(), 0.0);
            Assert.AreEqual(originalSum, random.Transpose().SumOfElements(), 0.001);
        }

        [Test]

        public void TestIsSymmetric()
        {
            Assert.True(small.IsSymmetric());
            Assert.True(large.IsSymmetric());
            Assert.True(identity.IsSymmetric());
            Assert.False(random.IsSymmetric());
        }

        [Test]

        public void TestDeterminant()
        {
            Assert.AreEqual(0, small.Determinant(), 0.0);
            Assert.AreEqual(0, large.Determinant(), 0.0);
            Assert.AreEqual(1, identity.Determinant(), 0.0);
        }

        [Test]

        public void TestInverse()
        {
            identity.Inverse();
            Assert.AreEqual(100, identity.SumOfElements(), 0.0);
            random.Inverse();
            random.Inverse();
            Assert.AreEqual(originalSum, random.SumOfElements(), 0.00001);
        }

        [Test]

        public void TestCharacteristics()
        {
            List<Eigenvector> vectors = small.Characteristics();
            Assert.AreEqual(2, vectors.Count, 0);
            vectors = identity.Characteristics();
            Assert.AreEqual(100, vectors.Count, 0);
            vectors = medium.Characteristics();
            Assert.AreEqual(46, vectors.Count, 0);
        }
    }
}