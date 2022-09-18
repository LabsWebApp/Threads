// BlockingCollection — это класс коллекции, обеспечивающий потокобезопасность. 
// Особенности:
//  1. Bounding
//  2. Blocking
// Обе функции помогают нам реализовать модель производитель-потребитель.

using System.Collections.Concurrent;
using static System.Console;

//по умолчанию Очередь и без ограничений
var bCollection = new BlockingCollection<int>(new ConcurrentBag<int>(), 2);

bCollection.Add(1);
bCollection.Add(2);

WriteLine(bCollection.TryAdd(3, TimeSpan.FromSeconds(1)) ? "Добавить удалось" : "Добавить не удалось");

foreach (var item in bCollection) WriteLine(item);

var element = bCollection.Take();
WriteLine(element);
element = bCollection.Take();
WriteLine(element);

WriteLine(bCollection.TryTake(out element, TimeSpan.FromSeconds(1)) ? element : "Получить не удалось");

ReadKey();

WriteLine("\nCompleteAdding method and IsCompleted Property");
bCollection = new ();
var thread1 = new Thread(() =>
{
    for (var i = 0; i < 10; ++i)
    {
        bCollection.Add(i);
        WriteLine("add " + i);
    }
    bCollection.CompleteAdding();
});
var thread2 = new Thread(() =>
{
    WriteLine("start");
    while (!bCollection.IsCompleted)
    {
        var item = bCollection.Take();
        WriteLine(item);
    }
    WriteLine("finish");
});
thread2.Start();
thread1.Start();

thread1.Join();
thread2.Join();

//bCollection.Add(99);

ReadKey();

WriteLine("\nAddToAny, TryAddToAny, TakeFromAny, TryTakeFromAny");
BlockingCollection<int>[] producers =
{
    new(boundedCapacity: 10),
    new(boundedCapacity: 10),
    new(boundedCapacity: 10)
};

new Thread(() =>
{
    for (int i = 1; i <= 10; ++i)
    {
        producers[0].Add(i);
        Thread.Sleep(100);
    }
    producers[0].CompleteAdding();
}).Start();

new Thread(() =>
{
    for (int i = 11; i <= 20; ++i)
    {
        producers[1].Add(i);
        Thread.Sleep(150);
    }
    producers[1].CompleteAdding();
}).Start();

new Thread(() =>
{
    for (int i = 21; i <= 30; ++i)
    {
        producers[2].Add(i);
        Thread.Sleep(250);
    }
    producers[2].CompleteAdding();
}).Start();

while (!producers[0].IsCompleted ||
       !producers[1].IsCompleted ||
       !producers[2].IsCompleted)
{
    BlockingCollection<int>.TryTakeFromAny(producers, out element, TimeSpan.FromSeconds(1));
    if (element != default) WriteLine(element);
}

foreach (var item in producers) WriteLine(item.Count);

ReadKey();