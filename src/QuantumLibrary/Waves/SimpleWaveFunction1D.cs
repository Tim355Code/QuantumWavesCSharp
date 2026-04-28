using System;
using CMath;

public class SimpeWaveFunction1D : WaveFunction1D
{
    private readonly Func<float, float, ComplexF> _wave;

    public SimpeWaveFunction1D(Func<float, float, ComplexF> wave, FloatRange domain, ComplexF normalizationConstant)
        : base(domain, normalizationConstant)
    {
        _wave = wave;
    }

    protected override ComplexF EvaluateRaw(float x, float t) => _wave(x, t);
}
