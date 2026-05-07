# Getting Started

## Installation

Clone the repository:

```bash
git clone https://gits-15.sys.kth.se/grudat26/merko-ovn7.git
```

Then include the project in your C# solution.

## Basic complex numbers

```csharp
using CMath;

ComplexF z = new ComplexF(2f, 5f);
ComplexF w = 2f + ComplexF.I * 5f;

float magnitude = MathC.Abs(z);
ComplexF conjugate = z.Conjugate;
```
