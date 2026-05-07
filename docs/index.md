# Getting Started

QuantumWavesCSharp is a small educational library which provides utilities for complex arithmetic and simple one dimensional quantum wave function calculations in C#.

This guide shows how to install the project, create complex numbers, define a normalized 1D wave function, and verify that its total probability is approximately 1.

## Installation

Clone the repository:
```bash
git clone https://github.com/Tim355Code/QuantumWavesCSharp.git
```
Then include the project in your C# solution.

## Complex arithmetic
```csharp
using CMath;

ComplexF z = new ComplexF(2f, 5f);
ComplexF w = 2f + ComplexF.I * 5f;

float magnitude = MathC.Abs(z);
ComplexF conjugate = z.Conjugate;

Console.WriteLine(conjugate); // 2 - 5i
```

## Create a wave function
A ```WaveFunction1D``` represents a complex valued function over a one dimensional domain.
The wave function is automatically normalized so that the total probability over its domain is approximately ```1```.
```csharp
using CMath;
using QuantumWaves;

WaveFunction1D wave = new SeparableWaveFunction1D
(
    spacePart: x => x,
    timePart: t => MathC.Exp(ComplexF.I * t),
    domain: new FloatRange(0f, 4f)
);
Console.WriteLine(wave.Amplitude); // ~ 0.2165 + 0i = 1 / sqrt(64 / 3)

float total = wave.ProbabilityInRange(t: 0f, domain: new FloatRange(0f, 4f));
Console.WriteLine(total); // ~1
```

## Next steps
- [Read the core concepts](concepts.md)
- [See more examples](examples/index.md)
- [Browse the API reference](../api/QuantumWaves.yml)
