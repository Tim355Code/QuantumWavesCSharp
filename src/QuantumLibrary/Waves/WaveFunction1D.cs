using System;
using CMath;
using QuantumWaves.Utils;

namespace QuantumWaves
{
    /// <summary>
    /// Base class for one dimensional wave functions.
    /// </summary>
    public abstract class WaveFunction1D
    {
        /// <summary>
        /// Gets or sets the amplitude of the wave function.
        /// </summary>
        public ComplexF Amplitude { get; set; }

        /// <summary>
        /// Gets the spatial domain of the wave function.
        /// </summary>
        public FloatRange Domain { get; protected set; }

        /// <summary>
        /// Initializes a new wave function with a domain and amplitude.
        /// </summary>
        protected WaveFunction1D(FloatRange domain, ComplexF amplitude)
        {
            Domain = domain;
            Amplitude = amplitude;
        }

        /// <summary>
        /// Evaluates the wave function ψ(x, t).
        /// </summary>
        public ComplexF Evaluate(float x, float t) => Domain.Contains(x) ?
            (Amplitude * EvaluateRaw(x, t)) : 0;

        /// <summary>
        /// Evaluates the unscaled wave function at (x, t).
        /// </summary>
        protected abstract ComplexF EvaluateRaw(float x, float t);

        /// <summary>
        /// Returns |ψ(x,t)|².
        /// </summary>
        public float ProbabilityDensity(float x, float t) => MathC.AbsSqr(Evaluate(x, t));

        /// <summary>
        /// Approximates the probability over a given range.
        /// </summary>
        public float ProbabilityInRange(float t, FloatRange domain, int intervals = 100000)
        {
            return MathEx.Simpson(x => ProbabilityDensity(x, t), domain, intervals);
        }

        /// <summary>
        /// Integrates |ψ(x,t)|² over a range without applying amplitude.
        /// </summary>
        protected float IntegrateRaw(float t, FloatRange domain, int intervals = 100000)
        {
            return MathEx.Simpson(
                x => Domain.Contains(x) ? MathC.AbsSqr(EvaluateRaw(x, t)) : 0f,
                domain,
                intervals
            );
        }

        /// <summary>Attempts to find a finite domain useful for integration.</summary>
        /// <param name="t">Time at which the wave function is evaluated.</param>
        /// <param name="domain">The found finite domain if successful.</param>
        /// <param name="tolerance">Relative convergence tolerance for infinite domain expansion.</param>
        /// <param name="maxAllowed">Maximum allowed integral value.</param>
        /// <param name="maxCount"> Maximum number of expansion iterations.</param>
        /// <returns>True if a finite domain was found, otherwise false.</returns>
        private bool TryGetSamplingDomain(float t, out FloatRange domain, float tolerance, float maxAllowed, int maxCount)
        {
            if (!Domain.IsInfinite)
            {
                domain = Domain;
                return true;
            }

            float L = 1f;
            float prev = 0f;

            for (int i = 0; i < maxCount; i++)
            {
                float current = IntegrateRaw(t, new FloatRange(-L, L));

                if (i > 0 && MathF.Abs(current - prev) < tolerance * MathF.Max(1f, MathF.Abs(current)))
                {
                    if (current <= float.Epsilon)
                    {
                        domain = default;
                        return false;
                    }

                    domain = new FloatRange(-L, L);
                    return true;
                }

                if (current > maxAllowed)
                {
                    domain = default;
                    return false;
                }

                prev = current;
                L *= 2f;
            }

            domain = default;
            return false;
        }

