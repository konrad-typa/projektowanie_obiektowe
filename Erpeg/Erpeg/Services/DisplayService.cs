using Erpeg.Core.Interfaces;

namespace Erpeg.Services;
public class DisplayService : IService
{
    public void Initialize() { }
    
    public static void Write(string text)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 0);
        Console.Write(text);
    }

    public static void Clear() => Console.Clear();
    
}