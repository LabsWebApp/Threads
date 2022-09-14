namespace Priority;

internal class ThreadWork
{
    private const long MaxCount = 20000000;

    public readonly Thread RunThread;
    private static bool _stop;

    public long Count { get; private set; }

    public ThreadWork(string name, ThreadPriority priority, ConsoleColor color)
    {
        RunThread = new Thread(Run) { Name = name, Priority = priority };
        Console.ForegroundColor = color;
        Console.WriteLine($"Поток {RunThread.Name} начат.");
        Console.ResetColor();
    }

    void Run()
    {
        do
        {
            Count++;
        }
        while (_stop == false && Count < MaxCount);

        _stop = true;
        Console.WriteLine($"\nПоток {RunThread.Name} завершен.");
    }

    public void BeginInvoke() => RunThread.Start();

    public void EndInvoke() => RunThread.Join();
}