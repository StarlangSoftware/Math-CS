using System;
using System.Collections.Generic;
using Math;
using NUnit.Framework;

namespace Test
{
    public class DiscreteDistributionTest
    {
        DiscreteDistribution smallDistribution;

        [SetUp]
        public void Setup()
        {
            smallDistribution = new DiscreteDistribution();
            smallDistribution.AddItem("item1");
            smallDistribution.AddItem("item2");
            smallDistribution.AddItem("item3");
            smallDistribution.AddItem("item1");
            smallDistribution.AddItem("item2");
            smallDistribution.AddItem("item1");
        }

        [Test]

        public void TestAddItem1()
        {
            Assert.AreEqual(3, smallDistribution.GetCount("item1"));
            Assert.AreEqual(2, smallDistribution.GetCount("item2"));
            Assert.AreEqual(1, smallDistribution.GetCount("item3"));
        }

        [Test]

        public void TestAddItem2()
        {
            Random random = new Random();
            DiscreteDistribution discreteDistribution = new DiscreteDistribution();
            for (int i = 0; i < 1000; i++)
            {
                discreteDistribution.AddItem("" + random.Next(1000));
            }

            int count = 0;
            for (int i = 0; i < 1000; i++)
            {
                if (discreteDistribution.ContainsItem("" + i))
                {
                    count += discreteDistribution.GetCount("" + i);
                }
            }

            Assert.AreEqual(1000, count);
        }

        [Test]

        public void TestAddItem3()
        {
            Random random = new Random();
            DiscreteDistribution discreteDistribution = new DiscreteDistribution();
            for (int i = 0; i < 1000; i++)
            {
                discreteDistribution.AddItem("" + random.Next(1000));
            }

            for (int i = 0; i < 1000000; i++)
            {
                discreteDistribution.AddItem("" + random.Next(1000000));
            }

            Assert.AreEqual(discreteDistribution.Count / 1000000.0, 0.632, 0.001);
        }

        [Test]

        public void TestRemoveItem()
        {
            smallDistribution.RemoveItem("item1");
            smallDistribution.RemoveItem("item2");
            smallDistribution.RemoveItem("item3");
            Assert.AreEqual(2, smallDistribution.GetCount("item1"));
            Assert.AreEqual(1, smallDistribution.GetCount("item2"));
            smallDistribution.AddItem("item1");
            smallDistribution.AddItem("item2");
            smallDistribution.AddItem("item3");
        }

        [Test]

        public void TestAddDistribution1()
        {
            DiscreteDistribution discreteDistribution = new DiscreteDistribution();
            discreteDistribution.AddItem("item4");
            discreteDistribution.AddItem("item5");
            discreteDistribution.AddItem("item5");
            discreteDistribution.AddItem("item2");
            smallDistribution.AddDistribution(discreteDistribution);
            Assert.AreEqual(3, smallDistribution.GetCount("item1"));
            Assert.AreEqual(3, smallDistribution.GetCount("item2"));
            Assert.AreEqual(1, smallDistribution.GetCount("item3"));
            Assert.AreEqual(1, smallDistribution.GetCount("item4"));
            Assert.AreEqual(2, smallDistribution.GetCount("item5"));
            smallDistribution.RemoveDistribution(discreteDistribution);
        }

        [Test]

        public void TestAddDistribution2()
        {
            DiscreteDistribution discreteDistribution1 = new DiscreteDistribution();
            for (int i = 0; i < 1000; i++)
            {
                discreteDistribution1.AddItem("" + i);
            }

            DiscreteDistribution discreteDistribution2 = new DiscreteDistribution();
            for (int i = 500; i < 1000; i++)
            {
                discreteDistribution2.AddItem("" + (1000 + i));
            }

            discreteDistribution1.AddDistribution(discreteDistribution2);
            Assert.AreEqual(1500, discreteDistribution1.Count);
        }

        [Test]

        public void TestRemoveDistribution()
        {
            DiscreteDistribution discreteDistribution = new DiscreteDistribution();
            discreteDistribution.AddItem("item1");
            discreteDistribution.AddItem("item1");
            discreteDistribution.AddItem("item2");
            smallDistribution.RemoveDistribution(discreteDistribution);
            Assert.AreEqual(1, smallDistribution.GetCount("item1"));
            Assert.AreEqual(1, smallDistribution.GetCount("item2"));
            Assert.AreEqual(1, smallDistribution.GetCount("item3"));
            smallDistribution.AddDistribution(discreteDistribution);
        }

