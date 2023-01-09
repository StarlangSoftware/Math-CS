using System;
using System.Collections.Generic;

namespace Math
{
    public class Matrix : ICloneable
    {
        private readonly int _row;
        private readonly int _col;
        private readonly double[][] _values;

        /**
         * <summary>Another constructor of {@link Matrix} class which takes row and column numbers as inputs and creates new values
         * {@link java.lang.reflect.Array} with given parameters.</summary>
         *
         * <param name="row">is used to create matrix.</param>
         * <param name="col">is used to create matrix.</param>
         */
        public Matrix(int row, int col)
        {
            _values = new double[row][];
            for (var i = 0; i < row; i++)
            {
                _values[i] = new double[col];
            }

            this._row = row;
            this._col = col;
        }

        /**
         * <summary>Another constructor of {@link Matrix} class which takes row, column, minimum and maximum values as inputs.
         * First it creates new values {@link java.lang.reflect.Array} with given row and column numbers. Then fills in the
         * positions with random numbers using minimum and maximum inputs.</summary>
         *
         * <param name="row">is used to create matrix.</param>
         * <param name="col">is used to create matrix.</param>
         * <param name="min">minimum value.</param>
         * <param name="max">maximum value.</param>
         * <param name="random"> random function to set the random values in the matrix.</param>
         */
        public Matrix(int row, int col, double min, double max, Random random)
        {
            _values = new double[row][];
            for (var i = 0; i < row; i++)
            {
                _values[i] = new double[col];
            }

            this._row = row;
            this._col = col;
            for (var i = 0; i < row; i++)
            {
                for (var j = 0; j < col; j++)
                {
                    _values[i][j] = min + (max - min) * random.NextDouble();
                }
            }
        }

        /**
         * <summary>Another constructor of {@link Matrix} class which takes size as input and creates new values {@link java.lang.reflect.Array}
         * with using size input and assigns 1 to each element at the diagonal.</summary>
         *
         * <param name="size">is used declaring the size of the array.</param>
         */
        public Matrix(int size)
        {
            int i;
            _values = new double[size][];
            for (i = 0; i < size; i++)
            {
                _values[i] = new double[size];
            }

            _row = size;
            _col = size;
            for (i = 0; i < size; i++)
            {
                _values[i][i] = 1;
            }
        }
        
        /**
         * <summary>The overridden clone method creates new Matrix and copies the content of values
         * {@link java.lang.reflect.Array} into new matrix.</summary>
         *
         * <returns>Matrix which is the copy of values {@link java.lang.reflect.Array}.</returns>
         */
        public object Clone()
        {
            var result = new Matrix(_row, _col);
            for (var i = 0; i < _row; i++)
                Array.Copy(_values[i], 0, result._values[i], 0, _col);
            return result;
        }

        /**
         * <summary>The getter for the index at given rowNo and colNo of values {@link java.lang.reflect.Array}.</summary>
         *
         * <param name="rowNo">integer input for row number.</param>
         * <param name="colNo">integer input for column number.</param>
         * <returns>item at given index of values {@link java.lang.reflect.Array}.</returns>
         */
        public double GetValue(int rowNo, int colNo)
        {
            return _values[rowNo][colNo];
        }

        /**
         * <summary>The setter for the value at given index of values {@link java.lang.reflect.Array}.</summary>
         *
         * <param name="rowNo">integer input for row number.</param>
         * <param name="colNo">integer input for column number.</param>
         * <param name="value">is used to set at given index.</param>
         */
        public void SetValue(int rowNo, int colNo, double value)
        {
            _values[rowNo][colNo] = value;
        }

        /**
         * <summary>The addValue method adds the given value to the item at given index of values {@link java.lang.reflect.Array}.</summary>
         *
         * <param name="rowNo">integer input for row number.</param>
         * <param name="colNo">integer input for column number.</param>
         * <param name="value">is used to add to given item at given index.</param>
         */
        public void AddValue(int rowNo, int colNo, double value)
        {
            _values[rowNo][colNo] += value;
        }

