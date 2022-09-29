// Если объект ядра с именем GlobalEvent уже существует будет получена ссылка на него.
// false - несигнальное состояние.
// ManualReset - тип события.
// GlobalEvent - имя по которому все приложения будут слушать событие.
EventWaitHandle manual = new EventWaitHandle(
    false, 
    EventResetMode.ManualReset, 
    "GlobalEvent::E721D8D2-2BAC-4259-AB65-214D37408AA7");

void Function()
{
    manual.WaitOne();

    while (true)
    {
        Console.WriteLine("Hello world!");
        Thread.Sleep(300);
    }
}

Thread thread = new Thread(Function);
thread.IsBackground = true;
thread.Start();

Console.WriteLine("Нажмите любую клавишу для начала работы потока.");
Console.ReadKey();

// Перевод события в сигнальное состояние.
// Все приложения, которые используют событие с именем GlobalEvent,
// получат оповещение о переходе в сигнальное состояние.
manual.Set();

// Delay.
Console.ReadKey();