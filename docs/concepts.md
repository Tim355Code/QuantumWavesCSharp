# Core Concepts

This library provides mathematical abstractions commonly used in one-dimensional quantum mechanics simulations.
At its core are:
- complex-valued arithmetic,
- wave function representations,
- normalization utilities,
- probability sampling tools,
- and built-in analytical solutions.

## Complex numbers
Quantum wave functions are complex valued and therefore require complex arithmetic for representation and evaluation.
Mathematically, complex numbers are made using the imaginary unit:
```i = sqrt(-1)```

`ComplexF` represents a complex number using two `float` values: a real component and an imaginary component.
```csharp
ComplexF z = new ComplexF(2f, 5f);
```
The value represents:
```z = real + imaginary * i```

### Polar form
Complex numbers can also be represented using a radius and angle:
```csharp
(float radius, float angle) = MathC.ToPolar(z);
ComplexF restored = MathC.FromPolar(radius, angle);
```
This representation is usually referred to as polar form or Euler form.

Using complex valued functions, we can represent physical particle states.

## Wave Functions
In quantum mechanics, the state of a particle is represented by a wave function.
```WaveFunction1D``` models a one dimensional wave function dependent on position and time:
```ψ(x, t)```

The probability density is:
```|ψ(x, t)|²```

### Separable Wave Function
Many analytical solutions to the Schrödinger equation can be split into independent spatial and time dependent components.

```SeparableWaveFunction1D``` represents a wave function by storing the two components separately.

### Weighted Wave Function
Systems rarely exist in pure eigenstates, and being able to write a wave function as a sum of eigenstates is very important.
```WeightedWaveFunction1D``` represents a wave function as a linear combination of other states, typically eigenstates.

## Normalization
A key aspect of the wave function is that its absolute square is equal to the probability density of finding a particle there.
The integral of the probability density over all space must equal one.

To help with this ```WaveFunction1D```  contains an ```Amplitude``` field, effectively used for normalization.
If it is not passed in the constructor explicitly the function will attempt to normalize itself. Simpson integration is used for this purpose.

## Sampling
Sampling allows probabilistic measurement of particle positions from the wave function.
```WaveFunction1D``` includes a built in sampler which approximates the cumulative density function (CDF) and randomly selects positions.

An example of sampling can be found [here](examples/waves.md#sampling).

## Built-in Solutions
To simplify testing, the library includes several analytical one dimensional quantum systems:
- Infinite well
- Harmonic oscillator
- Free particle plane wave
- Gaussian packet

These implementations can be used directly or serve as building blocks for more advanced simulations.
[See example usage.](examples/built-in.md)