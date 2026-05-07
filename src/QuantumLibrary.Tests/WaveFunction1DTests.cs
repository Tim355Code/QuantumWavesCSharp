using CMath;
using CMath.Tests;

namespace QuantumWaves.Tests;

public class WaveFunction1DTests
{
    [Fact]
    public void Evaluate_Works_WhenInsideDomain()
    {
        var wave = new SimpleWaveFunction1D((x, t) => 2f,
            new FloatRange(0f, 10f), 3f);

        ComplexF result = wave.Evaluate(5f, 0f);
        ComplexAssert.Equal(new ComplexF(6f, 0f), result, 5);
    }

    [Fact]
    public void Evaluate_Works_WhenOutsideDomain()
    {
        var wave = new SimpleWaveFunction1D((x, t) => 123f,
            new FloatRange(0f, 10f), 1f);

        ComplexF result = wave.Evaluate(-1f, 0f);
        ComplexAssert.Equal(new ComplexF(0, 0), result, 5);

    }

    [Fact]
    public void Evaluate_ReturnsZero_WhenOnDomainBoundary()
    {
        var wave = new SimpleWaveFunction1D((x, t) => 123f,
            new FloatRange(0f, 10f), 1f);

        ComplexF atMin = wave.Evaluate(0f, 0f);
        ComplexF atMax = wave.Evaluate(10f, 0f);

        Assert.Equal(0f, atMin.Real, 5);
        Assert.Equal(0f, atMax.Real, 5);
    }

    [Fact]
    public void ProbabilityDensity_ReturnsMagnitudeSquaredOfEvaluatedWave()
    {
        var wave = new SimpleWaveFunction1D((x, t) => new ComplexF(3f, 4f),
            new FloatRange(0f, 10f), 2f);

        // Raw value is 3 + 4i, amplitude is 2.
        // => value is 6 + 8i.
        // => |6 + 8i|^2 = 36 + 64 = 100.
        float density = wave.ProbabilityDensity(5f, 0f);

        Assert.Equal(100f, density, 5);
    }

    [Fact]
    public void ProbabilityDensity_ReturnsZeroOutsideDomain()
    {
        var wave = new SimpleWaveFunction1D((x, t) => new ComplexF(3f, 4f), new FloatRange(0f, 10f), 2f);
        Assert.Equal(0f, wave.ProbabilityDensity(11f, 0f), 5);
    }

    [Fact]
    public void ProbabilityInRange_Works()
    {
        var wave = new SimpleWaveFunction1D((x, t) => 2f,
            new FloatRange(0f, 10f), 3f);

        // ψ = 3 * 2 = 6
        // |ψ|^2 = 36
        // Integral over length 10 is 360.
        float probability = wave.ProbabilityInRange(t: 0f, domain: new FloatRange(0f, 10f), intervals: 10000);

        Assert.Equal(360f, probability, 1);
    }

    [Fact]
    public void TryNormalize_ReturnsTrue_ForNonZeroFiniteWave()
    {
        var wave = new SimpleWaveFunction1D((x, t) => 1f, new FloatRange(0f, 4f), 123f);
        Assert.True(wave.TryNormalize(t: 0f));

        // Integral of |1|^2 over length 4 is 4 => amplitude is 1 / sqrt(4)
        ComplexAssert.Equal(new ComplexF(0.5f, 0), wave.Amplitude, 1e-3f);
    }

    [Fact]
    public void TryNormalize_Works()
    {
        var wave = new SimpleWaveFunction1D((x, t) => 1f, new FloatRange(0f, 4f), 1f);

        bool success = wave.TryNormalize(t: 0f);

        Assert.True(success);

        float probability = wave.ProbabilityInRange(t: 0f, domain: new FloatRange(0f, 4f), intervals: 1000);

        Assert.Equal(1f, probability, 1e-3f);
    }

    [Fact]
    public void TryNormalize_ReturnsFalse_ForZeroWave()
    {
        var wave = new SimpleWaveFunction1D((x, t) => 0f,
            new FloatRange(0f, 4f), 1f);

        Assert.False(wave.TryNormalize(t: 0f));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Sample_Throws_WhenSampleCount_IsNotPositive(int sampleCount)
    {
        var wave = new SimpleWaveFunction1D((x, t) => 1f, new FloatRange(0f, 1f), 1f);

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            wave.Sample(sampleCount, out _, pointCount: 10, rng: new Random(123)));

        Assert.Equal("sampleCount", exception.ParamName);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    public void Sample_Throws_WhenPointCount_IsNotGreaterThanOne(int pointCount)
    {
        var wave = new SimpleWaveFunction1D((x, t) => 1f, new FloatRange(0f, 1f), 1f);

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            wave.Sample(sampleCount: 10, out _, pointCount: pointCount, rng: new Random(123)));

        Assert.Equal("pointCount", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void Sample_Throws_WhenTolerance_IsNotPositive(float tolerance)
    {
        var wave = new SimpleWaveFunction1D((x, t) => 1f, new FloatRange(0f, 1f), 1f);

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            wave.Sample(sampleCount: 10, out _, pointCount: 10, rng: new Random(123), tolerance: tolerance));

        Assert.Equal("tolerance", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void Sample_Throws_WhenMaxAllowed_IsNotPositive(float maxAllowed)
    {
        var wave = new SimpleWaveFunction1D((x, t) => 1f, new FloatRange(0f, 1f), 1f);
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            wave.Sample(sampleCount: 10, out _, pointCount: 10, rng: new Random(123), maxAllowed: maxAllowed));

        Assert.Equal("maxAllowed", exception.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Sample_Throws_WhenMaxCount_IsNotPositive(int maxCount)
    {
        var wave = new SimpleWaveFunction1D((x, t) => 1f, new FloatRange(0f, 1f), 1f);
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            wave.Sample(sampleCount: 10, out _, pointCount: 10, rng: new Random(123), maxCount: maxCount));

        Assert.Equal("maxCount", exception.ParamName);
    }

    [Fact]
    public void Sample_ReturnsSamples_InsideFiniteDomain()
    {
        var wave = new SimpleWaveFunction1D((x, t) => 1f, new FloatRange(0f, 1f), 1f);

        Assert.True(wave.Sample(sampleCount: 100, out float[] samples, t: 0f, pointCount: 100, rng: new Random(123)));
        Assert.Equal(100, samples.Length);

        foreach (float sample in samples)
        {
            Assert.True(sample > 0f);
            Assert.True(sample <= 1f);
        }
    }

    [Fact]
    public void Sample_ReturnsFalseAndEmptyArray_ForZeroWave()
    {
        var wave = new SimpleWaveFunction1D((x, t) => 0f, new FloatRange(0f, 1f), 1f);

        Assert.False(wave.Sample(sampleCount: 10, out float[] samples, t: 0f, pointCount: 100, rng: new Random(123)));

        Assert.Empty(samples);
    }
}