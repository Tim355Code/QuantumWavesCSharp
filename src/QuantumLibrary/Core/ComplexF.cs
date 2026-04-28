using System;

namespace CMath
{
    /// <summary>
    /// Represents a mutable complex number using floating-point components.
    /// </summary>
    public struct ComplexF : IEquatable<ComplexF>
    {
        /// <summary> Represents a complex number with zero components.</summary>
        public static readonly ComplexF Zero = new ComplexF(0, 0);
        /// <summary> Represents a complex number with unity real part and zero imaginary part.</summary>
        public static readonly ComplexF One = new ComplexF(1, 0);
        /// <summary> Represents a complex number with unity imaginary part and zero real part.</summary>
        public static readonly ComplexF I = new ComplexF(0, 1);

        /// Represents a complex number whose components are <see cref="float.NaN"/>.
        public static readonly ComplexF NaN = new ComplexF(float.NaN, float.NaN);

        /// <summary>
        /// Represents a complex number with a real component of positive infinity and zero imaginary part.
        /// </summary>
        public static readonly ComplexF PositiveRealInfinity = new ComplexF(float.PositiveInfinity, 0f);
        /// <summary>
        /// Represents a complex number with a real component of negative infinity and zero imaginary part.
        /// </summary>
        public static readonly ComplexF NegativeRealInfinity = new ComplexF(float.NegativeInfinity, 0f);
        /// <summary>
        /// Represents a complex number with an imaginary component of positive infinity and zero real part.
        /// </summary>
        public static readonly ComplexF PositiveImaginaryInfinity = new ComplexF(0f, float.PositiveInfinity);
        /// <summary>
        /// Represents a complex number with an imaginary component of negative infinity and zero real part.
        /// </summary>
        public static readonly ComplexF NegativeImaginaryInfinity = new ComplexF(0f, float.NegativeInfinity);
            
        /// <summary> Gets or sets the real component.</summary>
        public float Real { get; set; }
        /// <summary> Gets or sets the imaginary component.</summary>
        public float Imaginary { get; set; }

        /// <summary> Gets whether either component is <see cref="float.NaN"/>.</summary>
        public readonly bool IsNaN => float.IsNaN(Real) || float.IsNaN(Imaginary);
        /// <summary> Gets whether either component is infinite.</summary>
        public readonly bool IsInfinity => float.IsInfinity(Real) || float.IsInfinity(Imaginary);
        /// <summary> Gets whether both components are finite.</summary>
        public readonly bool IsFinite => float.IsFinite(Real) && float.IsFinite(Imaginary);

        /// <summary> Gets the magnitude of the complex number.</summary>
        /// <remarks> Computed as sqrt(Re^2 + Im^2). May overflow for large values.</remarks>
        public readonly float Magnitude => MathF.Sqrt(Real * Real + Imaginary * Imaginary);
        /// <summary> Gets the squared magnitude of the complex number.</summary>
        public readonly float MagnitudeSqr => Real * Real + Imaginary * Imaginary;

        /// <summary> Gets the complex conjugate. </summary>
        /// <returns> A new complex number with a negated imaginary part.</returns>
        public readonly ComplexF Conjugate => new ComplexF(Real, -Imaginary);

        /// <summary> Gets a normalized version of the complex number.</summary>
        /// <returns> The complex number divided by its own magnitude, NaN if magnitude is zero.</returns>
        public readonly ComplexF Normalized
        {
            get
            {
                float mag = Magnitude;
                if (mag == 0)
                    return NaN;
                return new ComplexF(Real / mag, Imaginary / mag);
            }
        }

        /// <summary> Creates a complex number.</summary>
        /// <param name="real">The real component.</param>
        /// <param name="imaginary">The imaginary component.</param>
        public ComplexF(float real, float imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        /// <summary>Returns a string that represents the complex number.</summary>
        /// <remarks>
        /// The string is formatted as "a + bi" or "a - bi".
        /// Special floating-point values (NaN, Infinity) use their default string representations.
        /// </remarks>
        public override readonly string ToString()
        {
            if (float.IsNaN(Imaginary))
                return $"{Real} + NaNi";

            if (MathF.Sign(Imaginary) < 0)
                return $"{Real} - {-Imaginary}i";

            return $"{Real} + {Imaginary}i";
        }

        /// <summary>
        /// Determines whether the specified object instance is equal to the complex number.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>Only true if both are component-wise equal complex numbers.</returns>
        public override readonly bool Equals(object? obj)
        {
            if (obj is ComplexF other)
            {
                return Real == other.Real && Imaginary == other.Imaginary;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified complex number is equal to the current complex number.
        /// </summary>
        /// <param name="other"> The other complex number to compare.</param>
        /// <returns> Component-wise equality.</returns>
        public readonly bool Equals(ComplexF other)
        {
            return Real == other.Real && Imaginary == other.Imaginary;
        }

        /// <summary> Combines the hash code of the real and imaginary components. </summary>
        /// <returns> A hash code for the current complex number.</returns>
        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Real, Imaginary);
        }

        /// <summary>
        /// Implicitly converts a real number to a complex number: value + 0*i.
        /// </summary>
        public static implicit operator ComplexF(float value)
            => new ComplexF(value, 0);

        public static ComplexF operator +(ComplexF a, ComplexF b)
            => new ComplexF(a.Real + b.Real, a.Imaginary + b.Imaginary);
        public static ComplexF operator -(ComplexF a, ComplexF b)
            => new ComplexF(a.Real - b.Real, a.Imaginary - b.Imaginary);
        public static ComplexF operator *(ComplexF a, ComplexF b)
            => new ComplexF(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
        
        /// <summary> Divides one complex number by another.</summary>
        /// <remarks>
        /// Returns <see cref="NaN"/> if the divisor is zero or if either operand contains <see cref="float.NaN"/>.
        /// </remarks>
        public static ComplexF operator /(ComplexF a, ComplexF b)
        {
            if (a.IsNaN || b.IsNaN)
                return NaN;

            if (b.Real == 0f && b.Imaginary == 0f)
                return NaN;

            float denom = b.MagnitudeSqr;

            return new ComplexF(
                (a.Real * b.Real + a.Imaginary * b.Imaginary) / denom,
                (a.Imaginary * b.Real - a.Real * b.Imaginary) / denom
            );
        }

        public static ComplexF operator -(ComplexF z)
            => new ComplexF(-z.Real, -z.Imaginary);

        /// <summary> Determines whether two complex numbers are equal.</summary>
        /// <remarks>
        /// This uses exact component comparison. If either component is <see cref="float.NaN"/>,
        /// the comparison returns false.
        /// </remarks>
        public static bool operator ==(ComplexF a, ComplexF b)
            => a.Real == b.Real && a.Imaginary == b.Imaginary;
        public static bool operator !=(ComplexF a, ComplexF b)
            => a.Real != b.Real || a.Imaginary != b.Imaginary;
    }
}