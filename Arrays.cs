using DataStructures;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

public class Arrays
{
    //LCMedium-LCSol-Accepted-T:O(n)-S:O(n): https://leetcode.com/problems/partition-labels/
    public void PartitionStringWithUniqueChars()
    {
        Console.WriteLine(PartitionLabels("ababcbacadefegdehijhklij"));
    }

    private IList<int> PartitionLabels(string S)
    {
        int[] map = new int[26];
        List<int> res = new List<int>();

        for(int idx = 0; idx < S.Length; idx++)
        {
            map[S[idx] - 'a'] = idx;
        }

        int start = 0;
        int last = 0;

        for(int idx = 0; idx < S.Length; idx ++)
        {
            last = Math.Max(last, map[S[idx] -'a']);

            if (last == idx)
            {
                res.Add(last - start +1);
                start = last + 1;
            }
        }

        return res;
    }

    private int FindPartitionLabel(IDictionary<int, HashSet<char>> map)
    {
        return 0;
    }

    private IList<int> PartitionStringWithUniqueChars(string input)
    {
        Dictionary<char, int> map = new Dictionary<char, int>();
        IList<int> res = new List<int>();
        List<int> split = new List<int>();

        int idx = 0;

        for(; idx < input.Length; idx++)
        {
            if (!map.ContainsKey(input[idx]))
            {
                map.Add(input[idx],idx);
            }
            else
            {
                split.Add(idx);
                map[input[idx]] = idx;
            }
        }

        idx = 0;
        int start = 0;
        int cur = start;
        int end = split[idx++];

        while (cur < input.Length)
        {
            while (cur < end)
            {
                if (map[input[cur]] > end)
                {
                    end = map[input[cur]];
                    while (idx + 1 < split.Count && split[idx] < end)
                    {
                        idx ++;
                    }
                }

                cur++;
            }
            
            res.Add(end-start + 1);

            start = end+1;
            cur = start;
            end = split[idx];
            
            if (idx + 1 < split.Count)
             {
                 idx++;
             }
        }

        return res;
    }

    //https://leetcode.com/articles/queue-reconstruction-by-height/#
    public void QueueReconstructionByHeight()
    {
        Pair p1 = new Pair(7,0);
        Pair p2 = new Pair(5,3);
        Pair p3 = new Pair(4,4);
        Pair p4 = new Pair(6,1);
        Pair p5 = new Pair(5,2);
        Pair p6 = new Pair(7,1);
        var pairs = new List<Pair>();

        pairs.AddRange(new Pair[] {p1, p2, p3, p4, p5, p6});

        var res = QueueReconstructionByHeight(pairs);
    }

    private List<Pair> QueueReconstructionByHeight(List<Pair> pairs)
    {
        IEnumerable<Pair> sorted = pairs.OrderByDescending(t=>t.x).ThenBy(t=>t.y);
        Stack<Pair> stk = new Stack<Pair>();
        List<Pair> output = new List<Pair>();

        var sortedEnumerator = sorted.GetEnumerator();
        while(sortedEnumerator.MoveNext())
        {
            if (sortedEnumerator.Current.y == output.Count())
            {
                output.Add(sortedEnumerator.Current);
            }
            else if (sortedEnumerator.Current.y < output.Count())
            {
                int idx = output.Count()-1;
                
                while(idx > 0 && sortedEnumerator.Current.y < output.Count())
                {
                    stk.Push(output[idx]);
                    output.RemoveAt(idx--);
                }

                output.Add(sortedEnumerator.Current);
            }
            else
            {
                while(stk.Count > 0 && sortedEnumerator.Current.y > output.Count)
                {
                    if (stk.Peek().y < output.Count())
                    {
                        output.Add(stk.Pop());
                    }
                }
            }
        }

        while(stk.Count > 0)
        {
            output.Add(stk.Pop());
        }

        return output;
    }

    public void TestHeap()
    {
        Heap<int> heap = new Heap<int>(true);
        heap.Push(7);
        heap.Push(2);
        heap.Push(3);
        heap.Push(1);
        heap.Push(8);
        Console.WriteLine(heap.Pop());
        Console.WriteLine(heap.Pop());
        Console.WriteLine(heap.Pop());
        Console.WriteLine(heap.Pop());
        Console.WriteLine(heap.Pop());
    }

    //https://leetcode.com/problems/shortest-subarray-with-sum-at-least-k/
    // Leetcode has invalid test case
    public void SubArraySumAtleastK()
    {
        int[] num = new int [] {-2, -1, 3, 5, 4, 2, 1, 6, -5, 8};
        //int[] num = new int [] {48,99,37,4,-31}; // invalid leetcode test case
        Console.WriteLine(SubArraySumAtleastK(num, 140));
    }

    private int SubArraySumAtleastK(int[] num, int k)
    {
        int end = 0;
        int start = 0;
        int sum = num[start];
        int len = int.MaxValue;

        while (end < num.Length-1)
        {
            if (sum < k)
            {
                end ++;
                sum += num[end];
                continue;
            }

            if (sum == k)
            {
                len = Math.Min(len, end - start + 1);
            }

            sum -= num[start++];
        }

        if (sum == k)
        {
            len = Math.Min(len, end-start +1);
        }

        return len = len == int.MaxValue ? -1 : len;
    }

    //https://leetcode.com/problems/shortest-subarray-with-sum-at-least-k/
    public void ShortestSubArrayWithSumAtleastK()
    {
        int[] arr = new int[] {2, -1, 2};
        int k = 3;
        Console.WriteLine(ShortestSubArrayWithSumAtleastK(arr, k));
    }

    private int ShortestSubArrayWithSumAtleastK(int[] arr, int k)
    {
        Dictionary<int, int> map = new Dictionary<int, int>();
        map.Add(0, arr[0]);

        for(int idx = 1; idx < arr.Length; idx ++)
        {
            map[idx] = map[idx-1] + arr[idx];
        }

        int min = int.MaxValue;
        int start = 0;
        List<int> range = new List<int>();

        for(int idx = 0; idx < arr.Length; idx++)
        {
            if (arr[idx] >= k)
            {
                min = 1;
                break;
            }

            if (map[idx] == k)
            {
                min = Math.Min(min, idx+1);
            }

            while (range.Count > 0 && (map[idx] - map[range[start]] >= k))
            {
                min = Math.Min(min, idx - range[start]);
                range.RemoveAt(start);
            }

            while (range.Count > 0 && map[idx] <= map[range.Last()])
            {
                range.RemoveAt(range.Count-1);
            }

            range.Add(idx);
        }

        return min;
    }

    //https://leetcode.com/problems/text-justification/
    public void TextJustification()
    {
        string[] words = new string[] {"This", "is", "an", "example", "of", "text", "justification."};
        int maxWidth = 16;

        var res = TextJustification(words, maxWidth);
    }

    private IList<string> TextJustification(string[] words, int maxWidth)
    {
        int count = 0;
        int start = 0;
        int curCount = 0;

        IList<string> res = new List<string>();

        for(int i = 0; i <= words.Length; i++)
        {
            if (count > 0)
            {
                curCount = 1 + count + words[i].Length;
            }
            else 
            {
                curCount = count + words[i].Length;
            }

            if (curCount > maxWidth || i == words.Length-1)
            {
                var cur = FlushWord(words, start, i-1, maxWidth, count);
                res.Add(cur);
                start = i;
                curCount = 0;
                count = 0;
            }

            count += count > 0 && i < words.Length-1 ? 1 : 0;
            count+= words[i].Length;
        }

        return res;
    }

    private string FlushWord(string[] words, int start, int end, int maxWidth, int count)
    {
        StringBuilder sb = new StringBuilder();
        int diff = maxWidth - count;
        int spaces = diff / (end-start);
        int excess = diff % (end-start) ;

        sb.Append(words[start]);

        for(int idx = start+1; idx <= end; idx++)
        {
            int curSpaces = spaces;

            while (curSpaces-- > 0 || excess-- > 0)
            {
                sb.Append(" ");
            }

            sb.Append(words[idx]);
        }

        return sb.ToString();
    }

    //Accepted: T:(n) S: O(n) https://leetcode.com/problems/subarray-sum-equals-k/
    public void SubArraySumK()
    {
        int[] arr = new int[] {1, 1, 1};
        Console.WriteLine(SubArraySumK(arr, 2));
    }

    private int SubArraySumK(int[] arr, int k)
    {
        Dictionary<int, int> map = new Dictionary<int, int>();
        int sum = 0;
        int count = 0;
        map.Add(0, 1);

        for(int idx = 0; idx < arr.Length; idx ++)
        {
            sum += arr[idx];

            if (map.ContainsKey(sum - k))
            {
                count += map[sum - k];
            }

            if (! map.ContainsKey(sum))
            {
                map.Add(sum, 0);
            }

            map[sum] += 1;
        }

        return count;
    }

    /*
    Oracle
    Given a string, return the smallest possible string lexicographically, by swapping the chars at the given list of indices
    input: "cadeh"
    indices which can be swapped: [0,1] [1,3], [2,4]
    */
    public void ReturnLexicographicBySwapping()
    {
        string s = "cadeh";
        int[] p1 = new int[] {0,1};
        int[] p2 = new int[] {1,3};
        int[] p3 = new int[] {2,4};

        List<int[]> indices = new List<int[]>() {p1, p2, p3};
        Console.WriteLine(ReturnLexicographicBySwapping(s, indices));
    }

    private string ReturnLexicographicBySwapping(string s, List<int[]> indices)
    {
        Dictionary<int, List<int>> adjList = new Dictionary<int, List<int>>();
        int[] parent = new int[5];
        int[] size = new int[5];

        foreach(int[] edge in indices)
        {
            parent[edge[0]] = edge[0];
            parent[edge[1]] = edge[1];
        }

        foreach(int[] edge in indices)
        {
            Union(edge[0], edge[1], size, parent, adjList);
        }

        char[] arr = s.ToCharArray();

        foreach(KeyValuePair<int, List<int>> pair in adjList)
        {
            Sort(pair, arr);
        }

        return new string(arr);
    }

    private void Sort(KeyValuePair<int, List<int>> pair, char[] arr)
    {
        List<int> indices = pair.Value;
        indices.Add(pair.Key);
        indices.Sort();

        int start = 0;

        StringBuilder sb = new StringBuilder();

        for(int end = 1; end < indices.Count; end++)
        {
            while (end < indices.Count && indices[end] - indices[end-1] == 1)
            {
                end++;
            }

            string cur = string.Empty;

            if (end - start > 1)
            {
                Array.Sort(arr, start, end-start);
            }
            else
            {
                if (arr[start] > arr[end])
                {
                    char temp = arr[start];
                    arr[start] = arr[end];
                    arr[end] = temp;
                }
            }
        }
    }

    private bool Union(int x, int y, int[] size, int[] parent, Dictionary<int, List<int>> adjList)
    {
        int rootX = Find(x, parent);
        int rootY = Find(y, parent);

        if (rootX == rootY)
        {
            return false;
        }

        if (size[rootX] > size[rootY])
        {
            parent[rootX] = rootY;
            size[rootY]++;

            if (!adjList.ContainsKey(rootY))
            {
                adjList[rootY] = new List<int>();
            }

            adjList[rootY].Add(rootX);
        }
        else
        {
            parent[rootY] = rootX;
            size[rootX]++;

            if (!adjList.ContainsKey(rootX))
            {
                adjList[rootX] = new List<int>();
            }

            adjList[rootX].Add(rootY);
        }

        return true;
    }

    private int Find(int x, int[] parent)
    {
        if (parent[x] == x)
        {
            return x;
        }

        parent[x] = Find(parent[x], parent);

        return parent[x];
    }

    //https://leetcode.com/problems/meeting-rooms-ii/
    public void MinMeetingRoomsII()
    {
        int[][] intervals = new int[][]
        {
            // new int[] {2, 6},
            // new int[] {4, 5},
            // new int[] {7, 10},
            // new int[] {8, 12},
            new int[] {2,11},
            new int[] {6, 16},
            new int[] {11, 16}
        };

        Console.WriteLine(MinMeetingRoomsII(intervals));
    }

    private int MinMeetingRoomsII(int[][] intervals)
    {
        int[] starts = new int[intervals.Length];
        int[] ends = new int[intervals.Length];

        for(int i=0; i<intervals.Length; i++)
        {
            starts[i] = intervals[i][0];
            ends[i] = intervals[i][1];
        }

        Array.Sort(starts);
        Array.Sort(ends);

        int rooms = 0;
        int endsItr = 0;

        for(int i=0; i<starts.Length; i++)
        {
            if(starts[i]<ends[endsItr])
                rooms++;
            else
                endsItr++;
        }
        return rooms;
    }

    private void Sort<T>(T[][] data, int col)
    { 
        Comparer<T> comparer = Comparer<T>.Default;
        Array.Sort<T[]>(data, (x,y) => comparer.Compare(x[col],y[col])); 
    }

    //Accepted: T:O(n), S:O(n): https://leetcode.com/problems/merge-intervals/
    public void MergeIntervals()
    {
        int[][] intervals = new int[][]
        {
            new int[]{1, 4},
            new int[]{2, 6},
            new int[]{8, 10},
            new int[]{15, 18},
        };

        var res = MergeIntervals(intervals);
    }

    private int[][] MergeIntervals(int[][] intervals)
    {
        if (intervals == null || intervals.Length == 0)
        {
            return intervals;
        }

        Sort(intervals, 0);
        List<int[]> res = new List<int[]>();
        int start = 0;
        int end = 0;
        int idx = 0;

        while(idx < intervals.Length)
        {
            start = intervals[idx][0];
            end = intervals[idx][1];

            while (idx < intervals.Length && intervals[idx][0] <= end)
            {
                end = Math.Max(intervals[idx][1], end);
                idx++;
            }

            res.Add(new int[] {start, end});
        }

        return res.ToArray();
    }

    //Accepted-LcMedium-SelfSol-T:O(n^2)-S:O(n^2) https://leetcode.com/problems/number-of-islands/
    public void NumIslands() 
    {
        int[] x = new int[]{-1, 1, 0,  0};
        int[] y = new int[]{ 0, 0, 1, -1};
        char[][] grid = new char[][]
        {
            new char[]{'1', '1', '1', '1', '0'},
            new char[]{'1', '1', '0', '1', '0'},
            new char[]{'1', '1', '0', '0', '0'},
            new char[]{'0', '0', '0', '0', '0'},
        };

        int count = 0;
        
        for(int r = 0; r < grid.Length; r++)
        {
            for(int c = 0; c < grid[0].Length; c++)
            {
                if (grid[r][c] == '1')
                {
                    Console.WriteLine("r: " + r + " c: " + c);
                    FindIslands(grid, x, y, r, c);
                    count++;
                }
            }
        }
        
        Console.WriteLine(count);
    }
    
    private void FindIslands(char[][] grid, int[] x, int[] y, int r, int c)
    {
        Queue<int[]> queue = new Queue<int[]>();
        
        queue.Enqueue(new int[] {r,c});
        
        while (queue.Count > 0)
        {
            var item = queue.Dequeue();
            Console.WriteLine("Clearing r: " + item[0] + " c: " + item[1]);
            grid[item[0]][item[1]] = '0';
            
            for(int idx = 0; idx < x.Length; idx++)
            {
                int row = item[0] + x[idx];
                int col = item[1] + y[idx];
                
                if (row < 0 || col < 0 || row > grid.Length || col > grid[0].Length || grid[row][col] == '0')
                {
                    continue;
                }
                
                queue.Enqueue(new int[] {row, col});
            }
        }
    }

    /*
    FaceBook
    Find number of Isnalds in T:O(n^2) & S:O(1)
    */
    public void FindIslands()
    {
        int[][] arr = new int[][]
        {
            new int[] {1, 1, 1},
            new int[] {0, 1, 0},
            new int[] {1, 1, 1},
        };

        int res = 0;

        for(int row = 0; row < arr.Length; row ++)
        {
            for(int col = 0; col < arr[0].Length; col ++)
            {
                if (arr[row][col] == 1)
                {
                    res +=FindIslands(arr, row, col);
                }
            }
        }

        Console.WriteLine(res);
    }

    private int FindIslands(int[][] arr, int row, int col)
    {
        if ((col -1 >=0  && arr[row][col-1] == 1 || row-1 >= 0 && arr[row-1][col] == 1))
        {
            return 0;
        }

        return 1;
    }

    public void GCD()
    {
        Console.WriteLine(GCD(1701, 3768));
    }

    private int GCD(int x, int y)
    {
        if (x < y)
        {
            return GCD(y, x);
        }

        int temp = 0;

        while (y > 0)
        {
            temp = x;
            x = y;
            y = temp % y;
        }

        return y == 0 ? x : -1;
    }

    //LCHard Accepted: T:O(n), S:O(n): https://leetcode.com/problems/insert-interval/
    public void InsertInterval()
    {
        int[][] intervals = new int[][]
        {
            new int[]{1,2},
            new int[]{3,5},
            new int[]{6,7},
            new int[]{8,10},
            new int[]{12,16},
        };

        var res = InsertInterval(intervals, new int[]{4,8});
    }

    private int[][] InsertInterval(int[][] intervals, int[] newInterval)
    {
         List<int[]> resList = new List<int[]>();

        if (intervals == null || intervals.Length == 0)
        {
            resList.Add(newInterval);
            return resList.ToArray();
        }

        int cur = 0;

        while(cur < intervals.Length && newInterval[0] > intervals[cur][1])
        {
            resList.Add(intervals[cur++]);
        }

        int start = newInterval[0];
        int end = newInterval[1];

        //merge
        while (cur < intervals.Length && newInterval[1] >= intervals[cur][0])
        {
            start = Math.Min(intervals[cur][0], start);
            end = Math.Max(intervals[cur][1], end);
            cur++;
        }

        resList.Add(new int[]{start, end});

        while (cur < intervals.Length)
        {
            resList.Add(new int[]{intervals[cur][0], intervals[cur][1]});
            cur++;
        }

        return resList.ToArray();
    }

    //Accepted-LCDiscussSol-LCHard-T: O(n^2): https://leetcode.com/problems/split-array-largest-sum/
    public void SplitLargestSum()
    {
        int[] arr  =new int[]{7, 2, 5, 10, 8, 11, 4};
        int m = 3;
        Console.WriteLine(splitArray(arr, m));
    }

    public int splitArray(int[] nums, int m)
    {
        int max = 0; long sum = 0;
        foreach (int num in nums)
        {
            max = Math.Max(num, max);
            sum += num;
        }
        if (m == 1)
        {
             return (int)sum;
        }

        //binary search
        long l = max; long r = sum;
        while (l <= r)
        {
            long mid = (l + r)/ 2;
            if (valid(mid, nums, m))
            {
                r = mid - 1;
            }
            else
            {
                l = mid + 1;
            }
        }

        return (int)l;
    }

    public bool valid(long target, int[] nums, int m)
    {
        int count = 1;
        long total = 0;

        foreach(int num in nums)
        {
            total += num;
            if (total > target)
            {
                total = num;
                count++;
                if (count > m)
                {
                    return false;
                }
            }
        }

        return true;
    }

    //Accepted-LCMedium-T:O(n): https://leetcode.com/problems/minimum-size-subarray-sum/
    public void MinSubArrayLen()
    {
        Console.WriteLine(MinSubArrayLen(100, new int[] {}));
    }

    private int MinSubArrayLen(int s, int[] nums)
    {
        if (nums == null || nums.Length == 0 || nums.Length == 1)
        {
            return 0;
        }

        int min = int.MaxValue;
        int sum = 0;
        int start = 0;
        int end = 0;

        while (end < nums.Length)
        {
            sum += nums[end];

            while (sum >= s)
            {
                min = Math.Min(end - start + 1, min);
                sum -= nums[start];
                start++;
            }

            end++;
        }

        return min == int.MaxValue ? 0 : min;
    }

    public void Permutations()
    {
        int[] arr = new int[] {1, 2, 3};
        Permutations(arr, 3, 0);
    }

    private void Permutations(int[] arr, int k, int n)
    {
        if (n > arr.Length)
        {
            return;
        }

        if (k == n)
        {
            Print(arr);
            return;
        }

        for(int i = n; i < k; i ++)
        {
            Helpers.Swap(arr, i, n);
            Permutations(arr, k, n+1);
            Helpers.Swap(arr, n, i);
        }
    }

