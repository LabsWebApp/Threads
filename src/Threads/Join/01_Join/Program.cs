// Метод, который планируется выполнять во Вторичном потоке.
void ThreadFunc()
{
    Console.WriteLine($"ID Вторичного потока: {Thread.CurrentThread.ManagedThreadId}");
    Console.ForegroundColor = ConsoleColor.Yellow;

    for (int i = 0; i < 160; i++)
    {
        Thread.Sleep(20);
        Console.Write(".");
    }

    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine("\nВторичный поток завершился.");
}

// Создание нового потока.
var thread = new Thread(new ThreadStart(ThreadFunc));

Console.WriteLine($"ID Первичного потока: {Thread.CurrentThread.ManagedThreadId} \n");
Console.WriteLine("Запуск нового потока...");

thread.Start();
var current = Thread.CurrentThread;
//Console.WriteLine(current.ThreadState);
// Ожидание первичным потоком, завершения работы вторичного потока.
thread.Join(500);      //TODO Снять комментарий.
//Console.WriteLine(current.ThreadState);

Console.ForegroundColor = ConsoleColor.DarkRed;

// Работа первичного потока.
for (int i = 0; i < 160; i++)
{
    Thread.Sleep(20);
    Console.Write("-");
}

Console.ForegroundColor = ConsoleColor.Gray;

Console.WriteLine("\nПервичный поток завершился.");

// Задержка.
Console.ReadKey();