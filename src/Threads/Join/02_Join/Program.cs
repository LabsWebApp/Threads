void WriteChar(char chr, int count, ConsoleColor color)
{
    Console.ForegroundColor = color;
    for (int i = 0; i < count; i++)
    {
        Thread.Sleep(20);
        Console.Write(chr);
    }

    Console.ForegroundColor = ConsoleColor.Gray;
}

// Метод выполняющийся в третичном потоке.(Запуск из вторичного потока.)
void Method3()
{
    Console.WriteLine($"Третичный поток # {Thread.CurrentThread.GetHashCode()}");
    WriteChar('3', 160, ConsoleColor.Yellow);
    Console.WriteLine("Третичный поток завершился.");
}

// Метод выполняющийся во вторичном потоке.(Запуск из первичного потока.)
void Method2()
{
    Console.WriteLine($"Вторичный поток # {Thread.CurrentThread.GetHashCode()}");
    WriteChar('2', 80, ConsoleColor.DarkBlue);

    // Создание третичного потока.
    var thread = new Thread(Method3);
    thread.Start();
    thread.Join();

    WriteChar('2', 80, ConsoleColor.DarkBlue);
    Console.WriteLine("Вторичный поток завершился.");
}

//Первичный поток
Console.WriteLine($"Первичный поток # {Thread.CurrentThread.GetHashCode()}");
WriteChar('1', 80, ConsoleColor.DarkRed);

// Создание вторичного потока.
var thread = new Thread(Method2);
thread.Start();
thread.Join();

WriteChar('1', 80, ConsoleColor.DarkRed);

Console.WriteLine("Первичный поток завершился.");

// Задержка.
Console.ReadKey();