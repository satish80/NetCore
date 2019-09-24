using System;
using System.Threading;
using System.Threading.Tasks;

public class ThreadedQueue
{
    SemaphoreSlim enqueueSema = new SemaphoreSlim(5, 5);
    SemaphoreSlim dequeueSema = new SemaphoreSlim(0, 5);

    Mutex mutex = new Mutex();
    int[] arr = new int[5];
    int eIdx = 0;
    int dIdx = 0;

    public ThreadedQueue()
    {
        SimulateThreads();
    }

    public void Enqueue(int val)
    {
        while (enqueueSema.CurrentCount == 0)
        {

        }
        
        mutex.WaitOne();
        enqueueSema.Wait();

        arr[eIdx] = val;
        Console.WriteLine($"enqueued {val} at index {eIdx}") ;

        eIdx = (eIdx + 1) % 5;
        Console.WriteLine($"New eIdx is {eIdx}") ;

        mutex.ReleaseMutex();
        dequeueSema.Release(1);
    }

    public int Dequeue()
    {
        int res = -1;

        while(dequeueSema.CurrentCount == 0)
        {

        }

        mutex.WaitOne();
        dequeueSema.Wait();

        res = arr[dIdx];
        Console.WriteLine($"dequeued {res} at index {dIdx}");

        dIdx = (dIdx + 1) % 5;
        Console.WriteLine($"New dIdx is {dIdx}") ;

        mutex.ReleaseMutex();
        enqueueSema.Release(1);

        return res;
    }

    public void SimulateThreads()
    {
        var t1 = Task.Run(()=> 
        {
            Enqueue(1);
        });

        var t2 = Task.Run(()=> 
        {
            Enqueue(2);
        });

        var t3 = Task.Run(()=> 
        {
            Enqueue(3);
        });

        var t4 = Task.Run(()=> 
        {
            Dequeue();
        });

        var t5 = Task.Run(()=> 
        {
            Dequeue();
        });

        var t6 = Task.Run(()=> 
        {
            Enqueue(6);
        });

        var t7 = Task.Run(()=> 
        {
            Dequeue();
        });

        var t8 = Task.Run(()=> 
        {
            Dequeue();
        });

        var t9 = Task.Run(()=> 
        {
            Enqueue(9);
        });

        var t10 = Task.Run(()=> 
        {
            Enqueue(10);
        });

        var t11 = Task.Run(()=> 
        {
            Enqueue(11);
        });

        var t12 = Task.Run(()=> 
        {
            Enqueue(12);
        });

        var t13 = Task.Run(()=> 
        {
            Enqueue(13);
        });
        
        Task.WaitAll(new Task[] {t1, t2, t5, t4, t3, t7, t6, t9, t10, t8, t11, t12, t13});
    }
}