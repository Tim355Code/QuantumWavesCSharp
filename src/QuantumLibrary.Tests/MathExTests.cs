using CMath;
using CMath.Tests;
using QuantumWaves.Utils;

namespace QuantumWaves.Tests;

public class MathExTests
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(5, 120)]
    public void Factorial_Works(int n, long expected)
    {
        Assert.Equal(expected, MathEx.Factorial(n));
    }

    [Fact]
    public void Factorial_ThrowsOn_NegativeInput()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => MathEx.Factorial(-1));
    }

    [Theory]
    [InlineData(0, 0, 10, 10)]
    [InlineData(1, 0, 2, 2)]
    [InlineData(2, 0, 3, 9)]
    public void Simpson_Works(float exponent, float min, float max, float expected)
    {
        Func<float, float> f = (x) => MathF.Pow(x, exponent);
        FloatRange range = new FloatRange(min, max);
        Assert.Equal(expected, MathEx.Simpson(f, range, 100), 3);
    }

    [Fact]
    public void SimpsonComplex_Works()
    {
        Func<float, ComplexF> f = (x) => new ComplexF(x, x * x);
        FloatRange range = new FloatRange(0, 2);

        ComplexAssert.Equal(new ComplexF(2, 8 / 3f), MathEx.SimpsonComplex(f, range, 100), 3);
    }

    [Fact]
    public void Simpson_ThrowsOn_NullFunction()
    {
        FloatRange range = new FloatRange(0, 1);
        Assert.Throws<ArgumentNullException>(() => MathEx.Simpson(null!, range, 100));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    public void Simpson_ThrowsOn_NonPositiveIntervals(int intervals)
    {
        FloatRange range = new FloatRange(0, 1);
        Assert.Throws<ArgumentOutOfRangeException>(() => MathEx.Simpson(x => x, range, intervals));
    }

    [Fact]
    public void Simpson_ThrowsOn_OddIntervals()
    {
        FloatRange range = new FloatRange(0, 1);
        Assert.Throws<ArgumentException>(() => MathEx.Simpson(x => x, range, 99));
    }

    [Fact]
    public void Simpson_ThrowsOn_InfiniteDomain()
    {
        FloatRange range = FloatRange.Infinite;
        Assert.Throws<ArgumentException>(() => MathEx.Simpson(x => x, range, 100));
    }

    [Fact]
    public void SimpsonComplex_ThrowsOn_NullFunction()
    {
        FloatRange range = new FloatRange(0, 1);

        Assert.Throws<ArgumentNullException>(() =>
            MathEx.SimpsonComplex(null!, range, 100));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    public void SimpsonComplex_ThrowsOn_NonPositiveIntervals(int intervals)
    {
        FloatRange range = new FloatRange(0, 1);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            MathEx.SimpsonComplex(x => new ComplexF(x, 0), range, intervals));
    }

    [Fact]
    public void SimpsonComplex_ThrowsOn_OddIntervals()
    {
        FloatRange range = new FloatRange(0, 1);

        Assert.Throws<ArgumentException>(() =>
            MathEx.SimpsonComplex(x => new ComplexF(x, 0), range, 99));
    }

    [Fact]
    public void SimpsonComplex_ThrowsOn_InfiniteDomain()
    {
        FloatRange range = FloatRange.Infinite;
        Assert.Throws<ArgumentException>(() => MathEx.SimpsonComplex(x => new ComplexF(x, 0), range, 100));
    }
}
