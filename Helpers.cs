using System;
using System.Collections.Generic;

public class ArrayComparer : IComparer<int[]>
{
    public int Compare(int[] x, int[] y)
    {
        return x[1].CompareTo(y[1]);
    }
}

public class Helpers
{
    public static void Swap(int[] arr, int src, int target)
    {
        int temp = arr[target];
        arr[target] = arr[src];
        arr[src] = temp;
    }
}