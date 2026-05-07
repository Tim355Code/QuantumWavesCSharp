using CMath;
using CMath.Tests;

namespace QuantumWaves.Tests;

public class WeightedWaveFunction1DTests
{
    [Fact]
    public void Constructor_Throws_WhenArraySizesDoNotMatch()
    {
        var coefficients = new[] { ComplexF.One };
        var waves = new WaveFunction1D[]
        {
            new SimpleWaveFunction1D((x, t) => 1f, FloatRange.Infinite, 1f),
            new SimpleWaveFunction1D((x, t) => 2f, FloatRange.Infinite, 1f)
        };

        Assert.Throws<ArgumentException>(() => new WeightedWaveFunction1D(coefficients, waves));
    }

    [Fact]
    public void Constructor_Throws_WhenNoWaveFunctionsProvided()
    {
        Assert.Throws<ArgumentException>(() => new WeightedWaveFunction1D([], []));
    }

    [Fact]
    public void Constructor_Throws_WhenWaveFunctionElementIsNull()
    {
        var coefficients = new[] { ComplexF.One };
        WaveFunction1D[] waves = [null!];

        Assert.Throws<ArgumentNullException>(() => new WeightedWaveFunction1D(coefficients, waves));
    }

    [Fact]
    public void Evaluate_ComputesWeightedSum()
    {
        var wave1 = new SimpleWaveFunction1D((x, t) => 1f, FloatRange.Infinite, 1f);

        var wave2 = new SimpleWaveFunction1D( (x, t) => 2f, FloatRange.Infinite, 1f);

        var weighted = new WeightedWaveFunction1D([2f, 3f], [wave1, wave2]);

        // 2*1 + 3*2 = 8
        ComplexF result = weighted.Evaluate(0f, 0f);

        ComplexAssert.Equal(new ComplexF(8f, 0f), result, 5);
    }

    [Fact]
    public void Add_Works()
    {
        var weighted = new WeightedWaveFunction1D([1f], [new SimpleWaveFunction1D((x, t) => 1f, FloatRange.Infinite, 1f)]);
        var wave = new SimpleWaveFunction1D((x, t) => 2f, FloatRange.Infinite, 1f);

        weighted.Add(3f, wave);

        Assert.Equal(2, weighted.WaveFunctions.Count);
        Assert.Equal(2, weighted.Coefficients.Count);
        Assert.Same(wave, weighted.WaveFunctions[1]);
    }

    [Fact]
    public void Add_Throws_WhenWaveFunctionIsNull()
    {
        var weighted = new WeightedWaveFunction1D([1f], [new SimpleWaveFunction1D((x, t) => 1f, FloatRange.Infinite, 1f)]);
        Assert.Throws<ArgumentNullException>(() => weighted.Add(1f, null!));
    }
}