        /// <summary>
        /// Generates random position samples of the wave on a finite domain.
        /// </summary>
        /// <param name="count">The number of samples to generate.</param>
        /// <param name="samples">Contains the samples if successful, otherwise an empty array.</param>
        /// <param name="t">Time at which normalization is evaluated.</param>
        /// <param name="domain">The domain to sample.</param>
        /// <param name="pointCount">The number of discrete points used to approximate the cumulative distribution.</param>
        /// <param name="rng">The random number generator used for sampling.</param>
        /// <returns>True if sampling succeeded, otherwise false.</returns>
        private bool SampleFromFiniteDomain(int count, out float[] samples, float t, FloatRange domain,
            int pointCount, Random rng)
        {
            samples = new float[0];

            float[] xs = new float[pointCount];
            float[] cdf = new float[pointCount];

            float amplitudeSqr = MathC.AbsSqr(Amplitude);

            for (int i = 0; i < pointCount; i++)
            {
                float x = domain.Min + domain.Length * (i + 1f) / pointCount;

                xs[i] = x;
                cdf[i] = amplitudeSqr * IntegrateRaw(t, new FloatRange(domain.Min, x));
            }

            float total = cdf[pointCount - 1];

            if (total <= float.Epsilon)
                return false;

            // Normalize CDF
            for (int i = 0; i < pointCount; i++)
                cdf[i] /= total;

            cdf[pointCount - 1] = 1f;
            samples = new float[count];

            for (int k = 0; k < count; k++)
            {
                float u = (float)rng.NextDouble();

                int index = cdf.LowerBound(u);
                samples[k] = xs[index];
            }

            return true;
        }

        /// <summary>
        /// Attempts to normalize the wave function.
        /// </summary>
        /// <param name="t">Time at which normalization is evaluated.</param>
        /// <param name="tolerance">Relative convergence tolerance for infinite domain expansion.</param>
        /// <param name="max_allowed">Maximum allowed integral value.</param>
        /// <param name="max_count"> Maximum number of expansion iterations.</param>
        /// <returns>True if normalization was successfull, otherwise false.</returns>
        public bool TryNormalize(float t = 0f, float tolerance = 1e-4f, float max_allowed = 1e6f, int max_count = 10000)
        {
            if (!TryGetSamplingDomain(t, out FloatRange integrationDomain, tolerance, max_allowed, max_count))
                return false;

            float current = IntegrateRaw(t, integrationDomain);

            if (current <= float.Epsilon || current > max_allowed || float.IsNaN(current) || float.IsInfinity(current))
                return false;

            Amplitude = 1f / MathF.Sqrt(current);
            return true;
        }

        /// <summary>
        /// Generates random position samples of the wave.
        /// </summary>
        /// <param name="sampleCount">The number of samples to generate. Must be greater than zero.</param>
        /// <param name="samples">Contains the samples if successful, otherwise an empty array.</param>
        /// <param name="t">The time at which the wave function is sampled.</param>
        /// <param name="pointCount">
        /// The number of discrete points used to approximate the cumulative distribution.
        /// Must be greater than one.
        /// </param>
        /// <param name="rng">The random number generator used for sampling.</param>
        /// <param name="tolerance">
        /// Relative convergence tolerance for infinite domain expansion. Must be greater than zero.
        /// </param>
        /// <param name="maxAllowed">Maximum allowed integral value. Must be greater than zero.</param>
        /// <param name="maxCount">Maximum number of expansion iterations. Must be greater than zero.</param>
        /// <returns>True if sampling succeeded, otherwise false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="sampleCount"/> is less than or equal to zero,
        /// <paramref name="pointCount"/> is less than or equal to one,
        /// <paramref name="tolerance"/> is less than or equal to zero,
        /// <paramref name="maxAllowed"/> is less than or equal to zero,
        /// or <paramref name="maxCount"/> is less than or equal to zero.
        /// </exception>
        public bool Sample(int sampleCount, out float[] samples, float t = 0f, int pointCount = 1000, 
            Random? rng = null, float tolerance = 1e-4f, float maxAllowed = 1e6f, int maxCount = 10000)
        {
            samples = Array.Empty<float>();

            if (sampleCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleCount), "must be greater than zero.");

            if (pointCount <= 1)
                throw new ArgumentOutOfRangeException(nameof(pointCount), "must be greater than one.");

            if (tolerance <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(tolerance), "must be greater than zero.");

            if (maxAllowed <= float.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(maxAllowed), "must be greater than zero.");

            if (maxCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxCount), "must be greater than zero.");

            rng ??= new Random();

            if (!TryGetSamplingDomain(t, out FloatRange samplingDomain, tolerance, maxAllowed, maxCount))
                return false;

            return SampleFromFiniteDomain(sampleCount, out samples, t, samplingDomain, pointCount, rng);
        }
    }
}