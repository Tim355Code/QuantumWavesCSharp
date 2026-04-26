# API Reference

## Core Types

### ComplexF
```ComplexF``` is an immutable struct representing a complex number using two ```float``` values:
one real component and one imaginary component.
```csharp
ComplexF z = new ComplexF(2f, 5f);
ComplexF w = 2f + ComplexF.I * 5f;
```
#### Constructors
```csharp
public ComplexF(float real, float imaginary);
```
Creates a complex number: ```real + i * imaginary```.

#### Properties
```csharp
public float Real { get; }
public float Imaginary { get; }
```
Returns the real and imaginary components.
```csharp
public float Magnitude { get; }
```
Returns the magnitude of ```z```: ```Abs(a + bi) = sqrt(a^2 + b^2)
```csharp
public float MagnitudeSqr { get; }
```
Returns the magnitude squared.
```csharp
public ComplexF Conjugate { get; }
```
Returns the complex conjugate: ```Conjugate(a + bi) = a - bi```
```csharp
public ComplexF Normalized { get; }
```
Returns the complex number divided by its own magnitude. Returns NaN if magnitude is zero.
```csharp
public override string ToString();
```
Returns a string representation of the complex number: ```"a + bi"```
```csharp
public override bool Equals(object? obj);
```
Determines whether two object instances are equal.
```csharp
public override int GetHashCode();
```
A hash code for the current object.
```csharp
public bool IsNaN { get; }
```
Returns whether the current instance is NaN.
```csharp
public bool IsInfinity { get; }
```
Returns whether any of the components are an infinity.

#### Constants
```csharp
public static readonly ComplexF Zero;
public static readonly ComplexF One;
public static readonly ComplexF I;
public static readonly ComplexF NaN;
public static readonly ComplexF Infinity;
```
Common complex constants.

#### Operators
```csharp
public static implicit operator ComplexF(float value);
```
Implicitly converts a real number to a complex number: ```value + 0*i```
```csharp
public static ComplexF operator +(ComplexF a, ComplexF b);
public static ComplexF operator -(ComplexF a, ComplexF b);
public static ComplexF operator *(ComplexF a, ComplexF b);
public static ComplexF operator /(ComplexF a, ComplexF b);
```
Standard complex arithmetic.
```csharp
public static ComplexF operator -(ComplexF z);
```
Unary negation.
```csharp
public static bool operator ==(ComplexF a, ComplexF b);
public static bool operator !=(ComplexF a, ComplexF b);
```
Exact component-wise equality.

### MathC
```MathC``` contains utility functions for complex numbers.

#### Abs
```csharp
public static float Abs(ComplexF a)
```
Returns the magnitude.

#### AbsSqr
```csharp
public static float AbsSqr(ComplexF a)
```
Returns the magnitude squared.

#### Approximately
```csharp
public static bool Approximately(ComplexF a, ComplexF b, float tolerance = 1e-5f)
```
Compares two complex numvers and returns true if they are similar. Tolerance marks the maximal allowed magnitude in deviation. If either has ```NaN``` or ```Infinity``` components, returns ```false```.

#### Arg
```csharp
public static float Arg(ComplexF z)
```
Returns the angle of ```z``` in the complex plane in range [-π, π]. ```arg(0)``` return ```NaN```.

#### ToPolar
```csharp
public static (float radius, float angle) ToPolar(ComplexF z)
```
Returns polar coordinates ```(r, θ)``` such that: ```z = r * e^iθ``` with θ in range [-π, π].  ```ToPolar(0)``` return ```(0, NaN)```.

#### FromPolar
```csharp
public static ComplexF FromPolar(float radius, float angle)
```
Creates a complex number from polar coordinates. ```FromPolar(r, θ) = r(cosθ + i*sin(θ))```.

#### Exp
```csharp
public static ComplexF Exp(ComplexF z)
```
Computes ```e^z```.

#### Pow
```csharp
public static ComplexF Pow(ComplexF baseValue, ComplexF exponent)
```
Computes complex exponentiation.

#### Sqrt
```csharp
public static ComplexF Sqrt(ComplexF baseValue)
```
Computes complex square root.

#### Ln
```csharp
public static ComplexF Ln(ComplexF z)
```
Computes complex natural logarithm. ```Ln(0)``` returns ```(-Infinity, NaN)```.

#### Log
```csharp
public static ComplexF Log(ComplexF z, float base)
```
Computes complex logarithm in the base b: ```log_b(z) = ln(z)/ln(b)```

#### Sin
```csharp
public static ComplexF Sin(ComplexF z)
```
Computes complex sine: ```sin(z) = (e^iz - e^-iz)/2i```

#### Cos
```csharp
public static ComplexF Cos(ComplexF z)
```
Computes complex cosine: ```cos(z) = (e^iz + e^-iz)/2```

#### Tan
```csharp
public static ComplexF Tan(ComplexF z)
```
Computes complex tangent: ```tan(z) = sin(z)/cos(z)```

#### Cis
```csharp
public static ComplexF Cis(float angle)
```
Returns ```cis(angle) = cos(angle) + isin(angle)```.

#### Lerp
```csharp
public static ComplexF Lerp(ComplexF a, ComplexF b, float t)
```
Performs a component-wise linear interpolation between two complex numbers based on the given weight.

## FloatRange
```csharp
public readonly struct FloatRange
{
    public static readonly FloatRange Infinite;
    
    public float Min { get; }
    public float Max { get; }
    
    public FloatRange(float min, float max);
    
    public bool Contains(float value);
    
    public float Length { get; }
    public bool IsInfinite { get; }
}
```
Represents a one dimensional domain.

## Wave Functions

### WaveFunction1D
```csharp
public class WaveFunction1D
{
    private readonly Func<float, float, ComplexF> _evaluateRaw;

