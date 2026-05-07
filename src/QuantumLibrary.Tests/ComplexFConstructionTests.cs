namespace CMath.Tests;

public class ComplexFConstructionTests
{
    [Fact]
    public void Constructor_Works()
    {
        var c = new ComplexF(2f, 3f);

        Assert.Equal(2f, c.Real);
        Assert.Equal(3f, c.Imaginary);
    }

    [Fact]
    public void Constants_HaveExpectedValues()
    {
        Assert.Equal(new ComplexF(0f, 0f), ComplexF.Zero);
        Assert.Equal(new ComplexF(1f, 0f), ComplexF.One);
        Assert.Equal(new ComplexF(0f, 1f), ComplexF.I);

        Assert.True(ComplexF.NaN.IsNaN);
        Assert.True(ComplexF.PositiveRealInfinity.IsInfinity);
        Assert.True(ComplexF.NegativeRealInfinity.IsInfinity);
        Assert.True(ComplexF.PositiveImaginaryInfinity.IsInfinity);
        Assert.True(ComplexF.NegativeImaginaryInfinity.IsInfinity);
    }
}
