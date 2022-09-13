// Основные и фоновые потоки. По умолчанию свойство IsBackground равно false.

// Метод, который планируется выполнять в отдельном потоке.
static void WriteSecond()
{
    while (true)
    {
        Console.WriteLine(new string(' ', 15) + "Secondary");
        Thread.Sleep(500);
    }
}

// Работа вторичного потока.
ThreadStart writeSecond = new ThreadStart(WriteSecond);
Thread thread = new Thread(writeSecond);
// Завершить работу вторичного потока
thread.IsBackground = true;
//thread.Priority = ThreadPriority.Highest;
thread.Start();

// Работа первичного потока.
for (int i = 0; i < 10; i++)
{
    Console.WriteLine("Primary");
    Thread.Sleep(500);
}
