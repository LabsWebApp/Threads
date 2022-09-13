// Потоки.

// Метод, который планируется выполнять в отдельном потоке.
static void WriteSecond()
{
    for (int i = 0; i<1000;i++) 
    {
        var s = $"{new string(' ', 10)}Secondary";
        Console.WriteLine(s);
    }
}


ThreadStart writeSecond = WriteSecond;
//writeSecond += WriteSecondPlus;
Thread thread = new Thread(writeSecond);
thread.Start();

while (true) Console.WriteLine("Primary");

// Delay.
//Console.ReadKey();