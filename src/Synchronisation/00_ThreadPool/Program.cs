//https://en.wikipedia.org/wiki/Thread_pool#/media/File:Thread_pool.svg

static void Task1(Object state)
{
    Thread.CurrentThread.Name = "1ая нить";
    Console.WriteLine($"Поток: {Thread.CurrentThread.Name}, ID: {Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine();
    Thread.Sleep(500);
}

static void Task2(Object state)
{
    Thread.CurrentThread.Name = "2ая нить";
    Console.WriteLine($"Поток: {Thread.CurrentThread.Name}, ID: {Thread.CurrentThread.ManagedThreadId}");
    Thread.Sleep(500);
}

//Когда метод GetAvailableThreads возвращает значение, переменная, указанная параметром workerThreads, 
//содержит число дополнительных рабочих потоков, которые могут быть запущены, а переменная, указанная параметром 
//completionPortThreads, содержит число дополнительных потоков асинхронного ввода/вывода, которые могут быть запущены.

//Если доступные потоки отсутствуют, дополнительные запросы к пулу потоков будут оставаться в очереди до тех пор, 
//пока в пуле потоков не появятся доступные потоки.
static void ShowThreadInfo()
{
    int availableWorkThreads, availableIOThreads, maxWorkThreads, maxIOThreads;
    ThreadPool.GetAvailableThreads(out availableWorkThreads, out availableIOThreads);
    ThreadPool.GetMaxThreads(out maxWorkThreads, out maxIOThreads);
    Console.WriteLine($"-------------Доступно рабочих потоков в пуле: {availableWorkThreads} из {maxWorkThreads}");
    Console.WriteLine($"-------------Доступно потоков ввода-вывода в пуле: {availableIOThreads} из {maxIOThreads}\n");
}

Console.WriteLine("Начало работы программы");
ShowThreadInfo();
Console.WriteLine("Запускаем Task1 в потоке из пула потоков");
ThreadPool.QueueUserWorkItem(new WaitCallback(Task1!));
ShowThreadInfo();
Console.WriteLine("Запускаем Task2 в потоке из пула потоков");
Thread.Sleep(1500);
ThreadPool.QueueUserWorkItem(Task2!);
ShowThreadInfo();
Console.WriteLine("Главный поток.");

Thread.Sleep(1000);

Console.WriteLine("Главный поток завершен.\n");

// Delay.
Console.WriteLine("Task1 и Task2 закончили работу");
ShowThreadInfo();
Console.ReadKey();