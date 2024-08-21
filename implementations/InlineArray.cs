using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace StackallocBenchmarking;

public class InlineArray : Base
{
    [System.Runtime.CompilerServices.InlineArray(COUNT)]
    public struct Buffer100<T>
    {
        private T _element0;
    }

    public unsafe void Run()
    {
        int count = GetCount();

        Buffer100<string> structBuffer = new();
        Span<string> strings = MemoryMarshal.CreateSpan(
            ref Unsafe.As<Buffer100<string>, string>(ref structBuffer), count);

        FillSpan(strings);
        GetString(strings);

        CallGC();

        GetString(strings);
    }
}