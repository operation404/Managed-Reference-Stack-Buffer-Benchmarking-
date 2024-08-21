using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace StackallocBenchmarking;

public class StackBuffer : Base
{
    public unsafe void Run()
    {
        int count = GetCount();

        IntPtr* ptr = stackalloc IntPtr[count];
        ref string tRef = ref Unsafe.AsRef<string>(ptr);
        Span<string> strings = MemoryMarshal.CreateSpan(ref tRef, count);

        Span<GCHandle> handles = stackalloc GCHandle[count];
        for (int i = 0; i < count; i++)
        {
            handles[i] = GCHandle.Alloc(strings[i], GCHandleType.Pinned);
        }

        FillSpan(strings);
        GetString(strings);

        CallGC();

        GetString(strings);

        for (int i = 0; i < count; i++)
        {
            handles[i].Free();
        }
    }
}