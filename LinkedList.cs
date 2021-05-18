using System;
using System.Collections;
using System.Collections.Generic;
using DataStructures;
public class LinkedList
{
    //https://leetcode.com/problems/swap-nodes-in-pairs/
    //Given 1->2->3->4, you should return the list as 2->1->4->3.
    public void SwapAlternatePairs()
    {
        SLLNode head = new SLLNode(2);
        head.Next = new SLLNode(1);
        head.Next.Next = new SLLNode(5);
        head.Next.Next.Next = new SLLNode(8);
        head.Next.Next.Next.Next = new SLLNode(7);
        head.Next.Next.Next.Next.Next = new SLLNode(10);
        head.Next.Next.Next.Next.Next.Next = new SLLNode(12);
        head.Next.Next.Next.Next.Next.Next.Next = new SLLNode(14);

        SLLNode node = new SLLNode(1);
        node.Next = new SLLNode(2);
        node.Next.Next = new SLLNode(3);
        var res = SwapAlternatePairs(node);
    }

    private SLLNode SwapAlternatePairs(SLLNode head)
    {
        SLLNode cur = head.Next;
        SLLNode prev = head;
        SLLNode temp = null;

        prev.Next = cur.Next;
        cur.Next = prev;
        head = cur;

        while(cur.Next != null && prev.Next != null)
        {
            cur = prev.Next;
            prev.Next = cur.Next;
            prev = prev.Next;
            temp = prev.Next;
            prev.Next = cur;
            cur.Next = temp;
            prev = cur;
        }

        return head;
    }

    //Accepted:LCHard:LCSol:T:O(nLogk)-S:O(n) https://leetcode.com/problems/merge-k-sorted-lists/
    public void MergeKSortedList()
    {
        SLLNode node = new SLLNode(1);
        node.Next = new SLLNode(4);
        node.Next.Next = new SLLNode(5);

        SLLNode node2 = new SLLNode(1);
        node2.Next = new SLLNode(3);
        node2.Next.Next = new SLLNode(4);

        SLLNode node3 = new SLLNode(2);
        node3.Next = new SLLNode(6);

        SLLNode[] lists = new SLLNode[]
        {
            node3, node2, node
        };

        var res = MergeKSortedListPQueue(lists);
        //var res = Partition(lists, 0, lists.Length-1);
    }

    private SLLNode MergeKSortedListPQueue(SLLNode[] lists)
    {
        Heap<SLLNode> heap = new Heap<SLLNode>(true);

        foreach(SLLNode node in lists)
        {
            heap.Push(node);
        }

        SLLNode head = new SLLNode(0);
        SLLNode tail = head;

        while (heap.Count > 0)
        {
            var cur = heap.Pop();
            tail.Next = cur;
            tail = cur;

            if (cur.Next != null)
            {
                heap.Push(cur.Next);
            }
        }

        return head.Next;
    }

    private SLLNode Partition(SLLNode[] lists, int start, int end)
    {
        if (start == end)
        {
            return lists[start];
        }
        
        int mid = (end-start)/2 + start;

        var l1 = Partition(lists, start, mid);
        var l2 = Partition(lists, mid+1, end);

        var res = Merge(l1, l2);

        return res;
    }

    private SLLNode Merge(SLLNode l1, SLLNode l2)
    {
        SLLNode res = null;
        if (l1 == null)
        {
            return l2;
        }

        if (l2 == null)
        {
            return l1;
        }

        if (l1.Value > l2.Value)
        {
            l2.Next = Merge(l1, l2.Next);
            res = l2;
        }
        else
        {
            l1.Next = Merge(l1.Next, l2);
            res = l1;
        }

        return res;
    }

    private SLLNode MergeKSortedListUsingHeap(SLLNode[] lists)
    {
        Heap<SLLNode> heap = new Heap<SLLNode>(true);

        foreach(SLLNode node in lists)
        {
            if (node!= null)
            {
                heap.Push(node);
            }
        }

        SLLNode head = new SLLNode(0);
        SLLNode tail = head;

        while (heap.Count > 0)
        {
            var cur = heap.Pop();
            tail.Next = cur;
            tail = tail.Next;

            if (tail.Next != null)
            {
                heap.Push(tail.Next);
            }
        }

        return head.Next;
    }

