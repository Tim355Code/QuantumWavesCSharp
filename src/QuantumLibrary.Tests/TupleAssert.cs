namespace CMath.Tests;

public static class TupleAssert
{
    public static void Equal((float a, float b) tupleA, (float a, float b) tupleB, int precision = 5)
    {
        Assert.Equal(tupleA.a, tupleB.a, precision);
        Assert.Equal(tupleA.b, tupleB.b, precision);
    }
}
