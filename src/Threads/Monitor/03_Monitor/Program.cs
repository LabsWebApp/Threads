namespace monitor;

class Program
{
    private static int _counter = 0;

    // Нельзя использовать объекты блокировки структурного типа.
    // block - не может быть структурным.
    private static int _block = 0;

    private static void Function()
    {
        for (int i = 0; i < 50; ++i)
        {
            // Устанавливается блокировка постоянно в новый object (boxing).
            Monitor.Enter((object)_block);
            try
            {
                Console.WriteLine(++_counter);
            }
            finally
            {
                // Попытка снять блокировку с не заблокированного объекта (boxing создает новый объект).
                Monitor.Exit((object)_block);
            }
        }
    }

    static void Main()
    {
        Thread[] threads = { new(Function), new(Function), new(Function), new(Function), new(Function) };

        foreach (Thread t in threads)
        {
            t.Start();
        }

        // Задержка.
        Console.ReadKey();
    }
}