using System;
using System.Collections.Generic;


public class Graph
{

    public void CourseScheduling()
    {
        List<Pair> courses = new List<Pair>();
        courses.Add(new Pair(1,0));
        courses.Add(new Pair(2,0));
        courses.Add(new Pair(3,1));
        courses.Add(new Pair(3,2));

        //[1,0],[2,0],[3,1],[3,2]
        DirectedGraph graph = ConstructDirectedGraph(courses);
        Stack<int> stk = new Stack<int>();

        CourseScheduling(0, graph, stk);
        int[] arr = new int[graph.Vertices.Count];
        int idx = 0;

        foreach(int num in stk)
        {
            arr[idx++] = num;
        }
    }

    private void CourseScheduling(int vertex, DirectedGraph graph, Stack<int> stk)
    {
        foreach(int dependency in graph.AdjList[vertex])
        {
            CourseScheduling(dependency, graph, stk);
        }

        stk.Push(vertex);
    }

    private DirectedGraph ConstructDirectedGraph(List<Pair> pairs)
    {
        DirectedGraph graph = new DirectedGraph();

        foreach(Pair pair in pairs)
        {
            graph.AddEdge(pair.x, pair.y);
        }

        return graph;
    }

    //https://leetcode.com/contest/weekly-contest-97/problems/possible-bipartition/
    public void BiPartition()
    {
        Pair p1 = new Pair(1, 2);
        Pair p2 = new Pair(2, 4);
        Pair p3 = new Pair(6,4);
        Pair p4 = new Pair(5, 2);
        Pair p5 = new Pair(3, 7);
        Pair p6 = new Pair(4, 1);

        List<Pair> dislikes = new List<Pair>();
        dislikes.AddRange(new Pair[] {p1, p2, p3, p4, p5, p6});

        Console.WriteLine(BiPartition(dislikes));
    }

    private bool BiPartition(List<Pair> dislikes)
    {
        UndirectedGraph graph = ConstructGraph(dislikes);
        HashSet<int> A = new HashSet<int>();
        HashSet<int> B = new HashSet<int>();
        int vertex = 1;

        while(vertex < graph.Vertices.Count)
        {
            if (!graph.Vertices[vertex])
            {
                if (! Dfs(vertex, -1, graph, A, B))
                {
                    return false;
                }
            }

            vertex ++;
        }

        return true;
    }

    private UndirectedGraph ConstructGraph(List<Pair> dislikes)
    {
        UndirectedGraph graph = new UndirectedGraph();

        foreach(Pair p in dislikes)
        {
            graph.AddEdge(p.x, p.y);
        }

        return graph;
    }

    private bool Dfs(int vertex, int parent, UndirectedGraph graph, HashSet<int> A, HashSet<int> B)
    {
        if (graph.Vertices[vertex])
        {
            return true;
        }

        foreach(int neighbor in graph.AdjList[vertex])
        {
            if (neighbor == parent)
            {
                continue;
            }

            if (A.Contains(vertex))
            {
                if (A.Contains(neighbor))
                {
                    return false;
                }

                B.Add(neighbor);
            }
            else if (B.Contains(vertex))
            {
                if (B.Contains(neighbor))
                {
                    return false;
                }

                A.Add(neighbor);
            }
            else
            {
                A.Add(vertex);
                B.Add(neighbor);
            }

            if (!Dfs(neighbor, vertex, graph, A, B))
            {
                return false;
            }
        }

        graph.Vertices[vertex] = true;

        return true;
    }

    //https://leetcode.com/discuss/interview-question/356981
    public void MinCostToConnectNodes()
    {
        int[][] input = new int[][]
        {
            new int[] {1, 4},
            new int[] {4, 5},
            new int[] {2, 3}
        };

        Tuple<int, int, int> newEdge1 = new Tuple<int, int, int>(1, 2, 5);
        Tuple<int, int, int> newEdge2 = new Tuple<int, int, int>(1, 3, 10);
        Tuple<int, int, int> newEdge3 = new Tuple<int, int, int>(1, 6, 2);
        Tuple<int, int, int> newEdge4 = new Tuple<int, int, int>(5, 6, 5);
        List<Tuple<int, int, int>> newEdges = new List<Tuple<int, int, int>>(){newEdge1, newEdge2, newEdge3, newEdge4};

        Console.WriteLine(MinCostToConnectNodes(input, newEdges, 6));
    }

    private int MinCostToConnectNodes(int[][] input, List<Tuple<int, int, int>> newEdges, int n)
    {
        int[] size = new int[n+1];
        int[] root = new int[n+1];

        for(int idx = 0; idx <= n; idx ++)
        {
            root[idx] = idx;
            size[idx] = 1;
        }

        foreach(int[] arr in input)
        {
            Union(arr[0], arr[1], size, root);
        }

        int count = 0;

        foreach(Tuple<int, int, int> newEdge in newEdges)
        {
            if (Find(newEdge.Item1, root) == Find(newEdge.Item2, root))
            {
                continue;
            }

            count += newEdge.Item3;

            Union(newEdge.Item1, newEdge.Item2, size, root);
        }

        return count;
    }

    private void Union(int x, int y, int[] size, int[] root)
    {
        int rootX = Find(x, root);
        int rootY = Find(y, root);

        if (rootX == rootY)
        {
            return;
        }

        if (size[x] >= size[y])
        {
            root[y] = rootX;
            size[rootX] ++;
        }
        else
        {
            root[x] = rootY;
            size[rootY]++;
        }

        return;
    }

    private int Find(int x, int[] root)
    {
        if (root[x] != x)
        {
           root[x] = Find(root[x], root);
        }

        return root[x];
    }
}

public class UndirectedGraph
{
    public Dictionary<int, bool> Vertices = new Dictionary<int, bool>();
    public Dictionary<int, HashSet<int>> AdjList = new Dictionary<int, HashSet<int>>();


    public void AddEdge(int source, int edge)
    {
        AddEdges(source, edge);
        AddEdges(edge, source);
    }

    private void AddEdges(int source, int edge)
    {
        if (!Vertices.ContainsKey(source))
        {
            Vertices.Add(source, false);
        }
 
        if (!AdjList.ContainsKey(source))
        {
            AdjList.Add(source, new HashSet<int>());
        }

        AdjList[source].Add(edge);
    }
}

public class DirectedGraph
{
    public HashSet<int> Vertices;

    public Dictionary<int, List<int>> AdjList;

    public DirectedGraph()
    {
        this.Vertices = new HashSet<int>();
        this.AdjList = new Dictionary<int, List<int>>();
    }

    public void AddEdge(int vertex, int edge)
    {
        if (!this.Vertices.Contains(vertex))
        {
            this.Vertices.Add(vertex);
        }

        if (!this.AdjList.ContainsKey(vertex))
        {
            this.AdjList.Add(vertex, new List<int>());
        }

        this.AdjList[vertex].Add(edge);
    }
}