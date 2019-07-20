public class Trees
{
    //Ensure all the node values are the same, given a tree with nodes having different values
    // Constraints: Only parent node can passs excess values to child and vice versa
    public void BalanceTreeNodeValues()
    {
        TreeNode node = new TreeNode(5,null);
        node.Left = new TreeNode(2,node);
        node.Left.Left = new TreeNode(3,node.Left);
        node.Left.Right = new TreeNode(8, node.Left);
        node.Right = new TreeNode(5, node);
        node.Right.Left = new TreeNode(2,node.Right);
        node.Right.Right = new TreeNode(3,node.Right);

        TreeNode.MaxNodeValue = 4;

        var res = BalanceTreeNodeValues(node); 
    }

    private TreeNode BalanceTreeNodeValues(TreeNode node)
    {
        if (node == null)
        {
            return null;
        }

        BalanceTreeNodeValues(node.Left);
        BalanceTreeNodeValues(node.Right);

        TreeNode.Balance = true;
        node.BalanceValue();
        
        return node;
    }
}

public class TreeNode
{
    private int value;
    private bool balance;
    public TreeNode(int value, TreeNode parent)
    {
        this.Value = value;
        this.Parent = parent;
    }
    public TreeNode Left;
    public TreeNode Right;

    public TreeNode Parent;

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

        TreeNode small = null;
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