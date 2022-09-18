namespace WaitPulse;

internal class TickTock
{
    private readonly object _locker = new object();

    private long _first = 1;
    private bool _flag;

    public void Tick(bool running)
    {
        lock (_locker)
        {
            if (!running)
            {
                if (_flag)
                {
                    Console.WriteLine("...\n");
                    _flag = false;
                }
                // Остановить часы
                Monitor.Pulse(_locker);
                return;
            }

            Thread.Sleep(500);
            Console.Write("Тик ");
            Interlocked.CompareExchange(ref _first, 0, 1);
            
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
                if (_flag)
                {
                    Console.Write("...\n");
                    _flag = false;
                }
                Monitor.Pulse(_locker);
                return;
            }
            if (Interlocked.Read(ref _first) == 1)
            {
                Console.WriteLine("... так");
                Interlocked.CompareExchange(ref _first, 0, 1);
                _flag = true;
            }
            else
            {
                Thread.Sleep(500);
                Console.WriteLine("так");
            }

            Monitor.Pulse(_locker);
            Monitor.Wait(_locker);
        }
    }
}