        [Test]

        public void TestGetSum1()
        {
            Assert.AreEqual(6, smallDistribution.GetSum(), 0.0);
        }

        [Test]

        public void TestGetSum2()
        {
            Random random = new Random();
            DiscreteDistribution discreteDistribution = new DiscreteDistribution();
            for (int i = 0; i < 1000; i++)
            {
                discreteDistribution.AddItem("" + random.Next(1000));
            }

            Assert.AreEqual(1000, discreteDistribution.GetSum(), 0.0);
        }

        [Test]

        public void TestGetIndex()
        {
            Assert.AreEqual(0, smallDistribution.GetIndex("item1"));
            Assert.AreEqual(1, smallDistribution.GetIndex("item2"));
            Assert.AreEqual(2, smallDistribution.GetIndex("item3"));
        }

        [Test]

        public void TestContainsItem()
        {
            Assert.True(smallDistribution.ContainsItem("item1"));
            Assert.False(smallDistribution.ContainsItem("item4"));
        }

        [Test]

        public void TestGetItem()
        {
            Assert.AreEqual("item1", smallDistribution.GetItem(0));
            Assert.AreEqual("item2", smallDistribution.GetItem(1));
            Assert.AreEqual("item3", smallDistribution.GetItem(2));
        }

        [Test]

        public void TestGetValue()
        {
            Assert.AreEqual(3, smallDistribution.GetValue(0));
            Assert.AreEqual(2, smallDistribution.GetValue(1));
            Assert.AreEqual(1, smallDistribution.GetValue(2));
        }

        [Test]

        public void TestGetCount()
        {
            Assert.AreEqual(3, smallDistribution.GetCount("item1"));
            Assert.AreEqual(2, smallDistribution.GetCount("item2"));
            Assert.AreEqual(1, smallDistribution.GetCount("item3"));
        }

        [Test]

        public void TestGetMaxItem1()
        {
            Assert.AreEqual("item1", smallDistribution.GetMaxItem());
        }

        [Test]

        public void TestGetMaxItem2()
        {
            var include = new List<string> {"item2", "item3"};
            Assert.AreEqual("item2", smallDistribution.GetMaxItem(include));
        }

        [Test]

        public void TestGetProbability1()
        {
            Random random = new Random();
            DiscreteDistribution discreteDistribution = new DiscreteDistribution();
            for (int i = 0; i < 1000; i++)
            {
                discreteDistribution.AddItem("" + i);
            }

            Assert.AreEqual(0.001, discreteDistribution.GetProbability("" + random.Next(1000)), 0.0);
        }

        [Test]

        public void TestGetProbability2()
        {
            Assert.AreEqual(0.5, smallDistribution.GetProbability("item1"), 0.0);
            Assert.AreEqual(0.333333, smallDistribution.GetProbability("item2"), 0.0001);
            Assert.AreEqual(0.166667, smallDistribution.GetProbability("item3"), 0.0001);
        }

        [Test]

        public void TestGetProbabilityLaplaceSmoothing1()
        {
            Random random = new Random();
            DiscreteDistribution discreteDistribution = new DiscreteDistribution();
            for (int i = 0; i < 1000; i++)
            {
                discreteDistribution.AddItem("" + i);
            }

            Assert.AreEqual(2.0 / 2001, discreteDistribution.GetProbabilityLaplaceSmoothing("" + random.Next(1000)),
                0.0);
            Assert.AreEqual(1.0 / 2001, discreteDistribution.GetProbabilityLaplaceSmoothing("item0"), 0.0);
        }

        [Test]

        public void TestGetProbabilityLaplaceSmoothing2()
        {
            Assert.AreEqual(0.4, smallDistribution.GetProbabilityLaplaceSmoothing("item1"), 0.0);
            Assert.AreEqual(0.3, smallDistribution.GetProbabilityLaplaceSmoothing("item2"), 0.0);
            Assert.AreEqual(0.2, smallDistribution.GetProbabilityLaplaceSmoothing("item3"), 0.0);
            Assert.AreEqual(0.1, smallDistribution.GetProbabilityLaplaceSmoothing("item4"), 0.0);
        }

        [Test]

        public void TestEntropy()
        {
            Assert.AreEqual(1.4591, smallDistribution.Entropy(), 0.0001);
        }
    }
}