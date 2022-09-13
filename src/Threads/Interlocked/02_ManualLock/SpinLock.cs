namespace ManualLock;

public class SpinLock
{
    // Указывает выполняется ли блок кода потоком. 0 - блок свободен. 1 - блок занят.
    private int _block;

    //  Интервал через который потоки проверяют переменную block.
    private readonly int _wait;

    public SpinLock(int wait) => _wait = wait < 0 ? 0 : wait;

    /// <summary>
    /// Установить блокировку.
    /// </summary>
    public void Enter()
    {
        // Метод CompareExchange() 
        // 1. Сравнивает начальное значение первого аргумента с третьим аргументом.
        // 2. Если первый аргумент равен третьему аргументу, то в первый аргумент записывается значение второго аргумента.
        // 3. Иначе, если первый аргумент не равен третьему аргументу, то первый аргумент остается без изменения.
        // 4. Возвращает начальное значение первого аргумента (в  случае).
        int result = Interlocked.CompareExchange(ref _block, 1, 0);

        while (result == 1)
        {
            // Блокировка занята, ожидать.
            Thread.Sleep(_wait);
            result = Interlocked.CompareExchange(ref _block, 1, 0);
        }
    }

    /// <summary>
    /// Сбросить блокировку.
    /// </summary>
    public void Exit() => Interlocked.Exchange(ref _block, 0);
}