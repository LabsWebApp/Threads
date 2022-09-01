// Потоки.

// Метод, который планируется выполнять в отдельном потоке.
static void WriteSecond()
{
    while (true)
    {
        Console.WriteLine(new string(' ', 10) + "Secondary");
    }
}

ThreadStart writeSecond = WriteSecond;
Thread thread = new Thread(writeSecond);
thread.Start();

while (true)
{
    Console.WriteLine("Primary");
}

// Delay.
Console.ReadKey();