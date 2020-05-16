using System.Collections.Generic;
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
}