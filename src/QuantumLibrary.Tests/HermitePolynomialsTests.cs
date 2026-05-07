using QuantumWaves.Utils;

namespace QuantumWaves.Tests;

public class HermitePolynomialsTests
{
    private const int Precision = 5;

    [Fact]
    public void Delegate_RejectsNegativeN()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            HermitePolynomials.Delegate(-1));
    }

    [Theory]
    [InlineData(0f, 1f)]
    [InlineData(1f, 1f)]
    [InlineData(-2f, 1f)]
    public void H0_IsAlwaysOne(float x, float expected)
    {
        var h0 = HermitePolynomials.Delegate(0);

        Assert.Equal(expected, h0(x), Precision);
    }

    [Theory]
    [InlineData(0f, 0f)]
    [InlineData(1f, 2f)]
    [InlineData(-2f, -4f)]
    public void H1_IsTwoX(float x, float expected)
    {
        var h1 = HermitePolynomials.Delegate(1);

        Assert.Equal(expected, h1(x), Precision);
    }

    [Theory]
    [InlineData(0f, -2f)]
    [InlineData(1f, 2f)]
    [InlineData(2f, 14f)]
    public void H2_MatchesKnownPolynomial(float x, float expected)
    {
        var h2 = HermitePolynomials.Delegate(2);

        Assert.Equal(expected, h2(x), Precision);
    }

    [Theory]
    [InlineData(0f, 0f)]
    [InlineData(1f, -4f)]
    [InlineData(2f, 40f)]
    public void H3_MatchesKnownPolynomial(float x, float expected)
    {
        var h3 = HermitePolynomials.Delegate(3);

        Assert.Equal(expected, h3(x), Precision);
    }

    [Theory]
    [InlineData(0f, 12f)]
    [InlineData(1f, -20f)]
    [InlineData(2f, 76f)]
    public void H4_MatchesKnownPolynomial(float x, float expected)
    {
        var h4 = HermitePolynomials.Delegate(4);

        Assert.Equal(expected, h4(x), Precision);
    }
}