namespace QuantumWaves.Tests;

public class FloatRangeTests
{
    [Fact]
    public void Constructor_Works()
    {
        var range = new FloatRange(1.5f, 10.5f);

        Assert.Equal(1.5f, range.Min);
        Assert.Equal(10.5f, range.Max);
    }

    [Theory]
    [InlineData(0f, 10f, 5f, true)]
    [InlineData(0f, 10f, 0.0001f, true)]
    [InlineData(0f, 10f, 9.9999f, true)]
    [InlineData(0f, 10f, 0f, false)]
    [InlineData(0f, 10f, 10f, false)]
    [InlineData(0f, 10f, -1f, false)]
    [InlineData(0f, 10f, 11f, false)]
    public void Contains_Works(float min, float max, float value, bool expected)
    {
        var range = new FloatRange(min, max);
        var result = range.Contains(value);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Contains_IsExclusiveOfMinAndMax()
    {
        var range = new FloatRange(-5f, 5f);
        Assert.False(range.Contains(-5f));
        Assert.False(range.Contains(5f));
    }

    [Fact]
    public void Constructor_ThrowsWhenMaxIsLessThanMin()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new FloatRange(10f, 5f));
    }

    [Fact]
    public void Contains_ReturnsTrue_ForFiniteValue_InInfiniteRange()
    {
        var range = FloatRange.Infinite;
        Assert.True(range.Contains(0f));
        Assert.True(range.Contains(float.MinValue));
        Assert.True(range.Contains(float.MaxValue));
    }

    [Fact]
    public void Contains_ReturnsFalse_ForInfinityValues_InInfiniteRange()
    {
        var range = FloatRange.Infinite;
        Assert.False(range.Contains(float.NegativeInfinity));
        Assert.False(range.Contains(float.PositiveInfinity));
    }

    [Theory]
    [InlineData(0f, 10f, 10f)]
    [InlineData(-5f, 5f, 10f)]
    [InlineData(2.5f, 7.5f, 5f)]
    public void Length_Works_OnFiniteRange(float min, float max, float expected)
    {
        var range = new FloatRange(min, max);
        Assert.Equal(expected, range.Length);
    }

    [Fact]
    public void Length_Works_OnInfiniteRange()
    {
        var range = FloatRange.Infinite;
        Assert.Equal(float.PositiveInfinity, range.Length);
    }

    [Theory]
    [InlineData(0f, 10f, false)]
    [InlineData(float.NegativeInfinity, 10f, true)]
    [InlineData(0f, float.PositiveInfinity, true)]
    [InlineData(float.NegativeInfinity, float.PositiveInfinity, true)]
    public void IsInfinite_Works(float min, float max, bool expected)
    {
        var range = new FloatRange(min, max);
        Assert.Equal(expected, range.IsInfinite);
    }

    [Fact]
    public void Infinite_HasNegativeInfinityMinAndPositiveInfinityMax()
    {
        Assert.Equal(float.NegativeInfinity, FloatRange.Infinite.Min);
        Assert.Equal(float.PositiveInfinity, FloatRange.Infinite.Max);
    }

    [Fact]
    public void Infinite_IsInfinite()
    {
        Assert.True(FloatRange.Infinite.IsInfinite);
    }
}
