namespace QuantumWaves.Tests;

public class SimpleWaveFunction1DTests
{
    [Fact]
    public void Evaluate_Works()
    {
        bool called = false;

        var wave = new SimpleWaveFunction1D(
            (x, t) =>
            {
                called = true;
                return x + t;
            }, FloatRange.Infinite, 1f);

        wave.Evaluate(1f, 2f);
        Assert.True(called);
    }
}