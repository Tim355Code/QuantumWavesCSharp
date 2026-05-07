# QuantumWavesCSharp ![.NET Standard](https://img.shields.io/badge/.NET-Standard%202.1-blue)

A small C# library for working with complex numbers and simple quantum wave functions.
The library is designed to be lightweight and easy to use. It can be used for simulations, numerical experiments, or as a tool for visualization.

## Features
- ```ComplexF``` - a float based complex number type.
- ```MathC``` - provides mathematical operations on complex numbers
- Wave function abstractions (1D)
- Built-in solutions such as:
   - infinite potential well,
   - harmonic oscillator,
   - Gaussian wave packet
     
## Installation
Clone the repository:
```bash
git clone https://github.com/Tim355Code/QuantumWavesCSharp.git
```
Then include the project in your C# solution.

---
## Documentation
- [Getting started](https://tim355code.github.io/QuantumWavesCSharp/index.html)
- [Core concepts](https://tim355code.github.io/QuantumWavesCSharp/concepts.html)
- [Examples](https://tim355code.github.io/QuantumWavesCSharp/examples)
- [API Reference](https://tim355code.github.io/QuantumWavesCSharp/api/QuantumWaves.html)

## Example
```csharp
using QuantumWaves;
using CMath;

ComplexF z = 2f + ComplexF.I * 3f;

float magnitude = MathC.Abs(z);
ComplexF conjugate = z.Conjugate;

// Example wave function
var psi = new SimpleWaveFunction1D((x, t) =>
{
    return MathC.Exp(ComplexF.I * x);
}, FloatRange.Infinite, ComplexF.One); // defined over (-∞, ∞)

float probability = psi.ProbabilityDensity(1.0f, 0.0f);
```
See the rest [here](docs/examples/index.md)

## Running tests
```bash
cd src
dotnet test
```
## Roadmap
- Three dimensional wave function abstractions
- Unity visualization examples
