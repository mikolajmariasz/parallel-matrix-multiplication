# Parallel Matrix Multiplication Benchmark

## Overview
This project benchmarks two parallel implementations of matrix multiplication in C#:
- **Parallel.For** (Task Parallel Library)
- **Thread-based** (`System.Threading.Thread`)

### Key Components:
- `Matrix.cs`: Matrix operations and storage
- `ParallelMultiplier.cs`: TPL implementation using `Parallel.For`
- `ThreadMultiplier.cs`: Manual thread management implementation
- `Program.cs`: Benchmark script
