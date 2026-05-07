using QuantumWaves.Solutions;

namespace QuantumWaves.Tests;

public class InfiniteWell1DTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void State_Throws_WhenN_IsNotPositive(int n)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            InfiniteWell1D.State(n, length: 1f, mass: 1f));

        Assert.Equal("n", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void State_Throws_WhenLength_IsNotPositive(float length)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            InfiniteWell1D.State(n: 1, length: length, mass: 1f));

        Assert.Equal("length", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void State_Throws_WhenMass_IsNotPositive(float mass)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            InfiniteWell1D.State(n: 1, length: 1f, mass: mass));

        Assert.Equal("mass", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void State_Throws_WhenHBar_IsNotPositive(float hBar)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            InfiniteWell1D.State(n: 1, length: 1f, mass: 1f, hBar: hBar));

        Assert.Equal("hBar", exception.ParamName);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void State_IsNormalizable(int n)
    {
        var wave = InfiniteWell1D.State(n: n,length: 2f, mass: 1f, hBar: 1f);
        Assert.True(wave.TryNormalize());
    }

    [Fact]
    public void State_HasDomainWithExpectedLength()
    {
        var wave = InfiniteWell1D.State(n: 2,length: 2f, mass: 1f, hBar: 1f);
        Assert.Equal(2f, wave.Domain.Length);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Energy_Throws_WhenN_IsNotPositive(int n)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            InfiniteWell1D.Energy(n, length: 1f, mass: 1f));

        Assert.Equal("n", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void Energy_Throws_WhenLength_IsNotPositive(float length)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            InfiniteWell1D.Energy(n: 1, length: length, mass: 1f));

        Assert.Equal("length", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void Energy_Throws_WhenMass_IsNotPositive(float mass)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            InfiniteWell1D.Energy(n: 1, length: 1f, mass: mass));

        Assert.Equal("mass", exception.ParamName);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void Energy_Throws_WhenHBar_IsNotPositive(float hBar)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            InfiniteWell1D.Energy(n: 1, length: 1f, mass: 1f, hBar: hBar));

        Assert.Equal("hBar", exception.ParamName);
    }

    [Fact]
    public void Energy_Works()
    {
        float energy = InfiniteWell1D.Energy(n: 1, length: 1f, mass: 1f, hBar: 1f);

        float expected = MathF.PI * MathF.PI / 2f;

        Assert.Equal(expected, energy, 5);
    }
}
