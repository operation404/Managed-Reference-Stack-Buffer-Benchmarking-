using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace StackallocBenchmarking;

public class DangerousStackBuffer : Base
{
    public unsafe void Run()
    {
        int count = GetCount();

        IntPtr* ptr = stackalloc IntPtr[count];
        ref string tRef = ref Unsafe.AsRef<string>(ptr);
        Span<string> strings = MemoryMarshal.CreateSpan(ref tRef, count);

        FillSpan(strings);
        GetString(strings);

        CallGC();

        GetString(strings);
    }
}