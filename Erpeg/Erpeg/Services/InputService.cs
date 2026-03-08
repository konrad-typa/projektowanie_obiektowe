using Erpeg.Core.Interfaces;

namespace Erpeg.Services;
public class InputService : IService
{
    public void Initialize() { }
    
    public static ConsoleKey? LastKey { get; private set; }
    
    public static void ReadInput()
    {
        if (Console.KeyAvailable)
        {
            LastKey = Console.ReadKey(true).Key;
        }
        else 
        {
            LastKey = null;
        }
    }
}