namespace CMath.Tests;

public class MathCLerpTests
{
    [Theory]
    [InlineData(0f, 0f, 10f, 20f, 0f, 0f, 0f)]
    [InlineData(0f, 0f, 10f, 20f, 0.5f, 5f, 10f)]
    [InlineData(0f, 0f, 10f, 20f, 1f, 10f, 20f)]
    [InlineData(2f, 4f, 10f, 20f, 0.25f, 4f, 8f)]
    public void LerpUnclamped_Works(float aReal, float aImaginary, float bReal, float bImaginary, float t,
        float expectedReal, float expectedImaginary)
    {
        var a = new ComplexF(aReal, aImaginary);
        var b = new ComplexF(bReal, bImaginary);
        var expected = new ComplexF(expectedReal, expectedImaginary);

        ComplexAssert.Equal(expected, MathC.LerpUnclamped(a, b, t), 5);
    }

    [Theory]
    [InlineData(0f, 0f, 10f, 20f, -1f, 0f, 0f)]
    [InlineData(0f, 0f, 10f, 20f, 0f, 0f, 0f)]
    [InlineData(0f, 0f, 10f, 20f, 0.5f, 5f, 10f)]
    [InlineData(0f, 0f, 10f, 20f, 1f, 10f, 20f)]
    [InlineData(0f, 0f, 10f, 20f, 2f, 10f, 20f)]
    public void Lerp_Works_AndClampsT(float aReal, float aImaginary, float bReal, float bImaginary, float t,
        float expectedReal, float expectedImaginary)
    {
        var a = new ComplexF(aReal, aImaginary);
        var b = new ComplexF(bReal, bImaginary);
        var expected = new ComplexF(expectedReal, expectedImaginary);

        ComplexAssert.Equal(expected, MathC.Lerp(a, b, t), 5);
    }
}
