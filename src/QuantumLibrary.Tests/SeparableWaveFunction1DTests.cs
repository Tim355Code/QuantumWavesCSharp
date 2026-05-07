using System;
using CMath;
using CMath.Tests;

namespace QuantumWaves.Tests;

public class SeparableWaveFunction1DTests
{
    [Fact]
    public void Evaluate_Works()
    {
        var wave = new SeparableWaveFunction1D(new FloatRange(0f, 10f), x => x, t => 2f * t, amplitude: 3f);

        // ψ = amplitude * space * time
        // = 3 * 2 * (2 * 2)
        // = 24
        ComplexF result = wave.Evaluate(2f, 2f);
        ComplexAssert.Equal(new ComplexF(24f, 0f), result, 5);
    }

    [Fact]
    public void Evaluate_ReturnsZero_OutsideDomain()
    {
        var wave = new SeparableWaveFunction1D(new FloatRange(0f, 1f), x => 1f, t => 1f, amplitude: 1f);
        ComplexF result = wave.Evaluate(-1f, 0f);
        ComplexAssert.Equal(ComplexF.Zero, result, 5);
    }

    [Fact]
    public void TimeSolution_Works()
    {
        float energy = 2f;
        float hBar = 1f;

        Func<float, ComplexF> solution = SeparableWaveFunction1D.TimeSolution(energy, hBar);

        // exp(-iEt/ħ)
        // with E=2, t=π/4, ħ=1
        // => exp(-iπ/2) = -i
        ComplexF value = solution(MathF.PI / 4f);

        ComplexAssert.Equal(new ComplexF(0f, -1f), value, 1e-3f);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void TimeSolution_Throws_WhenHBarIsNotPositive(float hBar)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            SeparableWaveFunction1D.TimeSolution(energy: 1f, hBar));

        Assert.Equal("hBar", exception.ParamName);
    }
}
