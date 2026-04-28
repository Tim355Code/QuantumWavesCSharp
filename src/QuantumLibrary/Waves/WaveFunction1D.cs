using CMath;

public abstract class WaveFunction1D
{
    public ComplexF NormalizationConstant { get; set; }
    public FloatRange Domain { get; protected set; }

    public WaveFunction1D(FloatRange domain, ComplexF normalizationConstant)
    {
        Domain = domain;
        NormalizationConstant = normalizationConstant;
    }

    /// <summary>
    /// Evaluates the wave function ψ(x, t)
    /// </summary>
    public ComplexF Evaluate(float x, float t) => Domain.Contains(x) ?
        (NormalizationConstant * EvaluateRaw(x, t)) : 0;

    protected abstract ComplexF EvaluateRaw(float x, float t);

    /// <summary>
    /// Returns |ψ(x,t)|^2
    /// </summary>
    public float ProbabilityDensity(float x, float t) => MathC.AbsSqr(Evaluate(x, t));
}
