using AutoResetEvent auto = new AutoResetEvent(false);
StatusChecker checker = new StatusChecker(15);

// Представляет метод, обрабатывающий вызовы от события Timer.
TimerCallback checkStatus = new TimerCallback(checker.CheckStatus!);

Console.WriteLine("Создание таймера.\n");

// Создать таймер, который сигнализирует делегат для вызова CheckStatus через одну секунду,
// и каждую 1/4 секунды после этого.        

// Параметры:
// checkStatus - делегат TimerCallback, представляющий выполняемый метод. 
// auto - объект, передаваемый в качестве аргумента методу обратного вызова или значение null. 
// dueTime: 1000 - количество времени до начала вызова checkStatus, в миллисекундах.
//          (Timeout.Infinite - не допустить запуск таймера. Значение (0) - немедленный запуск таймера.)
// period: 250 - интервал между вызовами параметра callback, в миллисекундах. 
//          (Timeout.Infinite - отключение периодической сигнализации.)
var timer = new Timer(checkStatus, auto, 1000, 250);

// WaitOne - блокирует текущий поток до получения текущим дескриптором WaitHandle сигнала, 
// используя целочисленное значение со знаком для указания интервала времени.
// 5000 - Время ожидания в миллисекундах или Timeout.Infinite (-1), если ожидание должно быть бесконечным.
auto.WaitOne(5000);

Console.WriteLine("\nИзменение периода на 1/2 секунды.\n");

// 0 - Количество времени, в миллисекундах,
// которое должно пройти до вызова метода обратного вызова при создании объекта Timer.
// 1000 - Временной интервал в миллисекундах между вызовами метода обратного вызова,
// определенный в конструкторе объекта Timer.
timer.Change(5000, 1000);

auto.WaitOne(15000);

Console.WriteLine("\nУничтожение таймера.");
timer.Dispose();

// Delay.
Console.ReadKey();

class StatusChecker
{
    private readonly int _maxCount;
    private int _invokeCount;

    public StatusChecker(int maxCount) => _maxCount = maxCount;

    public void CheckStatus(Object stateInfo)
    {
        Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} начал работу.");
        Thread.Sleep(10000);
        // Учет вызовов метода.
        Console.WriteLine($"Проверка статуса {++_invokeCount}.");

        if (_invokeCount == _maxCount)
        {
            _invokeCount = 0;                   // Обнуление счетчика.
            ((AutoResetEvent)stateInfo).Set(); // Посылка сигнала.
        }
    }
}