using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class Heap<T>
    {
        bool minHeap = true;
        IComparer<T> comparer = null;
        List<T> arr = null;

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

                if ((minHeap && Comparer<T>.Default.Compare(arr[parent], arr[idx]) > 0))
                {
                    Swap(arr, parent, idx);
                }

                if((!minHeap && Comparer<T>.Default.Compare(arr[idx], arr[parent]) > 0))
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
            public int Compare(T x, T y)
            {
                if (typeof(T) == typeof(int))
                {
                    return Comparer<T>.Default.Compare(x, y);
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

    public class Trie
    {
        public TrieNode Root = null;

        public Trie()
        {
            this.Root = new TrieNode();
        }

        public class TrieNode
        {
            public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
            public List<string> Suggestions = new List<string>();
            public bool IsEndOfWord;

            public TrieNode()
            {
            }
        }

        public void Insert(string s)
        {
            TrieNode cur = Root;

            foreach(char ch in s)
            {
                if (!cur.Children.ContainsKey(ch))
                {
                    cur.Children.Add(ch, new TrieNode());
                }

                cur = cur.Children[ch];
                cur.Suggestions.Add(s);
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
}