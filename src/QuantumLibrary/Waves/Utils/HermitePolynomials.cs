using System;

namespace QuantumWaves.Utils
{
    /// <summary>
    /// Provides methods for generating Hermite polynomials.
    /// </summary>
    public static class HermitePolynomials
    {
        /// <summary>
        /// Returns a delegate representing the nth Hermite polynomial.
        /// </summary>
        /// <param name="n">The order of the polynomial (must be non-negative).</param>
        /// <returns>A function that evaluates the nth Hermite polynomial at a given x.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if n is negative.</exception>
        public static Func<float, float> Delegate(int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), "may not be negative.");

            Func<float, float>[] states = new Func<float, float>[Math.Max(2, n + 1)];
            states[0] = x => 1;
            states[1] = x => 2 * x;

            for (int i = 2; i <= n; i++)
            {
                int k = i;
                var prev = states[k - 1];
                var prevPrev = states[k - 2];

                states[k] = x => 2 * x * prev(x) - 2 * (k - 1) * prevPrev(x);
            }

            return states[n];
        }
    }
}