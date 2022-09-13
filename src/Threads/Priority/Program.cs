// Приоритеты потоков. 

using Priority;

var work1 = new ThreadWork("с высоким приоритетом", ThreadPriority.Highest, ConsoleColor.Red);
var work2 = new ThreadWork("с низким приоритетом", ThreadPriority.Lowest, ConsoleColor.Yellow);

work2.BeginInvoke();
work1.BeginInvoke();


work1.EndInvoke();
work2.EndInvoke();

Console.WriteLine();
Console.WriteLine($"Поток {work1.RunThread.Name} досчитал до {work1.Count}");
Console.WriteLine($"Поток {work2.RunThread.Name} досчитал до {work2.Count}");
Console.WriteLine($"Разы: {Math.Round((double)work1.Count / work2.Count, 5)}");

// Delay.
Console.ReadKey();