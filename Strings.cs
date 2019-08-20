
using System;
using System.Text;
using System.Collections.Generic;

public class Strings
{
    public void StrStr()
    {
        Console.WriteLine(StrStr("Hello", "ll"));
    }

    private int StrStr(string HayStack, string Needle)
    {
        Dictionary<char, int> map = new Dictionary<char, int>();

        // 'Hello'
        for(int i = 0; i < Needle.Length-1; i ++)
        {
            if (!map.ContainsKey(Needle[i]))
            {
                map.Add(Needle[i], Needle.Length - i - 1);
            }
            else
            {
                map[Needle[i]] = Needle.Length - i - 1;
            }
        }

        int idx = Needle.Length - 1;
        int cur = Needle.Length - 1;

        while (cur < HayStack.Length)
        {
            idx = Needle.Length -1;

            while(cur >-1 && idx > -1 && HayStack[cur] == Needle[idx])
            {
                cur--;
                idx--;
            }

            if (idx < 0)
            {
                return cur+1;
            }

            if (map.ContainsKey(HayStack[cur]))
            {
                cur+= map[HayStack[cur]];
            }
            else
            {
                cur += Needle.Length;
            }
        }

        return -1;
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

        //Console.WriteLine(WordBreak("applespenapt", map));

        Console.WriteLine(WordBreak("catsandog", map));
    }

    private bool WordBreak(string str, HashSet<string> map)
    {
        if (string.IsNullOrEmpty(str))
        {
            return false;
        }

        Stack<int> stk = new Stack<int>();

        int idx = 0;
        int l = 1;

        while (idx < str.Length)
        {
            while(idx + l <=str.Length)
            {
                if (map.Contains(str.Substring(idx, l)))
                {
                    idx += l-1;
                    stk.Push(idx);

                    if (stk.Peek() == str.Length-1)
                    {
                        return true;
                    }

                    idx ++;
                    l = 1;
                }
                else
                {
                    l +=1;
                }
            }

            if (stk.Count == 0)
            {
                return false;
            }

            l = stk.Pop();
            idx = stk.Count > 0 ? stk.Peek() + 1 : 0;
            l = l - idx + 2;
        }

        return false;
    }

    public void checkPangram()
    {
        List<string> strings = new List<string>();
        strings.Add("we promptly judged antique ivory buckets for the next prize");

        if (strings == null || strings.Count == 0) 
        {
            Console.WriteLine("0");
        }

        HashSet<int> map = null;

        StringBuilder sb = new StringBuilder();

        foreach(string str in strings)
        {
            map = new HashSet<int>();
            sb.Append(CheckPangram(map, str));
        }

        Console.WriteLine(sb.ToString());
    }

    private string CheckPangram(HashSet<int> map, string str)
    {
        foreach(char ch in str)
        {
            if (!map.Contains(ch))
            {
                Console.WriteLine($"ch: {ch} being added for string: {str}");
                map.Add(ch);
            }
        }

        for(int idx = 97; idx <122; idx++)
        {
            if (!map.Contains(idx))
            {
                return "0";
            }
        }

        return "1";
    }

    //Accepted: https://leetcode.com/contest/weekly-contest-149/problems/swap-for-longest-repeated-character-substring/
    public void SwapForLongestRepeatedChar()
    {
        string arr = "aabaaabaaaba";
        Console.WriteLine(SwapForLongestRepeatedChar(arr));
    }

    private int SwapForLongestRepeatedChar(string arr)
    {
        Dictionary<char, int> map = new Dictionary<char, int>();

        for(int idx = 0; idx < arr.Length; idx ++)
        {
            if (!map.ContainsKey(arr[idx]))
            {
                map.Add(arr[idx], 0);
            }

            map[arr[idx]]++;
        }

        int count = 1;
        int i = 0;
        int max = 1;

        while (i < arr.Length)
        {
            count = 0;
            var maxCount = map[arr[i]];
            if (maxCount > 1)
            {
                var cur = i;

                count = CountOccurence(arr, i, ref cur);

                if (count > 0)
                {
                    i = cur;
                }

                cur +=2;

                if (cur < arr.Length && arr[cur] == arr[i])
                {
                    var res = CountOccurence(arr, i, ref cur);
                    if (res > 0)
                    {
                        count += res+1;
                    }
                }
                else if(cur < arr.Length)
                {
                    count +=1;
                }

                count = count > maxCount ? maxCount : count;
                max = Math.Max(max, count);
            }

            i++;
        }

        return max;
    }

    private int CountOccurence(string arr, int i, ref int cur)
    {
        int count = 0;

        while (cur < arr.Length && arr[cur] == arr[i])
        {
            count ++;
            cur ++;
        }

        cur --;
        return count;
    }
}