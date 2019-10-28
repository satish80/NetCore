﻿using System;

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
            // arrObj.GameOfLife();                             //** To Be Completed **
            // arrObj.MaxDistanceOfWaterFromLand();
            // arrObj.WaysToEncode();
            // arrObj.Sort2dArray();                            //** To Be Completed **
            // arrObj.FindOnesInRange();
            // arrObj.DecodeWays();
            // arrObj.MostStonesRemoved();
            // arrObj.DailyTemperatures();
            // arrObj.RemoveStones();
            // arrObj.FindFirstLastPosInArray();
            // arrObj.DailyTemperatures();
            // arrObj.FindInMountainArray();
            // arrObj.MinChairs();
            // arrObj.LongestSubsequenceOfGivenDifference();
            // arrObj.QueensAttackKing();
            // arrObj.OptimalUtilization();

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
            // treeObj.MinDepth();
            // treeObj.DistributeCoins(); // Brilliant Sol
            // treeObj.BinaryTreeUpsideDown();
            // treeObj.FillNodesWithEqualValues();
            // treeObj.BoundaryOfTree();
            // treeObj.FloorCeiling();
            // treeObj.FindRightNeighbor();
            // treeObj.SubTreeWithDeepestNodes();
            // treeObj.MaximumBinaryTree();
            // treeObj.VerticalOrder();
            // treeObj.PopulateNextRightPointers();
            // treeObj.MaxLevelSum();
            treeObj.BSTFromPreOrder();

            DP dpObj = new DP();
            //dpObj.FindPalindromeSubstrings();
            //dpObj.PartitionKSubsetsMatchingSum();
            // dpObj.WordBreak();
            // dpObj.MinCostToMergeStones();
            
            Graph gObj = new Graph();
            //gObj.BiPartition();
            //gObj.CourseScheduling();
            // gObj.MinCostToConnectNodes();
            // gObj.ValidGraphTree();
            // gObj.CriticalConnections();

            Strings sObj = new Strings();
            //sObj.StrStr();
            // sObj.SwapForLongestRepeatedChar();
            // sObj.WordBreak();
            // sObj.checkPangram();
            // TBD: sObj.DecodeString();
            // sObj.LongestSubstringKDistinctChars();
            // sObj.IsPalindrome();
            // sObj.ReorderLogFiles();                            // ** Fails Leetcode test case

            LinkedList lObj = new LinkedList();
            // lObj.SwapAlternatePairs();
            // lObj.RemoveZeroSumNodes();
            // lObj.ReverseSLL();
            // lObj.PlusOne();
            // lObj.MinCostToMergeStones();

            // ThreadedQueue thObj = new ThreadedQueue();

            Recursion rObj = new Recursion();
            //rObj.ExpressionAddOperators();                      //** To Be Completed **
            // rObj.LetterCombinations();
        }
    }
}