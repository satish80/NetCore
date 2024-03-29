using System;
using System.Text;
using System.Collections.Generic;

public class Recursion
{
    //https://leetcode.com/articles/letter-combinations-of-a-phone-number/
    public void LetterCombinations()
    {
        Dictionary<char, string> map = new Dictionary<char, string>();
        map.Add('2', "abc");
        map.Add('3', "def");

        var output = LetterCombinations("23", 0, map, new List<string>(), string.Empty);
    }

    private List<string> LetterCombinations(string input, int idx, Dictionary<char, string> map, List<string> output, string cur)
    {
        if (input.Length == idx)
        {
            output.Add(cur);
            return output;
        }

        foreach(char ch in map[input[idx]])
        {
            LetterCombinations(input, idx + 1, map, output, cur + ch);
        }

        return output;
    }

    public void MaxRegion()
    {
        List<int> x = new List<int>();
        List<int> y = new List<int>();
        x.Add(-1);
        x.Add(1);
        x.Add(0);
        x.Add(0);
        x.Add(-1);
        x.Add(1);
        x.Add(1);
        x.Add(-1);

        y.Add(0);
        y.Add(0);
        y.Add(-1);
        y.Add(1);
        y.Add(-1);
        y.Add(1);
        y.Add(-1);
        y.Add(1);

        int sum = 0;
        int max = 0;

        int[][] grid = new int[][]
        {
            new int[]{1, 1, 0, 0},
            new int[]{0, 1, 1, 0},
            new int[]{0, 0, 1, 0},
            new int[]{1, 0, 0, 0}
        };

        for(int row = 0; row < grid.Length; row ++)
        {
            for(int col = 0; col < grid[row].Length; col ++)
            {
                if (grid[row][col] == 0)
                {
                    continue;
                }

                int res = MaxRegion(grid, x, y, row, col, sum);
                max = Math.Max(max, res);
            }
        }

        Console.WriteLine(max);
    }

    private int MaxRegion(int[][] grid, List<int> x, List<int> y, int r, int c, int sum)
    {
        if (grid[r][c] == 1)
        {
            sum += 1;
            grid[r][c] = 0;
        }

        for(int idx = 0; idx < 8; idx ++)
        {
            var row = x[idx] + r;
            var col = y[idx] + c;

            if (row < 0 || col <0 || row >= grid.Length || col >= grid[r].Length || grid[row][col] == 0)
            {
                continue;
            }

           sum = MaxRegion(grid, x, y, row, col, sum);
        }

        return sum;
    }

     /*
    You are presented with an 8 by 8 matrix representing the positions of pieces on a chess board. The only pieces on the board are the black king 
    and various white pieces. Given this matrix, determine whether the king is in check. For details on how each piece moves,
    see here https://en.wikipedia.org/wiki/Chess_piece#Moves_of_the_pieces .
    For example, given the following matrix:
    ...K....
    ........
    .B......
    ......P.
    .......R
    ..N.....
    ........
    .....Q..
    */
    public void KingCheck()
    {

    }

    private bool KingCheck(int i)
    {
        return true;
    }

    //https://leetcode.com/problems/kth-largest-element-in-an-array/
    public void KthLargest()
    {
        int[] arr = new int[]{3, 2, 11, 4, 8, 7};
        int k = 2;
        int n = arr.Length -1;
        Console.WriteLine(KthLargest(arr, 0, n, k));
    }

    private int KthLargest(int[] arr, int start, int end, int k)
    {
        int j = start;
        int pivot = start;

        while (j < end)
        {
            if (arr[j] <= arr[end])
            {
                Helpers.Swap(arr, pivot++, j);
            }

            j++;
        }

        Helpers.Swap(arr, pivot, end);

        int m = end - pivot + 1;
        if (m == k)
        {
            return arr[pivot];
        }

        if (m > k)
        {
            return KthLargest(arr, pivot+1, end, k);
        }
        else
        {
            return KthLargest(arr, start, pivot-1, k - m);
        }
    }

