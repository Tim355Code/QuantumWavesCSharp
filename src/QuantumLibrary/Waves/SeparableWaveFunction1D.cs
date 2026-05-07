using System;
using CMath;

namespace QuantumWaves
{
    /// <summary>
    /// Represents a separable one dimensional wave function.
    /// </summary>
    public sealed class SeparableWaveFunction1D : WaveFunction1D
    {
        /// <summary>Gets the spatial part of the wave function.</summary>
        public Func<float, ComplexF> SpacePart { get; }

        /// <summary>Gets the time dependent part of the wave function.</summary>
        public Func<float, ComplexF> TimePart { get; }

        /// <summary>Initializes a separable wave function with specified space and time components.</summary>
        /// <param name="domain">The spatial domain of the wave function.</param>
        /// <param name="spacePart">The spatial component of the wave function.</param>
        /// <param name="timePart">The time dependent component of the wave function.</param>
        /// <param name="amplitude">The amplitude of the wave function.</param>
        /// /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="spacePart"/> or <paramref name="timePart"/> is <see langword="null"/>.
        /// </exception>
        public SeparableWaveFunction1D(FloatRange domain,
            Func<float, ComplexF> spacePart, Func<float, ComplexF> timePart,
            float amplitude) : base(domain, amplitude)
        {
            SpacePart = spacePart ?? throw new ArgumentNullException(nameof(SpacePart));
            TimePart = timePart ?? throw new ArgumentNullException(nameof(TimePart));
        }

        /// <summary>
        /// Initializes a separable wave function with specified space and time components,
        /// and automatically normalizes its amplitude.
        /// </summary>
        /// <param name="domain">The spatial domain of the wave function.</param>
        /// <param name="spacePart">The spatial component of the wave function.</param>
        /// <param name="timePart">The time dependent component of the wave function.</param>
        /// /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="spacePart"/> or <paramref name="timePart"/> is <see langword="null"/>.
        /// </exception>
        public SeparableWaveFunction1D(FloatRange domain, Func<float, ComplexF> spacePart,
            Func<float, ComplexF> timePart) : this(domain, spacePart, timePart, 1f)
        {
            TryNormalize();
        }

        /// <summary>Creates the generic time dependent solution for a given energy.</summary>
        /// <param name="energy">Energy value.</param>
        /// <param name="hBar">Reduced Planck constant. Must be greater than zero.</param>
        /// <returns>A function representing the time evolution.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="hBar"/> is less than or equal to zero.
        /// </exception>
        public static Func<float, ComplexF> TimeSolution(float energy, float hBar = 1)
        {
            if (hBar <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(hBar), "must be greater than zero.");

            return (t) => MathC.Exp(-ComplexF.I * energy * t / hBar);
        }
        
        /// <summary>
        /// Evaluates the unscaled wave function at (<paramref name="x"/>, <paramref name="t"/>) using the space and time components.
        /// </summary>
        /// <param name="x">The position at which to evaluate the spatial component.</param>
        /// <param name="t">The time at which to evaluate the time dependent component.</param>
        /// <returns>The unscaled value of the wave function at <paramref name="x"/> and <paramref name="t"/>.</returns>
        protected override ComplexF EvaluateRaw(float x, float t) => SpacePart(x) * TimePart(t);
    }
}