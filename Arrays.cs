using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

public class Arrays
{
    //https://leetcode.com/problems/partition-labels/
    public void PartitionStringWithUniqueChars()
    {
        Console.WriteLine(PartitionStringWithUniqueChars("abacbdefedghiho"));
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

    //https://leetcode.com/problems/merge-intervals/
    public void MergeIntervals()
    {

    }

    private int[][] MergeIntervals(int[][] intervals)
    {
        return null;
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

    public void DailyTemperatures()
    {
        int[] arr = new int[] {6, 5, 1, 10, 8, 0, 2};
        var res = DailyTemperatures(arr);
    }

    public int[] DailyTemperatures(int[] temperatures) 
    {
        Stack<int> stk = new Stack<int>();
        int[] ret = new int[temperatures.Length];
        for(int i = 0; i < temperatures.Length; i++) 
        {
            while(stk.Count > 0 && temperatures[i] > temperatures[stk.Peek()]) 
            {
                int idx = stk.Pop();
                ret[idx] = i - idx;
            }
            stk.Push(i);
        }
        return ret;
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

    //https://leetcode.com/problems/game-of-life/discuss/73366/Clean-O(1)-space-O(mn)-time-Java-Solution
    public void GameOfLife()
    {
        List<Pair> pairs = new List<Pair>();
        pairs.Add(new Pair(-1,0));
        pairs.Add(new Pair(1,0));
        pairs.Add(new Pair(0,-1));
        pairs.Add(new Pair(0,1));
        pairs.Add(new Pair(-1,-1));
        pairs.Add(new Pair(1,1));
        pairs.Add(new Pair(-1,1));
        pairs.Add(new Pair(1,-1));

        GameOfLife();
    }

    private void GameOfLife(int[,] arr, List<Pair> pairs)
    {
        
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

    //https://leetcode.com/problems/decode-ways/
    public void DecodeWays()
    {
        Console.WriteLine(DecodeWays("110"));
    }

    private int DecodeWays(string s)
    {
        if (string.IsNullOrEmpty(s) || s[0] == '0')
        {
            return 0;
        }

        int[] res = new int[s.Length];
        res[0] = 1;

        for (int idx = 1; idx < s.Length; idx ++)
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

        return res[s.Length-1];
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

        for(int row = 0; row < stones.Length; row ++)
        {
            for(int col = row + 1; col < stones.Length; col ++)
            {
                if (stones[row][0] == stones[col][0] || stones[row][1] == stones[col][1])
                {
                    d.Union(row, col);
                }
            }
        }

        return stones.Length - d.Count;
    }

    public class DSU
    {
        private int[] size;
        private int[] root;
        public int Count;

        public DSU(int n)
        {
            size = new int[n];
            root = new int[n];
            Count = n;

            for(int i = 0; i < n; i++)
            {
                root[i] = i;
            }
        }

        public int Find(int x)
        {
            if (root[x] != x)
            {
                root[x] = Find(root[x]);
            }

            return root[x];
        }

        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            if (rootX == rootY)
            {
                return;
            }

            if (size[rootX] <= size[rootY])
            {
                root[rootX] = rootY;
                size[rootY]++;
            }
            else
            {
                root[rootY] = rootX;
                size[rootX]++;
            }

            Count--;
        }
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

    //Accepted: https://leetcode.com/problems/next-permutation/
    public void NextPermutation()
    {
        int[] arr = new int[]{1,5,1};
        NextPermutation(arr);
    }

    private void NextPermutation(int[] arr)
    {
        int idx = arr.Length -2;

        while (idx >= 0 && arr[idx] >= arr[idx+1])
        {
            idx--;
        }

        if (idx >= 0)
        {
            int i = arr.Length-1;
            while(i > idx && arr[idx] >= arr[i]) 
            {
                i--;
            }

            Swap(arr, i, idx);
        }

        ReverseArray(arr, idx+1);
    }

    private void Swap(int[] arr, int source, int target)
    {
        int temp = arr[source];
        arr[source] = arr[target];
        arr[target] = temp;
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

    //https://leetcode.com/problems/min-cost-climbing-stairs/solution/
    public void MinCostClimbingStairs()
    {
        int[] cost = new int[] {0,0,1,1};
        Console.WriteLine(MinCostClimbingStairs(cost));
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