    //https://leetcode.com/problems/jump-game-iii/discuss/
    public void JumpGameIII()
    {
        int[] arr = new int[]{719,622,532,746,476,295,285,472,712,283,808,140,730,334,215,509,573,121,54,430,791,41,351,548,38,108,415,490,393,183,798,423,112,172,791,195,68,489,803,703,248,705,213,757,473,382,334,693,6,414,223,352,718,727,403,702,13,171,256,71,645,94,159,83,513,119,10,33,64,179,635,492,87,133,767,781,182,289,636,155,729,216,64,728,649,802,149,321,179,662,195,143,299,7,630,33,527,706,726,752,755,101,732,663,794,24,799,780,438,707,482,237,252,107,659,527,652,636,48,388,405,42,247,191,654,324,6,314,649,249,289,382,137,808,455,774,571,789,176,634,762,266,799,54,126,41,681,802,157,148,745,265,777,436,233,455,337,606,239,338,508,322,210,482,534,245,618,589,567,639,355,736,534,113,588,240,795,367,245,249,641,783,701,469,521,518,59,528,250,634,135,13,645,739,531,102,36,291,22,541,482,153,533,664,559,784,707,28,297,630,3,606,237,216,39,793,517,194,92,506,63,526,55,504,295,185,110,35,9,527,8,54,259,498,229,684,579,619,409,330,187,60,112,180,477,24,313,190,180,807,115,788,238,599,464,160,464,662,809,300,788,658,137,630,363,321,706,434,358,534,257,195,226,473,191,223,282,518,378,339,34,644,231,523,547,544,491,263,683,528,797,587,753,445,450,783,537,249,374,546,662,149,394,202,571,562,524,587,606,645,100,193,37,329,650,92,462,131,623,510,257,118,434,493,721,748,280,248,515,232,41,166,644,112,455,760,633,50,488,589,611,117,649,791,385,67,200,305,285,733,471,468,755,582,359,543,366,206,74,24,20,395,192,199,801,33,658,474,341,500,781,367,714,576,669,327,508,325,796,608,38,656,710,219,59,481,735,475,425,400,690,541,806,488,246,735,310,762,454,15,550,74,289,351,84,486,81,161,341,500,629,224,364,182,309,530,539,713,116,511,428,392,524,681,599,120,658,266,592,184,76,160,284,490,771,74,398,336,155,502,356,268,427,98,12,232,383,381,563,10,634,669,650,79,298,734,730,803,201,142,35,704,405,788,678,171,407,314,770,697,741,649,227,346,80,790,620,504,306,601,764,490,115,266,657,463,475,116,390,396,538,178,130,602,496,196,56,382,252,663,696,343,775,341,427,69,242,354,658,568,281,21,625,3,499,551,569,744,0,398,586,645,32,600,537,477,679,335,779,405,540,563,443,629,477,164,591,21,719,255,241,649,602,247,713,218,349,599,53,55,773,187,529,460,621,558,56,699,335,666,177,354,607,145,580,529,367,678,712,756,405,52,169,144,275,95,496,45,705,378,385,6,795,45,463,63,511,222,81,683,807,468,142,125,697,238,358,765,195,747,636,504,451,121,544,692,5,774,89,357,240,48,673,443,539,632,111,224,575,22,108,277,85,0,456,783,410,519,27,500,570,35,576,231,293,463,307,229,341,36,274,262,170,709,232,149,156,223,797,408,562,796,394,320,324,710,520,654,12,674,617,432,365,379,250,217,699,267,197,354,423,365,312,253,535,174,800,430,320,217,652,129,650,448,387,399,390,185,709,539,241,474,70,756,45,616,397,317,252,372,48,306,21,554,725,186,422,717,392,683,810,752,369,421,537,13,736,144,79,536,177,648,231,788,677,428,446,571,162,562,147,100,416,723,49,731,727,625,644,43,688,0,399,8,174,281,137,751,116,256,452,427,129,602,277,169,213,108,595,803,412,727,81,648,334,433,659,519,273,103,184,758,652,776,605,385,603,318,577,185,584,491,684,251,604,560,567,515,112,256,446,116,741,801,338,581,571,465,78,403,10,566,264,247,43,808,123,267,271,180,666};

        Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();

        for(int idx =0; idx < arr.Length; idx ++)
        {
            if (!map.ContainsKey(idx))
            {
                map.Add(idx, new List<int>());
            }

            if (idx+arr[idx] < arr.Length)
            {
                map[idx].Add(idx + arr[idx]);
            }

            if (arr[idx] <= idx)
            {
                map[idx].Add(idx - arr[idx]);
            }
        }

        Console.WriteLine(JumpGameIII(arr, map, new bool[arr.Length], 734));
    }

