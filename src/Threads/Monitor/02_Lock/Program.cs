namespace monitor;

class Program
{
    private static readonly object Block = new();
    private static int _counter;
    private static readonly Random Random = new();

    private static void Function()
    {
        // Управляющий поток увеличивает счетчик и ожидает
        // произвольный период времени от 1 до 12 секунд. 

        lock (Block)
        {
            ++_counter;
        }

        int time = Random.Next(1000, 12000);
        Thread.Sleep(time);

        lock (Block)
        {
            --_counter;
        }
    }

    private static void Report()
    {
        while (true)
        {
            int count;

            lock (Block)
            {
                count = _counter;
            }

            Console.WriteLine($"{count} поток(ов) активно");
            Thread.Sleep(100);
        }
    }

    static void Main()
    {
        var reporter = new Thread(Report) { IsBackground = true };
        reporter.Start();

        var threads = new Thread[150];

        for (uint i = 0; i < 150; ++i)
        {
            threads[i] = new Thread(Function);
            threads[i].Start();
        }
    }
}