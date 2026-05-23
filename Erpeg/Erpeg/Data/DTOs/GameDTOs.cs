using Erpeg.Data.Models.Maps;

namespace Erpeg.Data.DTOs;

public class GameStateDto
{
    public MapDTO Map { get; set; }
    public PlayerDTO LocalPlayer { get; set; }
    public List<string> AvailableActions { get; set; }
    public List<string> RecentLogs { get; set; }
    
    public UIContextDto UIContext { get; set; } 
    public InventoryInfoDto InventoryInfo { get; set; }
}

public class ItemDTO
{
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public double Weight { get; set; }
    public char Symbol { get; set; }
    public string Color { get; set; }
}

public class CharacterDTO
{
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public char Symbol { get; set; }
    public string Color { get; set; }
}

public class PlayerDTO
{
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public char Symbol { get; set; }
    public string Color { get; set; }
    public int Mana { get; set; }
    public int MaxMana { get; set; }
    public int Damage { get; set; }
    public int Defense { get; set; }
    public List<ItemDTO> Inventory { get; set; }
    public Dictionary<string, ItemDTO> Equipment { get; set; }
    public int Gold { get; set; }
    public int Coins { get; set; }
    public double CurrentWeight { get; set; }
    public double MaxWeight { get; set; }
    public int Strength { get; set; }
    public int Stamina { get; set; }
    public int Dexterity { get; set; }
    public int Intelligence { get; set; }
    public int Aggression { get; set; }
    public int Luck { get; set; }
}

public class MapDTO
{
    public string Name { get; set; }
    public int SizeX { get; set; }
    public int SizeY { get; set; }
    public List<ItemDTO> Items { get; set; }
    public List<CharacterDTO> Characters { get; set; }
    public TileType[][] Tiles { get; set; }
}

public class UIContextDto
{
    public string Message { get; set; }
    public bool ShowBar { get; set; }
    public int BarCurrent { get; set; }
    public int BarMax { get; set; }
}

public class InventoryInfoDto
{
    public bool IsOpen { get; set; }
    public int SelectedIdx { get; set; }
    public int Offset { get; set; }
    public int WindowSize { get; set; }
}

public class PlayerInputDto
{
    public ConsoleKey Key { get; set; }
}