    private void Print(int[] arr)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            Console.WriteLine($"{arr[i]} \t");
        }
        Console.WriteLine("-----------------");
    }

    //LCHard-Accepted-LCSol-T:O(n) S:O(1):https://leetcode.com/problems/first-missing-positive/
    public void FirstMissingPositive()
    {
        int[] arr = new int[] {0, 3,4,1};

        Console.WriteLine(FirstMissingPositive(arr, 3));
    }

    private int FirstMissingPositive(int[] nums, int n)
    {
        for(int idx = 0; idx < nums.Length; idx++)
        {
            // Place the array contents at the index positions
            while (nums[idx] > 0 && nums[idx] <= n && nums[nums[idx]-1] != nums[idx])
            {
                Helpers.Swap(nums, idx, nums[idx]-1);
            }
        }

        for(int idx = 0; idx < nums.Length; idx++)
        {
            if (nums[idx] != idx+1)
            {
                return idx+1;
            }
        }

        return n+1;
    }

    //Accepted:LCEasy-SelfSol-T:O(n)-S:O(1) https://leetcode.com/problems/kth-missing-positive-number/
    public void FindKthMissingPositive()
    {
        int[] arr = new int[] {5,6,7,8,9};
        int  k = 9;
        Console.WriteLine(FindKthPositive(arr, k));
    }

    private int FindKthPositive(int[] arr, int k)
    {
        int prev = 0;
        int max = int.MinValue;

        for(int idx = 0; idx < arr.Length && k > 0; idx++)
        {
            prev = idx > 0 ? arr[idx-1] : prev;

            if (arr[idx] - prev > 1)
            {
                int diff = arr[idx] - (prev +1);
                k -= diff;

                if (k == 0)
                {
                    return prev + diff;
                }
                else if (k < 0)
                {
                    return arr[idx] + k-1;
                }
            }

            max = Math.Max (max, arr[idx]);
        }

        int res = k > 0 ? max + k : 0;
        
        return res;
    }

    //https://leetcode.com/problems/remove-invalid-parentheses/
    public void RemoveInvalidParanthesis()
    {
        string s = "()())()";
        var res = RemoveInvalidParanthesis(s, 0, new HashSet<string>());
    }

    private HashSet<string> RemoveInvalidParanthesis(string s, int idx, HashSet<string> res)
    {
        if (idx >= s.Length)
        {
            return res;
        }

        if (ValidParanthesis(s))
        {
            res.Add(s);
        }

        StringBuilder sb = new StringBuilder();

        if (idx >= 1)
        {
            sb.Append(s.Substring(0, idx));
        }

        sb.Append(s.Substring(idx+1, s.Length -1 - idx));

        RemoveInvalidParanthesis(s, idx+1, res);
        RemoveInvalidParanthesis(sb.ToString(), idx+1, res);

        return res;
    }

    private bool ValidParanthesis(string s)
    {
        Stack<char> stk = new Stack<char>();

        int idx = 0;

        while (idx < s.Length)
        {
            if (s[idx] == ')')
            {
                if (stk.Count() > 0 && stk.Peek() == '(')
                {
                    stk.Pop();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                stk.Push('(');
            }

            idx++;
        }

        return stk.Count() == 0;
    }

    //Accepted: LCMedium - Self - T:O(n): https://leetcode.com/problems/missing-element-in-sorted-array/
    public void FirstMissingInSortedArray()
    {
        int[] arr = new int[] {1, 2, 4};
        Console.WriteLine(FirstMissingInSortedArray(arr, 3));
    }

    private int FirstMissingInSortedArray(int[] arr, int k)
    {
        int idx = 0;
        int missingCount = 0;
        int cur = arr[0];
        int missing = 0;

        while (idx < arr.Length && missingCount < k)
        {
            while (arr[idx] > cur && missingCount < k)
            {
                missing = cur;
                missingCount++;
                cur++;
            }

            idx++;
            cur++;
        }

        while (missingCount < k)
        {
            missing = cur++;
            missingCount++;
        }

        return missing;
    }

    //Accepted:LCHard:LCSol:T:O(n^2):S:O(n) https://leetcode.com/problems/largest-rectangle-in-histogram/
    public void LargestRectangleArea()
    {
        int[] arr = new int[]{2,1,2};
        Console.WriteLine(LargestRectangleArea(arr));
    }

    private int LargestRectangleArea(int[] heights)
    {
        Stack<int> stk = new Stack<int>();
        int max = int.MinValue;
        int area = 0;
        int idx = 0;

        for(;idx < heights.Length;)
        {
            // Push if the upcoming height is greater than the current in the stack
            if (stk.Count == 0 || heights[idx] >= heights[stk.Peek()])
            {
                stk.Push(idx++);
            }
            else
            {
                //Pop if the upcoming one is less than the stk.Peek()
                var top = stk.Pop();

                if (stk.Count == 0)
                {
                    area = heights[top] * idx;
                }
                else
                {
                    area = heights[top] * (idx - stk.Peek() - 1);
                }

                max = Math.Max(area , max);
            }
        }

        while (stk.Count > 0)
        {
            var top = stk.Pop();

            if (stk.Count == 0)
            {
                area = heights[top] * idx;
            }
            else
            {
                area = heights[top] * (idx - stk.Peek() - 1);
            }

            max = Math.Max(area , max);
        }

        return max;
    }

    //Accepted-LcHard-LCSol-T:O(n)-S:O(n) https://leetcode.com/problems/valid-number/
    public void ValidNumber()
    {
        Console.WriteLine(ValidNumber("9e2.5"));
    }

    private bool ValidNumber(string s)
    {
        bool seenDigit = false;
        bool seenExponent = false;
        bool seenDot = false;
        
        for (int i = 0; i < s.Length; i++)
        {
            char curr = s[i];
            if (char.IsDigit(curr))
            {
                seenDigit = true;
            }
            else if (curr == '+' || curr == '-')
            {
                if (i > 0 && s[i - 1] != 'e' && s[i - 1] != 'E')
                {
                    return false;
                }
            }
            else if (curr == 'e' || curr == 'E')
            {
                if (seenExponent || !seenDigit)
                {
                    return false;
                }
                seenExponent = true;
                seenDigit = false;
            }
            else if (curr == '.')
            {
                if (seenDot || seenExponent)
                {
                    return false;
                }
                seenDot = true;
            }
            else
            {
                return false;
            }
        }
        
        return seenDigit;
    }

    //Accepted-LcMedium-SelfSol-T:O(m+n)-S:O(m+n) https://leetcode.com/problems/repeated-string-match/
    public void RepeatedStringMatch()
    {
        string a = "abcd";
        string b = "cdabcdab";

        Console.WriteLine(RepeatedStringMatch(a, b));
    }

    private int RepeatedStringMatch(string a, string b)
    {
        int count = 0;
        StringBuilder sb = new StringBuilder();
        
        while(sb.Length < b.Length + a.Length)
        {
            sb.Append(a);
            count++;

            if (sb.ToString().Contains(b))
            {
                return count;
            }
        }
        
        return -1;
    }

    //Accepted:LCHard:LCSol:T:O(n^2):S:O(n) https://leetcode.com/problems/maximal-rectangle/
    public void MaximalRectangle()
    {
        char[][] matrix = new char[][]
        {
            new char[] {'1','0','1','0','0'},
            new char[] {'1','0','1','1','1'},
            new char[] {'1','1','1','1','1'},
            new char[] {'1','0','0','1','0'}
        };

        Console.WriteLine(MaximalRectangle(matrix));
    }

    private int MaximalRectangle(char[][] matrix)
    {
        if (matrix.Length == 0)
        {
            return 0;
        }

        int[] res = new int[matrix[0].Length];
        int max = int.MinValue;
        
        for(int r = 0; r < matrix.Length; r++)
        {
            for(int c = 0; c < matrix[r].Length; c++)
            {
                if (r == 0)
                {
                    res[c] = int.Parse(matrix[r][c].ToString());
                }
                else
                {
                    res[c] = (matrix[r][c] == '1' && matrix[r-1][c] == '1') ? res[c] + 1 : int.Parse(matrix[r][c].ToString());
                }
            }

            int val = LargestRectangle(res);
            max = Math.Max(max, val);                                                                                       
        }
        
        return max;
    }

    private int LargestRectangle(int[] res)
    {
        int area = 0;
        int max = int.MinValue;
        
        Stack<int> stk = new Stack<int>();
        
        int idx = 0;
        
        while (idx < res.Length)
        {
            if (stk.Count == 0 || res[idx] >= res[stk.Peek()])
            {
                stk.Push(idx++);
            }
            else
            {
                int top = stk.Pop();
                
                if (stk.Count == 0)
                {
                    area = res[top] * idx;
                }
                else
                {
                     area = res[top] * (idx - stk.Peek() -1);
                }
                
                max = Math.Max(area, max);
            }
        }
        
        while (stk.Count > 0)
        {
            int top = stk.Pop();
                
            if (stk.Count() == 0)
            {
                area = res[top] * idx;
            }
            else
            {
                 area = res[top] * (idx - stk.Peek() -1);
            }

            max = Math.Max(area, max);
        }
        
        return max;
    }

    //Accepted-LcHard-LCSol-https://leetcode.com/problems/string-compression-ii/
    public void GetLengthOfOptimalCompression()
    {
        string s = "aaabcccd";
        int k = 2;
        Console.WriteLine(GetLengthOfOptimalCompression(0, ' ', 0, k, s));
    }

    private int GetLengthOfOptimalCompression(int start, char last, int last_count, int left, string s)
    {
        if (left < 0)
        {
            return int.MaxValue;
        }

        if (start >= s.Length)
        {
            return 0;
        }

        if (s[start] == last)
        {
            // we have a stretch of the last_count of the same chars, what is the cost of adding one more? 
            int incr = last_count == 1 || last_count == 9 || last_count == 99 ? 1 : 0;

            //no need to delete here, if we have a stretch of chars like 'aaaaa' - we delete it from the beginning in the else delete section
            return incr + GetLengthOfOptimalCompression(start+1, last, last_count+1, left, s); // # we keep this char for compression
        }
        else
        {
            //# keep this char for compression - it will increase the result length by 1 plus the cost of compressing the rest of the string 
            int keep_counter = 1 + GetLengthOfOptimalCompression(start+1, s[start], 1, left, s);

            //# delete this char
            int del_counter =  GetLengthOfOptimalCompression(start + 1, last, last_count, left - 1, s);

            return Math.Min(keep_counter, del_counter);
        }
    }

    public void JumpingOnClouds()
    {
        int[] num = new int[] {0, 1, 0};
        Console.WriteLine(JumpingOnClouds(num));
    }

    private int JumpingOnClouds(int[] c) 
    {
        int count = 0;
        int cur = 0;

        while (cur < c.Length-1)
        {
            if (cur+2 < c.Length && c[cur+2] < 1 )
            {
                count++;
                cur= cur + 2;
            }
            else if (cur+1 < c.Length && c[cur+1] < 1 )
            {
                count++;
                cur = cur + 1;
            }
        }

        return count;
    }

    //LCMedium-Self-T:O(n)-S:O(n):https://leetcode.com/problems/jump-game-iii/
    public void CanReach()
    {
        int[] arr  = new int[] {4,2,3,0,3,1,2};
        bool[] visited = new bool[arr.Length];
        Console.WriteLine(CanReach(arr, visited, 0));
    }

    private bool CanReach(int[] arr, bool[] visited, int cur)
    {
        bool res = false;

        if (visited[cur])
        {
            return false;
        }

        visited[cur] = true;

        if (arr[cur] == 0)
        {
            return true;
        }

        if (cur + arr[cur] < arr.Length)
        {
            res = CanReach(arr, visited, cur + arr[cur]);
        }

        if (!res && cur + arr[cur] >= 0)
        {
            res = CanReach(arr, visited, Math.Abs(cur - arr[cur]));
        }

        return res;
    }

    //Accepted-LcMedium-SelfSol-T:O(N^2)-S:O(1) https://leetcode.com/problems/leftmost-column-with-at-least-a-one/
    public void LeftMostColumnWithOne()
    {
        int[][] arr = new int[][]
        {
            new int[] {0, 0, 0, 1},
            new int[] {0, 0, 1, 1},
            new int[] {0, 1, 1, 1},
        };

        var res = LeftMostColumnWithOne(arr);
    }

    private int LeftMostColumnWithOne(int[][] arr)
    {
        int r = arr.Length-1;
        int c = arr[0].Length-1;
        int min = int.MaxValue;

        while (r >= 0)
        {
            int left = 0;
            int right = c;

            while (left <= right)
            {
                int mid = (right-left) / 2 + left;

                if (arr[r][mid] == 1)
                {
                    min = Math.Min(min, mid);
                    right = mid-1;
                }
                else
                {
                    left = mid+1;
                }
            }

            r--;
        }

        return min == int.MaxValue ? -1 : min;
    }

    //Accepted-LCHard-SelfSol-T:O(n^2)-S:O(n^2)-https://leetcode.com/problems/making-a-large-island/
    public void LargestIsLand()
    {
        // int[][] grid = new int[][]
        // {
        //     new int[]{1, 0, 1, 1},
        //     new int[]{0, 0, 0, 1},
        //     new int[]{1, 1, 0, 0},
        //     new int[]{0, 0, 1, 1}
        // };

        int[][] grid = new int[][]
        {
            new int[]{1, 0, 1},
            new int[]{0, 0, 0},
            new int[]{0, 1, 1}
        };

        Console.WriteLine(LargestIsland(grid));
    }

    private int LargestIsland(int[][] grid)
    {
        Dictionary<int, int> map = new Dictionary<int, int>();
        int color = 2;
        int count = 0;
        int max = int.MinValue;

        for(int row = 0; row < grid.Length; row++)
        {
            for(int col = 0; col < grid[0].Length; col++)
            {
                if (grid[row][col] == 1)
                {
                    map[color] = MapIslands(grid, row, col, color, ref count);
                    max = Math.Max(max, map[color]);
                    count = 0;
                    color++;
                }
            }
        }

        HashSet<int> set = new HashSet<int>();

        for(int row = 0; row < grid.Length; row ++)
        {
            for(int col = 0; col < grid[0].Length; col++)
            {
                if (grid[row][col] == 0)
                {
                    if (col-1 >= 0 && map.ContainsKey(grid[row][col-1]) && !set.Contains(grid[row][col-1]))
                    {
                        count = map[grid[row][col-1]];
                        set.Add(grid[row][col-1]);
                    }

                    if (col+1 < grid[0].Length && map.ContainsKey(grid[row][col+1]) && !set.Contains(grid[row][col+1]))
                    {
                        count += map[grid[row][col+1]];
                        set.Add(grid[row][col+1]);
                    }

                    if (row-1 >= 0 && map.ContainsKey(grid[row-1][col]) && !set.Contains(grid[row-1][col]))
                    {
                        count += map[grid[row-1][col]];
                        set.Add(grid[row-1][col]);
                    }

                    if (row+1 < grid.Length && map.ContainsKey(grid[row+1][col]) && !set.Contains(grid[row+1][col]))
                    {
                        count += map[grid[row+1][col]];
                        set.Add(grid[row+1][col]);
                    }

                    max = Math.Max(max, ++count);
                }

                count = 0;
                set.Clear();
            }
        }

        return max;
    }

    private int MapIslands(int[][] grid, int row, int col, int color, ref int count)
    {
        if (row < 0 || col < 0 || row >= grid.Length || col >= grid[0].Length || grid[row][col] == 0)
        {
            return 0;
        }

        if (grid[row][col] > 1 )
        {
            return 0;
        }

        grid[row][col] = color;
        count++;

        MapIslands(grid, row, col+1, color, ref count);
        MapIslands(grid, row, col-1, color, ref count);
        MapIslands(grid, row+1, col, color, ref count);
        MapIslands(grid, row-1, col, color, ref count);

        return count;
    }

    //Accepted-LcMedium-SelfSol-T:O(n)-S:O(1) https://leetcode.com/problems/container-with-most-water/
    public void MaxArea()
    {
        int[] arr = new int[] {1,8,6,2,5,4,8,3,7};
        Console.WriteLine(MaxArea(arr));
    }

    private int MaxArea(int[] height)
    {
        int left = 0, right = height.Length -1;
        int max = 0, cur = 0;

        while (left < right)
        {
            int diff = Math.Abs(left - right);
            cur =  diff * Math.Min(height[left], height[right]);
            max = Math.Max(max, cur);
            cur = 0;

            if (height[left] < height[right])
            {
                left++; 
            }
            else
            {
                right--;
            }
        }

        return max;
    }

    //LCMedium-LCSol-T:O(n^2):S:O(n^2) https://leetcode.com/problems/spiral-matrix-ii/
    public void GenerateMatrix()
    {
        int n = 3;
        var res = GenerateMatrix(n);
    }

    private int[,] GenerateMatrix(int n)
    {
        int[,] matrix = new int[n,n];
        int rowStart = 0;
        int rowEnd = n-1;
        int colStart = 0;
        int colEnd = n-1;
        int num = 1; //change
        
        while (rowStart <= rowEnd && colStart <= colEnd)
        {
            for (int i = colStart; i <= colEnd; i ++)
            {
                matrix[rowStart,i] = num ++; //change
            }

            rowStart ++;
            
            for (int i = rowStart; i <= rowEnd; i ++)
            {
                matrix[i,colEnd] = num ++; //change
            }

            colEnd --;
            
            for (int i = colEnd; i >= colStart; i --)
            {
                if (rowStart <= rowEnd)
                    matrix[rowEnd,i] = num ++; //change
            }

            rowEnd --;
            
            for (int i = rowEnd; i >= rowStart; i --)
            {
                if (colStart <= colEnd)
                    matrix[i,colStart] = num ++; //change
            }

            colStart ++;
        }
        
        return matrix;
    }

    //https://leetcode.com/problems/verifying-an-alien-dictionary/
    public void IsAlienSorted()
    {
        string[] words = new string[] {"hello","leetcode"};
        string order = "hlabcdefgijkmnopqrstuvwxyz";
        Console.WriteLine(IsAlienSorted(words, order));
    }

    private bool IsAlienSorted(string[] words, string order)
    {
        int[] mapping = new int[26];

        for(int idx = 0; idx < order.Length; idx++)
        {
            mapping[order[idx]-'a'] = idx;
        }

        for(int idx = 1; idx < words.Length; idx++)
        {
            if (Bigger(words[idx-1], words[idx], mapping))
            {
                return false;
            }
        }

        return true;
    }

    private bool Bigger(string s1, string s2, int[] mapping)
    {
        for(int idx = 0; idx < Math.Min(s1.Length, s2.Length); idx++)
        {
            if (mapping[s1[idx]-'a'] > mapping[s2[idx]-'a'])
            {
                return true;
            }
        }

        return false;
    }

    //Accepted-LCMedium-LCSol-T:O(n)-S:O(1) https://leetcode.com/problems/sort-colors/
    public void SortColors()
    {
        int[] nums = new int[] {2,0,2,1,1,0};
        SortColors(nums);
    }

    private void SortColors(int[] nums)
    {
        int j = 0, k = nums.Length-1;
        for (int i=0; i <= k; i++)
        {
            if (nums[i] == 0)
            {
                Helpers.Swap(nums,i, j++);
            }
            else if (nums[i] == 2)
            {
                Helpers.Swap(nums, i--, k--);
            }
        }
    }

    //Accepted:LCMedium:LCSol-T:O(2^n) https://leetcode.com/problems/combination-sum/
    public void CombinationSum()
    {
        int[] arr=  new int[] {2,3,6, 7};
        var res = CombinationSum(arr, 7);
    }

    private IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        IList<IList<int>> res = new List<IList<int>>();
        List<int> list = new List<int>();

        CombinationSum(candidates, 0, target, list, res);

        return res;
    }

    private void CombinationSum(int[] candidates, int start, int remain, List<int> list, IList<IList<int>> res)
    {
        if (remain < 0)
        {
            return;
        }

        if (remain == 0)
        {
            res.Add(new List<int>(list));
        }
        else
        {
            for(int i = start; i < candidates.Length; i++)
            {
                list.Add(candidates[i]);
                CombinationSum(candidates, i, remain - candidates[i], list, res);
                list.Remove(candidates[i]);
            }
        }
    }

    //https://leetcode.com/problems/combination-sum-ii/
    public void CombinationSumII()
    {
        int[] arr = new int[] {1,1,2,5,6,7,10};
        
        IList<IList<int>> res = new List<IList<int>>();
        Array.Sort(arr);
        CombinationSumII(arr, 8, 0, 0, new List<int>(), res);
    }

    private IList<IList<int>> CombinationSumII(int[] candidates, int target, int idx, int sum, List<int> list, IList<IList<int>> res)
    {
        if (sum == target)
        {
            var cur = new List<int>(list);
            res.Add(cur);
            return res;
        }
        
        if (idx == candidates.Length)
        {
            return res;
        }
        
        for(int i = idx; i < candidates.Length; i ++)
        {
            if (i > idx && candidates[i] == candidates[i-1])
            {
                continue;
            }
            
            list.Add(candidates[i]);
            CombinationSumII(candidates, target, i+1, sum + candidates[i], list, res);
            list.RemoveAt(list.Count()-1);
        }
        
        return res;
    }

    //https://leetcode.com/problems/subsets/
    public void Subsets()
    {
        int[] nums = new int[] {1, 2, 3};
        IList<IList<int>> coll = new List<IList<int>>();
        var list = new List<int>();
        var res = Subsets(nums, 0, coll, list);
    }

    private IList<IList<int>> Subsets(int[] nums, int idx, IList<IList<int>> res, List<int> list)
    {
        res.Add(new List<int>(list));

        for(int i = idx; i < nums.Length; i++)
        {
            list.Add(nums[i]);
            Subsets(nums, i+1, res, list);
            list.RemoveAt(list.Count-1);
        }

        return res;
    }

    //Accepted-LCMedium-LCSol-T:O(2^n)-S:O(n) https://leetcode.com/problems/subsets-ii/
    public void SubsetsWithDuplicates()
    {
        int[] nums = new int[] {1, 2, 2};
        IList<IList<int>> coll = new List<IList<int>>();
        var list = new List<int>();
        Array.Sort(nums);
        SubsetsWithDup(nums, 0, list, coll);
    }
    private IList<IList<int>> SubsetsWithDup(int[] nums, int idx, IList<int> list, IList<IList<int>> res)
    {
        if (idx == nums.Length)
        {
            res.Add(new List<int>(list));
            return res;
        }
        

        for(int i = idx; i < nums.Length; i ++)
        {
            list.Add(nums[idx]);
            SubsetsWithDup(nums, i+1, list, res);
            list.RemoveAt(list.Count()-1);

        }
        
        
        return res;
    }

    private void SubsetsWithDuplicates(int[] nums, int idx, List<int> path, IList<IList<int>> res)
    {
        res.Add(new List<int>(path));

        for(int i = idx; i < nums.Length; i ++)
        {
            if(i == idx || nums[i] != nums[i-1])
            {
                path.Add(nums[i]);
                SubsetsWithDuplicates(nums, i+1, path, res);
                path.RemoveAt(path.Count-1);
            }
        }
    }

    //Accepted-LcMedium-SelfSol-T:O(n!)-S:O(n!) https://leetcode.com/problems/permutations/
    public void Permute()
    {
        int[] nums = new int[] {1,2,3};
        IList<IList<int>> res = new List<IList<int>>();
        Permute(nums, 0, new List<int>(), res);

        foreach(IList<int> list in res)
        {
            foreach(int i in list)
            {
                Console.WriteLine(i);
            }
        }
    }

    private IList<IList<int>> Permute(int[] nums, int idx, List<int> list, IList<IList<int>> res)
    {   
        if (list.Count == nums.Length)
        {
            res.Add(new List<int>(list));
            return res;
        }
        
        if (idx > nums.Length)
        {
            return res;
        }

        for(int i = 0; i < nums.Length; i++)
        {
            if (list.Contains(nums[i]))
            {
                continue;
            }
            
            list.Add(nums[i]);
            
            Permute(nums, i+1, list, res);
            
            list.RemoveAt(list.Count-1);
        }
        
        return res;
    }

    //Accepted-LcMedium-LCSol-T:O(nlogn)-S:O(1) https://leetcode.com/problems/cutting-ribbons/
    public void MaxLengthToCutRibbons()
    {
        int[] arr = new int[]{7,5,9};
        int k = 4;
        Console.WriteLine(MaxLengthToCutRibbons(arr, k));
    }

    private int MaxLengthToCutRibbons(int[] ribbons, int k)
    {
        int lo = 1;
        int hi = ribbons.Max();
        int maxCutLength = 0;

        while (lo < hi)
        {
            int mid = (hi- lo) / 2 + lo;
            if (IsValidRibbonCut(ribbons, mid, k))
            {
                maxCutLength = mid;
                lo = mid+1;
            }
            else
            {
                hi = mid-1;
            }
        }

        return maxCutLength;
    }

    private bool IsValidRibbonCut(int[] ribbons, int mid, int k)
    {
        int totalCuts = 0;

        foreach(int i in ribbons)
        {
            totalCuts += i/mid;
        }

        return totalCuts >= k;
    }

    //Accepted-LcMedium-LcSol-T:O(n!)-S:O(n!) https://leetcode.com/problems/permutations-ii/
    public void PermuteUnique()
    {
        var res = new List<IList<int>>();
        var list = new List<int>();
        HashSet<int> visited = new HashSet<int>();

        int[] nums = new int[]{1,2,3};
        PermuteUnique(nums, res, 0);
    }

    private List<IList<int>> PermuteUnique(int[] nums, List<IList<int>> res, int idx)
    {
        if (idx == nums.Length)
        {
            var list = new List<int>();
            for(int i = 0; i < nums.Length; i++)
            {
                Console.WriteLine(nums[i]);
                list.Add(nums[i]);
            }
            res.Add(list);
            return res;
        }

        HashSet<int> appeared = new HashSet<int>();

        for(int i = idx; i < nums.Length; i++)
        {
            if (appeared.Contains(nums[i]))
            {
                continue;
            }

            appeared.Add(nums[i]);

            Helpers.Swap(nums, i, idx);
            PermuteUnique(nums, res, idx+1);
            Helpers.Swap(nums, i, idx);
        }

        return res;
    }

    //Accepted-LcMedium-LcSol-T:O(nk)-S:O(nk) https://leetcode.com/problems/paint-house-ii/
    public void PaintHouseII()
    {
        int[][] costs = new int[][]
        {
            new int[] {1,5,3},
            new int[] {2,9,4}
        };

        Console.WriteLine(PaintHouseIIDP(costs));
    }

    private int PaintHouseIIDP(int[][] costs)
    {
        if (costs.Length == 0)
        {
            return 0;
        }

        int min1 = 0, min2 = 0;
        int index1 = -1;

        for(int r = 0; r < costs.Length; r++)
        {
            int m1 = int.MaxValue;
            int m2 = int.MaxValue;
            int idx1 = -1;

            for(int c = 0; c < costs[0].Length; c++)
            {
                int cost = costs[r][c] + (index1 != c ? min1 : min2);

                if (m1 > cost)
                {
                    m2 = m1;
                    m1 = cost;
                    idx1 = c;
                }
                else if (m2 > cost)
                {
                    m2 = cost;
                }
            }

            min1 = m1;
            min2 = m2;
            index1 = idx1;
        }

        return min1;
    }

    private int PaintHouseII(int[][] costs, int k, int cost, HashSet<int> visited, ref int minCost) 
    {
        if (k == -1)
        {
            minCost = Math.Min(cost, minCost);
            return minCost;
        }
        
        for(int i = 0; i < costs[0].Length; i ++)
        {
            if (visited.Contains(i))
            {
                continue;
            }
            
            visited.Add(i);
            PaintHouseII(costs, k-1, cost + costs[k][i], visited, ref minCost);
            visited.Remove(i);
        }

        return minCost;
    }

    //https://leetcode.com/problems/exclusive-time-of-functions/
    public void ExclusiveTime()
    {
        List<string> logs = new List<string>()
        {
            "0:start:0","0:start:1","0:start:2","0:end:3","0:end:4","0:end:5"
            //"0:start:0","0:start:2","0:end:5","1:start:7","1:end:7","0:end:8"
        };

        var res = ExclusiveTime(2, logs);
    }

    public int[] ExclusiveTime(int n, IList<string> logs) 
    {
        int[] res = new int[n];
        
        Stack<Tuple<int, int>> stk = new  Stack<Tuple<int, int>>();
        var first = logs[0].Split(":");

        stk.Push(new Tuple<int, int>(int.Parse(first[0]), int.Parse(first[2])));
        int idx = 1;
        int lastEndTime = 0;

        while (idx < logs.Count)
        {
            string[] cur = logs[idx].Split(":");

            int id = int.Parse(cur[0]);
            string operation = cur[1];
            int time = int.Parse(cur[2]);
            time -= lastEndTime;

            if (operation == "start")
            {
                if (stk.Count > 0)
                {
                    var peekItem = stk.Peek();
                    res[peekItem.Item1] += time - peekItem.Item2;
                }

                stk.Push(new Tuple<int, int>(id, time));
            }
            else
            {
                var peekItem = stk.Pop();
                res[peekItem.Item1] += time - peekItem.Item2 +1;
                lastEndTime = int.Parse(cur[2])+1;
            }

            idx++;
        }

        return res;
    }

    //https://leetcode.com/problems/maximum-sum-of-two-non-overlapping-subarrays/
    public void MaxSumTwoNoOverlap()
    {
        int[] arr = new int[]{2,1,5,6,0,9,5,0,3,8};
        Console.WriteLine(MaxSumTwoNoOverlap(arr, 4, 3));
    }

     private int MaxSumTwoNoOverlap(int[] A, int L, int M)
     {
        for (int i = 1; i < A.Length; ++i)
        {
            A[i] += A[i - 1];
        }

        int res = A[L + M - 1], Lmax = A[L - 1], Mmax = A[M - 1];

        for (int i = L + M; i < A.Length; ++i)
        {
            Lmax = Math.Max(Lmax, A[i - M] - A[i - L - M]);
            Mmax = Math.Max(Mmax, A[i - L] - A[i - L - M]);
            res = Math.Max(res, Math.Max(Lmax + A[i] - A[i - M], Mmax + A[i] - A[i - L]));
        }

        return res;
     }

    private int SlidingWindowMaxSum(int[] A, int size, int otherSize, ref int left)
    {
        int sum = 0;
        int start = 0;
        int cur = 0;
        int max = int.MinValue;

        for(int idx = 0; idx < A.Length; idx++)
        {
            if (cur < left && cur+size > left)
            {
                idx = left + otherSize;
            }
            else if (idx == left )
            {
                idx += size;
                sum = 0;
            }

            if (sum == 0)
            {
                cur = idx;
                while(idx < A.Length && idx-cur < size)
                {
                    sum += A[idx++];
                }
                idx--;
            }
            else
            {
                sum += A[idx];
                sum-= A[cur++];
            }

            if (max < sum)
            {
                max = sum;
                start = cur;
            }
        }

        left = start;
        return max;
    }

    //https://leetcode.com/problems/paint-house/
    public void PaintHouse()
    {
        int[][] costs = new int[][]
        {
            new int[] {5,8,6},
            new int[] {19,14,13},
            new int[] {7,5,12},
            new int[] {14,15,17},
            new int[] {3,20,10}
            // new int[] {17,2,17},
            // new int[] {16,16,5},
            // new int[] {14,3,19}
            //new int[] {7,6,2}
        };

        Console.WriteLine(MinCost(costs));
    }

    private int MinCost(int[][] costs)
    {
        if (costs.Length == 0)
        {
            return 0;
        }

        int min1 = 0, min2 = 0;
        int index1 = -1;

        for(int r = 0; r < costs.Length; r++)
        {
            int m1 = int.MaxValue;
            int m2 = int.MaxValue;
            int idx1 = -1;

            for(int c = 0; c < costs[0].Length; c++)
            {
                int cost = costs[r][c] + (index1 != c ? min1 : min2);

                if (m1 > cost)
                {
                    m2 = m1;
                    m1 = cost;
                    idx1 = c;
                }
                else if (m2 > cost)
                {
                    m2 = cost;
                }
            }

            min1 = m1;
            min2 = m2;
            index1 = idx1;
        }

        return min1;
    }

    //Accepted:LcMedium-SelfSol-O(nLogn)-S:O(n) https://leetcode.com/problems/remove-covered-intervals/
    public void RemoveCoveredIntervals()
    {
        int[][] arr = new int[][] 
        {
            new int[] {1, 2},
            new int[] {1, 4},
            new int[] {3, 4}
        };

        Console.WriteLine(RemoveCoveredIntervals(arr));
    }

    private int RemoveCoveredIntervals(int[][] intervals)
    {
        Array.Sort(intervals, (x, y)=> {return x[0] == y[0] ? y[1] - x[1] : x[0] - y[0];});

        int count = 1;

        int ps = intervals[0][0];
        int pe = intervals[0][1];

        for(int idx = 1; idx < intervals.Length; idx++)
        {
            if (intervals[idx][0] >= ps && intervals[idx][1] <= pe)
            {
                continue;
            }

            ps = intervals[idx][0];
            pe = intervals[idx][1];

            count++;
        }
        
        return count;
    }

    //Accepted-LCMedium-Self-https://leetcode.com/problems/angle-between-hands-of-a-clock/
    public void AngleClock()
    {
        var angle = AngleClock(12, 30);
        Console.WriteLine(angle);
    }

    private double AngleClock(int hour, double minutes)
    {
        double hAngle = ((hour%12)*30) + (minutes/60 * 5) * 6;
        double mAngle = minutes * 6;
        double res = Math.Abs(mAngle - hAngle);

        return res > 180 ? 360 - res : res;
    }

    //https://leetcode.com/problems/longest-increasing-path-in-a-matrix/
    public void LongestIncreasingPath()
    {
        int[][] arr = new int[][]
        {
            new int[] {2147483647,1},
            // new int[] {3,2,6},
            // new int[] {2,2,1}
        };

        int max = int.MinValue;
        bool[,] visited = new bool[arr.Length, arr[0].Length];

        for(int row=0; row < arr.Length; row++)
        {
            for(int col = 0; col < arr[0].Length; col++)
            {
                LongestIncreasingPath(arr, row, col, visited, new List<int>(), ref max);
            }
        }

        Console.WriteLine(max);
    }

    private void LongestIncreasingPath(int[][] matrix, int row, int col, bool[,] visited, List<int> path, ref int maxLength)
    {
        if (row < 0 || col < 0 || row >= matrix.Length || col >= matrix[0].Length || visited[row,col] || (path.Count > 0 && matrix[row][col] <= path[path.Count-1]))
        {
            return;
        }

        visited[row,col] = true;

        path.Add(matrix[row][col]);
        maxLength = maxLength < path.Count ? path.Count : maxLength;

        LongestIncreasingPath(matrix, row, col+1, visited, path, ref maxLength);
        LongestIncreasingPath(matrix, row, col-1, visited, path, ref maxLength);
        LongestIncreasingPath(matrix, row+1, col, visited, path, ref maxLength);
        LongestIncreasingPath(matrix, row-1, col, visited, path, ref maxLength);

        path.Remove(matrix[row][col]);

        visited[row, col] = false;
    }

    //Accepted-LCHard-LCSol-T:O(MN^2)-S:O(N) https://leetcode.com/problems/delete-columns-to-make-sorted-iii/
    public void MinDeletionSize()
    {
        string[] strs = new string[] {"babca","bbazb"};
        //string[] strs = new string[] {"ghi","def","abc"};
        Console.WriteLine(MinDeletionSizeCre(strs));
    }

    private int MinDeletionSizeCre(string[] strs)
    {
        int m = strs.Length, n = strs[0].Length, res = n - 1, k;

        //dp holds the max subsequence at ith position
        int[] dp = new int[n];

        for(int idx= 0; idx < n; idx++)
        {
            dp[idx] = 1;
        }

        for (int j = 0; j < n; ++j)
        {
            for (int i = 0; i < j; ++i)
            {
                for (k = 0; k < m; ++k)
                {
                    if (strs[k].ElementAt(i) > strs[k].ElementAt(j)) break;
                }

                if (k == m && dp[i] + 1 > dp[j])
                {
                    dp[j] = dp[i] + 1;
                }
            }

            //n-dp[j] : No of chars to be deleted
            res = Math.Min(res, n - dp[j]);
        }

        return res;
    }

    //Accepted-LCMedium-Self-T:O(n)-https://leetcode.com/problems/top-k-frequent-elements/
    public void TopKFrequent()
    {
        //int[] nums = new int[] {1,1,1,2,2,3};
        int[] nums = new int[]{-1, -1};
        int k = 1;
        var res = TopKFrequent(nums, k);
    }

    private int[] TopKFrequent(int[] nums, int k)
    {
        IDictionary<int, int> map = new Dictionary<int, int>();
        List<int> res = new List<int>();

        foreach(int num in nums)
        {
            if (! map.ContainsKey(num))
            {
                map.Add(num, 0);
            }

            map[num]++;
        }

        Dictionary<int, List<int>> bucket = new Dictionary<int, List<int>>();

        foreach(int val in map.Keys)
        {
            int frequency = map[val];

            if (!bucket.ContainsKey(frequency))
            {
                bucket.Add(frequency, new List<int>());
            }

            bucket[frequency].Add(val);
        }

        for(int idx = nums.Length; idx > 0 && res.Count < k; idx --)
        {
            if (bucket.ContainsKey(idx))
            {
                res.AddRange(bucket[idx]);
            }
        }

        return res.ToArray();
    }

    //https://leetcode.com/problems/kth-largest-element-in-an-array/
    public void KthLargest()
    {
        int[] nums = new int[]{3,2,3,1,2,4,5,5,6};
        int k = 5;
        Console.WriteLine(KthLargest(nums, 0, nums.Length-1, k));
    }

    private int KthLargestFirstPivot(int[] nums, int k, int start, int end)
    {
        int pivot = start;
        int idx = start;

        while (idx < end)
        {
            if (nums[idx] <= nums[end])
            {
                Helpers.Swap(nums, idx, pivot);
                pivot++;
            }

            Helpers.Swap(nums, idx, pivot);
        }

        int m = nums.Length - pivot;

        if (m == k)
        {
            return nums[pivot];
        }

        if (m < k)
        {
            return KthLargestFirstPivot(nums, k-m, start, pivot-1);
        }
        else
        {
            return KthLargestFirstPivot(nums, k, pivot+1, end);
        }
    }

    private int KthLargestWithLastPivot(int[] nums, int start, int end, int k)
    {
        int idx = start;
        int pivot = end;

        while (idx < pivot)
        {
            if (nums[idx++] > nums[pivot])
            {
                Helpers.Swap(nums, idx, pivot);
            }

            pivot--;
        }

        if (pivot - start + 1 == k)
        {
            return nums[pivot];
        }

        if (pivot > k)
        {
            return KthLargestWithLastPivot(nums, start, pivot-1, k);
        }
        else
        {
            return KthLargestWithLastPivot(nums, pivot + 1, end, k);
        }
    }

    private int KthLargest(int[] nums, int start, int end, int k)
    {
        int idx = start;
        int pivot = start;

        while (idx < end)
        {
            if(nums[idx] <= nums[end])
            {
                Helpers.Swap(nums, pivot++, idx);
            }

            idx++;
        }

        Helpers.Swap(nums, pivot, end);

        int m = end - pivot +1;
        if (m == k)
        {
            return nums[pivot];
        }

        if (m > k)
        {
            return KthLargest(nums, pivot+1, end, k);
        }
        else
        {
            return KthLargest(nums, start, pivot-1, k-m);
        }
    }


    //https://leetcode.com/problems/friends-of-appropriate-ages/
    public void FriendsOfAppropriateAge()
    {
        int[] nums = new int[] {20,30,100,110,120};
        Console.WriteLine(FriendsOfAppropriateAge(nums));
    }

    public int FriendsOfAppropriateAge(int[] ages)
    {
        Dictionary<int, int> count = new Dictionary<int, int>();

        foreach (int age in ages)
        {
            if (!count.ContainsKey(age))
            {
                count.Add(age, 0);
            }
            count[age] += 1;
        }

        int res = 0;
        foreach (int a in count.Keys)
        {
            foreach (int b in count.Keys)
            {
                if (ValidFriendRequest(a, b))
                {
                     res += count[a] * (count[b] - (a == b ? 1 : 0));
                }
            }
        }

        return res;
    }

    private bool ValidFriendRequest(int a, int b)
    {
        return !(b <= 0.5 * a + 7 || b > a || (b > 100 && a < 100));
    }

    public void WallsGates()
    {
        int[,] arr = new int[4,4]
        {
            {1, -1, 0, 1},
            {1, 1, 1, -1},
            {1, -1, 1, -1},
            {0, -1, 1, 1}
        };
        
        int[,] res = WallsGates(arr);
        
        for(int row = 0; row < arr.GetLength(0); row ++)
        {
            Console.WriteLine("----------\n");
            for(int col = 0; col < arr.GetLength(1); col ++)
            {
                Console.WriteLine($"{res[row, col]} \t");
            }
        }
    }

    private int[,] WallsGates(int[,] arr)
    {
        if (arr == null)
        {
            throw new ArgumentNullException("...");
        }
        
        List<Pair> pairs = new List<Pair>();
        int[,] output = new int[arr.GetLength(0), arr.GetLength(1)];
        
        for(int row = 0; row < arr.GetLength(0); row ++)
        {
            for(int col = 0; col < arr.GetLength(1); col ++)
            {
                output[row, col] = -2;
            }
        }

        for(int row = 0; row < arr.GetLength(0); row ++)
        {
            for(int col = 0; col < arr.GetLength(1); col ++)
            {
                if (output[row, col] == -2)
                {
                    DFS(arr, output, row, col);
                }
            }
         }
        
        return output;
        
    }
    
    private int DFS(int[,] arr, int[,] output, int row, int col)
    {
        if (row >= arr.GetLength(0) || col >= arr.GetLength(1) || arr[row, col] == -1)
        {
            return 0;
        }
        
        if (arr[row, col] == 0)
        {
            return 0;
        }
        
        var right = DFS(arr, output, row, col + 1); 
        
        var bottom = DFS(arr, output, row+1, col);
        
        if (right >= 0)
        {
            right +=1; 
        }
        
        if (bottom >= 0)
        {
            bottom +=1;
        }
        
        if (arr[row, col] == 1)
        {
            if (right > 0 && bottom >0)
            {
                output[row, col] = Math.Min(right, bottom);
            }
            else
            {
                output[row, col] = right == 0 ? bottom : right;
            }
        }
        
        if (right == 0)
        {
            return bottom;
        }
        else if (bottom == 0)
        {
            return right;
        }
        else
        {
            return Math.Min(right, bottom);
        }
    }

    //Accepted:LcMedium-SelfSol-T: O(n) S:O(1): https://leetcode.com/problems/max-consecutive-ones-iii/
    public void MaxConsecutiveOnes()
    {
        int[] arr = new int[]{0,0,1,1,0,0,1,1,1,0,1,1,0,0,0,1,1,1,1};
        int K = 3;
        Console.WriteLine(MaxConsecutiveOnesCre(arr, K));
    }

    private int MaxConsecutiveOnesCre(int[] nums, int k)
    {
        int left = 0;
        int right = 0;
        int max = nums[0] == 1 ? 1 : 0;
        int count = k;
        
        while (right < nums.Length)
        {
            if (nums[right] == 0)
            {
                if (count == 0)
                {
                    while (left < right && nums[left] != 0)
                    {
                        left++;
                    }

                    left++;
                }
                else
                {
                    count--;
                }
            }
            
            max = Math.Max(max, right-left+1);
            right++;
        }
        
        return max;
    }

    private int MaxConsecutiveOnes(int[] A, int K)
    {
        int max = 0;
        int idx = 0;
        int zCount = 0;
        int start = 0;

        while (idx < A.Length)
        {
            if (A[idx] == 0)
            {
                zCount++;
            }

            while (zCount > K)
            {
                if (A[start] == 0)
                {
                    zCount--;
                }

                start++;
            }

            max = Math.Max(max, idx - start + 1);
            idx++;
        }

        return max;
    }

    //Accepted-LcMedium-SelfSol-T:O(n^2)-S:O(n^2) https://leetcode.com/problems/game-of-life/discuss/73366/Clean-O(1)-space-O(mn)-time-Java-Solution
    public void GameOfLife()
    {
        int[][] board = new int[][]
        {
            new int[]{0,1,0},
            new int[]{0,0,1},
            new int[]{1,1,1},
            new int[]{0,0,0}
        };

        GameOfLife(board);
    }

    private void GameOfLife(int[][] board)
    {
        for(int r = 0; r < board.Length; r++)
        {
            for(int c = 0; c < board[r].Length; c++)
            {
                var neighborCount = CountNeighbors(board, r, c);
                if (board[r][c] == 1)
                {
                    if (neighborCount < 2 || neighborCount > 3)
                    {
                        board[r][c] = -1;
                    }
                }
                else if (neighborCount == 3)
                {
                    board[r][c] = 2;
                }
            }
        }

        for(int r = 0; r < board.Length; r++)
        {
            for(int c = 0; c < board[r].Length; c++)
            {
                if (board[r][c] == -1)
                {
                    board[r][c] = 0;
                }
                if (board[r][c] == 2)
                {
                    board[r][c] = 1;
                }
            }
        }
    }

    private int CountNeighbors(int[][] board, int r, int c)
    {
        int[] x = new int[] {-1, 1, 0, 0, 1, -1, 1, -1};
        int[] y = new int[] {0, 0, 1, -1, 1, 1, -1, -1};

        int count = 0;

        for(int idx = 0; idx < x.Length; idx++)
        {
            var row = r + x[idx];
            var col = c + y[idx];

            if (row < 0 || col < 0 || row >= board.Length || col >= board[0].Length)
            {
                continue;
            }

            if (board[row][col] == 1 || board[row][col] == -1)
            {
                count++;
            }
        }

        return count;
    }

    /*
    Given an array nums, write a function to move all 0's to the end of it while maintaining the relative order of the non-zero elements.
    Input: [0,1,0,3,12]
    Output: [1,3,12,0,0]
    */
    public void MoveZerosAtEnd()
    {
        int[] arr = new int[]{0,1,0,3,12};
        MoveZerosAtEnd(arr);
    }

    private void MoveZerosAtEnd(int[] arr)
    {
        int idx = 0;
        int rIdx = -1;

        while(idx < arr.Length)
        {
            if (arr[idx] == 0 && rIdx == -1)
            {
                rIdx = idx;
            }
            else if (rIdx > -1 && arr[idx] != 0)
            {
                arr[rIdx++] = arr[idx];
                arr[idx] = 0;
            }
            idx ++;
        }
    }

    //https://leetcode.com/problems/course-schedule-ii/
    public void FindOrderOfCourses()
    {
        int[][] prerequisites = new int[][]
        {
            new int[]{1,0},
            new int[]{1,2},
            new int[]{0,1},
        };

        int numCourses = 3;
        
        var res = FindOrderOfCoursesUsingBFS(numCourses, prerequisites);
    }

    private int[] FindOrderOfCoursesUsingBFS(int numCourses, int[][] prerequisites)
    {
        if (numCourses == 0)
        {
            return null;
        }

        int[] indegree = new int[numCourses];
        List<int> order = new List<int>();

        for(int idx = 0; idx < prerequisites.Length; idx++)
        {
            indegree[prerequisites[idx][0]]++;
        }

        Queue<int> queue = new Queue<int>();

        for(int idx =0; idx < numCourses; idx++)
        {
            if(indegree[idx] == 0)
            {
                queue.Enqueue(idx);
                order.Add(idx);
            }
        }

        while(queue.Count > 0)
        {
            var item = queue.Dequeue();

            for(int idx = 0; idx < prerequisites.Length; idx++)
            {
                if (prerequisites[idx][1] == item)
                {
                    indegree[prerequisites[idx][0]]--;

                    if (indegree[prerequisites[idx][0]] == 0)
                    {
                        order.Add(prerequisites[idx][0]);
                        queue.Enqueue(prerequisites[idx][0]);
                    }
                }
            }
        }

        return order.Count != numCourses ? new int[0]: order.ToArray();
    }

    private List<int> FindOrderOfCourses(Dictionary<int, HashSet<int>> map, int idx, List<int> res, HashSet<int> visited)
    {
        visited.Add(idx);

        if (map.ContainsKey(idx))
        {
            foreach(int dependency in map[idx])
            {
                if (visited.Contains(dependency))
                {
                    continue;
                }

                FindOrderOfCourses(map, dependency, res, visited);

                if (!res.Contains(dependency))
                {
                    res.Add(dependency);
                }
            }
        }

        if (!res.Contains(idx))
        {
            res.Add(idx);
        }

        return res;
    }

    //https://leetcode.com/problems/parallel-courses/
    public void MinimumSemesters()
    {
        int[][] arr = new int[][]
        {
            new int[] {1,4},
            new int[] {2,3},
            new int[] {3,4},
            new int[] {5,2},
            new int[] {6,1}
        };

        Console.WriteLine(MinimumSemesters(6, arr));
    }

    public int MinimumSemesters(int N, int[][] relations)
    {
        Dictionary<int, List<int>> g = new Dictionary<int, List<int>>(); // key: prerequisite, value: course list. 
        int[] inDegree = new int[N + 1]; // inDegree[i]: number of prerequisites for i.
        foreach(int[] r in relations)
        {
            if (!g.ContainsKey(r[0]))
            {
                var list = new List<int>();
                g.Add(r[0], list);
            }

            g[r[0]].Add(r[1]);
            ++inDegree[r[1]]; // count prerequisites for r[1].
        }

        Queue<int> q = new Queue<int>(); // save current 0 in-degree vertices.
        for (int i = 1; i <= N; ++i)
        {
            if (inDegree[i] == 0)
            {
                q.Enqueue(i);
            }
        }

        int semester = 0;
        while (q.Count > 0)
        {
            // BFS traverse all currently 0 in degree vertices.
            for (int sz = q.Count; sz > 0; --sz)
            {
                // sz is the search breadth.
                int c = q.Dequeue();
                --N;
                if (!g.ContainsKey(c))
                {
                    continue; // c's in-degree is currently 0, but it has no prerequisite.
                }

                foreach (int course in g[c])
                {
                    if (--inDegree[course] == 0) // decrease the in-degree of course's neighbors.
                    {
                        q.Enqueue(course); // add current 0 in-degree vertex into Queue.
                    }
                }

                g.Remove(c);
            }

            ++semester; // need one more semester.
        }

        return N == 0 ? semester : -1;
    }

    public void Sudoku()
    {
        
    }

    private bool SolveSudoku(int[,] arr, int row, int col, int val)
    {
        if (row == 9 &  col == 9)
        {
            return BoardFilled(arr);
        }

        for(; row < 9; row ++)
        {
            for(; col < 9; col++)
            {
                for(;val < 10; val ++)
                {
                    if (arr[row, col] > 0)
                    {
                        continue;
                    }

                    int temp = arr[row, col];

                    if (!ValidBoard(arr, row, col, val))
                    {
                        continue;
                    }

                    arr[row, col] = val;

                    if (!SolveSudoku(arr, row, col, val))
                    {
                        arr[row, col] = temp;
                    }
                }
            }
        }

        return true;
    }

    private bool ValidBoard(int[,] arr, int row, int col, int val)
    {
        for(int c = 0; c < 9; c ++)
        {
            if (arr[row, c] == val)
            {
                return false;
            }
        }

        for(int r = 0; r < 9; r ++)
        {
            if (arr[r, col] == val)
            {
                return false;
            }
        }

        int squareRow = row /3;
        int squareCol = col / 3;

        for(int sRow = squareRow * 3; sRow < squareRow * 3 + 2; sRow ++)
        {
            for(int sCol = squareCol * 3; sCol < squareCol * 3 + 2; sCol ++)
            {
                if (arr[sRow, sCol] == val)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool BoardFilled(int[,] arr)
    {
        for(int row = 0; row < 9; row ++)
        {
            for(int col = 0; col < 9; col ++)
            {
                if (arr[row, col] == 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    /*
    Implement Rand17() which returns a random number between 0 & 16. You can use Rand13() which returns a random number between 0 & 12.
    */
    public void RandomNumber()
    {
        HashSet<int> map = new HashSet<int>();
        bool allPresent = false;
        int sum = 0;

        for(int i = 0; i < 17; i ++)
        {
            Console.WriteLine(Rand17(map, ref sum, ref allPresent));
        }
    }

    private int Rand17()
    {
        int rand = Rand13();

        while(rand > 16)
        {
            rand = (Rand13() *2)%17;
        }

        return rand;
    }

    private int Rand17(HashSet<int> map, ref int sum, ref bool allPresent)
    {
        int rand = -1;
        int newRand = -1;
        
        while(true)
        {
            rand = Rand13();
            //Console.WriteLine($"rand is {rand}");
            if (!map.Contains(rand))
            {
                map.Add(rand);
                return rand;
            }

            if (allPresent)
            {
                continue;
            }

            newRand = rand % 4;
            
            while (map.Contains(12 + newRand + 1)) 
            {
                //Console.WriteLine($"The new rand {12 + newRand} exists already");
                newRand = Rand13() %4;
            }

            if (!map.Contains(12 + newRand+1))
            {
                break;
            }
        }
        
        rand = 12 + newRand+1;
        sum+= rand;
        allPresent = sum == 58;

        map.Add(rand);
        return rand;
    }

    private int Rand13()
    {
        Random random = new Random();
        return random.Next(13);
    }

    //Accepted-LcMedium-SelfSol-T:O(m+n)-S:O(1) https://leetcode.com/problems/meeting-scheduler
    public void MinAvailableDuration()
    {
        int[][] slots1 = new int[][] 
        {
            new int[]{216397070,363167701},
            new int[]{98730764,158208909},
            new int[]{441003187,466254040},
            new int[]{558239978,678368334},
            new int[]{683942980,717766451}
        };

        int[][] slots2 = new int[][] 
        {
            new int[]{50490609,222653186},
            new int[]{512711631,670791418},
            new int[]{730229023,802410205},
            new int[]{812553104,891266775},
            new int[]{230032010,399152578}
        };

        int duration = 456085;
        var res = MinAvailableDuration(slots1, slots2, duration);
    }

    private IList<int> MinAvailableDuration(int[][] slots1, int[][] slots2, int duration)
    {
        Array.Sort(slots1, (a,b) => a[0]- b[0]);
        Array.Sort(slots2, (a,b) => a[0]- b[0]);

        int minSize = Math.Min(slots1.Length, slots2.Length);
        int s1 = 0, s2 = 0;
        int start = 0, end = 0;
        
        while (s1 < slots1.Length && s2 < slots2.Length)
        {
            if (slots1[s1][0] > slots2[s2][0] && slots1[s1][0] > slots2[s2][1])
            {
                s2++;
            }
            else if (slots2[s2][0] > slots1[s1][0] && slots2[s2][0] > slots1[s1][1])
            {
                s1++;
            }
            else
            {
                start = Math.Max(slots1[s1][0], slots2[s2][0]);
                end = Math.Min(slots1[s1][1], slots2[s2][1]);
                
                if (start + duration <= end)
                {
                    return new int[]{start, start+ duration};    
                }
                
                if ((s2 == slots2.Length -1 && s1 < slots1.Length-1) || slots1[s1][1] < slots2[s2][1] )
                {
                    s1++;
                }
                else if ((s1 == slots1.Length -1 && s2 < slots2.Length-1) || slots2[s2][1] < slots1[s1][1])
                {
                    s2++;
                }
                else
                {
                    s1++;
                    s2++;
                }
            }
        }
        
        return new int[0];
    }

    //Accepted-LcHard-SelfSol-T:O(n)-S:O(n) https://leetcode.com/problems/bus-routes/
    public void NumBusesToDestination()
    {
        int[][] routes = new int[][]
        {
            new int[] {1,2,7},
            new int[] {3,6,7}
        };

        int source = 1;
        int target = 6;

        Dictionary<int, HashSet<int>> map = new Dictionary<int, HashSet<int>>();

        for(int idx = 0; idx < routes.Length; idx++)
        {
            foreach(int route in routes[idx])
            {
                if (!map.ContainsKey(route))
                {
                    map.Add(route, new HashSet<int>());
                }

                map[route].Add(idx);
            }
        }

        Console.WriteLine(NumBusesToDestination(map, routes, source, target));
    }
    
    private int NumBusesToDestination(Dictionary<int, HashSet<int>> map, int[][] routes, int source, int target)
    {
        HashSet<int> visited = new HashSet<int>();
        Queue<int[]> queue = new Queue<int[]>();

        queue.Enqueue(new int[]{0, source});

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            if (item[1] == target)
            {
                return item[0];
            }

            foreach(int idx in map[item[1]])
            {
                if (visited.Contains(idx))
                {
                    continue;
                }

                foreach(int j in routes[idx])
                {
                    if (visited.Contains(j))
                    {
                        continue;
                    }

                    queue.Enqueue(new int[] {item[0]+1, j});
                }

                visited.Add(idx);
            }
        }

        return -1;
    }

    //Accepted-LcHard-LcSol-T:O(ELogE) S:O(N) https://leetcode.com/problems/optimize-water-distribution-in-a-village/
    public void MinCostToSupplyWater()
    {
        int[] wells = new int[] {1,2,2};
        int[][] pipes = new int[][]
        {
            new int[] {1,2,1},
            new int[] {2,3,1},
        };

        int n = 3;

        Console.WriteLine(MinCostToSupplyWater(n, wells, pipes));
    }

    private int MinCostToSupplyWater(int n, int[] wells, int[][] pipes)
    {
        int[] parent = new int[n+1];

        List<int[]> edges = new List<int[]>();
        for (int i = 0; i < n; i++) 
        {
            parent[i + 1] = i + 1;
            // Draw an edge from 0, to point to itself 
            edges.Add(new int[] {0, i + 1, wells[i]});
        }

        foreach(int[] p in pipes)
        {
            edges.Add(p);
        }

        edges.Sort((a, b) => a[2].CompareTo(b[2]));

        int res = 0;

        foreach(int[] e in edges)
        {
            int x = Find(e[0], parent), y = Find(e[1], parent);
            if (x != y)
            {
                res += e[2];
                parent[x] = y;
            }
        }

        return res;
    }

    //Accepted-LcHard-LCSol-T:O(mnn) S:O(m) https://leetcode.com/problems/number-of-submatrices-that-sum-to-target/
    public void NumSubmatrixSumTarget()
    {
        int[][] matrix = new int[][]
        {
            new int[]{1,-1},
            new int[]{-1, 1},
        };

        int target = 0;
        Console.WriteLine(NumSubmatrixSumTarget(matrix, target));
    }

    private int NumSubmatrixSumTarget(int[][] A, int target)
    {
        int res = 0, m = A.Length, n = A[0].Length;

        for (int i = 0; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                A[i][j] += A[i][j - 1];
            }
        }

        Dictionary<int, int> counter = new Dictionary<int, int>();
        
        //Iterate columns
        for (int i = 0; i < n; i++)
        {
            //Iterate columns until i
            for (int j = i; j < n; j++)
            {
                int cur = 0;

                counter.Clear();
                counter[0] = 1;

                //Iterate rows
                for (int k = 0; k < m; k++)
                {
                    cur += A[k][j] - (i > 0 ? A[k][i - 1] : 0);

                    if (counter.ContainsKey(cur-target))
                    {
                        res += counter[cur-target];
                    }

                    if (!counter.ContainsKey(cur))
                    {
                        counter.Add(cur, 0);
                    }
                    
                    counter[cur] += 1;
                }
            }
        }

        return res;
    }

    //Accepted:LcMEdium-LCSol-T:O(logn)-S:O(1) https://leetcode.com/problems/find-first-and-last-position-of-element-in-sorted-array/
    public void SearchRange()
    {
        int[] nums = new int[] {5,7,7,8,8,10};
        int target = 8;
        var output = SearchRange(nums, target);
    }

    private int[] SearchRange(int[] nums, int target)
    {
        int start = 0, end = nums.Length - 1;
        int[] ret = new int[] {-1, -1};

        if(nums == null || nums.Length == 0)
            return new int[]{-1, -1 };

        // Search for the left one
        while (start < end)
        {
            int mid = (end-start) /2 + start;
            if (nums[mid] < target)
            {
                start = mid + 1;
            }
            else
            {
                end = mid;
            }
        }

        if (nums[start]!=target)
        {
            return ret;
        }
        else
        {
            ret[0] = start;
        }

        // Search for the right one
        end = nums.Length-1;  // We don't have to set start to 0 the second time.
        while (start < end)
        {
            int mid = (end-start) /2 + start + 1; // Make mid biased to the right
            if (nums[mid] > target)
            {
                end = mid - 1;
            }
            else
            {
                start = mid;        // So that this won't make the search range stuck.
            }
        }

        ret[1] = end;
        return ret;
    }

    //https://leetcode.com/problems/best-meeting-point/
    public void BestMeetingPoint()
    {
        // int[][] arr = new int[][]
        // {
        //     new int[]{1,0,0,0,1},
        //     new int[]{0,0,0,0,0},
        //     new int[]{0,0,1,0,0}
        // };

        int[][] arr = new int[][]
        {
            new int[]{1,1}
        };

        Console.WriteLine(BestMeetingPoint(arr));
    }

    private int BestMeetingPoint(int[][] grid)
    {
        List<int> rows = new List<int>();
        List<int> cols = new List<int>();

        for(int row = 0; row < grid.Length; row++)
        {
            for(int col = 0; col < grid[0].Length; col++)
            {
                if (grid[row][col] == 1)
                {
                    rows.Add(row);
                    cols.Add(col);
                }
            }
        }

        return MinMoveToBestMeetingPoint(rows) + MinMoveToBestMeetingPoint(cols);
    }

    private int MinMoveToBestMeetingPoint(List<int> list)
    {
        list.Sort();
        int left = 0, right = list.Count-1;
        int dist = 0;

        while(left < right)
        {
            dist += list[right--] - list[left++];
        }

        return dist;
    }

    //Accepted-LcMedium-SelfSol-T:O()-S:O() https://leetcode.com/problems/combinations/
    public void Combinations() 
    {
       int n = 4;
       int k = 2;

       var res = Combinations(n, k, 1, new List<int>(), new List<IList<int>>());
    }
    
    private IList<IList<int>> Combinations(int n, int k, int idx, List<int> cur ,IList<IList<int>> res)
    {
        if(cur.Count == k)
        {
            res.Add(new List<int>(cur));
            return res;
        }
        
        for(int i = idx; i <= n; i++)
        {
            cur.Add(i);
            Combinations(n, k, i+1, cur, res);
            cur.RemoveAt(cur.Count()-1);
        }
        
        return res;
    }

    //https://leetcode.com/problems/kth-smallest-element-in-a-sorted-matrix/
    public void KthSmallestInSortedMatrix()
    {

    }

    private int KthSmallestInSortedMatrix(int[][] matrix, int k)
    {
        return 0;
    }

    //https://leetcode.com/problems/minimum-size-subarray-sum/
    public void MinSubArraySum()
    {
        int[] nums = new int[] {2,3,1,2,4,3};
        int target = 7;
        Console.WriteLine(MinSubArraySumTwoPointers(target, nums));
    }

    private int MinSubArraySumTwoPointers(int target, int[] nums)
    {
        int left = 0;
        int right = 0;
        int min = int.MaxValue;
        int sum = 0;

        while (right < nums.Length)
        {
            sum += nums[right];

            while (sum >= target)
            {
                min = Math.Min(min, right- left + 1);
                sum-= nums[left++];
            }

            right++;
        }

        return min == int.MaxValue ? 0 : min;
    }

    public int MinSubArraySumPrefixSum(int target, int[] nums)
    {
        int[] res = new int[nums.Length];
        res[0] = nums[0];
        
        for(int idx = 1; idx < nums.Length; idx++)
        {
             res[idx] += res[idx-1] + nums[idx];
        }

        int min = int.MaxValue;
        for (int i = 0; i < nums.Length; i++)
        {
            for(int j = i; j < nums.Length; j++)
            {
                if (res[j]-res[i] + nums[i] >= target)
                {
                    min = Math.Min(min, j-i+1);
                } 
            } 
        }

        return min == int.MaxValue ? 0 : min;
    }

    //https://leetcode.com/problems/kth-smallest-element-in-a-sorted-matrix/
    public void KthSmallest()
    {

    }

    private int KthSmallest(int[][] matrix, int k)
    {
        return 0;
    }

    //Accepted-LCMedium-SelfSol-T:O(logn)-S:O(1)https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/
    public void MinRotatedSortedArray()
    {
        int[] arr = new int[] {3,4,5,1,2};
        Console.WriteLine(MinRotatedSortedArray(arr));
    }

    private int MinRotatedSortedArray(int[] arr)
    {
        int min = arr[0];
        int start = 0;
        int end = arr.Length-1;

        while (start < end-1)
        {
            int mid = (end-start)/2 +start;
            
            if (arr[start] < arr[mid])
            {
                start = mid;
            }
            else
            {
                end = mid;
            }
        }
        
        return Math.Min(min, arr[start] < arr[end] ? arr[start] : arr[end]);
    }

    //https://leetcode.com/problems/water-and-jug-problem/
    public void CanMeasureWater()
    {
        int jug1Capacity = 3, jug2Capacity = 5, targetCapacity = 4;
        Console.WriteLine(CanMeasureWater(jug2Capacity, jug1Capacity, targetCapacity));
    }

    private bool CanMeasureWater(int jug1Capacity, int jug2Capacity, int targetCapacity)
    {
        if (jug1Capacity + jug2Capacity < targetCapacity)
        {
            return false;
        }

        if (jug1Capacity == targetCapacity || jug2Capacity == targetCapacity || jug1Capacity + jug2Capacity == targetCapacity )
        {
            return true;
        }

        if (jug1Capacity > jug2Capacity)
        {
            return CanMeasureWater(jug1Capacity, jug2Capacity, targetCapacity);
        }

        while(jug1Capacity != 0)
        {
            int temp = jug1Capacity;
            jug1Capacity = jug2Capacity % jug1Capacity;
            jug2Capacity = temp;
        }

        return (targetCapacity) % jug2Capacity == 0;
    }

    //Accepted-LcMedium-LCSol-T:O(n)-S:O(n) https://leetcode.com/problems/task-scheduler/
    #region
    /*Given a char array representing tasks CPU need to do. It contains capital letters A to Z where different letters represent different tasks.
     Tasks could be done without original order. Each task could be done in one interval. For each interval, CPU could finish one task or just be idle.
     However, there is a non-negative cooling interval n that means between two same tasks, there must be at least n intervals that CPU are doing different
     tasks or just be idle.

    You need to return the least number of intervals the CPU will take to finish all the given tasks.

    Input: tasks = ["A","A","A","B","B","B"], n = 2
    Output: 8
    Explanation: A -> B -> idle -> A -> B -> idle -> A -> B.*/
    #endregion
    public void TaskScheduler()
    {
        char[] ch = new char[]{'A','A','A','B','B','B'};
        int n = 2;
        Console.WriteLine(TaskScheduler(ch, n));
    }

    private int TaskScheduler(char[] tasks, int n)
    {
        int[] c = new int[26];
        foreach(char t in tasks)
        {
            c[t - 'A']++;
        }

        //Sort the array by Ascending
        Array.Sort(c);

        int max_val = c[25] - 1, idle_slots = max_val * n;
        for (int i = 24; i >= 0 && c[i] > 0; i--)
        {
            idle_slots -= Math.Min(c[i], max_val);
        }

        return idle_slots > 0 ? idle_slots + tasks.Length : tasks.Length;
    }

    //Accepted: T:O(n^2), S:O(n): https://leetcode.com/problems/3sum/
    public void ThreeSum()
    {
        int[] arr = new int[] {-1, 0, 1, 2, -1, -4};

        var res = ThreeSum(arr);
    }

    private IList<IList<int>> ThreeSum(int[] arr)
    {
        Array.Sort(arr);

        IList<IList<int>> res = new List<IList<int>>();

        for(int i = 0; i < arr.Length-2;i++)
        {
            int lo = i+1;
            int hi = arr.Length-1;

            if (i > 0 && arr[i] == arr[i-1])
            {
                continue;
            }

            while (lo < hi)
            {
                int sum = arr[i] + arr[lo] + arr[hi];
                if (sum == 0)
                {
                    var cur = new List<int>();
                    cur.AddRange(new int[]{arr[i], arr[lo], arr[hi]});
                    res.Add(cur);

                    while (lo < hi && arr[lo] == arr[lo+1])
                    {
                        lo++;
                    }

                    while (hi < arr.Length-2 && arr[hi] == arr[hi+1])
                    {
                        hi--;
                    }

                    lo++;
                }
                else if (sum < 0)
                {
                    lo ++;
                }
                else 
                {
                    hi--;
                }
            }
        }

        return res;
    }

    //https://leetcode.com/articles/missing-element-in-sorted-array/#
    public void FindMissingInSortedArray()
    {
        Console.WriteLine(FindMissingInSortedArray(new int[] {4, 7, 9, 10}, 3));
    }

    private int FindMissingInSortedArray(int[] arr, int k)
    {
        int count = 0;
        int num = -1;

        for(int idx = 1; idx < arr.Length; idx ++)
        {
            int cur = arr[idx-1];
            num = -1;
            while (arr[idx] - cur > 1)
            {
                count ++;
                num = num == - 1 ? cur + 1 : num + 1;

                if (count == k)
                {
                    return num;
                }

                cur ++;
            }
        }

        return num;
    }

    //https://leetcode.com/contest/weekly-contest-150/problems/as-far-from-land-as-possible/
    public void MaxDistanceOfWaterFromLand()
    {
        int[,] arr = new int[3,3]
        {
            {1, 0, 0},
            {0, 0, 0},
            {0, 0, 0}
        };

        Console.WriteLine(MaxDistanceOfWaterFromLand(arr));
    }

    private int MaxDistanceOfWaterFromLand(int[,] arr)
    {
        HashSet<Pair> ones = FindLand(arr);
        int[,] res = new int[arr.GetLength(0), arr.GetLength(1)];

        foreach(Pair p in ones)
        {
            FindMaxDistanceFromLand(arr, p, res, ones);
        }

        int count = 0;
        
        for(int r = 0; r < arr.GetLength(0); r ++)
        {
            for(int c = 0; c < arr.GetLength(1); c ++)
            {
                count = Math.Max(count, arr[r,c]);
            }
        }

        return count;
    }

    private void FindMaxDistanceFromLand(int[,] arr, Pair pair, int[,] res, HashSet<Pair> ones)
    {
        List<Pair> paths = new List<Pair>()
        {
            new Pair(0,-1), new Pair(0,1), new Pair(1,0), new Pair(-1,0)
        };

        bool[,] visited = new bool[arr.GetLength(0), arr.GetLength(1)];

        Queue<Pair> queue = new Queue<Pair>();
        queue.Enqueue(new Pair(pair.x, pair.y, 0));

        while(queue.Count > 0)
        {
            var item = queue.Dequeue();

            foreach(Pair path in paths)
            {
                var row = item.x + path.x;
                var col = item.y + path.y;

                if (!IsValid(arr, row, col) || visited[row, col] || ones.Contains(new Pair(row, col)))
                {
                    continue;
                }

                visited[item.x, item.y] = true;

                if (arr[row, col] == 0 || arr[row, col] > item.Level + 1)
                {
                    arr[row, col] = item.Level + 1;
                    queue.Enqueue(new Pair(row, col, item.Level+1));
                }
            }
        }
    }

    private HashSet<Pair> FindLand(int[,] arr)
    {
        HashSet<Pair> ones = new HashSet<Pair>();
        
        for(int r = 0; r < arr.GetLength(0); r++)
        {
            for(int c = 0; c < arr.GetLength(1); c ++)
            {
                if (arr[r,c] == 1)
                {
                    ones.Add(new Pair(r, c));
                }
            }
        }

        return ones;
    }

    private bool IsValid(int[,] arr, int row, int col)
    {
        if (row < 0 || col < 0 || row >= arr.GetLength(0) || col >= arr.GetLength(1))
        {
            return false;
        }

        return true;
    }

    //https://leetcode.com/problems/number-of-corner-rectangles/
    public void NumberOfCornerRectangle()
    {
        int[][] grid = new int[][]
        {
            new int[] {1,1,1},
            new int[] {1,1,1},
            new int[] {1,1,1},
        };

        Console.WriteLine(NumberOfCornerRectangle(grid));
    }

    private int NumberOfCornerRectangle(int[][] grid)
    {
        int res = 0;

        for(int i = 0; i < grid.Length; i++)
        {
            for(int j = i+1; j < grid.Length; j++)
            {
                int count = 0;
                for(int k = 0; k <grid[0].Length; k++)
                {
                    if (grid[i][k] == 1 && grid[j][k] == 1)
                    {
                        count ++;
                    }
                }

                res += (count * (count-1)/2);
            }
        }

        return res;
    }

    //Accepted:T:O(n^2) S:O(n): https://leetcode.com/problems/valid-tic-tac-toe-state/
    public void ValidTicTacToe()
    {
        string[] s = new string[]
        {
            "XOX",
            "OXO",
            "  X"
        };

        Console.WriteLine(ValidTicTacToe(s));
    }

    private bool ValidTicTacToe(string[] board)
    {
        int[] rows = new int[3];
        int[] cols = new int[3];
        int turn = 0;
        int diag = 0;
        int antiDiag = 0;
        bool xWin = false;
        bool oWin = false;

        for(int row = 0; row < board.Length; row ++)
        {
            for(int col = 0; col < 3; col++)
            {
                if (board[row][col] == 'X')
                {
                    rows[row] ++;
                    cols[col]++;
                    turn++;

                    if (row == col)
                    {
                        diag++;
                    }

                    if (row + col == 2)
                    {
                        antiDiag++;
                    }
                }
                else if (board[row][col] == 'O')
                {
                    rows[row] --;
                    cols[col]--;
                    turn--;

                    if (row == col)
                    {
                        diag--;
                    }

                    if (row + col == 2)
                    {
                        antiDiag--;
                    }
                }
            }
        }

        xWin = rows[0] == 3 || rows[1] == 3 || rows[2] == 3 || cols[0] == 3 || cols[1] == 3 || cols[2] == 3 || diag == 3 | antiDiag == 3;
        oWin = rows[0] == -3 || rows[1] == -3 || rows[2] == -3 || cols[0] == -3 || cols[1] == -3 || cols[2] == -3|| diag == -3 | antiDiag == -3;

        if (xWin && turn == 0 ||  oWin && turn == 1)
        {
            return false;
        }

        return (turn == 0 || turn == 1) && (!xWin || !oWin);
    }

    //Accepted-LcMedium-SelfSol-T:O(nlogn)-S:O(n) https://leetcode.com/problems/remove-sub-folders-from-the-filesystem/
    public void RemoveSubfolders()
    {
        string[] folder = new string[] {"/a","/a/b","/c/d","/c/d/e","/c/f"};
        var res = RemoveSubfolders(folder);
    }

    private IList<string> RemoveSubfolders(string[] folder)
    {
        HashSet<string> map = new HashSet<string>();
        IList<string> res = new List<string>();
        
        
        Array.Sort(folder, (x,y)=> {return x.Length.CompareTo(y.Length);});
        bool found = false;
        
        foreach(string f in folder)
        {
            found = false;
            for(int idx = 0; idx < f.Length; idx++)
            {
                if (idx > 0 && f[idx] == '/')
                {
                    if (res.Contains(f.Substring(0, idx)))
                    {
                        found = true;
                        break;
                    }
                }
            }
            
            if (!found)
            {
                res.Add(f);
            }
        }
        
        return res;
    }

    //Accepted: T:O(n), S:O(n): https://leetcode.com/problems/trapping-rain-water/
    public void TrappingRainWater()
    {
        int[] arr = new int[] {0,1,0,2,1,0,1,3,2,1,2,1};
        Console.WriteLine(TrappingRainWater(arr));
    }

    private int TrappingRainWater(int[] arr)
    {
        if (arr == null || arr.Length == 0)
        {
            return 0;
        }
        
        int[] left = new int[arr.Length];
        int[] right = new int[arr.Length];
        left[0] = arr[0];
        right[arr.Length-1] = arr[arr.Length-1];
        
        for(int idx = 1; idx < arr.Length; idx++)
        {
            left[idx] = Math.Max(arr[idx], left[idx-1]);
        }
        
        for(int idx = arr.Length-2; idx >= 0; idx--)
        {
            right[idx] = Math.Max(arr[idx], right[idx+1]);
        }

        int trap = 0;
        
        for(int idx = 1; idx < arr.Length-1; idx++)
        {
            var bounds = Math.Min(left[idx], right[idx]);
            if (arr[idx] < bounds)
            {
                trap+= (bounds - arr[idx]);
            }
        }
        
        return trap;
    }

    /*
    This problem was asked by Facebook.
    Given the mapping a = 1, b = 2, ... z = 26, and an encoded message, count the number of ways it can be decoded.
    For example, the message '111' would give 3, since it could be decoded as 'aaa', 'ka', and 'ak'.
    You can assume that the messages are decodable. For example, '001' is not allowed.
    */
    public void WaysToEncode()
    {
        Console.WriteLine(WaysToEncode("1111"));
    }

    private int WaysToEncode(string str)
    {
        int[] dp = new int[str.Length];
        dp[0] = 1;
        dp[1] = 2;

        for(int idx = 2; idx < str.Length; idx ++)
        {
            if (int.Parse(str.Substring(idx-1, 2)) > 10)
            {
                dp[idx] = dp[idx-1] + dp[idx-2];
            }
            else
            {
                dp[idx] = dp[idx-1] + 1;
            }
        }

        return dp[str.Length-1];
    }

    //https://leetcode.com/discuss/interview-question/381172/google-phone-screen-sort-a-2d-array
    public void Sort2dArray()
    {
        int[,] arr = new int[,] 
        {
            {1, 5, 7},
            {2, 6, 9},
            {0, 4, 8}
        };

        Sort2dArray(arr);
    }

    private void Sort2dArray(int[,] arr)
    {
        for(int row = arr.GetLength(0)-1; row >= 0; row --)
        {
            for(int col = arr.GetLength(1)-1; col >= 0; col --)
            {
                FindAndReplaceBiggest(arr, row, col);
            }
        }
    }

    private void FindAndReplaceBiggest(int[,] arr, int row, int col)
    {
        int max = 0;
        int newRow = -1;
        int newCol = -1;
        int c = arr.GetLength(1)-1;

        for(int r = 0; r <= row; r ++)
        {
            if (row == r)
            {
                c = col -1;
            }

            if (arr[r, c] > max)
            {
                max = arr[r, c];
                newRow = r;
                newCol = c;
            }
        }

        int temp = arr[row, col];

        arr[row, col] = arr[newRow, newCol];

        FillInLogn(arr, temp, newRow, 0, c);
    }

    private void FillInLogn(int[,] arr, int val, int r, int start, int end)
    {
        if (end - start == 1)
        {
            int col = arr.GetLength(1) -1;

            while (col > end)
            {
                arr[r, col] = arr[r,col-1];
                col--;
            }

            arr[r, end] = val;

            return;
        }

        int mid = (start + end) / 2;

        if (val < arr[r, start])
        {
            FillInLogn(arr, val, r, start, mid);
        }
        else if (val > arr[r, start])
        {
            FillInLogn(arr, val, r, mid, end);
        }
    }

    //https://leetcode.com/discuss/interview-question/383819/facebook-phone-screen-number-of-1s-in-range-decode-ways
    public void FindOnesInRange()
    {
        int[] arr = new int[6]{0, 1, 1, 1, 0, 0};
        Console.WriteLine(FindOnesInRange(arr, 3,5,0, arr.Length-1));
    }

    private int FindOnesInRange(int[] arr, int start, int end, int s, int e)
    {
        int count = 0;

        if (start == s)
        {
            for(int idx = s; idx <= end; idx ++)
            {
                count += arr[idx] == 1 ? 1 : 0;
            }

            return count;
        }

        int mid = (s+ e) / 2;

        if (mid <= start)
        {
            count = FindOnesInRange(arr, start, end, mid, e);
        }
        else
        {
            count = FindOnesInRange(arr, start, end, s, mid);
        }

        return count;
    }

    //https://www.geeksforgeeks.org/painters-partition-problem-set-2/
    public void PainterPartition()
    {
        int[] arr = new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9};
        int k = 3;
        Console.WriteLine(PainterPartition(arr, k));
    }

    private int PainterPartition(int[] arr, int n)
    {
        int sum = 0;

        for(int idx = 0; idx < arr.Length; idx++)
        {
            sum += arr[idx];
        }

        var avg= sum / n;
        var max = int.MinValue;
        int cur = 0;

        for(int idx = 0; idx < arr.Length; idx++)
        {
            cur+= arr[idx];

            if (cur >= avg)
            {
                max = Math.Max(max, cur);
                cur = 0;
            }
        }

        return max;
    }

    //https://leetcode.com/problems/decode-ways/
    public void DecodeWays()
    {
        Console.WriteLine(DecodeWays("226"));
    }

    private int DecodeWays(string s)
    {
        if (string.IsNullOrEmpty(s) || s[0] == '0')
        {
            return 0;
        }

        int[] res = new int[s.Length+1];
        res[0] = 1;
        res[1] = s[0] != '0' ? 1 : 0;
        for (int idx = 2; idx <= s.Length; idx ++)
        {
            int first = int.Parse(s.Substring(idx-1, 1));
            int second = idx >= 2 ? int.Parse(s.Substring(idx-2, 2)) : 0;

            if (first >= 1 && first <= 9)
            {
                res[idx] += res[idx-1];
            }

            if (second > 9 && second < 27)
            {
                res[idx] += res[idx-2];
            }
        }

        return res[s.Length];
    }

    //Accepted: https://leetcode.com/problems/valid-mountain-array/
    public void CheckMountainArray()
    {
        int[] arr = {14,82,89,84,79,70,70,68,67,66,63,60,58,54,44,43,32,28,26,25,22,15,13,12,10,8,7,5,4,3};
        Console.WriteLine(CheckMountainArray(arr));
    }

    private bool CheckMountainArray(int[] arr)
    {
        if (arr.Length < 3)
        {
            return false;
        }
        
        bool increasing = true;
        bool decreasing = false;
        
        for(int idx = 1; idx < arr.Length; idx ++)
        {
            if (increasing && !decreasing && arr[idx] <= arr[idx-1])
            {
                return false;
            }
            
            if (decreasing && idx+1 < arr.Length && arr[idx] <= arr[idx+1])
            {
                return false;
            }
            
            if (increasing && !decreasing && idx + 1 < arr.Length && arr[idx] > arr[idx+1])
            {
                decreasing = true;
            }
        }
        
        return decreasing == true;
    }

    //Accepted:https://leetcode.com/problems/find-peak-element/
    public void FindPeakElement()
    {
        int[] arr = new int[] {1,2,1,4};

        Console.WriteLine(FindPeakElement(arr, 0, arr.Length-1));
    }

    private int FindPeakElement(int[] nums, int s, int e)
    {
        if (s == e)
        {
            return s;
        }

        int mid = s + (e-s)/2;
        int next = mid+1;

        if (nums[mid] > nums[mid+1])
        {
            return FindPeakElement(nums, s, mid);
        }
        else
        {
            return FindPeakElement(nums, next, e);
        }
    }

    //Accepted:https://leetcode.com/problems/number-of-burgers-with-no-waste-of-ingredients/
    public void NoOfBurgers()
    {
        int tomato = 4208;
        int cheese = 1305;

        int max = tomato / 4;
        max = Math.Min(max, cheese);
        Console.WriteLine(NoOfBurgers(tomato, cheese, 0, max, max, new List<int>()));
    }

    private IList<int> NoOfBurgers(int tomato, int cheese, int s, int e, int max, List<int> res)
    {
        int mid = s + (e-s)/2;
        int cur = (mid * 4) + (cheese - mid)*2 ;
    
        if ((s == mid || e == mid) && max > 0 && cur != tomato)
        {
            return res;
        }

        if (cur < tomato)
        {
            return NoOfBurgers(tomato, cheese, mid+1, e, max, res);
        }
        else if (cur > tomato)
        {
            return NoOfBurgers(tomato, cheese, s, mid, max, res);
        }
        else
        {
            res.Add(mid);
            res.Add(cheese- mid);
            return res; 
        }
    }

    /*
    Asked by Google
    Given a stack of N elements, interleave the first half of the stack with the second half reversed 
    using only one other queue. This should be done in-place. Recall that you can only push or pop from a stack,
    and enqueue or dequeue from a queue. For example, if the stack is [1, 2, 3, 4, 5], it should 
    become [1, 5, 2, 4, 3]. If the stack is [1, 2, 3, 4], it should become [1, 4, 2, 3].
    Hint: Try working backwards from the end state.
    */
    public void InterleaveFirstHalfWithReversed()
    {
        Stack<int> stk = new Stack<int>();
        stk.Push(1);
        stk.Push(2);
        stk.Push(3);
        stk.Push(4);
        stk.Push(5);
        stk.Push(6);

        int count = stk.Count / 2;
        Queue<int> queue = new Queue<int>();

        while(stk.Count > count)
        {
            queue.Enqueue(stk.Pop());
        }

        var res = InterleaveFirstHalfWithReversed(stk, queue);

        while (queue.Count > 0)
        {
            res.Push(queue.Dequeue());
        }
    }

    private Stack<int> InterleaveFirstHalfWithReversed(Stack<int> stk, Queue<int> queue)
    {
        int val = int.MinValue;
        if (stk.Count > 1)
        {
            val = stk.Pop();
            if (stk.Count > 1)
            {
                InterleaveFirstHalfWithReversed(stk, queue);
            }
        }

        if (queue.Count > 0)
        {
            stk.Push(queue.Dequeue());
        }

        if (val!= int.MinValue)
        {
            stk.Push(val);
        }

        return stk;
    }

    //https://leetcode.com/problems/most-stones-removed-with-same-row-or-column/
    public void MostStonesRemoved()
    {
        int[,] arr = new int[,]
        {
            {1, 1, 0},
            {1, 0, 1},
            {0, 1, 1}
        };

        int max = 0;
        CreMostStonesRemoved(arr, 0, 0, 0, ref max);

        Console.WriteLine($"Max stones which can be removed: {max}");
    }

    private void CreMostStonesRemoved(int[,] arr, int r, int c, int val, ref int max)
    {
        if (r >= arr.GetLength(0))
        {
            return;
        }

        for(int row = r; row < arr.GetLength(0); row ++)
        {
            for(int col = c; col < arr.GetLength(1); col ++)
            {
                if (IsRemovalValid(arr, row, col))
                {
                    arr[row, col] = 0;
                    if (col + 1 < arr.GetLength(1))
                    {
                        max = Math.Max(val+1, max);
                        CreMostStonesRemoved(arr, row, col + 1, val + 1, ref max);

                        arr[row, col] = 1;
                        CreMostStonesRemoved(arr, row, col + 1, val, ref max);
                    }
                    else
                    {
                        col = 0;
                        break;
                    }
                }
            }
        }
    }

    private bool IsRemovalValid(int[,] arr, int r, int c)
    {
        for(int row = 0; row < arr.GetLength(0); row ++)
        {
            if (r == row)
            {
                continue;
            }

            if (arr[row, c] == 1)
            {
                return true;
            }
        }

        for(int col = 0; col < arr.GetLength(1); col ++)
        {
            if (c == col)
            {
                continue;
            }

            if (arr[r, col] == 1)
            {
                return true;
            }
        }

        return false;
    }

    //LCMedium-LCSol-T:O(nlogn)https://leetcode.com/problems/capacity-to-ship-packages-within-d-days/
    public void ShipWithinDays()
    {
        int[] arr = new int[]{3,2,2,4,1,4};
        Console.WriteLine(ShipWithinDays(arr, 3));
    }

    private int ShipWithinDays(int[] weights, int D)
    {
        int left = 0, right = 0;
        foreach (int w in weights)
        {
            left = Math.Max(left, w);
            right += w;
        }

        while (left < right)
        {
            int mid = (left + right) / 2, need = 1, cur = 0;
            foreach (int w in weights)
            {
                if (cur + w > mid)
                {
                    need += 1;
                    cur = 0;
                }

                cur += w;
            }

            if (need > D)
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }

        return left;
    }

    //Accepted:LCMedium-SelfSol-T:O(M*N) https://leetcode.com/problems/minimum-number-of-people-to-teach/
    public void MinTeachings()
    {
        int[][] languages = new int[][] 
        {
            new int[]{2},
            new int[]{1,3},
            new int[]{1,2},
            new int[] {3}
        };

        int[][] friendships = new int[][]
        {
            new int[] {1,4},
            new int[]{1,2},
            new int[]{3,4},
            new int[]{2,3}
        };

        Console.WriteLine(MinimumTeachings(3, languages, friendships));
    }

    private int MinimumTeachings(int n, int[][] languages, int[][] friendships)
    {
        Dictionary<int, int> langToSpokenCountMap = new Dictionary<int, int>();
        HashSet<int> users = new HashSet<int>();
        Dictionary<int, HashSet<int>> userLanguages = new Dictionary<int, HashSet<int>>();

        for(int idx = 0; idx < languages.Length; idx++)
        {
            for(int count = 0; count < languages[idx].Length; count++)
            {
                if (!userLanguages.ContainsKey(idx+1))
                {
                    userLanguages[idx+1] = new HashSet<int>();
                }

                userLanguages[idx+1].Add(languages[idx][count]);
            }
        }

        bool common = false;

        foreach(int[] friends in friendships)
        {
            common = false;
            foreach(int lang in userLanguages[friends[0]])
            {
                if (userLanguages[friends[1]].Contains(lang))
                {
                    common = true;
                    break;
                }
            }

            if (!common)
            {
                users.Add(friends[0]);
                users.Add(friends[1]);
            }
        }

        int max = 0;

        foreach(int user in users)
        {
            foreach(int lang in userLanguages[user])
            {
                if(!langToSpokenCountMap.ContainsKey(lang))
                {
                    langToSpokenCountMap.Add(lang, 0);
                }

                langToSpokenCountMap[lang]++;

                max = Math.Max (max, langToSpokenCountMap[lang]);
            }
        }

        return users.Count - max;
    }

    //https://leetcode.com/problems/most-stones-removed-with-same-row-or-column/
    public void RemoveStones()
    {
        int[][] arr = new int[][]
        {
            new int[] {0,0},
            new int[] {0,1},
            new int[] {1,0},
            new int[] {1,2},
            new int[] {2,0},
            new int[] {2,2}
        };

        Console.WriteLine(RemoveStones(arr));
    }

    private int RemoveStones(int[][] stones)
    {
        DSU d = new DSU(stones.Length);

        for(int col = 0 ; col < stones.Length; col ++)
        {
            d.Union(stones[col][0], stones[col][1]);
        }

        return stones.Length - d.Count;
    }

    //https://leetcode.com/problems/find-first-and-last-position-of-element-in-sorted-array/
    public void FindFirstLastPosInArray()
    {
        int[] arr = new int[] {5,7,7,8,8,10};
        var res = FindFirstLastPosInArray(arr, 8, 0, arr.Length-1);
    }

    private int[] FindFirstLastPosInArray(int[] arr, int target, int start, int end)
    {
        if (end - start == 1)
        {
            return new int[] {-1, -1};
        }

        int mid = start + (end-start) / 2;

        if (target == arr[start])
        {
            return FindOccurence(arr, start);
        }
        else if (target == arr[end])
        {
            return FindOccurence(arr, start);
        }
        else if (target < arr[mid])
        {
            return FindFirstLastPosInArray(arr, target, start, mid);
        }
        else
        {
            return FindFirstLastPosInArray(arr, target, mid, end);
        }
    }

    private int[] FindOccurence(int[] arr, int idx)
    {
        int[] res = new int[2];
        int start = idx;
        int end = idx;

        while (++idx < arr.Length)
        {
            if (arr[idx] == arr[start])
            {
                end = idx;
            }
            else
            {
                break;
            }
        }

        res[0] = start;
        res[1] = end;

        return res;
    }

    //Accepted: https://leetcode.com/problems/rotate-image/
    /* Asked by Facebook
    Given an N by N matrix, rotate it by 90 degrees clockwise.
    For example, given the following matrix:
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9]

    you should return:

    [7, 4, 1],
    [8, 5, 2],
    [9, 6, 3] */
    public void RotateImage()
    {
        int[][] arr = new int[3][]
        {
            new int[] {1,2,3},
            new int[] {4,5,6},
            new int[] {7,8,9}
        };

        RotateImage(arr);
    }

    private void RotateImage(int[][] arr)
    {
        //Transpose
        for(int row = 0; row < arr.Length; row ++)
        {
            for(int col = row; col < arr.Length; col++)
            {
                int temp = arr[col][row];
                arr[col][row] = arr[row][col];
                arr[row][col] = temp;
            }
        }

        //Reverse
        for(int row = 0; row < arr.Length; row ++)
        {
            for(int col = 0; col < arr.Length/2; col++)
            {
                int temp = arr[row][col];
                arr[row][col] = arr[row][arr.Length-1-col];
                arr[row][arr.Length-1-col] = temp;
            }
        }
    }

    //Accepted-LcMedium-SelfSol-T:O(n) S:O(n) https://leetcode.com/problems/simplify-path/
    public void SimplifyPath()
    {
        string path = "/../";
        Console.WriteLine(SimplifyPath(path));
    }

    private string SimplifyPath(string path)
    {
        Stack<string> stk = new Stack<string>();

        string[] strs = path.Split("/");

        foreach(string str in strs)
        {
            if (str == ".." )
            {
                if (stk.Count > 0)
                {
                    stk.Pop();
                }
            }
            else if (str == "." || str == "")
            {

            }
            else
            {
                stk.Push(str);
            }
        }

        StringBuilder sb = new StringBuilder();
        string res = string.Empty;

        foreach(string val in stk)
        {
            res =  "/" + val + res;
        }

        return res == string.Empty ? "/" : res;
    }

    //Accepted-LcMedium-SelfSol-T:O(N*N) S:O(n^2) https://leetcode.com/problems/rotting-oranges/
    public int OrangesRotting() 
    {
        int[][] grid = new int[][]
        {
            new int[] {2,0,1,1,1,1,1,1,1,1},
            new int[] {1,0,1,0,0,0,0,0,0,1},
            new int[] {1,0,1,0,1,1,1,1,0,1},
            new int[] {1,0,1,0,1,0,0,1,0,1},
            new int[] {1,0,1,0,1,0,0,1,0,1},
            new int[] {1,0,1,0,1,1,0,1,0,1},
            new int[] {1,0,1,0,0,0,0,1,0,1},
            new int[] {1,0,1,1,1,1,1,1,0,1},
            new int[] {1,0,0,0,0,0,0,0,0,1},
            new int[] {1,1,1,1,1,1,1,1,1,1}
        };

        int max = 2;

        Queue<Tuple<int, int, int>> queue = new Queue<Tuple<int, int, int>>();

        for(int r = 0; r < grid.Length; r++)
        {
            for(int c = 0; c < grid[0].Length; c++)
            {
                if (grid[r][c] == 2)
                {
                    var newItem = new Tuple<int, int, int>(r,c,2);
                    queue.Enqueue(newItem);
                }
            }
        }

        OrangesRotting(grid, queue, ref max);

        for(int r = 0; r < grid.Length; r++)
        {
            for(int c = 0; c < grid[0].Length; c++)
            {
                if (grid[r][c] == 1)
                {
                    return -1;
                }
            }
        }
        
        return max-2;
    }
    
    private void OrangesRotting(int[][] grid, Queue<Tuple<int, int, int>> queue, ref int max)
    {
        while(queue.Count > 0)
        {
            var item = queue.Dequeue();
            var r = item.Item1;
            var c = item.Item2;
            var val = item.Item3;

            if (c > 0 && grid[r][c-1] == 1)
            {
                var newItem = new Tuple<int, int, int>(r, c-1, val+1);
                max = Math.Max(max, val+1);
                queue.Enqueue(newItem);
                grid[r][c-1] = val+1;
            }

            if (c+1 < grid[0].Length && grid[r][c+1] == 1)
            {
                var newItem = new Tuple<int, int, int>(r, c+1, val+1);
                max = Math.Max(max, val+1);
                queue.Enqueue(newItem);
                grid[r][c+1] = val+1;
            }

            if (r > 0 &&  grid[r-1][c] == 1)
            {
                var newItem = new Tuple<int, int, int>(r-1, c, val+1);
                max = Math.Max(max, val+1);
                queue.Enqueue(newItem);
                grid[r-1][c] = val+1;
            }

            if (r+1 < grid.Length && grid[r+1][c] == 1)
            {
                var newItem = new Tuple<int, int, int>(r+1, c, val+1);
                max = Math.Max(max, val+1);
                queue.Enqueue(newItem);
                grid[r+1][c] = val+1;
            }
        }
    }

    //Accepted: https://leetcode.com/problems/next-permutation/
    public void NextPermutation()
    {
        //int[] arr = new int[]{1, 2, 3};
        var arr = PopulateArray("abc");
        for(int i = 0; i < 6; i ++)
        {
            NextPermutationCre(arr);

            for(int j = 0; j < arr.Length; j++)
            {
                Console.WriteLine((char)(arr[j]+ 'a'));
            }
        }
    }

    private int[] PopulateArray(string str)
    {
        int[] arr = new int[str.Length];
        for(int idx = 0; idx < str.Length; idx++)
        {
            arr[idx] = str[idx] - 'a';
        }

        return arr;
    }

    private void NextPermutationCre(int[] arr)
    {
        int idx = arr.Length-2;

        while(idx >= 0 && arr[idx] > arr[idx+1]) // break on first smaller number
        {
            idx--;
        }

        if (idx == -1)
        {
            return;
        }

        if (arr[idx] <= arr[arr.Length-1])
        {
            Helpers.Swap(arr, idx, arr.Length-1);
        }
        else
        {
            Helpers.Swap(arr, idx, idx+1);
        }

        ReverseArray(arr, idx+1);
    }

    private void NextPermutation(int[] arr)
    {
        int idx = arr.Length -2;

        while (idx >= 0 && arr[idx] >= arr[idx+1]) // Find the biggest number in the sequence, break when it is smaller
        {
            idx--;
        }

        if (idx >= 0)
        {
            int i = arr.Length-1;
            while(i > idx && arr[idx] >= arr[i]) // Find the smallest 
            {
                i--;
            }

            Helpers.Swap(arr, i, idx); //Swap them
        }

        ReverseArray(arr, idx+1);
    }

    private void ReverseArray(int[] arr, int start)
    {
        int temp = 0;
        int end = arr.Length-1;

        while (start < end)
        {
             temp = arr[start];
            arr[start++] = arr[end];
            arr[end--] = temp;
        }
    }

    //https://leetcode.com/problems/product-of-array-except-self/
    public void ProductExceptSelf()
    {
        int[] arr = new int[] {1,2,3,4};
        var res = ProductExceptSelf(arr);
    }

    private int[] ProductExceptSelf(int[] nums)
    {
        int[] left = new int[nums.Length];
        int[] right = new int[nums.Length];
        int[] res = new int[nums.Length];
        
        left[0] = 1;
        right[nums.Length-1] = 1;
        
        for(int i = 1; i < nums.Length; i++)
        {
            left[i] = nums[i-1] * left[i-1];
        }
        
        for(int i = nums.Length-2; i >=0; i--)
        {
            right[i] = nums[i+1] * right[i+1];
        }
        
        for(int i = 0; i < nums.Length; i++)
        {
            res[i] = left[i] * right[i];
        }
        
        return res;
    }

    //https://leetcode.com/problems/path-with-minimum-effort/
    public void MinimumEffortPath()
    {
        int[] x = new int[] {1,-1,0, 0,};
        int[] y = new int[] {0, 0,1,-1 };

        int[][] heights = new int[][]
        {
            new int[] {1,2,3},
            new int[] {3,8,4},
            new int[] {5,3,5}
        };

        Console.WriteLine(MinimumEffortPath(heights, x, y));
    }

    private int MinimumEffortPath(int[][] heights, int[] x, int[] y)
    {
        Heap<int[]> queue = new Heap<int[]>(true, (a,b)=> {return a[2].CompareTo(b[2]);} );
        int[,] efforts = new int[heights.Length, heights[0].Length];

        for(int i = 0; i < heights.Length; i++)
        {
            for(int j = 0; j < heights.Length; j++)
            {
                efforts[i,j] = int.MaxValue;
            }
        }

        queue.Push(new int[3]{0,0,0});
        efforts[0,0] = 0;

        while (queue.Count > 0)
        {
            var cur = queue.Pop();
            var r = cur[0];
            var c = cur[1];
            
            if (r == heights.Length-1 && c == heights[0].Length-1)
            {
                return cur[2];
            }

            for(int i = 0; i < x.Length; i++)
            {
                var row = x[i] + r;
                var col = y[i] + c;

                if (row == heights.Length || row < 0 || col == heights[row].Length || col < 0)
                {
                    continue;
                }

                int diff = Math.Abs(heights[row][col] - heights[r][c]);
                int effort = Math.Max(cur[2], diff);

                if (efforts[row, col] > effort)
                {
                    efforts[row, col] = effort;
                    queue.Push(new int[3] {row, col, effort});
                }
            }
        }

        return -1;
    }

    /*
    Microsoft Azure Storage: Given lists of integer list, return the list of integers which are duplicated across all the lists
    */
    public void FindDuplicates()
    {
        List<int> list1 = new List<int>() {1, 2, 2, 5, 6};
        List<int> list2 = new List<int>() {1, 2, 2, 5, 6};
        List<int> list3 = new List<int>() {1, 2, 2, 4, 5};

        List<List<int>> lists = new List<List<int>>()
        {
            list1, list2, list3
        };

        var res = FindDuplicates(lists);
    }

    private IList<int> FindDuplicates(List<List<int>> lists)
    {
        List<int> pointers = new List<int>();
        List<int> res = new List<int>();
        
        for(int idx = 0; idx < lists.Count; idx++)
        {
            pointers.Add(0);
        }

        int max = int.MinValue;
        while(pointers[0] < lists[0].Count)
        {
            bool isMatch = true;

            for(int idx = 0; idx < lists.Count; idx++)
            {
                var pointer = pointers[idx];
                if (pointers[idx] >= lists[idx].Count)
                {
                    return res;
                }

                max = Math.Max(max, lists[idx][pointer]);

                if (lists[idx][pointer] < max)
                {
                    isMatch = false;
                    pointers[idx]++;
                }
            }

            if (isMatch)
            {
                res.Add(lists[0][pointers[0]]);

                for(int i = 0; i < lists.Count; i++)
                {
                    pointers[i]++;
                }
            }
        }

        return res;
    }

    /*
    Given a set of integers, find the minimum number of shuffle to get the array in sequence.
    */
    public void MinShuffleToArrange()
    {
        int[] arr = new int[] {2,5,1,3,4};
        Console.WriteLine(MinShuffleToArrange(arr));
    }

    private int MinShuffleToArrange(int[] arr)
    {
        int[] dp = new int[arr.Length];
        int len = 0;

        foreach(int num in arr)
        {
            int idx = Array.BinarySearch(dp, 0, len, num);

            if (idx < 0)
            {
                idx = -(idx+ 1);
            }

            dp[idx] = num;

            if (len == idx)
            {
                len++;
            }
        }

        return len;
    }

    /*
    Microsoft Azure Storage: Find the element in a sorted unbounded array
    */
    public void FindElementInSortedUnboundArray()
    {
        int upper = 100;
        int[] arr= new int[10000000];
        int num = 10000000;
        int lower = 0;
        int factor = 1000;

        while(true)
        {
            try
            {
                if (num > arr[upper])
                {
                    lower = upper;
                    upper*=factor;
                }
                else
                {
                    FindElementInSortedUnboundArray(arr, num, lower, upper);
                }
            }
            catch(Exception)
            {
                upper = (upper-lower)/2 + upper;
                factor = 1000;
            }
        }
    }

    private bool FindElementInSortedUnboundArray(int[] arr, int num, int start, int end)
    {
        if (end-start == 1 && arr[start] != num)
        {
            return false;
        }

        try
        {
            int mid = (end-start)/2 + start;

            if (num < arr[mid])
            {
                return FindElementInSortedUnboundArray(arr, num, start, mid-1);
            }
            else if (num > arr[mid])
            {
                return FindElementInSortedUnboundArray(arr, num, mid+1, end);
            }
            else
            {
                return true;
            }
        }
        catch(Exception ex)
        {
            end = (end -start)/2+ start;

            return FindElementInSortedUnboundArray(arr, num, start, end);
        }
    }

    /*
    Description : You are given an array and a integer k.
    Let's define the target position of every element as the index at which it would appear if this element was sorted.
    For example, if the array is [1, 2, 3, 4, 5] then the target position for 1 is 0, for 2 is 1 and so on.

    Now, every element in the array is present at either its target position or k indexes away from its target position.

    For instance, if the target position of an element is index 5, and k = 2, then this element may be present anywhere between indexes [3, 7] included.

    The task is to sort the array.

    Example input : [1, 4, 5, 2, 3, 8, 7, 6], k = 2
    Expected output : [1, 2, 3, 4, 5, 6, 7, 8]
    */
    public void SortArrayWithKPositionDisplaced()
    {
        int[] arr = new int[] {1, 4, 5, 2, 3, 8, 7, 6};
        int k = 2;

        SortArrayWithKPositionDisplaced(arr, k);
    }

    private void SortArrayWithKPositionDisplaced(int[] arr, int k)
    {
        int idx = 0;

        while(idx < arr.Length)
        {
            while(arr[idx]-1 != idx)
            {
                Helpers.Swap(arr, idx, idx+k);
            }

            idx++;
        }
    }

    //Accepted-LCMedium-LCSol-T:O(9^m) S:O(1) https://leetcode.com/problems/sudoku-solver/
    public void SolveSudoku()
    {
        char[][] board = new char[9][]
        {
            new char[] {'5','3','.','.','7','.','.','.','.'},
            new char[] {'6','.','.','1','9','5','.','.','.'},
            new char[] {'.','9','8','.','.','.','.','6','.'},
            new char[] {'8','.','.','.','6','.','.','.','3'},
            new char[] {'4','.','.','8','.','3','.','.','1'},
            new char[] {'7','.','.','.','2','.','.','.','6'},
            new char[] {'.','6','.','.','.','.','2','8','.'},
            new char[] {'.','.','.','4','1','9','.','.','5'},
            new char[] {'.','.','.','.','8','.','.','7','9'}
        };

        SolveSudoku(board);
    }

    private bool SolveSudoku(char[][] board)
    {
        for(int row = 0; row < 9; row++)
        {
            for(int col = 0; col < 9; col++)
            {
                if (board[row][col] != '.')
                {
                    continue;
                }

                for(int i = 1; i <= 9; i++)
                {
                    if (CanPlaceInSudoku(board, row, col, i))
                    {
                        char temp = board[row][col];
                        board[row][col] = char.Parse(i.ToString());

                        if (SolveSudoku(board))
                        {
                            return true;
                        }
                        else
                        {
                            board[row][col] = temp;
                        }
                    }
                }

                return false;
            }
        }

        return true;
    }

    private bool CanPlaceInSudoku(char[][] board, int row, int col, int num)
    {
        int boxR = (row / 3) * 3;
        int boxC = (col / 3) *3;
        char ch = char.Parse(num.ToString());

        for(int i=0; i < 9; i++)
        {
            if(board[row][i] == ch || board[i][col] == ch) //check in row or column
            {
                 return false; 
            }

            if(board[boxR + (i/3)][boxC + (i%3)] == ch)
            {
                return false; //check in box
            }
        }

        return true;
    }

    /*
     Microsoft Azure Machine Learning: Given list of flights and cost, return the min cost to reach the destination
    */
    //https://leetcode.com/problems/cheapest-flights-within-k-stops/
    public void FlightCost()
    {
        // Tuple<string, string, int> d1 = new Tuple<string, string, int>("s", "c", 10);
        // Tuple<string, string, int> d2 = new Tuple<string, string, int>("s", "n", 50);
        // Tuple<string, string, int> d3 = new Tuple<string, string, int>("c", "n", 10);
        // Tuple<string, string, int> d4 = new Tuple<string, string, int>("n", "o", 10);

        // List<Tuple<string, string, int>> flightDetails = new List<Tuple<string, string, int>>(){d1, d2, d3, d4};
        // string start = "s";
        // string dest = "o";
        // int k = 99;

        //Console.WriteLine(FlightCost(flightDetails, start, dest, k));


        int[][] arr = new int[][]
        {
            new int[] {0,1,100},
            new int[] {1,2,100},
            new int[] {0,2,500}
        };

        int start = 0;
        int dest =  2;
        int k = 1;

        Console.WriteLine(FindCheapestPrice(arr.Length, arr, start, dest, k));
    }

    private int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k)
    {
        Dictionary<int, List<int[]>> map = new Dictionary<int, List<int[]>>();

        for(int r = 0; r < flights.Length; r++)
        {
            if (!map.ContainsKey(flights[r][0]))
            {
                map.Add(flights[r][0], new List<int[]>());
            }

            map[flights[r][0]].Add(new int[]{flights[r][1], flights[r][2]});
        }

        Heap<int[]> queue = new Heap<int[]>(true, (a, b)=> 
        {
            return a[2].CompareTo(b[2]);
        });

        queue.Push(new int[]{flights[0][0], 0, 0});

        while (queue.Count > 0)
        {
            var cur = queue.Pop();

            if (cur[0] == dst)
            {
                return cur[2];
            }

            foreach(int[] flight in map[cur[0]])
            {
                if (cur[1]+1 <= k+1)
                {
                    queue.Push(new int[]{flight[0], cur[1]+1, cur[2] + flight[1]});
                }
            }
        }

        return -1;
    }

    /*
    Find all distinct combinations of a given length k of an array
    Input: {1,2,3}, k=2
    Output: {1,2}, {1,3}, {2,3}
    Input: {1,2,1}, k=2
    Output: {1,1}, {1,2}
    */
    public void DistinctCombination()
    {
        int[] arr = new int[] {1,2,3};
        int k = 2;
        List<List<int>> res = new List<List<int>>();
        List<int> cur = new List<int>();
        DistinctCombination(arr, k, 0, cur, res);

        foreach(List<int> list in res)
        {
            foreach(int num in list)
            {
                Console.WriteLine(num);
            }
        }
    }

    private void DistinctCombination(int[] arr, int k, int idx,  List<int> cur, List<List<int>> res)
    {
        if (cur.Count == k)
        {
            res.Add(new List<int>(cur));
            return;
        }

        for(int i = idx; i < arr.Length; i ++)
        {
            cur.Add(arr[i]);
            DistinctCombination(arr, k, i+1, cur, res);
            cur.RemoveAt(cur.Count-1);
        }

        return;
    }

    //https://leetcode.com/problems/cherry-pickup/
    public void CherryPickup()
    {
        int[][] grid = new int[][]
        {
            new int[] { 0, 1, 1, 0, 0},
            new int[] { 1, 1, 1, 1, 0},
            new int[] {-1, 1, 1, 1,-1},
            new int[] { 0, 1, 1, 1, 0},
            new int[] { 1, 0,-1, 0, 0}
        };

        int[] x = new int[] {1, 0};
        int[] y = new int[] {0, 1};
        
        int?[,,,] dp = new int?[grid.Length,grid[0].Length,grid.Length,grid[0].Length];

        var res = Math.Max(0, CherryPickup(grid, 0, 0, 0, 0, dp));
        Console.WriteLine(res);
    }

    private int CherryPickup(int[][] grid, int r1, int c1, int r2, int c2, int?[,,,] dp)
    {
        if (r1 >= grid.Length || c1 >= grid[0].Length ||  r2 >= grid.Length ||  c2 >= grid[0].Length 
        || grid[r1][c1] == -1 || grid[r2][c2] == -1)
            {
                return int.MinValue;
            }

        if (r1 == grid.Length-1 && c1 == grid[0].Length-1)
        {
            return grid[r1][c1];
        }

        if (dp[r1,c1,r2,c2] != null)
        {
            return (int)dp[r1,c1,r2,c2];
        }

        if (r2 == grid.Length-1 && c2 == grid[0].Length-1)
        {
            return grid[r2][c2];
        }

        int cherries = 0;

        if (r1==r2 && c1 == c2)
        {
            cherries = grid[r1][c1];
        }
        else
        {
            cherries = grid[r1][c1] + grid[r2][c2];
        }

        cherries += Math.Max(
            Math.Max(CherryPickup(grid, r1+1, c1, r2+1, c2, dp), CherryPickup(grid, r1+1, c1, r2, c2+1, dp)),
            Math.Max(CherryPickup(grid, r1, c1+1, r2+1, c2, dp), CherryPickup(grid, r1, c1+1, r2, c2+1, dp)));

        dp[r1,c1,r2,c2] = cherries;

        return (int)dp[r1,c1,r2,c2];
    }

    //https://leetcode.com/problems/minimum-moves-to-move-a-box-to-their-target-location/
    public void MinPushBox()
    {
        char[][] grid = new char[][]
        {
            new char[] {'#','#','#','#','#','#'},
            new char[] {'#','T','#','#','#','#'},
            new char[] {'#','.','.','B','.','#'},
            new char[] {'#','.','#','#','.','#'},
            new char[] {'#','.','.','.','S','#'},
            new char[] {'#','#','#','#','#','#'}
        };

        Console.WriteLine(MinPushBox(grid));
    }

    private int MinPushBox(char[][] grid)
    {
        int[] x = new int[] {0, 1, -1, 0};
        int[] y = new int[] {1, 0, 0, -1};

        int[] target = new int[2];
        int[] start = new int[2];
        int[] box = new int[2];

        for(int r = 0; r < grid.Length; r++)
        {
            for(int c = 0; c < grid[r].Length; c++)
            {
                if (grid[r][c] == 'T')
                {
                    target[0] = r;
                    target[1] = c;
                }
                else if (grid[r][c] == 'S')
                {
                    start[0] = r;
                    start[1] = c;
                }
                else if (grid[r][c] == 'B')
                {
                    box[0] = r;
                    box[1] = c;
                }
            }
        }

        Heap<int[]> queue = new Heap<int[]>(true, (x,y)=> 
        {
            return x[0] - y[0];
        });

        bool[,] visited = new bool[grid.Length, grid[0].Length];

        queue.Push(new int[]{Distance(start[0],start[1], box[0], box[1]), 0, start[0], start[1]});

        bool reachedBox = false;

        visited[0, 0] = true;
        while(queue.Count > 0)
        {
            var cur = queue.Pop();
            var r = cur[2];
            var c = cur[3];
            
            for(int idx = 0; idx < x.Length; idx++)
            {
                var row = r + x[idx];
                var col = c + y[idx];

                if (row < 0 || col < 0 || row >= grid.Length || col >= grid[0].Length || visited[row, col] || grid[row][col] == '#')
                {
                    continue;
                }

                if (grid[row][col] == 'T')
                {
                    return cur[1];
                }
                else if (grid[row][col] == 'B')
                {
                    reachedBox = true;
                    start[0] = box[0];
                    start[1] = box[1];
                }

                visited[row, col] = true;

                if (!reachedBox)
                {
                    queue.Push(new int[]{Distance(row, col, box[0], box[1]), 0, row, col});
                }
                else
                {
                    queue.Push(new int[]{Distance(row, col, target[0], target[1])+cur[1]+1, cur[1]+1, row, col});
                }
            }
        }

        return -1;
    }

    private int Distance(int x, int y, int tx, int ty)
    {
        return Math.Abs(x-tx) + Math.Abs(ty-y);
    }

    /*
    Each experience has a start time, end time, interest level.
    // # An interest level is defined by non-negative number. Higher the number means more interest.
    // # You want to schedule a day maximizing the total interest level. 

    // [13-14] [12-13.5] [15-16]
    //  2    4        1
    //4+1
    // [12-14] [13-13.5] [13.5-15] [15-16]
    //  6          1       6           1
    // 8
    // 12-14: 7
    // 13-13.5
    // [s,e,interest]
    */
    public void GetInterestLevel()
    {
        float[][] input = new float[][]
        {
            new float[]{2, 5, 5},
            new float[]{3, 6, 6},
            new float[]{5, 10, 2},
            new float[]{4, 10, 8},
            new float[]{8, 9, 5},
            new float[]{13, 14, 1},
            new float[]{13, 17, 5},
            new float[]{14, 16, 8}
        };

        Array.Sort(input, (x,y)=> x[0].CompareTo(y[0]));
        
        float res = float.MinValue;
        Dictionary<int, float> memo = new Dictionary<int, float>();
        
        for(int idx = 0; idx < input.Length; idx++)
        {
            res = Math.Max(res, GetInterestLevel(input, idx,  memo));    
        }
        
        Console.WriteLine(res);
    }

    private static float GetInterestLevel(float[][] input,  int idx, Dictionary<int, float> memo)
    {
        if (idx == input.Length-1)
        {
            return 0;
        }
        
        if (memo.ContainsKey(idx))
        {
            return memo[idx];
        }

        float res = int.MinValue;

        for(int i = idx; i < input.Length; i++)
        {
            if (input[i][0] < input[idx][1])
            {
                continue;
            }

            float cur = GetInterestLevel(input, i, memo) + input[i][2];
            res = Math.Max(res, cur);

            if (!memo.ContainsKey(i))
            {
                memo.Add(i, 0);
            }

            memo[i] = cur == int.MinValue ? 0 : cur;
        }

        return res;
    }

    //https://leetcode.com/problems/sliding-window-median/
    public void SlidingWindowMedian()
    {
        int[] arr = new int[] {1,3,-1,-3,5,3,6,7};
        int k = 3;

        var res = SlidingWindowMedian(arr, k);
    }

    private double[] SlidingWindowMedian(int[] nums, int k)
    {
        int n = nums.Length - k +1;
        double[] res = new double[n];
        Heap<int> minHeap = new Heap<int>(true);
        Heap<int> maxHeap = new Heap<int>(false);

        for(int i = 0; i <= nums.Length; i++)
        {
            if (i >= k)
            {
                res[i-k] = GetMedian(minHeap, maxHeap);
                RemoveElementFromSlidingWindowMedian(nums[i-k], minHeap, maxHeap);
            }
            if (i < nums.Length)
            {
                AddElementToSlidingWindowMedian(nums[i], minHeap, maxHeap);
            }
        }

        return res;
    }

    private void AddElementToSlidingWindowMedian(int num, Heap<int> minHeap, Heap<int> maxHeap)
    {
        if (num < GetMedian(minHeap, maxHeap))
        {
            maxHeap.Push(num);
        }
        else
        {
            minHeap.Push(num);
        }

        if (maxHeap.Count > minHeap.Count)
        {
            minHeap.Push(maxHeap.Pop());
        }

        if (minHeap.Count - maxHeap.Count > 1)
        {
            maxHeap.Push(minHeap.Pop());
        }
    }

    private void RemoveElementFromSlidingWindowMedian(int num, Heap<int> minHeap, Heap<int> maxHeap)
    {
        if (num < GetMedian(minHeap, maxHeap))
        {
            maxHeap.Remove(num);
        }
        else
        {
            minHeap.Remove(num);
        }

        if (maxHeap.Count > minHeap.Count)
        {
            minHeap.Push(maxHeap.Pop());
        }

        if (minHeap.Count - maxHeap.Count > 1)
        {
            maxHeap.Push(minHeap.Pop());
        }
    }

    private double GetMedian(Heap<int> minHeap, Heap<int> maxHeap)
    {
        if (minHeap.Count == 0 && maxHeap.Count == 0)
        {
            return 0;
        }

        if (minHeap.Count == maxHeap.Count)
        {
            return (minHeap.Peek() + maxHeap.Peek()) / 2;
        }
        else
        {
            return minHeap.Peek();
        }
    }

    //Accepted: https://leetcode.com/problems/queens-that-can-attack-the-king/
    public void QueensAttackKing()
    {
        int[][] queens = new int[][]
        {
            new int[] {2, 0},
            new int[] {1, 6},
            new int[] {3, 4},
            new int[] {4, 1},
        };

        int[] king = new int[2] {3, 5};

        List<Pair> pairs = new List<Pair>();
        pairs.Add(new Pair(0, -1));
        pairs.Add(new Pair(0, 1));
        pairs.Add(new Pair(-1, 0));
        pairs.Add(new Pair(1, 0));
        pairs.Add(new Pair(-1, -1));
        pairs.Add(new Pair(-1, 1));
        pairs.Add(new Pair(1, -1));
        pairs.Add(new Pair(1, 1));

        var res = QueensAttackKing(queens, king, pairs);
    }

    private IList<IList<int>> QueensAttackKing(int[][] queens, int[] king, List<Pair> pairs)
    {
        int[][] board = PopulateBoard(queens, king);
        IList<IList<int>> res = new List<IList<int>>();

        foreach(Pair p in pairs)
        {
            var r = king[0] + p.x;
            var c = king[1] + p.y;

            while(true)
            {
                if (! IsValid(board, r, c))
                {
                    break;
                }

                if (board[r][c] == 1)
                {
                    var list = new List<int>();
                    list.Add(r);
                    list.Add(c);
                    res.Add(list);
                    break;
                }

                r += p.x * 1;
                c += p.y * 1;
            }
        }

        return res;
    }

    private bool IsValid(int[][] board, int r, int c)
    {
        if (r < 0 || r > board.Length-1 || c < 0 || c > board[0].Length-1)
        {
            return false;
        }

        return true;
    }

    private int[][] PopulateBoard(int[][] queens, int[] king)
    {
        int[][] board = new int[8][]
        {
            new int[8]{-1, -1, -1, -1, -1, -1, -1, -1},
            new int[8]{-1, -1, -1, -1, -1, -1, -1, -1},
            new int[8]{-1, -1, -1, -1, -1, -1, -1, -1},
            new int[8]{-1, -1, -1, -1, -1, -1, -1, -1},
            new int[8]{-1, -1, -1, -1, -1, -1, -1, -1},
            new int[8]{-1, -1, -1, -1, -1, -1, -1, -1},
            new int[8]{-1, -1, -1, -1, -1, -1, -1, -1},
            new int[8]{-1, -1, -1, -1, -1, -1, -1, -1},
        };

        for(int i = 0; i < queens.Length; i++)
        {
            int r = queens[i][0];
            int c = queens[i][1];
            board[r][c] = 1;
        }

        return board;
    }

    //https://leetcode.com/problems/find-in-mountain-array/
    public void FindInMountainArray()
    {
        int[] arr = new int[] {0,1,2,4,2,1};
        Console.WriteLine(FindInMountainArray(arr, 3, 0, arr.Length-1));
    }

    private int FindInMountainArray(int[] arr, int target, int start, int end)
    {
        if (end - start == 1)
        {
            if (arr[start] == target)
            {
                return start;
            }
            else if (arr[end] == target)
            {
                return end;
            }

            return -1;
        }

        int idx = -1;

        int mid = (end - start) / 2 + start;

        if (arr[mid] > target)
        {
            idx = FindInMountainArray(arr, target, start, mid);
        }
        else if (arr[mid] < target)
        {
            idx = FindInMountainArray(arr, target, mid, end);
        }
        else
        {
            return mid;
        }

        return idx;
    }

    //https://leetcode.com/discuss/interview-question/373202
    public void OptimalUtilization()
    {
        int[][] a = new int[3][]
        {
             new int[] {1, 8},
             new int[] {2, 15},
             new int[] {3, 9},
        };

        int[][] b = new int[3][]
        {
             new int[] {1, 8}, 
             new int[] {2, 11},
             new int[] {3, 12},
        };

        var res = OptimalUtilization(a, b, 20);
    }

    private List<int[]> OptimalUtilization(int[][] a, int[][] b, int target)
    {
        if (a == null || b == null)
        {
            return null;
        }

        if (a.Length < b.Length)
        {
            return OptimalUtilization(b, a, target);
        }

        int close = 0;

        List<int[]> res = null;

        foreach(int[] big in a)
        {
            foreach(int[] small in b)
            {
                var cur = big[1] + small[1];

                if (cur <= target && target - cur < target - close )
                {
                    res = new List<int[]>();
                    res.Add(new int[] { big[0], small[0] });
                    close = cur;
                }
                else if (cur == close)
                {
                    res.Add(new int[] { big[0], small[0] });
                }
            }
        }

        return res;
    }

    //Accepted: https://leetcode.com/problems/longest-arithmetic-subsequence-of-given-difference/
    public void LongestSubsequenceOfGivenDifference()
    {
        int[] arr = new int[]{-6,6,-8,0,7,-8,5,-7,10,-10};
        Console.WriteLine(LongestSubsequenceOfGivenDifference(arr, -6));
    }

    private int LongestSubsequenceOfGivenDifference(int[] arr, int diff) 
    {
        Dictionary<int, int> map = new  Dictionary<int, int>();
        
        int max = 1;
        
        for(int idx = arr.Length-1; idx >=0; idx --)
        {
            if (map.ContainsKey(arr[idx] + diff) && map[arr[idx] + diff] > 0)
            {
                map[arr[idx]] = map[arr[idx] + diff] +1;

                max = Math.Max(max, map[arr[idx]]); 
            }
            else
            {
                map.TryAdd(arr[idx], 1);
            }
        }

        return max;
    }

    //LCMedium-Self-O(n)-Accepted: https://leetcode.com/problems/minimum-deletion-cost-to-avoid-repeating-letters/
    public void MinCostForDeletionToAvoidRepetition()
    {
        string s = "aabaa";
        int[] cost = new int[] {1, 2, 3, 4, 1};

        Console.WriteLine(MinCostForDeletionToAvoidRepetition(s, cost));
    }

    private int MinCostForDeletionToAvoidRepetition(string s, int[] cost)
    {
        int idx = 0;
        int res = 0;
        int max = int.MinValue;
        int curCost = 0;

        while (idx < s.Length)
        {
            curCost = cost[idx];
            max = cost[idx];

            while (++idx < s.Length && s[idx] == s[idx-1])
            {
                max = Math.Max(max, cost[idx]);
                curCost += cost[idx];
            }

            res += curCost - max;
        }

        return res;
    }

    //https://leetcode.com/problems/shortest-subarray-to-be-removed-to-make-array-sorted/
    public void FindLengthOfShortestSubarray()
    {
        int[] arr = new int[] {1,2,3,10,4,2,3,5};
        Console.WriteLine(FindLengthOfShortestSubarray(arr));
    }

    private int FindLengthOfShortestSubarray(int[] arr)
    {
        int n = arr.Length;
        int start = 0, end = n - 1;
        
        while(start < n - 1 && arr[start] <= arr[start + 1])
        {
            start++;
        }

        if(start == n - 1)
        {
            return 0;
        }

        while(end >= start && arr[end] >= arr[end - 1])
        {
            end--;
        }

        int result = Math.Min(n - 1 - start, end);

        int i = 0, j = end;
        while(i <= start && j < n)
        {
            if(arr[j] >= arr[i])
            {
                result = Math.Min(result, j - i - 1);
                i++;
            }
            else
            {
                j++;
            }
        }

        return result;
    }

    //https://leetcode.com/discuss/interview-question/356520
    public void MinChairs()
    {
        List<Pair> pairs = new List<Pair>();
        pairs.Add(new Pair(1,5));
        pairs.Add(new Pair(2,5));
        pairs.Add(new Pair(6,7));
        pairs.Add(new Pair(5,6));
        pairs.Add(new Pair(3,8));

        Console.WriteLine(MinChairs(pairs));
    }

    private int MinChairs(List<Pair> pairs)
    {
        var sorted = pairs.OrderBy(x => x.y);

        int idx = 0;
        int count = pairs.Count;
        HashSet<int> visited = new HashSet<int>();

        while (idx < pairs.Count)
        {
            var cur = pairs[idx];
            int next = idx + 1;

            while (next < sorted.Count())
            {

                if (pairs[next].x >= cur.y && !visited.Contains(next))
                {
                    count --;
                    visited.Add(next);
                    cur = sorted.ElementAt(next);
                }

                next ++;
            }

            visited.Add(idx);

            idx++;
        }

        return count;
    }

    //https://leetcode.com/discuss/interview-question/419839/amazon-phone-screen
    public void MaxSquareSumInMatrix()
    {

    }

    // private int MaxSquareSumInMatrix(int[,] arr, int sum)
    // {
    //     int totalElements = arr.GetLength(0) * arr.GetLength(1);
    //     int[,] dp = new int[totalElements, arr.GetLength(1)];

    //     int dimension = 2;
    //     int maxSquare = 1;

    //     for(int row = 0; row< arr.GetLength(0); row ++)
    //     {
    //         for(int col = 0; col < arr.GetLength(1); col++)
    //         {
    //             var res = CalculateSum(row, col, dimension, sum, dp, arr);
    //             maxSquare = Math.Max(maxSquare, res);
    //         }
    //     }
    // }

    private int CalculateSum(int row, int col, int requiredSum, int[,] dp, int[,] arr)
    {
        //Given a dimension
        int dimension = 2;
        int curDimension = dimension;
        int sum = 0;
        int c = col;
        int r = row+1;
        int offset = 0;

        // Increment dimension when current dimension meets the criteria
        while (curDimension <= dimension)
        {
            for(; c < col + dimension; c++)
            {
                //if ()
                for(; r < row + dimension; r++)
                {
                    if ((r == row + dimension -1 && c == col + dimension -1) && sum <= requiredSum)
                    {
                        curDimension += dimension;
                        dimension ++;
                    }
                    else
                    {
                        offset = r * 10 + c;
                        dp[offset, dimension] += dimension > 2 && r > 0 ? dp[offset, dimension - 1] + arr[r,c] : arr[r-1, c] + arr[r, c];
                    }
                }
            }
        }

        return curDimension;
    }

    //Accepted: T:O(log n) : https://leetcode.com/problems/search-in-rotated-sorted-array/
    public void SearchInRotatedSortedArray()
    {
        int[] num = new int[] {1};

        Console.WriteLine(SearchInRotatedSortedArray(num, 1, 0, num.Length-1));
    }

    private int SearchInRotatedSortedArray(int[] arr, int num, int start, int end)
    {
        int res = -1;

        if (start > end)
        {
            return -1;
        }

        if (end - start <= 1)
        {
            res = arr[start] == num ? start : arr[end] == num ? end : -1;
            return res;
        }

        int mid = (end-start)/2 + start;

        if ((arr[start] < arr[mid] && num >= arr[start] && num <= arr[mid]) || (arr[mid] < arr[end]) && (num < arr[mid] || num > arr[end]))
        {
            res = SearchInRotatedSortedArray(arr, num, start, mid);
        }
        else if ((arr[start] < arr[mid] && (num < arr[start] || num > arr[end])) || (arr[mid] <= arr[end] && num >= arr[mid] && num <= arr[end]))
        {
            res = SearchInRotatedSortedArray(arr, num, mid, end);
        }

        return res;
    }

    /*
     Asked by Google
    Given an array of numbers and an index i, return the index of the nearest larger number of the number at index i, where distance 
    is measured in array indices. For example, given [4, 1, 3, 5, 6] and index 0, you should return 3.If two distances to larger numbers
    are the equal, then return any one of them. If the array at i doesn't have a nearest larger integer, then return null.
    Follow-up: If you can preprocess the array, can you do this in constant time?
    */
    public void NextGreaterNumber()
    {
        int[] arr = new int[5] {4, 1, 3, 5, 6};
        Console.WriteLine(NextGreaterNumber(arr, 1));
    }

    private int NextGreaterNumber(int[] arr, int index)
    {
        int[] res = new int[arr.Length];
        for(int i = 0; i < res.Length; i++)
        {
            res[i] = -1;
        }

        Stack<int> stk = new Stack<int>();

        stk.Push(0);
        int idx = 1;

        while(idx < arr.Length)
        {
            if (arr[stk.Peek()] < arr[idx])
            {
                res[stk.Pop()] = idx;
            }

            stk.Push(idx);
            idx++;
        }

        return res[index];
    }

    //Accepted-LCEasy-LCSol-T:O(n):S:O(n) https://leetcode.com/problems/min-cost-climbing-stairs/solution/
    public void MinCostClimbingStairs()
    {
        int[] cost = new int[] {0,0,1,1};
        Console.WriteLine(MinCostClimbingStairs(cost));
    }

    private int MinCostClimbingStairsCre(int[] cost)
    {
        int[] dp = new int[cost.Length];
        dp[0] = cost[0];
        dp[1] = cost[1];

        for(int idx = 2; idx < cost.Length; idx++)
        {
            dp[idx] = Math.Min(cost[idx-1] + cost[idx], cost[idx-2] + cost[idx]);
        }

        return Math.Min(dp[cost.Length-1], dp[cost.Length-2]);
    }

    private int MinCostClimbingStairs(int[] cost) 
    {
        int len = cost.Length;
        int[] dp = new int[len];
        dp[0] = cost[0];
        dp[1] = cost[1];
        for (int i = 2; i < len; i++)
        {
            dp[i] = Math.Min(dp[i - 1]+ cost[i], dp[i - 2] + cost[i]);
        }

        return Math.Min(dp[len - 1], dp[len - 2]);
    }

    //https://leetcode.com/problems/friend-circles/
    public void FriendCircles()
    {
        int[][] M = new int[3][]
        {
            new int[] {1, 0, 1},
            new int[] {0, 0, 0},
            new int[] {1, 0, 1},
        };

        Console.WriteLine(FriendCircles(M));
    }

    public int FriendCircles(int[][] M)
    {
        int[] visited = new int[M.Length];
        int count = 0;
        for (int i = 0; i < M.Length; i++)
        {
            if (visited[i] == 0)
            {
                dfs(M, visited, i);
                count++;
            }
        }
        return count;
    }

    private void dfs(int[][] M, int[] visited, int i)
    {
        for (int j = 0; j < M.Length; j++)
        {
            if (M[i][j] == 1 && visited[j] == 0)
            {
                visited[j] = 1;
                dfs(M, visited, j);
            }
        }
    }

    //https://leetcode.com/problems/combination-sum-iv/
    public void CombinationSumIV()
    {
        int[] arr = new int[] {1,2,3};
        int target = 4;
        Console.WriteLine(CombinationSumIV(arr, target, 0, 0));
    }

    private int CombinationSumIV(int[] nums, int target, int sum, int idx)
    {
        if (sum == target)
        {
            return 1;
        }

        if (sum> target || idx >= nums.Length)
        {
            return 0;
        }

        int res = 0;
        for(int i = 0; i < nums.Length; i++)
        {
            res += CombinationSumIV(nums, target, sum+ nums[i], i);
        }

        return res;
    }

    //Accepted-LcMedium-SelfSol-T:O(n)-S:O(n) https://leetcode.com/problems/daily-temperatures/
    public void DailyTemperatures()
    {
        int[] arr = new int[]{73,74,75,71,69,72,76,73};
        var res = DailyTemperatures(arr);
    }

    private int[] DailyTemperatures(int[] arr) 
    {
        Stack<int> stk = new Stack<int>();
        stk.Push(arr.Length-1);
        
        int[] res = new int[arr.Length];
        
        for(int idx = arr.Length-2; idx >= 0; idx--)
        {
            while (stk.Count > 0 && arr[idx] >= arr[stk.Peek()])
            {
                stk.Pop();
            }
            
            if (stk.Count > 0 && arr[idx] < arr[stk.Peek()])
            {
                res[idx] = stk.Peek() - idx;
            }

            stk.Push(idx);
        }
        
        return res;
    }


    //Accepted-LcHard-LCSol-T:O(n^3)-S:O(n^2) https://leetcode.com/problems/minimum-cost-to-cut-a-stick/
    public void MinCostToCutStick()
    {
        int[] cuts = new int[] {1,3,4,5};
        int n = 7;

        var cutList = new List<int>();
        cutList.Add(0);
        cutList.Add(n);

        cutList.AddRange(cuts);
        cutList.Sort();
        Console.WriteLine(MinCostToCutStick(cutList, 0, cutList.Count-1, new int[102,102]));
    }

    private int MinCostToCutStick(List<int> cuts, int start, int end,  int[,] dp)
    {
        if (end-start == 1)
        {
            return 0;
        }

        if (dp[start, end] == 0)
        {
            dp[start, end] = int.MaxValue;

            for(int i = start+1; i < end; i++)
            {
                dp[start, end] = Math.Min(dp[start, end], MinCostToCutStick(cuts, start, i, dp) + 
                MinCostToCutStick(cuts, i, end, dp) + cuts[end] - cuts[start]);
            }
        }

        return dp[start, end];
    }

    //Accepted-LcMedium-SelfSol-T:O(n)-S:O(n) https://leetcode.com/problems/buildings-with-an-ocean-view/
    public void FindBuildings()
    {
        int[] nums = new int[]{2,2,2,2};
        Console.WriteLine(FindBuildings(nums));
    }

    public int[] FindBuildings(int[] heights) 
    {
        int[] largest = new int[heights.Length];
        
        for(int idx = heights.Length-1; idx >= 0 ;idx--)
        {
            largest[idx] = idx == heights.Length-1 ? heights[idx] : Math.Max(heights[idx], largest[idx+1]); 
            Console.WriteLine(idx);
        }
        
        List<int> res = new List<int>();
        
        for(int idx = 0; idx < heights.Length; idx++)
        {
            int next = idx + 1 < heights.Length ? largest[idx+1] : 0;
            
            if (heights[idx] > next)
            {
                res.Add(idx);
            }
        }
        
        return res.ToArray();
    }

    //https://leetcode.com/problems/continuous-subarray-sum/
    public void CheckSubarraySum()
    {
        int[] nums = new int[]{23,2,6,4,7};
        int k = 6;
        Console.WriteLine(CheckSubarraySum(nums, k));
    }

    private bool CheckSubarraySum(int[] nums, int k)
    {
        Dictionary<int, int> map = new Dictionary<int, int>();
        map.Add(0, -1);

        int runningSum = 0;
        for (int i=0; i < nums.Length; i++)
        {
            runningSum += nums[i];
            if (k != 0)
            {
                runningSum %= k;
            }

            if (map.ContainsKey(runningSum))
            {
                int prev = map[runningSum];
                if (i - prev > 1) 
                {
                    return true;
                }
            }
            else
            {
                map[runningSum] = i;
            }
        }
        
        return false;
    }

    //https://leetcode.com/problems/3sum-closest/
    public void ThreeSumClosest()
    {
        int[] nums = new int[]{1,1,-1,-1,3};
        Console.WriteLine(ThreeSumClosest(nums, 3));
    }

    private int ThreeSumClosest(int[] nums, int target)
    {
        int left = 0;
        int right = left+2;
        int sum = 0;

        sum = nums[0] + nums[1] + nums[2];
        int diff = int.MaxValue;
        int res = sum;

        while(right < nums.Length)
        {
            if (diff > Math.Abs(target - sum))
            {
                diff = Math.Abs(target - sum);
                res = sum;
            }

            if (right +1 >= nums.Length)
            {
                break;
            }

            sum-= nums[left++];
            sum+=nums[++right];
        }

        return res;
    }

    //https://leetcode.com/problems/expression-add-operators/
    public void ExpressionAddOperators()
    {
        string s = "105", exp = string.Empty;
        int target = 5;
        var res = new List<string>();
        ExpressionAddOperators(s, exp, 0, target, 0, res, 0);
        // Helper(res, string.Empty, s, target, 0, 0, 0);
    }

    private void ExpressionAddOperators(string num, string path, long val, int target, int idx, IList<string> res, long prev)
    {
        if (idx == num.Length)
        {
            if(val == target)
            {
                res.Add(path);
            }

            return;
        }

        for(int i = 1; i+idx <= num.Length; i++)
        {
            if(i != 1 && num[idx] == '0') 
            {
                break;
            }

            int cur = int.Parse(num.Substring(idx, i));

            if (path == string.Empty)
            {
                ExpressionAddOperators(num, path + "+" +cur, cur, target, idx+i, res, cur);
            }
            else
            {
                ExpressionAddOperators(num, path + "+" +cur, val + cur, target, idx+i, res, cur);
                ExpressionAddOperators(num, path + "-" +cur, val - cur, target, idx+i, res, -cur);
                ExpressionAddOperators(num, path + "*" +cur, val - prev + prev * cur, target, idx+i, res, prev * cur);
            }
        }
    }

    private void Helper(List<string> result, string path, string num, int target, int pos, long val, long carry)
    {
        if(pos == num.Length)
        {
            if(val == target)
                result.Add(path);
            return;
        }
        
        for(int i = 1; i+pos <= num.Length; i++)
        {
            if(num[pos] == '0' && i!=1)
                break;

            long n = long.Parse(num.Substring(pos, i));
            //int sbLength = sb.Length;

            if(path == string.Empty)
            {
                //sb.Append(n);
                Helper(result, path + "+" +n, num, target, pos+i, n, n);
                //sb.Length = sbLength;
            }
            else
            {
                //sb.Append("+"+n);
                Helper(result, path + "+" + n, num, target, pos+i, val+n, n);
                //sb.Length = sbLength;
                //sb.Append("-"+n);
                Helper(result, path + "-" + n, num, target, pos+i, val-n, -n);
                //sb.Length = sbLength;
                //sb.Append("*"+n);
                Helper(result, path + "*" + n, num, target, pos+i, val-carry + carry*n, carry*n);
                //sb.Length = sbLength;
            }
        }
    }

    //https://leetcode.com/problems/count-different-palindromic-subsequences/
    public void CountPalindromicSubsequences()
    {
        string s = "bccb";
        Dictionary<string, int> map = new Dictionary<string, int>();
        HashSet<string> set = new HashSet<string>();

        CountPalindromicSubsequences(s, 0, s[0].ToString(), map);
        Console.WriteLine(map.Count);
    }

    private void CountPalindromicSubsequences(string s, int idx, string str, Dictionary<string, int> map)
    {
        if (map.ContainsKey(str))
        {
            return;
        }
        
        for(int i = 1; i <= s.Length; i++)
        {
            if (idx+i <= s.Length)
            {
                CountPalindromicSubsequences(s, idx+i, s.Substring(idx, i), map);
            }

            //Skip
            if (idx+i+i <= s.Length)
            {
                var cur = s.Substring(0, idx+i-1);
                var res = cur + s.Substring(idx+i, i);
                CountPalindromicSubsequences(s, idx+i, res, map);
            }

            if (str.Length > 0 && !map.ContainsKey(str) && ValidPalindrome(str))
            {
                map.Add(str, 1);
            }
        }

        return;
    }

    private bool ValidPalindrome(string str)
    {
        int left = 0;
        int right = str.Length-1;

        while (left <= right)
        {
            if (str[left++] != str[right--])
            {
                return false;
            }
        }

        return true;
    }

    /*
    Google
    Given list of start time and duration, return the list of processes running parallely
    input: 100-112 102-120 105-150 107-108
    output: 107-108: 1 
            105-150: 2
            102-120: 2
            100-112: 1
    */
    public void FindParallelProcess()
    {

    }

    private Dictionary<int, List<int>> FindParallelProcess(List<ProcessData> ranges)
    {
        Heap<ProcessData> queue = new Heap<ProcessData>(true, (x,y)=> {return x.Start.CompareTo(y.Start);});

        return null;
    }

    public class ProcessData
    {
        public int Start;
        public int End;

        public int ProcessId;
    }

    /*
    Given a list of N triangles with integer side lengths, determine how many different triangles there are. Two triangles are considered to be the same if they can both be placed on the plane such that their vertices occupy exactly the same three points.
    arr is a list of structs/objects that each represent a single triangle with side lengths a, b, and c.
    It's guaranteed that all triplets of side lengths represent real triangles.
    All side lengths are in the range [1, 1,000,000,000]
    1 <= N <= 1,000,000
    Output
    Return the number of distinct triangles in the list.
    Example 1
    arr = [[2, 2, 3], [3, 2, 2], [2, 5, 6]]
    output = 2
    The first two triangles are the same, so there are only 2 distinct triangles.
    Example 2
    arr = [[8, 4, 6], [100, 101, 102], [84, 93, 173]]
    output = 3
    All of these triangles are distinct.
    Example 3
    arr = [[5, 8, 9], [5, 9, 8], [9, 5, 8], [9, 8, 5], [8, 9, 5], [8, 5, 9]]
    output = 1
    */
    public void CountingTriangles()
    {
        int[][] arr = new int[][]
        {
            new int[] {5, 8, 9},
            new int[] {5, 9, 8},
            new int[] {9, 5, 8},
            new int[] {9, 8, 5},
            new int[] {8, 9, 5},
            new int[] {8, 5, 9},
        };

        Console.WriteLine(CountDistinctTriangles(arr));
    }

    private int CountDistinctTriangles(int[][] arr) 
    {
        HashSet<string> map = new HashSet<string>();
        
        for(int idx = 0; idx < arr.Length; idx++)
        {
            var sortedArr = arr[idx];
            Array.Sort(sortedArr);
            var str = ConstructString(sortedArr);
            
            if (map.Count > 0 && map.Contains(str))
            {
                continue;
            }
            
            map.Add(str);
        }

        // Write your code here
        return map.Count();
    }
  
    private string ConstructString(int[] arr)
    {
        StringBuilder sb = new StringBuilder();
        
        for(int idx = 0; idx < arr.Length; idx++)
        {
            sb.Append(arr[idx].ToString());
        }
        
        return sb.ToString();
    }

    //Accepted-LCMedium-SelfSol-T:O(n) S:O(1) https://leetcode.com/problems/insert-into-a-sorted-circular-linked-list/submissions/
    public void InsertIntoSortedCircularLinkedList()
    {
        SLLNode node = new SLLNode(3);
        node.Next = new SLLNode(5);
        node.Next.Next = new SLLNode(1);
        var res = InsertIntoSortedCircularLinkedList(node, 0);
    }

    private SLLNode InsertIntoSortedCircularLinkedList(SLLNode node, int val)
    {
        SLLNode insertNode = new SLLNode(val);
        
        if (node == null)
        {
            insertNode.Next = insertNode;
            return insertNode;
        }
        
        SLLNode prev = node;
        SLLNode head = node;
        node = node.Next;
        
        while (node.Next != head.Next)
        {
            //between
            if ((node.Next.Value >= val && node.Value <= val) )
            {
                prev = node;
                break;
            }
            //smallest
            else if (node.Value >= node.Next.Value && node.Next.Value <= prev.Value && node.Next.Value >= val)
            {
                prev = node;
            }
            //largest
            else if (node.Value <= val && node.Next.Value < node.Value )
            {
                prev = node;
            }
            
            node = node.Next;
        }
        
        
        SLLNode next = prev.Next;
        prev.Next = insertNode;
        insertNode.Next = next;
        
        return head;
    }

    //https://leetcode.com/problems/partition-equal-subset-sum/
    public void PartitionIntoEqualSubsets()
    {
        int[] nums = new int[]{1,5,11,5};
        int target = 0;
        int sum = 0;
        
        foreach(int i in nums)
        {
            sum += i;
        }
        
        if (sum %2 != 0)
        {
            Console.WriteLine(false);
            return;
        }
        
        target = sum / 2;

        Console.WriteLine(PartitionIntoEqualSubsetsDp(nums));
        //Console.WriteLine(PartitionIntoEqualSubsets(nums, 0, 2, 0, target, new HashSet<int>()));
    }

    public bool PartitionIntoEqualSubsetsDp(int[] nums)
    {
        int sum = 0;
        
        foreach(int num in nums)
        {
            sum += num;
        }
        
        if ((sum & 1) == 1)
        {
            return false;
        }

        sum /= 2;

        int n = nums.Length;
        bool[,] dp = new bool[n+1,sum+1];

        dp[0,0] = true;
        
        for (int i = 1; i < n+1; i++)
        {
            dp[i,0] = true;
        }
        for (int j = 1; j < sum+1; j++)
        {
            dp[0,j] = false;
        }
        
        for (int i = 1; i < n+1; i++)
        {
            for (int j = 1; j < sum+1; j++)
            {
                dp[i,j] = dp[i-1,j];

                if (j >= nums[i-1])
                {
                    dp[i,j] = (dp[i,j] || dp[i-1,j-nums[i-1]]);
                }
            }
        }
   
        return dp[n,sum];
    }

    private bool PartitionIntoEqualSubsets(int[] nums, int idx, int k, int sum, int target, HashSet<int> visited)
    {
        if (k == 0)
        {
            return true;    
        }

        if (sum == target)
        {   
            return PartitionIntoEqualSubsets(nums, 0, k-1, 0, target, visited);
        }
        
        for(int i = idx; i < nums.Length; i++)
        {
            if (visited.Contains(i) || sum + nums[i] > target)
            {
                continue;
            }

            visited.Add(i);
            var res = PartitionIntoEqualSubsets(nums, i+1, k, sum+ nums[i], target, visited);
            if (res)
            {
                return true;    
            }

            visited.Remove(i);
        }
        
        return false;
    }

    //https://leetcode.com/problems/partition-to-k-equal-sum-subsets/
    public void CanPartitionKSubsets()
    {
        int[] arr = new int[] {4,3,2,3,5,2,1};
        int k = 4;
        int sum = 0;

        for(int idx = 0; idx < arr.Length; idx++)
        {
            sum += arr[idx];
        }

        Console.WriteLine(CanPartitionKSubsets(arr, k, 0, 0, sum/ k, new bool[arr.Length]));
    }

    private bool CanPartitionKSubsets(int[] arr, int k, int idx, int sum, int target, bool[] visited)
    {
        if (k == 0)
        {
            return true;
        }

        if (sum == target)
        {
            return CanPartitionKSubsets(arr, k-1, 0, 0, target, visited);
        }

        for(int i = idx; i < arr.Length; i ++)
        {
            if (visited[i] || sum + arr[i] > target || (i > 0 && arr[i] == arr[i-1]))
            {
                continue;
            }

            visited[i] = true;

            if (CanPartitionKSubsets(arr, k, i+1, sum+arr[i], target, visited))
            {
                return true;
            }

            visited[i] = false;
        }

        return false;
    }

    //Accepted-LCMedium-LCSol-T:O(n)-https://leetcode.com/problems/possible-bipartition/
    public void PossibleBipartition()
    {
        int[][] arr = new int[][]
        {
            new int[] {1,2},
            new int[] {3,4},
            new int[] {5,6},
            new int[] {6,7},
            new int[] {8,9},
            new int[] {7,8},
        };

        Console.WriteLine(PossibleBipartition(10, arr));
    }

    private bool PossibleBipartition(int N, int[][] dislikes)
    {
        int[] color = new int[N+1];
        Dictionary<int, List<int>> adj = new Dictionary<int, List<int>>();

        foreach(int[] arr in dislikes)
        {
            if (!adj.ContainsKey(arr[0]))
            {
                adj.Add(arr[0], new List<int>());
            }
            adj[arr[0]].Add(arr[1]);

            if (!adj.ContainsKey(arr[1]))
            {
                adj.Add(arr[1], new List<int>());
            }
            adj[arr[1]].Add(arr[0]);
        }

        for(int idx = 1; idx<= N; idx++)
        {
            if (color[idx] == 0)
            {
                color[idx] = 1;
                Queue<int> queue = new Queue<int>();
                queue.Enqueue(idx);

                while (queue.Count > 0)
                {
                    int cur = queue.Dequeue();
                    if (!adj.ContainsKey(cur))
                    {
                        break;
                    }
                    foreach(int neighbor in adj[cur])
                    {
                        if (color[neighbor] == 0)
                        {
                            color[neighbor] = color[cur] == 1 ? 2 : 1;
                            queue.Enqueue(neighbor);
                        }
                        else
                        {
                            if (color[neighbor] == color[cur])
                            {
                                return false;
                            }
                        }
                    }
                }
            }
        }

        return true;
    }

    //https://leetcode.com/problems/furthest-building-you-can-reach/
    public void FurthestBuilding()
    {
        int[] A = new int[] {4,12,2,7,3,18,20,3,19};
        int bricks = 10, ladders = 2;

        Console.WriteLine(FurthestBuilding(A, bricks, ladders));
    }

    private int FurthestBuilding(int[] A, int bricks, int ladders)
    {
        Heap<int> heap = new Heap<int>(true);

        for(int idx = 0; idx < A.Length-1; idx++)
        {
            int diff = A[idx+1] - A[idx];

            if (diff > 0)
            {
                heap.Push(diff);
            }

            if (heap.Count > ladders)
            {
                bricks -= heap.Pop();
            }

            if (bricks <= 0)
            {
                return idx;
            }
        }

        return A.Length-1;
    }

    //https://leetcode.com/problems/shortest-distance-from-all-buildings/
    public int ShortestDistanceFromAllBuildings()
    {
        int[][] grid = new int[][]
        {
            new int[] {1,0,2,0,1},
            new int[] {0,0,0,0,0},
            new int[] {0,0,1,0,0}
        };

        int min = int.MaxValue;
        int[,] dist = new int[grid.Length, grid[0].Length];
        int start = 1;

        for(int i = 0; i < grid.Length; i++)
        {
            for(int j = 0; j < grid[0].Length; j++)
            {
                if(grid[i][j] == 1)
                {
                    ShortestDistanceFromAllBuildings(grid, dist, i, j, --start, ref min);
                }
            }
        }

        return min == int.MaxValue ? -1 : min;
    }

    private void ShortestDistanceFromAllBuildings(int[][] grid, int[,] dist, int row, int col, int start, ref int min)
    {
        int[] delta = new int[]{ 0, 1, 0, -1, 0 };

        Queue<int[]> queue = new Queue<int[]>();
        queue.Enqueue(new int[]{row, col});

        int level = 0;
        min = int.MaxValue;

        while(queue.Count > 0)
        {
            int size = queue.Count;
            level++;
            for(int k = 0; k < size; k++)
            {
                int[] node = queue.Dequeue();
                for(int i = 1; i < delta.Length; i++)
                {
                    int newRow = node[0] + delta[i - 1];
                    int newCol = node[1] + delta[i];
                    
                    if (newRow >= 0 && newRow < grid.Length && newCol >= 0 && newCol < grid[0].Length && grid[newRow][newCol] == start)
                    {
                        queue.Enqueue(new int[]{ newRow, newCol });
                        dist[newRow,newCol] += level;
                        min = Math.Min(min, dist[newRow,newCol]);
                        grid[newRow][newCol]--;
                    }
                }
            }
        }
    }

    //LcHard:LcSol:T:O(): https://leetcode.com/problems/permutation-sequence/
    public void GetPermutation()
    {
        Console.WriteLine(GetPermutation(4, 9));
    }

    private string GetPermutation(int n, int k)
    {
        int[] factorials = new int[n+1];
        List<int> nums = new List<int>();

        factorials[0] = 1;
        for(int i = 1; i <= n; ++i)
        {
            // generate factorial system bases 0!, 1!, ..., (n - 1)!
            factorials[i] = factorials[i - 1] * i;
            // generate nums 1, 2, ..., n
            nums.Add(i);
        }

        // fit k in the interval 0 ... (n! - 1)
        k--;

        // compute factorial representation of k
        StringBuilder sb = new StringBuilder();
        for(int i = 1; i <= n; i++)
        {
            int index = k/factorials[n-i];
            sb.Append(nums[index]);
            nums.RemoveAt(index);
            k-= index * factorials[n-i];
        }

        return sb.ToString();
    }

    /*
    Asked by Netflix
    Given a sorted list of integers of length N, 
    determine if an element x is in the list without performing any multiplication, division, or bit-shift operations.
    */
    public void FindNumFromSortedArray()
    {
        int[] arr = new int[] {2, 3, 5, 9, 12};
        Console.WriteLine(FindNumFromSortedArray(arr, 2, 0, arr.Length-1));
    }

    private bool FindNumFromSortedArray(int[] arr, int x, int start, int end)
    {
        if (end - start <= 1)
        {
            return arr[end] == x || arr[start] == x;
        }

        int mid = (start + end ) /2;

        if (x > arr[mid])
        {
            return FindNumFromSortedArray(arr,x,  mid, end);
        }
        else
        {
            return FindNumFromSortedArray(arr, x, start, mid);
        }
    }

    //https://leetcode.com/problems/find-median-from-data-stream/
    public void MedianFromStream()
    {
        Heap<int> minHeap = new Heap<int>(true);
        Heap<int> maxHeap = new Heap<int>(false);

        int[] arr = new int[] {2, 4, 1, 5, 9, 12};

        foreach(int num in arr)
        {
            MedianFromStream(num, minHeap, maxHeap);
        }

        if (minHeap.Count == maxHeap.Count)
        {
            Console.WriteLine((minHeap.Pop() + maxHeap.Pop()) / 2);
        }
        else
        {
            if (minHeap.Count > maxHeap.Count)
            {
                Console.WriteLine(minHeap.Pop());
            }
            else
            {
                Console.WriteLine(maxHeap.Pop());
            }
        }
    }

    private void MedianFromStream(int num, Heap<int> minHeap, Heap<int> maxHeap)
    {
        if (minHeap.Count > 0 && num < minHeap.Peek())
        {
            maxHeap.Push(num);
        }
        else
        {
            minHeap.Push(num);
        }
        Rebalance(minHeap, maxHeap);
    }

    private void Rebalance(Heap<int> minHeap, Heap<int> maxHeap)
    {
        var bigger = maxHeap.Count >  minHeap.Count ? maxHeap : minHeap;
        var smaller = maxHeap.Count <  minHeap.Count ? maxHeap : minHeap;

        if (bigger.Count > smaller.Count)
        {
            smaller.Push(bigger.Pop());
        }
    }

    //https://leetcode.com/problems/median-of-two-sorted-arrays/
    public void FindMedianSortedArrays()
    {
        int[] arr1 = new int[] {2,4,6,9,10};
        int[] arr2 = new int[] {1, 4, 8, 11};
        var res = FindMedianSortedArrays(arr1, arr2);
        Console.WriteLine(res);
    }

    private double FindMedianSortedArraysCre(int[] nums1, int[] nums2)
    {
        if (nums1.Length > nums2.Length)
        {
            return FindMedianSortedArrays(nums2, nums1);
        }

        int x = nums1.Length;
        int y = nums2.Length;

        int lo = 0, hi = x;

        while (lo <= hi)
        {
            int partitionX = (lo + hi) / 2;
            int partitionY = (x+y+1)/2 - partitionX;

            int maxLeftX = partitionX == 0 ? int.MinValue : nums1[partitionX-1];
            int minRightX = partitionX == x ? int.MaxValue : nums1[partitionX];

            int maxLeftY = (partitionY == 0) ? int.MinValue : nums2[partitionY-1];
            int minRightY = (partitionY == y) ? int.MaxValue : nums2[partitionY];

            if (maxLeftX > minRightY)
            {
                hi = partitionX-1;
            }
            else if (maxLeftY > minRightX)
            {
                lo = partitionX+1;
            }
            else 
            {
                if ((x + y) % 2 == 0)
                {
                    return Math.Max(Math.Max(maxLeftX, maxLeftY) , Math.Max(minRightX, minRightY));
                }
                else
                {
                    return Math.Max(maxLeftX, maxLeftY);
                }
            }
        }

        return -1;
    }

    private double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        int N1 = nums1.Length;
        int N2 = nums2.Length;
        //                                         bigger, smaller
        if (N1 < N2) return FindMedianSortedArrays(nums2, nums1);	// Make sure A2 is the shorter one.
        
        int lo = 0, hi = N2 * 2;

        while (lo <= hi)
        {
            int mid2 = (lo + hi) / 2;   // Try Cut 2 
            int mid1 = N1 + N2 - mid2;  // Calculate Cut 1 accordingly
            
            double L1 = (mid1 == 0) ? int.MinValue : nums1[(mid1-1)/2];	// Get L1, R1, L2, R2 respectively
            double L2 = (mid2 == 0) ? int.MinValue : nums2[(mid2-1)/2];
            double R1 = (mid1 == N1 * 2) ? int.MaxValue : nums1[(mid1)/2];
            double R2 = (mid2 == N2 * 2) ? int.MaxValue : nums2[(mid2)/2];
            
            if (L1 > R2)
            {
                lo = mid2 + 1;	// A1's lower half is too big; need to move C1 left (C2 right)
            }
            else if (L2 > R1)
            {
                hi = mid2 - 1;	// A2's lower half too big; need to move C2 left.
            }
            else
            {
                return (Math.Max(L1,L2) + Math.Min(R1, R2)) / 2;	// Otherwise, that's the right cut.
            }
        }

        return -1;
    }
 
    //Accepted:LCMedium:selfSol: T:n! https://leetcode.com/problems/letter-combinations-of-a-phone-number/
    public void LetterCombinationsOfPhoneNumber()
    {
        Dictionary<int, List<char>> map = new Dictionary<int, List<char>>();
        List<char> list1 = new List<char>();
        list1.Add('a');
        list1.Add('b');
        list1.Add('c');

        List<char> list2 = new List<char>();
        list2.Add('d');
        list2.Add('e');
        list2.Add('f');

        List<char> list3 = new List<char>();
        list3.Add('g');
        list3.Add('h');
        list3.Add('i');

        List<char> list4 = new List<char>();
        list4.Add('j');
        list4.Add('k');
        list4.Add('l');

        List<char> list5 = new List<char>();
        list5.Add('m');
        list5.Add('n');
        list5.Add('o');

        List<char> list6 = new List<char>();
        list6.Add('p');
        list6.Add('q');
        list6.Add('r');
        list6.Add('s');

        List<char> list7 = new List<char>();
        list7.Add('t');
        list7.Add('u');
        list7.Add('v');

        List<char> list8 = new List<char>();
        list8.Add('w');
        list8.Add('x');
        list8.Add('y');
        list8.Add('z');


        map.Add(2, list1);
        map.Add(3, list2);
        map.Add(4, list3);
        map.Add(5, list4);
        map.Add(6, list5);
        map.Add(7, list6);
        map.Add(8, list7);
        map.Add(9, list8);

        IList<string> res = LetterCombinationsOfPhoneNoFIFO("234");
    }

    private IList<string> LetterCombinationsOfPhoneNoFIFO(string digits)
    {
        IList<String> ans = new List<String>();

		if(string.IsNullOrEmpty(digits))
        {
             return ans;
        }

        String[] mapping = new String[] {"0", "1", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz"};
        ans.Add("");

        while(ans[0].Length != digits.Length)
        {
            String remove = ans[0];
            ans.RemoveAt(0);
            String map = mapping[digits[remove.Length]-'0'];

            foreach(char c in map.ToCharArray())
            {
                ans.Add(remove+c);
            }
        }

        return ans;
    }

    //T:O(4n)
    private IList<string> LetterCombinationsOfPhoneNumberDFS(string digits, Dictionary<int, List<char>> map, int idx)
    {
        IList<string> res = new List<string>();

        if (idx < digits.Length)
        {
            int cur = int.Parse(digits[idx].ToString());

            idx++;

            foreach(char ch in map[cur])
            {
                var output = LetterCombinationsOfPhoneNumberDFS(digits, map, idx);

                if (output.Count == 0)
                {
                    res.Add(ch.ToString());
                }

                foreach(string str in output)
                {
                    res.Add(string.Concat(ch, str));
                }
            }
        }

        return res;
    }

    public void HasCircularLoop()
    {
        int[] arr = new int[]{-2,1,-1,-2,-2};
        Console.WriteLine(HasCircularLoop(arr));
    }

    private static bool HasCircularLoop(int[] arr)
    {
        int max = int.MinValue;
        int min = int.MaxValue;

        int idx = 0;
        for(; idx < arr.Length; idx ++)
        {
            max = Math.Max(arr[idx], max);
            min = Math.Min(arr[idx], min);
        }

        max +=1;
        min -=1;

        idx = 0;

        while (true)
        {
            if (arr[idx] > max)
            {
                //idx = (arr.Length-1) % (arr[idx] - max);
                if (arr[idx] > max)
                {
                    return true;
                }
            }

            if (arr[idx] < 0 && arr[idx] < min)
            {
                //idx = (arr.Length-1) % (arr[idx] + min);

                if (arr[idx] < min)
                {
                    return true;
                }
            }

            int newIdx = 0;
            if (idx + arr[idx] > arr.Length-1)
            {
                newIdx = ((arr.Length-1) % (idx + arr[idx]));
            }
            else if (idx + arr[idx] < 0)
            {
                newIdx = ((arr.Length-1) % (idx + arr[idx]));
            }
            else
            {
                newIdx = idx + arr[idx];
            }

            arr[idx] += arr[idx] > 0 ? max : min;
            idx = newIdx;
        }
    }

    //Accepted: T:O(n): https://leetcode.com/problems/asteroid-collision/
    public void AsteroidCollision()
    {
        int[] arr = new int[]{8, -8};
        var res = AsteroidCollision(arr);
    }

    private int[] AsteroidCollision(int[] arr)
    {
        int next = 1;
        Stack<int> stk = new Stack<int>();
        stk.Push(arr[0]);

        while (next < arr.Length)
        {
            while (next < arr.Length && stk.Count() > 0 && arr[next] < 0 && stk.Peek() > 0)
            {
                if (Math.Abs(stk.Peek()) > Math.Abs(arr[next]))
                {
                    next++;
                    continue;
                }

                int diff = stk.Peek() + arr[next];
                stk.Pop();

                if (diff == 0)
                {
                    next++;
                }
            }

            if (next < arr.Length)
            {
                stk.Push(arr[next]);
            }
            next++;
        }

        int[] res = new int[stk.Count()];
        int idx = stk.Count() - 1;

        while(stk.Count > 0)
        {
            res[idx--] = stk.Pop();
        }

        return res;
    }

    //https://leetcode.com/problems/remove-k-digits/
    public void RemoveKDigits()
    {
        Console.WriteLine(RemoveKDigits("1234567890",9));
    }

    private string RemoveKDigits(string arr, int num)
    {
        int k = num;
        if (string.IsNullOrEmpty(arr))
        {
            return arr;
        }

        Stack<int> stk = new Stack<int>();
        int idx = 0;
        StringBuilder sb = new StringBuilder();

        while (idx < arr.Length)
        {
            if (stk.Count > 0 && arr[idx] >= arr[stk.Peek()] && k > 0)
            {
                if ((idx+1 < arr.Length && arr[idx] > arr[idx + 1]) || idx +1 == arr.Length)
                {
                    k--;
                    idx++;
                    continue;
                }
            }
            else if (stk.Count > 0 && arr[idx] <= arr[stk.Peek()] && k > 0)
            {
                k--;
                stk.Pop();
            }

            stk.Push(idx);
            idx++;
        }

        var top = stk.Pop();
        while (k > 0 && stk.Count > 0)
        {
            if (arr[top] < arr[stk.Peek()])
            {
                stk.Pop();
            }
            else
            {
                top = stk.Pop();
            }

            k--;
        }

        if (arr.Length - num > 1)
        {
            stk.Push(top);
        }
        else if (arr.Length - num == 1)
        {
            if(stk.Count > 0)
            {
                 stk.Pop();
            }
            
            stk.Push(top);
        }

        if (stk.Count == 0)
        {
            return "0";
        }

        for(idx = arr.Length; idx >= 0; idx --)
        {
            if (stk.Count > 0 && idx == stk.Peek())
            {
                stk.Pop();
                sb.Insert(0, arr[idx]);
            }
        }

        return sb.Length > 1 && sb.ToString()[0] == '0' ? sb.Remove(0,1).ToString() :sb.ToString();
    }

    //Accepted T: O(nlogn) S:O(n): https://leetcode.com/problems/non-overlapping-intervals/
    public void NonOverlapingIntervals()
    {
        int[][] arr = new int[7][]
        {
            new int[]{0,2},
            new int[]{1,3},
            new int[]{1,3},
            new int[]{2,4},
            new int[]{3,5},
            new int[]{3,5},
            new int[]{4,6},
        };

        var res = NonOverlapingIntervals(arr);
    }

    private int NonOverlapingIntervals(int[][] arr)
    {
        Array.Sort(arr, new ArrayComparer());

        int curIdx = 0;
        int NextIdx = 1;
        int res = 0;
        Queue<int> queue = new Queue<int>();

        while(NextIdx < arr.Length)
        {
            while (NextIdx < arr.Length && arr[curIdx][1] <= arr[NextIdx][0])
            {
                NextIdx++;
                curIdx ++;

                while (queue.Count > 0 && curIdx == queue.Peek())
                {
                    queue.Dequeue();
                    curIdx ++;
                }
            }

            if (NextIdx < arr.Length)
            {
                queue.Enqueue(NextIdx);
                res++;
                NextIdx++;
            }
        }

        return res;
    }
}

public class Pair : IEquatable<Pair>
{
    public Pair()
    {

    }
    
    public Pair(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Pair(int x, int y, int level)
    {
        this.x = x;
        this.y = y;
        this.Level = level;
    }

    public bool Equals(Pair obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        return this.x == ((Pair)obj).x && this.y == ((Pair)obj).y;
    }
    
    public int x;
    public int y;

    public int Level;
}