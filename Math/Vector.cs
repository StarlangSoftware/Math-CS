using System.Collections.Generic;

namespace Math
{
    public class Vector
    {
        private int _size;
        private readonly List<double> _values;

        /**
         * <summary>A constructor of {@link Vector} class which takes an {@link ArrayList} values as an input. Then, initializes
         * values {@link ArrayList} and size variable with given input and ts size.</summary>
         *
         * <param name="values">{@link ArrayList} input.</param>
         */
        public Vector(List<double> values)
        {
            this._values = values;
            _size = values.Count;
        }

        /**
         * <summary>Another constructor of {@link Vector} class which takes integer size and double x as inputs. Then, initializes size
         * variable with given size input and creates new values {@link ArrayList} and adds given input x to values {@link ArrayList}.</summary>
         *
         * <param name="size">{@link ArrayList} size.</param>
         * <param name="x">   item to add values {@link ArrayList}.</param>
         */
        public Vector(int size, double x)
        {
            this._size = size;
            _values = new List<double>();
            for (var i = 0; i < size; i++)
            {
                _values.Add(x);
            }
        }

        /**
         * <summary>Another constructor of {@link Vector} class which takes integer size, integer index and double x as inputs. Then, initializes size
         * variable with given size input and creates new values {@link ArrayList} and adds 0.0 to values {@link ArrayList}.
         * Then, sets the item of values {@link ArrayList} at given index as given input x.</summary>
         *
         * <param name="size"> {@link ArrayList} size.</param>
         * <param name="index">to set a particular item.</param>
         * <param name="x">    item to add values {@link ArrayList}'s given index.</param>
         */
        public Vector(int size, int index, double x)
        {
            this._size = size;
            _values = new List<double>();
            for (var i = 0; i < size; i++)
            {
                _values.Add(0.0);
            }

            _values[index] = x;
        }

        /**
         * <summary>Another constructor of {@link Vector} class which takes double values {@link java.lang.reflect.Array} as an input.
         * It creates new values {@link ArrayList} and adds given input values {@link java.lang.reflect.Array}'s each item to the values {@link ArrayList}.
         * Then, initializes size with given values input {@link java.lang.reflect.Array}'s length.</summary>
         *
         * <param name="values">double {@link java.lang.reflect.Array} input.</param>
         */
        public Vector(double[] values)
        {
            this._values = new List<double>();
            foreach (var value in values)
            {
                this._values.Add(value);
            }

            _size = values.Length;
        }

        /**
         * <summary>The biased method creates a {@link Vector} result, add adds each item of values {@link ArrayList} into the result Vector.
         * Then, insert 1.0 to 0th position and return result {@link Vector}.</summary>
         *
         * <returns>result {@link Vector}.</returns>
         */
        public Vector Biased()
        {
            var result = new Vector(0, 0);
            foreach (var value in _values)
            {
                result.Add(value);
            }

            result.Insert(0, 1.0);
            return result;
        }

        /**
         * <summary>The add method adds given input to the values {@link ArrayList} and increments the size variable by one.</summary>
         *
         * <param name="x">double input to add values {@link ArrayList}.</param>
         */
        public void Add(double x)
        {
            _values.Add(x);
            _size++;
        }

        /**
         * <summary>The insert method puts given input to the given index of values {@link ArrayList} and increments the size variable by one.</summary>
         *
         * <param name="pos">index to insert input.</param>
         * <param name="x">  input to insert to given index of values {@link ArrayList}.</param>
         */
        public void Insert(int pos, double x)
        {
            _values.Insert(pos, x);
            _size++;
        }

        /**
         * <summary>The remove method deletes the item at given input position of values {@link ArrayList} and decrements the size variable by one.</summary>
         *
         * <param name="pos">index to remove from values {@link ArrayList}.</param>
         */
        public void Remove(int pos)
        {
            _values.Remove(pos);
            _size--;
        }

        /**
         * <summary>The clear method sets all the elements of values {@link ArrayList} to 0.0.</summary>
         */
        public void Clear()
        {
            for (var i = 0; i < _values.Count; i++)
            {
                _values[i] = 0.0;
            }
        }

        /**
         * <summary>The sumOfElements method sums up all elements in the vector.</summary>
         *
         * <returns> Sum of all elements in the vector.</returns>
         */
        public double SumOfElements() {
            double total = 0;
            for (var i = 0; i < _size; i++) {
                total += _values[i];
            }
            return total;
        }
        
