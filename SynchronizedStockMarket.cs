using System;
using System.Threading;
using System.Threading.Tasks;

public class SynchronizedStockMarket
{
    private Mutex mutex = new Mutex();
    private int stocks = 500;
 
    public void Buy(int qty)
    {
        while (stocks <= qty)
        {
            Console.WriteLine($"Waiting to buy {qty} shares as it is more than available stock count: {stocks}");
        }

        mutex.WaitOne();

        while (stocks <= qty)
        {
            Console.WriteLine($"Again waiting to buy {qty} shares as it is more than available stock count: {stocks}");
        }

        stocks -= qty;
        Console.WriteLine($"Stock after buying {qty} is {stocks}");
        mutex.ReleaseMutex();
    }

    public void Sell(int qty)
    {
        if (qty + stocks > 500)
        {
            Console.WriteLine($"Unable to sell as {qty} + {stocks} > 500");
        }

        mutex.WaitOne();
        if (qty + stocks > 500)
        {
            Console.WriteLine($"Returning as unable to sell as {qty} + {stocks} > 500");
            mutex.ReleaseMutex();
            return;
        }

        stocks += qty;
        Console.WriteLine($"Stock after selling {qty} is {stocks}");
        mutex.ReleaseMutex();
    }

    public void SimulateThreads()
    {
        Task t1 = Task.Run(()=> 
        {
            Buy(100);
        });
        
        Task t2 = Task.Run(()=> 
        {
            Sell(50);
        });

        Task t3 = Task.Run(()=> 
        {
            Sell(300);
        });

        Task t4 = Task.Run(()=> 
        {
            Sell(10);
        });

        Task t5 = Task.Run(()=> 
        {
            Sell(80);
        });

        Task t6 = Task.Run(()=> 
        {
            Buy(50);
        });

        Task.WaitAll(new Task[] {t1, t2, t3, t4, t5, t6});
    }
}