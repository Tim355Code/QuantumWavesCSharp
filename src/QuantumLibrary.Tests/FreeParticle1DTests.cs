using QuantumWaves.Solutions;

namespace QuantumWaves.Tests;

public class FreeParticle1DTests
{
    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void PlaneWave_Throws_WhenMass_IsNotPositive(float mass)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            FreeParticle1D.PlaneWave(k: 1f, mass: mass, hBar: 1f));
        Assert.Equal("mass", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void PlaneWave_Throws_WhenHBar_IsNotPositive(float hBar)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            FreeParticle1D.PlaneWave(k: 1f, mass: 1f, hBar: hBar));
        Assert.Equal("hBar", exception.ParamName);
    }

    [Fact]
    public void PlaneWave_HasInfiniteDomain()
    {
        var wave = FreeParticle1D.PlaneWave(k: 1f, mass: 1f, hBar: 1f);
        Assert.True(wave.Domain.IsInfinite);
    }

    [Fact]
    public void PlaneWave_IsNotNormalizable()
    {
        var wave = FreeParticle1D.PlaneWave(k: 1f, mass: 1f, hBar: 1f);
        Assert.False(wave.TryNormalize());
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void Energy_Throws_WhenMass_IsNotPositive(float mass)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            FreeParticle1D.Energy(k: 1f, mass: mass, hBar: 1f));
        Assert.Equal("mass", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void Energy_Throws_WhenHBar_IsNotPositive(float hBar)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            FreeParticle1D.Energy(k: 1f, mass: 1f, hBar: hBar));
        Assert.Equal("hBar", exception.ParamName);
    }

    [Fact]
    public void Energy_Works()
    {
        float energy = FreeParticle1D.Energy(k: 2f, mass: 2f, hBar: 3f);

        // E = (ħk)^2 / (2m)
        // = (3 * 2)^2 / (2 * 2)
        // = 36 / 4
        // = 9
        Assert.Equal(9f, energy, 5);
    }
}