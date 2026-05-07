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

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void GaussianWavePacket_Throws_WhenSigma_IsNotPositive(float sigma)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            FreeParticle1D.GaussianWavePacket(x0: 0f, sigma: sigma, k0: 1f, mass: 1f));
        Assert.Equal("sigma", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void GaussianWavePacket_Throws_WhenMass_IsNotPositive(float mass)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            FreeParticle1D.GaussianWavePacket(x0: 0f, sigma: 1f, k0: 1f, mass: mass));
        Assert.Equal("mass", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void GaussianWavePacket_Throws_WhenHBar_IsNotPositive(float hBar)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            FreeParticle1D.GaussianWavePacket(x0: 0f, sigma: 1f, k0: 1f, mass: 1f, hBar: hBar));
        Assert.Equal("hBar", exception.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void GaussianWavePacket_Throws_WhenTermCount_IsNotPositive(int termCount)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            FreeParticle1D.GaussianWavePacket( x0: 0f, sigma: 1f, k0: 1f, mass: 1f, termCount: termCount));
        Assert.Equal("termCount", exception.ParamName);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(10)]
    public void GaussianWavePacket_Throws_WhenTermCount_IsEven(int termCount)
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            FreeParticle1D.GaussianWavePacket( x0: 0f, sigma: 1f, k0: 1f, mass: 1f, termCount: termCount));
        Assert.Equal("termCount", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void GaussianWavePacket_Throws_WhenDeltaK_IsNotPositive(float deltaK)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            FreeParticle1D.GaussianWavePacket(x0: 0f, sigma: 1f, k0: 1f, mass: 1f, deltaK: deltaK));
        Assert.Equal("deltaK", exception.ParamName);
    }

    [Fact]
    public void GaussianWavePacket_HasInfiniteDomain()
    {
        var wave = FreeParticle1D.GaussianWavePacket( x0: 0f, sigma: 1f, k0: 1f, mass: 1f);
        Assert.True(wave.Domain.IsInfinite);
    }

    [Fact]
    public void GaussianWavePacket_ProbabilityDensity_AtCenter_MatchesExpectedValue()
    {
        float sigma = 1f / MathF.Sqrt(2f * MathF.PI);
        var wave = FreeParticle1D.GaussianWavePacket(x0: 0f, sigma: sigma, k0: 1f, mass: 1f, termCount: 1999);
        float density = wave.ProbabilityDensity(0f, 0f);

        Assert.Equal(1f, density, 1e-2f);
    }

    [Fact]
    public void GaussianWavePacket_ProbabilityDensity_OneUnitFromCenter_MatchesExpMinusPi()
    {
        float sigma = 1f / MathF.Sqrt(2f * MathF.PI);
        var wave = FreeParticle1D.GaussianWavePacket(x0: 0f, sigma: sigma, k0: 1f, mass: 1f, termCount: 1999);
        float density = wave.ProbabilityDensity(1f, 0f);

        Assert.Equal(MathF.Exp(-MathF.PI), density, 1e-2f);
    }

    [Fact]
    public void GaussianWavePacket_Sample_ReturnsValuesNearCenter()
    {
        float sigma = 1f / MathF.Sqrt(2f * MathF.PI);
        var wave = FreeParticle1D.GaussianWavePacket(x0: 0f, sigma: sigma, k0: 1f, mass: 1f, termCount: 199);

        Assert.True(wave.Sample(
            sampleCount: 1000,
            out float[] samples,
            t: 0f,
            pointCount: 500,
            segmentIntervals: 8,
            rng: new Random(123)));

        float average = samples.Average();

        Assert.Equal(0f, average, 0.05f);
    }
}