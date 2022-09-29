//Методы Wait(), Pulse() и PulseAll()
//Временное снятие блокировки внутри lock (Monitor.Enter - Monitor.Exit)

using WaitPulse;

while (true)
{
    Console.WriteLine("Часы пошли");

    var tt = new TickTock();
    var tick = new TickTocking("Tick", tt);
    var tock = new TickTocking("Tock", tt);

    tick.TickTockingThread.Join();
    tock.TickTockingThread.Join();

    Console.WriteLine("Часы остановлены");
    Thread.Sleep(1000);
}
