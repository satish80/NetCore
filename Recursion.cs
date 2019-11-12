using System;
using System.Text;
using System.Collections.Generic;

public class Recursion
{
    //https://leetcode.com/problems/expression-add-operators/
    public void ExpressionAddOperators()
    {
        List<string> final = new List<string>();
        List<string> expression = new List<string>();
        expression.Add("+");
        expression.Add("-");
        expression.Add("*");
        expression.Add("/");

        ExpressionAddOperators("123", 6, 0, expression, string.Empty, final);
    }

    private int ExpressionAddOperators(string num, int target, int idx, List<string> expression, 
    string path, List<string> final)
    {
        if (idx > num.Length-1)
        {
            return 0;
        }

        if (idx == num.Length - 1)
        {
            return int.Parse(num[idx].ToString());
        }

        int calculate = 0;

        path = path + num.Substring(idx, 1);
        foreach(string exp in expression)
        {
            switch (exp)
            {
                case "+":
                {
                    int res = ExpressionAddOperators(num.Substring(idx+1), target, idx+1, expression, path + "+", final);
                    calculate = int.Parse(num.Substring(idx, 1)) + res;

                    break;
                }
                case "-":
                {
                    int res = ExpressionAddOperators(num.Substring(idx+1), target, idx+1, expression, path + "-", final);
                    calculate = int.Parse(num.Substring(idx, 1)) - res;

                    break;
                }
                case "*":
                {
                    int res = ExpressionAddOperators(num.Substring(idx+1), target, idx+1, expression, path + "*", final);
                    calculate = int.Parse(num.Substring(idx, 1)) * res;
                    break;
                }
                case "/":
                {
                    int res = ExpressionAddOperators(num.Substring(idx+1), target, idx+1, expression, path + "/", final);
                    calculate = int.Parse(num.Substring(idx, 1)) / (res == 0 ? 1 : res);

                    break;
                }
            }

            if(calculate == target)
            {
                final.Add(path);
            }
        }

        return calculate;
    }

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
