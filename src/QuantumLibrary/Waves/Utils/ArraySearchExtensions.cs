namespace QuantumWaves.Utils
{
    /// <summary>Provides binary search helpers for arrays.</summary>
    public static class ArraySearchExtensions
    {
        /// <summary> Returns the index of the first value greater than or equal to the <paramref name="target"/>.</summary>
        /// <param name="values">The sorted array to search.</param>
        /// <param name="target">The value to compare to.</param>
        /// <returns>
        /// The index of the first element greater than or equal to <paramref name="target"/>.
        /// Returns <c>values.Length</c> if no match is found.
        /// </returns>
        public static int LowerBound(this float[] values, float target)
        {
            int low = 0;
            int high = values.Length;

            while (low < high)
            {
                int mid = low + ((high - low) >> 1);

                if (values[mid] < target)
                    low = mid + 1;
                else
                    high = mid;
            }

            return low;
        }
    }
}
