using System.Collections.Concurrent;

const int lenght = 5;
WaitHandle[] waitHandles = new WaitHandle[lenght];
Random random = new(DateTime.Now.Millisecond);

// ConcurrentBag потокобезопасное хранение данных по поведению, микс из хеш-таблички и очереди

Console.WriteLine("ConcurrentBag");
ConcurrentBag<int> bag = new ConcurrentBag<int>();

for (int i = 0; i < lenght; i++)
{
    var handle = new EventWaitHandle(false, EventResetMode.AutoReset);
    var j = i;

    var thread = new Thread(() =>
    {
        bag.Add(j);
        Console.WriteLine($"Add: {j}");
        Thread.Sleep(random.Next(100, 1000));
        handle.Set();
    });

    waitHandles[j] = handle;
    thread.Start();
}
WaitHandle.WaitAll(waitHandles);

Console.WriteLine(new string('_', 20));

while (bag.TryTake(out int x)) Console.WriteLine($"Take: {x}");

//Console.ReadKey();
Console.WriteLine();


// ConcurrentQueue потокобезопасная очередь
Console.WriteLine("ConcurrentQueue");
ConcurrentQueue<int> queue = new ConcurrentQueue<int>();

for (int i = 0; i < lenght; i++)
{
    var handle = new EventWaitHandle(false, EventResetMode.AutoReset);
    var j = i;

    var thread = new Thread(() =>
    {
        Thread.Sleep(random.Next(100, 1000));
        queue.Enqueue(j);
        Console.WriteLine($"Add: {j}");
        handle.Set();
    });

    waitHandles[j] = handle;
    thread.Start();
}
WaitHandle.WaitAll(waitHandles);

Console.WriteLine(new string('_', 20)); 

while (queue.TryDequeue(out int x)) Console.WriteLine($"Take: {x}");

Console.WriteLine(new string('_', 20));


// ConcurrentStack потокобезопасный стек
Console.WriteLine("ConcurrentStack");
ConcurrentStack<int> stack = new ConcurrentStack<int>();

for (int i = 0; i < lenght; i++)
{
    var handle = new EventWaitHandle(false, EventResetMode.AutoReset);
    var j = i;

    var thread = new Thread(() =>
    {
        Thread.Sleep(random.Next(100, 1000));
        stack.Push(j);
        Console.WriteLine($"Add: {j}");
        handle.Set();
    });

    waitHandles[j] = handle;
    thread.Start();
}
WaitHandle.WaitAll(waitHandles);

Console.WriteLine(new string('_', 20));

while (stack.TryPop(out int x)) Console.WriteLine($"Take: {x}");

Console.ReadKey();