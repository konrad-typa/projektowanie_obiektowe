namespace Erpeg.Data.Models.Maps;

public struct MapCell(char symbol, ConsoleColor color = ConsoleColor.White)
{
    public char Symbol { get; init; } = symbol;
    public ConsoleColor Color { get; init; } = color;
}