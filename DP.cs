using System;
using System.Collections.Generic;
using System.Text;

public class DP
{
    // Solved using LinkedList
    public void MinCostToMergeStones()
    {
        int[] arr = new int[] {3, 5, 1, 2, 6};
        Console.WriteLine(MinCostToMergeStones(arr, 3));
    }

    private int MinCostToMergeStones(int[] arr, int k)
    {
        int[] dp = new int[arr.Length];

        // fill DP array
        for(int n = 0; n < arr.Length; n ++)
        {
            dp[n] = arr[n];
        }

        int cost = 0;
        int count = 0;

        int kno = arr.Length / k + arr.Length % k;
        if ((kno) % k != 0)
        {
            return -1;
        }

        kno += k % 2 == 0 ? 1 : 0;

        CalcMinSum(arr, dp, k);
        Queue<int> queue = SortDPAsQueue(dp);

        foreach(int idx in queue)
        {
            if (dp[idx] == 0)
            {
                continue;
            }

            if (idx > 0)
            {
                int bound = idx -k > 0 ? idx -k : 0;
                for(int i = idx-1; i >= bound; i --)
                {
                    if (dp[i] != 0)
                    {
                        dp[i] = arr[i];
                    }
                }
            }

            for(int i = idx + 1; i <= idx + k; i ++)
            {
                dp[i] = 0;
            }

            cost += dp[idx];
            count ++;
        }

        while (count < kno)
        {
            for(int i =0; i < dp.Length; i++)
            {
                if (dp[i] == 0)
                {
                    continue;
                }

                int cur = i;
                int target = i + k;

                while (cur < target)
                {
                    if (dp[cur] == 0)
                    {
                        target++;
                        cur++;
                    }

                    dp[i] += dp[cur];
                    dp[cur] = 0;
                }

                cost += dp[i];
                count++;
            }
        }

        return cost;
    }

    private void CalcMinSum(int[] arr, int[] dp, int k)
    {
        for(int idx = 0; idx + k -1 < dp.Length; idx ++)
        {
            int cur = idx+1;

            while (cur < idx + k)
            {
                dp[idx] += dp[cur++];
            }
        }
    }

    private Queue<int> SortDPAsQueue(int[] dp)
    {
        // Key
        List<KeyValuePair<int, int>> temp = new List<KeyValuePair<int, int>>();

        for(int idx = 0; idx < dp.Length; idx ++)
        {
            temp.Add(new KeyValuePair<int, int>(dp[idx], idx));
        }

        temp.Sort(new MyComparer());

        Queue<int> queue = new Queue<int>();

        foreach(KeyValuePair<int, int> pair in temp)
        {
            if (dp[pair.Value] == pair.Key)
            {
                continue;
            }

            queue.Enqueue(pair.Value);
        }

        return queue;
    }

    public class MyComparer : IComparer<KeyValuePair<int, int>>
    {
        public int Compare(KeyValuePair<int, int> obj1, KeyValuePair<int, int> obj2)
        {
            return obj1.Key.CompareTo(obj2.Key);
        }
    }

    //https://leetcode.com/problems/word-break-ii/
    public void WordBreakII()
    {
        IList<string> wordDict = new List<string>();
        wordDict.Add("pine");
        wordDict.Add("pineapple");
        wordDict.Add("pen");
        wordDict.Add("applepen");
        wordDict.Add("cat");
        wordDict.Add("sand");
        wordDict.Add("cats");
        wordDict.Add("and");
        wordDict.Add("dog");
        wordDict.Add("aaaa");
        wordDict.Add("aa");
        wordDict.Add("apple");

        string s = "pineapplepenapple";

        bool[] dp = new bool[] {false, false, true, true, false, false, true, false, false, true};

        Dictionary<int, List<int>> map = GetWordsMapIndex(s, wordDict);

        List<string> res = new List<string>();

        var coll = WordBreakII(s, wordDict, res, string.Empty, map, 0);

        foreach(string str in res)
        {
            Console.WriteLine(str);
        }
    }

    private Dictionary<int, List<int>> GetWordsMapIndex(string s, IList<string> dict)
    {
        Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();

        for(int i = 0; i < s.Length; i ++)
        {
            map.Add(i, new List<int>());

            for(int j = 1; i + j <= s.Length; j ++)
            {
                if (dict.Contains(s.Substring(i, j)))
                {
                    map[i].Add(j);
                }
            }
        }

        return map;
    }

    private List<string> WordBreakII(string s, IList<string> wordDict, List<string> res, string cur, 
    Dictionary<int, List<int>> map, int idx)
    {
        if (cur.Length - cur.Split(" ").Length +1 == s.Length)
        {
            res.Add(cur);
            return res;
        }

        if (idx >= s.Length)
        {
            return res;
        }

        foreach(int i in map[idx])
        {
            cur = string.IsNullOrEmpty(cur) ? cur : cur.TrimEnd() + " ";
            WordBreakII(s, wordDict, res, cur + s.Substring(idx, i), map, idx + i);
        }

        return res;
    }

