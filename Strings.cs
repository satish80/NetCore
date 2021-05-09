
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using DataStructures;

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

    //https://leetcode.com/problems/add-binary/
    public void AddBinary()
    {
        Console.WriteLine(AddBinary("11", "1"));
    }

    private string AddBinary(string a, string b)
    {
        StringBuilder sb = new StringBuilder();
        int i = a.Length - 1, j = b.Length -1, carry = 0;

        while (i >= 0 || j >= 0)
        {
            int sum = carry;
            if (j >= 0)
            {
                sum += b[j--] - '0';
            }

            if (i >= 0)
            {
                sum += a[i--] - '0';
            }

            sb.Append(sum % 2);
            carry = sum / 2;
        }

        if (carry != 0)
        {
            sb.Append(carry);
        }

        return sb.ToString().Reverse();
    }
    
    //https://leetcode.com/problems/palindrome-permutation/
    public void CanPermutePalindrome()
    {
        string s = "carerac";
        Console.WriteLine(CanPermutePalindrome(s));
    }

    private bool CanPermutePalindrome(string s)
    {
        Dictionary<char, int>  map = new Dictionary<char, int>();
        
        for(int idx = 0; idx < s.Length; idx++)
        {
            if (!map.ContainsKey(s[idx]))
            {
                map.Add(s[idx], 1);
            }
            else
            {
                map[s[idx]]--;
                if (map[s[idx]] == 0)
                {
                    map.Remove(s[idx]);
                }
            }
        }
        
        return map.Count <= 1;
    }

    //https://leetcode.com/discuss/interview-question/1167901/facebook-onsite-interview-london-21
    public void RemoveConsecutiveChars()
    {
        string str = "abba";
    }

    private string RemoveConsecutiveChars(string str)
    {
        int idx = 1;
        StringBuilder sb = new StringBuilder();

        while (idx < str.Length)
        {
            sb.Append(str[idx]);
            idx++;

            while (idx < str.Length && str[idx] == str[idx-1])
            {
                idx++;
            }
        }

        return sb.ToString();
    }

    //https://leetcode.com/problems/group-shifted-strings/
    public void GroupStrings()
    {
        string[] strings = new string[]
        {
            //"abc","bcd","acef","bdfg","xyz","az","ba","a","z"
            "az", "yx"
        };

        var res = GroupStrings(strings);
    }

    private IList<IList<string>> GroupStrings(string[] strings)
    {
        Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();

        foreach(string str in strings)
        {
            string res = string.Empty;
            int offset = str[0] - 'a';

            for(int idx = 0; idx < str.Length; idx++)
            {
                int diff = str[idx] - offset;

                if (diff < 0)
                {
                    diff+= 26;
                }

                res += diff;
            }

            if (!map.ContainsKey(res))
            {
                map.Add(res, new List<string>());
            }

            map[res].Add(str);
        }

        IList<IList<string>> r = new List<IList<string>>();

        foreach(string key in map.Keys)
        {
            r.Add(map[key]);
        }

        return r;
    }

    //Accepted:LCMedium:T:O(nlogn *m):https://leetcode.com/problems/group-anagrams/
    public void GroupAnagrams()
    {
        string[] strs = new string[]{"eat","tea","tan","ate","nat","bat"};
        var res = GroupAnagrams(strs);
    }

    private IList<IList<string>> GroupAnagrams(string[] strs)
    {
        Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
        IList<IList<string>> res = new List<IList<string>>();

        foreach(string str in strs)
        {
            var sortedStr = string.Concat(str.OrderBy(c=>c));

            if (!map.ContainsKey(sortedStr))
            {
                map.Add(sortedStr, new List<string>());
            }

            map[sortedStr].Add(str);
        }

        foreach(KeyValuePair<string, List<string>> pair in map)
        {
            res.Add(pair.Value);
        }

        return res;
    }

    //Accepted: T:O(n), S:O(k) :https://leetcode.com/problems/minimum-window-substring/
    public void MinWindowsSubstring()
    {
        Console.WriteLine(CreMinWindowsSubstring("acbbaca", "aba"));
        //Console.WriteLine(CreMinWindowsSubstring("ADOBECODEBANC", "ABC"));
    }

    private string CreMinWindowsSubstring(string s, string t)
    {
        Dictionary<int, int> map = new Dictionary<int, int>();

        foreach(char ch in s)
        {
            if (!map.ContainsKey(ch))
            {
                map.Add(ch, 0);
            }
        }

        foreach(char ch in t)
        {
            if (map.ContainsKey(ch))
            {
                map[ch]++;
            }
        }

        int counter = t.Length, begin = 0, end = 0, d = int.MaxValue, head = 0;

        while (end < s.Length)
        {
            if (map.ContainsKey(s[end]) && map[s[end]] > 0)
            {
                counter--;
            }

            map[s[end]]--;
            end++;

            while (counter == 0)
            {
                if(end-begin < d)
                {
                    d = end-begin;
                    head = begin;
                }

                map[s[begin]]++;
                if (map[s[begin]] > 0)
                {
                    counter++;
                }

                begin++;
            }
        }

        return d == int.MaxValue ? "" : s.Substring(head, d);
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

        var res = LetterCombinationsCre("234", 0, string.Empty, map, new List<string>());
    }

    private List<string> LetterCombinationsCre(string str, int idx, string cur, Dictionary<char, List<char>> map, List<String> res)
    {
        if (idx == str.Length)
        {
            res.Add(cur);
            return res;
        }

        var mStr = map[str[idx]];

        for(int i = 0; i < 3; i++)
        {
            LetterCombinationsCre(str, idx+1, cur + mStr[i], map, res);
        }

        return res;
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

    //https://leetcode.com/problems/minimum-remove-to-make-valid-parentheses/
    public void MinRemoveToMakeValid()
    {
        string s = "))((";
        Console.WriteLine(MinRemoveToMakeValid(s));
    }

    private string MinRemoveToMakeValid(string s) 
    {
        Stack<int> stk = new Stack<int>();
        StringBuilder sb = new StringBuilder();
        
        for(int idx = 0; idx < s.Length; idx++)
        {
            if (s[idx] == '(')
            {
                stk.Push(idx);
            }
            else if (s[idx] == ')')
            {
                if (stk.Count > 0 && stk.Peek() == '(')
                {
                    stk.Pop();
                }
                else
                {
                    stk.Push(idx);
                }
            }
        }

        int  i = -1;
        char[] arr = s.ToCharArray();

        for(int idx = s.Length-1; idx >= 0; idx--)
        {
            if (stk.Count > 0)
            {
               i = stk.Pop();
            }

            if (idx == i)
            {
                arr[idx] = ' ';
            }
        }

        return arr.ToString().Replace(" ", "");
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

    //https://leetcode.com/problems/palindrome-partitioning/
    public void PalindromePartition()
    {
        string s = "aab";
        var res = PalindromePartition(s);
    }

    private IList<IList<string>> PalindromePartition(string s)
    {
        bool[,] dp = new bool[s.Length,s.Length];
        IList<IList<string>> res = new List<IList<string>>();
        List<string> path = new List<string>();

        //No of chars
        for(int i = 0; i < s.Length; i++)
        {
            //Position
            for(int j = 0; j <= i; j++)
            {
                if(s.ElementAt(i) == s.ElementAt(j) && (i - j < 2 || dp[j-1,i-1]))
                {
                    dp[j,i] = true;
                }
            }
        }

        bool[,] credp = new bool[,]
        {
            {true, true, true},
            {true, false, false},
            {false, false, false}
        };
        PalindromePartitionHelper(credp, res, path, s, 0);
        return res;
    }

    private void PalindromePartitionHelper(bool[,] dp, IList<IList<string>> res, List<string> path, string s, int pos)
    {
        if (pos == s.Length)
        {
            res.Add(new List<string>(path));
            return;
        }

        for(int i = pos; i < s.Length; i++)
        {
            if (dp[pos, i-pos+1])
            {
                var cur = s.Substring(pos, i-pos+1);
                path.Add(cur);
                PalindromePartitionHelper(dp, res, path, s, i+1);
                path.RemoveAt(path.Count-1);
            }
        }
    }

    private bool Palindrome(string s, int i, int length)
    {
        string str = s.Substring(i, length);

        int left = 0;
        int right = str.Length-1;

        while (left < right)
        {
            if (s[left++] != s[right++])
            {
                return false;
            }
        }

        return true;
    }

    /*
    Asked by Google
    Given a string of words delimited by spaces, reverse the words in string. For example, given "hello world here", return "here world hello"
    Follow-up: given a mutable string representation, can you perform this operation in-place?
    */
    public void ReverseString()
    {
        string str = "hello world here";
        var res = ReverseString(str);
    }

    private char[] ReverseString(string str)
    {
        char[] arr = str.ToCharArray();
        int left = 0, right = arr.Length-1;

        Reverse(arr, left, right);

        left = 0; right = 0;

        while (right < arr.Length)
        {
            while (right+1 < arr.Length && arr[right+1] != ' ' )
            {
                right++;
            }

            Reverse(arr, left, right);
            left = right = right+2;
        }

        return arr;
    }

    private void Reverse(char[] arr, int left, int right)
    {
        while(left < right)
        {
            char temp = arr[left];
            arr[left] = arr[right];
            arr[right] = temp;
            left++;
            right--;
        }
    }

    /*
    [ 124 ] Asked by Google
    Given a word W and a string S, find all starting indices in S which are anagrams of W.
    For example, given that W is "ab", and S is "abxaba", return 0, 3, and 4.
    */
    public void FindAnagramIndices()
    {
        string word = "ab";
        string str = "abxaba";
        var res = FindAnagramIndices(word, str);
    }

    private List<int> FindAnagramIndices(string word, string str)
    {
        List<int> res = new List<int>();
        Dictionary<char, int> map = new Dictionary<char, int>();
        int count = 0;

        foreach(char ch in str)
        {
            if (!map.ContainsKey(ch))
            {
                map.Add(ch, 0);
            }
        }

        foreach(char ch in word)
        {
            if(map.ContainsKey(ch))
            {
                map[ch]++;
            }
        }

        count = word.Length;

        int left = 0, right = 0;

        while(right < str.Length)
        {
            while (count > 0 && map[str[right]] > 0)
            {
                map[str[right++]]--;
                count--;
            }

            if (count == 0)
            {
                res.Add(left);
            }

            while (left < right)
            {
                map[str[left]]++;
                if (map[str[left]] >0)
                {
                    count++;
                }

                left++;
            }

            if (count < word.Length)
            {
                // map.Values.
            }
        }

        return res;
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

    //https://leetcode.com/problems/word-search/
    public void WordSearch()
    {
        char[][] board = new char[][]
        {
            new char[]{'A','B','C','E'},
            new char[]{'S','F','C','S'},
            new char[]{'A','D','E','E'}
        };

        bool[,] visited = new bool[board.Length, board[0].Length];
        Console.WriteLine(WordSearch(board, "ABCCED", 0, 0, 0, string.Empty, visited));
    }

    private bool WordSearch(char[][] board, string word, int row, int col, int idx, string cur, bool[,] visited)
    {
        if (row < 0 || col < 0 || row >= board.Length || col >= board[row].Length || visited[row, col] )
        {
            return false;
        }

        if (word == cur)
        {
            return true;
        }

        bool res = false;

        visited[row, col] = true;

        if (word[idx] == board[row][col])
        {
            idx++;
            cur = cur+board[row][col];
        }

        res = WordSearch(board, word, row, col+1, idx, cur, visited) ||
            WordSearch(board, word, row+1, col, idx, cur, visited) ||
            WordSearch(board, word, row-1, col, idx, cur, visited) ||
            WordSearch(board, word, row, col-1, idx, cur, visited);

        visited[row, col] = false;
        return res;
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

    //https://leetcode.com/problems/word-search-ii/
    public void WordSearchII()
    {
        char[][] board = new char[][]
        {
            new char[] {'o','a','a','n'},
            new char[] {'e','t','a','e'},
            new char[] {'i','h','k','r'},
            new char[] {'i','f','l','v'}
        };

        string[] words = new string[] {"oath","pea","eat","rain"};
        var node  = ConstructTrie(board, words);

        List<string> res = new List<string>();

        for(int row = 0; row < board.Length; row++)
        {
            for(int col = 0; col < board[0].Length; col++)
            {
                FindWords(board, node.Root, row, col, res);
            }
        }
    }

    private Trie ConstructTrie(char[][] board, string[] words)
    {
        Trie root = new Trie();

        foreach(string word in words)
        {
            root.Insert(word);
        }

        return root;
    }

    private IList<string> FindWords(char[][] board, TrieNode word, int row, int col, IList<string> res)
    {
        if (row < 0 || col < 0|| row >= board.Length || col >= board[0].Length || board[row][col] == '#' || word.Children.Count == 0)
        {
            return res;
        }

        if (word.IsEndOfWord)
        {
            res.Add(word.Word);
            word.Children.Clear();
        }

        if (!word.Children.ContainsKey(board[row][col]) || word.Children.Count == 0)
        {
            return res;
        }

        word = word.Children[board[row][col]];
        char ch = board[row][col];
        board[row][col] = '#';
        FindWords(board, word, row, col+1, res);
        FindWords(board, word, row+1, col, res);
        FindWords(board, word, row, col-1, res);
        FindWords(board, word, row-1, col, res);

        board[row][col] = ch;
        return res;
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
        Console.WriteLine(DecodeString("3[a]2[b4[F]c]"));
    }

    private string DecodeString(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return s;
        }

        Stack<int> intStk = new Stack<int>();
        Stack<string> strStk = new Stack<string>();
        StringBuilder str = new StringBuilder();

        int idx = 0;
        int count = 0;
        int product = 10;

        while (idx < s.Length)
        {
            while (idx < s.Length && s[idx] != '[' && s[idx] != ']')
            {
                if ((s[idx] >= 65 && s[idx] <= 90) || s[idx] >= 97 && s[idx] <= 122)
                {
                    str.Append(s[idx++]);
                }
                else
                {
                    if (!string.IsNullOrEmpty(str.ToString()))
                    {
                        strStk.Push(str.ToString());
                        str.Clear();
                    }

                    while(s[idx] >= 48 && s[idx] <= 57)
                    {
                        count = count * product + int.Parse(s[idx++].ToString());
                    }

                    if (count > 0)
                    {
                        intStk.Push(count);
                        count = 0;
                    }
                }
            }

            if (!string.IsNullOrEmpty(str.ToString()))
            {
                strStk.Push(str.ToString());
                str.Clear();
            }

            if (idx < s.Length -1 && s[idx] == ']' )
            {
                var repeat = Repeat(strStk.Pop(), intStk.Pop());
                var concatStr = strStk.Count > 0 ? string.Concat(strStk.Pop(), repeat) : repeat;
                strStk.Push(concatStr);
            }

            idx++;
        }

        if (strStk.Count > 1)
        {
            var suffix = strStk.Pop();
            var concatStr = string.Concat(strStk.Pop(), suffix);
            strStk.Push(concatStr);
        }

        return strStk.Pop();
    }

    private string Repeat(string str, int count)
    {
        StringBuilder sb = new StringBuilder();
        for(int idx = 0; idx < count; idx++)
        {
            sb.Append(str);
        }

        return sb.ToString();
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

    //https://leetcode.com/problems/longest-substring-with-at-least-k-repeating-characters/
    public void LongestSubstringWithKRepeatingChars()
    {
        string s = "ababbc";
        int k = 2;
        Console.WriteLine(LongestSubstringWithKRepeatingChars(s, k));
    }

    private int LongestSubstringWithKRepeatingChars(string s, int k)
    {
        if (s == null || s.Length == 0) return 0;

        int res = 0;
        int start = 0;
        int[] chars = new int[26];
        int idx = 0;

        for (int i = 0; i < s.Length; i++)
        {
            chars[s[i] - 'a'] ++;
        }

        bool flag = true;
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] < k && chars[i] > 0)
            {
                flag = false;
            }
        }

        if (flag == true) return s.Length;

        while(idx < s.Length)
        {
            if (chars[s[idx]-'a'] < k)
            {
                res = Math.Max(res, LongestSubstringWithKRepeatingChars(s.Substring(start, idx), k));
                start = idx+1;
            }

            idx++;
        }

        res = Math.Max(res, LongestSubstringWithKRepeatingChars(s.Substring(start), k));

        return res;
    }

    //Accepted:LCEasy-SelfSol-T:O(n):https://leetcode.com/problems/sentence-similarity/
    public void AreSentencesSimilar()
    {
        var sentence1 = new string[] {"great","acting","skills"};
        var sentence2 = new string[] {"fine","painting","talent"};

        var similarPairs = new List<IList<string>>()
        {
            new List<string>() {"great","fine"},
            new List<string>() {"drama","acting"},
            new List<string>() {"skills","talent"}
        };

        Console.WriteLine(AreSentencesSimilar(sentence1, sentence2, similarPairs));
    }

    private bool AreSentencesSimilar(string[] sentence1, string[] sentence2, IList<IList<string>> similarPairs)
    {
        if (sentence1.Length != sentence2.Length)
        {
            return false;
        }

        IDictionary<string, HashSet<string>> map = new Dictionary<string, HashSet<string>>();

        foreach(IList<string> similarPair in similarPairs)
        {
            if (!map.ContainsKey(similarPair[0]))
            {
                map.Add(similarPair[0], new HashSet<string>());
            }

            if (!map.ContainsKey(similarPair[1]))
            {
                map.Add(similarPair[1], new HashSet<string>());
            }

            map[similarPair[0]].Add(similarPair[1]);

            map[similarPair[1]].Add(similarPair[0]);
        }

        for(int idx = 0; idx < sentence1.Length; idx++)
        {
            if (sentence1[idx] != sentence2[idx])
            {
                if (! map.ContainsKey(sentence1[idx]))
                {
                    return false;
                }

                if (! map[sentence1[idx]].Contains(sentence2[idx]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    //https://leetcode.com/problems/find-all-anagrams-in-a-string/
    public void FindAllAnagrams()
    {
        string s = "cbaebabacd";
        string p = "abc";

        var res = FindAllAnagrams(s, p);
    }

    private IList<int> FindAllAnagrams(string s, string p)
    {
        Dictionary<char, int> map = new Dictionary<char, int>();
        IList<int> res = new List<int>();

        foreach(char ch in p)
        {
            if(!map.ContainsKey(ch))
            {
                map.Add(ch, 0);
            }

            map[ch]++;
        }

        int start = 0;
        int end = 0;
        int counter = map.Count;

        while(end < s.Length)
        {
            if (map.ContainsKey(s[end]))
            {
                map[s[end]]--;

                if (map[s[end]] == 0)
                {
                    counter--;
                }
            }

            end++;

            while(counter == 0)
            {
                if (map.ContainsKey(s[start]))
                {
                    map[s[start]]++;
                    if (map[s[start]] > 0)
                    {
                        counter++;
                    }
                }

                if (end-start == p.Length)
                {
                    res.Add(start);
                }
                start++;
            }
        }

        return res;
    }

    /*
    Asked by Dropbox
    Given a string s and a list of words words, where each word is the same length, find all starting indices of substrings in s that is
    a concatenation of every word in words exactly once. For example, given s = "dogcatcatcodecatdog" and words = ["cat", "dog"], return [0, 13], 
    since "dogcat" starts at index 0 and "catdog" starts at index 13.
    Given s = "barfoobazbitbyte" and words = ["dog", "cat"], return [] since there are no substrings composed of "dog" and "cat" in s.

    The order of the indices does not matter.
    */
    public void FindConcatenationOfWords()
    {
        string s = "dogcatcatcodecatdog";
        string[] words = new string[] {"cat", "dog"};
        var res = FindConcatenationOfWords(s, words);
    }

    private List<int> FindConcatenationOfWords(string str, string[] words)
    {
        int counter = 0;
        Dictionary<char, int> map = new Dictionary<char, int>();
        List<int> res = new List<int>();
        int wordCount = 0;

        foreach(string word in words)
        {
            foreach(char ch in word)
            {
                wordCount++;
                if (!map.ContainsKey(ch))
                {
                    map.Add(ch, 0);
                }

                map[ch]++;
            }

            counter = map.Count;
        }

        int start = 0;
        int end = 0;

        while (end < str.Length)
        {
            if (map.ContainsKey(str[end]) && counter > 0)
            {
                map[str[end]]--;

                if (map[str[end]] == 0)
                {
                    counter--;
                }
            }

            end++;

            while (counter == 0)
            {
                if (end-start == wordCount)
                {
                    res.Add(start);
                }

                if (map.ContainsKey(str[start]) )
                {
                    map[str[start]]++;
                    
                    if (map[str[start]] > 0)
                    {
                        counter++;
                    }
                }

                start++;
            }
        }

        return res;
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

    //https://leetcode.com/problems/valid-palindrome-ii/
    public void ValidPalindromeII()
    {
        Console.WriteLine(ValidPalindromeII("aguokepatgbnvfqmgmlcupuufxoohdfpgjdmysgvhmvffcnqxjjxqncffvmhvgsymdjgpfdhooxfuupuculmgmqfvnbgtapekouga"));
    }

    private bool ValidPalindromeII(string s)
    {
        if (s == null)
        {
            return true;
        }

        int left = 0;
        int right = s.Length -1;
        int count = 0;

        while (left < right)
        {
            if (s[left] != s[right])
            {
                if (count > 0)
                {
                    return false;
                }
                count++;

                if (s[left+1] == s[right])
                {
                    left++;
                }
                else if (s[left] == s[right-1])
                {
                    right --;
                }
                else
                {
                    return false;
                }

                continue;
            }

            left++;
            right--;
        }

        return true;
    }

    //:LC Hard:Accepted:T:O(n^2), S:O(n^2): https://leetcode.com/problems/valid-palindrome-iii/
    public void ValidPalindromeIII()
    {
        Console.WriteLine(ValidPalindromeIII("abcdeca", 2));
    }

    private bool ValidPalindromeIII(string s, int k)
    {
        char[] r = s.ToCharArray();
        Array.Reverse(r);
        int[,] dp = new int[s.Length+1, s.Length+1];

        for(int row = 1; row <= s.Length; row++)
        {
            for(int col = 1; col <= r.Length; col++)
            {
                if (s[row-1] != r[col-1])
                {
                    dp[row, col] = Math.Max(dp[row-1,col], dp[row,col-1]);
                }
                else
                {
                    dp[row, col] = dp[row-1,col-1] + 1;
                }
            }
        }

        return s.Length  - dp[s.Length, s.Length] <= k;
    }

    /* There's a faulty keyboard which types a space whenever key 'e' is hit.
    Given a string which is the sentence typed using that keyboard and a dictionary of valid words, return all possible correct formation of the sentence.
    Example:
    Input: s = "I lik   to   xplor   univ rs ", dictionary  = {"like", "explore", "toe", "universe", "rse"}
    Output:
    ["I like to explore universe",
    "I like toe xplor  universe",
    "I like to explore univ rse",
    "I like toe xplor  univ rse"]
    There are two spaces after "lik", "to" and before "univ" in the sentence indicating one is actual space and another one is resulted by hitting key 'e' */
    public void FaultyKeyBoard()
    {
        string s = "I lik   to   xplor   univ rs ";
        HashSet<string> dictionary = new HashSet<string>();
        dictionary.Add("like");
        dictionary.Add("explore");
        dictionary.Add("toe");
        dictionary.Add("universe");
        dictionary.Add("rse");

        var res = FaultyKeyBoard(s, dictionary, 0, s.Length-1);
    }

    private IList<string> FaultyKeyBoard(string s, HashSet<string> dictionary, int start, int end)
    {
        int idx = start;
        string cur = string.Empty;
        IList<string> resWithE = new List<string>();
        IList<string> resWithoutE = new List<string>();
        IList<string> res = new List<string>();

        while (idx < s.Length)
        {
            if (s[idx] == ' ' && (idx + 1 < s.Length && s[idx+1] == ' '))
            {
                var subStringWithE = string.Concat(s.Substring(start, idx), 'e');
                var subStringWithoutE = string.Concat(s.Substring(start, idx));

                if (dictionary.Contains(subStringWithE))
                {
                    cur = subStringWithE;
                    resWithE = FaultyKeyBoard(s, dictionary, idx+1, end);
                    ConstructResultString(cur, resWithE);
                    break;
                }
                else if (dictionary.Contains(subStringWithoutE))
                {
                    cur = subStringWithoutE;
                    resWithE = FaultyKeyBoard(s, dictionary, idx+1, end);
                    ConstructResultString(cur, resWithE);
                    break;
                }
            }

            idx++;
        }

        return res;
    }

    private IList<string> ConstructResultString(string s, IList<string> list)
    {
        IList<string> res = new List<string>();

        foreach(string str in list)
        {
            res.Add(string.Concat(s, str));
        }

        return res;
    }

    //https://leetcode.com/problems/longest-substring-without-repeating-characters/
    public void LongestSubstringWithoutRepeatingChars()
    {
        string s = " ";
        Console.WriteLine(LongestSubstringWithoutRepeatingChars(s));
    }

    private int LongestSubstringWithoutRepeatingChars(string s)
    {
        int len = 0;
        int start = 0;
        int end = 0;

        Dictionary<char, int> map = new Dictionary<char, int>();

        while (end < s.Length && start <= end)
        {
            if (map.ContainsKey(s[end]))
            {
                var idx = start;
                var endIdx = map[s[end]];
                start = map[s[end]]+1;

                while (idx <= endIdx)
                {
                    map.Remove(s[idx++]);
                }
            }

            len = Math.Max(len, end-start+1);

            map.Add(s[end], end++);
        }

        return len;
    }

    //https://leetcode.com/contest/weekly-contest-197/problems/number-of-substrings-with-only-1s/
    public void NumberOfSubstringWithOnly1s()
    {
        string str = "111111";
        Console.WriteLine(NumberOfSubstringWithOnly1s(str));
    }

    private int NumberOfSubstringWithOnly1s(string s)
    {
        int count = 0;

        if (string.IsNullOrEmpty(s))
        {
            return count;
        }

        int cur = 0;

        for(int idx = 0; idx < s.Length; idx++)
        {
            if (s[idx] == '0')
            {
                count += (cur * (cur+1)) /2;
                cur = 0;
            }
            else 
            {
                cur += 1;
            }
        }

        return (cur == 0 ? count : count+=((cur * (cur+1)) /2) );
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

    //Accepted-LCHard-Self-T:O(n)-https://leetcode.com/problems/parsing-a-boolean-expression/
    public void ParseBoolExpr()
    {
        string expression = "|(&(t,f,t),!(t))";
        Console.WriteLine(ParseBoolExpr(expression));
    }

    private bool ParseBoolExpr(string expression)
    {
        Stack<char> values = new Stack<char>();

        int idx = 0;

        while (idx < expression.Length)
        {
            char ch = expression[idx];

            if (ch == ')')
            {
                HashSet<char> set = new HashSet<char>();

                while(values.Peek() != '(')
                {
                    set.Add(values.Pop());
                }

                values.Pop();

                var op = values.Pop();

                if (op == '|')
                {
                    values.Push(set.Contains('t') ? 't' : 'f');
                }
                else if (op == '&')
                {
                    values.Push(set.Contains('f') ? 'f' : 't');
                }
                else if (op == '!')
                {
                    values.Push(set.Contains('t') ? 'f' : 't');
                }
            }
            else if (ch != ',')
            {
                values.Push(ch);
            }

            idx++;
        }

        return values.Pop() == 't' ? true : false;
    }

    //Accepted: T:O(n), S:O(n): https://leetcode.com/problems/remove-all-adjacent-duplicates-in-string-ii/
    public void RemoveAdjacentDuplicates()
    {
        string str = "deeedbbcccbdaa";
        Console.WriteLine(RemoveAdjacentDuplicates(str, 3));
    }

    private string RemoveAdjacentDuplicates(string s, int k)
    {
        int i = 0, n = s.Length;
        int[] count = new int[n];

        char[] stack = s.ToCharArray();

        for (int j = 0; j < n; ++j, ++i)
        {
            stack[i] = stack[j];
            count[i] = i > 0 && stack[i - 1] == stack[j] ? count[i - 1] + 1 : 1;

            if (count[i] == k)
            {
                i -= k;
            }
        }

        return new String(stack, 0, i);
    }

    //LCMedium-LCSoln-T:O(n) S:O(words length):https://leetcode.com/problems/number-of-matching-subsequences/
    public void NumMatchingSubSeq()
    {
        string S = "abcde";
        string[] words = {"a", "bb", "acd", "ace"};
        var res = NumMatchingSubseq(S, words);
    }

    private int NumMatchingSubseq(String S, String[] words)
    {
        int ans = 0;
        List<Node>[] heads = new List<Node>[26];
        for (int i = 0; i < 26; ++i)
            heads[i] = new List<Node>();

        foreach (string word in words)
        {
            heads[word[0] - 'a'].Add(new Node(word, 0));
        }

        foreach (char c in S)
        {
            List<Node> old_bucket = heads[c - 'a'];
            heads[c - 'a'] = new List<Node>();

            foreach (Node node in old_bucket)
            {
                node.Idx++;
                if (node.Idx == node.Word.Length)
                {
                    ans++;
                }
                else
                {
                    heads[node.Word[node.Idx] - 'a'].Add(node);
                }
            }

            old_bucket.Clear();
        }

        return ans;
    }

    //LCEasy-Self-T:O(N^2):https://leetcode.com/problems/repeated-substring-pattern/
    public void RepeatedSubstringPattern()
    {
        string s = "abab";
        Console.WriteLine(RepeatedSubstringPattern(s));
    }

    private bool RepeatedSubstringPattern(string s)
    {
        int splitLength = s.Length /2;
        int firstIdx = 0;
        int secondIdx = 0;

        while(splitLength > 0)
        {
            firstIdx = 0;
            secondIdx = firstIdx + splitLength;

            while(secondIdx < s.Length)
            {
                if (secondIdx+splitLength > s.Length || s.Substring(firstIdx, splitLength) != s.Substring(secondIdx, splitLength))
                {
                    break;
                }

                if (secondIdx + splitLength == s.Length)
                {
                    return true;
                }

                firstIdx = secondIdx;
                secondIdx = secondIdx + splitLength;
            }

            splitLength--;
            if (splitLength > 0 && s.Length % splitLength != 0)
            {
                splitLength--;
            }
        }

        return false;
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