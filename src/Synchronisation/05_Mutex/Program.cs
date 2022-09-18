// Рекурсивное запирание.

using SomeClass instance = new ();

var thread = new Thread(instance.Method1);
thread.Start();

var thread2 = new Thread(instance.Method1);
thread2.Start();

// Delay.
thread.Join();
thread2.Join();
Console.ReadKey();

class SomeClass : IDisposable
{
    private readonly Mutex _mutex = new Mutex();

    public void Method1()
    {
        _mutex.WaitOne();
        Console.WriteLine("Method1 Start " + Thread.CurrentThread.ManagedThreadId);
        Method2();
        _mutex.ReleaseMutex();
        Console.WriteLine("Method1 End " + Thread.CurrentThread.ManagedThreadId);
    }

    public void Method2()
    {
        _mutex.WaitOne();

        Console.WriteLine("Method2 Start " + Thread.CurrentThread.ManagedThreadId);
        Thread.Sleep(1000);
        _mutex.ReleaseMutex();
        Console.WriteLine("Method2 End " + Thread.CurrentThread.ManagedThreadId);
    }

    public void Dispose()
    {
        _mutex.Dispose();
    }
}
