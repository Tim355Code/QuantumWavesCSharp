using System;
using CMath;

namespace QuantumWaves
{
    /// <summary>
    /// Represents a separable one dimensional wave function.
    /// </summary>
    public sealed class SeparableWaveFunction1D : WaveFunction1D
    {
        /// <summary>
        /// Gets the spatial part of the wave function.
        /// </summary>
        public readonly Func<float, ComplexF> SpacePart;

        /// <summary>
        /// Gets the time dependent part of the wave function.
        /// </summary>
        public readonly Func<float, ComplexF> TimePart;

        /// <summary>
        /// Initializes a separable wave function with specified space and time components.
        /// </summary>
        public SeparableWaveFunction1D(FloatRange domain, 
            Func<float, ComplexF> spacePart, Func<float, ComplexF> timePart,
            float amplitude) : base(domain, amplitude)
        {
            SpacePart = spacePart;
            TimePart = timePart;
        }

        /// <summary>
        /// Creates the generic time dependent solution for a given energy.
        /// </summary>
        /// <param name="energy">Energy value.</param>
        /// <param name="h_bar">Reduced Planck constant (must be greater than zero).</param>
        /// <returns>A function representing the time evolution.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if h_bar is not positive.</exception>
        public static Func<float, ComplexF> TimeSolution(float energy, float h_bar = 1)
        {
            if (h_bar <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(h_bar), "must be greater than zero.");

            return (t) => MathC.Exp(-ComplexF.I * energy * t / h_bar);
        }
        
        /// <summary>
        /// Evaluates the unscaled wave function at (x, t) using the space and time components.
        /// </summary>
        protected override ComplexF EvaluateRaw(float x, float t) =>  SpacePart(x) * TimePart(t);
    }
}