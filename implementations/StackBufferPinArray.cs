using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace StackallocBenchmarking;

/*
https://www.reddit.com/r/csharp/comments/oz1mj6/comment/h7xq8hv/

This doesn't actually do what I'd need it to though, it keeps the array pinned but not any
of the objects that the array contains references to. I need those objects to stay pinned
so that references on the stack continue to correctly point to them.
*/

public class StackBufferPinArray : Base
{
    public unsafe void Run()
    {
        int count = GetCount();

        IntPtr* ptr = stackalloc IntPtr[count];
        ref string tRef = ref Unsafe.AsRef<string>(ptr);
        Span<string> strings = MemoryMarshal.CreateSpan(ref tRef, count);

        ref string r0 = ref MemoryMarshal.GetArrayDataReference(StringArr);
        fixed (void* p = &Unsafe.As<string, byte>(ref r0))
        {
            FillSpan(strings);
            GetString(strings);

            CallGC();

            GetString(strings);
        }
    }
}