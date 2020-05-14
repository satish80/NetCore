
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

    //Accepted: T:O(n), S:O(k) :https://leetcode.com/problems/minimum-window-substring/
    public void MinWindowsSubstring()
    {
        //Console.WriteLine(MinWindowsSubstring("acbbaca", "aba"));
        Console.WriteLine(MinWindowsSubstring("ADOBECODEBANC", "ABC"));
    }

    private string MinWindowsSubstring(string s, string t)
    {
        int left = 0;
        int right = 0;

        Dictionary<char, int> map = new Dictionary<char, int>();

        foreach(char ch in t)
        {
            if (!map.ContainsKey(ch))
            {
                map.Add(ch, 0);
            }

            map[ch]++;
        }

        int cur = t.Length;
        string res = string.Empty;

        while(right < s.Length)
        {
            if (map.ContainsKey(s[right]) && map[s[right]]-- > 0)
            {
                cur --;
            }

            while (cur == 0)
            {
                if (right - left < res.Length || res.Length == 0)
                {
                    res = s.Substring(left, right-left+1);
                }

                if (map.ContainsKey(s[left]) &&  map[s[left]] ++ == 0)
                {
                    cur ++;
                }

                left++;
            }

            right++;
        }

        return res;
    }

    //https://leetcode.com/problems/encode-string-with-shortest-length/
    #region
    /*Given a non-empty string, encode the string such that its encoded length is the shortest.
    The encoding rule is: k[encoded_string], where the encoded_string inside the square brackets is being repeated exactly k times.

    Note:
    k will be a positive integer and encoded string will not be empty or have extra space.
    You may assume that the input string contains only lowercase English letters. The string's length is at most 160.
    If an encoding process does not make the string shorter, then do not encode it. If there are several solutions, return any of them is fine.

    Input: "aaa"
    Output: "aaa"
    Explanation: There is no way to encode it such that it is shorter than the input string, so we do not encode it.

    Input: "aaaaa"
    Output: "5[a]"
    Explanation: "5[a]" is shorter than "aaaaa" by 1 character.

    Input: "aaaaaaaaaa"
    Output: "10[a]"
    Explanation: "a9[a]" or "9[a]a" are also valid solutions, both of them have the same length = 5, which is the same as "10[a]".

    Input: "aabcaabcd"
    Output: "2[aabc]d"
    Explanation: "aabc" occurs twice, so one answer can be "2[aabc]d".*/
    #endregion
    public void ShortenedLetterCount()
    {

    }

    private string ShortenedLetterCount(string s)
    {
        return null;
    }

    //https://leetcode.com/problems/letter-combinations-of-a-phone-number/
    /*
    Given a string containing digits from 2-9 inclusive, return all possible letter combinations that the number could represent.
    A mapping of digit to letters (just like on the telephone buttons) is given below. Note that 1 does not map to any letters.

    Input: "23"
    Output: ["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"].*/
    public void LetterCombinations()
    {
        Dictionary<char, List<char>> map = new Dictionary<char, List<char>>();
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

        map.Add('2', list1);
        map.Add('3', list2);
        map.Add('4', list3);

        var res = LetterCombinations("234", 0, string.Empty, map, new List<string>());
    }

    private List<string> LetterCombinations(string digits, int idx, string str, Dictionary<char, List<char>> map, List<string> output)
    {
        if (idx == digits.Length)
        {
            output.Add(str);
            return output;
        }

        for(int i = 0; i < 3; i++)
        {
            LetterCombinations(digits, idx+1, str + map[digits[idx]][i], map, output);
        }

        return output;
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

    //Accepted:T:O(n) S:O(n) https://leetcode.com/problems/backspace-string-compare/
    public bool BackspaceCompare() 
    {
        Stack<char> one = new Stack<char>();
        Stack<char> two = new Stack<char>();
        string S = "abab#c";
        string T = "ad#c";

        UpdateStack(one, S);
        UpdateStack(two, T);
        
        if (one.Count != two.Count)
        {
            return false;
        }
        
        while(one.Count > 0)
        {
            if (one.Pop() != two.Pop())
            {
                return false;
            }
        }
        
        return true;
        
    }
    
    private void UpdateStack(Stack<char> stk, string str)
    {
        foreach(char ch in str)
        {
            if (ch != '#')
            {
                stk.Push(ch);
            }
            else
            {
                if (stk.Count > 0)
                {
                    stk.Pop();
                }
            }
        }
    }

    //https://leetcode.com/problems/verifying-an-alien-dictionary/
    public void VerifyAlienDictionary()
    {
        string[] words = new string[] {"word","world","row"}; 
        string order = "worldabcefghijkmnpqstuvxyz";

        Console.WriteLine(VerifyAlienDictionary(words, order));
    }

    private bool VerifyAlienDictionary(string[] words, string order)
    {
        Dictionary<char, int> map = new Dictionary<char, int>();

        for(int idx = 0; idx < order.Length; idx++)
        {
            if (!map.ContainsKey(order[idx]))
            {
                map[order[idx]] = idx;
            }
        }

        for(int idx = 1; idx < words.Length; idx++)
        {
            if (bigger(words[idx-1], words[idx], map))
            {
                return false;
            }
        }

        return true;
    }

    private bool bigger(string str1, string str2, Dictionary<char, int> map)
    {
        for(int idx = 0; idx < Math.Min(str1.Length, str2.Length); idx++)
        {
            if (map[str1[idx]] > map[str2[idx]])
            {
                return true;
            }
        }

        return false;
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

    //https://leetcode.com/discuss/interview-question/502496/google-onsite-substrings-that-dont-have-every-character-in-an-alphabet
    public void SubstringsNotMatchingAlphabets()
    {

    }

    private int SubstringsNotMatchingAlphabets(string str, HashSet<char> set)
    {
        int idx = 0;
        return idx;

    }

    //https://leetcode.com/problems/find-all-anagrams-in-a-string/
    public void FindAnagrams()
    {
        string s = "xyzbacd";
        string p = "abc";

        var res = findAnagrams(s, p);
    }

    private IList<int> FindAnagrams(string s, string p)
    {
        Dictionary<char, int> dict = new Dictionary<char, int>();
        Dictionary<char, int> slidingDict = new Dictionary<char, int>();

        PopulateDict(p, dict);
        PopulateDict(p, slidingDict);

        int idx = 0;
        int start = -1;

        List<int> res = new List<int>();

        while (idx < s.Length)
        {
            if (!dict.ContainsKey(s[idx]))
            {
                start = -1;
                if(slidingDict.Count < dict.Count)
                {
                    slidingDict.Clear();
                    PopulateDict(p, slidingDict);
                }
                idx++;
                continue;
            }

            if (slidingDict.Count > 0)
            {
                if (!slidingDict.ContainsKey(s[idx]))
                {
                    slidingDict.Clear();
                    PopulateDict(p, slidingDict);
                    start = -1;
                }

                start = start == -1 ? idx : start;
                slidingDict[s[idx]] -=1;
                if (slidingDict[s[idx]] == 0)
                {
                    slidingDict.Remove(s[idx]);
                }
            }

            if (slidingDict.Count == 0)
            {
                res.Add(start);
                PopulateDict(p, slidingDict);
            }

            idx++;
        }

        return res;
    }

    public List<int> findAnagrams(String s, String p)
    {
        int left = 0;
        int right = 0;
        int sLen = s.Length;
        int pLen = p.Length;
        int[] hash = new int[256];
        List<int> pos = new List<int>();
        
        for (int i = 0; i<pLen; i++)
        {
            hash[(int)p[i]]++;
        }

        int count = 0;

        while (right < sLen)
        {
            if (hash[(int)s[right]] > 0)
            {
                hash[(int)s[right]]--;
                count++;
                right++;
            }
            else
            {
                hash[(int)s[left]]++;
                count--;
                left++;
            }

            if(count == pLen)
            {
                pos.Add(left);
            }
            
        }
        return pos;
    }

    private void PopulateDict(string p, Dictionary<char, int> dict)
    {
        foreach(char c in p)
        {
            if (!dict.ContainsKey(c))
            {
                dict.Add(c, 0);
            }
            dict[c] += 1;
        }
    }

    //Accepted: T: O(n): https://leetcode.com/problems/longest-valid-parentheses/
    public void LongestValidParantheses()
    {
        Console.WriteLine(LongestValidParantheses("())((())"));
    }

    private int LongestValidParantheses(string s)
    {
        Stack<int> stk = new Stack<int>();
        int start = -1;
        int max = 0;
        int idx = 0;

        while (idx < s.Length)
        {
            if (s[idx] == ')')
            {
                if (stk.Count > 0)
                {
                    var cur = stk.Pop();
                    if (stk.Count == 0)
                    {
                        max = Math.Max(max, idx - start);
                    }
                    else
                    {
                        max = Math.Max(max, idx - stk.Peek());
                    }
                }
                else
                {
                    start = idx;
                }
            }
            else
            {
                stk.Push(idx);
            }

            idx++;
        }

       return max;
    }

    //https://leetcode.com/problems/string-transforms-into-another-string/submissions/
    public void StringTransformation()
    {
        string str1 = "abcdefghijklmnopqrstuvwxyz";
        string str2 = "bcdefghijklmnopqrstuvwxyza";

        Console.WriteLine(CanConvert(str1, str2));
    }

    public bool CanConvert(string str1, string str2)
    {
        Dictionary<char, char> map = new Dictionary<char, char>();

        int idx = 0;

        while (idx < str1.Length)
        {
            if (map.ContainsKey(str1[idx]) && map[str1[idx]] != str2[idx])
            {
                return false;
            }

            if (!map.ContainsKey(str1[idx]))
            {
                map.Add(str1[idx], str2[idx]);
            }

            idx++;
        }

        return map.Count <= 26;
    }

    //Accepted: https://leetcode.com/problems/construct-k-palindrome-strings/
    public void ConstructKPalindromeStrings()
    {
        string s = "abacacbacab";
        int k = 3;
        Console.WriteLine(ConstructKPalindromeStrings(s, k));
    }

    private bool ConstructKPalindromeStrings(string s, int k)
    {
        int oddCount = 0;

        Dictionary<char,int> map = new Dictionary<char, int>();

        for(int idx = 0; idx < s.Length; idx++)
        {
            if (!map.ContainsKey(s[idx]))
            {
                map.Add(s[idx], 0);
            }

            map[s[idx]] ++;

            oddCount +=  map[s[idx]] %2 > 0 ? 1: -1;
        }

        return oddCount <= k && k <= s.Length;
    }

    public void CanPermutePalindrome()
    {
        string str = "carerac";
        Console.WriteLine(CanPermutePalindrome(str));
    }

    private bool CanPermutePalindrome(string s)
    {
        Dictionary<char, int> map = new Dictionary<char, int>();
        int oddCount = 0;

        for(int idx = 0; idx < s.Length; idx++)
        {
            if (!map.ContainsKey(s[idx]))
            {
                map.Add(s[idx], 0);
            }

            map[s[idx]] ++;

            oddCount += map[s[idx]] % 2 > 0 ? 1 : -1;
        }

        return oddCount <= 1;
    }

    //Accepted: https://leetcode.com/problems/shortest-palindrome/
    public void ShortestPalindrome()
    {
        string str = "abcd";
        Console.WriteLine(ShortestPalindrome(str));
    }

    private string ShortestPalindrome(string s)
    {
        int j = 0;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (s[i] == s[j])
            {
                j += 1;
            }
        }

        if (j == s.Length)
        {
             return s;
        }

        String suffix = s.Substring(j);

        return suffix.Reverse() + ShortestPalindrome(s.Substring(0, j)) + suffix;
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