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

            Trees treeObj = new Trees();
            //treeObj.BalanceTreeNodeValues();
            //treeObj.CheckEqualTree();
            treeObj.ConstructBinaryTreeFromInAndPreorder();

            DP dpObj = new DP();
            //dpObj.FindPalindromeSubstrings();
            //dpObj.PartitionKSubsetsMatchingSum();
            
            Graph gObj = new Graph();
            //gObj.BiPartition();
            //gObj.CourseScheduling();

            Strings sObj = new Strings();
            //sObj.StrStr();
            // sObj.SwapForLongestRepeatedChar();

            LinkedList lObj = new LinkedList();
            // lObj.SwapAlternatePairs();
        }
    }
}