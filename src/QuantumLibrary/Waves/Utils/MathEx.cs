using System;
using CMath;

namespace QuantumWaves.Utils
{
    /// <summary>
    /// Provides common mathematical utility functions.
    /// </summary>
    public static class MathEx
    {
        /// <summary>
        /// Computes the factorial of a non-negative integer.
        /// </summary>
        /// <param name="n">The input value (must be non-negative).</param>
        /// <returns>The factorial of n.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if n is negative.</exception>
        public static long Factorial(int n)
        {
            if (n < 0) throw new ArgumentOutOfRangeException("n must be non-negative");

            long result = 1;
            for (int i = 2; i <= n; i++)
                result *= i;

            return result;
        }

        /// <summary>
        /// Approximates the definite integral of a function using Simpson's rule.
        /// </summary>
        /// <param name="f">The function to integrate.</param>
        /// <param name="domain">The integration range.</param>
        /// <param name="intervals">The number of intervals.</param>
        /// <returns>The approximate integral value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if f is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if intervals is not positive.</exception>
        /// <exception cref="ArgumentException">Thrown if intervals is not even or domain is invalid.</exception>
        public static float Simpson(Func<float, float> f, FloatRange domain, int intervals)
        {
            if (f == null)
                throw new ArgumentNullException(nameof(f));
            if (intervals <= 0)
                throw new ArgumentOutOfRangeException(nameof(intervals), "must be positive.");
            if (intervals % 2 != 0)
                throw new ArgumentException("Simpson's rule requires an even number of intervals.", nameof(intervals));
            if (domain.IsInfinite)
                throw new ArgumentException("Integration range must be finite.");
            if (domain.Length <= float.Epsilon)
                return 0f;

            float h = domain.Length / intervals;
            float sum = f(domain.Min) + f(domain.Max);

            for (int i = 1; i < intervals; i++)
            {
                float x = domain.Min + i * h;
                sum += (i % 2 == 0 ? 2f : 4f) * f(x);
            }

            return sum * h / 3f;
        }

        /// <summary>
        /// Approximates the definite integral of a complex-valued function using Simpson's rule.
        /// </summary>
        /// <param name="f">The function to integrate.</param>
        /// <param name="domain">The integration range.</param>
        /// <param name="intervals">The number of intervals.</param>
        /// <returns>The approximate complex integral value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if f is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if intervals is not positive.</exception>
        /// <exception cref="ArgumentException">Thrown if intervals is not even or domain is invalid.</exception>
        public static ComplexF SimpsonComplex(Func<float, ComplexF> f, FloatRange domain, int intervals)
        {
            if (f == null)
                throw new ArgumentNullException(nameof(f));
            if (intervals <= 0)
                throw new ArgumentOutOfRangeException(nameof(intervals), "must be positive.");
            if (intervals % 2 != 0)
                throw new ArgumentException("Simpson's rule requires an even number of intervals.", nameof(intervals));
            if (domain.IsInfinite)
                throw new ArgumentException("Integration range must be finite.");
            if (domain.Length <= float.Epsilon)
                return 0f;

            float h = domain.Length / intervals;
            ComplexF sum = f(domain.Min) + f(domain.Max);

            for (int i = 1; i < intervals; i++)
            {
                float x = domain.Min + i * h;
                sum += (i % 2 == 0 ? 2f : 4f) * f(x);
            }

            return sum * h / 3f;
        }
    }
}