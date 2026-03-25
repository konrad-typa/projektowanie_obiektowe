using Erpeg.Core.Interfaces;
using System.Runtime.InteropServices;

namespace Erpeg.Services;
public class DisplayService
{
    public static void Initialize()
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
    }
    
    public static void Write(string text)
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(text);
    }

    public static void Clear() => Console.Clear();
    
}