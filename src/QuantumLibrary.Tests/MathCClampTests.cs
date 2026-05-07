namespace CMath.Tests;

public class MathCClampTests
{
    [Theory]
    [InlineData(0.5f, 0.5f, 0f, 0f, 1f, 1f, 0.5f, 0.5f)]
    [InlineData(-1f, 0.5f, 0f, 0f, 1f, 1f, 0f, 0.5f)]
    [InlineData(0.5f, 2f, 0f, 0f, 1f, 1f, 0.5f, 1f)]
    [InlineData(-1f, 2f, 0f, 0f, 1f, 1f, 0f, 1f)]
    public void Clamp_Works(float valueReal, float valueImaginary, float minReal, float minImaginary,
        float maxReal, float maxImaginary, float expectedReal, float expectedImaginary)
    {
        var value = new ComplexF(valueReal, valueImaginary);
        var min = new ComplexF(minReal, minImaginary);
        var max = new ComplexF(maxReal, maxImaginary);
        var expected = new ComplexF(expectedReal, expectedImaginary);

        ComplexAssert.Equal(expected, MathC.Clamp(value, min, max), 5);
    }

    [Theory]
    [InlineData(0.5f, 0.5f, 0.5f, 0.5f)]
    [InlineData(-1f, 0.5f, 0f, 0.5f)]
    [InlineData(0.5f, 2f, 0.5f, 1f)]
    [InlineData(-1f, 2f, 0f, 1f)]
    public void Clamp01_Works(float valueReal, float valueImaginary, float expectedReal, float expectedImaginary)
    {
        var value = new ComplexF(valueReal, valueImaginary);
        var expected = new ComplexF(expectedReal, expectedImaginary);

        ComplexAssert.Equal(expected, MathC.Clamp01(value), 5);
    }
}
