using QuantumWaves.Solutions;

namespace QuantumWaves.Tests;

public class HarmonicOscillator1DTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(-5)]
    public void State_Throws_WhenN_IsNegative(int n)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            HarmonicOscillator1D.State(n, mass: 1f, omega: 1f));

        Assert.Equal("n", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void State_Throws_WhenMass_IsNotPositive(float mass)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            HarmonicOscillator1D.State(n: 0, mass: mass, omega: 1f));

        Assert.Equal("mass", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void State_Throws_WhenOmega_IsNotPositive(float omega)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            HarmonicOscillator1D.State(n: 0, mass: 1f, omega: omega));

        Assert.Equal("omega", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void State_Throws_WhenHBar_IsNotPositive(float hBar)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            HarmonicOscillator1D.State(n: 0, mass: 1f, omega: 1f, hBar: hBar));

        Assert.Equal("hBar", exception.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    public void State_IsNormalizable(int n)
    {
        var wave = HarmonicOscillator1D.State(n: n, mass: 1f, omega: 1f, hBar: 1f);
        Assert.True(wave.TryNormalize());
    }

    [Fact]
    public void State_HasInfiniteDomain()
    {
        var wave = HarmonicOscillator1D.State(n: 0, mass: 1f, omega: 1f, hBar: 1f);
        Assert.True(wave.Domain.IsInfinite);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-5)]
    public void Energy_Throws_WhenN_IsNegative(int n)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            HarmonicOscillator1D.Energy(n, omega: 1f));

        Assert.Equal("n", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void Energy_Throws_WhenOmega_IsNotPositive(float omega)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            HarmonicOscillator1D.Energy(n: 0, omega: omega));

        Assert.Equal("omega", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void Energy_Throws_WhenHBar_IsNotPositive(float hBar)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            HarmonicOscillator1D.Energy(n: 0, omega: 1f, hBar: hBar));

        Assert.Equal("hBar", exception.ParamName);
    }

    [Fact]
    public void Energy_Works()
    {
        float energy = HarmonicOscillator1D.Energy(n: 2, omega: 2f, hBar: 3f);

        // E_n = ħω(n + 1/2)
        // = 3 * 2 * (2 + 0.5)
        // = 15
        Assert.Equal(15f, energy, 5);
    }
}