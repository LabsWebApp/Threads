namespace monitor
{
    class Program
    {
        // Объект для блокировки.
        private static readonly object Block = new();

        // Счетчик потоков.
        private static int _counter;
        private static readonly Random Random = new();

        // Выполняется в отдельном потоке.
        private static void Function()
        {
            // Управляющий поток увеличивает счетчик и ожидает
            // произвольный период времени от 1 до 12 секунд.

            try
            {
                Monitor.Enter(Block); // Начало блокировки.
                ++_counter;
            }
            finally
            {
                Monitor.Exit(Block);  // Конец блокировки.
            }

            int time = Random.Next(1000, 12000);
            Thread.Sleep(time);

            try
            {
                Monitor.Enter(Block); // Начало блокировки.
                --_counter;
            }
            finally
            {
                Monitor.Exit(Block);  // Конец блокировки.
            }
        }

        // Мониторинг количества запущенных потоков.
        private static void Reporter()
        {
            while (true)
            {
                int count;

                try
                {
                    Monitor.Enter(Block);// Начало блокировки.
                    count = _counter;
                }
                finally
                {
                    Monitor.Exit(Block);
                }

                Console.WriteLine($"{count} поток(ов) активно");
                Thread.Sleep(100);
            }
        }

        static void Main()
        {
            var reporter = new Thread(Reporter) { IsBackground = true };
            reporter.Start();

            var threads = new Thread[150];

            for (uint i = 0; i < 150; ++i)
            {
                threads[i] = new Thread(Function);
                threads[i].Start();
            }

           Thread.Sleep(15000);
        }
    }
}