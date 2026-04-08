using System;
using Erpeg.Core.Interfaces;

namespace Erpeg.Services;

public class InputService : IService
{
    public static Action<ConsoleKey> OnInput; 
    
    public void Initialize() { }

    public static void ReadInput()
    {
        if (!Console.KeyAvailable) return;
        var key = Console.ReadKey(true).Key;
        OnInput?.Invoke(key);
    }
}