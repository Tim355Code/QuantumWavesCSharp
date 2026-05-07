# Concepts

## Complex numbers

`ComplexF` represents a complex number using two `float` values: a real component and an imaginary component.

```csharp
ComplexF z = new ComplexF(2f, 5f);
```

The value represents:

```text
z = real + imaginary * i
```

## Polar form

Complex numbers can also be represented using a radius and angle:

```csharp
(float radius, float angle) = MathC.ToPolar(z);
ComplexF restored = MathC.FromPolar(radius, angle);
```

## Wave functions

`WaveFunction1D` represents a function of position and time:

```text
ψ(x, t)
```

The probability density is:

```text
|ψ(x, t)|²
```

## Superposition

`WeightedWaveFunction1D` represents a weighted sum of wave functions.
