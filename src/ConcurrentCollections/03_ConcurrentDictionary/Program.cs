// ConcurrentDictionary — это потокобезопасный класс коллекции для хранения пар ключ/значение.
// Он использует внутреннюю блокировку.
// Он предоставляет другие методы по сравнению с классом Dictionary,
//  используйте: AddOrUpdate, GetOrAdd, TryAdd, TryUpdate, TryRemove и TryGetValue
//  для выполнения CRUD операций с ConcurrentDictionary.

// Пример:
using System.Collections.Concurrent;
// ReSharper disable All

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
    for (var i = 0; i < 100; i++)
    {
        if (dictionary.TryGetValue(i.ToString(), out var s)) 
            Console.Write(s + " ");
        //Thread.Sleep(93);
    }
});
thread2.Start();
thread1.Start();
thread1.Join();
thread2.Join();

//Console.ReadKey();
Console.WriteLine();

dictionary.AddOrUpdate(
    100.ToString(),
    (key, arg) => $"{key} скоро {arg}",
    (key, old, arg) => $"{key} (уже было:\"{old}\"), так когда {arg}?",
    101);
if (dictionary.TryGetValue(100.ToString(), out var str))
    Console.WriteLine(str);

dictionary.AddOrUpdate(
    100.ToString(),
    (key, arg) => $"{key} скоро {arg}",
    (key, old, arg) => $"{key} (уже было:\"{old}\"), так когда {arg}?",
    101);
if (dictionary.TryGetValue(100.ToString(), out str))
    Console.WriteLine(str);
Console.ReadKey();