using System;
using System.Collections.Generic;

public class Trees
{
    //Ensure all the node values are the same, given a tree with nodes having different values
    // Constraints: Only parent node can passs excess values to child and vice versa
    public void BalanceTreeNodeValues()
    {
        ParentTreeNode node = new ParentTreeNode(5,null);
        node.Left = new ParentTreeNode(2,node);
        node.Left.Left = new ParentTreeNode(3,node.Left);
        node.Left.Right = new ParentTreeNode(8, node.Left);
        node.Right = new ParentTreeNode(5, node);
        node.Right.Left = new ParentTreeNode(2,node.Right);
        node.Right.Right = new ParentTreeNode(3,node.Right);

        ParentTreeNode.MaxNodeValue = 4;

        var res = BalanceTreeNodeValues(node); 
    }

    private ParentTreeNode BalanceTreeNodeValues(ParentTreeNode node)
    {
        if (node == null)
        {
            return null;
        }

        BalanceTreeNodeValues(node.Left);
        BalanceTreeNodeValues(node.Right);

        ParentTreeNode.Balance = true;
        node.BalanceValue();

        return node;
    }

    //Accepted: https://leetcode.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal/
    public void ConstructBinaryTreeFromInAndPreorder()
    {
        int[] pre = new int[] {5, 2, 12, 4, 6, 3, 7};
        int[] inOrder = new int[] {12, 2, 4, 5, 3, 6, 7};
        int pIdx = -1;
        var res = ConstructBinaryTreeFromInAndPreorder(pre, inOrder, ref pIdx, 0, inOrder.Length-1);
    }

    private TreeNode ConstructBinaryTreeFromInAndPreorder(int[] pre, int[] inOrder, ref int pIdx, int start, int end)
    {
        if (start > end || end < 0)
        {
            return null;
        }

        pIdx++;
        int iIdx = FindInOrderIndex(inOrder, pre[pIdx]);

        TreeNode node = new TreeNode(inOrder[iIdx]);
        node.Left = ConstructBinaryTreeFromInAndPreorder(pre, inOrder, ref pIdx, start, iIdx - 1);
        node.Right = ConstructBinaryTreeFromInAndPreorder(pre, inOrder, ref pIdx, iIdx + 1, end);

        return node;
    }

    private int FindInOrderIndex(int[] inOrder, int val)
    {
        for(int idx = 0; idx < inOrder.Length; idx ++)
        {
            if (inOrder[idx] == val)
            {
                return idx;
            }
        }

        return -1;
    }

    //https://leetcode.com/problems/sum-of-left-leaves/
    public void SumOfLeftLeaves() 
    {
        TreeNode root = new TreeNode(0);
        root.Left = new TreeNode(2);
        root.Right = new TreeNode(4);
        root.Left.Left = new TreeNode(1);
        root.Left.Left.Left = new TreeNode(5);
        root.Left.Left.Right = new TreeNode(1);
        root.Right.Right = new TreeNode(-1);
        root.Right.Right.Right = new TreeNode(8);
        root.Right.Left = new TreeNode(3);
        root.Right.Left.Right = new TreeNode(6);


       Console.WriteLine(SumOfLeftLeavesInt(root, 0));
    }
    
    private int SumOfLeftLeavesInt(TreeNode root, int? sum)
    {
         int res = 0;
        
        if (root == null)
        {
            return res;
        }

        if (root.Left == null && root.Right == null)
        {
            return sum.Value;
        }
        
        res +=  SumOfLeftLeavesInt(root.Left, root.Left?.Value);
        
        res +=  SumOfLeftLeavesInt(root.Right, 0);
        
        return res;
    }

    public void CheckEqualTree() 
    {
        HashSet<int> map = new HashSet<int>();

        TreeNode node = new TreeNode(4);
        node.Left = new TreeNode(10);
        node.Left.Right = new TreeNode(2);
        node.Right = new TreeNode(3);
        node.Right.Right = new TreeNode(17);
        node.Right.Right.Left = new TreeNode(1);
        node.Right.Right.Right = new TreeNode(1);

        int total = CheckEqualTree(node, map);
        Console.WriteLine(map.Contains(total/ 2));
    }