    public void NthFibonacci()
    {
        Console.WriteLine(NthFibonacci(5));
    }

    private int NthFibonacci(int n)
    {
        int idx = 1;
        int val1 = 0;
        int val2 = 1;
        int sum = 0;

        while (idx < n)
        {
            sum += val2;
            val2 = val1;
            val1 = sum;
            idx++;
        }

        return sum;
    }

    //https://leetcode.com/discuss/interview-question/437403/Karat-interview-agentor-phone-or-find-rectangle-coordinates
    public void FindRectangleCoordinates()
    {
        int[,] arr = new int[,]
        {
            {0,0,0,0,0},
            {0,1,1,0,0},
            {0,1,1,0,0},
            {0,0,0,0,1}
        };

        var res = FindRectangleCoordinates(arr);
    }

    private List<Pair> FindRectangleCoordinates(int[,] arr)
    {
        Pair start = null;
        Pair end = null;

        List<Pair> pairs = new List<Pair>();

        for(int row = 0; row < arr.GetLength(0); row ++)
        {
            for(int col = 0; col < arr.GetLength(1); col ++)
            {
                if (row == 0 || col == 0)
                {
                    continue;
                }

                 if (arr[row, col] == 0 && end != null && end.x +1 == row && col == start.y)
                 {
                     pairs.Add(end);
                     end = null;
                     start = null;
                     continue;
                 }

                if (arr[row, col] == 0)
                {
                    continue;
                }

                if (start == null)
                {
                    start = new Pair(row, col);
                    pairs.Add(start);
                }

                end = new Pair(row, col);
            }
        }

        if (end != null)
        {
            pairs.Add(end);
        }

        return pairs;
    }

    /*FB
    Each of these plants has been treated with some amount of pesticide. After each day, if any plant has more pesticide than the plant on its 
    left it dies. You are given the initial values of the pesticide in each of the plants. Print the number of days 
    after which no plant dies, i.e. the time after which there are no plants with more pesticide content than the plant to their left.
    For example, pesticide levels . Using a -indexed array, day  plants  and  die leaving . On day , plant  of the current array dies leaving.
    As there is no plant with a higher concentration of pesticide than the one to its left, plants stop dying after day .
    */
    public void PoisonousPlants()
    {
        //int[] arr = new int[] {6, 5, 8, 4, 7, 10, 9};
        // int[] arr = new int[] {6, 5, 8, 4, 7, 10, 9, 7, 6};
        // int[] arr = new int[] {1,1,2,0,5,5,3,2,1};
        // int[] arr = new int[] {0,3,1,2,3};
           int[] arr = new int[] {1, 1, 2, 3, 0, 1};
        Console.WriteLine(PoisonousPlants(arr));
    }

    private int PoisonousPlants(int[] arr)
    {
        Stack<KeyValuePair<int, int>> stk = new Stack<KeyValuePair<int, int>>();
        stk.Push(new KeyValuePair<int, int>(0, 0));
        int max = 0;
        int idx = 1;

        while(idx < arr.Length)
        {
            if (arr[idx] > arr[idx-1])
            {
                if (idx-1 == stk.Peek().Key)
                {
                    var s = stk.Pop();
                    stk.Push(new KeyValuePair<int, int>(s.Key, s.Value+1));
                    max = Math.Max(max, stk.Peek().Value);
                }

                idx++;
                continue;
            }

            if (arr[idx] > arr[stk.Peek().Key])
            {
                var s = stk.Pop();
                stk.Push(new KeyValuePair<int, int>(s.Key, s.Value+1));
                max = Math.Max(stk.Peek().Value, max);
            }
            else
            {
                stk.Push(new KeyValuePair<int, int>(idx, 0));
            }

            idx++;
        }

        return max;
    }

    //Accepted: https://leetcode.com/problems/palindromic-substrings/
    // Return the palindromic sub strings for a given string
    public void FindPalindromeSubstrings()
    {
        Console.WriteLine(FindPalindromicSubstrings("aaa"));
    }

    private int FindPalindromicSubstrings(string str)
    {
        if (str.Length == 0)
        {
            return 0;
        }

        int count = 0;

        for(int idx = 0; idx < str.Length; idx ++)
        {
            CountPalindromicSubstrings(str, idx, idx, ref count);
            CountPalindromicSubstrings(str, idx, idx+1, ref count);
        }

        return count;
    }

