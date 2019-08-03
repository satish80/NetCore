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

            Trees treeObj = new Trees();
            //treeObj.BalanceTreeNodeValues();

            DP dpObj = new DP();
            //dpObj.FindPalindromeSubstrings();
            
            Graph gObj = new Graph();
            gObj.BiPartition();
        }
    }
}