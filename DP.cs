using System;

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

}