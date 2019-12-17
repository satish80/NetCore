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
        wordDict.Add("apple");
        wordDict.Add("abc");
        wordDict.Add("b");
        wordDict.Add("cd");

        //var coll = WordBreakII("pineapplepenapple", wordDict);
        var coll = WordBreakII("catsanddog", wordDict);
        //var coll = WordBreakII("abcd", wordDict);

        foreach(string str in coll)
        {
            Console.WriteLine(str);
        }
    }

    private List<string> WordBreakII(string s, IList<string> wordDict)
    {
        Dictionary<int, HashSet<string>> map = new Dictionary<int, HashSet<string>>();
        List<string> res = new List<string>();
        int lastIdx = -1;

        int i = 0;
        Queue<int> queue = new Queue<int>();

        while(i < s.Length)
        {
            for(int j = 1; j + i -1 < s.Length; j ++)
            {
                if(wordDict.Contains(s.Substring(i, j)))
                {
                    if (!map.ContainsKey(i))
                    {
                        map.Add(i, new HashSet<string>());
                    }

                    map[i].Add(s.Substring(i, j));
                    lastIdx = i+j;
                    queue.Enqueue(i+j);
                }
            }

            if (queue.Count == 0)
            {
                i++;
            }
            else
            {
                i = queue.Dequeue();
            }
        }

        if (lastIdx < 0 || (lastIdx < s.Length))
        {
            return res;
        }

        var coll = GetSuffix(0, map, new Dictionary<int, HashSet<string>>());
        foreach(string str in coll)
        {
            res.Add(str);
        }

        return res;
    }

    private HashSet<string> GetSuffix(int idx, Dictionary<int, HashSet<string>> map, Dictionary<int, HashSet<string>> res)
    {
        foreach(string str in map[idx])
        {
            int next = idx + str.Length;

            if (map.ContainsKey(next))
            {
                HashSet<string> coll = null;

                if (!res.ContainsKey(next))
                {
                    coll = GetSuffix(next, map, res);
                }
                else
                {
                    coll = res[next];
                }

                foreach(string s in coll)
                {
                    if (!res.ContainsKey(idx))
                    {
                        res.Add(idx, new HashSet<string>());
                    }

                    res[idx].Add(str + " " + s);
                }
            }
            else if (!res.ContainsKey(idx))
            {
                res[idx] = map[idx];
            }
        }

        return res[idx];
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

    //https://leetcode.com/problems/palindromic-substrings/
    // Return the palindromic sub strings for a given string
    public void FindPalindromeSubstrings()
    {
        Console.WriteLine(countSubstrings("abbaefgfl"));
    }

    private int FindPalindromeSubstrings(string input)
    {
        int[,] dp = new int[input.Length, input.Length];

        char[] arr = input.ToCharArray();
        Array.Reverse(arr);
        string rev = new string(arr);

        for(int row = 0; row < input.Length; row ++)
        {
            for(int col = 0; col < input.Length; col ++)
            {
                if (rev[row] == input[col])
                {
                    dp[row, col] = 1;
                }
            }
        }

        int count = input.Length;
        
        for(int row = 0; row < input.Length; row ++)
        {
            for(int col = 0; col < input.Length; col ++)
            {
                if (dp[row, col] == 1)
                {
                    var r = row;
                    var c = col;

                    bool found = false;

                    while(r < input.Length && c < input.Length && dp[r,c] == 1)
                    {
                        if (r+1 < input.Length && c+1 < input.Length )
                        {
                            if (input[c+1] == input[c] && rev[r+1] == rev[r])
                            {
                                count +=1;
                            }
                            else
                            {
                                found = true;
                            }
                        }

                        dp[r, c] = 0;

                        r++;
                        c++;
                    }

                    if (found)
                    {
                        found = false;
                        count +=1;
                    }
                }
            }
        }

        return count;
    }

    private int countSubstrings(String s) 
    {
        int n = s.Length;
        int res = 0;
        bool[,] dp = new bool[n,n];
        for (int i = n - 1; i >= 0; i--) 
        {
            for (int j = i; j < n; j++) 
            {
                dp[i,j] = s[i] == s[j] && (j - i < 3 || dp[i + 1, j - 1]);
                if(dp[i,j])
                {
                    ++res;
                }
            }
        }
        
        return res;
    }

    //https://leetcode.com/problems/partition-to-k-equal-sum-subsets/
    public void PartitionKSubsetsMatchingSum()
    {
        int[] arr = new int[] {4, 3, 2, 3, 5, 2, 1};
        Console.WriteLine(PartitionKSubsetsMatchingSum(arr, 4));
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
}