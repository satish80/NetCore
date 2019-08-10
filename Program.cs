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
            treeObj.CheckEqualTree();

            DP dpObj = new DP();
            //dpObj.FindPalindromeSubstrings();
            //dpObj.PartitionKSubsetsMatchingSum();
            
            Graph gObj = new Graph();
            //gObj.BiPartition();

            Strings sObj = new Strings();
            //sObj.StrStr();

            LinkedList lObj = new LinkedList();
            // lObj.SwapAlternatePairs();
        }
    }
}