using System;

namespace StackallocBenchmarking;

public class HeapArray : Base
{
    public void Run()
    {
        int count = GetCount();
        Span<string> strings = new string[count];

        FillSpan(strings);
        GetString(strings);

        CallGC();

        GetString(strings);
    }
}