    private void CountPalindromicSubstrings(string str, int left, int right, ref int count)
    {
        while (left >= 0 && right < str.Length && str[left] == str[right])
        {
            count ++;
            left--;
            right++;
        }
    }

    //https://leetcode.com/problems/partition-to-k-equal-sum-subsets/
    public void PartitionKSubsetsMatchingSum()
    {
        //int[] arr = new int[] {4, 3, 2, 3, 5, 2, 1};
        int[] arr = new int[] {10,10,7,7,7,7,6,7};
        Console.WriteLine(isKPartitionPossible(arr, arr.Length, 2));
        // Console.WriteLine(PartitionKSubsetsMatchingSum(arr, 0, 2, 0, 30, new bool[arr.Length]));
        //int sum = 60;
        //int k = 2;
        //Console.WriteLine(k != 0 && sum % k == 0 && canPartition(0, arr, new bool[arr.Length], 2, 0, 30));
    }

    private bool PartitionKSubsetsMatchingSum(int[] arr, int k)
    {
        int sum = 0;
        int target = 0;
        
        foreach(int num in arr)
        {
            sum += num;
        }

        target = sum / k;
        int count = 0;

        bool[,] dp = new bool[arr.Length, target+1];
        HashSet<int> visited = new HashSet<int>();
        
        for(int row = 0; row < arr.Length; row ++)
        {
            for(int col = 1; col <= target; col++)
            {
                if (arr[row] == col)
                {
                    dp[row, col] = true;

                    if (col == target)
                    {
                        visited.Add(row);
                        count++;
                    }
                }
                else if (arr[row] < col)
                {
                    if (row > 0)
                    {
                        var diff = target - arr[row];
                        int r = row-1;

                        while (r >= 0 && visited.Contains(r))
                        {
                            r--;
                        }

                        if (r >= 0 && dp[r, diff])
                        {
                            dp[row, col] = true;
                            count++;
                            visited.Add(row);
                            visited.Add(r);
                        }
                    }
                }
                else
                {
                        int r = row - 1;

                        while (r >= 0 && !visited.Contains(r))
                        {
                            r--;
                        }

                    dp[row, col] = r > 0 ? dp[r, col] : false;
                }
            }
        }

        return count == k;
    }


    private bool isKPartitionPossibleRec(int[] arr, int[] subsetSum, bool[] taken, int subset, int K, int N, int curIdx, int limitIdx) 
    {
        if (subsetSum[curIdx] == subset)
        { 
            /* current index (K - 2) represents (K - 1) subsets of equal 
                sum last partition will already remain with sum 'subset'*/
            if (curIdx == K - 2) 
                return true; 
      
            // recursive call for next subsetition 
            return isKPartitionPossibleRec(arr, subsetSum, taken, subset, 
                                                K, N, curIdx + 1, N - 1); 
        }
      
        // start from limitIdx and include elements into current partition 
        for (int i = limitIdx; i >= 0; i--) 
        { 
            // if already taken, continue 
            if (taken[i]) 
                continue; 
            int tmp = subsetSum[curIdx] + arr[i]; 
      
            // if temp is less than subset then only include the element 
            // and call recursively 
            if (tmp <= subset) 
            { 
                // mark the element and include into current partition sum 
                taken[i] = true; 
                subsetSum[curIdx] += arr[i]; 
                bool nxt = isKPartitionPossibleRec(arr, subsetSum, taken, 
                                                subset, K, N, curIdx, i - 1); 
      
                // after recursive call unmark the element and remove from 
                // subsetition sum 
                taken[i] = false; 
                subsetSum[curIdx] -= arr[i]; 
                if (nxt) 
                    return true; 
            } 
        } 
        return false; 
    }

    // Method returns true if arr can be partitioned into K subsets 
    // with equal sum 
    private bool isKPartitionPossible(int []arr, int N, int K) 
    { 
        // If K is 1, then complete array will be our answer 
        if (K == 1) 
            return true; 
      
        // If total number of partitions are more than N, then 
        // division is not possible 
        if (N < K) 
            return false; 
      
        // if array sum is not divisible by K then we can't divide 
        // array into K partitions 
        int sum = 0; 
        for (int i = 0; i < N; i++) 
            sum += arr[i]; 
        if (sum % K != 0) 
            return false; 

        // the sum of each subset should be subset (= sum / K) 
        int subset = sum / K; 
        int []subsetSum = new int[K]; 
        bool []taken = new bool[N]; 
      
        // Initialize sum of each subset from 0 
        for (int i = 0; i < K; i++) 
            subsetSum[i] = 0; 
      
        // mark all elements as not taken 
        for (int i = 0; i < N; i++) 
            taken[i] = false; 
      
        // initialize first subsubset sum as last element of 
        // array and mark that as taken 
        subsetSum[0] = arr[N - 1]; 
        taken[N - 1] = true; 
      
        // call recursive method to check K-substitution condition 
        return isKPartitionPossibleRec(arr, subsetSum, taken, 
                                        subset, K, N, 0, N - 1); 
    }

