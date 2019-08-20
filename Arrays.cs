using System;
using System.Linq;
using System.Collections.Generic;

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