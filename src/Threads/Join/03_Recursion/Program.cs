namespace JoinRecursion;

class Program
{
    // Общая переменная счетчик.
    //[ThreadStatic] //TODO Снять комментарий
    public static int Counter;

    // Рекурсивный запуск потоков.
    public static void Method()
    {
        if (Counter < 10)
        {
            Counter++; // Увеличение счетчика вызванных методов.
            Console.WriteLine($"{Counter} - СТАРТ --- {Thread.CurrentThread.GetHashCode()}");
            Thread.Sleep(10);
            var thread = new Thread(Method);
            thread.Start();
            //thread.Join(); //TODO Комментарий        
        }

        Console.WriteLine($"Поток {Thread.CurrentThread.GetHashCode()} завершился.");
    }

    private static void Main()
    {
        // Запуск вторичного потока.
        var thread = new Thread(Method);
        thread.Start();
        thread.Join();

        Console.WriteLine("Основной поток завершил работу...");

        // Задержка.
        Console.ReadKey();
    }
}