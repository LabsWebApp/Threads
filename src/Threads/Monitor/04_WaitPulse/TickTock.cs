namespace WaitPulse;

internal class TickTock
{
    private readonly object _locker = new object();

    public void Tick(bool running)
    {
        lock (_locker)
        {
            if (!running)
            {
                // Остановить часы
                Monitor.Pulse(_locker);
                return;
            }

            //Thread.Sleep(500);
            Console.Write("Тик ");

            // Разрешить выполнение метода Tock()
            Monitor.Pulse(_locker);

            // Ожидать завершение Tock()
            Monitor.Wait(_locker);
        }
    }

    public void Tock(bool running)
    {
        lock (_locker)
        {
            if (!running)
            {
                Monitor.Pulse(_locker);
                return;
            }

            //Thread.Sleep(500);
            Console.WriteLine("так");

            Monitor.Pulse(_locker);
            Monitor.Wait(_locker);
        }
    }
}