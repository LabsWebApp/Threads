namespace WaitPulse;

internal class TickTocking
{
    public readonly Thread TickTockingThread;
    TickTock tt;

    // Новый поток
    public TickTocking(string name, TickTock tt)
    {
        TickTockingThread = new Thread(this.Run);
        this.tt = tt;
        TickTockingThread.Name = name;
        TickTockingThread.Start();
    }

    private void Run()
    {
        switch (TickTockingThread.Name!.ToLower())
        {
            case "tick":
                for (int i = 0; i < 5; i++) tt.Tick(true);
                tt.Tick(false);
                break;
            case "tock":
                for (int i = 0; i < 5; i++) tt.Tock(true);
                tt.Tock(false);
                break;
            default: throw new ArgumentException("\"Tick\" or \"Tock\" only");
        }
    }
}