
using System;
using System.Text;
using System.Collections.Generic;

public class Strings
{
    //https://leetcode.com/problems/implement-strstr/
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

    public void IsPalindrome()
    {
        Console.WriteLine(IsPalindrome("amrtmma"));
    }
    private bool IsPalindrome(string str)
    {
        int left = 0;
        int right = str.Length-1;
        
        int count = 0;
        
        while (right-left >= 1)
        {
            Console.WriteLine($"left: {str[left]} right: {str[right]}");
            if (str[left] != str[right])
            {
                if (count < 1)
                {
                    if (left < right && str[left+1] == str[right])
                    {
                        left ++;
                    }
                    else if (left < right && str[left] == str[right-1])
                    {
                        right--;
                    }
                    else
                    {
                        return false;
                    }
                    
                    count++;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                left ++;
                right --;
            }
        }
        
        return true;
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

    //Accepted: https://leetcode.com/problems/longest-substring-with-at-most-k-distinct-characters/
    public void LongestSubstringKDistinctChars()
    {
        Console.WriteLine(LongestSubstringKDistinctChars("ee", 1));
    }

    private int LongestSubstringKDistinctChars(string str, int k)
    {
        if (k == 0)
        {
            return 0;
        }

        Dictionary<char, int> map = new Dictionary<char, int>();

        int start = 0;
        int cur = 0;
        int max = 0;

        while (cur < str.Length)
        {
            while ((cur < str.Length) && (map.Count < k || map.ContainsKey(str[cur])))
            {
                if (!map.ContainsKey(str[cur]))
                {
                    map.Add(str[cur], 0);
                }

                map[str[cur]]++;
                cur++;
            }

            max = Math.Max(max, cur - start );
            map[str[start]]--;

            if (map[str[start]] == 0)
            {
                map.Remove(str[start]);
            }

            start++;
        }

        return max;
    }

    //https://leetcode.com/problems/decode-string/
    public void DecodeString() 
    {
        int start = 0;
        string str = "3[a]2[bc]";
        //string str = "3[a2[c]]";
        Console.WriteLine(DecodeString(str, ref start));
    }

    private string DecodeString(string s, ref int start)
     {
        int idx = 0;
        Stack<char> stk = new Stack<char>();
        StringBuilder sb = new StringBuilder();

        while(idx < s.Length)
        {
                if (s[idx] == ']')
                {
                    var temp = new StringBuilder();

                    sb = Construct(stk, sb);
                    stk.Pop();
                }
                else
                {
                    stk.Push(s[idx]);
                }

            idx ++;
        }

        if (stk.Count > 0)
        {
            sb = Construct(stk, sb);
        }

        return sb.ToString();
    }

private StringBuilder Construct(Stack<char> stk, StringBuilder sb)
{
    var temp = new StringBuilder();
    while(stk.Count > 0 && stk.Peek() != '[')
    {
        if (stk.Peek() > 96 && stk.Peek() < 123) // letters
        {
            temp = new StringBuilder();

            while (stk.Peek() > 96 && stk.Peek() < 123)
            {
                var str = new StringBuilder(stk.Pop().ToString());
                temp = str.Append(temp);
            }

            sb = temp.Append(sb);
        }
        else if(stk.Peek() > 47 && stk.Peek() < 58) // Numbers
        {
            temp = new StringBuilder();
            if (stk.Peek() > 47 && stk.Peek() < 58)
            {
                int.TryParse(stk.Pop().ToString(), out int val);
                sb = Repeat(sb, val);
            }
        }
    }

    return sb;
}
    private StringBuilder Repeat(StringBuilder sb, int times)
    {
        var res = new StringBuilder();

        for(int idx = 0; idx < times; idx ++)
        {
            res.Append(sb);
        }
        
        return res;
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

    // Given string abc, print sequence such as 
    // a, b, c, aa, ab, ac, ba, bb, bc, ca, cb, cc, aaa, aab,..
    public void GenerateSequence()
    {

    }

    private int NextSequence(Dictionary<int, char> map, int curSequence, ref int length)
    {
        int len = length;
        Stack<int> stk = new Stack<int>();
        int num = 0;
        while (len > 0)
        {
            num = curSequence/len;
            stk.Push(num);
            curSequence -= num * length;
            len /= 10;
        }

        num = 0;

        var item = stk.Pop();
        int product = 10;

        while (item + 1 % 3 == 1)
        {
            num = product * item;
            product *= 10;
            item = stk.Pop();
        }

        while (stk.Count > 0)
        {
            num = product * stk.Pop() + item;
            product *= 10;
        }

        return num;
    }
}