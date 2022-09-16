void CallbackMethod(object state, bool timedOut)
{
    Console.WriteLine("Start Signal");
    Thread.Sleep(3000);
    Console.WriteLine("Signal");
}

AutoResetEvent auto = new AutoResetEvent(false);
WaitOrTimerCallback callback = new WaitOrTimerCallback(CallbackMethod);

// auto - от кого ждать сигнал
// callback - что выполнять
// null - 1-й аргумент Callback метода
// 2000 - интервал между вызовами Callback метода
// если true - вызвать Callback метод один раз. Если false - вызывать Callback метод с интервалом.
// ThreadPool.RegisterWaitForSingleObject(auto, callback, null, Timeout.Infinite, true);

var waitHandle = ThreadPool
    .RegisterWaitForSingleObject(auto, callback, null, 2000, false);
//var waitHandle = ThreadPool
//    .RegisterWaitForSingleObject(auto, callback, null, 2000, false);

Console.WriteLine("S - сигнал, Q - выход");

while (true)
{
    var operation = Console.ReadKey(true).KeyChar.ToString().ToUpper();

    if (operation == "S")
    {
        // будим сами раньше 2 секунд
        auto.Set();
    }
    if (operation == "Q")
    {
        waitHandle.Unregister(auto);
        break;
    }
}
Console.ReadKey();