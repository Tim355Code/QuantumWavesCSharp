using QuantumWaves;
using QuantumWaves.Solutions;

WaveFunction1D infiniteWell = HarmonicOscillator1D.State(3, 1f, 1f);
Console.WriteLine($"True normalization constant: {infiniteWell.Amplitude}");

Console.WriteLine(infiniteWell.TryNormalize());
Console.WriteLine($"Approx normalization constant: {infiniteWell.Amplitude}");
