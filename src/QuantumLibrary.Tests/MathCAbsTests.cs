namespace CMath.Tests;

public class MathCAbsTests
{
    [Fact]
    public void Abs_Works()
    {
        Assert.Equal(5f, MathC.Abs(new ComplexF(3f, 4f)), 5);
    }

    [Fact]
    public void AbsSqr_Works()
    {
        Assert.Equal(25f, MathC.AbsSqr(new ComplexF(3f, 4f)), 5);
    }

    [Fact]
    public void Approximately_ReturnsTrue_WhenWithinTolerance()
    {
        var a = new ComplexF(1f, 1f);
        var b = new ComplexF(1.000001f, 1.000001f);

        Assert.True(MathC.Approximately(a, b));
    }

    [Fact]
    public void Approximately_ReturnsFalse_WhenOutsideTolerance()
    {
        var a = new ComplexF(1f, 1f);
        var b = new ComplexF(1.1f, 1.1f);

        Assert.False(MathC.Approximately(a, b));
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    public void Approximately_Throws_WhenToleranceIsNotPositive(float tolerance)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            MathC.Approximately(ComplexF.Zero, ComplexF.Zero, tolerance));
    }
}