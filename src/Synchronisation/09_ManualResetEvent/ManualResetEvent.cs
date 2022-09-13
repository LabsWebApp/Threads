namespace ManualResetEventSimple;

internal class ManualResetEventSample : IDisposable
{
    private readonly ManualResetEvent _manualReset = new ManualResetEvent(false);

    public void RunAll()
    {
        var workers = new Workers(_manualReset);
        new Thread(workers.Worker1).Start();
        new Thread(workers.Worker2).Start();
        new Thread(workers.Worker3).Start();

        Thread.Sleep(1000);
        _manualReset.Reset();
        Console.WriteLine("Нажмите на любую клавишу для перевода ManualResetEvent в сигнальное состояние.\n");
        Console.ReadKey();
        _manualReset.Set();
        Console.WriteLine("Основной поток дошел до конца.");
    }

    public void Dispose() => _manualReset.Dispose();
}