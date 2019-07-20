using System;

public class DP
{
    // Return the palindromic sub strings for a given string
    public void FindPalindromeSubstrings()
    {
        Console.WriteLine(FindPalindromeSubstrings("abbacfgfr"));
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
                    var r = row + 1;
                    var c = col +1;

                    bool found = false;

                    while(r < input.Length && c < input.Length && dp[r,c] == 1)
                    {
                        found = true;
                        dp[r++, c++] = 0;
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
}