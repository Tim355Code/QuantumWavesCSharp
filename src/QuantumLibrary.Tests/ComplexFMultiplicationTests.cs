namespace CMath.Tests;

public class ComplexFMultiplicationTests
{
    [Theory]
    [InlineData(1f, 2f, 3f, 4f, -5f, 10f)]
    [InlineData(0f, 1f, 0f, 1f, -1f, 0f)]
    [InlineData(2f, 0f, 3f, 4f, 6f, 8f)]
    public void Multiplication_Works(float ar, float ai, float br, float bi, float er, float ei)
    {
        var result = new ComplexF(ar, ai) * new ComplexF(br, bi);

        Assert.Equal(er, result.Real, 5);
        Assert.Equal(ei, result.Imaginary, 5);
    }

    [Fact]
    public void Multiplication_ByZero_ReturnsZero()
    {
        Assert.Equal(ComplexF.Zero, new ComplexF(3f, 4f) * ComplexF.Zero);
    }

    [Fact]
    public void Multiplication_ByOne_ReturnsSameValue()
    {
        var z = new ComplexF(3f, 4f);

        Assert.Equal(z, z * ComplexF.One);
    }

    [Fact]
    public void Multiplication_ByNaN_ReturnsNaN()
    {
        Assert.True((new ComplexF(3f, 4f) * ComplexF.NaN).IsNaN);
    }

    [Fact]
    public void Multiplication_WithInfinity_ReturnsInfinity()
    {
        var result = new ComplexF(3f, 4f) * ComplexF.PositiveRealInfinity;

        Assert.True(result.IsInfinity);
    }
}
