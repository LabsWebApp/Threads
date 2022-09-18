namespace ManualResetEventSimple;

internal class AutoResetEventSample : IDisposable
{
    private readonly AutoResetEvent _autoReset = new AutoResetEvent(false);

    public void RunAll()
    {
        var workers = new Workers(_autoReset);
        new Thread(workers.Worker1).Start();
        new Thread(workers.Worker2).Start();
        new Thread(workers.Worker3).Start();

        _autoReset.Set();
        Thread.Sleep(1000);
        _autoReset.Set();
        Thread.Sleep(1000);
        _autoReset.Set();
        Thread.Sleep(1000);
        _autoReset.Set();
        Thread.Sleep(1000);
        _autoReset.Set();
        Console.WriteLine("Основной поток дошел до конца.");
    }

    public void Dispose() => _autoReset.Dispose();
}