public class LinkedList
{
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
        var res = SwapAlternatePairs(head);
    }

    private SLLNode SwapAlternatePairs(SLLNode head)
    {
        SLLNode cur = head.Next;
        SLLNode prev = head;
        SLLNode temp = null;

        prev.Next = cur.Next;
        cur.Next = prev;
        head = cur;

        while(cur.Next != null)
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
}

public class SLLNode
{
    public SLLNode Next;
    public int Value;

    public SLLNode(int value)
    {
        this.Value = value;
    }
}