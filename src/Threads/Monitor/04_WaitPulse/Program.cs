//Методы Wait(), Pulse() и PulseAll
//Временное снятие блокировки внутри lock (Monitor.Enter - Monitor.Exit)

using WaitPulse;

while (true)
{
    Console.WriteLine("Часы пошли");

    var tt = new TickTock();
    var tock = new TickTocking("Tock", tt);
    var tick = new TickTocking("Tick", tt);

    tick.TickTockingThread.Join();
    tock.TickTockingThread.Join();

    Console.WriteLine("Часы остановлены");
    Thread.Sleep(1000);
}
