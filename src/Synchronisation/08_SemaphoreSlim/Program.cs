// SemaphoreSlim  - легковесный класс-семафор, который не использует объекты синхронизации ядра.

using SemaphoreSlim slim = new SemaphoreSlim(1, 2);

void Function()
{
    slim.Wait();

    Console.WriteLine($"Поток {Thread.CurrentThread.Name} начал работу.");
    Thread.Sleep(1000);
    Console.WriteLine($"Поток {Thread.CurrentThread.Name} закончил работу.\n");

    slim.Release();
}

Thread[] threads = { new(Function), new(Function), new(Function), new(Function), new(Function) };

for (var i = 0; i < threads.Length; i++)
{
    threads[i].Name = (++i).ToString();
    threads[i].Start();
}

Thread.Sleep(2000);
slim.Release();  // Возможен принудительный сброс из потока владельца семафора.

// Delay.
Console.ReadKey();