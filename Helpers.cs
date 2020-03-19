using System;
using System.Collections.Generic;
using System.Text;

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

public static class StringExtensions
{
    public static string Reverse(this string str)
    {
        StringBuilder sb = new StringBuilder();

        for(int idx = str.Length-1; idx >= 0; idx--)
        {
            sb.Append(str[idx]);
        }

        return sb.ToString();
    }
}