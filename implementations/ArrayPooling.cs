using System;
using System.Buffers;

namespace StackallocBenchmarking;

public class ArrayPooling : Base
{
    public void Run()
    {
        int count = GetCount();
        string[] rentedArray = ArrayPool<string>.Shared.Rent(count);
        Span<string> strings = rentedArray;

        FillSpan(strings);
        GetString(strings);

        CallGC();

        GetString(strings);
        ArrayPool<string>.Shared.Return(rentedArray, true);
    }
}