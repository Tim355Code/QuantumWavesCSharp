namespace CMath.Tests;

public class ComplexFPropertyTests
{
    [Fact]
    public void Constants_HaveExpectedValues()
    {
        Assert.Equal(new ComplexF(0f, 0f), ComplexF.Zero);
        Assert.Equal(new ComplexF(1f, 0f), ComplexF.One);
        Assert.Equal(new ComplexF(0f, 1f), ComplexF.I);
    }

    [Fact]
    public void IsNaN_WhenEitherComponentIsNaN()
    {
        Assert.True(new ComplexF(float.NaN, 1f).IsNaN);
        Assert.True(new ComplexF(1f, float.NaN).IsNaN);
    }

    [Fact]
    public void IsInfinity_WhenEitherComponentIsInfinity()
    {
        Assert.True(new ComplexF(float.PositiveInfinity, 1f).IsInfinity);
        Assert.True(new ComplexF(1f, float.NegativeInfinity).IsInfinity);
    }

    [Fact]
    public void IsFinite_WhenBothComponentsAreFinite()
    {
        Assert.True(new ComplexF(3f, -4f).IsFinite);
        Assert.False(new ComplexF(float.PositiveInfinity, 0f).IsFinite);
    }
}
