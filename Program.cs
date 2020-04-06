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
            // arrObj.MaxSquareSumInMatrix();                   //** To be Completed */
            // arrObj.HasCircularLoop();
            // arrObj.RemoveKDigits();
            // arrObj.CheckMountainArray();
            // arrObj.TestHeap();
            // arrObj.SubArraySumK();
            // arrObj.JumpingOnClouds();
            // arrObj.FindPeakElement();
            // arrObj.NoOfBurgers();
            // arrObj.InterleaveFirstHalfWithReversed();
            // arrObj.RotateImage();
            // arrObj.NextPermutation();
            // arrObj.NonOverlapingIntervals();
            // arrObj.AsteroidCollision();
            // arrObj.SubArraySumK();
            // arrObj.RemoveInvalidParanthesis();
            // arrObj.SearchInRotatedSortedArray();
            // arrObj.NextGreaterNumber();
            // arrObj.MedianFromStream();
            // arrObj.MaxConsecutiveOnes();
            // arrObj.MinimumSemesters();
            // arrObj.GCD();
            // arrObj.Permutations();
            //arrObj.ShortestSubArrayWithSumAtleastK();

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
            // treeObj.BSTFromPreOrder();
            // treeObj.HeightBalanced();
            // treeObj.RightSibling();
            // treeObj.ReplaceElementByItsRank();
            // treeObj.Serialize();
            // treeObj.ConstructBSTFromPostOrderSequence();
            // treeObj.AllElementsOfBST();
            // treeObj.EvenValuedGrandParent();
            // treeObj.DeleteLeavesWithGivenValue();
            // treeObj.FindMaxNInBST();
            // treeObj.MaxProductOfSplittedBinaryTree();
            // treeObj.BstToDoubleLinkedList();
            // treeObj.FlattenBinaryTreeToSLL();
            // treeObj.LevelOrderTraversal();
            // treeObj.ConstructBSTFromPreInorder();
            // treeObj.DeleteNodes();
            // treeObj.ValidateBST();
            // treeObj.DeleteNode();
            // treeObj.LongestZigZagPathInBinaryTree();
            // treeObj.PopulateNextPointers();

            DP dpObj = new DP();
            // dpObj.FindPalindromeSubstrings();
            // dpObj.PartitionKSubsetsMatchingSum();
            // dpObj.WordBreak();
            // dpObj.MinCostToMergeStones();
            // dpObj.FindRectangleCoordinates();
            // dpObj.PoisonousPlants();
            // dpObj.WordBreakII();
            // dpObj.BuySellWithCoolDown();
            // dpObj.BuySellStockII();
            // dpObj.CountSquareMatrices();
            // dpObj.SplitIntoPalindromes(); // Brilliant Sol
            // dpObj.MinInsertionStepsToPalindrome();
            // dpObj.MinModificationsToReachEnd();
            // dpObj.MinPathSum();            
            // dpObj.NthFibonacci();
            // dpObj.BuySellStock();
            // dpObj.MinSubsetDifference();                       // ** Revisit test case
            // dpObj.KSubsequences();
            // dpObj.DeleteOperationForTwoStrings();
            dpObj.LongestConsecutiveSequence();

            Graph gObj = new Graph();
            //gObj.BiPartition();
            //gObj.CourseScheduling();
            // gObj.MinCostToConnectNodes();
            // gObj.ValidGraphTree();
            // gObj.CriticalConnections();
            // gObj.AlienDictionary();
            // gObj.AccountsMerge();
            //gObj.TreeDiameter();

            Strings sObj = new Strings();
            //sObj.StrStr();
            // sObj.SwapForLongestRepeatedChar();
            // sObj.WordBreak();
            // sObj.checkPangram();
             //sObj.DecodeString();
            // sObj.LongestSubstringKDistinctChars();
            // sObj.IsPalindrome();
            // sObj.ReorderLogFiles();                            // ** Fails Leetcode test case
            // sObj.MinWindowsSubstring();
            // sObj.MakeAnagram();
            // sObj.SameCharacterFrequency();
            // sObj.LongestWordInDictionary();
            //sobj.SubstringsNotMatchingAlphabets();               //** TBD
            // sObj.FindAnagrams();
            // sObj.LongestValidParantheses();
            // sObj.StringTransformation();
            // sObj.ShortestPalindrome();
            // sObj.ConstructKPalindromeStrings();

            LinkedList lObj = new LinkedList();
            // lObj.SwapAlternatePairs();
            // lObj.RemoveZeroSumNodes();
            // lObj.ReverseSLL();
            // lObj.PlusOne();
            // lObj.MinCostToMergeStones();
            // lObj.CloneList();

            // ThreadedQueue thObj = new ThreadedQueue();

            Recursion rObj = new Recursion();
            //rObj.ExpressionAddOperators();                      //** To Be Completed **
            // rObj.LetterCombinations();
            // rObj.MaxRegion();
            // rObj.JumpGameIII();
            // rObj.KthLargest();
            // rObj.WordBoggle();

        }
    }
}