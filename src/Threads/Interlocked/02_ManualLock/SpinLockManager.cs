namespace ManualLock;

/// <summary>
/// Логика работы lock
/// </summary>
public class SpinLockManager : IDisposable
{
    private readonly SpinLock _block;

    public SpinLockManager(SpinLock block)
    {
        _block = block;
        block.Enter();
    }

    public void Dispose() => _block.Exit();
}