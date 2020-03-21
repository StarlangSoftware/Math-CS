namespace Math
{
    public class Distribution
    {
        private static readonly double Z_MAX = 6.0;
        private static readonly double Z_EPSILON = 0.000001;
        private static readonly double CHI_EPSILON = 0.000001;
        private static readonly double CHI_MAX = 99999.0;
        private static readonly double LOG_SQRT_PI = 0.5723649429247000870717135;
        private static readonly double I_SQRT_PI = 0.5641895835477562869480795;
        private static readonly double BIGX = 200.0;
        private static readonly double I_PI = 0.3183098861837906715377675;
        private static readonly double F_EPSILON = 0.000001;
        private static readonly double F_MAX = 9999.0;

        /**
         * <summary>The ex method takes a double x as an input, if x is less than -BIGX it returns 0, otherwise it returns Euler's number
         * <i>e</i> raised to the power of x.</summary>
         *
         * <param name="x">double input.</param>
         * <returns>0 if input is less than -BIGX, Euler's number <i>e</i> raised to the power of x otherwise.</returns>
         */
        private static double Ex(double x)
        {
            if (x < -BIGX)
            {
                return 0;
            }

            return System.Math.Exp(x);
        }

        /**
         * <summary>The beta method takes a double {@link java.lang.reflect.Array} x as an input. It loops through x and accumulates
         * the value of gammaLn(x), also it sums up the items of x and returns (accumulated result - gammaLn of this summation).</summary>
         *
         * <param name="x">double {@link java.lang.reflect.Array} input.</param>
         * <returns>gammaLn(sum).</returns>
         */
        public static double Beta(double[] x)
        {
            double sum = 0.0, result = 0.0;
            int i;
            for (i = 0; i < x.Length; i++)
            {
                result += GammaLn(x[i]);
                sum += x[i];
            }

            result -= GammaLn(sum);
            return result;
        }

        /**
         * <summary>The gammaLn method takes a double x as an input and returns the logarithmic result of the gamma distribution at point x.</summary>
         *
         * <param name="x">double input.</param>
         * <returns>the logarithmic result of the gamma distribution at point x.</returns>
         */
        public static double GammaLn(double x)
        {
            double[] cof =
            {
                76.18009172947146, -86.50532032941677, 24.01409824083091, -1.231739572450155, 0.1208650973866179e-2,
                -0.5395239384953e-5
            };
            int j;
            var y = x;
            var tmp = x + 5.5;
            tmp -= (x + 0.5) * System.Math.Log(tmp);
            var ser = 1.000000000190015;
            for (j = 0; j <= 5; j++)
            {
                ser += cof[j] / ++y;
            }

            return -tmp + System.Math.Log(2.5066282746310005 * ser / x);
        }

        /**
         * <summary>The zNormal method performs the Z-Normalization. It ensures, that all elements of the input vector are transformed
         * into the output vector whose mean is approximately 0 while the standard deviation is in a range close to 1.</summary>
         *
         * <param name="z">double input.</param>
         * <returns>normalized value of given input.</returns>
         */
        public static double ZNormal(double z)
        {
            double x;
            if (z == 0.0)
            {
                x = 0.0;
            }
            else
            {
                var y = 0.5 * System.Math.Abs(z);
                if (y >= Z_MAX * 0.5)
                {
                    x = 1.0;
                }
                else
                {
                    if (y < 1.0)
                    {
                        var w = y * y;
                        x = ((((((((0.000124818987 * w - 0.001075204047) * w + 0.005198775019) * w - 0.019198292004) *
                                w + 0.059054035642) * w - 0.151968751364) * w + 0.319152932694) * w - 0.531923007300) *
                            w + 0.797884560593) * y * 2.0;
                    }
                    else
                    {
                        y -= 2.0;
                        x = (((((((((((((-0.000045255659 * y + 0.000152529290) * y - 0.000019538132) * y -
                                       0.000676904986) * y + 0.001390604284) * y - 0.000794620820) * y -
                                    0.002034254874) * y + 0.006549791214) * y - 0.010557625006) * y + 0.011630447319) *
                                y - 0.009279453341) * y + 0.005353579108) * y - 0.002141268741) * y + 0.000535310849) *
                            y + 0.999936657524;
                    }
                }
            }

            if (z > 0.0)
            {
                return (x + 1.0) * 0.5;
            }

            return (1.0 - x) * 0.5;
        }

        /**
         * <summary>he zInverse method returns the Z-Inverse of given probability value.</summary>
         *
         * <param name="p">double probability.</param>
         * <returns>the Z-Inverse of given probability.</returns>
         */
        public static double ZInverse(double p)
        {
            double minZ = -Z_MAX;
            double maxZ = Z_MAX;
            double zValue = 0.0;
            if (p <= 0.0 || p >= 1.0)
            {
                return 0.0;
            }

            while (maxZ - minZ > Z_EPSILON)
            {
                var pValue = ZNormal(zValue);
                if (pValue > p)
                {
                    maxZ = zValue;
                }
                else
                {
                    minZ = zValue;
                }

                zValue = (maxZ + minZ) * 0.5;
            }

            return zValue;
        }

        /**
         * <summary>The chiSquare method is used to determine whether there is a significant difference between the expected
         * frequencies and the observed frequencies in one or more categories. It takes a double input x and an integer freedom
         * for degrees of freedom as inputs. It returns the Chi Squared result.</summary>
         *
         * <param name="x">      double input.</param>
         * <param name="freedom">integer input for degrees of freedom.</param>
         * <returns>the Chi Squared result.</returns>
         */
        public static double ChiSquare(double x, int freedom)
        {
            double y = 0;
            if (x <= 0.0 || freedom < 1)
            {
                return 1.0;
            }

            var a = 0.5 * x;
            var even = freedom % 2 == 0;
            if (freedom > 1)
            {
                y = Ex(-a);
            }

            var s = even ? y : 2.0 * ZNormal(-System.Math.Sqrt(x));

            if (freedom > 2)
            {
                x = 0.5 * (freedom - 1.0);
                var z = even ? 1.0 : 0.5;

                double e;
                double c;
                if (a > BIGX)
                {
                    e = even ? 0.0 : LOG_SQRT_PI;

                    c = System.Math.Log(a);
                    while (z <= x)
                    {
                        e = System.Math.Log(z) + e;
                        s += Ex(c * z - a - e);
                        z += 1.0;
                    }

                    return s;
                }

                if (even)
                {
                    e = 1.0;
                }
                else
                {
                    e = I_SQRT_PI / System.Math.Sqrt(a);
                }

                c = 0.0;
                while (z <= x)
                {
                    e *= a / z;
                    c += e;
                    z += 1.0;
                }

                return c * y + s;
            }

            return s;
        }

        /**
         * <summary>The chiSquareInverse method returns the Chi Square-Inverse of given probability value with given degree of freedom.</summary>
         *
         * <param name="p">      double probability.</param>
         * <param name="freedom">integer input for degrees of freedom.</param>
         * <returns>the chiSquare-Inverse of given probability.</returns>
         */
        public static double ChiSquareInverse(double p, int freedom)
        {
            var minChiSquare = 0.0;
            var maxChiSquare = CHI_MAX;
            if (p <= 0.0)
            {
                return maxChiSquare;
            }

            if (p >= 1.0)
            {
                return 0.0;
            }

            var chiSquareValue = freedom / System.Math.Sqrt(p);
            while (maxChiSquare - minChiSquare > CHI_EPSILON)
            {
                if (ChiSquare(chiSquareValue, freedom) < p)
                {
                    maxChiSquare = chiSquareValue;
                }
                else
                {
                    minChiSquare = chiSquareValue;
                }

                chiSquareValue = (maxChiSquare + minChiSquare) * 0.5;
            }

            return chiSquareValue;
        }

        /**
        * <summary>The fDistribution method is used to observe whether two samples have the same variance. It takes a double input F
        * and two integer freedom1 and freedom2 for degrees of freedom as inputs. It returns the F-Distribution result.</summary>
        *
        * <param name="fValue">       double input.</param>
        * <param name="freedom1">integer input for degrees of freedom.</param>
        * <param name="freedom2">integer input for degrees of freedom.</param>
        * <returns>the F-Distribution result.</returns>
        */
        public static double FDistribution(double fValue, int freedom1, int freedom2)
        {
            int i, j;
            double y, d, p;
            if (fValue < F_EPSILON || freedom1 < 1 || freedom2 < 1)
            {
                return 1.0;
            }

            var a = freedom1 % 2 != 0 ? 1 : 2;

            var b = freedom2 % 2 != 0 ? 1 : 2;

            var w = fValue * freedom1 / freedom2;
            var z = 1.0 / (1.0 + w);
            if (a == 1)
            {
                if (b == 1)
                {
                    p = System.Math.Sqrt(w);
                    y = I_PI;
                    d = y * z / p;
                    p = 2.0 * y * System.Math.Atan(p);
                }
                else
                {
                    p = System.Math.Sqrt(w * z);
                    d = 0.5 * p * z / w;
                }
            }
            else
            {
                if (b == 1)
                {
                    p = System.Math.Sqrt(z);
                    d = 0.5 * z * p;
                    p = 1.0 - p;
                }
                else
                {
                    d = z * z;
                    p = w * z;
                }
            }

            y = 2.0 * w / z;
            for (j = b + 2; j <= freedom2; j += 2)
            {
                d *= (1.0 + a / (j - 2.0)) * z;
                if (a == 1)
                {
                    p = p + d * y / (j - 1.0);
                }
                else
                {
                    p = (p + w) * z;
                }
            }

            y = w * z;
            z = 2.0 / z;
            b = freedom2 - 2;
            for (i = a + 2; i <= freedom1; i += 2)
            {
                j = i + b;
                d *= y * j / (i - 2.0);
                p -= z * d / j;
            }

            if (p < 0.0)
            {
                p = 0.0;
            }
            else
            {
                if (p > 1.0)
                {
                    p = 1.0;
                }
            }

            return 1.0 - p;
        }

        /**
         * <summary>The fDistributionInverse method returns the F-Distribution Inverse of given probability value.</summary>
         *
         * <param name="p">       double probability.</param>
         * <param name="freedom1">integer input for degrees of freedom.</param>
         * <param name="freedom2">integer input for degrees of freedom.</param>
         * <returns>the F-Distribution Inverse of given probability.</returns>
         */
        public static double FDistributionInverse(double p, int freedom1, int freedom2)
        {
            var maxF = F_MAX;
            var minF = 0.0;
            if (p <= 0.0 || p >= 1.0)
            {
                return 0.0;
            }

            if (freedom1 == freedom2 && freedom1 > 2500)
            {
                return 1 + 4.0 / freedom1;
            }

            var fValue = 1.0 / p;
            while (System.Math.Abs(maxF - minF) > F_EPSILON)
            {
                if (FDistribution(fValue, freedom1, freedom2) < p)
                {
                    maxF = fValue;
                }
                else
                {
                    minF = fValue;
                }

                fValue = (maxF + minF) * 0.5;
            }

            return fValue;
        }

        /**
         * <summary>The tDistribution method is used instead of the normal distribution when there is small samples. It takes a double input T
         * and an integer freedom for degree of freedom as inputs. It returns the T-Distribution result by using F-Distribution method.</summary>
         *
         * <param name="T">      double input.</param>
         * <param name="freedom">integer input for degrees of freedom.</param>
         * <returns>the T-Distribution result.</returns>
         */
        public static double TDistribution(double T, int freedom)
        {
            if (T >= 0)
            {
                return FDistribution(T * T, 1, freedom) / 2;
            }
            else
            {
                return 1 - FDistribution(T * T, 1, freedom) / 2;
            }
        }

        /**
         * <summary>The tDistributionInverse method returns the T-Distribution Inverse of given probability value.</summary>
         *
         * <param name="p">      double probability.</param>
         * <param name="freedom">integer input for degrees of freedom.</param>
         * <returns>the T-Distribution Inverse of given probability.</returns>
         */
        public static double TDistributionInverse(double p, int freedom)
        {
            if (p < 0.5)
            {
                return System.Math.Sqrt(FDistributionInverse(p * 2, 1, freedom));
            }
            else
            {
                return -System.Math.Sqrt(FDistributionInverse((1 - p) * 2, 1, freedom));
            }
        }
    }
}