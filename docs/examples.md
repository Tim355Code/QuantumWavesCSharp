# Examples

## Complex arithmetic

```csharp
using CMath;

ComplexF z = 2f + ComplexF.I * 3f;

float magnitude = MathC.Abs(z);
float angle = MathC.Arg(z);
ComplexF unit = z.Normalized;
```

## Creating a wave function

```csharp
using CMath;
using QuantumWaves;

var psi = new SeparableWaveFunction1D(
    FloatRange.Infinite,
    ComplexF.One,
    x => MathC.Exp(ComplexF.I * x),
    t => MathC.Exp(-ComplexF.I * t)
);

float density = psi.ProbabilityDensity(1.0f, 0.0f);
```