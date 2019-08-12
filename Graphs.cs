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