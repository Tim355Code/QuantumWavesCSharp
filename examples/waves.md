# Basic wave function operations and calculations

```WaveFunction1D``` provides utility functions for computing properties commonly used in quantum simulations. 

This includes:
- normalization,
- probability in range,
- and spatial sampling.

## Manual normalization
Finding normalization constants is not always feasible, so ```WaveFunction1D``` provides a ```TryNormalize``` function.
```TryNormalize``` attempts to integrate the probability density over the entire domain and adjust the amplitude so the total probability becomes approximately ```1```.

For infinite domains or divergent functions, normalization may fail and the function returns ```false```.
```csharp
using CMath;
using QuantumWaves;

WaveFunction1D wave = new SeparableWaveFunction1D
(
    spacePart: x => MathF.Exp(-x),
    timePart: t => MathC.Exp(ComplexF.I * t),
    domain: new FloatRange(0, float.PositiveInfinity),
    amplitude: 1f
);

if (wave.TryNormalize())
{
    Console.WriteLine(wave.Amplitude); // approx sqrt(2) + 0i
}
```

## Probability in range
```ProbabilityInRange``` integrates the probability density over a specified interval.
This is useful for measuring how much of the wave function lies inside a region of space.
```csharp
using CMath;
using QuantumWaves;

WaveFunction1D wave = new SeparableWaveFunction1D
(
    spacePart: x => 1,
    timePart: t => MathC.Exp(ComplexF.I * t),
    domain: new FloatRange(0f, 4f)
);

wave.TryNormalize();
Console.WriteLine(wave.ProbabilityInRange(t: 0, domain: new FloatRange(-100, 100))); // approx 1
Console.WriteLine(wave.ProbabilityInRange(t: 0, domain: new FloatRange(0, 2f)));     // approx 0.5
```

## Sampling
Spatial sampling is commonly used in visualization and simulation of quantum systems. Since approximating the cumulative distribution function can be relatively expensive, the method allows bulk sampling.
```csharp
using CMath;
using QuantumWaves;

WaveFunction1D wave = new SeparableWaveFunction1D
(
    spacePart: x => MathF.Exp(-x),
    timePart: t => MathC.Exp(ComplexF.I * t),
    domain: new FloatRange(0, float.PositiveInfinity),
    amplitude: MathF.Sqrt(2f)
);

wave.Sample(sampleCount: 10, out float[] samples, t: 0, rng: new Random(Seed: 21));
for(int i = 0; i < 10; i++)
{
    Console.WriteLine(samples[i]); // Depends on seed, in this case between 0.0 and 2.0
}
```

## See also
- [Core concepts](../concepts.md)
- [Built-in solutions examples](built-in.md)
- [API reference](../api/QuantumWaves.yml)
