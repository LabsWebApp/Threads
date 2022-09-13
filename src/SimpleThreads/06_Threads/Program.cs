int counter = 0;

// ThreadStart
Thread thread = new Thread(delegate () { Console.WriteLine($"1. counter = {++counter}"); });
thread.Start();

Thread.Sleep(100);
Console.WriteLine($"2. counter = {counter}");

// ParameterizedThreadStart
thread = new Thread(argument =>
{
    if (argument == null) throw new ArgumentNullException(nameof(argument));
    Console.WriteLine($"3. counter = {(int)argument}");
});
thread.Start(counter);

// Delay.
Console.ReadKey();