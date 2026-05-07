namespace CMath.Tests;

public class MathCExponentialTests
{
    [Theory]
    [InlineData(0f, 0f, 1f, 0f)]
    [InlineData(1f, 0f, 2.718282f, 0f)]
    [InlineData(0f, MathF.PI, -1f, 0f)]
    [InlineData(1f, MathF.PI, -2.718282f, 0f)]
    public void Exp_Works(float real, float imaginary, float expectedReal, float expectedImaginary)
    {
        var z = new ComplexF(real, imaginary);
        var expected = new ComplexF(expectedReal, expectedImaginary);

        ComplexAssert.Equal(expected, MathC.Exp(z), 4);
    }

    [Theory]
    [InlineData(1f, 0f, 0f, 0f)]
    [InlineData(MathF.E, 0f, 1f, 0f)]
    [InlineData(-1f, 0f, 0f, MathF.PI)]
    [InlineData(0f, 1f, 0f, MathF.PI / 2f)]
    [InlineData(3f, 4f, 1.609438f, 0.927295f)]
    public void Ln_Works(float real, float imaginary, float expectedReal, float expectedImaginary)
    {
        var z = new ComplexF(real, imaginary);
        var expected = new ComplexF(expectedReal, expectedImaginary);

        ComplexAssert.Equal(expected, MathC.Ln(z), 4);
    }

    [Theory]
    [InlineData(8f, 0f, 2f, 0f, 3f, 0f)]
    [InlineData(100f, 0f, 10f, 0f, 2f, 0f)]
    [InlineData(1f, 0f, 10f, 0f, 0f, 0f)]
    public void Log_Works(float valueReal, float valueImaginary, float baseReal, float baseImaginary,
        float expectedReal, float expectedImaginary)
    {
        var value = new ComplexF(valueReal, valueImaginary);
        var baseValue = new ComplexF(baseReal, baseImaginary);
        var expected = new ComplexF(expectedReal, expectedImaginary);

        ComplexAssert.Equal(expected, MathC.Log(value, baseValue), 4);
    }

    [Theory]
    [InlineData(2f, 0f, 3f, 0f, 8f, 0f)]
    [InlineData(4f, 0f, 0.5f, 0f, 2f, 0f)]
    [InlineData(-1f, 0f, 2f, 0f, 1f, 0f)]
    [InlineData(MathF.E, 0f, 0f, MathF.PI, -1f, 0f)]
    public void Pow_Works(float baseReal, float baseImaginary, float exponentReal, float exponentImaginary,
        float expectedReal, float expectedImaginary)
    {
        var baseValue = new ComplexF(baseReal, baseImaginary);
        var exponent = new ComplexF(exponentReal, exponentImaginary);
        var expected = new ComplexF(expectedReal, expectedImaginary);

        ComplexAssert.Equal(expected, MathC.Pow(baseValue, exponent), 4);
    }

    [Theory]
    [InlineData(4f, 0f, 2f, 0f)]
    [InlineData(0f, 4f, 1.414214f, 1.414214f)]
    [InlineData(-4f, 0f, 0f, 2f)]
    [InlineData(3f, 4f, 2f, 1f)]
    public void Sqrt_Works(float real, float imaginary, float expectedReal, float expectedImaginary)
    {
        var z = new ComplexF(real, imaginary);
        var expected = new ComplexF(expectedReal, expectedImaginary);

        ComplexAssert.Equal(expected, MathC.Sqrt(z), 4);
    }

    [Fact]
    public void ExpAndLn_AreInverses_ForPositiveRealNumber()
    {
        var z = new ComplexF(5f, 0f);

        ComplexAssert.Equal(z, MathC.Exp(MathC.Ln(z)), 4);
    }

    [Fact]
    public void Sqrt_ResultSquared_EqualsOriginal()
    {
        var z = new ComplexF(3f, 4f);

        var sqrt = MathC.Sqrt(z);

        ComplexAssert.Equal(z, sqrt * sqrt, 4);
    }
}