    //Accepted: https://leetcode.com/problems/word-break/
    public void WordBreak()
    {
        HashSet<string> map = new HashSet<string>();
        map.Add("apple");
        map.Add("apples");
        map.Add("pen");
        map.Add("penapt");
        map.Add("cats");
        map.Add("dog");
        map.Add("sand");
        map.Add("and");
        map.Add("cat");
        map.Add("og");

        //Console.WriteLine(WordBreak("applespenapt", map));

        Console.WriteLine(WordBreak("catsandog", map));
    }

    private bool WordBreak(string s, HashSet<string> dict)
    {
        bool[] f = new bool[s.Length + 1];
        
        f[0] = true;

        for(int i=1; i <= s.Length; i++)
        {
            for(int j=0; j < i; j++)
            {
                if(f[j] && dict.Contains(s.Substring(j, i-j)))
                {
                    f[i] = true;
                    break;
                }
            }
        }

        return f[s.Length];
    }

    public void MinModificationsToReachEnd()
    {
        char[,] arr = new char[3,3]
        {
            {'R', 'D', 'U'},
            {'L', 'L', 'L'},
            {'U', 'U', 'R'}
        };

        Console.WriteLine(MinModificationsToReachEnd(arr));
    }

    private int MinModificationsToReachEnd(char[,] arr)
    {
        int[,] dp = new int[arr.GetLength(0), arr.GetLength(1)];

        for(int r = 0; r < arr.GetLength(0); r++)
        {
            for(int c = 0; c < arr.GetLength(1); c ++)
            {
                dp[r,c] = int.MaxValue;
            }
        }

        dp[0,0] = 0;

        return MinModificationsToReachEnd(dp, arr, arr.GetLength(0)-1, arr.GetLength(1)-1);
    }

    private int MinModificationsToReachEnd(int[,] dp, char[,] arr, int row, int col)
    {
        int top = int.MaxValue;
        int left = int.MaxValue;

        if (row >= 1)
        {
            // top = (dp[row-1,col] == int.MaxValue) ?
            // MinModificationsToReachEnd(dp, arr, row-1, col) % int.MaxValue + (arr[row-1,col] != 'D' ? 1 : 0)
            // : dp[row-1, col];

            top = MinModificationsToReachEnd(dp, arr, row-1, col) % int.MaxValue + (arr[row-1,col] != 'D' ? 1 : 0);
        }

        if (col >= 1)
        {
            // left = (dp[row,col-1] == int.MaxValue) ?
            // MinModificationsToReachEnd(dp, arr, row, col-1) % int.MaxValue + ( arr[row,col-1] != 'R' ? 1 : 0)
            // : dp[row, col-1];

            left = MinModificationsToReachEnd(dp, arr, row, col-1) % int.MaxValue + ( arr[row,col-1] != 'R' ? 1 : 0);
        }

        if (!(row == 0 && col == 0))
        {
            dp[row, col] = Math.Min(top, left);
        }

        return dp[row, col];
    }

    //Accepted: T:O(n): S:O(n): https://leetcode.com/problems/longest-consecutive-sequence/
    public void LongestConsecutiveSequence()
    {
        int[] arr = new int[] {100, 4, 200, 1, 3, 2};
        Console.WriteLine(LongestConsecutiveSequence(arr));
    }

    private int LongestConsecutiveSequence(int[] arr)
    {
        int max = 0;

        Dictionary<int, int> map = new Dictionary<int, int>();

        for(int idx = 0; idx < arr.Length; idx ++)
        {
            if (!map.ContainsKey(arr[idx]))
            {
                int left = map.ContainsKey(arr[idx]-1) ? map[arr[idx]-1] : 0;
                int right = map.ContainsKey(arr[idx]+1) ? map[arr[idx]+1] : 0;

                int sum = left + right + 1;
                map[arr[idx]] = sum;

                max = Math.Max(max, sum);

                //Extend the boundaries
                map[arr[idx] - left] = sum;
                map[arr[idx] + right] = sum;
            }
        }

        return max;
    }

    //Accepted: T: O(n^2): S: O(n^2): https://leetcode.com/problems/delete-operation-for-two-strings/
    public void DeleteOperationForTwoStrings()
    {
        string str1 = "sea";
        string str2 = "eat";

        Console.WriteLine(DeleteOperationForTwoStrings(str1, str2));
    }

