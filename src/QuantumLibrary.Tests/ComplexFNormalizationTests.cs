namespace CMath.Tests;

public class ComplexFNormalizationTests
{
    [Fact]
    public void Magnitude_Works()
    {
        Assert.Equal(5f, new ComplexF(3f, 4f).Magnitude, 5);
    }

    [Fact]
    public void MagnitudeSqr_Works()
    {
        Assert.Equal(25f, new ComplexF(3f, 4f).MagnitudeSqr, 5);
    }

    [Fact]
    public void Normalized_Works()
    {
        var result = new ComplexF(3f, 4f).Normalized;

        Assert.Equal(0.6f, result.Real, 5);
        Assert.Equal(0.8f, result.Imaginary, 5);
    }

    [Fact]
    public void Normalized_Zero_ReturnsNaN()
    {
        Assert.True(ComplexF.Zero.Normalized.IsNaN);
    }
}