    private bool JumpGameIII(int[] arr, Dictionary<int, List<int>> map, bool[] visited, int idx)
    {
        if(arr[idx] == 0)
        {
            return true;
        }

        foreach(int nextIdx in map[idx])
        {
            if (visited[nextIdx])
            {
                return false;
            }

            visited[nextIdx] = true;

            if (JumpGameIII(arr, map, visited, nextIdx))
            {
                return true;
            }
        }

        return false;
    }

    //Accepted:LcMedium:SelfSol-T:O(4n):S:O(n^2) https://leetcode.com/problems/word-search/
    public void WordBoggle()
    {
        // char[][] board = new char[3][]
        // {
        //     new char[]{'A', 'B', 'C', 'E'},
        //     new char[]{'S', 'F', 'C', 'S'},
        //     new char[]{'A', 'D', 'E', 'E'},
        // };

        char[][] board = new char[1][]
        {
            new char[]{'a', 'b'}
        };

        bool[,] visited = new bool[board.Length, board[0].Length];

        int[][] arr = new int[2][]
        {
            new int[]{-1, 1, 0, 0},
            new int[] {0, 0, -1, 1}
        };

        string input = "ba";
        bool res = false;
        for(int i = 0; i < board.Length; i ++)
        {
            for(int j = 0; j < board[i].Length; j ++)
            {
                res = WordBoggle(board, input, visited, i, j, string.Empty, arr);
                if (res)
                {
                    break;
                }
            }
        }

        Console.WriteLine($"Word contained: {res}");
    }

    private bool WordBoggle(char[][] boggle, string input, bool[,] visited, int row, int col,
     string str, int[][] arr)
    {
        if (input == str)
        {
           return true;
        }

        if(row < 0 || col < 0 || row >= boggle.Length || col >= boggle[0].Length || visited[row, col] || str.Length == input.Length)
        {
            return false;
        }

        bool res = false;
        visited[row,col] = true;

        str += boggle[row][col];
        for(int j = 0; j < arr[0].Length; j++)
        {
            int r = arr[0][j] + row;
            int c = arr[1][j] + col;

            if (r == row && c == col)
            {
                continue;
            }

            res = WordBoggle(boggle, input, visited, r, c, str, arr);
            if (res)
            {
                return res;
            }
        }

        visited[row,col] = false;

        return res;
    }

    //https://leetcode.com/discuss/interview-question/267985/
    public void WordDice()
    {

    }

    // private bool WordDice(string str, int idx, char ch, Dictionary<char, List<int>> letToRow, Dictionary<int, List<char>> rowToLet, 
    // HashSet<char> visited)
    // {
    //     if (idx >= str.Length)
    //     {
    //         return false;
    //     }

    //     foreach(int row in letToRow[ch])
    //     {
    //         foreach(char let in rowToLet[row])
    //         {
    //             if (visited.Contains(let))
    //             {
    //                 continue;
    //             }
    //         }
    //     }
    // }
}
