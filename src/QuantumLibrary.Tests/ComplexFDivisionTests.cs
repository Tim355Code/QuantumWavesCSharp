namespace CMath.Tests;

public class ComplexFDivisionTests
{
    [Theory]
    [InlineData(1f, 2f, 3f, 4f, 0.44f, 0.08f)]
    [InlineData(3f, 4f, 1f, 0f, 3f, 4f)]
    [InlineData(6f, 8f, 2f, 0f, 3f, 4f)]
    [InlineData(3f, 4f, 0f, 1f, 4f, -3f)]
    public void Division_Works(float ar, float ai, float br, float bi, float er, float ei)
    {
        var result = new ComplexF(ar, ai) / new ComplexF(br, bi);

        Assert.Equal(er, result.Real, 5);
        Assert.Equal(ei, result.Imaginary, 5);
    }

    [Theory]
    [InlineData(0f, 0f)]
    [InlineData(3f, 4f)]
    [InlineData(-3f, 4f)]
    public void Division_ByZero_ReturnsNaN(float real, float imaginary)
    {
        var result = new ComplexF(real, imaginary) / ComplexF.Zero;

        Assert.True(result.IsNaN);
    }

    [Fact]
    public void Division_WithNaN_ReturnsNaN()
    {
        Assert.True((new ComplexF(3f, 4f) / ComplexF.NaN).IsNaN);
        Assert.True((ComplexF.NaN / new ComplexF(3f, 4f)).IsNaN);
    }

    [Fact]
    public void Division_ByOne_ReturnsSameValue()
    {
        var z = new ComplexF(3f, 4f);

        Assert.Equal(z, z / ComplexF.One);
    }
}