    private int DeleteOperationForTwoStrings(string str1, string str2)
    {
        int[,] dp = new int[str1.Length, str2.Length];

        for(int i = 0; i < str1.Length; i++)
        {
            for(int j = 0; j < str2.Length; j++)
            {
                if (str1[i] == str2[j])
                {
                    dp[i,j] = 1;

                    if (i > 0 && j > 0)
                    {
                        dp[i,j] +=  dp[i-1,j-1];
                    }
                }
                else
                {
                    if (i > 0)
                    {
                        dp[i,j] = dp[i-1, j];
                    }
                    if (j > 0)
                    {
                        dp[i,j] = Math.Max(dp[i,j], dp[i, j-1]);
                    }
                }
            }
        }

        int totalLength = str1.Length + str2.Length;
        int res = (str1.Length > 0 && str2.Length > 0) ? (dp[str1.Length-1, str2.Length-1] *2) : 0;

        return totalLength - res;
    }

    //Accepted: T:O(n^2) https://leetcode.com/problems/minimum-path-sum/
    public void MinPathSum()
    {
        int[][] arr = new int[2][]
        {
            new int[] {1,2, 5},
            new int[] {3, 2, 1},
        };

        Console.WriteLine(MinPathSum(arr));
    }

    private int MinPathSum(int[][] arr)
    {
        for(int row = 1; row < arr.Length; row ++)
        {
            arr[row][0] = arr[row-1][0] + arr[row][0];
        }

        for(int col = 1; col < arr.Length; col ++)
        {
            arr[0][col] = arr[0][col-1] + arr[0][col];
        }

        for(int row = 1; row < arr.Length; row ++)
        {
            for(int col = 1; col < arr[0].Length; col ++)
            {
                arr[row][col] = Math.Min(arr[row-1][col], arr[row][col-1]) + arr[row][col];
            }
        }

        return arr[arr.Length-1][arr[0].Length-1];
    }

    //Accepted: https://leetcode.com/problems/minimum-insertion-steps-to-make-a-string-palindrome/
    public void MinInsertionStepsToPalindrome()
    {
        string str = "zjveiiwvc";
        Console.WriteLine(MinInsertionStepsToPalindrome(str));
    }

    private int MinInsertionStepsToPalindrome(string str)
    {
        int[,] dp = new int[str.Length+1, str.Length+1];

        for(int i = 1; i <= str.Length; i ++)
        {
            for(int j = 1; j <= str.Length; j ++)
            {
                dp[i,j] = str[i-1] == str[str.Length-j] ? dp[i-1, j-1] + 1 : Math.Max(dp[i-1, j], dp[i, j-1]);
            }
        }

        return str.Length - dp[str.Length, str.Length];
    }

    public void MinSubsetDifference()
    {
        int[] arr = new int[]{2, 4, 5, 3,6};
        var res = MinSubsetDifference(arr);
    }

    public List<List<int>> MinSubsetDifference(int[] arr)
    {
        List<List<int>> res = new List<List<int>>();

        int sum = 0;
        int avg = 0;

        for(int i = 0; i < arr.Length; i++)
        {
            sum += arr[i];
        }

        avg = sum / 2;
        bool[,] dp = new bool[arr.Length, avg+1];

        int maxCol = -1;
        int maxRow = -1;

        for(int row = 0; row < dp.GetLength(0); row ++)
        {
            for(int col = 1; col < dp.GetLength(1); col ++)
            {
                var diff = arr[row] - col;

                if (diff == 0)
                {
                    dp[row, col] = true;
                }

                if (diff < 0)
                {
                    dp[row, col] = row > 0 ? dp[row-1, Math.Abs(diff)] : false;
                }
                else
                {
                    if (row > 0 && dp[row-1, diff])
                    {
                        dp[row, col] = dp[row-1, diff];
                    }
                }

                if (col == dp.GetLength(1)-1 && dp[row, col])
                {
                    maxCol = dp[row,col] == true ? col : maxCol;
                    maxRow = row;
                    break;
                }

                if (row == dp.GetLength(0)-1)
                {
                    maxCol = dp[row,col] == true ? col : maxCol;
                    maxRow = row;
                }
            }
        }

        int r = maxRow;
        List<int> list = new List<int>();
        int val = maxCol;
        int c = maxCol;

        while (r >= 0)
        {
            if (val == 0)
            {
                break;
            }

            val -= arr[r];
            list.Add(arr[r]--);

            while (r >= 0 && dp[r, val] == true)
            {
                r --;
            }

            r++;
        }

        res.Add(list);
        return res;
    }

    //https://leetcode.com/contest/weekly-contest-165/problems/count-square-submatrices-with-all-ones/
    public void CountSquareMatrices()
    {
        int[][] matrix = new int[5][]
        {
            new int[]{0,0,0},
            new int[]{0,1,0},
            new int[]{0,1,0},
            new int[]{1,1,1},
            new int[]{1,1,0}
        };

        Console.WriteLine(CountSubSquareMatrices(matrix));
    }

