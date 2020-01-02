using System;
using System.Collections.Generic;
using System.Text;

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

    //https://leetcode.com/problems/delete-nodes-and-return-forest/
    public void DeleteNodeReturnForest()
    {

    }

    private TreeNode DelNodes(TreeNode root, int[] to_delete, ref int idx, IList<TreeNode> res)
    {
        if (root == null)
        {
            return null;
        }

        TreeNode left = DelNodes(root.Left, to_delete, ref idx, res);
        TreeNode right = DelNodes(root.Right, to_delete, ref idx, res);

        if (root.Value.Value == to_delete[idx])
        {
            res.Add(root);
            idx++;
            return null;
        }

        return root;
    }

    //https://leetcode.com/problems/smallest-subtree-with-all-the-deepest-nodes/
    public void SubTreeWithDeepestNodes()
    {
        TreeNode node = new TreeNode(3);
        node.Left = new TreeNode(5);
        node.Right = new TreeNode(1);
        node.Left.Left = new TreeNode(6);
        node.Left.Right = new TreeNode(2);
        node.Left.Right.Left = new TreeNode(7);
        node.Left.Right.Right = new TreeNode(4);
        node.Right.Left = new TreeNode(0);
        node.Right.Right = new TreeNode(8);

        var res = SubTreeWithDeepestNodes(node);
    }

    private KeyValuePair<int, TreeNode> SubTreeWithDeepestNodes(TreeNode node)
    {
        if (node == null)
        {
            return new KeyValuePair<int, TreeNode>(0, null);
        }

        KeyValuePair<int, TreeNode> l =  SubTreeWithDeepestNodes(node.Left);
        KeyValuePair<int, TreeNode> r = SubTreeWithDeepestNodes(node.Right);

        return new KeyValuePair<int, TreeNode>(Math.Max(l.Key, r.Key) +1, 
        l.Key == r.Key ? node : (l.Key > r.Key ? l.Value : r.Value));
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

    //https://leetcode.com/problems/binary-tree-vertical-order-traversal/
    public void VerticalOrder()
    {
        TreeNode node = new TreeNode(3);
        node.Left = new TreeNode(9);
        node.Right = new TreeNode(20);
        node.Right.Left = new TreeNode(15); 
        node.Right.Right = new TreeNode(7); 

        var res = VerticalOrder(node);
    }

    private List<List<int>> VerticalOrder(TreeNode root)
    {
        List<List<int>> cols = new List<List<int>>();
        if (root == null)
        {
            return cols;
        }

        int[] range = new int[] {0, 0};
        getRange(root, range, 0);

        for (int i = range[0]; i <= range[1]; i++)
        {
            cols.Add(new List<int>());
        }

        Queue<TreeNode> queue = new Queue<TreeNode>();
        Queue<int> colQueue = new Queue<int>();

        queue.Enqueue(root);
        colQueue.Enqueue(-range[0]);

        while (queue.Count > 0)
        {
            TreeNode node = queue.Dequeue();
            int col = colQueue.Dequeue();
            
            cols[col].Add(node.Value.Value);
            
            if (node.Left != null)
            {
                queue.Enqueue(node.Left);
                colQueue.Enqueue(col - 1);
            }

            if (node.Right != null)
            {
                queue.Enqueue(node.Right);
                colQueue.Enqueue(col + 1);
            }
        }

        return cols;
    }

    public void getRange(TreeNode root, int[] range, int col)
    {
        if (root == null)
        {
            return;
        }
        range[0] = Math.Min(range[0], col);
        range[1] = Math.Max(range[1], col);
        
        getRange(root.Left, range, col - 1);
        getRange(root.Right, range, col + 1);
    }

    /*
    Asked by Google
    Given the sequence of keys visited by a postorder traversal of a binary search tree, reconstruct the tree.
    For example, given the sequence 2, 4, 3, 8, 7, 5, you should construct the following tree:
        5
       / \
      3   7
     / \   \
    2   4   8
    */
    public void ConstructBSTFromPostOrderSequence()
    {
        int[] arr = new int[] {2, 4, 3, 8, 7, 5};
        var res = ConstructBSTFromPostOrderSequence(arr, 0, arr.Length-1);
    }

    private TreeNode ConstructBSTFromPostOrderSequence(int[] arr, int start, int end)
    {
        if (start > end)
        {
            return null;
        }

        TreeNode node = new TreeNode(arr[end]);

        int idx = end;
        while(idx >= start)
        {
            if (arr[idx] < arr[end])
            {
                break;
            }
            idx--;
        }

        node.Right = ConstructPostOrder(arr, idx + 1, end-1);
        node.Left = ConstructPostOrder(arr, start, idx);

        return node;
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

    //Accepted: https://leetcode.com/problems/construct-binary-search-tree-from-preorder-traversal/solution/
    public void BSTFromPreOrder()
    {
        int[] arr = new int[] {10, 8, 6, 9, 12, 11, 15};
        int idx = 0;
        var res = BSTFromPreOrder(arr, int.MinValue, int.MaxValue, ref idx);
    }

    private TreeNode BSTFromPreOrder(int[] arr, int low, int high, ref int idx)
    {
        if (idx >= arr.Length )
        {
            return null;
        }

        int val = arr[idx];

        if (val < low || val > high)
        {
            return null;
        }

        TreeNode node = new TreeNode(val);
        idx++;

        node.Left = BSTFromPreOrder(arr, low, val, ref idx);
        node.Right = BSTFromPreOrder(arr, val, high, ref idx);

        return node;
    }

    //https://leetcode.com/problems/maximum-binary-tree/
    public void MaximumBinaryTree()
    {
        int[] arr = new int[] {3, 2, 1, 6, 0, 5};
        var res = MaximumBinaryTree(arr);
    }

    private TreeNode MaximumBinaryTree(int[] nums)
    {
        Stack<TreeNode> stack = new Stack<TreeNode>();

        for(int i = 0; i < nums.Length; i++)
        {
            TreeNode curr = new TreeNode(nums[i]);

            while(stack.Count > 0 && stack.Peek().Value < nums[i])
            {
                curr.Left = stack.Pop();
            }

            if(stack.Count > 0)
            {
                stack.Peek().Right = curr;
            }

            stack.Push(curr);
        }
        
        while (stack.Count > 1)
        {
            stack.Pop();
        }

        return stack.Count == 0 ? null : stack.Pop();
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

    /*
     Given a Node in the Tree (not the root & not the tree) find its first right neighbor.
     You cannot use extra space.
    */
    public void FindRightNeighbor()
    {
        TreeNode node = new TreeNode(1);
        node.Left = new TreeNode(2);
        node.Left.Parent = node;
        node.Right = new TreeNode(3);
        node.Right.Parent = node;
        node.Left.Left = new TreeNode(4);
        node.Left.Left.Parent = node.Left;
        node.Right.Right = new TreeNode(5);
        node.Right.Right.Parent = node.Right.Right;

        var res = FindRightNeighbor(node.Left.Left);
        Console.WriteLine(res.Value);
    }

    private TreeNode FindRightNeighbor(TreeNode node)
    {
        if (node == null)
        {
            return null;
        }

        int level = 1;

        node = node.Parent;

        while(node != null)
        {
            if (node.Right != null)
            {
                var res = FindRightSibling(node.Right, level-1);

                if (res != null)
                {
                    return res;
                }
            }

            level++;
            node = node.Parent;
        }

        return null;
    }

    private TreeNode FindRightSibling(TreeNode node, int level)
    {
        if (node == null)
        {
            return null;
        }

        if (level == 0)
        {
            return node;
        }

        TreeNode res = FindRightSibling(node.Left, level-1);
        res = FindRightSibling(node.Right, level-1);

        return res;
    }

    /*
    This problem was recently asked by Apple:
    Given an integer k and a binary search tree, find the floor (less than or equal to) of k, and the ceiling (larger than or equal to) of k.
    If either does not exist, then print them as None.
     */
    public void FloorCeiling()
    {
        TreeNode node = new TreeNode(8);
        node.Left = new TreeNode(4);
        node.Right = new TreeNode(12);
        node.Left.Left = new TreeNode(2);
        node.Left.Right = new TreeNode(6);
        node.Right.Left = new TreeNode(10);
        node.Right.Right = new TreeNode(14);

        TreeNode prev = null;
        int floor = -10000;
        int ceiling = 10000;

        FloorCeiling(node, ref prev, 5, ref floor, ref ceiling);
        Console.WriteLine($"Floor is {floor}");
        Console.WriteLine($"Ceiling is {ceiling}");
    }

    private void FloorCeiling(TreeNode node, ref TreeNode prev, int k, ref int floor, ref int ceiling)
    {
        if (node == null)
        {
            return;
        }

        FloorCeiling(node.Left, ref prev, k, ref floor, ref ceiling);

        if (prev != null && prev.Value <= k && node.Value >=k)
        {
            int diff = ceiling - floor;
            if(diff > node.Value - prev.Value)
            {
                ceiling = node.Value.Value;
                floor = prev.Value.Value;
            }
        }

        prev = node;

        FloorCeiling(node.Right, ref prev, k, ref floor, ref ceiling);

        return;
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

    //https://leetcode.com/problems/boundary-of-binary-tree/
    public void BoundaryOfTree()
    {
        TreeNode node= new TreeNode(1);
        node.Right = new TreeNode(2);
        node.Right.Left = new TreeNode(3);
        node.Right.Right = new TreeNode(4);

        List<int> res = new List<int>();

        res.Add(node.Value.Value);
        res.AddRange(LeftView(node.Left, new List<int>()));
        res.AddRange(Leaves(node.Left, new List<int>()));
        res.AddRange(Leaves(node.Right, new List<int>()));
        res.AddRange(RightView(node.Right, new List<int>()));
    }

    private List<int> LeftView(TreeNode node, List<int> res)
    {
        if (node == null || (node.Left == null && node.Right == null))
        {
            return res;
        }

        res.Add(node.Value.Value);

        if (node.Left == null)
        {
            LeftView(node.Right, res);
        }
        else
        {
            LeftView(node.Left, res);
        }

        return res;
    }

    private List<int> RightView(TreeNode node, List<int> res)
    {
        if (node == null || (node.Left == null && node.Right == null))
        {
            return res;
        }

        if (node.Right == null)
        {
            RightView(node.Left, res);
        }
        else
        {
            RightView(node.Right, res);
        }

        res.Add(node.Value.Value);

        return res;
    }

    private List<int> Leaves(TreeNode node, List<int> res)
    {
        if (node == null)
        {
            return res;
        }

        if (node.Left == null && node.Right == null)
        {
            res.Add(node.Value.Value);
            return res;
        }

        Leaves(node.Left, res);
        Leaves(node.Right, res);

        return res;
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

    //Accepted: https://leetcode.com/problems/distribute-coins-in-binary-tree/
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

    //Accepted: https://leetcode.com/problems/populating-next-right-pointers-in-each-node/
    public void PopulateNextRightPointers()
    {
        TreeNode node = new TreeNode(1);
        node.Left = new TreeNode(2);
        node.Right = new TreeNode(3);
        node.Left.Left = new TreeNode(4);
        node.Left.Right = new TreeNode(5);
        node.Right.Left = new TreeNode(6);
        node.Right.Right = new TreeNode(7);

        PopulateNextRightPointers(node);
    }

    private TreeNode PopulateNextRightPointers(TreeNode node)
    {
        TreeNode prev = node;
        TreeNode cur = node;

        while (prev != null && prev.Left != null)
        {
            cur = prev;

            while(cur != null)
            {
                cur.Left.NextRight = cur.Right;
                if (cur.NextRight != null)
                {
                    cur.Right.NextRight = cur.NextRight.Left;
                }
                cur = cur.NextRight;
            }

            prev = prev.Left;
        }

        return node;
    }
    //Accepted: https://leetcode.com/problems/maximum-level-sum-of-a-binary-tree/
    public void MaxLevelSum()
    {
        TreeNode node = new TreeNode(989);
        node.Right = new TreeNode(10250);
        node.Right.Left = new TreeNode(98693);
        node.Right.Right = new TreeNode(-89388);
        node.Right.Right.Right = new TreeNode(-32127);

        Console.WriteLine(MaxLevelSum(node));
    }

    private int MaxLevelSum(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }
        
        int[] map = new int[10000];
        
        MaxLevelSum(root, 1, map);
        
        int maxIdx = 0;
        
        for(int idx = 1; idx < map.Length; idx++)
        {
            maxIdx = map[idx] > map[maxIdx] ? idx : maxIdx;
        }
        
        return maxIdx;
    }
    
    private void MaxLevelSum(TreeNode node, int level, int[] map)
    {
        if (node == null)
        {
            return;
        }

        map[level] += node.Value.Value;

        MaxLevelSum(node.Left, level + 1, map);
        MaxLevelSum(node.Right, level + 1, map);

        return;
    }

    //Accepted: https://leetcode.com/problems/balanced-binary-tree/
    public void HeightBalanced()
    {
        // var root = ConstructTree(new int?[]{3,9,null,null, 20, 15, null, null, 7}, 0);
        TreeNode root = new TreeNode(1);
        root.Left = new TreeNode(2);
        root.Left.Left = new TreeNode(3);
        root.Left.Left.Left = new TreeNode(4);
        root.Left.Right = new TreeNode(3);
        root.Left.Left.Right = new TreeNode(4);
        root.Right = new TreeNode(2);

        bool isHeightBalanced = true;
        HeightBalanced(root, ref isHeightBalanced);

        Console.WriteLine($"The tree is height balanced: {isHeightBalanced}");
    }

    private int HeightBalanced(TreeNode node, ref bool isHeightBalanced)
    {
        if (node == null)
        {
            return 0;
        }

        int left = 0;
        int right = 0;

        if (isHeightBalanced)
        {
            left = HeightBalanced(node.Left, ref isHeightBalanced) + 1;
        }

        if (isHeightBalanced)
        {
            right = HeightBalanced(node.Right, ref isHeightBalanced) + 1;
        }

        if (Math.Abs(left-right) > 1)
        {
            isHeightBalanced = false;
        }

        return Math.Max(left, right);
    }

    private TreeNode ConstructTree(int?[] arr, int idx)
    {
        if (idx >= arr.Length || arr[idx] == null)
        {
            return null;
        }

        TreeNode node  = new TreeNode(arr[idx].Value);
        node.Left = ConstructTree(arr, ++idx);
        node.Right = ConstructTree(arr, ++idx);

        return node;
    }

    public void RightSibling()
    {
    /*
            1
           / \
          2   3
         /   / \
        4   5   6
    */
        TreeNode node = new TreeNode(1);
        node.Left = new TreeNode(2);
        node.Left.Parent = node;
        
        node.Left.Left = new TreeNode(4);
        node.Left.Left.Parent = node.Left;
            
        node.Right = new TreeNode(3);
        node.Right.Parent = node;
        
        node.Right.Left = new TreeNode(5);
        node.Right.Left.Parent = node.Right;
        
        node.Right.Right = new TreeNode(6);
        node.Right.Right.Parent = node.Right;

        Console.WriteLine(Right(node.Left.Left));
    }

    private int Right(TreeNode node)
    {
        int level = 0;
        TreeNode cur = node.Parent;
        level++;
        Stack<TreeNode> stk = new Stack<TreeNode>();

        if (cur.Right != null && cur.Right != node)
        {
            return cur.Right.Value.Value;
        }

        TreeNode prev = node;
        while (cur != null )
        {
            if (stk.Count == 0)
            {
                cur = cur.Parent;
                stk.Push(cur);
                level++;
            }
            else
            {
                cur = stk.Pop();

                if (cur.Right != null)
                {
                    cur = cur.Right;
                    stk.Push(cur);
                    level --;

                    if (level == 0)
                    {
                        return cur.Value.Value;
                    }

                    while(cur.Left != null)
                    {
                        cur = cur.Left;
                        level--;

                        if (level == 0)
                        {
                            return cur.Value.Value;
                        }
                    }
                }
            }
        }

        return level == 0 ? cur.Value.Value : -1;

    }

    //https://www.faceprep.in/replace-each-element-by-its-rank-in-the-given-array/
    public void ReplaceElementByItsRank()
    {
        int[] arr = new int[] {4, 2, 3, 7};
        TreeNode root = new TreeNode(arr[0]);
        root.Rank = 1;

        for(int idx = 1; idx < arr.Length; idx++)
        {
            ReplaceElementByItsRank(root, arr, new int[arr.Length], idx);
        }
    }

    private int[] ReplaceElementByItsRank(TreeNode node, int[] arr, int[] res, int idx)
    {
        if (idx >= arr.Length || node == null)
        {
            return res;
        }

        if (arr[idx] > node.Value)
        {
            ReplaceElementByItsRank(node.Right, arr, res, idx);
            var newNode = new TreeNode(arr[idx]);
            newNode.Rank = node.Rank + 1;
            node.Right = newNode;
        }
        else
        {
            node.Rank ++;
            int newRank = 1;

            if (node.Left != null)
            {
                if (arr[idx] < node.Left.Rank)
                {
                    ReplaceElementByItsRank(node.Left, arr, res, idx);
                }
                else
                {
                    newRank = node.Rank -1;
                }
            }
            else
            {
                newRank = 1;
            }

            var newNode = new TreeNode(arr[idx]);
            newNode.Rank = newRank;
            if (newRank == 1)
            {
                node.Left = newNode;
            }
            else
            {
                node.Left.Right = newNode;
            }
        }

        return res;
    }

    //Accepted: https://leetcode.com/problems/serialize-and-deserialize-binary-tree/
    public void Serialize() 
    {
        TreeNode root = new TreeNode(1);
        root.Left = new TreeNode(2);
        root.Right = new TreeNode(3);
        root.Right.Left = new TreeNode(4);
        root.Right.Right = new TreeNode(5);

        var res = Serialize(null, new StringBuilder());
        TreeNode node = Deserialize(res);
    }

    private string Serialize(TreeNode node, StringBuilder sb)
    {
        if (node == null)
        {
            sb.Append(",N");
            return sb.ToString();
        }

        if (sb.Length > 0)
        {
            sb.Append(string.Concat(",", node.Value.ToString()));
        }
        else
        {
            sb.Append(node.Value.ToString());
        }

        Serialize(node.Left, sb);
        Serialize(node.Right, sb);

        return sb.ToString();
    }

    // Decodes your encoded data to tree.
    private TreeNode Deserialize(string data) 
    {
        int idx = 0;
        return Deserialize(data.Split(","), ref idx);
    }

    private TreeNode Deserialize(string[] arr, ref int idx)
    {
        if (idx >= arr.Length || arr[idx] == "N" || arr[idx] == "")
        {
            return null;
        }

        TreeNode node = new TreeNode(Int32.Parse(arr[idx++]));
        node.Left = Deserialize(arr, ref idx);

        idx++;

        node.Right = Deserialize(arr, ref idx);

        return node;
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
    public TreeNode NextRight;
    public TreeNode Parent;

    public int Rank;
    public int? Value;
}

public class ParentTreeNode
{
    private int value;
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