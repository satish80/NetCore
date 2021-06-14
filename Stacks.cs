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

    //Accepted-LcHard-LcSol-T:O(1)-S:O(n) https://leetcode.com/problems/maximum-frequency-stack/
    public void FrequencyStack()
    {
        FreqStack freqStack = new FreqStack();
        freqStack.Push(5);
        freqStack.Push(7);
        freqStack.Push(5);
        freqStack.Push(7);        
        freqStack.Push(4);
        freqStack.Push(5);

        for(int idx = 0; idx < 4; idx++)
        {
            Console.WriteLine(freqStack.Pop());
        }
    }

    public class FreqStack 
    {
        Dictionary<int, int> freq = new Dictionary<int, int>();
        Dictionary<int, Stack<int>> stkMap = new Dictionary<int, Stack<int>>();
        int maxFrequency = 0;

        public FreqStack() 
        {
            
        }
        
        public void Push(int val) 
        {
            if (!freq.ContainsKey(val))
            {
                freq.Add(val, 0);
            }

            freq[val]++;
            int frequency = freq[val];
            maxFrequency = Math.Max(maxFrequency, frequency);

            if (!stkMap.ContainsKey(frequency))
            {
                stkMap.Add(frequency, new Stack<int>());
            }

            stkMap[frequency].Push(val);
        }
    
        public int Pop() 
        {
            int res = stkMap[maxFrequency].Pop();
            if (stkMap[maxFrequency].Count == 0)
            {
                maxFrequency--;
            }

            freq[res]--;

            return res;
        }
    }
}