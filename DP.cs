using System;
using System.Collections.Generic;

public class DP
{
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

    public int countSubstrings(String s) 
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

        Console.WriteLine(WordBreak("applespenapt", map));

        //Console.WriteLine(WordBreak("catsandog", map));
    }

    private bool WordBreak(string s, HashSet<string> dict)
    {
        bool[] f = new bool[s.Length + 1];
        
        f[0] = true;
        
        
        for(int i=1; i <= s.Length; i++){
            for(int j=0; j < i; j++){
                if(f[j] && j+i < s.Length && dict.Contains(s.Substring(j, i))){
                    f[i] = true;
                    break;
                }
            }
        }
        
        return f[s.Length];
    }
}