    private int CountSubSquareMatrices(int[][] arr)
    {
        for(int row = 0; row < arr.Length; row ++)
        {
            for(int col = 0; col < arr[0].Length; col ++)
            {
                if (row == 0 || col ==0 || arr[row][col] == 0)
                {
                    continue;
                }

                arr[row][col] = Math.Min(Math.Min(arr[row][col-1], arr[row-1][col-1]), arr[row-1][col]) + arr[row][col];
            }
        }

        int answer = 0;

        for(int row = 0; row < arr.Length; row ++)
        {
            for(int col = 0; col < arr[0].Length; col++)
            {
                answer+= arr[row][col];
            }
        }

        return answer;
    }

    //Accepted: T:O(n) S:O(n): https://leetcode.com/problems/min-cost-climbing-stairs/
    #region
    /*On a staircase, the i-th step has some non-negative cost cost[i] assigned (0 indexed).
    Once you pay the cost, you can either climb one or two steps. You need to find minimum cost to reach the top of the floor, 
    and you can either start from the step with index 0, or the step with index 1.

    Input: cost = [10, 15, 20]
    Output: 15
    Explanation: Cheapest is start on cost[1], pay that cost and go to the top.
    Example 2:
    Input: cost = [1, 100, 1, 1, 1, 100, 1, 1, 100, 1]
    Output: 6
    Explanation: Cheapest is start on cost[0], and only step on 1s, skipping cost[3].*/
    #endregion
    public void MinCostToClimbStairs()
    {
        int[] arr = new int[] {0, 1, 0, 0};
        Console.WriteLine(MinCostToClimbStairs(arr));
    }

    private int MinCostToClimbStairs(int[] cost)
    {
        int[] dp = new int[cost.Length];
        dp[0] = cost[0];
        dp[1] = cost[1];

        for(int idx = 2; idx < cost.Length; idx++)
        {
            dp[idx] = Math.Min(cost[idx-1], cost[idx-2]) + cost[idx];
        }

        return Math.Min(dp[cost.Length-1], dp[cost.Length-2]);
    }

    //aCCEPTED: T:O(n^2): S: O(n^2): https://leetcode.com/problems/regular-expression-matching/
    #region 
    /* Given an input string (s) and a pattern (p), implement regular expression matching with support for '.' and '*'.
    '.' Matches any single character.
    '*' Matches zero or more of the preceding element.
    The matching should cover the entire input string (not partial).

    s could be empty and contains only lowercase letters a-z.
    p could be empty and contains only lowercase letters a-z, and characters like . or *.
    Example 1:

    s = "ab"
    p = ".*"
    Output: true
    Explanation: ".*" means "zero or more (*) of any character (.)".

    s = "aab"
    p = "c*a*b"
    Output: true
    Explanation: c can be repeated 0 times, a can be repeated 1 time. Therefore, it matches "aab".

    s = "mississippi"
    p = "mis*is*p*."
    Output: false */
    #endregion
    public void RegexMatch()
    {
        string s = "aab";
        string p = "c*a*b";

        Console.WriteLine(IsMatch(s, p));
    }

    private bool IsMatch(string s, string p)
    {
        bool[,] dp = new bool[s.Length+1, p.Length+1];

        dp[0,0] = true;

        for(int i=1; i<=p.Length; i++)
        {
            if(p[i-1] == '*' && dp[0, i-2])
            {
                dp[0,i] = true;
            }
        }

        for(int row = 1; row < s.Length+1; row++)
        {
            for(int col = 1; col < p.Length+1; col++)
            {
                if (p[col-1] == '*')
                {
                    dp[row,col] = dp[row, col-2];
                    if(!dp[row,col] && (p[col-2] == s[row-1] || p[col-2] == '.'))
                    {
                        // Take the same column from previous row
                        dp[row,col] = dp[row-1,col];
                    }
                }
                else if (p[col-1] == '.' || s[row-1] == p[col-1])
                {
                    dp[row, col] = dp[row-1, col-1];
                }
            }
        }

        return dp[s.Length, p.Length];
    }

    //Accepted T:O(n^2) S:O(n^2): https://leetcode.com/problems/wildcard-matching/
    public void WildCardMatch()
    {
        string p = "c*a*b";
        string s = "aab";
        Console.WriteLine(WildCardMatch(s, p));
    }

