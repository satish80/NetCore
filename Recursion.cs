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
}