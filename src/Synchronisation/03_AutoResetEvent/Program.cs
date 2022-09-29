// AutoResetEvent - Уведомляет ожидающий поток о том, что произошло событие. 
using AutoResetEvent auto = new AutoResetEvent(false);

void Function()
{
    auto.WaitOne(); // после завершения WaitOne() AutoResetEvent автоматически переходит в несигнальное состояние.
    Console.WriteLine("Красный свет");
    auto.WaitOne(); // после завершения WaitOne() AutoResetEvent автоматически переходит в несигнальное состояние.
    Console.WriteLine("Желтый");
    auto.WaitOne(); // после завершения WaitOne() AutoResetEvent автоматически переходит в несигнальное состояние.
    Console.WriteLine("Зеленый");
}

Console.WriteLine("Нажмите на любую клавишу для перевода AutoResetEvent в сигнальное состояние.\n");

var thread = new Thread(Function);

thread.Start();

Console.ReadKey();
auto.Set(); // Первый сигнал потоку

Console.ReadKey();
auto.Set(); // Послать второй сигнал 

Console.ReadKey();
auto.Set(); // Послать третий сигнал 

Console.WriteLine($"Поток жив? - {thread.ThreadState}");

// Delay.
Console.ReadKey();