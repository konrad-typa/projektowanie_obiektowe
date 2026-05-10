using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Erpeg.Services.RenderServices;

public static class UIHelper
{
    public const string PlaceHolder = "placeholder";
    public const string ColorReset = "\x1b[0m";
    public const string ColorHpRed = "\x1b[31m";
    public const string ColorGreen = "\x1b[32m";
    public const string ColorManaBlue = "\x1b[34m";
    public const string ColorYellow = "\x1b[33m";
    public const string ColorCyan = "\x1b[36m";
    public const string ColorMagenta = "\x1b[35m";
    public const string ColorDarkGray = "\x1b[90m";
    public const string ColorGold = "\x1b[38;2;150;140;75m";
    public const string ColorTeal = "\x1b[38;2;0;128;128m";
    public const string ColorSilver = "\x1b[38;2;192;192;192m";
    public const string ColorBrown = "\x1b[38;5;94m";
    public const string ColorPurple = "\x1b[38;5;129m";
    public const string ColorLightRed = "\x1b[91m";
    public const string ColorCopper = "\x1b[38;2;184;115;51m";
    public const string MutedBlue = "\x1b[38;2;85;115;150m"; 
    public const string MutedGreen = "\x1b[38;2;90;130;90m"; 
    public const string MutedRed = "\x1b[38;2;150;75;75m";   
    public const string MutedPurple = "\x1b[38;2;120;90;140m";
    public const string MutedGold = "\x1b[38;2;95;85;55m";
    public const string MutedTeal = "\x1b[38;2;75;130;120m";
    public const string MutedWood = "\x1b[38;2;120;95;75m";
    public const string Slate = "\x1b[38;2;100;115;130m";
    public const string BlueSlate = "\x1b[38;2;80;110;150m"; 
    public const string MutedCyan = "\x1b[38;2;75;145;145m";
    public const string DarkCyan = "\x1b[38;2;45;115;115m";
    public const string DustyCyan = "\x1b[38;2;100;150;150m";
    public const string MutedSwamp = "\x1b[38;2;85;95;65m";
    public const string MutedRose = "\x1b[38;2;150;95;105m";
    public const string MutedCoral = "\x1b[38;2;160;100;90m";
    public const string MutedIndigo = "\x1b[38;2;80;85;130m";
    public const string MutedLavender = "\x1b[38;2;135;115;145m";
    public const string MutedAmethyst = "\x1b[38;2;120;90;130m";
    public const string MutedCobalt = "\x1b[38;2;60;75;100m";
    
    public const string TextBone = "\x1b[38;2;220;215;195m";
    
    public const string FrameLightSteel = "\x1b[38;2;130;140;146m";
    public const string FrameStone = "\x1b[38;2;160;165;162m";
    
    public const string ColorEnemyWeak = "\x1b[38;2;170;55;55m";
    public const string ColorEnemyStrong = "\x1b[38;2;255;80;80m";
    
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
    
    public static List<string> DrawBox(string title, List<string> content, int width, int innerHeight, 
        string? color = ColorDarkGray)
    {
        var lines = new List<string>();
        int titleLen = VisibleLength(title);
        
        string topBorder = $"{color}┌" + 
                           new  string('─', Math.Max(0, width/2 - titleLen / 2 - 2 - (titleLen%2)))
                           + $" {title} "
                           + new string('─', Math.Max(0, width/2 - titleLen / 2) - 2) + $"┐{ColorReset}";
        lines.Add(topBorder);
        
        for (int i = 0; i < innerHeight; i++)
        {
            if (i < content.Count)
            {
                lines.Add($"{color}│{ColorReset}{PadOrCropAnsi(content[i], width - 2)}{color}│{ColorReset}");
            }
            else
            {
                lines.Add($"{color}│{ColorReset}{new string(' ', width - 2)}{color}│{ColorReset}");
            }
        }
        
        lines.Add($"{color}└" + new string('─', width - 2) + $"┘{ColorReset}");
    
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