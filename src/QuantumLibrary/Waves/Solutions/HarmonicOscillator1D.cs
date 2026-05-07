using System;
using QuantumWaves.Utils;

namespace QuantumWaves.Solutions
{
    /// <summary>
    /// Provides solutions for the one dimensional quantum harmonic oscillator.
    /// </summary>
    public static class HarmonicOscillator1D
    {
        /// <summary>
        /// Creates the nth energy eigenstate.
        /// </summary>
        /// <param name="n">Quantum number (must be non-negative).</param>
        /// <param name="mass">Particle mass (must be greater than zero).</param>
        /// <param name="omega">Angular frequency (must be greater than zero).</param>
        /// <param name="hBar">Reduced Planck constant (must be greater than zero).</param>
        /// <returns>A separable wave function representing the state.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="n"/> is invalid, or if <paramref name="mass"/>,
        /// <paramref name="omega"/>, or <paramref name="hBar"/> is less than or equal to zero.
        /// </exception>
        public static SeparableWaveFunction1D State(int n, float mass, float omega, float hBar = 1)
        {
            if (omega <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(omega), "must be greater than zero.");
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), "may not be negative.");
            if (mass <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(mass), "must be greater than zero.");
            if (hBar <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(hBar), "must be greater than zero.");

            float alpha = MathF.Sqrt(mass * omega / hBar);
            float expTerm = mass * omega / (2 * hBar);

            float normalizationConstant =
                MathF.Pow(mass * omega / (hBar * MathF.PI), 0.25f)
                / MathF.Sqrt(MathF.Pow(2, n) * MathEx.Factorial(n));

            Func<float, float> hermite = HermitePolynomials.Delegate(n);

            return new SeparableWaveFunction1D(
                FloatRange.Infinite,
                x => hermite(alpha * x) * MathF.Exp(-expTerm * x * x),
                SeparableWaveFunction1D.TimeSolution(Energy(n, omega, hBar), hBar),
                normalizationConstant
            );
        }

        /// <summary>
        /// Computes the energy of the nth harmonic oscillator state.
        /// </summary>
        /// <param name="n">Quantum number (must be non-negative).</param>
        /// <param name="omega">Angular frequency (must be greater than zero).</param>
        /// <param name="hBar">Reduced Planck constant (must be greater than zero).</param>
        /// <returns>The energy value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="n"/> is negative, or if
        /// <paramref name="omega"/> or <paramref name="hBar"/>
        /// is less than or equal to zero.
        /// </exception>
        public static float Energy(int n, float omega, float hBar = 1)
        {
            if (omega <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(omega), "must be greater than zero.");
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), "may not be negative.");
            if (hBar <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(hBar), "must be greater than zero.");

            return hBar * omega * (0.5f + n);
        }
    }
}