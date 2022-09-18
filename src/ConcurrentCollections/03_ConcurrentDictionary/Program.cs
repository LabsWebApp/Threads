// ConcurrentDictionary — это потокобезопасный класс коллекции для хранения пар ключ/значение.
// Он внутренне использует блокировку, чтобы предоставить вам потокобезопасный класс.
// Он предоставляет другие методы по сравнению с классом Dictionary,
// используйте: TryAdd, TryUpdate, TryRemove и TryGetValue для выполнения CRUD операций с ConcurrentDictionary.

// Пример:
using System.Collections.Concurrent;

ConcurrentDictionary<string, string> dictionary = new ();

var thread1 = new Thread(() =>
{
    for (int i = 0; i < 100; ++i)
    {
        dictionary.TryAdd(i.ToString(), i.ToString());
        Thread.Sleep(100);
    }
});

var thread2 = new Thread(() =>
{
    Thread.Sleep(300);
    foreach (var item in dictionary)
    {
        Console.WriteLine(item.Key + "-" + item.Value);
        Thread.Sleep(150);
    }
});
thread2.Start();
thread1.Start();
thread1.Join();
thread2.Join();

Console.ReadKey();