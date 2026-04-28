public readonly struct FloatRange
{
    public static readonly FloatRange Infinite = new FloatRange(float.NegativeInfinity, float.PositiveInfinity);
    
    public readonly float Min;
    public readonly float Max;
    
    public FloatRange(float min, float max)
    {
        Min = min;
        Max = max;
    }

    public bool Contains(float value)
    {
        return value > Min && value < Max;
    }
    
    public float Length => Max - Min;
    public bool IsInfinite => float.IsInfinity(Min) || float.IsInfinity(Max);
}
