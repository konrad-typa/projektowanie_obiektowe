using Erpeg.Data.Models.Maps;

namespace Erpeg.Data.DTOs;

public record LocalGameStateDTO
{
    public MapDTO? Map { get; init; }
    public PlayerDTO? LocalPlayer { get; init; }
    public List<string>? AvailableActions { get; init; }
    public List<string>? Logs { get; init; }
    
    public UIContextDto? UIContext { get; init; } 
    public InventoryInfoDto? InventoryInfo { get; init; }
}

public record GameUpdateDTO
{
    public MapChangeDTO? Map { get; init; }
    public PlayerDTO? LocalPlayer { get; init; }
    public List<string>? AvailableActions { get; init; }
    public List<string>? Logs { get; init; }
    
    public UIContextDto? UIContext { get; init; } 
    public InventoryInfoDto? InventoryInfo { get; init; }
}

public record ItemDTO
{
    public string? Name { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
    public double Weight { get; init; }
    public char Symbol { get; init; }
    public string? Color { get; init; }
}

public record CharacterDTO
{
    public string? Name { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
    public int Hp { get; init; }
    public int MaxHp { get; init; }
    public char Symbol { get; init; }
    public string? Color { get; init; }
}

public record PlayerDTO : CharacterDTO
{
    public int Mana { get; init; }
    public int MaxMana { get; init; }
    public int Damage { get; init; }
    public int Defense { get; init; }
    public List<ItemDTO>? Inventory { get; init; }
    public Dictionary<string, ItemDTO>? Equipment { get; init; }
    public int Gold { get; init; }
    public int Coins { get; init; }
    public double CurrentWeight { get; init; }
    public double MaxWeight { get; init; }
    public int Strength { get; init; }
    public int Stamina { get; init; }
    public int Dexterity { get; init; }
    public int Intelligence { get; init; }
    public int Aggression { get; init; }
    public int Luck { get; init; }
}

public record MapDTO
{
    public string? Name { get; init; }
    public int SizeX { get; init; }
    public int SizeY { get; init; }
    public List<ItemDTO>? Items { get; init; }
    public List<CharacterDTO>? Characters { get; init; }
    public TileType[][]? Tiles { get; init; }
}

public record MapChangeDTO
{
    public List<ItemDTO>? Items { get; init; }
    public List<CharacterDTO>? Characters { get; init; }
}

public record UIContextDto
{
    public string? Message { get; init; }
    public bool ShowBar { get; init; }
    public int BarCurrent { get; init; }
    public int BarMax { get; init; }
}

public record InventoryInfoDto
{
    public bool IsOpen { get; init; }
    public int SelectedIdx { get; init; }
    public int Offset { get; init; }
    public int WindowSize { get; init; }
}

public record PlayerInputDto
{
    public int Id { get; init; }
    public ConsoleKey Key { get; init; }
}