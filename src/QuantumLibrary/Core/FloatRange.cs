using System;


/// <summary>
/// Represents a range of float values.
/// </summary>
public readonly struct FloatRange
{
    /// <summary>
    /// A range spanning all infinite float values.
    /// </summary>
    public static readonly FloatRange Infinite = new FloatRange(float.NegativeInfinity, float.PositiveInfinity);

    /// <summary>
    /// The lower bound of the range.
    /// </summary>
    public readonly float Min;

    /// <summary>
    /// The upper bound of the range.
    /// </summary>
    public readonly float Max;

    /// <summary>
    /// Creates a new float range.
    /// </summary>
    /// <param name="min">The lower bound.</param>
    /// <param name="max">The upper bound.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="max"/> is less than <paramref name="min"/>.
    /// </exception>
    public FloatRange(float min, float max)
    {
        if (max < min)
            throw new ArgumentOutOfRangeException("Min may not be less max!");

        Min = min;
        Max = max;
    }

    /// <summary>
    /// Determines whether a value lies within the range.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <returns>True if the value is inside the range, otherwise false.</returns>
    public bool Contains(float value)
    {
        return value > Min && value < Max;
    }

    /// <summary>
    /// The size of the range.
    /// </summary>
    public float Length => Max - Min;

    /// <summary>
    /// Returns if either the minimum or maximum is infinite.
    /// </summary>
    public bool IsInfinite => float.IsInfinity(Min) || float.IsInfinity(Max);
}