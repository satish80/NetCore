using System;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Arrays arrObj = new Arrays();
            // arrObj.PartitionStringWithUniqueChars();
            // arrObj.MoveZerosAtEnd();
            //arrObj.QueueReconstructionByHeight();
            //arrObj.WallsGates();
            // arrObj.FindMissingInSortedArray();
            //TBD: arrObj.GameOfLife();
            // arrObj.MaxDistanceOfWaterFromLand();
            // arrObj.WaysToEncode();

            Trees treeObj = new Trees();
            //treeObj.BalanceTreeNodeValues();
            //treeObj.CheckEqualTree();
            // treeObj.ConstructBinaryTreeFromInAndPreorder();
            //treeObj.VerifyPreOrderSerialization();
            //treeObj.ConstructBSTFromPreOrder();
            // treeObj.SumOfLeftLeaves();
            // treeObj.NextGreater();
            // treeObj.IsValidBST();
            // treeObj.KDistanceBinaryTree(); //Brilliant Sol
            // treeObj.IterateBST();
            // treeObj.ConstructBinaryTreeFromString();
            // treeObj.FlattenTree();
            treeObj.MinDepth();

            DP dpObj = new DP();
            //dpObj.FindPalindromeSubstrings();
            //dpObj.PartitionKSubsetsMatchingSum();
            // dpObj.WordBreak();
            
            Graph gObj = new Graph();
            //gObj.BiPartition();
            //gObj.CourseScheduling();

            Strings sObj = new Strings();
            //sObj.StrStr();
            // sObj.SwapForLongestRepeatedChar();
            // sObj.WordBreak();
            // sObj.checkPangram();
            // TBD: sObj.DecodeString();

            LinkedList lObj = new LinkedList();
            // lObj.SwapAlternatePairs();
            // lObj.RemoveZeroSumNodes();
            // lObj.ReverseSLL();
        }
    }
}