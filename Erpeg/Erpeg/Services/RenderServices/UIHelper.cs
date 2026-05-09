using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Erpeg.Services.RenderServices;

public static class UIHelper
{
    public static readonly string PlaceHolder = "placeholder";
    public const string ColorReset = "\x1b[0m";
    public const string ColorRed = "\x1b[31m";
    public const string ColorGreen = "\x1b[32m";
    public const string ColorBlue = "\x1b[34m";
    public const string ColorYellow = "\x1b[33m";
    public const string ColorCyan = "\x1b[36m";
    public const string ColorMagenta = "\x1b[35m";
    public const string ColorDarkGray = "\x1b[90m";
    
    public static int VisibleLength(string text)
    {
        if (string.IsNullOrEmpty(text)) return 0;
        return Regex.Replace(text, @"\x1b\[[0-9;]*m", "").Length;
    }
    
    public static string PadOrCropAnsi(string text, int totalWidth)
    {
        text ??= "";
        int visibleLen = VisibleLength(text);
        
        if (visibleLen <= totalWidth) 
            return text + new string(' ', totalWidth - visibleLen);
        
        var result = new System.Text.StringBuilder();
        int currentVisible = 0;
        bool inAnsi = false;
    
        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            if (c == '\x1b') 
                inAnsi = true;
        
            result.Append(c);
        
            if (!inAnsi) 
                currentVisible++;
            else if (c == 'm') 
                inAnsi = false; 
            
            if (currentVisible == totalWidth) 
            {
                result.Append(ColorReset); 
                break;
            }
        }
    
        return result.ToString();
    }
    
    public static string CenterAnsi(string text, int width)
    {
        int visibleLen = VisibleLength(text);
        if (visibleLen >= width) 
            return text;

        int leftPadding = (width - visibleLen) / 2;
        int rightPadding = width - visibleLen - leftPadding;

        return new string(' ', leftPadding) + text + new string(' ', rightPadding);
    }
    
    public static List<string> DrawBox(string title, List<string> content, int width, int innerHeight)
    {
        var lines = new List<string>();
        int titleLen = VisibleLength(title);
        
        string topBorder = $"┌" + 
                           new  string('─', Math.Max(0, width/2 - titleLen / 2 - 2 - (titleLen%2)))
                           + $" {title} "
                           + new string('─', Math.Max(0, width/2 - titleLen / 2) - 2) + "┐";
        lines.Add(topBorder);
        
        for (int i = 0; i < innerHeight; i++)
        {
            if (i < content.Count)
            {
                lines.Add($"│{PadOrCropAnsi(content[i], width - 2)}│");
            }
            else
            {
                lines.Add($"│{new string(' ', width - 2)}│");
            }
        }
        
        lines.Add("└" + new string('─', width - 2) + "┘");
    
        return lines;
    }
    
    public static List<string> WrapText(string text, int maxWidth)
    {
        if (string.IsNullOrEmpty(text)) return new List<string>();

        var words = text.Split(' ');
        var lines = new List<string>();
        var currentLine = "";

        foreach (var word in words)
        {
            if (VisibleLength(currentLine) + VisibleLength(word) + 1 > maxWidth)
            {
                if (!string.IsNullOrEmpty(currentLine)) lines.Add(currentLine);
                currentLine = word;
            }
            else
            {
                currentLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
            }
        }
        
        if (!string.IsNullOrEmpty(currentLine)) lines.Add(currentLine);
        return lines;
    }

    public static string JustifyAnsi(string leftText, string rightText, int totalWidth, int rightPadding = 2)
    {
        int leftLen = VisibleLength(leftText);
        int rightLen = VisibleLength(rightText);
        
        int spacesCount = totalWidth - leftLen - rightLen - rightPadding;
    
        if (spacesCount >= 0)
        {
            return leftText + new string(' ', spacesCount) + rightText + new string(' ', rightPadding);
        }
        
        return leftText + " " + rightText; 
    }
    
    public static string GetProgressBar(int current, int max, int width, string filledColor, string emptyColor, char filledChar = '█', char emptyChar = '░')
    {
        if (max <= 0) max = 1; 
        current = Math.Max(0, Math.Min(current, max));
        
        double percentage = (double)current / max;
        int filledCount = (int)Math.Round(percentage * width);
        int emptyCount = width - filledCount;
        
        string filledBlocks = new string(filledChar, filledCount);
        string emptyBlocks = new string(emptyChar, emptyCount);
        
        return $"{filledColor}{filledBlocks}{emptyColor}{emptyBlocks}{ColorReset}";
    }
}