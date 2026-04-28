using CMath;

namespace QuantumLibrary.Tests;

public class ComplexFToStringTests
{
    [Fact]
    public void ToString_PositiveImaginary()
    {
        Assert.Equal("2 + 5i", new ComplexF(2f, 5f).ToString());
    }

    [Fact]
    public void ToString_NegativeImaginary()
    {
        Assert.Equal("2 - 5i", new ComplexF(2f, -5f).ToString());
    }

    [Fact]
    public void ToString_ZeroImaginary()
    {
        Assert.Equal("2 + 0i", new ComplexF(2f, 0f).ToString());
    }

    [Fact]
    public void ToString_NaN_ContainsNaN()
    {
        Assert.Contains("NaN", ComplexF.NaN.ToString());
    }

    [Fact]
    public void ToString_Infinity_UsesCurrentFloatFormatting()
    {
        Assert.Contains(float.PositiveInfinity.ToString(), ComplexF.PositiveRealInfinity.ToString());
        Assert.Contains(float.NegativeInfinity.ToString(), ComplexF.NegativeRealInfinity.ToString());
    }
}
