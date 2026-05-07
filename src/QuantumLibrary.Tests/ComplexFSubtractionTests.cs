namespace CMath.Tests;

public class ComplexFSubtractionTests
{
    [Theory]
    [InlineData(1f, 0f, 0f, 1f)]
    [InlineData(1f, 2f, 2f, 1f)]
    [InlineData(1.5f, 7.2f, -2.3f, 1.1f)]
    public void Subtraction_Works(float ar, float ai, float br, float bi)
    {
        var result = new ComplexF(ar, ai) - new ComplexF(br, bi);

        Assert.Equal(ar - br, result.Real, 5);
        Assert.Equal(ai - bi, result.Imaginary, 5);
    }

    [Fact]
    public void Subtraction_WithZero_ReturnsSameValue()
    {
        var z = new ComplexF(3f, -4f);

        Assert.Equal(z, z - ComplexF.Zero);
    }

    [Fact]
    public void Subtraction_WithNaN_ReturnsNaN()
    {
        Assert.True((new ComplexF(3f, -4f) - ComplexF.NaN).IsNaN);
    }

    [Fact]
    public void Subtraction_SameInfinity_ReturnsNaN()
    {
        Assert.True((ComplexF.PositiveRealInfinity - ComplexF.PositiveRealInfinity).IsNaN);
    }

    [Fact]
    public void UnaryNegation_Works()
    {
        Assert.Equal(new ComplexF(-3f, -4f), -new ComplexF(3f, 4f));
    }
}
