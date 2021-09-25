using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class Heap<T>
    {
        bool minHeap = true;
        IComparer<T> comparer = null;
        List<T> arr = null;

        Func<T,T,bool> comparerFunc = null;

        public int Count
        {
            get
            {
                return arr.Count;
            } 
        }

        public T Peek()
        {
            return arr[0];
        }

        public Heap(bool minHeap, IComparer<T> comparer = null)
        {
            arr = new List<T>();
            this.minHeap = minHeap;
            this.comparer = comparer == null ? new MyComparer() : comparer;
        }

        public Heap(bool minHeap, Func<T, T, int> comparerFunc)
        {
            arr = new List<T>();
            this.minHeap = minHeap;
            this.comparer = comparer == null ? new MyComparer(comparerFunc) : comparer;
        }

        public Heap(bool minHeap, Func<T, int> compFunc)
        {
            arr = new List<T>();
            this.minHeap = minHeap;
            this.comparer = comparer == null ? new MyComparer(compFunc) : comparer;
        }


        public void Remove(T val)
        {
            arr.Remove(val);
            Heapify();
        }

        public void Push(T val)
        {
            arr.Add(val);
            Heapify();
        }

        private void Heapify()
        {
            int idx = arr.Count-1;

            while (idx > 0)
            {
                int parent = (idx / 2);

                if ((minHeap && comparer.Compare(arr[parent], arr[idx]) > 0))
                {
                    Swap(arr, parent, idx);
                }

                if((!minHeap && comparer.Compare(arr[idx], arr[parent]) > 0))
                {
                    Swap(arr, parent, idx);
                }

                idx--;
            }
        }

        public T Pop()
        {
            T res = arr[0];
            arr.RemoveAt(0);
            Heapify();
            return res;
        }

        private void Swap(List<T> arr, int source, int target)
        {
            T temp = arr[target];
            arr[target] = arr[source];
            arr[source] = temp;
        }

        public class MyComparer : IComparer<T>
        {
            Func<T, T, int> comparerFunc = null;
            Func<T, int> compFunc = null;

            public MyComparer()
            {

            }
            public MyComparer(Func<T, T, int> comparerFunc)
            {
                this.comparerFunc = comparerFunc;
            }

            public MyComparer(Func<T, int> compFunc)
            {
                this.compFunc = compFunc;
            }

            public int Compare(T x, T y)
            {
                if (typeof(T) == typeof(int))
                {
                    return Comparer<T>.Default.Compare(x, y);
                }
                else if (typeof(T) == typeof(SLLNode))
                {
                    object one = (object)x;
                    object other = (object)y;
                    return ((SLLNode)one).Value - ((SLLNode)other).Value;
                }
                else if (comparerFunc != null)
                {
                    return this.comparerFunc(x, y);
                }
                else if (compFunc != null)
                {
                    return this.compFunc(x);
                }

                return 0;
            }
        }

        public class ArrayComparer : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                if (typeof(T) == typeof(int[]))
                {
                    object one = (object)x;
                    object other = (object)y;
                    int[] first = (int[])one;
                    int[] second = (int[])other;
                    return first[0] - second[0];
                }

                return 0;
            }
        }

        public class KeyValueComparer : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                if (typeof(T) == typeof(KeyValuePair<int, int>))
                {
                    object one = (object)x;
                    object other = (object)y;
                    return ((KeyValuePair<int, int>)one).Value - ((KeyValuePair<int, int>)other).Value;
                }

                return 0;
            }
        }
    }

    public class DSU
    {
        private int[] size;
        private int[] root;
        public int Count;

        public DSU(int n)
        {
            size = new int[n];
            root = new int[n];
            Count = n;

            for(int i = 0; i < n; i++)
            {
                root[i] = i;
            }
        }

        public int Find(int x)
        {
            if (root[x] != x)
            {
                root[x] = Find(root[x]);
            }

            return root[x];
        }

        public bool Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            if (rootX == rootY)
            {
                return false;
            }

            if (size[rootX] <= size[rootY])
            {
                root[rootX] = rootY;
                size[rootY]++;
            }
            else
            {
                root[rootY] = rootX;
                size[rootX]++;
            }

            Count--;

            return true;
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val=0, ListNode next=null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public class Trie
    {
        public TrieNode Root = null;

        public Trie()
        {
            this.Root = new TrieNode();
        }

        public void Insert(string s)
        {
            Insert(s.ToCharArray());
        }

        public void Insert(char[] s)
        {
            TrieNode cur = Root;
            StringBuilder sb = new StringBuilder();
             
            for(int idx = 0; idx < s.Length; idx++)
            {
                if (!cur.Children.ContainsKey(s[idx]))
                {
                    cur.Children.Add(s[idx], new TrieNode());
                }

                sb.Append(s[idx]);

                if (idx == s.Length-1)
                {
                cur.IsEndOfWord = true;
                cur.Word = sb.ToString();
                cur.Suggestions.Add(sb.ToString());
                }

                cur = cur.Children[s[idx]];
            }
        }

        public int[] InsertWithIndex(string s, int i, TrieNode cur)
        {
            int[] arr = new int[2];
            arr[0] = -1;
            arr[1] = -1;
            int max = int.MinValue;
            int curIdx = i;

            int[] res = new int[2];

            for(int idx = 0; idx < s.Length; idx++)
            {
                if (!cur.Children.ContainsKey(s[idx]))
                {
                    arr[0] = -1;
                    arr[1] = -1;
                    curIdx = -1;

                    cur.Children.Add(s[idx], new TrieNode());
                }
                else
                {
                    arr[0] = arr[0] == -1 ? i++ : arr[0];
                    arr[1] = curIdx++;

                    if(arr[0] > -1 && max < arr[1] - arr[0])
                    {
                        res[0] = arr[0];
                        res[1] = arr[1];
                        max = res[1] - res[0];
                    }
                }

                cur = cur.Children[s[idx]];
            }

            return res;
        }

        public List<string> Search(string s)
        {
            TrieNode cur = Root;
            TrieNode prev = Root;

            for(int idx = 0; idx <= s.Length; idx++)
            {
                if (idx == s.Length || !cur.Children.ContainsKey(s[idx]))
                {
                    prev = cur;
                    break;
                }

                prev = cur;
                cur = cur.Children[s[idx]];
            }

            return prev.Suggestions;
        }
    }

    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
        public List<string> Suggestions = new List<string>();
        public bool IsEndOfWord;

        public string Word {get; set;}

        public TrieNode()
        {
        }
    }

    public class BST
    {
        public static BST Root = null;
        public BST(int val)
        {
            this.Value = val;

            Root = Root == null ? this : Root;
        }

        public BST()
        {
            Root = Root == null ? this : Root;
        }

        public BST Left;
        public BST Right;
        public int Value;
        public int SmallNumbers;

        public BST Add(int val, BST node, ref int small)
        {
            if (node == null)
            {
                var newNode = new BST(val);
                newNode.SmallNumbers = small;

                return newNode;
            }

            if (node.Value > val)
            {
                node.Left = Add(val, node.Left, ref small);
            }
            else
            {
                small +=1;
                node.Right = Add(val, node.Right, ref small);
            }

            return node;
        }
    }

    public class UndirectedGraph<U>
    {
        public Dictionary<U, bool> Vertices = new Dictionary<U, bool>();
        public Dictionary<U, HashSet<U>> AdjList = new Dictionary<U, HashSet<U>>();

        public void AddEdge(U source, U edge)
        {
            AddEdges(source, edge);
            AddEdges(edge, source);
        }

        private void AddEdges(U source, U edge)
        {
            if (!Vertices.ContainsKey(source))
            {
                Vertices.Add(source, false);
            }

            if (!AdjList.ContainsKey(source))
            {
                AdjList.Add(source, new HashSet<U>());
            }

            AdjList[source].Add(edge);
        }
    }

    public class UndirectedGraphNode
    {
        public int val;
        public List<UndirectedGraphNode> neighbors = new List<UndirectedGraphNode>();

        public UndirectedGraphNode(int val)
        {
            this.val = val;
            this.neighbors = new List<UndirectedGraphNode>();
        }

        public UndirectedGraphNode(int val, List<UndirectedGraphNode> neighbors)
        {
            this.val = val;
            this.neighbors = neighbors;
        }
    }

    public class GraphNode
    {
        public int val;
        public IList<GraphNode> neighbors;
        
        public GraphNode()
        {
            val = 0;
            neighbors = new List<GraphNode>();
        }

        public GraphNode(int _val)
        {
            val = _val;
            neighbors = new List<GraphNode>();
        }
        
        public GraphNode(int _val, List<GraphNode> _neighbors)
        {
            val = _val;
            neighbors = _neighbors;
        }
    }

    public class Node
    {
        public Node(string word, int idx)
        {
            this.Word = word;
            this.Idx = idx;
        }

        public string Word;

        public int Idx;

    }

    public class BinaryMatrix
    {
        int[,] matrix = null;

        public BinaryMatrix(int[,] matrix)
        {
            this.matrix = matrix;
        }

        public int Get(int row, int col)
        {
            return this.matrix[row, col];
        }

        public IList<int> Dimensions()
        {
            return new List<int>() { matrix.GetLength(0),matrix.GetLength(1) };
        }
    }

    public class Robot {
     // Returns true if the cell in front is open and robot moves into the cell.
     // Returns false if the cell in front is blocked and robot stays in the current cell.
     public bool Move()
     {
         return true;
     }
 
     // Robot will stay in the same cell after calling turnLeft/turnRight.
     // Each turn will be 90 degrees.
      public void TurnLeft()
      {

      }
      public void TurnRight()
      {

      }
 
      // Clean the current cell.
      public void Clean()
      {
          
      }
  }
}