        /**
         * <summary>The increment method adds 1 to the item at given index of values {@link java.lang.reflect.Array}.</summary>
         *
         * <param name="rowNo">integer input for row number.</param>
         * <param name="colNo">integer input for column number.</param>
         */
        public void Increment(int rowNo, int colNo)
        {
            _values[rowNo][colNo] += 1;
        }

        /**
         * <summary>The getter for the row variable.</summary>
         *
         * <returns>row number.</returns>
         */
        public int GetRow()
        {
            return _row;
        }

        /**
         * <summary>The getRow method returns the vector of values {@link java.lang.reflect.Array} at given row input.</summary>
         *
         * <param name="row">integer input for row number.</param>
         * <returns>Vector of values {@link java.lang.reflect.Array} at given row input.</returns>
         */
        public Vector GetRow(int row)
        {
            return new Vector(_values[row]);
        }

        /**
         * <summary>The getColumn method creates an {@link ArrayList} and adds items at given column number of values {@link java.lang.reflect.Array}
         * to the {@link ArrayList}.</summary>
         *
         * <param name="column">integer input for column number.</param>
         * <returns>ArrayList of given column number.</returns>
         */
        public List<double> GetColumn(int column)
        {
            var vector = new List<double>();
            for (var i = 0; i < _row; i++)
            {
                vector.Add(_values[i][column]);
            }

            return vector;
        }

        /**
         * <summary>The getter for column variable.</summary>
         *
         * <returns>column variable.</returns>
         */
        public int GetColumn()
        {
            return _col;
        }

        /**
         * <summary>The columnWiseNormalize method, first accumulates items column by column then divides items by the summation.</summary>
         */
        public void ColumnWiseNormalize()
        {
            for (var i = 0; i < _row; i++)
            {
                var sum = 0.0;
                for (var j = 0; j < _col; j++)
                {
                    sum += _values[i][j];
                }

                for (var j = 0; j < _col; j++)
                {
                    _values[i][j] /= sum;
                }
            }
        }

        /**
         * <summary>The multiplyWithConstant method takes a constant as an input and multiplies each item of values {@link java.lang.reflect.Array}
         * with given constant.</summary>
         *
         * <param name="constant">value to multiply items of values {@link java.lang.reflect.Array}.</param>
         */
        public void MultiplyWithConstant(double constant)
        {
            for (var i = 0; i < _row; i++)
            {
                for (var j = 0; j < _col; j++)
                {
                    _values[i][j] *= constant;
                }
            }
        }

        /**
         * <summary>The divideByConstant method takes a constant as an input and divides each item of values {@link java.lang.reflect.Array}
         * with given constant.</summary>
         *
         * <param name="constant">value to divide items of values {@link java.lang.reflect.Array}.</param>
         */
        public void DivideByConstant(double constant)
        {
            for (var i = 0; i < _row; i++)
            {
                for (var j = 0; j < _col; j++)
                {
                    _values[i][j] /= constant;
                }
            }
        }

        /**
         * <summary>The add method takes a {@link Matrix} as an input and accumulates values {@link java.lang.reflect.Array} with the
         * corresponding items of given Matrix. If the sizes of both Matrix and values {@link java.lang.reflect.Array} do not match,
         * it throws {@link MatrixDimensionMismatch} exception</summary>
         *
         * <param name="m">Matrix type input.</param>
         */
        public void Add(Matrix m)
        {
            for (var i = 0; i < _row; i++)
            {
                for (var j = 0; j < _col; j++)
                {
                    _values[i][j] += m._values[i][j];
                }
            }
        }

        public Matrix Sum(Matrix m)
        {
            var result = new Matrix(_row, _col);
            for (var i = 0; i < _row; i++)
            {
                for (var j = 0; j < _col; j++)
                {
                    result._values[i][j] = _values[i][j] + m._values[i][j];
                }
            }

            return result;
        }

