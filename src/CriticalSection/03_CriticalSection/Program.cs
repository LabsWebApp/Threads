object locker = new object();
int num = 0;

void WriteSecond()
{
    for (int i = 0; i < 20; i++)
    {
        lock (locker)
        {
            num = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string(' ', 10) + "Secondary" +num);
            Console.ForegroundColor = ConsoleColor.Gray;
            Thread.Sleep(100);
            //Console.WriteLine(new string(' ', 10) + "Gray");
        }
    }
}

ThreadStart writeSecond = new ThreadStart(WriteSecond);
Thread thread = new Thread(writeSecond);
thread.Start();

for (int i = 0; i < 20; i++)
{
    lock (locker)
    {
        num = 0;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Primary"+num);
        Console.ForegroundColor = ConsoleColor.Gray;
        Thread.Sleep(100);
        //Console.WriteLine("Gray");
    }
}

// Delay.
Console.ReadKey();