    // 1->2->3->4
    // 4->3->2->1
    public void ReverseSLL()
    {
        SLLNode node = new SLLNode(1);
        node.Next = new SLLNode(2);
        node.Next.Next = new SLLNode(3);
        node.Next.Next.Next = new SLLNode(4);
    
        var res = ReverseSLL(node);
    }

    private SLLNode ReverseSLL(SLLNode node)
    {
        SLLNode prev = null;

        while (node != null)
        {
            var next = node.Next;
            node.Next = prev;
            prev = node;
            node = next;
        }

        return prev;
    }

    //https://leetcode.com/problems/palindrome-linked-list/
    public void IsPalindrome()
    {
        SLLNode node = new SLLNode(1);
        node.Next = new SLLNode(2);
        // node.Next.Next = new SLLNode(2);
        // node.Next.Next.Next = new SLLNode(1);
        var res = IsPalindrome(node);
    }

    private bool IsPalindrome(SLLNode head) 
    {
        var middle = FindMiddle(head);
       
        middle = Reverse(middle);
        
        while (middle != null)
        {
             Console.WriteLine("head is " + middle.Value);
             Console.WriteLine("middle is " + middle.Value);
            
            if (head.Value != middle.Value)
            {
                return false;
            }
            middle = middle.Next;
            head = head.Next;
        }
        
        return true;
    }
    
    private SLLNode FindMiddle(SLLNode node)
    {
        SLLNode slow = node;
        SLLNode fast = slow.Next.Next;
        
        while(fast != null)
        {
            Console.WriteLine("fast node " + fast.Value);
            Console.WriteLine("slow node " + slow.Value);
            fast = fast.Next.Next;
            slow = slow.Next;
        }
        
        return slow.Next;
    }

    private SLLNode Reverse(SLLNode node)
    {
        SLLNode prev = null;
        while (node != null)
        {
            var next = node.Next;
            node.Next = prev;
            prev = node;
            node = next;
        }

        return prev;
    }
    
    //https://leetcode.com/problems/minimum-cost-to-merge-stones/
    public void MinCostToMergeStones()
    {
        int[] stones = new int[]{6, 4, 4, 6};
        Console.WriteLine(MinCostToMergeStones(stones, 2));
    }

    private int MinCostToMergeStones(int[] arr, int k)
    {
        if (k > arr.Length)
        {
            return 0;
        }

        int kno = arr.Length / k + arr.Length % k;
        if (arr.Length % k != 0 && (kno) % k != 0)
        {
            return -1;
        }

        DLLNode left = null;
        DLLNode right = null;
        DLLNode temp = null;
        DLLNode cur = new DLLNode(arr[0]);
        DLLNode head = cur;

        int min = int.MaxValue;
        int minCost = 0;

        for(int idx = 1; idx < arr.Length; idx ++)
        {
            var node = new DLLNode(arr[idx]);
            cur.Next = node;
            node.Prev = cur;
            cur = node;
        }

        cur = head;
        int cost = 0;
        int length = arr.Length;

        while (head.Next != null)
        {
            cur = head;
            min = int.MaxValue;
            int len = length;

            while (cur.Next != null)
            {
                int count = 0;
                cost = 0;

                temp = cur;
                while (count < k)
                {
                    cost += cur.Value;
                    count ++;

                    if (cur.Next == null || count >= k)
                    {
                        break;
                    }

                    cur = cur.Next;
                }

                if (cost < min)
                {
                    left = temp;
                    right = cur;
                    min = cost;
                }

                if (len <= k)
                {
                    break;
                }

                cur = len >= k ? temp.Next : cur;
                len -=1;
            }

            minCost+= min;

            RemoveDllNodes(left, right, min);
            length-= k-1;
        }

        return minCost;
    }

    //https://leetcode.com/problems/flatten-a-multilevel-doubly-linked-list/
    public void FlattenList()
    {
        MultiLevelDLLNode node = new MultiLevelDLLNode(1);
        node.Next = new MultiLevelDLLNode(2);
        node.Next.Next = new MultiLevelDLLNode(3);
        node.Next.Next.Next = new MultiLevelDLLNode(4);
        node.Next.Next.Next.Next = new MultiLevelDLLNode(5);
        node.Next.Next.Next.Next.Next = new MultiLevelDLLNode(6);

        MultiLevelDLLNode child3 = new MultiLevelDLLNode(7);
        child3.Next = new MultiLevelDLLNode(8);
        child3.Next.Next = new MultiLevelDLLNode(9);
        child3.Next.Next.Next = new MultiLevelDLLNode(10);

        MultiLevelDLLNode child8 = new MultiLevelDLLNode(11);
        child8.Next = new MultiLevelDLLNode(12);

        node.Next.Next.Child = child3;
        child3.Next.Child = child8;

        var res = FlattenList(node);
    }

