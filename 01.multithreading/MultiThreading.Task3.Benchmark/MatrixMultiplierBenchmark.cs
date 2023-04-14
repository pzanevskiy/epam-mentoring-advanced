using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MultiThreading.Task3.MatrixMultiplier.Matrices;
using MultiThreading.Task3.MatrixMultiplier.Multipliers;
using System.Drawing;

namespace MultiThreading.Task3.Benchmark;

public class MatrixMultiplierBenchmark
{
    [Params(10, 100, 200)]
    public long _size;

    //private static readonly IMatrix _m1 = new Matrix(_size, _size, true);
    //private static readonly IMatrix _m2 = new Matrix(_size, _size, true);
    private static readonly IMatricesMultiplier _matricesMultiplier = new MatricesMultiplier();
    private static readonly IMatricesMultiplier _matricesMultiplierParallel = new MatricesMultiplierParallel();

    [Benchmark]
    public void MultiplyMatrix3On3Test()
    {
        IMatrix _m1 = new Matrix(_size, _size, true);
        IMatrix _m2 = new Matrix(_size, _size, true);
        _matricesMultiplier.Multiply(_m1, _m2);
    }

    [Benchmark]
    public void MultiplyMatrix3On3TestParallel()
    {
        IMatrix _m1 = new Matrix(_size, _size, true);
        IMatrix _m2 = new Matrix(_size, _size, true);
        _matricesMultiplierParallel.Multiply(_m1, _m2);
    }
}