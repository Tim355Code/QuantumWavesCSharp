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
        /// <param name="hBar">Reduced Planck constant (must be greater than zero).</param>
        /// <returns>A separable wave function representing the plane wave.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="mass"/> or <paramref name="hBar"/>
        /// is less than or equal to zero.
        /// </exception>
        /// <remarks>
        /// Plane waves are not square normalizable over an infinite domain.
        /// </remarks>
        public static SeparableWaveFunction1D PlaneWave(float k, float mass, float hBar)
        {
            if (mass <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(mass), "must be greater than zero.");
            if (hBar <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(hBar), "must be greater than zero.");

            float amplitude = 1 / MathF.Sqrt(2f * MathF.PI);

            return new SeparableWaveFunction1D(FloatRange.Infinite, x => MathC.Exp(ComplexF.I * k * x),
                SeparableWaveFunction1D.TimeSolution(Energy(k, mass, hBar), hBar), amplitude);
        }

        /// <summary>
        /// Computes the energy of a free particle given its wave number.
        /// </summary>
        /// <param name="k">Wave number.</param>
        /// <param name="mass">Particle mass (must be greater than zero).</param>
        /// <param name="hBar">Reduced Planck constant (must be greater than zero).</param>
        /// <returns>The particle energy.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="mass"/> or <paramref name="hBar"/>
        /// is less than or equal to zero.
        /// </exception>
        public static float Energy(float k, float mass, float hBar)
        {
            if (mass <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(mass), "must be greater than zero.");
            if (hBar <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(hBar), "must be greater than zero.");

            float a = hBar * k;
            return a * a / (2f * mass);
        }

        /// <summary>
        /// Creates an approximate Gaussian wave packet as a weighted sum of plane waves.
        /// </summary>
        /// <param name="x0">
        /// Initial center position of the packet.
        /// </param>
        /// <param name="sigma">
        /// Spatial width parameter of the Gaussian packet. Must be greater than zero.
        /// </param>
        /// <param name="k0">
        /// Central wave number of the packet.
        /// </param>
        /// <param name="mass">
        /// Particle mass. Must be greater than zero.
        /// </param>
        /// <param name="hBar">
        /// Reduced Planck constant. Must be greater than zero.
        /// </param>
        /// <param name="termCount">
        /// Number of plane wave terms used in the approximation.
        /// Must be positive and odd.
        /// </param>
        /// <param name="deltaK">
        /// Spacing between sampled wave numbers in momentum space.
        /// Must be greater than zero.
        /// </param>
        /// <returns>
        /// A weighted superposition of plane waves approximating a Gaussian wave packet.
        /// </returns>
        /// <remarks>
        /// The packet is constructed by discretizing the Fourier integral representation
        /// of a Gaussian momentum distribution.
        ///
        /// Larger <paramref name="termCount"/> and smaller <paramref name="deltaK"/>
        /// improve the approximation quality at the cost of additional computation.
        ///
        /// The resulting packet is approximately normalized when the sampled momentum
        /// range is sufficiently large.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="sigma"/>, <paramref name="mass"/>,
        /// <paramref name="hBar"/>, <paramref name="termCount"/>,
        /// or <paramref name="deltaK"/> is invalid.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="termCount"/> is even.
        /// </exception>
        public static WeightedWaveFunction1D GaussianWavePacket(float x0, float sigma, float k0, float mass, float hBar,
            int termCount = 500, float deltaK = 0.01f)
        {
            if (sigma <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(sigma), "must be greater than zero.");
            if (mass <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(mass), "must be greater than zero.");
            if (hBar <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(hBar), "must be greater than zero.");
            if (termCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(termCount), "must be greater than zero.");
            if (termCount % 2 == 0)
                throw new ArgumentException("must be odd.", nameof(termCount));
            if (deltaK <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(deltaK), "must be greater than zero.");

            ComplexF[] coefficients = new ComplexF[termCount];
            WaveFunction1D[] waves = new WaveFunction1D[termCount];

            int half = termCount / 2;

            float normalization = MathF.Pow(2f * sigma * sigma / MathF.PI, 0.25f);

            for (int i = 0; i < termCount; i++)
            {
                float dk = (i - half) * deltaK;
                float k = k0 + dk;

                coefficients[i] = normalization * MathF.Exp(-sigma * sigma * dk * dk) * 
                    MathC.Exp(-ComplexF.I * dk * x0) * deltaK;

                waves[i] = PlaneWave(k, mass, hBar);
            }

            return new WeightedWaveFunction1D(coefficients, waves);
        }
    }
}