        /**
         * <summary>The add method which takes a row number and a Vector as inputs. It sums up the corresponding values at the given row of
         * values {@link java.lang.reflect.Array} and given {@link Vector}. If the sizes of both Matrix and values
         * {@link java.lang.reflect.Array} do not match, it throws {@link MatrixColumnMismatch} exception.</summary>
         *
         * <param name="rowNo">integer input for row number.</param>
         * <param name="v">    Vector type input.</param>
         */
        public void Add(int rowNo, Vector v)
        {
            for (var i = 0; i < _col; i++)
            {
                _values[rowNo][i] += v.GetValue(i);
            }
        }

        /**
         * <summary>The subtract method takes a {@link Matrix} as an input and subtracts from values {@link java.lang.reflect.Array} the
         * corresponding items of given Matrix. If the sizes of both Matrix and values {@link java.lang.reflect.Array} do not match,
         * it throws {@link MatrixDimensionMismatch} exception.</summary>
         *
         * <param name="m">Matrix type input.</param>
         */
        public void Subtract(Matrix m)
        {
            for (var i = 0; i < _row; i++)
            {
                for (var j = 0; j < _col; j++)
                {
                    _values[i][j] -= m._values[i][j];
                }
            }
        }

        public Matrix Difference(Matrix m)
        {
            var result = new Matrix(_row, _col);
            for (var i = 0; i < _row; i++)
            {
                for (var j = 0; j < _col; j++)
                {
                    result._values[i][j] = _values[i][j] - m._values[i][j];
                }
            }

            return result;
        }

        /**
         * <summary>The multiplyWithVectorFromLeft method takes a Vector as an input and creates a result {@link java.lang.reflect.Array}.
         * Then, multiplies values of input Vector starting from the left side with the values {@link java.lang.reflect.Array},
         * accumulates the multiplication, and assigns to the result {@link java.lang.reflect.Array}. If the sizes of both Vector
         * and row number do not match, it throws {@link MatrixRowMismatch} exception.</summary>
         *
         * <param name="v">{@link Vector} type input.</param>
         * <returns>Vector that holds the result.</returns>
         */
        public Vector MultiplyWithVectorFromLeft(Vector v)
        {
            var result = new double[_col];
            for (var i = 0; i < _col; i++)
            {
                result[i] = 0.0;
                for (var j = 0; j < _row; j++)
                {
                    result[i] += v.GetValue(j) * _values[j][i];
                }
            }

            return new Vector(result);
        }

        /**
         * <summary>The multiplyWithVectorFromRight method takes a Vector as an input and creates a result {@link java.lang.reflect.Array}.
         * Then, multiplies values of input Vector starting from the right side with the values {@link java.lang.reflect.Array},
         * accumulates the multiplication, and assigns to the result {@link java.lang.reflect.Array}. If the sizes of both Vector
         * and row number do not match, it throws {@link MatrixColumnMismatch} exception.</summary>
         *
         * <param name="v">{@link Vector} type input.</param>
         * <returns>Vector that holds the result.</returns>
         */
        public Vector MultiplyWithVectorFromRight(Vector v)
        {
            var result = new double[_row];
            for (var i = 0; i < _row; i++)
            {
                result[i] = 0.0;
                for (var j = 0; j < _col; j++)
                {
                    result[i] += v.GetValue(j) * _values[i][j];
                }
            }

            return new Vector(result);
        }

        /**
         * <summary>The columnSum method takes a column number as an input and accumulates items at given column number of values
         * {@link java.lang.reflect.Array}.</summary>
         *
         * <param name="columnNo">Column number input.</param>
         * <returns>summation of given column of values {@link java.lang.reflect.Array}.</returns>
         */
        public double ColumnSum(int columnNo)
        {
            double sum = 0;
            for (var i = 0; i < _row; i++)
            {
                sum += _values[i][columnNo];
            }

            return sum;
        }

