namespace CMath.Tests;

public class MathCPolarTests
{
    [Theory]
    [InlineData(1, 0, 0)]
    [InlineData(0, 1, MathF.PI / 2f)]
    [InlineData(-1, 0, MathF.PI)]
    [InlineData(0, -1, -MathF.PI / 2f)]
    [InlineData(1, 1, MathF.PI / 4)]
    public void Arg_Works(float real, float imaginary, float expected)
    {
        ComplexF z = new ComplexF(real, imaginary);
        Assert.Equal(expected, MathC.Arg(z), 5);
    }

    [Theory]
    [InlineData(0, 1f, 0)]
    [InlineData(MathF.PI / 2, 0, 1f)]
    [InlineData(MathF.PI, -1f, 0f)]
    [InlineData(-MathF.PI / 2f, 0, -1f)]
    [InlineData(MathF.PI / 4f, 0.70711f, 0.70711f)]
    public void Cis_Works(float angle, float realExpected, float imaginaryExpected)
    {
        ComplexF z = new ComplexF(realExpected, imaginaryExpected);
        ComplexAssert.Equal(z, MathC.Cis(angle), 5);
    }

    [Theory]
    [InlineData(1f, 0f, 1f, 0f)]
    [InlineData(0f, -1f, 1f, -MathF.PI/2f)]
    [InlineData(3f, 4f, 5f, 0.927295f)]
    public void ToPolar_Works(float real, float imaginary, float expectedRadius, float expectedAngle)
    {
        ComplexF z = new ComplexF(real, imaginary);
        TupleAssert.Equal((expectedRadius, expectedAngle), MathC.ToPolar(z), 4);
    }

    [Theory]
    [InlineData(1f, 0f, 1f, 0f)]
    [InlineData(1f, -MathF.PI/2f, 0f, -1f)]
    [InlineData(5f, 0.927295f, 3f, 4f)]
    public void FromPolar_Works(float radius, float angle, float expectedReal, float expectedImaginary)
    {
        ComplexF z = new ComplexF(expectedReal, expectedImaginary);
        ComplexAssert.Equal(z, MathC.FromPolar(radius, angle), 4);
    }
}