    public WaveFunction1D(Func<float, float, ComplexF> wave, FloatRange domain, ComplexF normalizationConstant);
    
    public FloatRange Domain { get; }
    public ComplexF NormalizationConstant { get; set; }

    /// <summary>
    /// Evaluates the wave function ψ(x, t)
    /// </summary>
    public ComplexF Evaluate(float x, float t) => Domain.Contains(x) ?
        (NormalizationConstant * _evaluateRaw(x, t)) : 0;

    /// <summary>
    /// Returns |ψ(x,t)|^2
    /// </summary>
    public float ProbabilityDensity(float x, float t);
}
```

### SeparableWaveFunction1D
``` csharp
public sealed class SeparableWaveFunction1D : WaveFunction1D
{
    public SeparableWaveFunction1D(
        FloatRange domain,
        Func<float, ComplexF> spacePart,
        Func<float, ComplexF> timePart
    );

    public Func<float, ComplexF> SpacePart { get; }
    public Func<float, ComplexF> TimePart { get; }

    protected override ComplexF EvaluateRaw(float x, float t) => SpacePart(x) * TimePart(t);
}
```
Represents ```ψ(x,t) = φ(x) · T(t)```

### WeightedWaveFunction1D
```csharp
public readonly struct WeightedWaveFunction1D
{
    public ComplexF Coefficient { get; }
    public WaveFunction1D WaveFunction { get; }

    public WeightedWaveFunction1D(ComplexF coefficient, WaveFunction1D waveFunction);
}
```
Used for superpositions.

### Built-in Systems
All equations are by default expressed in natural units where ```ℏ = 1```. This may be changed by the user however.

#### InfiniteWell
```csharp
public static class InfiniteWell
{
    /// <summary>
    /// Returns the nth stationary state in a 1D infinite potential well in the domain [0, L].
    /// </summary>
    public static WaveFunction1D State(
        int n,
        float length,
        float mass,
        float h_bar = 1
    );
}
```
#### HarmonicOscillator
```csharp
public static class HarmonicOscillator
{
    /// <summary>
    /// Returns the nth stationary state of the harmonic oscillator.
    /// </summary>
    public static WaveFunction1D State(
        int n,
        float mass,
        float omega,
        float h_bar = 1
    );
}
```
### WaveUtils1D
```WaveUtils1D``` contains utility functions for one-dimensional wave functions.

#### Superpose
```csharp
public static WaveFunction1D Superpose(params WeightedWaveFunction1D[] terms);
```
Creates a superposition of wave functions. Note: assumed shared domain. If this is not the case, the functions may not be normalized.

#### Probability
```csharp
public static float Probability(WaveFunction1D wave, float t, FloatRange range, int steps);
```
Approximates the probability of finding a particle in \[min, max\].

#### SamplePositions
```csharp
public static float[] SamplePositions(WaveFunction1D wave, float t, int count, int resolution, Random? rng = null);
```
Samples positions according to ```|ψ(x,t)|^2```