        /**
         * <summary>The sumOfRows method creates a mew result {@link Vector} and adds the result of columnDum method's corresponding
         * index to the newly created result {@link Vector}.</summary>
         *
         * <returns>Vector that holds column sum.</returns>
         */
        public Vector SumOfRows()
        {
            var result = new Vector(0, 0.0);
            for (var i = 0; i < _col; i++)
            {
                result.Add(ColumnSum(i));
            }

            return result;
        }

        /**
         * <summary>The rowSum method takes a row number as an input and accumulates items at given row number of values
         * {@link java.lang.reflect.Array}.</summary>
         *
         * <param name="rowNo">Row number input.</param>
         * <returns>summation of given row of values {@link java.lang.reflect.Array}.</returns>
         */
        public double RowSum(int rowNo)
        {
            double sum = 0;
            for (var i = 0; i < _col; i++)
            {
                sum += _values[rowNo][i];
            }

            return sum;
        }

        /**
         * <summary>The multiply method takes a {@link Matrix} as an input. First it creates a result {@link Matrix} and puts the
         * accumulatated multiplication of values {@link java.lang.reflect.Array} and given {@link Matrix} into result
         * {@link Matrix}. If the size of Matrix's row size and values {@link java.lang.reflect.Array}'s column size do not match,
         * it throws {@link MatrixRowColumnMismatch} exception.</summary>
         *
         * <param name="m">Matrix type input.</param>
         * <returns>result {@link Matrix}.</returns>
         */
        public Matrix Multiply(Matrix m)
        {
            var result = new Matrix(_row, m._col);
            for (var i = 0; i < _row; i++)
            {
                for (var j = 0; j < m._col; j++)
                {
                    var sum = 0.0;
                    for (var k = 0; k < _col; k++)
                    {
                        sum += _values[i][k] * m._values[k][j];
                    }

                    result._values[i][j] = sum;
                }
            }

            return result;
        }

        /**
         * <summary>The elementProduct method takes a {@link Matrix} as an input and performs element wise multiplication. Puts result
         * to the newly created Matrix. If the size of Matrix's row and column size does not match with the values
         * {@link java.lang.reflect.Array}'s row and column size, it throws {@link MatrixDimensionMismatch} exception.</summary>
         *
         * <param name="m">Matrix type input.</param>
         * <returns>result {@link Matrix}.</returns>
         */
        public Matrix ElementProduct(Matrix m)
        {
            int i, j;
            var result = new Matrix(_row, m._col);
            for (i = 0; i < _row; i++)
            {
                for (j = 0; j < _col; j++)
                {
                    result._values[i][j] = _values[i][j] * m._values[i][j];
                }
            }

            return result;
        }

        /**
         * <summary>The sumOfElements method accumulates all the items in values {@link java.lang.reflect.Array} and
         * returns this summation.</summary>
         *
         * <returns>sum of the items of values {@link java.lang.reflect.Array}.</returns>
         */
        public double SumOfElements()
        {
            int i, j;
            var sum = 0.0;
            for (i = 0; i < _row; i++)
            {
                for (j = 0; j < _col; j++)
                {
                    sum += _values[i][j];
                }
            }

            return sum;
        }

        /**
         * <summary>The trace method accumulates items of values {@link java.lang.reflect.Array} at the diagonal.</summary>
         *
         * <returns>sum of items at diagonal.</returns>
         */
        public double Trace()
        {
            int i;
            var sum = 0.0;
            for (i = 0; i < _row; i++)
            {
                sum += _values[i][i];
            }

            return sum;
        }

        /**
         * <summary>The transpose method creates a new {@link Matrix}, then takes the transpose of values {@link java.lang.reflect.Array}
         * and puts transposition to the {@link Matrix}.</summary>
         *
         * <returns>Matrix type output.</returns>
         */
        public Matrix Transpose()
        {
            var result = new Matrix(_col, _row);
            for (var i = 0; i < _row; i++)
            {
                for (var j = 0; j < _col; j++)
                {
                    result._values[j][i] = _values[i][j];
                }
            }

            return result;
        }

