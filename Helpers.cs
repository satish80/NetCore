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

    public static void SwapValues(TreeNode a, TreeNode b)
    {
        int temp = a.Value.Value;
        a.Value = b.Value.Value;
        b.Value = temp;
    }

    public static TreeNode ConstructTree(int?[] arr)
    {
        Queue<TreeNode> queue = new Queue<TreeNode>();
        TreeNode head = new TreeNode((int)arr[0]);
        queue.Enqueue(head);

        int idx = 0;

        while (queue.Count > 0 && idx < arr.Length)
        {
            var cur =  queue.Dequeue();

            if (cur == null)
            {
                continue;
            }

            if (idx+1 < arr.Length && arr[++idx] != null)
            {
                cur.Left = new TreeNode((int)arr[idx]);
                cur.Left.Parent = cur;
                queue.Enqueue(cur.Left);
            }

            if (idx+1 < arr.Length && arr[++idx] != null)
            {
                cur.Right = new TreeNode((int)arr[idx]);
                cur.Right.Parent = cur;
                queue.Enqueue(cur.Right);
            }
        }

        return head;
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