// Класс Semaphore - используется для управления доступом к пулу ресурсов. 
// Потоки занимают слот семафора, вызывая метод WaitOne(), и освобождают занятый слот вызовом метода Release().

// Первый аргумент:
// Задаем количество слотов для использования в данный момент (не более максимального количества).
// Второй аргумент:
// Задаем максимальное количество слотов для данного семафора.
using Semaphore pool = new (3, 5, "SemaphorePool:AAED7056-380D-412E-9608-763495211EA8");

void Work(object number)
{
    pool.WaitOne();

    Console.WriteLine($"Поток {number} занял слот семафора.");
    Thread.Sleep(1000);
    Console.WriteLine($"Поток {number} -----> освобождает слот.");
    pool.Release();
    Thread.Sleep(1000);
}

for (int i = 1; i <= 15; i++)
{
    Thread thread = new Thread(new ParameterizedThreadStart(Work!));
    thread.Start(i);
}
Thread.Sleep(2000);
pool.Release(2);
// Задержка.
Console.ReadKey();