using System.Collections.Generic;

namespace Math
{
    public class Eigenvector : Vector
    {
        private readonly double _eigenValue;

        /**
         * <summary>A constructor of {@link Eigenvector} which takes a double eigenValue and an {@link ArrayList} values as inputs.
         * It calls its super class {@link Vector} with values {@link ArrayList} and initializes eigenValue variable with its
         * eigenValue input.</summary>
         *
         * <param name="eigenValue">double input.</param>
         * <param name="values">    {@link ArrayList} input.</param>
         */
        public Eigenvector(double eigenValue, List<double> values) : base(values)
        {
            this._eigenValue = eigenValue;
        }

        /**
         * <summary>The eigenValue method which returns the eigenValue variable.</summary>
         *
         * <returns>eigenValue variable.</returns>
         */
        public double GetEigenValue()
        {
            return _eigenValue;
        }
    }
}