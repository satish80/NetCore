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
}

public class TreeNode
{
    public TreeNode(int value)
    {
        this.Value = value;
    }

    public TreeNode Left;
    public TreeNode Right;

    public int Value;
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