        /**
         * <summary>The maxIndex method gets the first item of values {@link ArrayList} as maximum item, then it loops through the indices
         * and if a greater value than the current maximum item comes, it updates the maximum item and returns the final
         * maximum item's index.</summary>
         *
         * <returns>final maximum item's index.</returns>
         */
        public int MaxIndex()
        {
            var index = 0;
            var max = _values[0];
            for (var i = 1; i < _size; i++)
            {
                if (_values[i] > max)
                {
                    max = _values[i];
                    index = i;
                }
            }

            return index;
        }

        /**
         * <summary>The sigmoid method loops through the values {@link ArrayList} and sets each ith item with sigmoid function, i.e
         * 1 / (1 + Math.exp(-values.get(i))), i ranges from 0 to size.</summary>
         */
        public void Sigmoid()
        {
            for (var i = 0; i < _size; i++)
            {
                _values[i] = 1 / (1 + System.Math.Exp(-_values[i]));
            }
        }

        /**
         * <summary>The skipVector method takes a mod and a value as inputs. It creates a new result Vector, and assigns given input value to i.
         * While i is less than the size, it adds the ith item of values {@link ArrayList} to the result and increments i by given mod input.</summary>
         *
         * <param name="mod">  integer input.</param>
         * <param name="value">integer input.</param>
         * <returns>result Vector.</returns>
         */
        public Vector SkipVector(int mod, int value)
        {
            var result = new Vector(0, 0);
            var i = value;
            while (i < _size)
            {
                result.Add(_values[i]);
                i += mod;
            }

            return result;
        }

        /**
         * <summary>The add method takes a {@link Vector} v as an input. It sums up the corresponding elements of both given vector's
         * values {@link ArrayList} and values {@link ArrayList} and puts result back to the values {@link ArrayList}.
         * If their sizes do not match, it throws a VectorSizeMismatch exception.</summary>
         *
         * <param name="v">Vector to add.</param>
         */
        public void Add(Vector v)
        {
            for (var i = 0; i < _size; i++)
            {
                _values[i] = _values[i] + v._values[i];
            }
        }

        /**
         * <summary>The subtract method takes a {@link Vector} v as an input. It subtracts the corresponding elements of given vector's
         * values {@link ArrayList} from values {@link ArrayList} and puts result back to the values {@link ArrayList}.
         * If their sizes do not match, it throws a VectorSizeMismatch exception.</summary>
         *
         * <param name="v">Vector to subtract from values {@link ArrayList}.</param>
         */
        public void Subtract(Vector v)
        {
            for (var i = 0; i < _size; i++)
            {
                _values[i] = _values[i] - v._values[i];
            }
        }

        /**
         * <summary>The difference method takes a {@link Vector} v as an input. It creates a new double {@link java.lang.reflect.Array} result, then
         * subtracts the corresponding elements of given vector's values {@link ArrayList} from values {@link ArrayList} and puts
         * result back to the result {@link java.lang.reflect.Array}. If their sizes do not match, it throws a VectorSizeMismatch exception.</summary>
         *
         * <param name="v">Vector to find difference from values {@link ArrayList}.</param>
         * <returns>new {@link Vector} with result {@link java.lang.reflect.Array}.</returns>
         */
        public Vector Difference(Vector v)
        {
            var result = new double[v.Size()];
            for (var i = 0; i < _size; i++)
            {
                result[i] = _values[i] - v._values[i];
            }

            return new Vector(result);
        }

        /**
         * <summary>The dotProduct method takes a {@link Vector} v as an input. It creates a new double variable result, then
         * multiplies the corresponding elements of given vector's values {@link ArrayList} with values {@link ArrayList} and assigns
         * the multiplication to the result. If their sizes do not match, it throws a VectorSizeMismatch exception.</summary>
         *
         * <param name="v">Vector to find dot product.</param>
         * <returns>double result.</returns>
         */
        public double DotProduct(Vector v)
        {
            double result = 0;
            for (var i = 0; i < _size; i++)
            {
                result += _values[i] * v._values[i];
            }

            return result;
        }

        /**
         * <summary>The dotProduct method creates a new double variable result, then squares the elements of values {@link ArrayList} and assigns
         * the accumulation to the result.</summary>
         *
         * <returns>double result.</returns>
         */
        public double DotProduct()
        {
            double result = 0;
            for (var i = 0; i < _size; i++)
            {
                result += _values[i] * _values[i];
            }

            return result;
        }

        /**
         * <summary>The elementProduct method takes a {@link Vector} v as an input. It creates a new double {@link java.lang.reflect.Array} result, then
         * multiplies the corresponding elements of given vector's values {@link ArrayList} with values {@link ArrayList} and assigns
         * the multiplication to the result {@link java.lang.reflect.Array}. If their sizes do not match, it throws a VectorSizeMismatch exception.</summary>
         *
         * <param name="v">Vector to find dot product.</param>
         * <returns>Vector with result {@link java.lang.reflect.Array}.</returns>
         */
        public Vector ElementProduct(Vector v)
        {
            var result = new double[v.Size()];
            for (var i = 0; i < _size; i++)
            {
                result[i] = _values[i] * v._values[i];
            }

            return new Vector(result);
        }

