namespace CMath.Tests;

public class ComplexFEqualityTests
{
    [Fact]
    public void Equals_SameComponents_ReturnsTrue()
    {
        var a = new ComplexF(1f, 2f);
        object b = new ComplexF(1f, 2f);

        Assert.True(a.Equals(b));
        Assert.True(a == (ComplexF)b);
    }

    [Fact]
    public void Equals_DifferentComponents_ReturnsFalse()
    {
        var a = new ComplexF(1f, 2f);
        var b = new ComplexF(1f, 3f);

        Assert.False(a.Equals(b));
        Assert.True(a != b);
    }

    [Fact]
    public void Equals_NullOrDifferentType_ReturnsFalse()
    {
        var a = new ComplexF(1f, 2f);

        Assert.False(a.Equals(null));
        Assert.False(a.Equals("not complex"));
    }

    [Fact]
    public void Equals_NaNComponents_ReturnsFalse()
    {
        var a = new ComplexF(float.NaN, 0f);
        var b = new ComplexF(float.NaN, 0f);

        Assert.False(a == b);
    }

        [Fact]
    public void Equals_InfinityComponents_ReturnsTrue()
    {
        var a = new ComplexF(float.PositiveInfinity, 0f);
        var b = new ComplexF(float.PositiveInfinity, 0f);

        Assert.True(a == b);
    }

    [Fact]
    public void Inequality_SameComponents_ReturnsFalse()
    {
        var a = new ComplexF(1f, 2f);
        var b = new ComplexF(1f, 2f);

        Assert.False(a != b);
    }

    [Fact]
    public void Inequality_DifferentReal_ReturnsTrue()
    {
        var a = new ComplexF(1f, 2f);
        var b = new ComplexF(3f, 2f);

        Assert.True(a != b);
    }

    [Fact]
    public void Inequality_DifferentImaginary_ReturnsTrue()
    {
        var a = new ComplexF(1f, 2f);
        var b = new ComplexF(1f, 3f);

        Assert.True(a != b);
    }

    [Fact]
    public void Inequality_NaNComponents_ReturnsTrue()
    {
        var a = new ComplexF(float.NaN, 0f);
        var b = new ComplexF(float.NaN, 0f);

        Assert.True(a != b);
    }

    [Fact]
    public void Inequality_InfinityComponents_ReturnsFalse()
    {
        var a = new ComplexF(float.PositiveInfinity, 0f);
        var b = new ComplexF(float.PositiveInfinity, 0f);

        Assert.False(a != b);
    }

    [Fact]
    public void Equality_And_Inequality_AreConsistent()
    {
        var a = new ComplexF(1f, 2f);
        var b = new ComplexF(1f, 2f);
        var c = new ComplexF(2f, 3f);

        Assert.True(a == b);
        Assert.False(a != b);

        Assert.False(a == c);
        Assert.True(a != c);
    }
}
