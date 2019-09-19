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

    public void NextGreater()
    {
        TreeNode node= new TreeNode(100);
        node.Left = new  TreeNode(98);
        node.Left.Right = new  TreeNode(99);
        node.Left.Left = new TreeNode(96);
        node.Left.Left.Right = new TreeNode(97);
        node.Right = new TreeNode(102);

        TreeNode prev = null;
        Console.WriteLine(NextGreater(node, 96, ref prev));
    }

    private int NextGreater(TreeNode node, int num, ref TreeNode prev)
    {
        int res = 0;

        if (node == null)
        {
            return 0;
        }

        res = NextGreater(node.Left, num, ref prev);

        if (prev != null && prev.Value.Value == num)
        {
            prev = null;
            return node.Value.Value;
        }

        prev = node;

        if (res == 0)
        {
            res = NextGreater(node.Right, num, ref prev);
        }

        return res;
    }

    //https://leetcode.com/problems/validate-binary-search-tree/
    public void IsValidBST() 
    {
        TreeNode node = new TreeNode(5);
        node.Left = new TreeNode(1);
        node.Right = new TreeNode(4);
        node.Right.Left = new TreeNode(3);
        node.Right.Right = new TreeNode(6);

        TreeNode prev = null;
        Console.WriteLine(IsBST(node, ref prev));
    }
    
    private bool IsBST(TreeNode node, ref TreeNode prev)
    {
        if (node == null)
        {
            return true;
        }
        
        bool res = IsBST(node.Left, ref prev);
        
        if (res && (prev != null && prev.Value.Value > node.Value.Value))
        {
            return false;
        }
        
        prev = node;
        
        if (res)
        {
            res = IsBST(node.Right, ref prev);
        }
        
        return res;
    }

    //https://leetcode.com/problems/all-nodes-distance-k-in-binary-tree/
    public void KDistanceBinaryTree()
    {
        TreeNode node = new TreeNode(3);
        node.Left = new TreeNode(5);
        node.Right = new TreeNode(1);
        node.Left.Left = new TreeNode(6);
        node.Left.Right = new TreeNode(2);
        node.Right.Left = new TreeNode(0);
        node.Right.Right = new TreeNode(8);
        node.Left.Right.Left = new TreeNode(7);
        node.Left.Right.Right = new TreeNode(4);

        List<int> res = new List<int>();
        int K = 2;
        Dictionary<TreeNode, int> map = new Dictionary<TreeNode, int>();
        Find(node, node.Right, map);
        dfs(node, node.Right, K, map[node.Right], res, map);
    }

    private int Find(TreeNode root, TreeNode target, Dictionary<TreeNode, int> map) 
    {
        if (root == null)
        {
             return -1;
        }

        if (root == target) 
        {
            map.Add(root, 0);
            return 0;
        }

        int left = Find(root.Left, target, map);

        if (left >= 0)
        {
            map.Add(root, left + 1);
            return left + 1;
        }

        int right = Find(root.Right, target, map);

        if (right >= 0)
        {
            map.Add(root, right + 1);
            return right + 1;
        }

        return -1;
    }

    private void dfs(TreeNode root, TreeNode target, int K, int length, List<int> res, Dictionary<TreeNode, int> map) 
    {
        if (root == null)
        {
             return;
        }

        if (map.ContainsKey(root))
        {
             length = map[root];
        }

        if (length == K)
        {
             res.Add(root.Value.Value);
        }

        dfs(root.Left, target, K, length + 1, res, map);
        dfs(root.Right, target, K, length + 1, res, map);
    }

    //https://leetcode.com/contest/leetcode-weekly-contest-23/problems/construct-binary-tree-from-string/
    public void ConstructBinaryTreeFromString()
    {
        int idx = 0;
        var res = ConstructBinaryTreeFromString("4(2(3)(1))(6(5))", ref idx);
    }

    private TreeNode ConstructBinaryTreeFromString(string str, ref int idx)
    {
        if (idx >= str.Length)
        {
            return null;
        }

        TreeNode node = null;
        while(idx < str.Length)
        {
            if (str[idx] == '(')
            {
                idx++;
                return ConstructBinaryTreeFromString(str, ref idx);
            }
            else
            {
                if (int.TryParse(str[idx].ToString(), out int res))
                {
                    node = new TreeNode(res);
                    idx++;

                    if(str[idx] == ')')
                    {
                        idx++;
                        return node;
                    }

                    node.Left = ConstructBinaryTreeFromString(str, ref idx);
                    node.Right = ConstructBinaryTreeFromString(str, ref idx);
                    idx++;
                }
                else if (str[idx] == ')')
                {
                    idx++;
                }
            }

            return node;
        }
        return node;
    }

    //https://leetcode.com/problems/binary-tree-upside-down/description/
    public void BinaryTreeUpsideDown()
    {
        TreeNode node = new TreeNode(1);
        node.Left = new TreeNode(2);
        node.Right = new TreeNode(3);
        node.Left.Left = new TreeNode(4);
        node.Left.Right = new TreeNode(5);

        TreeNode root = null;
        BinaryTreeUpsideDown(node, ref root);
    }

    private TreeNode BinaryTreeUpsideDown(TreeNode node, ref TreeNode root)
    {
        if (node == null)
        {
            return null;
        }

        TreeNode left  = BinaryTreeUpsideDown(node.Left, ref root);

        TreeNode right = BinaryTreeUpsideDown(node.Right, ref root);

        if (left != null)
        {
            left.Left = right;
            left.Right = node;

            root = (root == null) ? left : root;
        }

        return node;
    }

    //https://leetcode.com/problems/flatten-binary-tree-to-linked-list/
    public void FlattenTree()
    {
        TreeNode node = new TreeNode(12);
        node.Left = new TreeNode(10);
        node.Left.Left = new TreeNode(8);
        node.Left.Right = new TreeNode(9);
        node.Right = new TreeNode(15);
        node.Left.Left.Left = new TreeNode(6);
        node.Left.Left.Right = new TreeNode(7);
        node.Left.Left.Left.Left = new TreeNode(3);
        node.Left.Left.Left.Right = new TreeNode(4);

        TreeNode last  = null;
        var res = FlattenTree(node, ref last);
    }

    private TreeNode FlattenTree(TreeNode node, ref TreeNode last)
    {
        if (node == null)
        {
            return null;
        }

        TreeNode left = FlattenTree(node.Left, ref last);

        var nodeRight = node.Right;

        if (left != null)
        {
            node.Right = left;
            var next = last == null ? left : last;
            next.Right = nodeRight;
            node.Left = null;
        }

        TreeNode right = FlattenTree(nodeRight, ref last);
        last = right;

        return node;
    }

    public void FillNodesWithEqualValues()
    {
        TreeNode node = new TreeNode(0);
        node.Left = new TreeNode(0);
        node.Right = new TreeNode(9);
        node.Left.Left = new TreeNode(0);
        node.Left.Right = new TreeNode(0);
        node.Right.Left = new TreeNode(12);
        node.Right.Right = new TreeNode(0);

        Console.WriteLine(FillNodesWithEqualValues(node, 0, 3));
    }

    private int FillNodesWithEqualValues(TreeNode node, int val, int k)
    {
        if (node == null)
        {
            return 0;
        }

        int left = FillNodesWithEqualValues(node.Left, node.Value.Value + val, k);

        int right = FillNodesWithEqualValues(node.Right, node.Value.Value + val, k);

        int res = left + right + node.Value.Value - k;

        node.Value = k;

        return res;
    }

    //https://leetcode.com/problems/minimum-depth-of-binary-tree/
    public void MinDepth() 
    {
        TreeNode node = new TreeNode(1);
        node.Left = new TreeNode(2); 

        int min = int.MaxValue;
        MinDepthInt(node, 0, ref min);

        Console.WriteLine(min);
    }
    
    private TreeNode MinDepthInt(TreeNode root, int dist, ref int min) 
    {
        if (root == null)
        {
            return null;
        }

        TreeNode left = MinDepthInt(root.Left, dist + 1, ref min);
        TreeNode right = MinDepthInt(root.Right, dist + 1, ref min);
     
        if (left == null && right == null)
        {
            min =  dist + 1 < min ? dist + 1 : min;
        }

        return root;
    }

    //https://leetcode.com/problems/distribute-coins-in-binary-tree/
    public void DistributeCoins()
    {
        TreeNode node = new TreeNode(0);
        node.Left = new TreeNode(0);
        node.Left.Left= new TreeNode(0);
        node.Left.Right = new TreeNode(0);
        node.Right = new TreeNode(0);
        node.Right.Left = new TreeNode(0);
        node.Right.Right = new TreeNode(7);

        Console.WriteLine(DistributeCoins(node, null)); 
    }

    private int DistributeCoins(TreeNode node, TreeNode prev) 
    {
        if (node == null) 
        {
            return 0;
        }

        int res = DistributeCoins(node.Left, node) + DistributeCoins(node.Right, node);

        if (prev != null) 
        {
            prev.Value += node.Value - 1;
        }

        return res + Math.Abs(node.Value.Value - 1);
    }

    public void IterateBST()
    {
        TreeNode node = new TreeNode(11);
        node.Left  = new TreeNode(6);
        node.Left.Left = new TreeNode(5);
        node.Left.Right = new TreeNode(9);
        node.Left.Right.Left = new TreeNode(7);
        node.Left.Right.Left.Right = new TreeNode(8);
        node.Right = new TreeNode(15);
        node.Right.Left = new TreeNode(12);
        node.Right.Right = new TreeNode(18);

        BSTIterator iterator = new BSTIterator(node);
        TreeNode cur = iterator.GetNextBST();

        while(cur != null)
        {
            Console.WriteLine(cur.Value);
            cur = iterator.GetNextBST();
        }
    }

    public class BSTIterator
    {
        private TreeNode cur = null;
        private TreeNode lastNode = null;

        Stack<TreeNode> stk = new Stack<TreeNode>();

        public BSTIterator(TreeNode node)
        {
            stk.Push(node);
            cur = node;
        }

        public TreeNode GetNextBST()
        {
            if (stk.Count > 0)
            {
                while (cur.Left != null && ((lastNode != null && cur.Left.Value > lastNode.Value) || lastNode == null))
                {
                    cur = cur.Left;
                    stk.Push(cur);
                }

                lastNode = stk.Pop();
                cur = lastNode;

                if (cur.Right != null)
                {
                    cur = cur.Right;
                    stk.Push(cur);
                }
                return lastNode;
            }

            return null;
        }
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