        /**
         * <summary>The partial method takes 4 integer inputs; rowStart, rowEnd, colStart, colEnd and creates a {@link Matrix} size of
         * rowEnd - rowStart + 1 x colEnd - colStart + 1. Then, puts corresponding items of values {@link java.lang.reflect.Array}
         * to the new result {@link Matrix}.</summary>
         *
         * <param name="rowStart">integer input for defining starting index of row.</param>
         * <param name="rowEnd">  integer input for defining ending index of row.</param>
         * <param name="colStart">integer input for defining starting index of column.</param>
         * <param name="colEnd">  integer input for defining ending index of column.</param>
         * <returns>result Matrix.</returns>
         */
        public Matrix Partial(int rowStart, int rowEnd, int colStart, int colEnd)
        {
            int i, j;
            var result = new Matrix(rowEnd - rowStart + 1, colEnd - colStart + 1);
            for (i = rowStart; i <= rowEnd; i++)
            {
                for (j = colStart; j <= colEnd; j++)
                {
                    result._values[i - rowStart][j - colStart] = _values[i][j];
                }
            }

            return result;
        }

        /**
         * <summary>The isSymmetric method compares each item of values {@link java.lang.reflect.Array} at positions (i, j) with (j, i)
         * and returns true if they are equal, false otherwise.</summary>
         *
         * <returns>true if items are equal, false otherwise.</returns>
         */
        public bool IsSymmetric()
        {
            for (var i = 0; i < _row - 1; i++)
            {
                for (var j = i + 1; j < _row; j++)
                {
                    if (_values[i][j] != _values[j][i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /**
         * <summary>The determinant method first creates a new {@link java.lang.reflect.Array}, and copies the items of  values
         * {@link java.lang.reflect.Array} into new {@link java.lang.reflect.Array}. Then, calculates the determinant of this
         * new {@link java.lang.reflect.Array}.</summary>
         *
         * <returns>determinant of values {@link java.lang.reflect.Array}.</returns>
         */
        public double Determinant()
        {
            var det = 1.0;
            var copy = new double[_row][];
            for (var i = 0; i < _row; i++)
            {
                copy[i] = new double[_col];
            }

            for (var i = 0; i < _row; i++)
            {
                for (var j = 0; j < _col; j++)
                {
                    copy[i][j] = _values[i][j];
                }
            }

            for (var i = 0; i < _row; i++)
            {
                det *= copy[i][i];
                if (det == 0.0)
                {
                    break;
                }

                for (var j = i + 1; j < _row; j++)
                {
                    var ratio = copy[j][i] / copy[i][i];
                    for (var k = i; k < _col; k++)
                    {
                        copy[j][k] = copy[j][k] - copy[i][k] * ratio;
                    }
                }
            }

            return det;
        }

        /**
         * <summary>The inverse method finds the inverse of values {@link java.lang.reflect.Array}.</summary>
         */
        public void Inverse()
        {
            double dum;
            int i;
            int j, k, l;
            var b = new Matrix(_row);
            var indxc = new int[_row];
            var indxr = new int[_row];
            var ipiv = new int[_row];
            for (j = 0; j < _row; j++)
            {
                ipiv[j] = 0;
            }

            for (i = 1; i <= _row; i++)
            {
                var big = 0.0;
                var irow = -1;
                var icol = -1;
                for (j = 1; j <= _row; j++)
                {
                    if (ipiv[j - 1] != 1)
                    {
                        for (k = 1; k <= _row; k++)
                        {
                            if (ipiv[k - 1] == 0)
                            {
                                if (System.Math.Abs(_values[j - 1][k - 1]) >= big)
                                {
                                    big = System.Math.Abs(_values[j - 1][k - 1]);
                                    irow = j;
                                    icol = k;
                                }
                            }
                        }
                    }
                }

                ipiv[icol - 1] = ipiv[icol - 1] + 1;
                if (irow != icol)
                {
                    for (l = 1; l <= _row; l++)
                    {
                        dum = _values[irow - 1][l - 1];
                        _values[irow - 1][l - 1] = _values[icol - 1][l - 1];
                        _values[icol - 1][l - 1] = dum;
                    }

                    for (l = 1; l <= _row; l++)
                    {
                        dum = b._values[irow - 1][l - 1];
                        b._values[irow - 1][l - 1] = b._values[icol - 1][l - 1];
                        b._values[icol - 1][l - 1] = dum;
                    }
                }

                indxr[i - 1] = irow;
                indxc[i - 1] = icol;
                var pivinv = (1.0) / (_values[icol - 1][icol - 1]);
                _values[icol - 1][icol - 1] = 1.0;
                for (l = 1; l <= _row; l++)
                {
                    _values[icol - 1][l - 1] = _values[icol - 1][l - 1] * pivinv;
                }

                for (l = 1; l <= _row; l++)
                {
                    b._values[icol - 1][l - 1] = b._values[icol - 1][l - 1] * pivinv;
                }

                int ll;
                for (ll = 1; ll <= _row; ll++)
                    if (ll != icol)
                    {
                        dum = _values[ll - 1][icol - 1];
                        _values[ll - 1][icol - 1] = 0.0;
                        for (l = 1; l <= _row; l++)
                        {
                            _values[ll - 1][l - 1] = _values[ll - 1][l - 1] - _values[icol - 1][l - 1] * dum;
                        }

                        for (l = 1; l <= _row; l++)
                        {
                            b._values[ll - 1][l - 1] = b._values[ll - 1][l - 1] - b._values[icol - 1][l - 1] * dum;
                        }
                    }
            }

            for (l = _row; l >= 1; l--)
            {
                if (indxr[l - 1] != indxc[l - 1])
                {
                    for (k = 1; k <= _row; k++)
                    {
                        dum = _values[k - 1][indxr[l - 1] - 1];
                        _values[k - 1][indxr[l - 1] - 1] = _values[k - 1][indxc[l - 1] - 1];
                        _values[k - 1][indxc[l - 1] - 1] = dum;
                    }
                }
            }
        }

        /**
         * <summary>The choleskyDecomposition method creates a new {@link Matrix} and puts the Cholesky Decomposition of values Array
         * into this {@link Matrix}. Also, it throws {@link MatrixNotSymmetric} exception if it is not symmetric and
         * {@link MatrixNotPositiveDefinite} exception if the summation is negative.</summary>
         *
         * <returns>Matrix type output.</returns>
         */
        public Matrix CholeskyDecomposition()
        {
            int i, j, k;
            double sum;
            Matrix b = new Matrix(_row, _col);
            for (i = 0; i < _row; i++)
            {
                for (j = i; j < _row; j++)
                {
                    sum = _values[i][j];
                    for (k = i - 1; k >= 0; k--)
                    {
                        sum -= _values[i][k] * _values[j][k];
                    }

                    if (i == j)
                    {
                        b._values[i][i] = System.Math.Sqrt(sum);
                    }
                    else
                    {
                        b._values[j][i] = sum / b._values[i][i];
                    }
                }
            }

            return b;
        }

        /**
         * <summary>The rotate method rotates values {@link java.lang.reflect.Array} according to given inputs.</summary>
         *
         * <param name="s">  double input.</param>
         * <param name="tau">double input.</param>
         * <param name="i">  integer input.</param>
         * <param name="j">  integer input.</param>
         * <param name="k">  integer input.</param>
         * <param name="l">  integer input.</param>
         */
        private void Rotate(double s, double tau, int i, int j, int k, int l)
        {
            var g = _values[i][j];
            var h = _values[k][l];
            _values[i][j] = g - s * (h + g * tau);
            _values[k][l] = h + s * (g - h * tau);
        }

        /**
         * <summary>The characteristics method finds and returns a sorted {@link ArrayList} of {@link Eigenvector}s. And it throws
         * {@link MatrixNotSymmetric} exception if it is not symmetric.</summary>
         *
         * <returns>a sorted {@link ArrayList} of {@link Eigenvector}s.</returns>
         */
        public List<Eigenvector> Characteristics()
        {
            int iq, ip, i;
            var matrix1 = (Matrix) Clone();
            var v = new Matrix(_row, _row);
            double[] d = new double[_row];
            double[] b = new double[_row];
            double[] z = new double[_row];
            double EPS = 0.000000000000000001;
            for (ip = 0; ip < _row; ip++)
            {
                for (iq = 0; iq < _row; iq++)
                {
                    v._values[ip][iq] = 0.0;
                }

                v._values[ip][ip] = 1.0;
            }

            for (ip = 0; ip < _row; ip++)
            {
                b[ip] = d[ip] = matrix1._values[ip][ip];
                z[ip] = 0.0;
            }

            for (i = 1; i <= 50; i++)
            {
                var sm = 0.0;
                for (ip = 0; ip < _row - 1; ip++)
                for (iq = ip + 1; iq < _row; iq++)
                    sm += System.Math.Abs(matrix1._values[ip][iq]);
                if (sm == 0.0)
                {
                    break;
                }

                double threshold;
                if (i < 4)
                    threshold = 0.2 * sm / System.Math.Pow(_row, 2);
                else
                    threshold = 0.0;
                for (ip = 0; ip < _row - 1; ip++)
                {
                    for (iq = ip + 1; iq < _row; iq++)
                    {
                        var g = 100.0 * System.Math.Abs(matrix1._values[ip][iq]);
                        if (i > 4 && g <= EPS * System.Math.Abs(d[ip]) && g <= EPS * System.Math.Abs(d[iq]))
                        {
                            matrix1._values[ip][iq] = 0.0;
                        }
                        else
                        {
                            if (System.Math.Abs(matrix1._values[ip][iq]) > threshold)
                            {
                                var h = d[iq] - d[ip];
                                double t;
                                if (g <= EPS * System.Math.Abs(h))
                                {
                                    t = matrix1._values[ip][iq] / h;
                                }
                                else
                                {
                                    var theta = 0.5 * h / matrix1._values[ip][iq];
                                    t = 1.0 / (System.Math.Abs(theta) +
                                               System.Math.Sqrt(1.0 + System.Math.Pow(theta, 2)));
                                    if (theta < 0.0)
                                    {
                                        t = -t;
                                    }
                                }

                                var c = 1.0 / System.Math.Sqrt(1 + System.Math.Pow(t, 2));
                                var s = t * c;
                                var tau = s / (1.0 + c);
                                h = t * matrix1._values[ip][iq];
                                z[ip] -= h;
                                z[iq] += h;
                                d[ip] -= h;
                                d[iq] += h;
                                matrix1._values[ip][iq] = 0.0;
                                int j;
                                for (j = 0; j < ip; j++)
                                {
                                    matrix1.Rotate(s, tau, j, ip, j, iq);
                                }

                                for (j = ip + 1; j < iq; j++)
                                {
                                    matrix1.Rotate(s, tau, ip, j, j, iq);
                                }

                                for (j = iq + 1; j < _row; j++)
                                {
                                    matrix1.Rotate(s, tau, ip, j, iq, j);
                                }

                                for (j = 0; j < _row; j++)
                                {
                                    v.Rotate(s, tau, j, ip, j, iq);
                                }
                            }
                        }
                    }
                }

                for (ip = 0; ip < _row; ip++)
                {
                    b[ip] = b[ip] + z[ip];
                    d[ip] = b[ip];
                    z[ip] = 0.0;
                }
            }

            var result = new List<Eigenvector>();
            for (i = 0; i < _row; i++)
            {
                if (d[i] > 0)
                {
                    result.Add(new Eigenvector(d[i], v.GetColumn(i)));
                }
            }

            result.Sort((x,y)=>x.GetEigenValue().CompareTo(y.GetEigenValue()));
            return result;
        }
        
    }
}