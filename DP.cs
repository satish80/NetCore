using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public class DP
{
    // Solved using LinkedList
    public void MinCostToMergeStones()
    {
        int[] arr = new int[] {3, 5, 1, 2, 6};
        Console.WriteLine(MergeStones(arr, 3));
    }

    private int MergeStones(int[] stones, int K)
    {
        int n = stones.Length;

        if ((n - 1) % (K - 1) > 0) 
        {
            return -1;
        }

        int[] prefix = new int[n+1];

        for (int i = 0; i <  n; i++)
        {
            prefix[i + 1] = prefix[i] + stones[i];
        }

        int[,] dp = new int[n,n];

        for (int m = K; m <= n; ++m)
        {
            for (int i = 0; i + m <= n; ++i)
            {
                int j = i + m - 1;
                dp[i,j] = int.MaxValue;

                for (int mid = i; mid < j; mid += K - 1)
                {
                    dp[i,j] = Math.Min(dp[i,j], dp[i,mid] + dp[mid + 1,j]);
                }

                if ((j - i) % (K - 1) == 0)
                {
                    dp[i,j] += prefix[j + 1] - prefix[i];
                }
            }
        }

        return dp[0, n - 1];
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

    //Accepted:LcMedium-LcSol-T:O(n^2)-S:O(n) https://leetcode.com/problems/coin-change-2/
    public void CoinChange2()
    {
        int[] arr = new int[] {1,2,5};
        int amount = 5;

        Console.WriteLine(CoinChange2(amount, arr));
    }

    private int CoinChange2(int amount, int[] coins)
    {
        int[] dp = new int[amount + 1];
        dp[0] = 1;

        foreach (int coin in coins)
        {
            for (int i = coin; i <= amount; i++)
            {
                dp[i] += dp[i-coin];
            }
        }

        return dp[amount];
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
        //var coll = WordBreakII(s, wordDict);

        foreach(string str in res)
        {
            Console.WriteLine(str);
        }
    }

    private IList<string> WordBreakII(string s, IList<string> wordDict)
    {
        IList<string> res = new List<string>();
        StringBuilder sb = new StringBuilder();
        bool[] dp = new bool[s.Length];
        dp[0] = true;

        for(int idx = 0; idx < s.Length; idx++)
        {
            if (!dp[idx])
            {
                continue;
            }

            for(int i = idx+1; i < s.Length-1; i++)
            {
                var str = s.Substring(idx, i-idx);
                if (wordDict.Contains(str))
                {
                    dp[i] = true;
                    sb.Append(str + " ");
                }
            }
            res.Add(sb.ToString());
            sb.Clear();
        }

        return res;
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

    //LCMedium-Accepted-LcSol-https://leetcode.com/problems/partition-to-k-equal-sum-subsets/
    public void PartitionKSubsetsMatchingSum()
    {

        int[] arr = new int[] {2,2,2,2,2,2,2,2,2,2,2,2,2,3,3};
        //int[] arr = new int[] {10,10,7,7,7,7,6,7};
        int k = 8;

        int sum = 0;
        for(int idx = 0; idx < arr.Length; idx++)
        {
            sum += arr[idx];
        }

        int targetSum = sum % k;
        if (sum % k != 0)
        {
            Console.WriteLine(false);
        }

        Console.WriteLine(CanPartitionKSubsets(arr, new bool[arr.Length], k, 0, sum / k, 0));
    }
    
    private bool CanPartitionKSubsets(int[] nums, bool[] visited, int k, int sum, int targetSum, int idx)
    {
        if (k == 0)
        {
            return true;
        }

        if (sum == targetSum)
        {
            return CanPartitionKSubsets(nums, visited, k-1, 0, targetSum, 0);
        }

        for(int i = idx; i < nums.Length; i++)
        {
            if (visited[i] || sum + nums[i] > targetSum)
            {
                continue;
            }

            visited[i] = true;

            if (CanPartitionKSubsets(nums, visited, k, sum + nums[i], targetSum, i + 1))
            {
                return true;
            }

            visited[i] = false;
        }

        return false;
    }

    //https://leetcode.com/problems/longest-palindromic-substring/
    public void LongestPalindromicSubstring()
    {
        string str = "bbb";
        Console.WriteLine(LongestPalindromicSubstring(str));
    }

    private string LongestPalindromicSubstring(string s)
    {
        bool[,] dp = new bool[s.Length, s.Length];
        string res = null;

        for(int i = s.Length-1; i >= 0; i--)
        {
            for(int j = i; j < s.Length; j++)
            {
                dp[i,j] = s[i] == s[j]  && (j-i < 2 || dp[i+1,j-1]);

                if (dp[i,j] && (res == null || res.Length < j-i+1))
                {
                    res = s.Substring(i, j-i+1);
                }
            }
        }

        return res;
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

    //Accepted: LCMedium: LCSol: T:O(n)-https://leetcode.com/problems/knight-dialer/
    public void KnightDialer()
    {
        int n = 3131;
        Console.WriteLine(KnightDialer(n));
    }

    private int KnightDialer(int n)
    {
        int[][] graph = new int[][]
        {
            new int[] {4,6},
            new int[]{6,8},
            new int[]{7,9},
            new int[]{4,8},
            new int[]{3,9,0},
            new int[]{},
            new int[]{1,7,0},
            new int[]{2,6},
            new int[]{1,3},
            new int[]{2,4}
        };

        int[,] dp = new int[n,10];
        for(int r = 0; r < n; r++)
        {
            for(int c = 0; c < 10; c++)
            {
                dp[r,c] = -1;
            }
        }

        int res = 0;
        for(int i = 0; i <= 9; i++)
        {
            res = (res + KnightDialer(n-1, i, graph, dp)) % (1000000007);
        }

        return res;
    }

    private int KnightDialer(int n, int cur, int[][] graph, int[,] dp)
    {
        if (n == 0)
        {
            return 1;
        }

        if (dp[n,cur] != -1)
        {
            return dp[n,cur];
        }

        int res = 0;

        foreach(int i in graph[cur])
        {
            res = (res + KnightDialer(n-1, i, graph, dp)) % (1000000007);
        }

        dp[n,cur] = res;
        return res;
    }

    //https://leetcode.com/problems/super-egg-drop/
    public void EggDrop()
    {
        int eggs = 2;
        int floors = 6;

        Console.WriteLine(EggDrop(eggs, floors));
    }

    private int EggDrop(int eggs, int floors)
    {
        int[,] dp = new int[floors+1, eggs+1];

        for(int i = 1; i <= floors; i++)
        {
            for(int j = 1; j <= eggs; j++)
            {
                // Add the condition to account for the floors at the current given floor & egg count.
                // 1 for current floor
                // egg breaks: (j-1), then look upto i -1 (try the next floor)
                // egg doesn't break: try the i-1 (try next floor), egg count remains same
                dp[i,j] = 1 + dp[i-1,j-1] + dp[i-1, j];

                // if the total floors is greater than or equal to the total floors, then return the i (the floor)
                if (dp[i,j] >= floors)
                {
                    return i;
                }
            }
        }

        return dp[eggs, floors];
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

    //https://leetcode.com/problems/count-square-submatrices-with-all-ones/
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

    //Accepted: T(O(nlog n), S:O(n): https://leetcode.com/problems/longest-increasing-subsequence/
    public void LongestIncreasingSubsequence()
    {
        int[] arr = new int[]{0, 8, 4, 12, 2};
        Console.WriteLine(LongestIncreasingSubsequence(arr));
    }

    private int LongestIncreasingSubsequence(int[] arr)
    {
        int[] dp = new int[arr.Length];
        int len = 0;

        foreach(int num in arr)
        {
            int idx = Array.BinarySearch(dp, 0, len, num);

            if (idx < 0)
            {
                idx  = -(idx + 1);
            }

            dp[idx] = num;

            if (idx == len)
            {
                len++;
            }
        }

        return len;
    }

    //LC Hard: https://leetcode.com/problems/form-largest-integer-with-digits-that-add-up-to-target/
    public void LargestNumber()
    {
        int[] arr = new int[]{4,4,2,5,6,7,2,5,5};
        Console.WriteLine(LargestNumber(arr, 9));
    }

    private string LargestNumber(int[] cost, int target)
    {
        int[] dp = new int[target + 1];
        for (int t = 1; t <= target; ++t)
        {
            dp[t] = -10000;
            for (int i = 0; i < 9; ++i)
            {
                if (t >= cost[i])
                {
                    dp[t] = Math.Max(dp[t], 1 + dp[t - cost[i]]);
                }
            }
        }

        if (dp[target] < 0)
        {
            return "0";
        }

        StringBuilder res = new StringBuilder();
        for (int i = 8; i >= 0; --i)
        {
            while (target >= cost[i] && dp[target] == dp[target - cost[i]] + 1)
            {
                res.Append(1 + i);
                target -= cost[i];
            }
        }

        return res.ToString();
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

    //https://leetcode.com/problems/egg-drop-with-2-eggs-and-n-floors/
    public void TwoEggDrop()
    {
        int floors = 3;
        Console.WriteLine(TwoEggDrop(floors));
    }

    private int TwoEggDrop(int n)
    {
        int eggs = 2;
        int floors = n;
        int[,] dp = new int[n+1, n+1];

        for(int i = 1; i <= eggs; i++)
        {
            for(int j = 1; j <= floors; j++)
            {
                dp[i,j] = 1 + dp[i-1, j-1] + dp[i-1, j];

                if (dp[i,j] > floors)
                {
                    return j;
                }
            }
        }

        return dp[eggs, floors];
    }

    //Accepted: T:O(n^2): S: O(n^2): https://leetcode.com/problems/regular-expression-matching/
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
                    dp[row,col] = dp[row, col-2]; // Consider Zero occurance of the 2 characters before *
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

    /*
    Google:
    There are two cities and a field between those. The field is split into a grid: rows x columns like this:
         cols
        +----+
        |    |
        |    |
   rows |    |
        |    |
        |    |
        +----+

    <citties are on the left and right>
    CIT (connectivity infrastructure team) is building service towers on the field.
    We don't know how they decide where to build towers (seemingly random), but they gave us a method: NextTower() that gives coordinates where they've put a tower.
    When the cities are connected we should ask them to stop by calling Stop() method they've given us.
    */
    public void CityConnection()
    {
        int[][] grid = new int[][]
        {
            new int[] {0,0,1,1},
            new int[] {1,1,1,0},
            new int[] {0,1,0,0},
            new int[] {1,0,1,0},
        };

        int[][] cache = grid;
        int nextR = 0;
        int nextC = 0;

        while(true)
        {
            var next = GetNextTower(grid, nextR, nextC);

            if (next == null)
            {
                break;
            }

            nextR = next[0];
            nextC = next[1];

            if(nextR > 0 && grid[nextR][nextC] == 1 && grid[nextR-1][nextC] == 1)
            {
                if (CopyRow(grid, cache, nextR-1, nextR))
                {
                    break;
                }
            }

            if (nextR < grid.Length-1 && grid[nextR][nextC] == 1 && grid[nextR+1][nextC] == 1)
            {
                if (CopyRow(grid, cache, nextR+1, nextR))
                {
                    break;
                }
            }
        }
    }

    private bool CopyRow(int[][] grid, int[][] cache, int sourceRow, int targetRow)
    {
        int c = 0;
        int count = 0;

        while(c < grid[0].Length)
        {
            if (grid[sourceRow][c] == 1)
            {
                cache[targetRow][c] = 1;
            }

            count += cache[targetRow][c];

            c++;
        }

        return count == grid[0].Length;
    }

    private int[] GetNextTower(int[][] grid, int r, int c)
    {
        for(int row = r; row < grid.Length; row ++)
        {
            for(int col = c+1; col < grid[0].Length; col++)
            {
                if (grid[row][col] == 1)
                {
                    return new int[]{row, col};
                }
            }

            c = -1;
        }

        return null;
    }

    //Accepted-LCMedium-LCSol-T:O(target*N)-S:O(target) https://leetcode.com/problems/combination-sum-iv/
    public void CombinationSumIV()
    {
        int[] arr = new int[] {1,2,3};
        int target = 4;
        int[] dp = new int[target+1];
        
        for(int idx = 0; idx <= target; idx++)
        {
            dp[idx] = -1;
        }
        
        dp[0] = 1;
        
        Console.WriteLine(helper(arr, dp, target));
    }

    private int helper(int[] nums, int[] dp, int target)
    {
        if (dp[target] != -1)
        {
            return dp[target];
        }

        int res = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (target >= nums[i])
            {
                res += helper(nums, dp, target - nums[i]);
            }
        }

        dp[target] = res;
        return res;
    }

    private int CombinationSumIV(int[] nums, int target)
    {
        int[] comb = new int[target + 1];
        comb[0] = 1;

        for (int i = 1; i < comb.Length; i++)
        {
            for (int j = 0; j < nums.Length; j++)
            {
                if (i - nums[j] >= 0)
                {
                    comb[i] += comb[i - nums[j]];
                }
            }
        }

        return comb[target];
    }

    //https://leetcode.com/problems/unique-paths/
    public void UniquePaths()
    {
        int m = 3, n = 3;
        Console.WriteLine(UniquePaths(m, n));
    }

    private int UniquePaths(int m, int n) 
    {
        int[,] dp = new int[m,n];
        
        for(int row = 0; row < m; row ++)
        {
            for(int col = 0; col < n; col ++)
            {
                if (row == 0 || col == 0)
                {
                    dp[row, col] = 1;
                }
                else
                {
                    dp[row, col] = dp[row-1, col] + dp[row, col-1];
                }
            }
        }
        
        return dp[m-1, n-1];
    }

    //Accepted-LcEasy-LcSol-T:O(n)-S:O(n) https://leetcode.com/problems/climbing-stairs/
    public void ClimbStairs()
    {
        Console.WriteLine(ClimbStairs(5));
    }

    private int ClimbStairs(int n)
    {
        int[] dp = new int[n + 1];
        if (n == 1)
        {
            return 1;
        }

        if (n == 2) 
        {
            return 2;
        }

        dp[0] = 0;
        dp[1] = 1;
        dp[2] = 2;

        for (int i = 3; i <= n; i++)
        {
          dp[i] = dp[i-1] + dp[i - 2];
        }
        
        return dp[n];
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
    #region
    /* Design an algorithm to find the maximum profit. You may complete as many transactions as you like (ie, buy one and sell one share
    of the stock multiple times) with the following restrictions:
    You may not engage in multiple transactions at the same time (ie, you must sell the stock before you buy again).
    After you sell your stock, you cannot buy stock on next day. (ie, cooldown 1 day) */
    #endregion
    public void BuySellWithCoolDown()
    {
        int[] arr = new int[] {1,2,3,0,2};
        Console.WriteLine(BuySellWithCoolDownCre(arr));
    }

    private int BuySellWithCoolDownCre(int[] prices)
    {
        if (prices.Length <=1)
        {
            return 0;
        }

        int[,] dp = new int[prices.Length,2];

      //dp[Day,stock]
        dp[0,0] = 0; // Did nothing on day 0
        dp[0,1] = -prices[0]; // Bought stock on day 0

        dp[1,0] = Math.Max(dp[0,0], dp[0,1]+ prices[1]);  // No stock on day 1: No stock on day 0 or Sold stock acquired on day 0
        dp[1,1] = Math.Max(dp[0,1], dp[0,0] - prices[1]); // 1 stock on day 1: Retained same stock from previous day or bought on day 1

        for(int idx = 2; idx < prices.Length; idx++)
        {
            dp[idx,0] = Math.Max(dp[idx-1,0], dp[idx-1,1] + prices[idx]);  // No stock on day 0: No stock previous day or Sold stock today
            dp[idx,1] = Math.Max(dp[idx-1,1], dp[idx-2,0] - prices[idx]);  // 1 stock : Retained same stock previous day or bought stock after cool down
        }

        return dp[prices.Length-1,0];
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

    //Accepted-LcHard-LcSol-T:O(n*m) S:O(n*m) https://leetcode.com/problems/shortest-common-supersequence/
    public void ShortestCommonSuperSequence()
    {
        string str1 = "abac", str2 = "cab";
        Console.WriteLine(ShortestCommonSuperSequence(str1, str2));
    }

    private string ShortestCommonSuperSequence(string A, string B)
    {
        int i = 0, j = 0;
        string res = "";

        foreach (char c in LCS(A, B)) 
        {
            while (A[i] != c)
            {
                res += A[i++];
            }
            
            while (B[j] != c)
            {
                res += B[j++];
            }

            res += c;
            i++;
            j++;
        }
        
        return res + A.Substring(i) + B.Substring(j);
    }

    private string LCS(string A, string B)
    {
        int n = A.Length, m = B.Length;
        string[,] dp = new string[n+1, m+1];

        for (int i = 0; i <= n; ++i)
        {
            for (int j = 0; j <= m; ++j)
            {
                dp[i,j] = string.Empty;
            }
        }

        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < m; ++j)
            {
                dp[i+1, j+1] = dp[i, j] == null ? string.Empty : dp[i, j];

                if (A[i] == B[j])
                {
                    dp[i + 1, j + 1] = dp[i, j] + A[i];
                }
                else
                {
                    dp[i + 1, j + 1] = dp[i+1, j].Length > dp[i, j+1].Length ?  dp[i+1, j] : dp[i, j+1];
                }
            }
        }

        return dp[n, m];
    }

    //Accepted: https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iii/
    public void BuySellStockIII()
    {
        int[] arr = new int[]{3,3,5,0,0,3,1,4};
        Console.WriteLine(BuySellStockIII(arr));
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

    //Accepted: T:O(n^2) S:O(n^2): https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iv/
    /* Say you have an array for which the i-th element is the price of a given stock on day i.
    Design an algorithm to find the maximum profit. You may complete at most k transactions.
    Note:
    You may not engage in multiple transactions at the same time (ie, you must sell the stock before you buy again). */
    public void BuySellStockIV()
    {
        int[] arr = new int[] {2, 3, 2, 1, 3};
        int k = 2;
        Console.WriteLine(BuySellStockIV(arr, k));
    }

    private int BuySellStockIV(int[] arr, int k)
    {
        // If the transactions are more than half, try buy / sell at alternating days
        if (k >=  arr.Length/2)
        {
            int res = 0;
            for (int i = 1; i < arr.Length; i++) 
            {
                if (arr[i] > arr[i-1])
                    res += arr[i] - arr[i-1];
            }

            return res;
        }

        int[,] dp = new int[k+1, arr.Length];
        int max = int.MinValue;
        int maxDiff = int.MinValue;

        for(int row = 1; row <= k; row ++)
        {
            maxDiff = int.MinValue;
            for(int col = 1; col < arr.Length; col ++)
            {
                maxDiff = Math.Max((dp[row-1, col-1] - arr[col-1]), maxDiff);
                max = Math.Max(dp[row, col-1], arr[col] + maxDiff);
                dp[row, col] = max;
            }
        }

        return dp[k, arr.Length-1];
    }

    //https://leetcode.com/problems/minimum-cost-for-tickets/
    public void MinCostTickets()
    {
        int[]days = new int[] {1,4,6,7,8,20};
        int[] costs = new int[] {2,7,15};

        Console.WriteLine(MinCostTickets(days, costs));
    }

    private int MinCostTickets(int[] arr, int[] costs)
    {
        int[,] dp = new int[arr.Length, arr.Length];
        int cost = 0;
        int minCost = int.MaxValue;

        for(int i = 0; i < arr.Length; i++)
        {
            int startIdx = i;

            cost+= (i-0)* costs[0];

            for(int j = 0; j < arr.Length; j++)
            {
                if(j == arr.Length -1 || j +1 < arr.Length  && arr[j+1] - arr[startIdx] +1 > 7)
                {
                    cost += Math.Min((j-startIdx +1) * costs[0], costs[1]);
                    startIdx = j+1;
                }
            }

            minCost = Math.Min(cost, minCost);
            cost = 0;
        }

        return minCost;
    }

    /*
    FaceBook
    A person goes for a movie on certain days in a month. Find the min cost of watching a movie with following conditions:
    Daily pass: $2, Weekly pass: $6, Monthly pass: $22
    */
    //Accepted-LcMedium-LcSol-T:O(n)-S:O(n) https://leetcode.com/problems/minimum-cost-for-tickets/
    public void MinCostForMovie()
    {
        // int[] arr = new int[]{1, 7, 12, 15, 18, 19, 23};
        // int[] week = new int[]{2, 3, 4, 3, 3, 2, 1};

        int[] days = new int[]{1,2,3,4,5,6,7,8,9,10,30,31};
        int[] costs = new int[] {2,7,15};
        Console.WriteLine(MinCostTicketsLC(days, costs));
    }

    private int MinCostTicketsLC(int[] days, int[] costs)
    {
        int[] dp = new int[days.Max()+1];
        HashSet<int> travelDays = new HashSet<int>();
        foreach(int day in days)
        {
            travelDays.Add(day);
        }

        for(int i = 1; i <= days.Max(); i++)
        {
            if (!travelDays.Contains(i))
            {
                dp[i] = dp[i-1];
                continue;
            }

            dp[i] = costs[0] + dp[i-1];
            dp[i] = Math.Min(costs[1] + dp[Math.Max(i-7, 0)], dp[i]);
            dp[i] = Math.Min(costs[2] + dp[Math.Max(i-30, 0)], dp[i]);
        }

        return dp[days.Max()];
    }

    //https://leetcode.com/problems/maximal-rectangle/
    public void MaximalRectangle()
    {
        char[] arr = new char[]{'3','1','3','2','2'};
        Console.WriteLine(MaximalRectangle(arr));
    }

    private int MaximalRectangle(char[] matrix)
    {
        return MaxRectangle(matrix);
    }

    private int MaxRectangle(char[] arr)
    {
        Stack<int> stk = new Stack<int>();

        stk.Push(0);
        int idx = 1;
        int max = 0;

        while(idx < arr.Length && stk.Count > 0)
        {
            max = Math.Max(CalcHistogram(stk, arr, int.Parse(arr[idx].ToString()), idx), max);

            stk.Push(idx++);
        }

        while(stk.Count > 0)
        {
            var item = stk.Pop();
            max = Math.Max(CalcHistogram(stk, arr, item, idx), max);
        }

        return max;
    }

    private int CalcHistogram(Stack<int> stk, char[] arr, int item, int idx)
    {
        int cur = 0;
        while (stk.Count > 0 && item <= int.Parse(arr[stk.Peek()].ToString()))
        {
            var pop = stk.Pop();
            cur = stk.Count > 0 ? int.Parse(arr[pop].ToString()) * (idx - stk.Peek() -1) : int.Parse(arr[pop].ToString()) * idx;
        }

        return cur;
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
}