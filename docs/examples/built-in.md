# Built-in solutions
`QuantumWaves.Solutions` contains several analytical solutions to common one dimensional quantum systems.

Solutions include:
- Infinite well,
- Harmonic Oscillator,
- Free particle plane waves,
- and the Gaussian packet.

## Infinite well
The infinite potential well is one of the simplest analytical solutions of the Schrödinger equation. The eigenstates are discrete standing waves.
```csharp
using QuantumWaves;
using QuantumWaves.Solutions;

SeparableWaveFunction1D wave = InfiniteWell1D.State(n: 2, length: 1f, mass: 1f);
Console.WriteLine(wave.Amplitude); // approx sqrt(2) + 0i

float energy = InfiniteWell1D.Energy(n: 1, length: MathF.PI, mass: 1, hBar: 2);
Console.WriteLine(energy); // 2
```

## Harmonic Oscillator
The harmonic oscillator is a fundamental quantum system with evenly spaced energy levels and Hermite polynomial eigenstates.
```csharp
using QuantumWaves;
using QuantumWaves.Solutions;

SeparableWaveFunction1D wave = HarmonicOscillator1D.State(n: 1, mass: MathF.PI, omega: 1);
Console.WriteLine(wave.Amplitude); // approx 1 / sqrt(2) + 0i

float energy = HarmonicOscillator1D.Energy(n: 5, omega: 2, hBar: 1);
Console.WriteLine(energy); // 11
```

## Gaussian packet
The Gaussian packet is approximated numerically using a weighted superposition of momentum eigenstates.
```csharp
using QuantumWaves;
using QuantumWaves.Solutions;

WeightedWaveFunction1D wave = FreeParticle1D.GaussianWavePacket(
    x0: 0,
    sigma: 1f / MathF.Sqrt(2 * MathF.PI),
    k0: 1,
    mass: 1,
    hBar: 1,
    termCount: 199
);

Console.WriteLine(wave.ProbabilityDensity(1, 0)); // approx e^-π

// This takes a while to run!!
if(wave.Sample(sampleCount: 10000, out float[] samples, t: 0, rng: new Random(Seed: 42), pointCount: 1500,
    tolerance: 1e-2f, segmentIntervals: 8))
{
    Console.WriteLine("Average: " + samples.Average()); // approx 0
}
```

## See also
- [Core concepts](../concepts.md)
- [Wave function examples](waves.md)
- [API reference](../../api/QuantumWaves.yml)