    private bool WildCardMatch(string s, string p)
    {
        bool[,] dp = new bool[s.Length+1, p.Length+1];
        dp[0,0] = true;

        for(int col = 1; col <= p.Length; col++)
        {
            if (p.Length > 0 && p[col-1] == '*' && dp[0,col-1])
            {
                dp[0, col] = true;
            }
        }

        for(int row = 1; row <= s.Length; row ++)
        {
            for(int col = 1; col <= p.Length; col++)
            {
                if (s[row-1] == p[col-1] || p[col-1] == '?')
                {
                    dp[row, col] = dp[row-1, col-1];
                }
                else if(p[col-1] == '*')
                {
                    dp[row,col] = dp[row-1, col] || dp[row, col-1];
                }
            }
        }

        return dp[s.Length, p.Length];
    }

    //Accepted: T: O(n^2) S:O(n):https://leetcode.com/problems/pascals-triangle-ii/
    public void PascalTriangle()
    {
        var res = PascalTriangle(4);
    }

    private IList<int> PascalTriangle(int k)
    {
        IList<int> res = new List<int>();
        int count = 0;

        while (count<= k)
        {
            FillPascalTriangle(res, count++);
        }

        return res;
    }

    private void FillPascalTriangle(IList<int> list, int count)
    {
        int left = 0;
        list.Add(1);
        int right = list.Count-2;

        while(list.Count >2 && left <= right)
        {
            list[right--] = list[left] + list[left+1];
            left++;
        }

        left = 0;
        right = list.Count-1;

        while(list.Count > 2 && left <= right)
        {
            list[left++] = list[right--];
        }
    }

    //https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-cooldown/
    public void BuySellWithCoolDown()
    {
        int[] arr = new int[] {1,2,3,0,2};
        Console.WriteLine(BuySellWithCoolDown(arr));
    }

    private int BuySellWithCoolDown(int[] arr)
    {
        if (arr == null || arr.Length == 0)
        {
            return 0;
        }

        int[] s1 = new int[arr.Length];
        int[] s2 = new int[arr.Length];
        int[] s3 = new int[arr.Length];

        s1[0] = 0;
        s2[0] = - arr[0];
        s3[0] = Int32.MinValue;

        for(int idx = 1; idx < arr.Length; idx ++)
        {
            s1[idx] = Math.Max(s1[idx-1], s3[idx-1]);
            s2[idx] = Math.Max(s2[idx-1], s1[idx-1] - arr[idx]);
            s3[idx] = s2[idx-1] + arr[idx];
        }

        return Math.Max(s1[arr.Length-1], s3[arr.Length-1]);
    }

    //https://leetcode.com/problems/best-time-to-buy-and-sell-stock/
    public void BuySellStock()
    {
        int[] arr = new int[] {7, 6, 4, 3, 1};

        Console.WriteLine(BuySellStock(arr));
    }

    private int BuySellStock(int[] arr)
    {
        int max = 0;
        int start = 0;

        for(int idx = 0; idx < arr.Length; idx++)
        {
            if (arr[idx] < arr[start])
            {
                start = idx;
            }

            max = Math.Max(max, arr[idx] - arr[start]);
        }

        return max;
    }

    //https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/
    public void BuySellStockII()
    {
        int[] arr = new int[] {7,1,5,3,6,4};
        Console.WriteLine(BuySellStockII(arr));
    }

    private int BuySellStockII(int[] arr)
    {
        int max = 0;
        int start = 0;
        int profit = 0;

        for(int idx = 1; idx < arr.Length; idx++)
        {
            if (arr[idx] < arr[idx-1])
            {
                profit += max;
                max = 0;
                start = idx;
            }

            max = Math.Max(max, arr[idx] - arr[start]);
        }

        return profit += max;
    }

    //Accepted: https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iii/
    public void BuySellStockIII()
    {
        int[] arr = new int[]{3,3,5,0,0,3,1,4};
        Console.WriteLine(BuySellStockII(arr));
    }

    private int BuySellStockIII(int[] arr)
    {
        int buy1 = int.MinValue;
        int sell1 = 0;
        int buy2 = int.MinValue;
        int sell2 = 0;

        for(int idx = 0; idx < arr.Length; idx++)
        {
            buy1 = Math.Max(buy1, -arr[idx]);
            sell1 = Math.Max(sell1, buy1 + arr[idx]);
            buy2 = Math.Max(buy2, sell1 -arr[idx]);
            sell2 = Math.Max(sell2, buy2 + arr[idx]);
        }

        return sell2;
    }

    //Find min k size sub-sequences in an array.[[1, 1, 2, 3, 2]]
    // Output: [[1,2,3],[1,2,5],[1,3,4],[1,3,5]]
    public void KSubsequences()
    {
        int[] arr = new int[]{1, 1, 2, 3, 2};
        int k = 3;
        var res = KSubsequences(arr, k);
    }

