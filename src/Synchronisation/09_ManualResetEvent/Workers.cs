namespace ManualResetEventSimple;

internal class Workers
{
    private readonly EventWaitHandle _eventWaitHandle;

    public Workers(EventWaitHandle eventWaitHandle) => _eventWaitHandle = eventWaitHandle;

    public void Worker1()
    {
        Console.WriteLine("Worker1 запускается...");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Worker1 работает, шаг: {i}");
            Thread.Sleep(2000);
            _eventWaitHandle.WaitOne();
        }
    }
    public void Worker2()
    {
        Console.WriteLine("Worker2 запускается...");

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Worker2 работает, шаг: {i}");
            Thread.Sleep(2000);
            _eventWaitHandle.WaitOne();
        }
    }
    public void Worker3()
    {
        Console.WriteLine("Worker3 запускается...");

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Worker3 работает, шаг: {i}");
            Thread.Sleep(2000);
            _eventWaitHandle.WaitOne();
        }
    }
}
