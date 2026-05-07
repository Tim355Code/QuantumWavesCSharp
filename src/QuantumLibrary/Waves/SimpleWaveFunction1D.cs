using System;
using CMath;

namespace QuantumWaves
{
    /// <summary>
    /// Represents a general one dimensional wave function defined by a delegate.
    /// </summary>
    public class SimpleWaveFunction1D : WaveFunction1D
    {
        private readonly Func<float, float, ComplexF> _wave;

        /// <summary>Initializes a wave function from a given function.</summary>
        /// <param name="wave">Function defining ψ(x, t).</param>
        /// <param name="domain">The spatial domain of the wave function.</param>
        /// <param name="amplitude">The amplitude of the wave function.</param>
        public SimpleWaveFunction1D(Func<float, float, ComplexF> wave, FloatRange domain, ComplexF amplitude)
            : base(domain, amplitude)
        {
            _wave = wave;
        }

        /// <summary>Evaluates the unscaled wave function at (x, t).</summary>
        /// <param name="x">The position at which to evaluate the unscaled wave function.</param>
        /// <param name="t">The time at which to evaluate the unscaled wave function.</param>
        /// <returns>The unscaled value of the wave function at <paramref name="x"/> and <paramref name="t"/>.</returns>
        protected override ComplexF EvaluateRaw(float x, float t) => _wave(x, t);
    }
}