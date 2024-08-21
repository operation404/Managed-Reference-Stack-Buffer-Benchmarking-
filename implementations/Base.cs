using System;

namespace StackallocBenchmarking;

public abstract class Base
{
    private Random rand = new(RAND_SEED);

    protected int GetCount()
    {
        return COUNT;
        //return rand.Next(COUNT - MIN_ITEMS) + MIN_ITEMS;
    }

    protected void FillSpan(Span<string> span)
    {
        rand.GetItems(StringArr, span);
    }

}