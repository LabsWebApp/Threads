// Альтернативные операции VolatileWrite() и VolatileRead() ключевому слову volatile.
namespace Volatile;

class Program
{
    // static volatile int stop = 0;
    static int stop;

    static void Main()
    {
        Console.WriteLine("Main: запускается поток на 2 секунды.");
        var t = new Thread(Worker!);
        t.Start();

        Thread.Sleep(2000);

        Thread.VolatileWrite(ref stop, 1);
        //stop = 1;

        Console.WriteLine("Main: ожидание завершения потока.");
        t.Join();
    }

    private static void Worker()
    {
        int x = 0;

        while (Thread.VolatileRead(ref stop) != 1)
        {
            x++;
        }

        Console.WriteLine($"Worker: остановлен при x = {x}.");
    }
}