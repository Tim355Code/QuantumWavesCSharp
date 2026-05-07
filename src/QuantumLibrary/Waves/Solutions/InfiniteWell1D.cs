using System;
using CMath;

namespace QuantumWaves.Solutions
{
    /// <summary>
    /// Provides solutions for a particle in a one dimensional infinite potential well.
    /// </summary>
    public static class InfiniteWell1D
    {
        /// <summary>
        /// Creates the nth energy eigenstate.
        /// </summary>
        /// <param name="n">Quantum number (must be greater than zero).</param>
        /// <param name="length">Well length (must be greater than zero).</param>
        /// <param name="mass">Particle mass (must be greater than zero).</param>
        /// <param name="h_bar">Reduced Planck constant (must be greater than zero).</param>
        /// <returns>A separable wave function representing the state.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if any parameter is invalid.</exception>
        public static SeparableWaveFunction1D State(int n, float length, float mass, float h_bar = 1)
        {
            if (length <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(length), "must be greater than zero.");
            if (n <= 0)
                throw new ArgumentOutOfRangeException(nameof(n), "must be greater than zero.");
            if (mass <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(mass), "must be greater than zero.");
            if (h_bar <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(h_bar), "must be greater than zero.");

            float normalizationConstant = MathF.Sqrt(2f / length);
            float sinTerm = n * MathF.PI / length;
            FloatRange domain = new FloatRange(0, length);
            
            return new SeparableWaveFunction1D(domain, (x) => MathF.Sin(x * sinTerm),
                SeparableWaveFunction1D.TimeSolution(Energy(n, length, mass, h_bar), h_bar), normalizationConstant);
        }

        /// <summary>
        /// Computes the energy of the nth state in the well.
        /// </summary>
        /// <param name="n">Quantum number (must be greater than zero).</param>
        /// <param name="length">Well length (must be greater than zero).</param>
        /// <param name="mass">Particle mass (must be greater than zero).</param>
        /// <param name="h_bar">Reduced Planck constant (must be greater than zero).</param>
        /// <returns>The energy value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if any parameter is invalid.</exception>
        public static float Energy(int n, float length, float mass, float h_bar = 1)
        {
            if (length <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(length), "must be greater than zero.");
            if (n <= 0)
                throw new ArgumentOutOfRangeException(nameof(n), "must be greater than zero.");
            if (mass <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(mass), "must be greater than zero.");
            if (h_bar <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(h_bar), "must be greater than zero.");
                
            float a =  n * MathF.PI * h_bar / length;
            return a * a / (2f * mass);
        }
    }
}
