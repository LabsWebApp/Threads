// Критическая секция (critical section).

// lock - блокирует блок кода так, что в каждый отдельный момент времени, этот блок кода
// сможет использовать только один поток. Все остальные потоки ждут пока текущий поток, закончит работу.

object block = new object();

Console.SetWindowSize(80, 40);

//new Thread(() =>
//{
//    object o = new();
//    lock (o)
//    {
//        while (true)
//        {
//            Console.WriteLine("fhgfhgfhg");
//            Thread.Sleep(500);
//        }
//    }
//})
//{ IsBackground = true }.Start();

MainClass instance = new MainClass(block);

for (int i = 0; i < 3; i++)
{
    new Thread(instance.Method).Start();
}

Thread.Sleep(500);

// Delay.
//Console.ReadKey();

class MainClass
{
    private object block;

    public MainClass(object block) => this.block = block;

    public void Method()
    {
        int hash = Thread.CurrentThread.GetHashCode();
        Console.WriteLine('*');

        lock (block) // комментировать lock.
        {
            for (int counter = 0; counter < 10; counter++)
            {
                Console.WriteLine($"Поток # {hash}: шаг {counter}");
                Thread.Sleep(100);
            }
            Console.WriteLine(new string('-', 20));
        }
    }
}