using Erpeg.Core.Interfaces;
using System;

namespace Erpeg.Services;

public class DisplayService
{
    private static int _lastWidth = 0;
    private static int _lastHeight = 0;

    private const int MinWidth = 115; 
    private const int MinHeight = 33;

    public static void Initialize()
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
    }
    
    public static void Write(string text)
    {
        if (Console.WindowWidth != _lastWidth || Console.WindowHeight != _lastHeight)
        {
            Console.Clear();
            _lastWidth = Console.WindowWidth;
            _lastHeight = Console.WindowHeight;
        }

        if (Console.WindowWidth < MinWidth || Console.WindowHeight < MinHeight)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write($"[!] Terminal size too small. Please resize to target {MinWidth}x{MinHeight}." +
                          $" (Currrent: {Console.WindowWidth}x{Console.WindowHeight})");
            return;
        }

        Console.SetCursorPosition(0, 0);
        Console.Write(text);
    }

    public static void Clear() => Console.Clear();
}