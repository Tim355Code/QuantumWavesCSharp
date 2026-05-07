using System;

namespace CMath
{
    /// <summary>
    /// A collection of common complex math functions.
    /// </summary>
    public static class MathC
    {
        /// <summary>Returns the magnitude of the specified complex number.</summary>
        /// <param name="z">The complex number.</param>
        /// <returns>The magnitude of <paramref name="z"/>.</returns>
        public static float Abs(ComplexF z)
        {
            return MathF.Sqrt(z.Real * z.Real + z.Imaginary * z.Imaginary);
        }

        /// <summary>Returns the squared magnitude of the specified complex number.</summary>
        /// <param name="z">The complex number.</param>
        /// <returns>The squared magnitude of <paramref name="z"/>.</returns>
        public static float AbsSqr(ComplexF z)
        {
            return z.Real * z.Real + z.Imaginary * z.Imaginary;
        }

        /// <summary>Returns whether two complex numbers are within the specified tolerance.</summary>
        /// <param name="a">The first complex number.</param>
        /// <param name="b">The second complex number.</param>
        /// <param name="tolerance">The maximum allowed distance between the two values.</param>
        /// <returns>
        /// <see langword="true"/> if the magnitude of <paramref name="a"/> - <paramref name="b"/>
        /// is less than or equal to <paramref name="tolerance"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool Approximately(ComplexF a, ComplexF b, float tolerance = 1e-5f)
        {
            if (tolerance <= 0)
                throw new ArgumentOutOfRangeException(nameof(tolerance), "Tolerance must be greater than zero.");

            ComplexF diff = a - b;
            return diff.MagnitudeSqr <= tolerance * tolerance;
        }

        /// <summary>Returns the argument of the specified complex number.</summary>
        /// <param name="z">The complex number.</param>
        /// <returns>The phase angle of <paramref name="z"/>, in radians.</returns>
        public static float Arg(ComplexF z)
        {
            return MathF.Atan2(z.Imaginary, z.Real);
        }

        /// <summary>Returns cos(angle) + i sin(angle).</summary>
        /// <param name="angle">The angle, in radians.</param>
        /// <returns>A complex number on the unit circle with the specified argument.</returns>
        public static ComplexF Cis(float angle)
        {
            return new ComplexF(MathF.Cos(angle), MathF.Sin(angle));
        }

        /// <summary>Returns the polar representation of a complex number.</summary>
        /// <param name="z">The complex number.</param>
        /// <returns>A tuple containing the magnitude and phase angle, in radians.</returns>
        public static (float radius, float angle) ToPolar(ComplexF z)
        {
            return (z.Magnitude, Arg(z));
        }

        /// <summary>Returns a complex number based on its polar form.</summary>
        /// <param name="radius">The distance from the origin.</param>
        /// <param name="angle">The phase angle, in radians.</param>
        /// <returns>The complex number represented by the specified polar coordinates.</returns>
        public static ComplexF FromPolar(float radius, float angle)
        {
            return radius * Cis(angle);
        }

        /// <summary>Returns e raised to the specified complex power.</summary>
        /// <param name="z">The complex exponent.</param>
        /// <returns>The value of e raised to <paramref name="z"/>.</returns>
        public static ComplexF Exp(ComplexF z)
        {
            return MathF.Exp(z.Real) * Cis(z.Imaginary);
        }

        /// <summary>Returns the result of raising a complex number to a complex power.</summary>
        /// <param name="baseValue">The base value.</param>
        /// <param name="exponent">The exponent.</param>
        /// <returns>The result of the exponentiation operation.</returns>
        /// <remarks>Uses the principal value of the complex logarithm.</remarks>
        public static ComplexF Pow(ComplexF baseValue, ComplexF exponent)
        {
            return Exp(exponent * Ln(baseValue));
        }

        /// <summary>Returns the natural logarithm of the specified complex number.</summary>
        /// <param name="z">The complex number.</param>
        /// <returns>The principal natural logarithm of <paramref name="z"/>.</returns>
        /// <remarks>Returns the principal value of the complex logarithm.</remarks>
        public static ComplexF Ln(ComplexF z)
        {
            return new ComplexF(MathF.Log(Abs(z)), Arg(z));
        }

