using System;
using CMath;

public sealed class SeparableWaveFunction1D : WaveFunction1D
{
    public Func<float, ComplexF> SpacePart { get; }
    public Func<float, ComplexF> TimePart { get; }

    public SeparableWaveFunction1D(FloatRange domain, 
        Func<float, ComplexF> spacePart, Func<float, ComplexF> timePart,
        float normalizationConstant) : base(domain, normalizationConstant)
    {
        SpacePart = spacePart;
        TimePart = timePart;
    }

    protected override ComplexF EvaluateRaw(float x, float t) => NormalizationConstant * SpacePart(x) * TimePart(t);
}
