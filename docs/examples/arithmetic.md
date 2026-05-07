# Complex Arithmetic and Math Utilities

```MathC``` provides utility functions for working with complex numbers and floating point mathematics commonly used in quantum simulations. 

This includes:
- exponential functions,
- logarithms,
- polar conversion,
- trigonometric operations,
- and other complex-valued arithmetic helpers.

## Basic arithmetic
```csharp
using CMath;

ComplexF a = new ComplexF(2, 1);
ComplexF b = new ComplexF(1, 3);

Console.WriteLine(a * b); // 7i - 1
Console.WriteLine(a / b); // 0.5 - 0.5i

ComplexF c = a + b;

Console.WriteLine(MathC.AbsSqr(c)); // 25
Console.WriteLine(MathC.Sqrt(c)); // 2 + i
Console.WriteLine(MathC.Pow(c, 2)); // 24i - 7
```

## Polar and Exponential Forms
Polar representations are useful for rotations, phase calculations, and exponential forms of complex numbers.
```csharp
using CMath;

ComplexF z = new ComplexF(1, 1);

Console.WriteLine(MathC.Arg(z)); // approx π/4
Console.WriteLine(MathC.ToPolar(z)); // approx (√2, π/4)

Console.WriteLine(MathC.Cis(MathF.PI/2)); // approx i
Console.WriteLine(MathC.FromPolar(2, MathF.PI)); // approx -2
```

## Interpolation
Interpolation utilities are helpful for smoothly transitioning between amplitudes, phases, or sampled states, especially in visualization.
```csharp
using CMath;

ComplexF a = ComplexF.Zero;
ComplexF b = new ComplexF(1, 2);

Console.WriteLine(MathC.Lerp(a, b, 0.5f)); // 0.5 + i
```

## See also
- [Core concepts](../concepts.md)
- [Wave function examples](waves.md)
- [API reference](../../api/QuantumWaves.yml)
