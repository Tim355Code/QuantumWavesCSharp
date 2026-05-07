namespace CMath.Tests;

public static class ComplexAssert
{
    public static void Equal(ComplexF expected, ComplexF actual, int precision = 5)
    {
        Assert.Equal(expected.Real, actual.Real, precision);
        Assert.Equal(expected.Imaginary, actual.Imaginary, precision);
    }

    public static void Equal(ComplexF expected, ComplexF actual, float tolerance = 1e-5f)
    {
        Assert.True(MathF.Abs(expected.Real - actual.Real) < tolerance);
        Assert.True(MathF.Abs(expected.Imaginary - actual.Imaginary) < tolerance);
    }
}
