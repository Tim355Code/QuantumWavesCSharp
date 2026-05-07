using System;
using CMath;

namespace QuantumWaves
{
    /// <summary>
    /// Represents a general one dimensional wave function defined by a delegate.
    /// </summary>
    public class SimpeWaveFunction1D : WaveFunction1D
    {
        private readonly Func<float, float, ComplexF> _wave;

        /// <summary>
        /// Initializes a wave function from a given function.
        /// </summary>
        /// <param name="wave">Function defining ψ(x, t).</param>
        /// <param name="domain">Spatial domain of the wave function.</param>
        /// <param name="normalizationConstant">Initial amplitude (normalization constant).</param>
        public SimpeWaveFunction1D(Func<float, float, ComplexF> wave, FloatRange domain, ComplexF normalizationConstant)
            : base(domain, normalizationConstant)
        {
            _wave = wave;
        }

        /// <summary>
        /// Evaluates the unscaled wave function at (x, t).
        /// </summary>
        protected override ComplexF EvaluateRaw(float x, float t) => _wave(x, t);
    }
}
