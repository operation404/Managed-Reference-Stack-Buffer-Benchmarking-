// See https://aka.ms/new-console-template for more information
// need to build and run in release mode: dotnet run -c Release

global using static StackallocBenchmarking.StringUtil;

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace StackallocBenchmarking;

public static class StringUtil
{
    public const int RAND_SEED = 0;
    public const int COUNT = 10;
    public const int MIN_ITEMS = 5;
    public static readonly string[] StringArr = new string[COUNT];

    static StringUtil()
    {
        for (int i = 0; i < COUNT; i++)
            StringArr[i] = $"Index: {i} Value: {Random.Shared.NextDouble()}";
    }

    public static void PrintStringSpan(Span<string> span)
    {
        foreach (ref readonly string str in span)
            Console.WriteLine(str);
    }

    public static void CallGC()
    {
        // GC.Collect(4, GCCollectionMode.Forced, true, true);
        // GC.WaitForFullGCComplete();
    }

    public static string GetString(Span<string> span)
    {
        return span.Length > 1 ? span[^1] : null;
    }
}

public class Program
{
    HeapArray _heapArray = new();
    ArrayPooling _arrayPooling = new();
    StackBuffer _stackBuffer = new();
    DangerousStackBuffer _dangerousStackBuffer = new();
    InlineArray _inlineArray = new();

    [Benchmark]
    public void TestHeapArray() => _heapArray.Run();
    [Benchmark]
    public void TestArrayPooling() => _arrayPooling.Run();
    [Benchmark]
    public void TestStackBuffer() => _stackBuffer.Run();
    [Benchmark]
    public void TestDangerousStackBuffer() => _dangerousStackBuffer.Run();
    [Benchmark]
    public void TestInlineArray() => _inlineArray.Run();

    public static void Main(string[] _)
    {
        Console.WriteLine(StringArr[1]); // ensure static string array is initialized
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(["-m", "0"]);
    }
}