        /**
         * The multiply method takes a {@link Vector} v as an input and creates new {@link Matrix} m of [size x size of input v].
         * It loops through the the both values {@link ArrayList} and given vector's values {@link ArrayList}, then multiply
         * each item with other with other items and puts to the new {@link Matrix} m.
         *
         * @param v Vector input.
         * @return Matrix that has multiplication of two vectors.
         */
        public Matrix Multiply(Vector v)
        {
            var m = new Matrix(_size, v._size);
            for (var i = 0; i < _size; i++)
            {
                for (var j = 0; j < v._size; j++)
                {
                    m.SetValue(i, j, _values[i] * v._values[j]);
                }
            }

            return m;
        }


        /**
         * <summary>The divide method takes a double value as an input and divides each item of values {@link ArrayList} with given value.</summary>
         *
         * <param name="value">is used to divide items of values {@link ArrayList}.</param>
         */
        public void Divide(double value)
        {
            for (var i = 0; i < _size; i++)
            {
                _values[i] = _values[i] / value;
            }
        }

        /**
         * <summary>The multiply method takes a double value as an input and multiplies each item of values {@link ArrayList} with given value.</summary>
         *
         * <param name="value">is used to multiply items of values {@link ArrayList}.</param>
         */
        public void Multiply(double value)
        {
            for (var i = 0; i < _size; i++)
            {
                _values[i] = _values[i] * value;
            }
        }

        /**
         * <summary>The product method takes a double value as an input and creates a new result {@link Vector}, then multiplies each
         * item of values {@link ArrayList} with given value and adds to the result {@link Vector}.</summary>
         *
         * <param name="value">is used to multiply items of values {@link ArrayList}.</param>
         * <returns>Vector result.</returns>
         */
        public Vector Product(double value)
        {
            var result = new Vector(0, 0);
            for (var i = 0; i < _size; i++)
            {
                result.Add(_values[i] * value);
            }

            return result;
        }

        /**
         * <summary>The l1Normalize method is used to apply Least Absolute Errors, it accumulates items of values {@link ArrayList} and sets
         * each item by dividing it by the summation value.</summary>
         */
        public void L1Normalize()
        {
            double sum = 0;
            for (var i = 0; i < _size; i++)
            {
                sum += _values[i];
            }

            for (var i = 0; i < _size; i++)
            {
                _values[i] = _values[i] / sum;
            }
        }

        /**
         * <summary>The l2Norm method is used to apply Least Squares, it accumulates second power of each items of values {@link ArrayList}
         * and returns the square root of this summation.</summary>
         *
         * <returns>square root of this summation.</returns>
         */
        public double L2Norm()
        {
            double sum = 0;
            for (var i = 0; i < _size; i++)
            {
                sum += System.Math.Pow(_values[i], 2);
            }

            return System.Math.Sqrt(sum);
        }

        /**
         * <summary>The cosineSimilarity method takes a {@link Vector} v as an input and returns the result of dotProduct(v) / l2Norm() / v.l2Norm().
         * If sizes do not match it throws a {@link VectorSizeMismatch} exception.</summary>
         *
         * <param name="v">Vector input.</param>
         * <returns>dotProduct(v) / l2Norm() / v.l2Norm().</returns>
         */
        public double CosineSimilarity(Vector v)
        {
            return DotProduct(v) / L2Norm() / v.L2Norm();
        }

        /**
         * <summary>The size method returns the size of the values {@link ArrayList}.</summary>
         *
         * <returns>size of the values {@link ArrayList}.</returns>
         */
        public int Size()
        {
            return _values.Count;
        }

        /**
         * <summary>Getter for the item at given index of values {@link ArrayList}.</summary>
         *
         * <param name="index">used to get an item.</param>
         * <returns>the item at given index.</returns>
         */
        public double GetValue(int index)
        {
            return _values[index];
        }

        /**
         * <summary>Setter for the setting the value at given index of values {@link ArrayList}.</summary>
         *
         * <param name="index">to set.</param>
         * <param name="value">is used to set the given index</param>
         */
        public void SetValue(int index, double value)
        {
            _values[index] = value;
        }

        /**
         * <summary>The addValue method adds the given value to the item at given index of values {@link ArrayList}.</summary>
         *
         * <param name="index">to add the given value.</param>
         * <param name="value">value to add to given index.</param>
         */
        public void AddValue(int index, double value)
        {
            _values[index] = _values[index] + value;
        }
    }
}