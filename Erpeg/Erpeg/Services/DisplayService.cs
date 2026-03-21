using Erpeg.Core.Interfaces;

namespace Erpeg.Services;
public class DisplayService
{
    private const int WindowWidth = 200;
    private const int WindowHeight = 40;
    
    public static void Initialize()
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Console.SetWindowSize(WindowWidth, WindowHeight);
        // Console.SetBufferSize(WindowWidth, WindowHeight);
    }
    
    public static void Write(string text)
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(text);
    }

    public static void Clear() => Console.Clear();
    
}