    private List<List<int>> KSubsequences(int[] arr, int k)
    {
        List<List<int>> map = new List<List<int>>();

        for(int i = arr.Length; i >= 0; i--)
        {
            map.Add(new List<int>());
        }

        int smallIdx = -1;
        int small = int.MaxValue;
        for(int i = arr.Length; i > 0; i--)
        {
            if (arr[i-1] < small)
            {
                smallIdx = i;
            }

            for(int j = arr.Length; j > i; j--)
            {
                if ((arr[j-1] - arr[i-1] == 1) || arr[i-1] == arr[j-1])
                {
                    map[i].Add(j);
                }
            }

            map[i].Sort();
        }

        List<List<int>> res = new List<List<int>>();
        return GetSequence(smallIdx, map, k-1, new List<int>(), res);
    }

    private List<List<int>> GetSequence(int start, List<List<int>> map, int k, List<int> list,
     List<List<int>> res)
    {
        if (start > map.Count)
        {
            return res;
        }

        list.Add(start);

        if (k == 0)
        {
            var resList = new List<int>();
            resList.AddRange(list);
            res.Add(resList);
            return res;
        }

        for(int i = 0; i < map[start].Count; i ++)
        {
            var idx = map[start][i];
            GetSequence(idx, map, k-1, list, res);
            list.Remove(idx);
        }

        return res;
    }

    /*
   Company: Google
   Given a string, split it into as few strings as possible such that each string is a palindrome.
   For example, given the input string racecarannakayak, return ["racecar", "anna", "kayak"].
   Given the input string abc, return ["a", "b", "c"].
    */
    public void SplitIntoPalindromes()
    {
        string str = "racecarannakayak";
        Console.WriteLine(MinCut(str));
    }

    private int MinCut(string s)
    {
        int n = s.Length;
        int[] cut = new int[n+1];  // number of cuts for the first k characters

        for (int i = 0; i <= n; i++)
        {
             cut[i] = i-1;
        }

        for(int i = 0; i < n; i++)
        {
            for (int j = 0; i-j >= 0 && i+j < n && s[i-j] == s[i+j] ; j++) // odd length palindrome
            {
                cut[i+j+1] = Math.Min(cut[i+j+1], 1 + cut[i-j]);
            }

            for (int j = 1; i-j+1 >= 0 && i+j < n && s[i-j+1] == s[i+j]; j++) // even length palindrome
            {
                cut[i+j+1] = Math.Min(cut[i+j+1], 1 + cut[i-j+1]);
            }
        }

        return cut[n];
    }

    private int minCut(string s) 
    {
        int n = s.Length;
        // s[0..i](inclusive) needs dp[i] cuts
        int[] dp = new int[n];
        for (int i = 0; i < n; i++)
            dp[i] = i;
        
        for (int center = 0; center < n; center++) {
            int l, r; // left bound, right bound of a palindrome
            // odd case
            l = r = center;
            while (l >= 0 && r < n && s[l] == s[r]) {
                dp[r] = Math.Min(dp[r], (l - 1 < 0 ? 0 : dp[l - 1] + 1));
                l--; r++;
            }
            
            // even case
            l = center;
            r = center + 1;
            while (l >= 0 && r < n && s[l] == s[r]) {
                dp[r] = Math.Min(dp[r], (l - 1 < 0 ? 0 : dp[l - 1] + 1));
                l--; r++;
            }
            
            // only initial value of l, r is different
            // the while loop is totally same
        }
        
        return dp[n - 1];
    }

    //My solution
    private int SplitIntoPalindromes(string str)
    {
        int[,] dp = new int[str.Length, str.Length];

        for(int r = 0; r < str.Length; r++)
        {
            for(int c = 0; c < str.Length; c++)
            {
                dp[r,c] = c-r;
            }
        }

        for(int r = 0; r < str.Length; r++)
        {
            for(int c = r+1; c < str.Length; c++)
            {
                if (dp[r, c] == c -r)
                {
                    UnsetIfPalindrome(str, r, c, dp);
                }

                for(int l = 0; r + l < c; l++)
                {
                    int col = r + l;
                    if (dp[r, col] == col -r)
                    {
                        UnsetIfPalindrome(str, r, col, dp);
                    }

                    if (dp[col+1, c] == c - (col+1))
                    {
                        UnsetIfPalindrome(str, col+1, c, dp);
                    }

                    dp[r,c] = Math.Min(dp[r, col] + dp[col+1, c] + 1, dp[r,c]);
                }
            }
        }

        return dp[0, str.Length-1];
    }

    private void UnsetIfPalindrome(string str, int start, int end, int[,] dp)
    {
        if (start == end)
        {
            return;
        }

        int s = start;
        int e = end;

        while (s < e)
        {
            if (str[s++] != str[e--])
            {
                return;
            }
        }

        dp[start, end] = 0;
    }
}