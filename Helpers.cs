using System;
using System.Collections.Generic;

public class ArrayComparer : IComparer<int[]>
{
    public int Compare(int[] x, int[] y)
    {
        return x[1].CompareTo(y[1]);
    }
}