// Interlocked - Предоставляет атомарные операции для переменных, общедоступных нескольким потокам. 

namespace SimpleInterlocked;

class Program
{
    // Счетчик запущенных потоков.
    private static long _counter;
    private static readonly Random Random = new(DateTime.Now.Millisecond);

    /// <summary>
    /// Метод, который планируется выполнять во вторичных потоках.
    /// </summary>
    private static void Function()
    {
        // Поток увеличивает счетчик.
        Interlocked.Increment(ref _counter);
        //_counter++;

        try
        {
            // Поток ожидает произвольный период времени от 1 до 12 секунд.
            var time = Random.Next(1000, 12000);
            Thread.Sleep(time);
        }
        finally
        {
            // Поток уменьшает счетчик. 
            Interlocked.Decrement(ref _counter);
            //_counter--;
        }
    }

    /// <summary>
    /// Проверка количества запущенных потоков.
    /// </summary>
    private static void Reporter()
    {
        while (true)
        {
            long number = Interlocked.Read(ref _counter);

            Console.WriteLine($"{number} поток(ов) активно.");
            Thread.Sleep(100);
        }
    }

    static void Main()
    {
        var reporter = new Thread(Reporter) { IsBackground = true };
        reporter.Start();

        Thread.Sleep(10);

        var threads = new Thread[150];

        for (uint i = 0; i < 15; ++i)
        {
            threads[i] = new Thread(Function);
            threads[i].Start();
        }

        //Thread.Sleep(15000);
    }
}