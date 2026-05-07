namespace CMath.Tests;

public class ComplexFGetHashCodeTests
{
    [Fact]
    public void GetHashCode_SameValues_ReturnsSameHashCode()
    {
        var a = new ComplexF(3f, -4f);
        var b = new ComplexF(3f, -4f);

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentValues_UsuallyReturnsDifferentHashCode()
    {
        var a = new ComplexF(3f, -4f);
        var b = new ComplexF(4f, -3f);

        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void GetHashCode_Zero_EqualsHashCodeOfEquivalentZero()
    {
        var z = new ComplexF(0f, 0f);

        Assert.Equal(ComplexF.Zero.GetHashCode(), z.GetHashCode());
    }
}
