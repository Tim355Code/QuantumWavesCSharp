# QuantumWavesCSharp ![.NET Standard](https://img.shields.io/badge/.NET-Standard%202.1-blue)

A small C# library for working with complex numbers and simple quantum wave functions.
The library is designed to be lightweight, easy to use. It can be used for simulations, numerical experiments, or as a tool for visualization.

## Features
- ```ComplexF``` - a float based complex number type.
- ```MathC``` - provides mathematical operations on complex numbers
- Wave function abstractions (1D)
- Built-in solutions such as infinite potential well
- Support for superposition of states (if time leftover)
- Wave function abstractions (3D) (if time leftover)
- Usage example for visualization (if time leftover)

## Installation

Clone the repository:
```bash
git clone https://gits-15.sys.kth.se/grudat26/merko-ovn7.git
```
Then include the project in your C# solution.

## Documentation
[API Reference](docs/api.md)

## Example
```csharp
using QuantumWaves;

ComplexF z = 2f + ComplexF.I * 3f;

float magnitude = MathC.Abs(z);
ComplexF conjugate = z.Conjugate;

// Example wave function (conceptual)
var psi = new WaveFunction1D(1, (x, t) =>
{
    return MathC.Exp(ComplexF.I * x);
}, FloatRange.Infinite, ComplexF.One);

float probability = psi.ProbabilityDensity(1.0f, 0.0f);
```
