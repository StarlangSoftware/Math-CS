using System;
using System.Collections.Generic;

namespace Math
{
    public class DiscreteDistribution : Dictionary<string, int>
    {
        private double _sum;

        /**
         * <summary>The addItem method takes a String item as an input and if this map contains a mapping for the item it puts the item
         * with given value + 1, else it puts item with value of 1.</summary>
         *
         * <param name="item">String input.</param>
         */
        public void AddItem(string item)
        {
            if (ContainsKey(item))
            {
                this[item] = this[item] + 1;
            }
            else
            {
                Add(item, 1);
            }

            _sum++;
        }

        /**
         * <summary>The removeItem method takes a String item as an input and if this map contains a mapping for the item it puts the item
         * with given value - 1, and if its value is 0, it removes the item.</summary>
         *
         * <param name="item">String input.</param>
         */
        public void RemoveItem(string item)
        {
            if (ContainsKey(item))
            {
                this[item] = this[item] - 1;
                if (this[item] == 0)
                {
                    Remove(item);
                }
                _sum--;
            }
        }

        /**
         * <summary>The addDistribution method takes a {@link DiscreteDistribution} as an input and loops through the entries in this distribution
         * and if this map contains a mapping for the entry it puts the entry with its value + entry, else it puts entry with its value.
         * It also accumulates the values of entries and assigns to the sum variable.</summary>
         *
         * <param name="distribution">{@link DiscreteDistribution} type input.</param>
         */
        public void AddDistribution(DiscreteDistribution distribution)
        {
            foreach (var entry in distribution.Keys)
            {
                if (ContainsKey(entry))
                {
                    this[entry] = this[entry] + distribution[entry];
                }
                else
                {
                    Add(entry, distribution[entry]);
                }

                _sum += distribution[entry];
            }
        }

        /**
         * <summary>The removeDistribution method takes a {@link DiscreteDistribution} as an input and loops through the entries in this distribution
         * and if this map contains a mapping for the entry it puts the entry with its key - value, else it removes the entry.
         * It also decrements the value of entry from sum and assigns to the sum variable.</summary>
         *
         * <param name="distribution">{@link DiscreteDistribution} type input.</param>
         */
        public void RemoveDistribution(DiscreteDistribution distribution)
        {
            foreach (var entry in distribution.Keys)
            {
                if (this[entry] - distribution[entry] != 0)
                {
                    this[entry] = this[entry] - distribution[entry];
                }
                else
                {
                    Remove(entry);
                }

                _sum -= distribution[entry];
            }
        }

        /**
         * <summary>The getter for sum variable.</summary>
         *
         * <returns>sum.</returns>
         */
        public double GetSum()
        {
            return _sum;
        }

        /**
         * <summary>The getIndex method takes an item as an input and returns the index of given item.</summary>
         *
         * <param name="item">to search for index.</param>
         * <returns>index of given item.</returns>
         */
        public int GetIndex(string item)
        {
            return new List<string>(Keys).IndexOf(item);
        }

        /**
         * <summary>The containsItem method takes an item as an input and returns true if this map contains a mapping for the
         * given item.</summary>
         *
         * <param name="item">to check.</param>
         * <returns>true if this map contains a mapping for the given item.</returns>
         */
        public bool ContainsItem(string item)
        {
            return ContainsKey(item);
        }

        /**
         * <summary>The getItem method takes an index as an input and returns the item at given index.</summary>
         *
         * <param name="index">is used for searching the item.</param>
         * <returns>the item at given index.</returns>
         */
        public string GetItem(int index)
        {
            return new List<string>(Keys)[index];
        }

        /**
         * <summary>The getValue method takes an index as an input and returns the value at given index.</summary>
         *
         * <param name="index">is used for searching the value.</param>
         * <returns>the value at given index.</returns>
         */
        public int GetValue(int index)
        {
            return this[new List<string>(Keys)[index]];
        }

        /**
         * <summary>The getCount method takes an item as an input returns the value to which the specified item is mapped, or {@code null}
         * if this map contains no mapping for the key.</summary>
         *
         * <param name="item">is used to search for value.</param>
         * <returns>the value to which the specified item is mapped</returns>
         */
        public int GetCount(string item)
        {
            return this[item];
        }

        /**
         * <summary>The getMaxItem method loops through the entries and gets the entry with maximum value.</summary>
         *
         * <returns>the entry with maximum value.</returns>
         */
        public string GetMaxItem()
        {
            var max = -1;
            string maxItem = null;
            foreach (var entry in Keys)
            {
                if (this[entry] > max)
                {
                    max = this[entry];
                    maxItem = entry;
                }
            }

            return maxItem;
        }

        /**
         * <summary>Another getMaxItem method which takes an {@link ArrayList} of Strings. It loops through the items in this {@link ArrayList}
         * and gets the item with maximum value.</summary>
         *
         * <param name="includeTheseOnly">{@link ArrayList} of Strings.</param>
         * <returns>the item with maximum value.</returns>
         */
        public string GetMaxItem(List<string> includeTheseOnly)
        {
            var max = -1;
            string maxItem = null;
            foreach (var item in includeTheseOnly)
            {
                int frequency = 0;
                if (ContainsItem(item))
                {
                    frequency = this[item];
                }

                if (frequency > max)
                {
                    max = frequency;
                    maxItem = item;
                }
            }

            return maxItem;
        }

        /**
         * <summary>The getProbability method takes an item as an input returns the value to which the specified item is mapped over sum,
         * or 0.0 if this map contains no mapping for the key.</summary>
         *
         * <param name="item">is used to search for probability.</param>
         * <returns>the probability to which the specified item is mapped.</returns>
         */
        public double GetProbability(string item)
        {
            if (ContainsKey(item))
            {
                return this[item] / _sum;
            }

            return 0.0;
        }

        public Dictionary<string, double> GetProbabilityDistribution()
        {
            var result = new Dictionary<string, double>();
            foreach (var item in Keys)
            {
                result[item] = GetProbability(item);
            }

            return result;
        }


        /**
         * <summary>The getProbabilityLaplaceSmoothing method takes an item as an input returns the smoothed value to which the specified
         * item is mapped over sum, or 1.0 over sum if this map contains no mapping for the key.</summary>
         *
         * <param name="item">is used to search for probability.</param>
         * <returns>the smoothed probability to which the specified item is mapped.</returns>
         */
        public double GetProbabilityLaplaceSmoothing(string item)
        {
            if (ContainsKey(item))
            {
                return (this[item] + 1) / (_sum + Count + 1);
            }

            return 1.0 / (_sum + Count + 1);
        }

        /**
         * <summary>The entropy method loops through the values and calculates the entropy of these values.</summary>
         *
         * <returns>entropy value.</returns>
         */
        public double Entropy()
        {
            var total = 0.0;
            foreach (var count in Values)
            {
                var probability = count / _sum;
                total += -probability * (System.Math.Log(probability) / System.Math.Log(2));
            }

            return total;
        }
    }
}