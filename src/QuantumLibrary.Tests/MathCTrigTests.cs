namespace CMath.Tests;

public class MathCTrigTests
{
    
    [Theory]
    [InlineData(MathF.PI / 2f, 0f, 1f, 0f)]
    [InlineData(0f, MathF.PI, 0f, 11.548739f)]
    [InlineData(1f, -1f, 1.298457f, -0.634963f)]
    public void Sin_Works(float real, float imaginary, float expectedReal, float expectedImaginary)
    {
        ComplexF z = new ComplexF(real, imaginary);
        ComplexF expected = new ComplexF(expectedReal, expectedImaginary);
        ComplexAssert.Equal(expected, MathC.Sin(z), 4);
    }

    [Theory]
    [InlineData(MathF.PI, 0f, -1f, 0f)]
    [InlineData(0f, MathF.PI, 11.591953f, 0f)]
    [InlineData(1f, -1f, 0.83373f, 0.9889f)]
    public void Cos_Works(float real, float imaginary, float expectedReal, float expectedImaginary)
    {
        ComplexF z = new ComplexF(real, imaginary);
        ComplexF expected = new ComplexF(expectedReal, expectedImaginary);
        ComplexAssert.Equal(expected, MathC.Cos(z), 4);
    }

    [Theory]
    [InlineData(MathF.PI, 0f, 0, 0f)]
    [InlineData(0f, MathF.PI, 0f, 0.996272f)]
    [InlineData(1f, -1f, 0.27175f, -1.08392f)]
    public void Tan_Works(float real, float imaginary, float expectedReal, float expectedImaginary)
    {
        ComplexF z = new ComplexF(real, imaginary);
        ComplexF expected = new ComplexF(expectedReal, expectedImaginary);
        ComplexAssert.Equal(expected, MathC.Tan(z), 4);
    }

    [Fact]
    public void Tan_NearPiOverTwo_ReturnsLargeFiniteValue()
    {
        var z = new ComplexF(MathF.PI / 2f, 0f);

        var result = MathC.Tan(z);

        Assert.True(MathF.Abs(result.Real) > 1000000f);
        Assert.Equal(0f, result.Imaginary, 5);
    }
}
