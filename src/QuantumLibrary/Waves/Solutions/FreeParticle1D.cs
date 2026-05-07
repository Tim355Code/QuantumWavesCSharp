using System;
using CMath;

namespace QuantumWaves.Solutions
{
    /// <summary>
    /// Provides solutions for a free particle in one dimension.
    /// </summary>
    public static class FreeParticle1D
    {
        /// <summary>
        /// Creates a plane wave solution for a free particle.
        /// </summary>
        /// <param name="k">Wave number.</param>
        /// <param name="mass">Particle mass (must be greater than zero).</param>
        /// <param name="h_bar">Reduced Planck constant (must be greater than zero).</param>
        /// <returns>A separable wave function representing the plane wave.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if mass or h_bar is not positive.</exception>
        public static SeparableWaveFunction1D PlaneWave(float k, float mass, float h_bar)
        {
            if (mass <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(mass), "must be greater than zero.");
            if (h_bar <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(h_bar), "must be greater than zero.");

            float amplitude = 1 / MathF.Sqrt(2f * MathF.PI);

            return new SeparableWaveFunction1D(FloatRange.Infinite, x => MathC.Exp(ComplexF.I * k * x),
                SeparableWaveFunction1D.TimeSolution(Energy(k, mass, h_bar), h_bar), amplitude);
        }

        /// <summary>
        /// Computes the energy of a free particle given its wave number.
        /// </summary>
        /// <param name="k">Wave number.</param>
        /// <param name="mass">Particle mass (must be greater than zero).</param>
        /// <param name="h_bar">Reduced Planck constant (must be greater than zero).</param>
        /// <returns>The particle energy.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if mass or h_bar is not positive.</exception>
        public static float Energy(float k, float mass, float h_bar)
        {
            if (mass <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(mass), "must be greater than zero.");
            if (h_bar <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(h_bar), "must be greater than zero.");

            float a = h_bar * k;
            return a * a / (2f * mass);
        }
    }
}
