// получаем текущий поток
Thread currentThread = Thread.CurrentThread;

//получаем имя потока
Console.WriteLine($"Имя потока: {currentThread.Name ?? "No Name"}");
currentThread.Name = "Метод Main";
Console.WriteLine($"Имя потока: {currentThread.Name}");

Console.WriteLine($"Запущен ли поток: {currentThread.IsAlive}");
Console.WriteLine($"Id потока: {currentThread.ManagedThreadId}");
Console.WriteLine($"Id потока: {currentThread.GetHashCode()}");
Console.WriteLine($"Приоритет потока: {currentThread.Priority}");
Console.WriteLine($"Статус потока: {currentThread.ThreadState}");

Console.ReadKey();