    private MultiLevelDLLNode FlattenList(MultiLevelDLLNode cur)
    {
        if (cur == null)
        {
            return null;
        }

        while(cur.Next != null)
        {
            while(cur.Child == null)
            {
                if (cur.Next == null)
                {
                    break;
                }
                cur = cur.Next;
            }

            if (cur.Child!= null)
            {
                MultiLevelDLLNode temp = cur.Next;
                cur.Next = cur.Child;
                var childLastNode = FlattenList(cur.Child);
                childLastNode = childLastNode == null ? cur.Next : childLastNode;
                childLastNode.Next = temp;
                cur = temp;
            }
        }

        return cur;
    }

    private void RemoveDllNodes(DLLNode left, DLLNode right, int val)
    {
        left.Value = val;
        left.Next = right.Next;
    }

    //Given a LinkedList and an integer k, remove the k-st element of the list from the end (1 pass)
    public void RemoveKthNode()
    {
        SLLNode node = new SLLNode(1);
        node.Next = new SLLNode(2);
        node.Next.Next = new SLLNode(3);
        node.Next.Next.Next = new SLLNode(4);
        node.Next.Next.Next.Next = new SLLNode(5);

        var res = RemoveKthNode(node, 4);
    }

    private SLLNode RemoveKthNode(SLLNode node, int k)
    {
        int slow = 1;
        int fast = 1;
        SLLNode slowNode = node;
        SLLNode fastNode = node;

        while (fast - slow +1 < k)
        {
            fastNode = fastNode.Next;
            fast++;
        }

        while (fastNode.Next != null)
        {
            fastNode = fastNode.Next;
            slowNode = slowNode.Next;
        }

        return slowNode;
    }

    //https://leetcode.com/contest/weekly-contest-151/problems/remove-zero-sum-consecutive-nodes-from-linked-list/
    public void RemoveZeroSumNodes()
    {
        SLLNode node = new SLLNode(1);
        node.Next = new SLLNode(3);
        node.Next.Next = new SLLNode(1);
        node.Next.Next.Next = new SLLNode(-4);
        node.Next.Next.Next.Next = new SLLNode(2);

        var res = RemoveZeroSumNodes(node);
    }

    private SLLNode RemoveZeroSumNodes(SLLNode head)
    {
        int sum = head.Value;
        SLLNode cur = head.Next;

        while(cur != null)
        {
            int s = sum;

            if (sum >= cur.Value)
            {
                
                head = TryRemoveZeroSumNodes(head, cur, ref sum);
                
            }

            sum += sum == s ? cur.Value : 0;
            cur = cur.Next;
        }

        return head;
    }

    private SLLNode TryRemoveZeroSumNodes(SLLNode head, SLLNode cur, ref int s)
    {
        int sum = s;
        var temp = head;
        SLLNode prev = null;

        while (temp != cur)
        {
            if (sum + cur.Value == 0)
            {
                if (head == temp)
                {
                    head = cur.Next;
                }
                else
                {
                    prev.Next = cur.Next;
                }

                s = sum;

                return head;
            }

            sum -= temp.Value;
            prev = temp;
            temp = temp.Next;
        }

        return head;
    }

    //https://leetcode.com/problems/copy-list-with-random-pointer/
    public void CloneList()
    {
        SLLNode head = new SLLNode(7);
        head.Next = new SLLNode(13);
        head.Next.Next = new SLLNode(11);
        head.Next.Next.Next = new SLLNode(10);
        head.Next.Next.Next.Next = new SLLNode(1);

        head.Next.Random = head;
        head.Next.Next.Random = head.Next.Next.Next.Next;
        head.Next.Next.Next.Random = head.Next.Next;

        var res = CloneList(head);
    }

    private SLLNode CloneList(SLLNode head)
    {
        SLLNode cur = head;
        SLLNode cloneHead = null;

        CloneNodes(cur, false);

        cur = head;
        CloneNodes(cur, true);

        cloneHead = cur.Next;

        while (cur != null)
        {
            SLLNode cloneNode = cur.Next;
            cur.Next = cur?.Next?.Next;
            cloneNode.Next = cur?.Next?.Next;

            cur = cur?.Next;
        }

        return cloneHead;
    }

