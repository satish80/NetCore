using System.Collections.Generic;

public class Heap<T>
{
    bool minHeap = true;
    List<T> arr = null;

    public Heap(bool minHeap)
    {
        arr = new List<T>();
        this.minHeap = minHeap;
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
            IComparer<T> comparer = new MyComparer();

            if ((minHeap && comparer.Compare(arr[parent], arr[idx]) > 0))
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
}