namespace ManualLock;

class Program
{
    private static readonly Random Random = new();
    private static readonly SpinLock Block = new(10); // Интервал 10 млск.

    private static readonly FileStream Stream = File
        .Open("log.txt", FileMode.Append, FileAccess.Write, FileShare.None);
    private static readonly StreamWriter Writer = new(Stream);

    // Будет запускаться в отдельном потоке.
    static void Function()
    {
        using (new SpinLockManager(Block)) // Вызывается block.Enter();
        {
            Writer.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} запускается.");
            Writer.Flush(); // Очищает буфер writer и записывает данные в файл.
        }   // Вызывается public void Dispose() { block.Exit(); }

        Thread.Sleep(Random.Next(10, 200)); // Усыпляется поток на случайный период времени.

        //using var _ = new SpinLockManager(Block);
        Writer.WriteLine($"Поток [{Thread.CurrentThread.ManagedThreadId}] завершается.");
        Writer.Flush(); // Очищает буфер writer и записывает данные в файл.
    }

    static void Main()
    {
        for (var i = 0; i < 50; ++i) new Thread(Function).Start();

        // Задержка.
        Console.ReadKey();
        Writer.WriteLine("*****Сеанс завершён*****");
        Writer.Flush(); // Очищает буфер writer и записывает данные в файл.
    }
}