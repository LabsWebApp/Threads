// Критическая секция (critical section).

// lock - это сокращенное использование System.Threading.Monitor.
// Monitor.Enter(this) - блокирует блок кода так, что его может использовать только текущий поток. 
// Все остальные потоки ждут пока текущий поток, закончит работу и вызовет Monitor.Exit(this).

object block = new object();

MainClass instance = new(block);

for (int i = 0; i < 3; i++)
{
    new Thread(instance.Method).Start();
}

// Delay.
Console.ReadKey();

class MainClass
{
    object block;

    public MainClass(object block) => this.block = block;

    public void Method()
    {
        int hash = Thread.CurrentThread.GetHashCode();
        Console.WriteLine("Hi");

        //Monitor.Enter(block); // комментировать.

        for (int counter = 0; counter < 10; counter++)
        {
            Console.WriteLine($"Поток # {hash}: шаг {counter}");
            Thread.Sleep(100);
        }
        Console.WriteLine(new string('-', 20));

        Monitor.Exit(block);  // комментировать.
    }
}