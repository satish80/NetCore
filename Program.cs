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

            Trees treeObj = new Trees();
            //treeObj.BalanceTreeNodeValues();
            //treeObj.CheckEqualTree();
            // treeObj.ConstructBinaryTreeFromInAndPreorder();
            //treeObj.VerifyPreOrderSerialization();
            //treeObj.ConstructBSTFromPreOrder();
            treeObj.SumOfLeftLeaves();

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

            LinkedList lObj = new LinkedList();
            // lObj.SwapAlternatePairs();
        }
    }
}