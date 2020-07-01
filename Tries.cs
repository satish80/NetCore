using DataStructures;
using System;
using static DataStructures.Trie;

public class Tries
{
    public void AutoSuggest()
    {
        Trie node = new Trie();
        node.Insert("car");
        node.Insert("art");
        node.Insert("articulate");
        node.Insert("artistic");
        node.Insert("artist");
        node.Insert("arts");

        var res = node.Search("arti");
    }

    //https://leetcode.com/problems/longest-duplicate-substring/
    public void LongestDuplicateSubstring()
    {
        string str = "banana";
        Console.WriteLine(LongestDuplicateSubstring(str));
    }

    private string LongestDuplicateSubstring(string s)
    {
        Trie node = new Trie();
        int max = int.MinValue;
        int[] res = new int[2];

        for(int idx = 0; idx < s.Length; idx++)
        {
            TrieNode curNode = node.Root;
            var cur = node.InsertWithIndex(s.Substring(idx, s.Length-idx), idx, curNode);

            if (max < cur[1] - cur[0])
            {
                max = cur[1] - cur[0];
                res[0] = cur[0];
                res[1] = cur[1];
            }
        }

        return s.Substring(res[0], res[1] - res[0] + 1);
    }
}