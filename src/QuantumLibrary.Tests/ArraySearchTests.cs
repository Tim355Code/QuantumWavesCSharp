namespace QuantumWaves.Tests;

using QuantumWaves.Utils;

public class ArraySearchExtensionsTests
{
    [Fact]
    public void LowerBound_Works_WhenContained()
    {
        float[] values = [1f, 2f, 3f, 4f];

        int index = values.LowerBound(3f);

        Assert.Equal(2, index);
    }

    [Fact]
    public void LowerBound_Works_WhenNotContained()
    {
        float[] values = [1f, 3f, 5f, 7f];

        int index = values.LowerBound(4f);

        Assert.Equal(2, index);
    }

    [Fact]
    public void LowerBound_ReturnsFirstDuplicate()
    {
        float[] values = [1f, 2f, 2f, 2f, 3f];

        int index = values.LowerBound(2f);

        Assert.Equal(1, index);
    }

    [Fact]
    public void LowerBound_ReturnsZero_WhenTargetIsSmallest()
    {
        float[] values = [10f, 20f, 30f];

        int index = values.LowerBound(5f);

        Assert.Equal(0, index);
    }

    [Fact]
    public void LowerBound_ReturnsLength_WhenTargetLargest()
    {
        float[] values = [1f, 2f, 3f];

        int index = values.LowerBound(10f);

        Assert.Equal(values.Length, index);
    }

    [Fact]
    public void LowerBound_ReturnsZero_ForEmptyArray()
    {
        float[] values = [];

        int index = values.LowerBound(1f);

        Assert.Equal(0, index);
    }
}