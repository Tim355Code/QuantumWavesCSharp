using CMath;

ComplexF a = new ComplexF(3, 4);
Console.WriteLine($"a: {a}");
Console.WriteLine($"Magnitude of a: {a.Magnitude}");
Console.WriteLine($"Conjugate of a: {a.Conjugate}");

ComplexF b = ComplexF.I * 2f;

ComplexF c = a * b;
Console.WriteLine($"c: {c}");

