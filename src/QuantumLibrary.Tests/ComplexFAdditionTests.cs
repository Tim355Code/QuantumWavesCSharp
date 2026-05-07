namespace CMath.Tests;

public class ComplexFAdditionTests
{
    [Theory]
    [InlineData(1f, 0f, 0f, 1f)]
    [InlineData(1f, 2f, 2f, 1f)]
    [InlineData(1.5f, 7.2f, -2.3f, 1.1f)]
    public void Addition_Works(float ar, float ai, float br, float bi)
    {
        var result = new ComplexF(ar, ai) + new ComplexF(br, bi);

        Assert.Equal(ar + br, result.Real, 5);
        Assert.Equal(ai + bi, result.Imaginary, 5);
    }

    [Fact]
    public void Addition_WithZero_ReturnsSameValue()
    {
        var z = new ComplexF(3f, -4f);

        Assert.Equal(z, z + ComplexF.Zero);
    }

    [Fact]
    public void Addition_WithNaN_ReturnsNaN()
    {
        Assert.True((new ComplexF(3f, -4f) + ComplexF.NaN).IsNaN);
    }

    [Fact]
    public void Addition_WithInfinity_ReturnsInfinity()
    {
        var result = new ComplexF(3f, -4f) + ComplexF.PositiveRealInfinity;

        Assert.True(result.IsInfinity);
        Assert.Equal(float.PositiveInfinity, result.Real);
    }
}
