using DataStructures;
using System;
using System.Text;
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

    //https://leetcode.com/problems/cheapest-flights-within-k-stops/
    public void FindCheapestPrice()
    {
        int[][] flights = new int[][]
        {
            new int[] {0,1,100},
            new int[] {1,2,100},
            new int[] {0,2,500}
        };

        Console.WriteLine(FindCheapestPrice(3,flights, 0, 2, 1));
    }

    private int FindCheapestPrice(int n, int[][] flights, int src, int dest, int k)
    {
        Dictionary<int, List<int[]>> prices = new Dictionary<int, List<int[]>>();
        foreach (int[] f in flights) 
        {
            if (!prices.ContainsKey(f[0]))
            {
                prices.Add(f[0], new List<int[]>());
            }

            prices[f[0]].Add(new int[]{f[1], f[2]});
        }

        Heap<int[]> pq = new Heap<int[]>(true, new ArrayComparer());

        //              Price, src, stops
        pq.Push(new int[] {0, src, k + 1});

        while (pq.Count > 0)
        {
            int[] top = pq.Pop();
            int price = top[0];
            int city = top[1];
            int stops = top[2];

            if (city == dest)
            {
                return price;
            }

            if (stops > 0)
            {
                List<int[]> adj = prices.GetValueOrDefault(city, new List<int[]>());

                foreach (int[] a in adj)
                {
                    pq.Push(new int[] {price + a[1], a[0], stops - 1});
                }
            }
        }

        return -1;
    }

    //Accepted-LCMedium-LCSol-O(V+E) S:O(V) https://leetcode.com/problems/is-graph-bipartite/
    public void IsBipartite()
    {
        int[][] graph = new int[][]
        {
            new int[] {1,3},
            new int[]{0,2},
            new int[]{1,3},
            new int[]{0,2}
        };

        Console.WriteLine(IsBipartite(graph));
    }

    private bool IsBipartite(int[][] graph)
    {
        Queue<int> queue = new Queue<int>();
        int[] colors = new int[graph.Length];

        for(int idx = 0; idx < graph.Length; idx++)
        {
            if (colors[idx] != 0)
            {
                continue;
            }

            queue.Enqueue(idx);
            colors[idx] = 1;

            while (queue.Count > 0)
            {
                int cur = queue.Dequeue();

                foreach(int neighbor in graph[cur])
                {
                    if (colors[neighbor] == 0)
                    {
                        colors[neighbor] = -colors[cur];
                        queue.Enqueue(neighbor);
                    }
                    else if (colors[neighbor] == colors[cur])
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    //https://leetcode.com/problems/possible-bipartition/
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

        int[][] arr = new int[][]
        {
            new int[] {1,2},
            new int[] {1,3},
            new int[] {2,4}
        };
        Console.WriteLine(BiPartitionUsingGraphColor(5, arr));
    }

    private bool BiPartitionUsingGraphColor(int N, int[][] dislikes)
    {
        if (N ==1)
        {
            return true;
        }

        int[] color = new int[N+1];
        Dictionary<int, List<int>> adj = new Dictionary<int, List<int>>();

        foreach(int[] arr in dislikes)
        {
            if (!adj.ContainsKey(arr[0]))
            {
                adj.Add(arr[0], new List<int>());
            }

            adj[arr[0]].Add(arr[1]);

            if (!adj.ContainsKey(arr[1]))
            {
                adj.Add(arr[1], new List<int>());
            }

            adj[arr[1]].Add(arr[0]);
        }

        for(int idx = 1; idx<= N; idx++)
        {
            if (color[idx] == 0)
            {
                color[idx] = 1;
                Queue<int> queue = new Queue<int>();
                queue.Enqueue(idx);

                while (queue.Count > 0)
                {
                    int cur = queue.Dequeue();

                    if (!adj.ContainsKey(cur))
                    {
                        break;
                    }

                    foreach(int neighbor in adj[cur])
                    {
                        if (color[neighbor] == 0)
                        {
                            color[neighbor] = color[cur] == 1 ? 2 : 1;
                            queue.Enqueue(neighbor);
                        }
                        else
                        {
                            if (color[neighbor] == color[cur])
                            {
                                return false;
                            }
                        }
                    }
                }
            }
        }

        return true;
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
            new int[] {1,2,5},
            new int[] {1,3,6},
            new int[] {2,3,1}
        };

        Console.WriteLine(MinCostToConnectNodes(3, input));

        // Tuple<int, int, int> newEdge1 = new Tuple<int, int, int>(1, 2, 5);
        // Tuple<int, int, int> newEdge2 = new Tuple<int, int, int>(1, 3, 10);
        // Tuple<int, int, int> newEdge3 = new Tuple<int, int, int>(1, 6, 2);
        // Tuple<int, int, int> newEdge4 = new Tuple<int, int, int>(5, 6, 5);
        // List<Tuple<int, int, int>> newEdges = new List<Tuple<int, int, int>>(){newEdge1, newEdge2, newEdge3, newEdge4};

        // Console.WriteLine(MinCostToConnectNodes_OldImpl(input, newEdges, 6));
    }

    private int MinCostToConnectNodes(int N, int[][] connections)
    {
        Array.Sort(connections, (a,b) => a[2]- b[2]);

        DSU dsu = new DSU(N+1);
        int cost = 0;

        foreach(int[] arr in connections)
        {
            if (!dsu.Union(arr[0], arr[1]))
            {
                cost+= arr[2];
            }
        }

        return dsu.Count == 2 ? cost : -1;
    }

    private int MinCostToConnectNodes_OldImpl(int[][] input, List<Tuple<int, int, int>> newEdges, int n)
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

    /*
    Given information about the links between various data centers, find the groups of isolated but connected data centers. 
    For example --> Input : {A<->B, B<->C, D<->E}, Output : {A, B, C}, {D, E}
    */
    public void DisjointSets()
    {
        int[][] edges = new int[][]
        {
            new int[]{2, 3},
            new int[]{1, 2},
            new int[]{1, 3},
            //new int[]{2, 4},
        };

        Console.WriteLine(DisjointSets(4, edges));
    }

    private int DisjointSets(int n, int[][] edges)
    {
        int[] root = new int[n];

       foreach(int[] edge in edges)
        {
            root[edge[0]] = edge[0];
            root[edge[1]] = edge[1];
        }
        
        foreach(int[] edge in edges)
        {
            int root1 = Find(edge[0], root);
            int root2 = Find(edge[1], root);
            
            if (root1 != root2)
            {
                root[root1] = root[root2];
                n--;
            }
        }
        
        return n;
    }

    //https://leetcode.com/problems/course-schedule/
    public void CourseSchedule()
    {
        DirectedGraph graph = new DirectedGraph(3);
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 2);

        Console.WriteLine(CanFinishCourse(graph));
    }

    private bool CanFinishCourse(DirectedGraph graph)
    {
        for(int idx = 0; idx < graph.Vertices; idx++)
        {
            if (! CanFinishCoursesDFS(graph, idx))
            {
                return false;
            }
            graph.Visited.Clear();
        }

        return true;
    }

    private bool CanFinishCoursesDFS(DirectedGraph graph, int idx)
    {
        if (graph.Visited.Contains(idx))
        {
            return false;
        }

        graph.Visited.Add(idx);

        if (!graph.AdjList.ContainsKey(2))
        {
            return true;
        }

        foreach(int i in graph.AdjList[idx])
        {
            if (!CanFinishCoursesDFS(graph, i))
            {
                return false;
            }
        }

        return true;
    }

    //https://leetcode.com/problems/course-schedule-ii/
    public void CourseScheduleII()
    {
        int[][] prerequisites = new int[][]
        {
            new int[]{1,0},
            new int[]{2,0},
            new int[]{3,0},
            new int[]{4,0},
        };
        int numCourses = 4;

        IDictionary<int, List<int>> map = new Dictionary<int, List<int>>();

        foreach(int[] arr in prerequisites)
        {
            if (!map.ContainsKey(arr[0]))
            {
                map.Add(arr[0], new List<int>());
            }

            map[arr[0]].Add(arr[1]);
        }

        IList<int> res = new List<int>();
        bool[] visited = new bool[numCourses];
        for(int i = 0; i < numCourses; i++)
        {
            CourseScheduleII(numCourses, map, visited, i, res);
        }
    }

    private IList<int> CourseScheduleII(int numCourses, IDictionary<int, List<int>> map, bool[] visited, int idx, IList<int> res)
    {
        if (visited[idx])
        {
            return res;
        }

        visited[idx] = true;

        if (map.ContainsKey(idx))
        {
            foreach(int prerequisite in map[idx])
            {
                CourseScheduleII(numCourses, map, visited, prerequisite, res);
            }
        }

        res.Add(idx);

        return res;
    }

    //https://leetcode.com/problems/alien-dictionary/
    public void AlienDictionary()
    {
        // string[] s = new string[]
        // {
        //   "wrt",
        //   "wrf",
        //   "er",
        //   "ett",
        //   "rftt"
        // };

        string[] s = new string[]
        {
            "abc","ab"
        };

        // AlienDictionary(s, new DirectedGraph(5));
        //var res = AlienDictionaryUsingBFSQueue(s);
        var res = AlienDictionary(s);
        Console.WriteLine(res);
    }

    public String AlienDictionaryUsingBFSQueue(String[] words)
    {
        Dictionary<char,HashSet<char>> map = new Dictionary<char, HashSet<char>>();
        Dictionary<char,int> degree=new Dictionary<char, int>();
        String result = "";

        if(words==null || words.Length == 0 )
        {
            return result;
        }

        foreach(string s in words)
        {
            foreach(char c in s.ToCharArray())
            {
                degree.TryAdd(c, 0);
            }
        }

        for(int i=0; i < words.Length-1; i++)
        {
            String cur = words[i];
            String next = words[i+1];

            int length = Math.Min(cur.Length, next.Length);

            for(int j = 0; j < length; j++)
            {
                char c1=cur[j];
                char c2=next[j];

                if(c1 != c2)
                {
                    HashSet<char> set=new HashSet<char>();
                    if(map.ContainsKey(c1))
                    {
                        set=map[c1];
                    }

                    if(!set.Contains(c2))
                    {
                        set.Add(c2);
                        map.TryAdd(c1, set);
                        degree[c2] += 1;
                    }

                    break;
                }
            }
        }

        Queue<char> q = new Queue<char>();
        foreach(char c in degree.Keys)
        {
            if(degree[c] == 0)
            {
                q.Enqueue(c);
            }
        }

        //Ones with degree: 0 will be the first ones lexicographically. Reduce the degree of others in the queue.
        while(q.Count > 0)
        {
            char c = q.Dequeue();
            result += c;

            if(map.ContainsKey(c))
            {
                foreach(char c2 in map[c])
                {
                    degree[c2] -= 1;

                    if(degree[c2] == 0)
                    {
                        q.Enqueue(c2);
                    }
                }
            }
        }

        if(result.Length != degree.Count)
        {
            return "";
        }

        return result;
    }

    private string AlienDictionary(string[] words)
    {
        Dictionary<char, HashSet<char>> map = new Dictionary<char, HashSet<char>>();
        Dictionary<char, int> degree = new Dictionary<char, int>();

        foreach(string str in words)
        {
            foreach(char ch in str)
            {
                degree[ch] = 0;
            }
        }

        for(int idx = 1; idx < words.Length; idx++)
        {
            string cur = words[idx-1];
            string next = words[idx];

            int len = Math.Min(words[idx-1].Length, words[idx].Length);

            for(int i = 0; i < len; i++)
            {
                char c1 = cur[i];
                char c2 = next[i];
                if (cur[i] != next[i])
                {
                    HashSet<char> set = new HashSet<char>();
                    if (map.ContainsKey(c1))
                    {
                        set = map[cur[i]];
                    }

                    if (!set.Contains(c2))
                    {
                        set.Add(c2);
                        map[c1] = set;
                        degree[c2]++;
                    }

                    break;
                }
            }
        }

        Queue<char> queue = new Queue<char>();

        foreach(KeyValuePair<char,int> pair in degree)
        {
            if (pair.Value == 0)
            {
                queue.Enqueue(pair.Key);
            }
        }

        StringBuilder sb = new StringBuilder();
        while (queue.Count > 0)
        {
            var cur = queue.Dequeue();
            sb.Append(cur);

            if (!map.ContainsKey(cur))
            {
                continue;
            }

            foreach(char ch in map[cur])
            {
                degree[ch]--;
                if (degree[ch] == 0)
                {
                    queue.Enqueue(ch);
                }
            }
        }

        return sb.ToString().Length != degree.Count ? "" : sb.ToString();
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

    //https://leetcode.com/problems/clone-graph/
    public void CloneGraphCre()
    {
        UndirectedGraphNode node1 = new UndirectedGraphNode(1);
        UndirectedGraphNode node2 = new UndirectedGraphNode(2);
        UndirectedGraphNode node3 = new UndirectedGraphNode(3);
        UndirectedGraphNode node4 = new UndirectedGraphNode(4);

        node1.neighbors.AddRange(new UndirectedGraphNode[] {node2, node4});
        node2.neighbors.AddRange(new UndirectedGraphNode[] {node1, node3});
        node3.neighbors.AddRange(new UndirectedGraphNode[] {node2, node4});
        node4.neighbors.AddRange(new UndirectedGraphNode[] {node1, node3});

        var res = CloneGraphCre(node1, new Dictionary<int, UndirectedGraphNode>());
    }

    private UndirectedGraphNode CloneGraph(UndirectedGraphNode node, Dictionary<int, UndirectedGraphNode> map)
    {
        if (node == null)
        {
            return null;
        }

        if (map.ContainsKey(node.val))
        {
            return map[node.val];
        }

        UndirectedGraphNode clone = new UndirectedGraphNode(node.val);

        foreach(UndirectedGraphNode neighbor in node.neighbors)
        {
            var cloneNeighbor = CloneGraph(neighbor, map);
            clone.neighbors.Add(cloneNeighbor);
        }

        return clone;
    }

    private UndirectedGraphNode CloneGraphCre(UndirectedGraphNode node, Dictionary<int, UndirectedGraphNode> map)
    {
        if (node == null)
        {
            return null;
        }

        if (map.ContainsKey(node.val))
        {
            return map[node.val];
        }

        UndirectedGraphNode cloneNode = new UndirectedGraphNode(node.val);
        map.Add(node.val, cloneNode);

        foreach(UndirectedGraphNode neighbor in node.neighbors)
        {
            var cloneNeighbor = CloneGraphCre(neighbor, map);
            cloneNode.neighbors.Add(cloneNeighbor);
        }

        return cloneNode;
    }

    //https://leetcode.com/problems/redundant-connection/
    public void RedundantConnection()
    {
        int[][] edges = new int[][]
        {
            new int[] {1,2},
            new int[] {2,3},
            new int[] {3,4},
            new int[] {1,4},
            new int[] {1,5}
        };

        var res = FindRedundantConnection(edges);
        Console.WriteLine(res[0].ToString(), res[1].ToString());
    }

    private int[] FindRedundantConnection(int[][] edges)
    {
        DSU dsu  = new DSU(edges.Length);

        foreach(int[] edge in edges)
        {
            if (!dsu.Union(edge[0], edge[1]))
            {
                return edge;
            }
        }

        throw new ArgumentException("Invalid argument");
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

    //LCMedium: LCSol: T:O(n^2): https://leetcode.com/problems/accounts-merge/
    public void AccountsMerge()
    {
        IList<IList<string>> contacts = new List<IList<string>>();
        var contact1 = new List<string>();
        contact1.Add("john");
        contact1.Add("john00@mail.com");
        contact1.Add("johnsmith@mail.com");

        var contact2 = new List<string>();
        contact2.Add("john");
        contact2.Add("johnnybravo@mail.com");

        var contact3 = new List<string>();
        contact3.Add("john");
        contact3.Add("johnsmith@mail.com");
        contact3.Add("john_newyork@mail.com");

        var contact4 = new List<string>();
        contact4.Add("mary");
        contact4.Add("mary@mail.com");

        contacts.Add(contact1);
        contacts.Add(contact2);
        contacts.Add(contact3);
        contacts.Add(contact4);

        var res = AccountsMerge(contacts);
    }

    private IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
    {
        Dictionary<string, string> parent = new Dictionary<string, string>();
        Dictionary<string, string> owners = new Dictionary<string, string>();
        Dictionary<string, List<string>> union = new Dictionary<string, List<string>>();
        IList<IList<string>> res = new List<IList<string>>();

        foreach(IList<string> account in accounts)
        {
            for(int idx = 1; idx < account.Count; idx++)
            {
                if (!parent.ContainsKey(account[idx]))
                {
                    parent.Add(account[idx], account[idx]);
                }

                if (!owners.ContainsKey(account[idx]))
                {
                    owners.Add(account[idx], account[0]);
                }
            }
        }

        foreach(IList<string> account in accounts)
        {
            var p = parent[account[1]];

            for(int idx = 2; idx < account.Count; idx++)
            {
                var contactParent = FindParent(account[idx], parent);
                parent[contactParent] = p;
            }
        }

        foreach(IList<string> account in accounts)
        {
            var p = FindParent(account[1], parent);

            for(int idx = 1;idx < account.Count; idx++)
            {
                if (! union.ContainsKey(p))
                {
                    union.Add(p, new List<string>());
                }

                union[p].Add(account[idx]);
            }
        }

        foreach(string p in union.Keys)
        {
            var list = new List<string>();
            list.Add(owners[p]);
            list.AddRange(union[p]);
            res.Add(list);
        }

        return res;
    }

    private IList<IList<string>> AccountMerge(IList<IList<string>> accounts)
    {
       Dictionary<string, string> parentMap = new Dictionary<string, string>();
       Dictionary<string, string> owners = new Dictionary<string, string>();
       Dictionary<string, List<string>> unions = new Dictionary<string, List<string>>();
       IList<IList<string>> res = new List<IList<string>>();

        foreach(IList<string> contacts in accounts)
        {
            for(int idx = 1; idx < contacts.Count; idx++)
            {
                //Each contact is a parent for itself
                if (!parentMap.ContainsKey(contacts[idx]))
                {
                    parentMap.Add(contacts[idx], contacts[idx]);
                }

                //Only one owner for contacts[idx]
                if (!owners.ContainsKey(contacts[idx]))
                {
                    owners.Add(contacts[idx], contacts[0]);
                }
            }
        }

        //Merge the parents for the contacts
        foreach(IList<string> contacts in accounts)
        {
            string p = FindParent(contacts[1], parentMap);

            for(int idx = 2; idx < contacts.Count; idx++)
            {
                var contactParent = FindParent(contacts[idx], parentMap);
                parentMap[contactParent] = p;
            }
        }

        //For a parent, union all the contacts
        foreach(IList<string> contacts in accounts)
        {
            string p = FindParent(contacts[1], parentMap);
            if (! unions.ContainsKey(p))
            {
                unions.Add(p, new List<string>());
            }

            for(int idx = 1; idx < contacts.Count; idx++)
            {
                if (! unions[p].Contains(contacts[idx]))
                {
                    unions[p].Add(contacts[idx]);
                }
            }
        }

        //Add the owner & contact list for that owner
        foreach(string p in unions.Keys)
        {
            var list = new List<string>();
            list.Add(owners[p]);
            unions[p].Sort();
            list.AddRange(unions[p]);
            res.Add(list);
        }

        return res;
    }

    private string FindParent(string contact, Dictionary<string, string> parentMap)
    {
        return parentMap[contact] == contact ? contact : FindParent(parentMap[contact], parentMap);
    }

    // private IList<HashSet<string>> AccountsMerge(IList<IList<string>> accounts)
    // {
    //     IList<HashSet<string>> res = new List<HashSet<string>>();

    //     UndirectedGraph<string> graph = new UndirectedGraph<string>();
        
    //     foreach(List<string> list in accounts)
    //     {
    //         for(int idx = 1; idx < list.Count; idx++)
    //         {
    //             if (!graph.Vertices.ContainsKey(list[idx]))
    //             {
    //                 graph.Vertices.Add(list[idx], false);
    //             }

    //             graph.AddEdge(list[idx], list[idx-1]);
    //         }
    //     }

    //     foreach(KeyValuePair<string, bool> contact in graph.Vertices)
    //     {
    //         var mergedContact = MergeContacts(graph, contact.Key, accounts);
    //         res.Add(mergedContact);
    //     }

    //     return res;
    // }

    // private HashSet<string> MergeContacts(UndirectedGraph<string> graph, string primary, IList<IList<string>> accounts)
    // {
    //     HashSet<String> contacts = new HashSet<String>();

    //     if (!graph.AdjList.ContainsKey(primary) || (graph.Vertices[primary]))
    //     {
    //         return contacts;
    //     }

    //     graph.Vertices[primary] = true;
    //     foreach(string contact in graph.AdjList[primary])
    //     {
    //         var res = MergeContacts(graph, contact, accounts);
    //         contacts.Add(contact);
    //         foreach(string str in res)
    //         {
    //             contacts.Add(str);
    //         }
    //     }

    //     return contacts;
    // }

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

    //https://leetcode.com/problems/course-schedule/
    public void CanFinishCourses()
    {
        int[][] prereq = new int[][]
        {
            new int[]{1,0},
            //new int[]{0,1}
        };

        int numCourses = 2;

        Console.WriteLine(CanFinishCourses(numCourses, prereq));
    }

    private bool CanFinishCourses(int numCourses, int[][] prerequisites)
    {
        Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();

        if (prerequisites == null || prerequisites.Length == 0)
        {
            return false;
        }


        foreach(int[] prerequisite in prerequisites)
        {
            if (!map.ContainsKey(prerequisite[0]))
            {
                map.Add(prerequisite[0], new List<int>());
            }

            map[prerequisite[0]].Add(prerequisite[1]);
        }

        return CanFinishCourses(map, prerequisites[0][0], numCourses, new HashSet<int>()) >= numCourses;
    }

    private int CanFinishCourses(Dictionary<int, List<int>> map, int course, int numCourses, HashSet<int> visited)
    {
        if (visited.Contains(course))
        {
            return -1;
        }

        if(!map.ContainsKey(course))
        {
            return 1;
        }

        int res = 0;
        visited.Add(course);

        foreach(int prerequisite in map[course])
        {
            var finishCourse = CanFinishCourses(map, prerequisite, numCourses, visited);

            if (finishCourse == -1)
            {
                return -1;
            }
            res += finishCourse;
        }

        map.Remove(course);
        return res = res == -1 ? -1 : res+1;
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