    private int CheckEqualTree(TreeNode node, HashSet<int> map)
    {
        int left = 0;
        int right = 0;
        if (node.Left != null)
        {
            left = CheckEqualTree(node.Left, map);
        }
        if (node.Right != null)
        {
            right = CheckEqualTree(node.Right, map);
        }
        
        int rootSum = left + right + node.Value.Value;
        map.Add(rootSum);

        return rootSum;
    }

    //https://leetcode.com/articles/verify-preorder-serialization-of-a-binary-tree/#
    public void VerifyPreOrderSerialization()
    {
        Console.WriteLine(VerifyPreOrderSerialization("9,3,4,#,#,1,#,#,2,#,6,#,#"));
        Console.WriteLine(VerifyPreOrderSerialization("9,#,#,1"));
    }

    private bool VerifyPreOrderSerialization(string arr)
    {
        if (string.IsNullOrEmpty(arr))
        {
            throw new ArgumentNullException("Input is null");
        }

        Stack<string> stk = new Stack<string>();

        int idx = 1;
        string[] str = arr.Split(',');
        stk.Push(str[0]);

        while (idx < str.Length && stk.Count > 0)
        {
            if (str[idx] == "#")
            {
                stk.Pop();

                if (stk.Count == 0)
                {
                    return false;
                }

                if (stk.Peek() == "L")
                {
                    stk.Pop();
                    stk.Push("R");
                }
                else if (stk.Peek() == "R")
                {
                    while (stk.Count > 0 && stk.Peek() != "L")
                    {
                        stk.Pop();
                    }

                    if (stk.Count == 0)
                    {
                        return true;
                    }

                    stk.Pop();
                    stk.Push("R");
                }
                else
                {
                    stk.Pop();
                }
            }
            else
            {
                if (stk.Peek() != "R")
                {
                    stk.Push("L");
                }

                stk.Push(str[idx]);
            }

            idx ++;
        }

        return stk.Count == 0;
    }

  //https://www.geeksforgeeks.org/construct-bst-from-given-preorder-traversa/
    public void ConstructBSTFromPreOrder()
    {
        int[] arr = new int[] {10, 5, 1, 7, 40 ,50};
        int idx = 0;
        var res = ConstructBSTFromPreOrder(arr, int.MinValue, int.MaxValue, ref idx);
    }

    private TreeNode ConstructBSTFromPreOrder(int[] pre, int min, int max, ref int idx)
    {
        if (idx >= pre.Length)
        {
            return null;
        }
        TreeNode node = null;

        if (pre[idx] >= min && pre[idx] <= max)
        {
            node = new TreeNode(pre[idx++]);

            node.Left = ConstructBSTFromPreOrder(pre, min, node.Value.Value, ref idx);
            node.Right = ConstructBSTFromPreOrder(pre, node.Value.Value, max, ref idx);
        }

        return node;
    }
}

public class TreeNode
{
    public TreeNode(int value)
    {
        this.Value = value;
    }

    public TreeNode Left;
    public TreeNode Right;

    public int? Value;
}

public class ParentTreeNode
{
    private int value;
    private bool balance;
    public ParentTreeNode(int value, ParentTreeNode parent)
    {
        this.Value = value;
        this.Parent = parent;
    }
    public ParentTreeNode Left;
    public ParentTreeNode Right;

    public ParentTreeNode Parent;

    public static bool Balance
    {
        get;
        set;
    }

    public static int MaxNodeValue;

    public void BalanceValue()
    {
        if (this.value > MaxNodeValue && Balance)
        {
            ShareExcess(this.value - MaxNodeValue);
            this.value = MaxNodeValue;
        }
    }

    public int Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;

            BalanceValue();
        }
    }

    private void ShareExcess(int excessValue)
    {
        if (!Balance)
        {
            return;
        }

        ParentTreeNode small = null;
        if (this.Left != null && this.Left.Value < MaxNodeValue)
        {
            small = Left;
        }
        if (this.Right!= null && (small == null || small.Value > this.Right.Value))
        {
            small = Right;
        }

        if (small!= null)
        {
            small.Value += excessValue;
        }
        else
        {
            this.Parent.Value += excessValue;
        }
    }    
}