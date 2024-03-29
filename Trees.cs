using System;
using System.Collections.Generic;
using System.Text;
using DataStructures;

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
        int[] pre = new int[] {3,9,20,15,7};
        int[] inOrder = new int[] {9,3,15,20,7};
        int idx = 0;

        var res = ConstructBinaryTreeFromInAndPreorder(inOrder, pre, 0, pre.Length-1, ref idx);
    }

    private TreeNode ConstructBinaryTreeFromInAndPreorder(int[] inorder, int[] preOrder, int start, int end, ref int idx)
    {
        if (idx >= preOrder.Length)
        {
            return null;
        }

        TreeNode node = new TreeNode(preOrder[idx++]);

        if (start == end)
        {
            return node;
        }

        int inIdx = FindInorderIndex(inorder, node.Value.Value, start, end);
        node.Left = ConstructBinaryTreeFromInAndPreorder(inorder, preOrder, start, inIdx-1, ref idx);
        node.Right = ConstructBinaryTreeFromInAndPreorder(inorder, preOrder, inIdx+1, end, ref idx);

        return node;
    }

    //https://leetcode.com/problems/find-duplicate-subtrees/
    public void FindDuplicateSubTrees()
    {
        int?[] arr=  new int?[]{1,2,3,4,null,2,4,null,null,4};
        var node = Helpers.ConstructTree(arr);
        var res = new List<TreeNode>();
        FindDuplicateSubTrees(node, new Dictionary<string, TreeNode>(), res);
    }

    private string FindDuplicateSubTrees(TreeNode root, Dictionary<string, TreeNode> map, IList<TreeNode> res)
    {
        if (root == null)
        {
            return string.Empty;
        }

        string left = FindDuplicateSubTrees(root.Left, map, res);
        string right = FindDuplicateSubTrees(root.Left, map, res);

        string cur = root.Value + "L" + left + "R" + right;
        
        if (!map.ContainsKey(cur))
        {
            map.Add(cur, root);
        }
        else
        {
            if (map[cur] != null)
            {           
                res.Add(root);
            }
            map[cur] = null;
        }

        return cur;
    }

    //https://leetcode.com/problems/count-of-smaller-numbers-after-self/
    public void CountSmallerNumbersAfterSelf()
    {
        int[] nums = new int[] {3, 2, 2, 6, 1};
        var res = CountSmallerNumbersAfterSelf(nums);
    }

    private IList<int> CountSmallerNumbersAfterSelf(int[] nums)
    {
        TreeNodeWithSum root = null;
        int[] ans = new int[nums.Length];

        for(int idx = nums.Length-1; idx >= 0; idx--)
        {
            root = Insert(nums, root, idx, ans, 0);
        }

        return new List<int>(ans);
    }

    private TreeNodeWithSum Insert(int[] nums, TreeNodeWithSum root, int idx, int[] ans, int preSum)
    {
        if (root == null)
        {
            root = new TreeNodeWithSum(nums[idx], 0, 0);
            ans[idx] = preSum;
        }
        
        if (nums[idx] < root.Value)
        {
            root.SmallerCount++;
            root.Left = Insert(nums, root.Left, idx, ans, preSum);
        }
        else if (nums[idx] > root.Value)
        {
            preSum += root.DupeCount+ root.SmallerCount;
            root.Right = Insert(nums, root.Right, idx, ans, preSum);
        }
        else
        {
            root.DupeCount++;
            ans[idx] = preSum + root.SmallerCount;
        }

        return root;
    }

    //Accepted-LCMedium-SelfSol-T:O(n)-S:O(n) https://leetcode.com/problems/construct-binary-tree-from-string/
    public void Str2tree()
    {
        string s = "4";
        var res = Str2Tree_Stack(s);
    }

    private TreeNode Str2Tree_Stack(string s)
    {
        Stack<TreeNode> stk = new Stack<TreeNode>();
        int idx = 0;

        TreeNode head = null;

        while (idx < s.Length)
        {
            if (s[idx] == '(')
            {
                idx++;
            }
            else if (s[idx] == ')')
            {
                stk.Pop();
                idx++;
            }
            else
            {
                var top = stk.Count > 0 ? stk.Peek() : null;
                int val = 0;
                string valStr = string.Empty;
                bool negative = false;

                while(idx < s.Length && (char.IsDigit(s[idx]) || s[idx] == '-'))
                {
                    if (s[idx] == '-')
                    {
                        negative = true;
                    }
                    else
                    {
                        valStr = valStr + s[idx];
                    }

                    idx++;
                }

                val = negative ? -int.Parse(valStr) : int.Parse(valStr);

                var curNode = new TreeNode(val);

                if (head == null)
                {
                    head = curNode;
                }

                if (top != null)
                {
                    if (top.Left == null)
                    {
                        top.Left = curNode;
                    }
                    else
                    {
                        top.Right = curNode;
                    }
                }

                stk.Push(curNode);
            }
        }

        return head;
    }

    //Accepted-LCMedium-SelfSol-T:O(n)-S:O(1) https://leetcode.com/problems/closest-binary-search-tree-value/
    public int ClosestValue() 
    {
        int?[] arr = new int?[] {4,2,5,1,3};
        var root = Helpers.ConstructTree(arr);
        double target = 3.714286;
        double minDiff = int.MaxValue;
        int rootVal = -1;
        ClosestValue(root, target, ref minDiff, ref rootVal);
        return rootVal;
    }
    
    private void ClosestValue(TreeNode root, double target, ref double minDiff, ref int rootVal)
    {
        if (root == null)
        {
            return;
        }
        
        double diff = Math.Abs(root.Value.Value - target);
        
        if (diff < minDiff)
        {
            minDiff = diff;
            rootVal = root.Value.Value;
        }
        
        if (root.Value  > target)
        {
            ClosestValue(root.Left, target, ref minDiff, ref rootVal);
        }
        else
        {
            ClosestValue(root.Right, target, ref minDiff, ref rootVal);
        }
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

    //Accepted-LcMedium-SelfSol-T:O(n)-S:O(H+N) https://leetcode.com/problems/delete-nodes-and-return-forest/
    public void DeleteNodeReturnForest()
    {
        int?[] arr = new int?[]{1,2,3,4,5,6,7};
        int[] to_delete = new int[] {3,5};

        TreeNode root = Helpers.ConstructTree(arr);

        HashSet<int> delList = new HashSet<int>();
        
        foreach(int del in to_delete)
        {
            delList.Add(del);
        }
        
        IList<TreeNode> res = new List<TreeNode>();

        if (!delList.Contains(root.Value.Value))
        {
            res.Add(root);
        }

        DelNodes(root, delList, res);
    }

    private TreeNode DelNodes(TreeNode root, HashSet<int> delList, IList<TreeNode> res)
    {
        if (root == null)
        {
            return null;
        }
        
        root.Left =  DelNodes(root.Left, delList, res);
        root.Right =  DelNodes(root.Right, delList, res);
        
        if (delList.Contains(root.Value.Value))
        {
            if (root.Left!= null)
            {
                res.Add(root.Left);
            }
            
            if (root.Right!= null)
            {
                res.Add(root.Right);
            }
            
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

    //https://leetcode.com/problems/recover-binary-search-tree/
    public void RecoverTree()
    {
        TreeNode node = new TreeNode(2);
        node.Left = new TreeNode(3);
        node.Right= new TreeNode(1);

        TreeNode prev = null, first = null, second = null;

        RecoverBST(node, ref prev, ref first, ref second);

        Helpers.SwapValues(first, second);
    }

    private void RecoverBST(TreeNode node, ref TreeNode prev, ref TreeNode first, ref TreeNode second)
    {
        if (node == null)
        {
            return;
        }

        RecoverBST(node.Left, ref prev, ref first, ref second);

        if (prev!= null)
        {
            if (first == null && node.Value.Value <= prev.Value.Value)
            {
                first = prev;
            }

            if (first != null && node.Value.Value <= prev.Value.Value)
            {
                second = node;
            }
        }

        prev = node;

        RecoverBST(node.Right, ref prev, ref first, ref second);
    }

    //https://leetcode.com/problems/binary-tree-maximum-path-sum/
    public void MaxPathSum()
    {
        int?[] arr = new int?[]{-3};
        var node = Helpers.ConstructTree(arr);
        int max = int.MinValue;
        MaxPathSum(node, 0, ref max);
        Console.WriteLine(max);
    }

    private int MaxPathSum(TreeNode root, int sum, ref int max)
    {
        if(root == null)
        {
            return 0;
        }
        
        int left = Math.Max(0,MaxPathSum(root.Left, sum + root.Value.Value, ref max));
        int right = Math.Max(0,MaxPathSum(root.Right, sum + root.Value.Value, ref max));
        
        max = Math.Max(max, left + right + root.Value.Value);
        
        return Math.Max(left, right) + root.Value.Value;
    }

    //https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-search-tree/
    public void LowestCommonAncestor()
    {
        int?[] arr = new int?[]{6,2,8,0,4,7,9,null,null,3,5};
        var node = Helpers.ConstructTree(arr);
        var p = node.Left.Right.Right;
        var q = node.Right;
        var res = LowestCommonAncestor(node, p, q);        
    }

    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) 
    {
        if(root.Value.Value > p.Value.Value && root.Value.Value > q.Value.Value)
        {
            return LowestCommonAncestor(root.Left, p, q);
        }
        else if(root.Value.Value < p.Value.Value && root.Value.Value < q.Value.Value)
        {
            return LowestCommonAncestor(root.Right, p, q);
        }
        else
        {
            return root;
        }
    }

    //https://leetcode.com/problems/lowest-common-ancestor-of-deepest-leaves/
    public void LCADeepestLeaves()
    {
        int?[] arr = new int?[]{3,5,1,6,2,0,8,null,null,7,4};
        TreeNode node = Helpers.ConstructTree(arr);
        var res = LCADeepestLeaves(node);
    }

    private TreeNode LCADeepestLeaves(TreeNode root)
    {
        if (root == null)
        {
            return null;
        }

        Queue<Tuple<TreeNode, int>> queue = new Queue<Tuple<TreeNode, int>>();
        queue.Enqueue(new Tuple<TreeNode, int>(root, 1));
        TreeNode first = null, second = null;
        int maxLevel = 1;

        while (queue.Count > 0)
        {
            var cur = queue.Dequeue();

            if (cur.Item1.Left != null)
            {
                queue.Enqueue(new Tuple<TreeNode, int>(cur.Item1.Left, cur.Item2+1));
            }

            if (cur.Item1.Right != null)
            {
                queue.Enqueue(new Tuple<TreeNode, int>(cur.Item1.Right, cur.Item2+1));
            }

            if (maxLevel < cur.Item2 && cur.Item1.Left!= null && cur.Item1.Right != null)
            {
                first = cur.Item1.Left;
                second = cur.Item1.Right;
                maxLevel = cur.Item2;
            }
        }

        return LCA(root, first, second);
    }

    private TreeNode LCA(TreeNode root, TreeNode first, TreeNode second)
    {
        if (root == null)
        {
            return null;
        }

        if (root.Left == first || root .Right == first)
        {
            return root;
        }

        if (root.Left == second || root.Right == second)
        {
            return root;
        }

        TreeNode left = LCA(root.Left, first, second);
        TreeNode right = LCA(root.Right, first, second);

        TreeNode lcaNode = left != null && right != null ? root : left != null ? left : right;

        return lcaNode;
    }

    /*
    This problem was asked by Google.
    Given the root of a binary search tree, and a target K, return two nodes in the
    tree whose sum equals K.
    For example, given the following tree and K of 20
      10
    /   \
    5    15
        /  \
       11   15
    Return the nodes 5 and 15.
    */
    public void BSTSumEqualsK()
    {
        int?[] arr = new int?[]{9, 5, 15, null, null, 11, 15};
        var node = Helpers.ConstructTree(arr);
        var res = new List<TreeNode>();
        int k = 20;
        BSTSumEqualsK(node, k, res, new Dictionary<int, TreeNode>());
    }

    private void BSTSumEqualsK(TreeNode node, int k, List<TreeNode> res, Dictionary<int, TreeNode> map)
    {
        if (node == null)
        {
            return;
        }

        if (node.Value.Value > k)
        {
            return;
        }

        BSTSumEqualsK(node.Left, k, res, map);

        if (map.ContainsKey(k - node.Value.Value))
        {
            res.Add(node);
            res.Add(map[k - node.Value.Value]);
            return;
        }

        map.Add(node.Value.Value, node);
        
        BSTSumEqualsK(node.Right, k, res, map);

        return;

    }

    //Accepted:LCMedium-LCSol-T:O(n):S:O(1) https://leetcode.com/problems/house-robber-iii/
    public void HouseRobberIII()
    {
        int?[] arr = new int?[] {3,1,1,1,3,null,1};
        TreeNode node = Helpers.ConstructTree(arr);
        var res = Rob(node);
        Console.WriteLine(Math.Max(res[0], res[1]));
    }

    private int[] Rob(TreeNode root)
    {
        if (root == null)
        {
            return new int[2];
        }

        int[] left = Rob(root.Left);
        int[] right = Rob(root.Right);
        int[] res = new int[2];

        //previous
        res[0] = Math.Max(left[0], left[1]) + Math.Max(right[0], right[1]);

        //current
        res[1] = root.Value.Value + left[0] + right[0];

         return res;
    }

    private void RecoverTree(TreeNode root, TreeNode pre, TreeNode first, TreeNode second)
    {
        if (root == null)
        {
            return;
        }

        RecoverTree(root.Left, pre, first, second);

        if (pre != null && pre.Value.Value > root.Value.Value)
        {
            if (first == null)
            {
                first = pre;
                second = root;
            }
            else
            {
                second = root;
            }
        }

        pre = root;

        if (first != null && second!= null)
        {
            int temp = first.Value.Value;
            first.Value = second.Value.Value;
            second.Value = temp;
        }

        RecoverTree(root.Right, pre, first, second);
    }

    //Accepted-LCMedium-Self-T:O(n)-https://leetcode.com/problems/binary-tree-vertical-order-traversal/
    public void VerticalOrder()
    {
        TreeNode node = new TreeNode(3);
        node.Left = new TreeNode(9);
        node.Right = new TreeNode(20);
        node.Right.Left = new TreeNode(15);
        node.Right.Right = new TreeNode(7);

        var res = VerticalOrderCre(null);
    }

    private List<List<int>> VerticalOrderCre(TreeNode root)
    {
        Queue<Tuple<TreeNode, int>> queue = new Queue<Tuple<TreeNode, int>>();
        List<List<int>> res = new List<List<int>>();

        if (root == null)
        {
            return res;
        }

        Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
        int min= int.MaxValue, max =int.MinValue;

        queue.Enqueue(new Tuple<TreeNode, int>(root, 0));

        while (queue.Count > 0)
        {
            var cur = queue.Dequeue();
            var node = cur.Item1;
            min = Math.Min(min, cur.Item2);
            max= Math.Max(max, cur.Item2);

            if (!map.ContainsKey(cur.Item2))
            {
                map[cur.Item2] = new List<int>();
            }

            map[cur.Item2].Add(node.Value.Value);

            if (node.Left!= null)
            {
                queue.Enqueue(new Tuple<TreeNode, int>(node.Left, cur.Item2-1));
            }
            if (node.Right!= null)
            {
                queue.Enqueue(new Tuple<TreeNode, int>(node.Right, cur.Item2+1));
            }
        }

        for(int idx = min; idx <= max; idx++)
        {
            if (map.ContainsKey(idx))
            {
                res.Add(map[idx]);
            }
        }
        return res;
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
        //int[] arr = new int[] {2, 4, 3, 8, 7, 5};
        int[] arr = new int[] {1, 4, 3, 8, 10, 9, 7};
        //var res = ConstructBSTFromPostOrderSequence(arr, 0, arr.Length-1);
        int idx = arr.Length-1;
        var res = ConstructBSTFromPostOrder(arr, int.MinValue, int.MaxValue, ref idx);
    }

    private TreeNode ConstructBSTFromPostOrder(int[] arr, int min, int max, ref int idx)
    {
        if (idx >= arr.Length)
        {
            return null;
        }
        TreeNode node = null;

        if (idx >=0 && arr[idx] >= min && arr[idx] <= max)
        {
            node = new TreeNode(arr[idx--]);
            node.Right = ConstructBSTFromPostOrder(arr, node.Value.Value, max, ref idx);
            node.Left = ConstructBSTFromPostOrder(arr, min, node.Value.Value, ref idx);
        }

        return node;
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

        node.Right = ConstructBSTFromPostOrderSequence(arr, idx + 1, end-1);
        node.Left = ConstructBSTFromPostOrderSequence(arr, start, idx);

        return node;
    }

    /*
    Typically, an implementation of in-order traversal of a binary tree has O(h) space complexity, 
    where h is the height of the tree. Write a program to compute the in-order traversal of a binary tree using O(1) space.
    */
    public void InOrderTraversal_O1Space()
    {

    }

    //Use Morris traversal
    private void InOrderTraversal_O1Space(TreeNode node)
    {
        Stack<TreeNode> stk = new Stack<TreeNode>();
        stk.Push(node);

    }

    //https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree-iii
    public void LCAIII()
    {
        int?[] arr = new int?[] {-1,0,3,-2,4,null,null,8};
        var node = Helpers.ConstructTree(arr);
        var p = node.Left.Left.Left;
        var q = node.Left.Right;
        var res = LCAIII(p, q);
    }

    private TreeNode LCAIII(TreeNode p, TreeNode q)
    {
        TreeNode a = p, b = q;
        while (a != b)
        {
            a = a == null? q : a.Parent;
            b = b == null? p : b.Parent;    
        }

        return a;
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

    //Accepted: https://leetcode.com/problems/convert-binary-search-tree-to-sorted-doubly-linked-list/
    public void BstToDoubleLinkedList()
    {
        TreeNode node= new TreeNode(4);
        node.Left = new TreeNode(2);
        node.Left.Left = new TreeNode(1);
        node.Left.Right = new TreeNode(3);
        node.Right = new TreeNode(5);

        TreeNode prev = null;
        var res = BstToDoubleLinkedList(node, ref prev);

    }

    private TreeNode BstToDoubleLinkedList(TreeNode node, ref TreeNode prev)
    {
        if (node == null)
        {
            return null;
        }

        TreeNode dummy = new TreeNode(0, null, null);
        prev = dummy;
        ConvertToDLL(node, ref prev);
        //connect head and tail
        prev.Right = dummy.Right;
        dummy.Right.Left = prev;
        return dummy.Right;
    }

    private void ConvertToDLL(TreeNode cur, ref TreeNode prev)
    {
        if (cur == null)
        {
            return;
        }

        ConvertToDLL(cur.Left, ref prev);

        prev.Right = cur;
        cur.Left = prev;
        prev = cur;

        ConvertToDLL(cur.Right, ref prev);
    }

    //Accepted:LCMedium:LCSol-T:O(n), S:O(1): https://leetcode.com/problems/flatten-binary-tree-to-linked-list/
    public void FlattenBinaryTreeToSLL()
    {
        TreeNode node = new TreeNode(1);
        node.Left = new TreeNode(2);
        node.Right = new TreeNode(5);
        node.Left.Left = new TreeNode(3);
        node.Left.Right = new TreeNode(4);
        node.Right.Right = new TreeNode(6);

        TreeNode prev = null;
        FlattenBinaryTreeToSLL(node, ref prev);
    }

    private void FlattenBinaryTreeToSLL(TreeNode node, ref TreeNode prev)
    {
        if (node == null)
        {
            return;
        }

        FlattenBinaryTreeToSLL(node.Right, ref prev);
        FlattenBinaryTreeToSLL(node.Left, ref prev);
        node.Right = prev;
        node.Left = null;
        prev = node;
    }

    //https://leetcode.com/problems/diameter-of-binary-tree/
    public void DiameterOfBinaryTree()
    {
        int max = 0;
        int?[] arr = new int?[]{1,2,3,4,5};

        var root = Helpers.ConstructTree(arr);
        DiameterOfBinaryTree(root, ref max);
        Console.WriteLine(max);
    }

    private int DiameterOfBinaryTree(TreeNode root, ref int max)
    {
        if (root == null)
        {
            return 0;
        }
        
        int left = DiameterOfBinaryTree(root.Left, ref max);
        int right = DiameterOfBinaryTree(root.Right, ref max);
        
        int cur = left + right;
        max = Math.Max(max, cur);
        
        return Math.Max(left, right) + 1;
    }

    //Accepted:LCMedium:Self:T:O(n)-S:O(n):https://leetcode.com/problems/correct-a-binary-tree/
    public void CorrectBinaryTree()
    {
        TreeNode node = new TreeNode(8);
        node.Left = new TreeNode(3);
        node.Right = new TreeNode(1);
        node.Left.Left = new TreeNode(7);
        node.Left.Left.Left = new TreeNode(2);
        node.Right.Left = new TreeNode(9);
        node.Right.Right = new TreeNode(4);
        node.Right.Right.Left = new TreeNode(5);
        node.Right.Right.Right = new TreeNode(6);
        node.Left.Left.Right = node.Right.Right;

        // TreeNode node = new TreeNode(1);
        // node.Left = new TreeNode(2);
        // node.Right = new TreeNode(3);
        // node.Left.Right = node.Right;

        var res = CorrectBinaryTree(node, new HashSet<TreeNode>());
    }

    private TreeNode CorrectBinaryTree(TreeNode node, HashSet<TreeNode> set)
    {
        if (node == null)
        {
            return null;
        }

        if (set.Contains(node.Right) || set.Contains(node.Left))
        {
            return null;
        }

        set.Add(node);

        node.Right =  CorrectBinaryTree(node.Right, set);
        node.Left =  CorrectBinaryTree(node.Left, set);

        return node;
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

    //https://leetcode.com/problems/count-of-smaller-numbers-after-self/
    public void CountSmallNumbersAfterSelf()
    {
        int[] arr = new int[] {1, 6, 2, 5};
        CountSmallNumbersAfterSelf(arr);
    }

    private void CountSmallNumbersAfterSelf(int[] nums)
    {
        IDictionary<int, int> list = new Dictionary<int, int>();
        BST root = new BST(nums[0]);
        int small = 0; 

        for(int idx = 1; idx < nums.Length; idx++)
        {
            root.Add(nums[idx], root, ref small);
            small = 0;
        }
    }

    //LCEasy-LCSol-T:O(n)-Accepted:https://leetcode.com/problems/minimum-distance-between-bst-nodes/submissions/
    public void MinDiffInBST()
    {
        int min = int.MaxValue;
        TreeNode prev = null;

        TreeNode node= new TreeNode(4);
        node.Left = new TreeNode(2);
        node.Left.Left = new TreeNode(1);
        node.Left.Right = new TreeNode(3);
        node.Right = new TreeNode(6);

        MinDiffInBST(node, ref min, ref prev);
        Console.WriteLine(min);
    }

    private TreeNode MinDiffInBST(TreeNode root, ref int min, ref TreeNode prev)
    {
        if (root == null)
        {
            return null;
        }

        TreeNode left = MinDiffInBST(root.Left, ref min, ref prev);

        if (prev != null)
        {
            min = Math.Min(Math.Abs(root.Value.Value - prev.Value.Value), min);
        }

        prev = root;

        TreeNode right = MinDiffInBST(root.Right, ref min, ref prev);

        return root;
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
        //Find(node, node.Right, map);
        //dfs(node, node.Right, K, map[node.Right], res, map);
        KDistanceBinaryTree(node, 1, 0, K, res);

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


    private IList<int> KDistanceBinaryTree(TreeNode node, int rootVal, int val, int target, IList<int> res)
    {
        if (node == null)
        {
            return res;
        }

        if (val > rootVal)
        {
            if (Math.Abs(val - rootVal) == target)
            {
                res.Add(node.Value.Value);
            }
        }
        else if (val < rootVal)
        {
            if (Math.Abs(val + (rootVal - val)) == target)
            {
                res.Add(node.Value.Value);
            }
        }
        else
        {
            if (val + rootVal == target)
            {
                res.Add(node.Value.Value);
            }
        }

        KDistanceBinaryTree(node.Left, rootVal, val +1, target, res);

        KDistanceBinaryTree(node.Right, rootVal, val +1, target, res);

        return res;
    }

    //Find the max value less than N in BST
    public void FindMaxNInBST()
    {
        TreeNode node = new TreeNode(5);
        node.Left = new TreeNode(3);
        node.Right = new TreeNode(7);
        node.Left.Left = new TreeNode(2);
        node.Left.Right = new TreeNode(4);
        node.Right.Left = new TreeNode(6);
        node.Right.Right = new TreeNode(8);
        int? max = null;
        Console.WriteLine(FindMaxNInBST(node, 4, ref max));
    }

    private int? FindMaxNInBST(TreeNode node, int n, ref int? max)
    {
        if (node == null)
        {
            return max;
        }

        max = ((max == null && node.Value < n) || max != null && node.Value < n && max < node.Value) ? node.Value : max;

        if (node.Value >= n)
        {
            FindMaxNInBST(node.Left, n, ref max);
        }
        else if (node.Value < n)
        {
            FindMaxNInBST(node.Right, n, ref max);
        }

        return max;
    }

    //https://leetcode.com/problems/binary-tree-level-order-traversal/
    public void LevelOrderTraversal()
    {
        TreeNode node = new TreeNode(3);
        node.Left = new TreeNode(9);
        node.Right = new TreeNode(20);
        node.Right.Left = new TreeNode(15);
        node.Right.Right = new TreeNode(7);

        var res = LevelOrderTraversal(node);
    }

    private IList<IList<int>> LevelOrderTraversal(TreeNode node)
    {
        Queue<Tuple<int, TreeNode>> queue = new Queue<Tuple<int, TreeNode>>();
        queue.Enqueue(new Tuple<int,TreeNode>(0, node));

        IList<IList<int>> res = new List<IList<int>>();

        while(queue.Count > 0)
        {
            var cur = queue.Dequeue();
            if (res[cur.Item1] == null)
            {
                res[cur.Item1] = new List<int>();
            }

            res[cur.Item1].Add(cur.Item2.Value.Value);

            if (cur.Item2.Left != null)
            {
                queue.Enqueue(new Tuple<int, TreeNode>(cur.Item1+1, cur.Item2.Left));
            }

            if (cur.Item2.Right != null)
            {
                queue.Enqueue(new Tuple<int, TreeNode>(cur.Item1+1, cur.Item2.Right));
            }
        }

        return res;
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

    //https://leetcode.com/problems/balance-a-binary-search-tree/
    public void BalanceBSTCre()
    {
        int?[] arr = new int?[]{1,null,2,null,3,null,4,null,null};
        var node = Helpers.ConstructTree(arr);
        var list = new List<TreeNode>();
        InorderBalanceBST(node, list) ;
        var res= BalanceBST(list, 0, list.Count-1);
    }

    private TreeNode InorderBalanceBST(TreeNode root, List<TreeNode> arr)
    {
        if (root == null)
        {
            return null;
        }

        TreeNode left = InorderBalanceBST(root.Left, arr);

        arr.Add(root);

        TreeNode right = InorderBalanceBST(root.Right, arr);

        return root;
    }

    private TreeNode BalanceBST(List<TreeNode> arr, int start, int end)
    {
        if (start > end)
        {
            return null;
        }

        int mid = (end-start)/2 + start;

        TreeNode node = arr[mid];
        node.Left = BalanceBST(arr, start, mid-1);
        node.Right = BalanceBST(arr, mid+1, end);

        return node;
    }

    //https://leetcode.com/problems/binary-tree-right-side-view/
    public void RightSideView()
    {
        int?[] arr = new int?[] {1,2,3,null,5,null,4};
        var node = Helpers.ConstructTree(arr);
        List<int> res = new List<int>();
        RightSideView(node, res, 0);
    }

    private void RightSideView(TreeNode root, List<int> res, int level)
    {
        if (root == null)
        {
            return;
        }

        if (res.Count == level)
        {
            res.Add(root.Value.Value);
        }

        RightSideView(root.Right, res, level+1);
        RightSideView(root.Left, res, level+1);

        return;
    }

    //https://leetcode.com/problems/delete-nodes-and-return-forest/
    public void DeleteNodes()
    {
        TreeNode node = new TreeNode(1);
        node.Left = new TreeNode(2);
        node.Right = new TreeNode(3);
        node.Left.Left = new TreeNode(4);
        node.Left.Right = new TreeNode(5);
        node.Right.Left = new TreeNode(6);
        node.Right.Right = new TreeNode(7);

        HashSet<int> set = new HashSet<int>();
        set.Add(3);
        set.Add(5);
        var list = new List<TreeNode>();
        list.Add(node);
        var res = DeleteNodes(node, set, list);
    }

    private TreeNode DeleteNodes(TreeNode root, HashSet<int> to_delete, IList<TreeNode> res)
    {
        if (root == null)
        {
            return null;
        }

        root.Left = DeleteNodes(root.Left, to_delete, res);
        root.Right = DeleteNodes(root.Right, to_delete, res);

        if (to_delete.Contains(root.Value.Value))
        {
            if (root.Left != null)
            {
                res.Add(root.Left);
            }

            if (root.Right != null)
            {
                res.Add(root.Right);
            }

            return null;
        }

        return root;
    }

    //https://leetcode.com/contest/weekly-contest-174/problems/maximum-product-of-splitted-binary-tree/
    public void MaxProductOfSplittedBinaryTree()
    {
        TreeNode node = new TreeNode(1);
        node.Right = new TreeNode(2);
        node.Right.Left = new TreeNode(3);
        node.Right.Right = new TreeNode(4);
        node.Right.Right.Left = new TreeNode(5);
        node.Right.Right.Right = new TreeNode(6);

        int sum = 21;
        int min = int.MaxValue;
        MaxProductOfSplittedBinaryTree(node, sum, ref min);
        int val = (sum + min) /2;
        Console.WriteLine(val);
        Console.WriteLine(sum-val);
    }

    private int MaxProductOfSplittedBinaryTree(TreeNode node, int sum, ref int min)
    {
        if (node == null)
        {
            return 0;
        }

        int left = MaxProductOfSplittedBinaryTree(node.Left, sum, ref min);
        int right = MaxProductOfSplittedBinaryTree(node.Right, sum, ref min);

        int lowest = Math.Min(Math.Min(Math.Abs(sum - (left + node.Value.Value)), Math.Abs(sum - (right + node.Value.Value))),
         Math.Abs(sum - (left + right + node.Value.Value)));

        int val1 = sum - lowest;
        int diff = Math.Abs(val1 - lowest);

        min = Math.Min(min, diff);

        return left + right + node.Value.Value;
    }

    //Accepted-LCMedium-SelfSol-T:O(n):S:O(1) https://leetcode.com/problems/longest-zigzag-path-in-a-binary-tree/
    public void LongestZigZag()
    {
        //int?[] arr = new int?[]{1,2,3,null,4,null,null,5,6,null,7};
        int?[] arr = new int?[]{1,null,2,3,4,null,null,5,6,null,7,null,null,null,8,null,9};
        TreeNode root = Helpers.ConstructTree(arr);

        int max =0;
        LongestZigZag(root, null, ref max);
        Console.WriteLine(max-1);
    }

    private int LongestZigZag(TreeNode root, bool? isLeft, ref int max)
    {
        if (root == null)
        {
            return 0;
        }

        int left = LongestZigZag(root.Left, true, ref max);
        int right = LongestZigZag(root.Right, false, ref max);
        int cur = 0;

        if (isLeft == null)
        {
            max = 1+ (Math.Max(max, Math.Max(left, right)));
        }
        else
        {
            cur = 1 + (isLeft.Value == true ? right: left);
            max = Math.Max(max, cur);
        }

        return cur;
    }

    //Accepted-LcMedium-SelfSol-T:O(n)-S:O(n) https://leetcode.com/problems/binary-tree-pruning/
    public void PruneTree()
    {
        int?[] arr = new int?[] {1,null,0,0,1};

        var node = Helpers.ConstructTree(arr);
        var res = PruneTree(node);
    }

    public TreeNode PruneTree(TreeNode root) 
    {
        if (root == null)
        {
            return null;
        }
        
        TreeNode left = PruneTree(root.Left);
        TreeNode right = PruneTree(root.Right);
        
        if (root.Left != null && left == null)
        {
            root.Left = null;
        }
        
        if (root.Right != null && right == null)
        {
            root.Right = null;
        }
        
        if (root.Left == null && root.Right == null && root.Value.Value == 0)
        {
            root = null;
        }
        
        return root;
    }

    //https://leetcode.com/problems/sum-root-to-leaf-numbers/
    public void SumNumbers()
    {
        int?[] arr = new int?[] {0,1};
        var node = Helpers.ConstructTree(arr);
        int res = 0;
        SumNumbers(node, 0, ref res);
        Console.WriteLine(res);
    }

    private void SumNumbers(TreeNode root, int curSum, ref int res)
    {
        if (root == null)
        {
            return;
        }

        if (root.Left == null && root.Right == null)
        {
            res += curSum + root.Value.Value;
            return;
        }

        int val = curSum + root.Value.Value;
        
        SumNumbers(root.Left, val*10, ref res);
        SumNumbers(root.Right, val*10, ref res);

        return;
    }

    //Accepted-LcMedium-LcSol:T:O(h)-S:O(1) https://leetcode.com/problems/binary-tree-coloring-game/
    public void BtreeGameWinningMove()
    {
        int left = 0, right =0;
        int n = 11, x = 3;
        int?[] arr = new int?[]{1,2,3,4,5,6,7,8,9,10,11};
        var  root = Helpers.ConstructTree(arr);
        BtreeGameWinningMove(root, n, x, ref left, ref right);

        Console.WriteLine(Math.Max(Math.Max(left, right), n-left-right-1) > n/2);
    }

    private int BtreeGameWinningMove(TreeNode root, int n, int x, ref int left, ref int right)
    {
        if (root == null)
        {
            return 0;
        }

        int l = BtreeGameWinningMove(root.Left, n, x, ref left, ref right);
        int r = BtreeGameWinningMove(root.Right, n, x, ref left, ref right);

        if (root.Value.Value == x)
        {
            left = l;
            right = r;
        }

        return 1 + l + r;
    }

    //Accepted-LCMedium-SelfSol-T:O(n)-S:O(1) https://leetcode.com/problems/diameter-of-n-ary-tree/
    public void DiameterOfNAryTree()
    {
        // TreeNode node= new TreeNode(1);
        // TreeNode node3 = new TreeNode(3);
        // TreeNode node2 = new TreeNode(2);
        // TreeNode node4 = new TreeNode(4);
        // node.Children.Add(node3);
        // node.Children.Add(node2);
        // node.Children.Add(node4);

        // node3.Children.Add(new TreeNode(5));
        // node3.Children.Add(new TreeNode(6));

        TreeNode node = new TreeNode(3);
        node.Children.Add(new TreeNode(1));
        node.Children.Add(new TreeNode(5));

        int max = int.MinValue;

        Console.WriteLine(DiameterOfNAryTree(node, ref max));
    }

    private int DiameterOfNAryTree(TreeNode node, ref int max)
    {
        if (node == null)
        {
            return 0;
        }

        int first = 0;
        int second = 0;

        foreach(TreeNode child in node.Children)
        {
            int res = DiameterOfNAryTree(child, ref max) + 1;
            if (first < res)
            {
                second = first;
                first = res;
            }
            else if (second < res)
            {
                second = res;
            }
        }

        max = Math.Max(max, first + second);

        return first;
    }

    //https://leetcode.com/problems/minimum-time-to-collect-all-apples-in-a-tree/
    public void MinTimeToPickApples()
    {
        int[][] edges = new int[][]
        {
            // new int[] {0,1},
            // new int[] {0,2},
            // new int[] {1,4},
            // new int[] {1,5},
            // new int[] {2,3},
            // new int[] {2,6},

            new int[] {0,2},
            new int[] {0,3},
            new int[] {1,2}
        };

        List<bool> hasApple = new List<bool>() {false,true,false,false};
        int n = 4;

        Console.WriteLine(MinTimeToPickApples(n, edges, hasApple));
    }

    private int MinTimeToPickApples(int n, int[][] edges, IList<bool> hasApple)
    {
        HashSet<string> visited = new HashSet<string>();
        Dictionary<int, int> parentMap = new Dictionary<int, int>();

        foreach(int[] edge in edges)
        {
            if (!parentMap.ContainsKey(edge[1]))
            {
                parentMap.Add(edge[1], edge[0]);
            }

            parentMap[edge[1]] = edge[0];
        }

        int count = 0;

        for(int idx = 0; idx < hasApple.Count; idx++)
        {
            if (hasApple[idx])
            {
                count += GetCount(idx, parentMap, visited);
            }
        }

        return count;
    }

    private int GetCount(int idx, Dictionary<int, int> parentMap, HashSet<string> visited)
    {
        int count = 0;

        string cur = string.Empty;

        while (parentMap.ContainsKey(idx))
        {
            cur = idx + "_" + parentMap[idx];

            if (visited.Contains(cur))
            {
                break;
            }

            visited.Add(cur);
            count += 2;
            idx = parentMap[idx];
        }

        return count;
    }

    //Accepted:LcMedium-LcSol-T:O(nlogn)-S:O(n) https://leetcode.com/problems/construct-binary-tree-from-inorder-and-postorder-traversal/
    public void BinaryTreeFromInorderPostOrder()
    {
        int[] inorder = new int[]{9,3,15,20,7};
        int[] postOrder = new int[] {9,15,7,20,3};

        int idx = postOrder.Length-1;
        var res = BinaryTreeFromInorderPostOrder(inorder, postOrder, 0, inorder.Length-1, ref idx);
    }

    private TreeNode BinaryTreeFromInorderPostOrder(int[] inorder, int[] postorder, int start, int end, ref int idx)
    {
        if (idx < 0 || start > end)
        {
            return null;
        }

        TreeNode root = new TreeNode(postorder[idx--]);

        if (start == end)
        {
            return root;
        }

        var inIdx = FindInorderIndex(inorder, root.Value.Value, start, end);
        root.Right = BinaryTreeFromInorderPostOrder(inorder, postorder, inIdx+1, end, ref idx);
        root.Left = BinaryTreeFromInorderPostOrder(inorder, postorder, start, inIdx-1, ref idx);

        return root;
    }

    private int FindInorderIndex(int[] inorder, int val, int start, int end)
    {
        int idx = start;

        while (idx <= end && val != inorder[idx])
        {
            idx++;
        }

        return idx;
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

    //Accepted-LCMedium-SelfSol-T:O(n)-S:O(n) https://leetcode.com/problems/binary-search-tree-iterator/
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

        while(iterator.HasNext())
        {
            Console.WriteLine(iterator.Next());
        }
    }

    public class BSTIterator
    {
        Stack<TreeNode> stk = new Stack<TreeNode>();
        TreeNode cur = null;

        public BSTIterator(TreeNode root)
        {
            Queue(root);
        }

        public int Next()
        {
            cur = stk.Pop();
            Queue(cur.Right);

            return cur.Value.Value;
        }

        public bool HasNext()
        {
            return stk.Count > 0;
        }

        private void Queue(TreeNode node)
        {
            while(node != null)
            {
                stk.Push(node);
                node = node.Left;
            }
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

    //Accepted: https://leetcode.com/problems/range-sum-of-bst/
    public void RangeSumBST()
    {
        TreeNode node = new TreeNode(10);
        node.Left = new TreeNode(5);
        node.Right = new TreeNode(15);
        node.Left.Left = new TreeNode(3);
        node.Left.Right = new TreeNode(7);
        node.Right.Right = new TreeNode(18);

        Console.WriteLine(RangeSumBSTCre(node, 7, 15));
    }

    private int RangeSumBSTCre(TreeNode root, int low, int high)
    {
        if (root == null)
        {
            return 0;
        }
        
        int left = RangeSumBST(root.Left, low, high);
        int right = RangeSumBST(root.Right, low, high);

        int rval = root.Value > low && root.Value < high ? root.Value.Value : 0;
        return rval + left + right;
    }

    private int RangeSumBST(TreeNode node, int L, int R)
    {
        int sum = 0;

        if (node == null)
        {
            return sum;
        }

        sum+= node.Value.Value >= L && node.Value.Value <= R ? node.Value.Value : 0;

        if (node.Value.Value < L || (node.Value.Value >= L && node.Value.Value <=R))
        {
            sum+= RangeSumBST(node.Right, L, R);
        }

        if (node.Value.Value > R || (node.Value.Value >= L && node.Value.Value <=R))
        {
            sum+= RangeSumBST(node.Left, L, R);
        }

        return sum;
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

    public void DeleteNode()
    {
        TreeNode node = new TreeNode(19);
        node.Left = new TreeNode(7);
        node.Left.Left = new TreeNode(2);
        node.Left.Right = new TreeNode(8);
        node.Left.Left.Right = new TreeNode(14);
        node.Left.Left.Right.Right = new TreeNode(16);
        node.Left.Right = new TreeNode(8);
        node.Left.Right.Right = new TreeNode(17);
        node.Right = new TreeNode(43);

        var res = DeleteNode(node, 7);
    }

    private TreeNode DeleteNode(TreeNode node ,int val)
    {
        if (node == null)
        {
            return null;
        }

        if (val > node.Value.Value)
        {
            node.Right = DeleteNode(node.Right, val);
        }
        else if (val < node.Value.Value)
        {
            node.Left = DeleteNode(node.Left, val);
        }
        else
        {
            if (node.Left == null && node.Right == null)
            {
                return null;
            }
            else if (node.Right == null && node.Left != null)
            {
                return node.Left;
            }
            else if (node.Right != null && node.Left == null)
            {
                return node.Right;
            }
            else
            {
                TreeNode big = FindBiggestLeftNode(node.Left);
                DeleteNode(node.Left, big.Value.Value);
                big.Left = node.Left;
                big.Right = node.Right;
                return big;
            }
        }

        return node;
    }

    private TreeNode FindBiggestLeftNode(TreeNode node)
    {
        TreeNode cur = node;

        while (cur.Right!= null)
        {
            cur = cur.Right;
        }

        return cur;
    }

    //Accepted: https://leetcode.com/problems/validate-binary-search-tree/
    public void ValidateBST()
    {
        TreeNode node= new TreeNode(5);
        node.Left = new TreeNode(1);
        node.Right = new TreeNode(6);
        node.Left.Right = new TreeNode(4);
        node.Left.Right.Left = new TreeNode(3);

        Console.WriteLine(ValidateBST(node, long.MinValue, long.MaxValue));
    }
    private bool ValidateBST(TreeNode root, long min, long max)
    {
        if (root == null)
        {
            return true;
        }

        if (root.Value.Value <= min || root.Value.Value >= max)
        {
            return false;
        }

        return ValidateBST(root.Left, min, root.Value.Value) && ValidateBST(root.Right, root.Value.Value, max);
    }

    //https://leetcode.com/problems/populating-next-right-pointers-in-each-node-ii/
    public void PopulateNextPointers()
    {
        TreeNode node = new TreeNode(1);
        node.Left = new TreeNode(2);
        node.Right = new TreeNode(3);
        node.Left.Left = new TreeNode(4);
        node.Left.Right = new TreeNode(5);
        node.Right.Right = new TreeNode(7);

        var res = PopulateNextPointers(node);
    }

    private TreeNode PopulateNextPointers(TreeNode node)
    {
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(node);

        TreeNode prev = null;

        int size = 1;

        while (queue.Count > 0)
        {
            size = queue.Count;
            prev = null;

            while(size > 0)
            {
                var cur  = queue.Dequeue();

                if (prev!= null)
                {
                    prev.NextRight = cur;
                }

                if (cur.Left != null)
                {
                    queue.Enqueue(cur.Left);
                }
                if (cur.Right != null)
                {
                    queue.Enqueue(cur.Right);
                }

                size --;
                prev = cur;
            }
        }

        return node;
    }

    //https://leetcode.com/contest/biweekly-contest-21/problems/longest-zigzag-path-in-a-binary-tree/
    public void LongestZigZagPathInBinaryTree()
    {
        TreeNode node = new TreeNode(1);
        node.Right = new TreeNode(1);
        node.Right.Left = new TreeNode(1);
        node.Right.Right = new TreeNode(1);
        node.Right.Right.Left = new TreeNode(1);
        node.Right.Right.Right = new TreeNode(1);
        node.Right.Right.Left.Right = new TreeNode(1);
        node.Right.Right.Left.Right.Right = new TreeNode(1);

        int max = 0;
        LongestZigZagPathInBinaryTree(node.Right, false, ref max);
        Console.WriteLine(max);
    }

    private int LongestZigZagPathInBinaryTree(TreeNode node, bool left, ref int max)
    {
        if (node == null)
        {
            return 0;
        }

        int l = LongestZigZagPathInBinaryTree(node.Left, true, ref max);
        int r = LongestZigZagPathInBinaryTree(node.Right, false, ref max);

        int res = left == true ? r + 1 : l + 1;
        max = Math.Max(max, res);
        return res;
    }

    //https://leetcode.com/problems/binary-tree-zigzag-level-order-traversal/
    public void ZigzagLevelOrder()
    {
        int?[] arr = new int?[] {3,9,20,null,null,15,7};
        TreeNode node = Helpers.ConstructTree(arr);
        var res = ZigzagLevelOrder(node);
    }

    private IList<IList<int>> ZigzagLevelOrder(TreeNode root)
    {
        Stack<TreeNode> s1 = new  Stack<TreeNode>();
        Stack<TreeNode> s2 = new  Stack<TreeNode>();
        
        IList<IList<int>> res = new List<IList<int>>();
        
        if (root == null)
        {
            return res;
        }
        
        s1.Push(root);
        
        while (s1.Count > 0 || s2.Count > 0)
        {
            TreeNode cur = null;
            var list = new List<int>();
            
            while (s1.Count > 0)
            {
                cur = s1.Pop();
                list.Add(cur.Value.Value);
                
                if (cur.Left != null)
                {
                    s2.Push(cur.Left);
                }
                
                if (cur.Right != null)
                {
                    s2.Push(cur.Right);
                }
            }
            
            if (list.Count > 0)
            {
                res.Add(list);
            }
            
            var stkList = new List<int>();
            
            while (s2.Count > 0)
            {
                cur = s2.Pop();
               
                if (cur.Right != null)
                {
                    s1.Push(cur.Right);
                }
                
                if (cur.Left != null)
                {
                    s1.Push(cur.Left);
                }
                
                stkList.Add(cur.Value.Value);
            }
            
            if (stkList.Count > 0)
            {
                res.Add(stkList);
            }
        }
        
        return res;
    }

    //https://leetcode.com/contest/weekly-contest-169/problems/all-elements-in-two-binary-search-trees/
    public void AllElementsOfBST()
    {
        IList<int> res = new List<int>();
        TreeNode node = new TreeNode(2);
        node.Left = new TreeNode(1);
        node.Right = new TreeNode(4);

        TreeNode second = new TreeNode(1);
        second.Left = new TreeNode(0);
        second.Right = new TreeNode(3);

        var node1 = new TreeNodeIterator(null);
        TreeNode enumerator1 = node1.Next();

        var node2 = new TreeNodeIterator(second);
        TreeNode enumerator2 = node2.Next();

        while(enumerator1 != null && enumerator2 != null)
        {
            if(enumerator1.Value.Value < enumerator2.Value.Value)
            {
                res.Add(enumerator1.Value.Value);
                enumerator1 = node1.Next();
            }
            else
            {
                res.Add(enumerator2.Value.Value);
                enumerator2 = node2.Next();
            }
        }

        while(enumerator1 != null)
        {
            Console.WriteLine(enumerator1.Value.Value);
            enumerator1 = node1.Next();
        }

        while(enumerator2 != null)
        {
            Console.WriteLine(enumerator2.Value.Value);
            enumerator2 = node2.Next();
        }
    }

    //Accepted: T:O(n) https://leetcode.com/problems/delete-leaves-with-a-given-value/
    public void DeleteLeavesWithGivenValue()
    {
        TreeNode node = new TreeNode(1);
        node.Left = new TreeNode(2);
        node.Right = new TreeNode(3);
        node.Left.Left = new TreeNode(2);
        node.Right.Left = new TreeNode(2);
        node.Right.Right = new TreeNode(4);

        var res = DeleteLeavesWithGivenValue(node, 2);
    }

    private TreeNode DeleteLeavesWithGivenValue(TreeNode node, int val)
    {
        if (node == null )
        {
            return null;
        }

        node.Left = DeleteLeavesWithGivenValue(node.Left, val);
        node.Right = DeleteLeavesWithGivenValue(node.Right, val);

        if (node.Left == null && node.Right == null && node.Value.Value == val)
        {
            node = null;
        }

        return node;
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

    //Accepted: https://leetcode.com/problems/construct-binary-tree-from-preorder-and-postorder-traversal/
    public void ConstructBinaryTreeFromPreAndPostOrder()
    {
        int[] pre = new int[] {1,2,4,5,3,6,7};
        int[] post = new int[] {4,5,2,6,7,3,1};
        int preIdx = 0;
        int postIdx = 0;

        var res = ConstructBinaryTreeFromPreAndPostOrder(pre, post, ref preIdx, ref postIdx);
    }

    private TreeNode ConstructBinaryTreeFromPreAndPostOrder(int[] pre, int[] post, ref int preIdx, ref int postIdx)
    {
        TreeNode root = new TreeNode(pre[preIdx++]);

        if (root.Value != post[postIdx])
        {
            root.Left = ConstructBinaryTreeFromPreAndPostOrder(pre, post, ref preIdx, ref postIdx);
        }

        if (root.Value != post[postIdx])
        {
            root.Right = ConstructBinaryTreeFromPreAndPostOrder(pre, post, ref preIdx, ref postIdx);
        }

        postIdx++;
        return root;
    }

    //Accepted: T:O(n), S:O(n): https://leetcode.com/problems/binary-tree-right-side-view/
    public void BinaryTreeRightSideView()
    {
        TreeNode node= new TreeNode(1);
        node.Left = new TreeNode(2);
        node.Left.Right = new TreeNode(5);
        node.Right = new TreeNode(3);

        var res = BinaryTreeRightSideView(node, new List<int>(), 0);
    }

    private IList<int> BinaryTreeRightSideView(TreeNode root, IList<int> res, int level)
    {
        if (root == null)
        {
            return res;
        }

        if (level == res.Count)
        {
            res.Add(root.Value.Value);
        }

        BinaryTreeRightSideView(root.Right, res, level+1);
        BinaryTreeRightSideView(root.Left, res, level+1);

        return res;
    }

    //https://leetcode.com/problems/boundary-of-binary-tree/
    public void BinaryTreeBoundary()
    {
        TreeNode root = new TreeNode(1);
        root.Left = new TreeNode(2);
        root.Left.Left = new TreeNode(4);
        root.Left.Left.Left = new TreeNode(8);
        root.Left.Left.Right = new TreeNode(9);
        root.Left.Right = new TreeNode(5);
        root.Left.Right.Left = new TreeNode(10);
        root.Left.Right.Right = new TreeNode(11);
        root.Right = new TreeNode(3);
        root.Right.Left = new TreeNode(6);
        root.Right.Right = new TreeNode(7);
        root.Right.Left.Left = new TreeNode(12);

        var res = new List<int>();
        res.Add(root.Value.Value);
        BinaryTreeLeftBoundary(root.Left, res);
        BinaryTreeLeavesBoundary(root.Left, res);
        BinaryTreeLeavesBoundary(root.Right, res);
        BinaryTreeRightBoundary(root.Right, res);
    }

    private IList<int> BinaryTreeLeftBoundary(TreeNode root, IList<int> res)
    {
        if (root == null || root.Left == null || root.Right == null)
        {
            return res;
        }

        res.Add(root.Value.Value);

        if (root.Left == null)
        {
            BinaryTreeLeftBoundary(root.Right, res);
        }
        else
        {
            BinaryTreeLeftBoundary(root.Left, res);
        }

        return res;
    }

    private void BinaryTreeLeavesBoundary(TreeNode root, IList<int> res)
    {
        if (root == null)
        {
            return;
        }

        if (root.Left == null && root.Right == null)
        {
            res.Add(root.Value.Value);
        }

        BinaryTreeLeavesBoundary(root.Left, res);
        BinaryTreeLeavesBoundary(root.Right, res);

        return;
    }

    private void BinaryTreeRightBoundary(TreeNode root, IList<int> res)
    {
        if (root == null || root.Left == null && root.Right == null)
        {
            return;
        }

        if (root.Right == null)
        {
            BinaryTreeRightBoundary(root.Left, res);
        }
        else
        {
            BinaryTreeRightBoundary(root.Right, res);
        }

        res.Add(root.Value.Value);

        return;
    }

    //Accepted: T:O(n), S: O(n): https://leetcode.com/problems/kth-smallest-element-in-a-bst/
    public void KthSmallestInBst()
    {
        // TreeNode node = new TreeNode(5);
        // node.Left = new TreeNode(3);
        // node.Right = new TreeNode(6);
        // node.Left.Left = new TreeNode(2);
        // node.Left.Right = new TreeNode(4);
        // node.Left.Left.Left = new TreeNode(1);

        TreeNode node = new TreeNode(1);
        node.Right = new TreeNode(2);

        int k = 2;

        Console.WriteLine(KthSmallestInBst(node, ref k));
    }

    private int KthSmallestInBst(TreeNode root, ref int k)
    {
        int res = -1;
        
        if (root == null)
        {
            return res;
        }
        
        res = KthSmallestInBst(root.Left, ref k);
        k--;

        if (k == 0)
        {
            return root.Value.Value;
        }

        if (k > 0)
        {
            res = KthSmallestInBst(root.Right, ref k);
        }
        
        return res;
    }

    //Accepted:LcMedium:LCSol:T:O(Logn^2)S:O(1) https://leetcode.com/problems/count-complete-tree-nodes/
    public void CountNodes()
    {
        int?[] arr = new int?[] {1,2,3,4,5,6};
        TreeNode node = Helpers.ConstructTree(arr);
        var res = CountNodes(node);
    }

    private int CountNodes(TreeNode root)
    {
        int left = HeightOfLeftTree(root);
        int right = HeightOfRightTree(root);

        if (left == right)
        {
            return (int)Math.Pow(2, left)-1;
        }

        var res = CountNodes(root.Left) + CountNodes(root.Right) + 1;
        return res;
    }

    private int HeightOfLeftTree(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }

        int left = HeightOfLeftTree(root.Left) + 1;

        return left;
    }

    private int HeightOfRightTree(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }

        int right = HeightOfRightTree(root.Right) + 1;

        return right;
    }

    //Accepted: https://leetcode.com/problems/sum-of-nodes-with-even-valued-grandparent/
    public void EvenValuedGrandParent()
    {
        TreeNode node = new TreeNode(6);
        node.Left = new TreeNode(7);
        node.Right = new TreeNode(8);
        node.Left.Left = new TreeNode(2);
        node.Left.Right = new TreeNode(7);
        node.Right.Left = new TreeNode(1);
        node.Right.Right = new TreeNode(3);
        node.Left.Left.Left = new TreeNode(9);
        node.Left.Right.Left = new TreeNode(1);
        node.Left.Right.Right = new TreeNode(4);
        node.Right.Right.Right = new TreeNode(5);

        int count=0;
        var res = EvenValuedGrandParent(node, null, null);
        Console.WriteLine(count);
    }

    private int EvenValuedGrandParent(TreeNode node, TreeNode parent, TreeNode grandParent)
    {
        int count = 0;

        if (node == null)
        {
            return 0;
        }

        if (grandParent != null && grandParent.Value.Value % 2 == 0)
        {
            count += node.Value.Value;
        }

        int left = EvenValuedGrandParent(node.Left, node, parent);
        int right = EvenValuedGrandParent(node.Right, node, parent);

        return left + right + count;
    }

    //https://leetcode.com/contest/biweekly-contest-26/problems/count-good-nodes-in-binary-tree/
    public void GoodNodes()
    {
        TreeNode node = new TreeNode(3);
        node.Left = new TreeNode(1);
        node.Left.Left = new TreeNode(3);
        node.Right = new TreeNode(4);
        node.Right.Left = new TreeNode(1);
        node.Right.Right = new TreeNode(5);

        int count = 0;
        GoodNodes(node, int.MinValue, ref count);
        Console.WriteLine(count);
    }

    private void GoodNodes(TreeNode node, int maxTillNow, ref int count)
    {
        if (node == null)
        {
            return;
        }

        if (node.Value.Value >= maxTillNow)
        {
            count++;
        }

        maxTillNow = Math.Max(node.Value.Value, maxTillNow);

        GoodNodes(node.Left, maxTillNow, ref count);
        GoodNodes(node.Right, maxTillNow, ref count);

        return;
    }

    //https://leetcode.com/problems/largest-bst-subtree/
    public void LargestBSTSubtree()
    {
        int?[] arr = new int?[]{3, 2, 4, null, null, 1};

        TreeNode root = Helpers.ConstructTree(arr);

        var res = LargestBSTSubtree(root);
        Console.WriteLine(res[2]);
    }

    private int[] LargestBSTSubtree(TreeNode node)
    {
        // return array for each node: 
        //     [0] --> min
        //     [1] --> max
        //     [2] --> largest BST in its subtree(inclusive)

        if (node == null)
        {
            return new int[]{int.MaxValue, int.MinValue, 0};
        }
        
        int[] left = LargestBSTSubtree(node.Left);
        int[] right = LargestBSTSubtree(node.Right);
        
        if(node.Value > left[1] && node.Value < right[0])
        {
            return new int[]{ Math.Min(node.Value.Value, left[0]), Math.Max(node.Value.Value, right[1]), left[2] + right[2] + 1};
        }
        else
        {
            return new int[]{int.MinValue, int.MaxValue, Math.Max(left[2], right[2])};
        }
    }

    //Accepted: T:O(n), S:O(n): https://leetcode.com/problems/find-largest-value-in-each-tree-row/
    public void LargestValues()
    {
        TreeNode node= new TreeNode(1);
        node.Left = new TreeNode(3);
        node.Right = new TreeNode(2);
        node.Left.Left = new TreeNode(5);
        node.Left.Right = new TreeNode(3);
        node.Right.Right = new TreeNode(9);

        var res = LargestValues(node);
    }

    private IList<int> LargestValues(TreeNode root)
    {
        IList<int> res = new List<int>();

        if (root == null)
        {
            return res;
        }

        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while(queue.Count > 0)
        {
            int count = queue.Count;
            int val = int.MinValue;

            while(queue.Count > 0 && count > 0)
            {
                var item = queue.Dequeue();
                val = Math.Max(item.Value.Value, val);
                count--;

                if (item.Left != null)
                {
                    queue.Enqueue(item.Left);
                }

                if (item.Right != null)
                {
                    queue.Enqueue(item.Right);
                }
            }

            res.Add(val);
        }

        return res;
    }

    //Accepted:LCMedium:Self:T:O(n):S:O(1):https://leetcode.com/problems/maximum-difference-between-node-and-ancestor/
    public void MaxAncestorDiff()
    {
        // TreeNode node = new TreeNode(8);
        // node.Left = new TreeNode(3);
        // node.Left.Left = new TreeNode(1);
        // node.Left.Right = new TreeNode(6);
        // node.Left.Right.Left = new TreeNode(4);
        // node.Left.Right.Right = new TreeNode(7);
        // node.Right = new TreeNode(10);
        // node.Right.Right = new TreeNode(14);
        // node.Right.Right.Left = new TreeNode(13);

        TreeNode node = new TreeNode(1);
        node.Right = new TreeNode(2);
        node.Right.Right = new TreeNode(0);
        node.Right.Right.Left  = new TreeNode(3);

        Console.WriteLine(MaxAncestorDiff(node, int.MinValue, int.MaxValue));
    }

    private int MaxAncestorDiff(TreeNode node, int max, int min)
    {
        max = Math.Max(max, node.Value.Value);
        min = Math.Min(min, node.Value.Value);

        int diff = max - min;
        int curDiff = 0;

        if (node.Left != null)
        {
            curDiff = MaxAncestorDiff(node.Left, max, min);
        }

        if (node.Right != null)
        {
            curDiff = Math.Max(curDiff, MaxAncestorDiff(node.Right, max, min));
        }

        return diff > curDiff ? diff : curDiff;
    }


    //https://leetcode.com/problems/find-nearest-right-node-in-binary-tree/
    public void FindNearestRightNode()
    {
        // TreeNode node= new TreeNode(1);
        // node.Left = new TreeNode(2);
        // node.Right = new TreeNode(3);
        // node.Left.Right = new TreeNode(4);
        // node.Right.Left = new TreeNode(5);
        // node.Right.Right = new TreeNode(6);

        TreeNode node= new TreeNode(3);
        node.Right = new TreeNode(4);
        node.Right.Left = new TreeNode(2);

        int curLevel = 0;
        int uLevel = -1;
        TreeNode rightNeighbor = null;
        FindNearestRightNode(node, node.Right.Left, curLevel, ref uLevel, ref rightNeighbor);
        Console.WriteLine(rightNeighbor.Value);
    }

    private TreeNode FindNearestRightNode(TreeNode root, TreeNode u, int curLevel, ref int ulevel, ref TreeNode rightNeighbor)
    {
        if (root == null)
        {
            return null;
        }

        if (ulevel != -1 && curLevel == ulevel && rightNeighbor == null)
        {
            rightNeighbor = root;
            return root;
        }

        if (root == u)
        {
            ulevel = curLevel;
        }

        TreeNode left = FindNearestRightNode(root.Left, u, curLevel + 1, ref ulevel, ref rightNeighbor);
        TreeNode right = FindNearestRightNode(root.Right, u, curLevel + 1, ref ulevel, ref rightNeighbor);

        return root;
    }

    //https://leetcode.com/problems/convert-sorted-list-to-binary-search-tree/
    public void SortedListToBST()
    {

    }

    private TreeNode SortedListToBST(SLLNode head, SLLNode end)
    {
        if (head == null || end == null)
        {
            return null;
        }

        SLLNode mid = FindMiddleNode(head, end);

        TreeNode node = new TreeNode(mid.Value);
        node.Left = SortedListToBST(head, mid);
        node.Right = SortedListToBST(mid.Next, end);

        return node;
    }

    private SLLNode FindMiddleNode(SLLNode head, SLLNode end)
    {
        SLLNode slow = head;
        SLLNode fast = head;

        while (fast.Next.Next != null)
        {
            slow = slow.Next;
            fast = fast.Next.Next;
        }

        return slow;
    }

    //https://leetcode.com/problems/balance-a-binary-search-tree/
    public void BalanceBST()
    {
        TreeNode node = new TreeNode(1);
        node.Right = new TreeNode(2);
        node.Right.Right = new TreeNode(3);
        node.Right.Right.Right = new TreeNode(4);
        node.Right.Right.Right.Right = new TreeNode(5);

        List<int> arr = new List<int>();
        BalanceBST(node, arr);
        TreeNode res = SortedArrayToBST(arr, 0, arr.Count-1);
    }

    private void BalanceBST(TreeNode root, List<int> arr)
    {
        if (root == null)
        {
            return;
        }

        BalanceBST(root.Left, arr);
        arr.Add(root.Value.Value);
        BalanceBST(root.Right, arr);

        return;
    }

    private TreeNode SortedArrayToBST(List<int> arr, int start, int end)
    {
        if (start > end)
        {
            return null;
        }

        int mid = start + (end-start) / 2;

        TreeNode node = new TreeNode(arr[mid]);
        node.Left = SortedArrayToBST(arr, start, mid-1);
        node.Right = SortedArrayToBST(arr, mid+1, end);

        return node;
    }

    //https://leetcode.com/problems/maximum-average-subtree/
    public void MaximumAverageSubTree()
    {
        TreeNode node = new TreeNode(5);
        node.Left = new TreeNode(6);
        node.Right = new TreeNode(1);
        double max = double.MinValue;
        Console.WriteLine(MaximumAverageSubTree(node, ref max));
    }

    private double MaximumAverageSubTree(TreeNode root, ref double max)
    {
        if (root == null)
        {
            return 0.0;
        }

        double left = MaximumAverageSubTree(root.Left, ref max);
        double right = MaximumAverageSubTree(root.Right, ref max);

        int divisor = left == 0.0 ? 0 : 1;
        divisor += right == 0.0 ? 0 : 1;
        divisor += 1;
        double average = (root.Value.Value + left + right) / divisor;

        max = Math.Max(max, average);
        return average;
    }

    //Accepted-LcMedium-SelfSol-T:O(n) S:O(n) https://leetcode.com/problems/trim-a-binary-search-tree/
    public void TrimBST()
    {
        int?[] arr = new int?[] {3,0,4,null,2,null,null,1};
        var root = Helpers.ConstructTree(arr);
        int low = 1, high = 3;
        var res = TrimBST(root, low, high);
    }

    private TreeNode TrimBST(TreeNode root, int low, int high)
    {
        if (root == null)
        {
            return null;
        }

        if (root.Value.Value < low )
        {
            return TrimBST(root.Right, low, high);
        }
        else if (root.Value.Value > high)
        {
            return TrimBST(root.Left, low, high);
        }
        else
        {
            root.Left = TrimBST(root.Left, low, high);
            root.Right = TrimBST(root.Right, low, high);
        }

        return root;
    }

    //https://leetcode.com/discuss/interview-question/963428/google-phone-most-frequent-element-in-a-bst
    public void MostFrequentElementInBST()
    {
        TreeNode node = new TreeNode(50);
        node.Left = new TreeNode(40);
        node.Left.Left = new TreeNode(40);
        node.Left.Right = new TreeNode(40);
        node.Right = new TreeNode(58);
        node.Right.Left = new TreeNode(58);
        node.Right.Right = new TreeNode(62);

        int max = 0, count = 0, prev = 0, val = -1;
        Console.WriteLine(MostFrequentElementInBST(node, ref prev, ref count, ref max, ref val));
    }

    private int MostFrequentElementInBST(TreeNode node, ref int prev, ref int count, ref int max, ref int val)
    {
        if (node == null)
        {
            return val;
        }

        MostFrequentElementInBST(node.Left, ref prev, ref count, ref max, ref val);

        if (node.Left != null)
        {
            prev = node.Left.Value.Value;
        }

        if (node.Value.Value == prev)
        {
            count ++;
            max = Math.Max(max, count);
            val = max == count ? node.Value.Value : val;
        }
        else
        {
            count = 0;
        }

        MostFrequentElementInBST(node.Right, ref prev, ref count, ref max, ref val);

        return val;
    }

    //https://leetcode.com/problems/maximum-sum-bst-in-binary-tree/
    public void MaxSumBST()
    {
        // TreeNode node= new TreeNode(1);
        // node.Left = new TreeNode(4);
        // node.Right = new TreeNode(3);
        // node.Left.Left = new TreeNode(2);
        // node.Left.Right = new TreeNode(4);
        // node.Right.Left = new TreeNode(2);
        // node.Right.Right = new TreeNode(5);
        // node.Right.Right.Left = new TreeNode(4);
        // node.Right.Right.Right = new TreeNode(6);

        // TreeNode node=  new TreeNode(5);
        // node.Left = new TreeNode(4);
        // node.Right = new TreeNode(8);
        // node.Left.Left = new TreeNode(3);
        // node.Right.Left = new TreeNode(6);
        // node.Right.Right= new TreeNode(3);

        TreeNode node =  new TreeNode(1);
        node.Right= new TreeNode(10);
        node.Right.Left = new TreeNode(-5);
        node.Right.Right = new TreeNode(20);

        int max = 0;

        MaxSumBST(node, ref max);
        Console.WriteLine(max);
    }

    private int MaxSumBST(TreeNode root, ref int max)
    {
        if (root == null)
        {
            return 0;
        }

        int left = MaxSumBST(root.Left, ref max);
        int right = MaxSumBST(root.Right, ref max);

        if (left != -1 && right!= -1 && (root.Left != null && root.Value > root.Left.Value || left == 0) 
            && (root.Right != null && root.Value < root.Right.Value || right == 0))
        {
            int res = root.Value.Value + left + right;
            max = Math.Max(max, res);
            return res;
        }
        else if (left == 0 && right == 0)
        {
            max = Math.Max(max, root.Value.Value);
            return root.Value.Value;
        }

        return -1;
    }
}

public class TreeNode
{
    public TreeNode(int value) : this (value, null, null)
    {

    }

    public TreeNode(int value, TreeNode left, TreeNode right)
    {
        this.Value = value;
        this.Left = left;
        this.Right = right;
        this.Children = new List<TreeNode>();
    }

    public TreeNode Left;
    public TreeNode Right;
    public TreeNode NextRight;
    public TreeNode Parent;

    public List<TreeNode> Children;

    public int Rank;
    public int? Value;
}

public class TreeNodeWithSum
{
    public TreeNodeWithSum(int value)
    {
        this.Value = value;
    }

    public TreeNodeWithSum(int value, int smallerCount, int dupeCount)
    {
        this.Value = value;
        this.DupeCount = dupeCount;
        this.SmallerCount = smallerCount;
    }

    public TreeNodeWithSum Left;
    public TreeNodeWithSum Right;

    public int DupeCount;
    public int Value;

    public int SmallerCount;
}

public class TreeNodeIterator
{
    Stack<TreeNode> stk = null;
    TreeNode node = null;

    public TreeNodeIterator(TreeNode node)
    {
        stk = new Stack<TreeNode>();
        stk.Push(node);
        this.node = node;
        StackLeftNodes();
    }

    public TreeNode Next()
    {
        if (stk.Count == 0)
        {
            return null;
        }

        node = stk.Pop();
        TreeNode res = node;

        if (node!= null && node.Right != null)
        {
            stk.Push(node.Right);
            node = node.Right;
        }

        StackLeftNodes();

        return res;
    }

    private void StackLeftNodes()
    {
        while(node!= null && node.Left != null)
        {
            stk.Push(node.Left);
            node = node.Left;
        }
    }
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