        /// <summary>Returns the logarithm of a specified complex value in a specified base.</summary>
        /// <param name="value">The complex value whose logarithm is calculated.</param>
        /// <param name="baseValue">The base of the logarithm.</param>
        /// <returns>The logarithm of <paramref name="value"/> in base <paramref name="baseValue"/>.</returns>
        /// <remarks>
        /// Equivalent to Ln(value) / Ln(baseValue). Uses the principal value of the complex logarithm.
        /// </remarks>
        public static ComplexF Log(ComplexF value, ComplexF baseValue)
        {
            return Ln(value) / Ln(baseValue);
        }

        /// <summary>Returns the principal square root of the specified complex number.</summary>
        /// <param name="z">The complex number.</param>
        /// <returns>The principal value whose square is equal to <paramref name="z"/>.</returns>
        public static ComplexF Sqrt(ComplexF z)
        {
            (float radius, float angle) = ToPolar(z);
            return FromPolar(MathF.Sqrt(radius), angle / 2f);
        }

        /// <summary>Returns the sine of the specified complex number.</summary>
        /// <param name="z">The complex number.</param>
        /// <returns>The sine of <paramref name="z"/>.</returns>
        public static ComplexF Sin(ComplexF z)
        {
            return (Exp(z * ComplexF.I) - Exp(-z * ComplexF.I)) / (2 * ComplexF.I);
        }

        /// <summary>Returns the cosine of the specified complex number.</summary>
        /// <param name="z">The complex number.</param>
        /// <returns>The cosine of <paramref name="z"/>.</returns>
        public static ComplexF Cos(ComplexF z)
        {
            return (Exp(z * ComplexF.I) + Exp(-z * ComplexF.I)) / 2f;
        }

        /// <summary>Returns the tangent of the specified complex number.</summary>
        /// <param name="z">The complex number.</param>
        /// <returns>The tangent of <paramref name="z"/>.</returns>
        /// <remarks> If the cosine of z has zero components returns <see cref="ComplexF.NaN"/>.</remarks>
        public static ComplexF Tan(ComplexF z)
        {
            return Sin(z) / Cos(z);
        }

        /// <summary>
        /// Clamps the given value component-wise between the given minimum and maximum values.
        /// </summary>
        /// <param name="value">The complex value to clamp.</param>
        /// <param name="min">The minimum component values.</param>
        /// <param name="max">The maximum component values.</param>
        /// <returns>The component-wise clamped complex value.</returns>
        /// <remarks>
        /// The real and imaginary components are clamped independently.
        /// </remarks>
        public static ComplexF Clamp(ComplexF value, ComplexF min, ComplexF max)
        {
            return new ComplexF(
                MathF.Min(max.Real, MathF.Max(value.Real, min.Real)),
                MathF.Min(max.Imaginary, MathF.Max(value.Imaginary, min.Imaginary)));
        }

        /// <summary>Clamps the value component-wise between 0 and 1.</summary>
        /// <param name="value">The complex value to clamp.</param>
        /// <returns>The component-wise clamped complex value.</returns>
        /// <remarks>
        /// The real and imaginary components are clamped independently.
        /// </remarks>
        public static ComplexF Clamp01(ComplexF value)
        {
            return new ComplexF(
                MathF.Min(1, MathF.Max(value.Real, 0)),
                MathF.Min(1, MathF.Max(value.Imaginary, 0)));
        }

        /// <summary>
        /// Linearly interpolates component-wise between two complex numbers without clamping the interpolation factor.
        /// </summary>
        /// <param name="a">The start value.</param>
        /// <param name="b">The end value.</param>
        /// <param name="t">The interpolation factor.</param>
        /// <returns>The component-wise interpolated complex value.</returns>
        public static ComplexF LerpUnclamped(ComplexF a, ComplexF b, float t)
        {
            return new ComplexF(
                a.Real + t * (b.Real - a.Real),
                a.Imaginary + t * (b.Imaginary - a.Imaginary));
        }

        /// <summary>
        /// Linearly interpolates component-wise between two complex numbers.
        /// </summary>
        /// <param name="a">The start value.</param>
        /// <param name="b">The end value.</param>
        /// <param name="t">The interpolation factor.</param>
        /// <returns>The component-wise interpolated complex value.</returns>
        /// <remarks>
        /// The interpolation factor <paramref name="t"/> is clamped to the range [0, 1].
        /// </remarks>
        public static ComplexF Lerp(ComplexF a, ComplexF b, float t)
        {
            float clamped = MathF.Max(0f, MathF.Min(t, 1f));
            return LerpUnclamped(a, b, clamped);
        }
    }
}