    private void CloneNodes(SLLNode cur, bool isRandom)
    {
        while(cur != null)
        {
            if (isRandom)
            {
                cur.Next.Random = cur.Random?.Next;
            }
            else
            {
                SLLNode cloneNode = new SLLNode(cur.Value);
                SLLNode temp = cur.Next;

                cur.Next = cloneNode;
                cloneNode.Next = temp;
            }

            cur = cur?.Next?.Next;
        }
    }

    //Accepted-LCEasy-LCSol-T:O(n)-S:O(n): https://leetcode.com/problems/intersection-of-two-linked-lists/
    public void GetIntersectionNode()
    {
        SLLNode headA = new SLLNode(4);
        headA.Next = new SLLNode(1);
        headA.Next.Next = new SLLNode(8);
        headA.Next.Next.Next = new SLLNode(4);
        headA.Next.Next.Next.Next = new SLLNode(5);

        SLLNode headB = new SLLNode(5);
        headB.Next = new SLLNode(6);
        headB.Next.Next = new SLLNode(2);
        headB.Next.Next.Next = new SLLNode(7);
        headB.Next.Next.Next.Next = new SLLNode(1);
        headB.Next.Next.Next.Next.Next = headA.Next.Next.Next;
        var res = GetIntersectionNode(headA, headB);
    }

    private SLLNode GetIntersectionNode(SLLNode headA, SLLNode headB)
    {
        if(headA == null || headB == null)
        {
            return null;
        }

        SLLNode p1 = headA;
        SLLNode p2 = headB;

        while (p1 != p2)
        {
            p1 = p1 == null ? headB : p1.Next;
            p2 = p2 == null ? headA : p2.Next;
        }

        return p1;
    }

    //https://leetcode.com/problems/reverse-nodes-in-k-group/
    public void ReverseKGroup()
    {
        SLLNode node = new SLLNode(1);
        node.Next = new SLLNode(2);
        node.Next.Next = new SLLNode(3);
        node.Next.Next.Next = new SLLNode(4);
        node.Next.Next.Next.Next = new SLLNode(5);
        node.Next.Next.Next.Next.Next = new SLLNode(6);

        var res = ReverseKGroup(node, 3);
    }

    private SLLNode ReverseKGroup(SLLNode node, int k)
    {
        int count = k;

        if (node == null)
        {
            return null;
        }

        SLLNode prev = null;
        SLLNode next = null;
        SLLNode tail = node;

        while (count > 0 && node != null)
        {
            next = node.Next;
            node.Next = prev;
            prev = node;
            node = next;

            count-= 1;
        }

        tail.Next = ReverseKGroup(node, k);

        return prev;
    }

    //https://leetcode.com/problems/plus-one-linked-list/
    public void PlusOne()
    {
        SLLNode node = new SLLNode(2);
        node.Next = new SLLNode (9);
        node.Next.Next = new SLLNode(9);

        var res = PlusOne(node); 
    }

    private SLLNode PlusOne(SLLNode node)
    {
        var rList = ReverseSLL(node);

        SLLNode cur = rList;
        int carry = 1;

        while (cur != null)
        {
            var length = cur.Value.ToString().Length;
            var newLength = (cur.Value + carry).ToString().Length; 

            if (newLength > length)
            {
                if(cur.Next != null)
                {
                    while(length-- > 0)
                    {
                        cur.Value = 0;
                    }

                    carry = 1;
                }
                else
                {
                    cur.Value += carry;
                }
            }
            else
            {
                if (carry > 0)
                {
                    cur.Value += carry;
                }
                
                carry = 0;
            }

            cur = cur.Next;
        }

        return ReverseSLL(rList);
    }
}

public class SLLNode
{
    public SLLNode Next;

    public SLLNode Random;

    public int Value;

    public SLLNode(int value)
    {
        this.Value = value;
    }
}

public class DLLNode
{
    public DLLNode Next;
    public DLLNode Prev;
    public int Value;

    public DLLNode(int value)
    {
        this.Value = value;
    }
}

public class MultiLevelDLLNode
{
    public MultiLevelDLLNode Next;
    public MultiLevelDLLNode Prev;
    public MultiLevelDLLNode Child;
    public int Value;

    public MultiLevelDLLNode(int value)
    {
        this.Value = value;
    }
}