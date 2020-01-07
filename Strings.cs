
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

    //https://leetcode.com/problems/reorder-data-in-log-files/
    public void ReorderLogFiles()
    {
        string[] logs = new string[]
        {
            "l5sh 6 3869 08 1295",
            "16o 94884717383724 9",
            "43 490972281212 3 51",
            "9 ehyjki ngcoobi mi",
            "2epy 85881033085988",
            "7z fqkbxxqfks f y dg",
            "9h4p 5 791738 954209",
            "p i hz uubk id s m l",
            "wd lfqgmu pvklkdp u",
            "m4jl 225084707500464",
            "6np2 bqrrqt q vtap h",
            "e mpgfn bfkylg zewmg",
            "ttzoz 035658365825 9",
            "k5pkn 88312912782538",
            "ry9 8231674347096 00",
            "w 831 74626 07 353 9",
            "bxao armngjllmvqwn q",
            "0uoj 9 8896814034171",
            "0 81650258784962331",
            "t3df gjjn nxbrryos b"
        };

        logs = ReorderLogFiles(logs);
    }

    private string[] ReorderLogFiles(string[] logs)
    {
        Array.Sort(logs, new CustomComparer());
        return logs;
    }

    public class CustomComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            bool isDigit1 = char.IsDigit(x[x.Length-1]);
            bool isDigit2 = char.IsDigit(y[y.Length-1]);

            if (! isDigit1 && ! isDigit2)
            {
                var split1 = x.Split(" ");
                var split2 = y.Split(" ");

                int idx = 1;
                int cmp = 0;

                while (idx < split1.Length && idx < split2.Length)
                {
                    cmp = split1[idx].CompareTo(split2[idx]);
                    if (cmp != 0)
                    {
                        return cmp;
                    }

                    idx ++;
                }

                return split1[0].CompareTo(split2[0]);
            }

            return isDigit1 ? (isDigit2 ? 0 : 1) : -1;
        }

        private string Identifier(string str)
        {
            int idx = str.Length-1;

            while(char.IsDigit(str[idx]))
            {
                idx --;
            }

            return str.Substring(0, idx);
        }
    }

    //https://leetcode.com/problems/minimum-window-substring/
    public void MinWindowsSubstring()
    {
        Console.WriteLine(MinWindowsSubstring("acbbaca", "aba"));
    }

    private string MinWindowsSubstring(string S, string T)
    {
        Dictionary<char, List<int>> map = new Dictionary<char, List<int>>();
        Dictionary<char, int> tMap = new Dictionary<char, int>();

        foreach(char c in T)
        {
            if (!tMap.ContainsKey(c))
            {
                tMap.Add(c, 0);
            }

            tMap[c]++;
        }

        int max = 0;
        int min = 0;
        int length = int.MaxValue;
        int count = 0;
        Queue<int> idxQueue = new Queue<int>();

        for(int idx = 0; idx < S.Length; idx++)
        {
            if (tMap.ContainsKey(S[idx]))
            {
                if (!map.ContainsKey(S[idx]))
                {
                    map.Add(S[idx], new List<int>());
                }
                else
                {
                    if (idxQueue.Count > 0 && map[S[idx]]!= null && idxQueue.Peek() == map[S[idx]][0] 
                    && map[S[idx]].Count >= tMap[S[idx]])
                    {
                        idxQueue.Dequeue();
                        if (idxQueue.Count > 0)
                        {
                            min = idxQueue.Peek();
                        }

                        map[S[idx]].RemoveAt(0);
                        count--;
                    }
                }

                map[S[idx]].Add(idx);

                if (map[S[idx]].Count <= tMap[S[idx]])
                {
                    count++;
                }

                max = Math.Max(idx, max);
                idxQueue.Enqueue(idx);
            }

            if (count == T.Length)
            {
                if (max - idxQueue.Peek() < length)
                {
                    length = max - idxQueue.Peek()+1;
                    min = idxQueue.Peek();
                }

                if (map[S[idxQueue.Peek()]].Count > 1)
                {
                    map[S[idxQueue.Dequeue()]].RemoveAt(0);
                }
                else
                {
                    map.Remove(S[idxQueue.Dequeue()]);
                }
                count--;
            }
        }

        //min = map[S[min]].Count > tMap[S[min]] ? map[S[min]][1]
       return length < int.MaxValue && min + length >= T.Length ? S.Substring(min, length) : string.Empty;
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

    public void MakeAnagram()
    {
        Console.WriteLine(MakeAnagram("fcrxzwscanmligyxyvym", "jxwtrhvujlmrpdoqbisbwhmgpmeoke"));
    }

    private int MakeAnagram(string a, string b) 
    {
        Dictionary<char, int> aSet = new Dictionary<char, int>();
        string big = b.Length >= a.Length ? b : a;
        string small = a.Length <= b.Length ? a : b;

        foreach(char ch in big)
        {
            if (!aSet.ContainsKey(ch))
            {
                aSet.Add(ch, 0);
            }

            aSet[ch] ++;
        }

        int count = 0;

        foreach(char ch in small)
        {
            if(aSet.ContainsKey(ch) && aSet[ch] > 0)
            {
                count ++;
                aSet[ch] -=1;
            }
        }

        return a.Length + b.Length - count*2;
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

    public void SameCharacterFrequency()
    {
        Console.WriteLine(SameCharacterFrequency("aeeffbbbccc"));
    }

    private string SameCharacterFrequency(string s) 
    {
        Dictionary<char,int> map = new Dictionary<char,int>();
        Dictionary<int, int> countMap = new Dictionary<int, int>();

        foreach(char ch in s)
        {
            if (!map.ContainsKey(ch))
            {
                map.Add(ch, 0);
            }

            map[ch] = map[ch] + 1;

            if (map[ch] > 1)
            {
                countMap[map[ch]-1] -=1;

                if (countMap[map[ch]-1] == 0)
                {
                    countMap.Remove(map[ch]-1);
                }
            }

            if (!countMap.ContainsKey(map[ch]))
            {
                countMap.Add(map[ch], 0);
            }

            countMap[map[ch]] += 1;
        }

        if (countMap.Count == 1)
        {
            return "YES";
        }

        if (countMap.Count == 2)
        {
            foreach(KeyValuePair<int, int> pair in countMap)
            {
                if (pair.Value == 1)
                {
                    return "YES";
                }
            }
        }

        return "NO";
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

    //https://leetcode.com/problems/longest-word-in-dictionary-through-deleting/
    public void LongestWordInDictionary()
    {
        HashSet<string> dictionary = new HashSet<string>();
        dictionary.Add("a");
        dictionary.Add("b");
        dictionary.Add("c");

        Console.WriteLine(LongestWordInDictionary("abpclplea", dictionary));
    }

    private string LongestWordInDictionary(string str, HashSet<string> dictionary)
    {
        string longest = string.Empty;

        foreach(string word in dictionary)
        {
            int count = 0;
            int idx = 0;
            for(int i = 0; i < str.Length; i++)
            {
                if (idx == word.Length)
                {
                    break;
                }

                if (str[i] == word[idx])
                {
                    count++;
                    idx++;
                }
            }

            if (count == word.Length && word.Length >= longest.Length)
            {
                if (word.Length > longest.Length || word.CompareTo(longest) < 0)
                {
                    longest = word;
                }
            }
        }

        return longest;
    }

    //https://leetcode.com/problems/decode-string/
    public void DecodeString() 
    {
        //string str = "3[a]2[bc]";
        //string str = "3[a2[c]]";
        // string str = 2[abc]3[cd]ef";
        //Console.WriteLine(DecodeString(str, ref start));
        Console.WriteLine(DecodeString("100[leetcode]"));
    }

    #region commented
    /*
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

    */

    #endregion commented

    private string DecodeString(string str)
    {
        int idx = 0;
        Stack<int> countStk = new Stack<int>();
        Stack<string> strStack = new Stack<string>();
        StringBuilder res = new StringBuilder();

        while (idx < str.Length)
        {
            if (char.IsDigit(str[idx]))
            {
                int num = int.Parse(str[idx++].ToString());
                int product = 10;
                while(idx < str.Length && char.IsDigit(str[idx]))
                {
                    num = (num *product) + int.Parse(str[idx].ToString());
                    idx++;
                }

                countStk.Push(num);
            }
            else if (str[idx] == '[')
            {
                idx ++;
            }
            else if (str[idx] == ']')
            {
                var sb = RepeatString(countStk, strStack);

                if (strStack.Count > 0)
                {
                    var sbStk = new StringBuilder(strStack.Pop());
                    sbStk.Append(sb.ToString());
                    strStack.Push(sbStk.ToString());
                }
                else
                {
                    res.Append(sb.ToString());
                }

                idx++;
            }
            else
            {
                if (countStk.Count == strStack.Count)
                {
                    countStk.Push(1);
                }

                StringBuilder cur = new StringBuilder();
                while(idx < str.Length && char.IsLetter(str[idx]))
                {
                    cur.Append(str[idx++]);
                }

                strStack.Push(cur.ToString());
            }
        }

        if (countStk.Count > 0 && strStack.Count > 0)
        {
            res.Append(RepeatString(countStk, strStack));
        }

        return res.ToString();
    }

    private string RepeatString(Stack<int> countStk, Stack<string> strStk)
    {
        int i = 0;
        int times = countStk.Pop();
        var s = strStk.Pop();
        StringBuilder sb = new StringBuilder();

        while (i < times)
        {
            sb.Append(s);
            i++;
        }

        return sb.ToString();
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