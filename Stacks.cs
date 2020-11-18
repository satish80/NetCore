using System;
using System.Collections.Generic;

public class Stacks
{
   //Accepted:LCMedium:LCSol-T:O(n) S:O(n) https://leetcode.com/problems/validate-stack-sequences/ 
    public void ValidateStackSequences()
    {
        int[] push = new int[]{1, 2, 3, 4, 5};
        int[] pop = new int[]{4, 5, 3, 2, 1};

        Console.WriteLine(ValidateStackSequences(push, pop));
    }

    public bool ValidateStackSequences(int[] pushed, int[] popped)
    {
        Stack<int> pushStk = new Stack<int>();

        int popIdx = 0;

        for(int i = 0; i < pushed.Length; i++)
        {
            pushStk.Push(pushed[i]);

            while (pushStk.Count > 0 && pushStk.Peek() == popped[popIdx])
            {
                pushStk.Pop();
                popIdx++;
            }
        }

        return pushStk.Count == 0;
    }
}