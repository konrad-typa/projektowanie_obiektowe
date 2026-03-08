namespace Erpeg.Data.World.Models;

public struct MapCell(char symbol, ConsoleColor color = ConsoleColor.White)
{
    public char Symbol { get; init; } = symbol;
    public ConsoleColor Color { get; init; } = color;
}