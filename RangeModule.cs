using System;
using System.Collections.Generic;

public class RangeModule
{
    List<int[]> ranges = null;
    
    public RangeModule() 
    {
        ranges = new List<int[]>();
    }
    
    public void Solve()
    {
        string[] cmd = new string[] {"addRange","addRange","addRange","queryRange","queryRange","queryRange","removeRange","queryRange"};
        int[][] ranges = new int[][] 
        {
            new int[]{10,180},
            new int[]{150,200},
            new int[]{250,500},
            new int[]{50,100},
            new int[]{180,300},
            new int[]{600, 1000},
            new int[]{50, 150},
            new int[]{10, 49},
        };

        for(int idx = 0; idx < cmd.Length; idx++)
        {
            if (cmd[idx] == "addRange")
            {
                AddRange(ranges[idx][0], ranges[idx][1]);
            }
            else if (cmd[idx] == "removeRange")
            {
                RemoveRange(ranges[idx][0], ranges[idx][1]);
            }
            else
            {
                Console.WriteLine(QueryRange(ranges[idx][0], ranges[idx][1]));
            }
        }
    }

    public void AddRange(int left, int right) 
    {
        if (left > right)
        {
            return;
        }
        
        if (ranges.Count == 0)
        {
            ranges.Add(new int[] {left, right});
            return;
        }
        
        AddRangeInternal(left, right);
    }
    
    public bool QueryRange(int left, int right) 
    {
        return QueryRangeInternal(left, right);
    }
    
    public void RemoveRange(int left, int right)
    {
        if (left > right)
        {
            return;
        }
        
        int[] res = null;
        for(int idx = 0; idx < ranges.Count; idx++)
        {
            if (left > ranges[idx][1] || right < ranges[idx][0])
            {
                continue;
            }
            
            var cur = ranges[idx];
            ranges.Remove(cur);
            //left > cur[0] && left < cur[1] && right > cur[1]
            if (left > cur[0]  && left < cur[1] && right > cur[1])
            {
                res = new int[]{cur[0], left};
                ranges.Add(res);
            }
            //left < cur[0] && right > cur[0] && right < cur[1]
            else if (left < cur[0] && right > cur[0]  && right < cur[1])
            {
                res = new int[]{right, cur[1]};
                ranges.Add(res);
            }
            else
            {
                res = new int[]{cur[0], left};
                ranges.Add(res);
                res = new int[]{right, cur[1]};
                ranges.Add(res);
                
            }

            return;
        }
    }
    
    private void AddRangeInternal(int left, int right)
    {
        for(int idx = 0; idx < ranges.Count; idx++)
        {
            if (left > ranges[idx][1] || right < ranges[idx][0])
            {
                continue;
            }
            
            var cur = ranges[idx];
            int start = left, end = right;

            if (left > cur[0] && left < cur[1] && right > cur[1])
            {
                end = right;
                ranges.Remove(cur);
            }
            else if (left < cur[0] && right > cur[0] && right < cur[1])
            {
                start = left;
                ranges.Remove(cur);
            }
        }
        
        ranges.Add(new int[] {left, right});
        return;
    }

    private bool QueryRangeInternal(int left, int right)
    {
        for(int idx = 0; idx < ranges.Count; idx++)
        {
            if (left > ranges[idx][1] || right < ranges[idx][0])
            {
                continue;
            }

            var cur = ranges[idx];

            if (left >= cur[0] && right <= cur[1])
            {
                return true;
            }
        }

        return false;
    }
}