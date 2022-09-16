// работаем с ManualResetEventSlim
using System.Diagnostics;

void MethodSlim(ManualResetEventSlim mre, bool value)
{
    //в цикле повторяем действие достаточно большое число раз
    for (int i = 0; i < 10000000; i++)
    {
        if (value) mre.Set();
        else mre.Reset();
    }
}

// работаем с классическим ManualResetEvent
void Method(ManualResetEvent mre, bool value)
{
    for (int i = 0; i < 10000000; i++)
    {
        if (value) mre.Set();
        else mre.Reset();
    }
}

var mres = new ManualResetEventSlim(false);
var mre = new ManualResetEvent(false);

long total = 0;
int COUNT = 5;

for (int i = 0; i < COUNT; i++)
{
    mres.Reset();
    //счётчик затраченного времени
    Stopwatch sw = Stopwatch.StartNew();

    //запускаем установку в потоке пула
    ThreadPool.QueueUserWorkItem((obj) =>
    {
        MethodSlim(mres, true);
        //Method(mre, true);
        mres.Set();
    });
    //запускаем сброс в основном потоке
    MethodSlim(mres, false);
    //Method(mre, false);

    //Ждём, пока выполнится поток пула
    mres.Wait();
    sw.Stop();

    Console.WriteLine($"Pass {i}: {sw.ElapsedMilliseconds} ms");
    total += sw.ElapsedMilliseconds;
}

Console.WriteLine();
Console.WriteLine("===============================");
Console.WriteLine("Done in average=" + total / (double)COUNT);
Console.ReadLine();