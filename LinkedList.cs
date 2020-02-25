using System;
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
        SLLNode next = node.Next;

        while (node != null)
        {
            node.Next = prev;
            prev = node;
            node = next;
            next = node?.Next;
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

    private void RemoveDllNodes(DLLNode left, DLLNode right, int val)
    {
        left.Value = val;
        left.Next = right.Next;
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