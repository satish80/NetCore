using DataStructures;
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
        int[] arr = new int[graph.Vertices];
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
        DirectedGraph graph = new DirectedGraph(pairs.Count);

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

    //https://leetcode.com/problems/connecting-cities-with-minimum-cost/
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

    //https://leetcode.com/problems/alien-dictionary/
    public void AlienDictionary()
    {
        string[] s = new string[]
        {
          "wrt",
          "wrf",
          "er",
          "ett",
          "rftt"
        };

        AlienDictionary(s, new DirectedGraph(5));
    }

    private void AlienDictionary(string[] dict, DirectedGraph graph)
    {
        for(int i = 0; i < dict.Length -1; i++)
        {
            string word1 = dict[i];
            string word2 = dict[i + 1];

            for(int j = 0; j < Math.Min(word1.Length, word2.Length); j ++)
            {
                if (word1[j] != word2[j])
                {
                    graph.AddEdge(word1[j] - 'a', word2[j] - 'a');
                    break;
                }
            }
        }

        Stack<int> stack = new Stack<int>();

        foreach(int vertex in graph.AdjList.Keys)
        {
            if (!graph.Visited.Contains(vertex))
            {
                TopologicalSortUtil(graph, vertex, stack);
            }
        }

        while (stack.Count > 0)
        {
            Console.WriteLine((char)('a' + stack.Pop()));
        }
    }

    private void TopologicalSortUtil(DirectedGraph graph, int vertex, Stack<int> stack)
    {
        graph.Visited.Add(vertex);

        if (graph.AdjList.ContainsKey(vertex))
        {
            foreach(int neighbor in graph.AdjList[vertex])
            {
                if (! graph.Visited.Contains(neighbor))
                {
                    TopologicalSortUtil(graph, neighbor, stack);
                }
            }
        }
        stack.Push(vertex);
    }

    //https://leetcode.com/problems/tree-diameter/
    public void TreeDiameter()
    {
        int[][] edges = new int[5][]
        {
            new int[] {0,1},
            new int[] {1,2},
            new int[] {2,3},
            new int[] {1,4},
            new int[] {4,5},
        };

        Console.WriteLine(TreeDiameter(edges));
    }

    private int TreeDiameter(int[][] edges)
    {
        UndirectedGraph graph = new UndirectedGraph();
        Dictionary<int, int> map = new Dictionary<int, int>();
        HashSet<int> visited = new HashSet<int>();

        for(int i = 0; i < edges.Length; i ++)
        {
            graph.AddEdge(edges[i][0], edges[i][1]);
        }

        int max = 0;

        for(int i = 0; i < edges.Length; i++)
        {
            int count = 0;
            map[i] = Dfs(graph, i, visited, ref count);
            max = Math.Max(max, map[i]);
        }

        return max;
    }

    private int Dfs(UndirectedGraph graph, int vertex, HashSet<int> visited, ref int count)
    {
        if (visited.Contains(vertex))
        {
            return count;
        }

        visited.Add(vertex);
        count += 1;

        foreach(int neighbor in graph.AdjList[vertex])
        {
            Dfs(graph, neighbor, visited, ref count);
        }

        visited.Remove(vertex);

        return count;
    }

    //https://leetcode.com/problems/accounts-merge/
    public void AccountsMerge()
    {
        IList<IList<string>> contacts = new List<IList<string>>();
        var contact1 = new List<string>();
        contact1.Add("johnsmith@mail.com");
        contact1.Add("john00@mail.com");

        var contact2 = new List<string>();
        contact2.Add("johnnybravo@mail.com");

        var contact3 = new List<string>();
        contact3.Add("johnsmith@mail.com");
        contact3.Add("john_newyork@mail.com");

        var contact4 = new List<string>();
        contact4.Add("mary@mail.com");

        contacts.Add(contact1);
        contacts.Add(contact2);
        contacts.Add(contact3);
        contacts.Add(contact4);

        var res = AccountsMerge(contacts);
    }

    private IList<HashSet<string>> AccountsMerge(IList<IList<string>> accounts)
    {
        IList<HashSet<string>> res = new List<HashSet<string>>();

        UndirectedGraph<string> graph = new UndirectedGraph<string>();
        
        foreach(List<string> list in accounts)
        {
            for(int idx = 1; idx < list.Count; idx++)
            {
                if (!graph.Vertices.ContainsKey(list[idx]))
                {
                    graph.Vertices.Add(list[idx], false);
                }

                graph.AddEdge(list[idx], list[idx-1]);
            }
        }

        foreach(KeyValuePair<string, bool> contact in graph.Vertices)
        {
            var mergedContact = MergeContacts(graph, contact.Key, accounts);
            res.Add(mergedContact);
        }

        return res;
    }

    private HashSet<string> MergeContacts(UndirectedGraph<string> graph, string primary, IList<IList<string>> accounts)
    {
        HashSet<String> contacts = new HashSet<String>();

        if (!graph.AdjList.ContainsKey(primary) || (graph.Vertices[primary]))
        {
            return contacts;
        }

        graph.Vertices[primary] = true;
        foreach(string contact in graph.AdjList[primary])
        {
            var res = MergeContacts(graph, contact, accounts);
            contacts.Add(contact);
            foreach(string str in res)
            {
                contacts.Add(str);
            }
        }

        return contacts;
    }

    //https://leetcode.com/problems/graph-valid-tree/
    public void ValidGraphTree()
    {
        int[][] edges = new int[4][]
        {
            new int[]{1, 0},
            new int[]{2, 0},
            new int[]{0, 3},
            new int[]{3, 1},
            //new int[]{1, 4},
        };

        Console.WriteLine(ValidGraphTree(5, edges));
    }

    #region Commented code
    /* Fails Leetcode test case
    
    private bool ValidGraphTree(int n, int[][] edges)
    {
        //         edge, parent
        Dictionary<int, int> parentMap = new Dictionary<int, int>();

        foreach(int[] edge in edges)
        {
            // edge[0] : parent
            // edge[1] : edge
            if (parentMap.ContainsKey(edge[1]))
            {
                return false;
            }

            parentMap.Add(edge[1], edge[0]);
        }

        if (n -1 != parentMap.Count)
        {
            return false;    
        }

        int top = -1;
        foreach(int[] edge in edges)
        {
            if (!parentMap.ContainsKey(edge[0]))
            {
                if (top != -1 && top != edge[0])
                {
                    return false;
                }

                top = edge[0];
            }
        }

        return true;
    }
    */
    #endregion

    public bool ValidGraphTree(int n, int[][] edges)
    {
        // initialize n isolated islands
        int[] nums = new int[n];
        for(int idx = 0; idx < n; idx ++)
        {
            nums[idx] = -1;
        }

        // perform union find
        for (int i = 0; i < edges.Length; i++)
        {
            int x = find(nums, edges[i][0]);
            int y = find(nums, edges[i][1]);

            // if two vertices happen to be in the same set
            // then there's a cycle
            if (x == y) return false;
            
            // union
            nums[x] = y;
        }

        return edges.Length == n - 1;
    }

    int find(int[] nums, int i)
    {
        if (nums[i] == -1)
        {
             return i;
        }

        return find(nums, nums[i]);
    }

    //https://leetcode.com/problems/reconstruct-itinerary/
    public void ReconstructItinerary()
    {
        IList<IList<string>> tickets = new List<IList<string>>();
        IList<string> t1 = new List<string>(new string[] {"JFK", "SFO"});
        IList<string> t2 = new List<string>(new string[] {"JFK", "ATL"});
        IList<string> t3 = new List<string>(new string[] {"SFO", "ATL"});
        IList<string> t4 = new List<string>(new string[] {"ATL", "JFK"});
        IList<string> t5 = new List<string>(new string[] {"ATL", "SFO"});
        tickets.Add(t1);
        tickets.Add(t2);
        tickets.Add(t3);
        tickets.Add(t4);
        tickets.Add(t5);

        var res = ReconstructItinerary(tickets);
    }

    private IList<string> ReconstructItinerary(IList<IList<string>> tickets)
    {
        Dictionary<string,Heap<string>> map = new Dictionary<string, Heap<string>>();

        foreach(IList<string> ticket in tickets)
        {
            if (!map.ContainsKey(ticket[0]))
            {
                map.Add(ticket[0], new Heap<string>(true));
            }

            map[ticket[0]].Push(ticket[1]);
        }

        var res = new List<string>(new string[] {"JFK"});
        ReconstructItinerary(map, "JFK", res);
        return res;
    }

    private void ReconstructItinerary(Dictionary<string,Heap<string>> map, string source, IList<string> itinerary)
    {
        if (!map.ContainsKey(source))
        {
            return;
        }

        while(map[source].Count > 0)
        {
            var dest = map[source].Pop();
            itinerary.Add(dest);
            ReconstructItinerary(map, dest, itinerary);
        }
    }

    //Accepted: T:O(V+E), S:O(V+E): https://leetcode.com/problems/clone-graph/
    public void CloneGraph()
    {
        GraphNode node = new GraphNode(1);
        GraphNode node2 = new GraphNode(2);
        GraphNode node3 = new GraphNode(3);
        GraphNode node4 = new GraphNode(4);

        node.neighbors.Add(node2);
        node.neighbors.Add(node4);

        node2.neighbors.Add(node);
        node2.neighbors.Add(node3);

        node3.neighbors.Add(node2);
        node3.neighbors.Add(node4);

        node4.neighbors.Add(node3);
        node4.neighbors.Add(node);

        var res = CloneGraph(node);
    }

    private GraphNode CloneGraph(GraphNode graph)
    {
        return CloneGraph(graph, new Dictionary<GraphNode, GraphNode>());
    }

    private GraphNode CloneGraph(GraphNode graph, Dictionary<GraphNode, GraphNode> cloneNodes)
    {
        if (graph == null)
        {
            return null;
        }

        if (cloneNodes.ContainsKey(graph))
        {
            return cloneNodes[graph];
        }

        GraphNode node = new GraphNode(graph.val);
        cloneNodes.Add(graph, node);

        foreach(GraphNode neighbor in graph.neighbors)
        {
            var cloneNode = CloneGraph(neighbor, cloneNodes);
            node.neighbors.Add(cloneNode);
        }

        return node;
    }

    //https://leetcode.com/problems/critical-connections-in-a-network/
    //Tarjan algorithm for strongly connected components: https://www.youtube.com/watch?v=TyWtx7q2D7Y
    public void CriticalConnections()
    {

    }

    //https://leetcode.com/problems/critical-connections-in-a-network/discuss/382632/Java-implementation-of-Tarjan-Algorithm-with-explanation
    private List<List<int>> CriticalConnections(int n, List<List<int>> connections)
    {
        int[] disc = new int[n], low = new int[n];
        // use adjacency list instead of matrix will save some memory, adjmatrix will cause MLE
        List<int>[] graph = new List<int>[n];
        List<List<int>> res = new List<List<int>>();

        for(int idx = 0; idx < disc.Length; idx ++)
        {
            disc[idx] = -1; // use disc to track if visited (disc[i] == -1)
        }
 
        for (int i = 0; i < n; i++)
        {
            graph[i] = new List<int>();
        }

        // build graph
        for (int i = 0; i < connections.Count; i++)
        {
            int from = connections[i][0], to = connections[i][1];
            graph[from].Add(to);
            graph[to].Add(from);
        }

        int time = 0;
        for (int i = 0; i < n; i++)
        {
            if (disc[i] == -1)
            {
                dfs(i, low, disc, graph, res, i, ref time);
            }
        }
        return res;
    }

    private void dfs(int u, int[] low, int[] disc, List<int>[] graph, List<List<int>> res, int pre, ref int time)
    {
        disc[u] = low[u] = ++time; // discover u
        for (int j = 0; j < graph[u].Count; j++)
        {
            int v = graph[u][j];

            if (v == pre)
            {
                continue; // if parent vertex, ignore
            }

            if (disc[v] == -1)
            {
                // if not discovered
                dfs(v, low, disc, graph, res, u, ref time);
            }

            // if v discovered and is not parent of u, update low[u], cannot use low[v] because u is not subtree of v
            low[u] = Math.Min(low[u], disc[v]);

            if (low[v] > disc[u])
            {
                // u - v is critical, there is no path for v to reach back to u or previous vertices of u
                var list = new List<int>{u, v};
                res.Add(list);
            }
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
        public Dictionary<int, List<int>> AdjList;
        public int Vertices;
        public HashSet<int> Visited;
        public DirectedGraph(int vertex)
        {
            this.Vertices = vertex;
            AdjList = new Dictionary<int, List<int>>(vertex);
            Visited = new HashSet<int>();
        }

        public void AddEdge(int vertex, int weight)
        {
            if (AdjList == null || ! AdjList.ContainsKey(vertex))
            {
                var list = new List<int>();
                list.Add(weight);
                AdjList.Add(vertex, list);
            }
            else
            {
                AdjList[vertex].Add(weight);
            }
        }
    }
}