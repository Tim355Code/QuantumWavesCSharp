using System;
using System.Collections.Generic;
using CMath;

namespace QuantumWaves
{
    /// <summary>
    /// Represents a weighted combination of one dimensional wave functions.
    /// </summary>
    public sealed class WeightedWaveFunction1D : WaveFunction1D
    {
        private readonly List<ComplexF> coefficients = new List<ComplexF>();
        private readonly List<WaveFunction1D> waveFunctions = new List<WaveFunction1D>();

        /// <summary>
        /// Gets the coefficients for each wave function.
        /// </summary>
        public IReadOnlyList<ComplexF> Coefficients => coefficients;

        /// <summary>
        /// Gets the wave functions in the weighted sum.
        /// </summary>
        public IReadOnlyList<WaveFunction1D> WaveFunctions => waveFunctions;

        /// <summary>
        /// Initializes a weighted wave function from coefficients and wave functions.
        /// </summary>
        /// <param name="startCoefficients">Initial coefficients.</param>
        /// <param name="startWaveFunctions">Initial wave functions.</param>
        /// <exception cref="ArgumentNullException">Thrown if an input array or wave function is null.</exception>
        /// <exception cref="ArgumentException">Thrown if no wave functions are provided or array sizes do not match.</exception>
        public WeightedWaveFunction1D(ComplexF[] startCoefficients, WaveFunction1D[] startWaveFunctions) 
            : base(FloatRange.Infinite, ComplexF.One)
        {
            if (startCoefficients == null) throw new ArgumentNullException(nameof(startCoefficients));
            if (startWaveFunctions == null) throw new ArgumentNullException(nameof(startWaveFunctions));
            if (startWaveFunctions.Length == 0) throw new ArgumentException("At least one wavefunction is required.");

            if (startCoefficients.Length != startWaveFunctions.Length)
                throw new ArgumentException(nameof(startCoefficients) + " and " 
                    + nameof(startWaveFunctions) + " do not match in size.");

            for(int i = 0; i < startCoefficients.Length; i++)
            {
                if (startWaveFunctions[i] == null) throw new ArgumentNullException($"Wave function index = {i} is null.");

                waveFunctions.Add(startWaveFunctions[i]);
                coefficients.Add(startCoefficients[i]);
            }
        }

        /// <summary>
        /// Adds a wave function with its coefficient.
        /// </summary>
        /// <param name="coefficient">Coefficient for the wave function.</param>
        /// <param name="waveFunction">Wave function to add.</param>
        /// <exception cref="ArgumentNullException">Thrown if waveFunction is null.</exception>
        public void Add(ComplexF coefficient, WaveFunction1D waveFunction)
        {
            if (waveFunction == null) throw new ArgumentNullException("Wave function may not be null.");

            waveFunctions.Add(waveFunction);
            coefficients.Add(coefficient);
        }

        /// <summary>
        /// Evaluates the weighted sum at (x, t).
        /// </summary>
        protected override ComplexF EvaluateRaw(float x, float t)
        {
            ComplexF total = ComplexF.Zero;
            for(int i = 0; i < coefficients.Count; i++)
            {
                total += waveFunctions[i].Evaluate(x, t) * coefficients[i];
            }
            return total;
        }
    }
}
