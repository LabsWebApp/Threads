// Методы, который планируется выполнять в отдельном потоке.
static void WriteSecond(object argument)
{
    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine(argument);
        Thread.Sleep(1000);
    }
}

static void WriteSecondPlus(object argument)
{
    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine("Plus " + argument);
        Thread.Sleep(1000);
    }
}

ParameterizedThreadStart writeSecond = new ParameterizedThreadStart(WriteSecond!);
writeSecond += WriteSecondPlus!;
Thread thread = new Thread(writeSecond);
var str = 1;
thread.Start(str);

Thread.Sleep(500);

// Delay.
Console.ReadKey();