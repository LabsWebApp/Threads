// Использование Mutex для синхронизации доступа к защищенным ресурсам.

using Mutex mutex = new Mutex();

void UseResource()
{
    // Метод WaitOne используется для запроса на владение мьютексом.
    // Блокирует текущий поток.
    mutex.WaitOne();

    Console.WriteLine($"{Thread.CurrentThread.Name} вошел в защищенную область.");
    Thread.Sleep(1000); // Выполнение некоторой работы...
    Console.WriteLine($"{Thread.CurrentThread.Name} покидает защищенную область.\r\n");

    mutex.ReleaseMutex();  // Освобождение Mutex.

    Console.WriteLine($"------{Thread.CurrentThread.Name} начал  работу.\r\n");
    Thread.Sleep(1000); // Выполнение некоторой работы...
    Console.WriteLine($"------{Thread.CurrentThread.Name} закончил  работу.\r\n");
}

void Function()
{
    for (int i = 0; i < 2; i++)
    {
        UseResource();
    }
}

for (int i = 0; i < 5;)
{
    Thread thread = new Thread(new ThreadStart(Function));
    thread.Name = $"Поток {++i}";
    thread.Start();
}

// Задержка.
Console.ReadKey();