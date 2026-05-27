using Erpeg.Data.Models;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;
using Erpeg.Data.Models.View;

namespace Erpeg.Data.DTOs;

public static class MapperDTO
{
    public static ItemDTO ToDto(this Item item, int x, int y)
    {
        return new ItemDTO
        {
            Name = item.Name,
            X = x,
            Y = y,
            Weight = item.Weight,
            Symbol = item.MapSymbol,
            Color = item.Color
        };
    }

    public static CharacterDTO ToDto(this CharacterData character, int x, int y)
    {
        return new CharacterDTO
        {
            Name = character.Name,
            X = x,
            Y = y,
            Hp = character.Hp,
            MaxHp = character.MaxHp,
            Symbol = character.MapSymbol,
            Color = character.Color
        };
    }
    
    public static MapDTO ToStaticDto(this MapData map)
    {
        var layout = new TileType[map.SizeX][];
        for (int i = 0; i < map.SizeX; i++)
        {
            layout[i] = new TileType[map.SizeY];
            for (int j = 0; j < map.SizeY; j++)
            {
                layout[i][j] = map.Layout[i, j];
            }
        }

        return new MapDTO
        {
            Name = map.Name,
            SizeX = map.SizeX,
            SizeY = map.SizeY,
            Tiles = layout
        };
    }

    public static MapChangeDTO ToDto(this MapData map)
    {
        return new MapChangeDTO
        {
            Items = map.Items.Select(x => x.Value.ToDto(x.Key.x, x.Key.y)).ToList(),
            Characters = map.Characters.Select(x => x.Value.ToDto(x.Key.x, x.Key.y)).ToList()
        };
    }

    public static PlayerDTO ToDto(this PlayerData player)
    {
        return new PlayerDTO
        {
            Name = player.Name,
            X = player.Position.x,
            Y = player.Position.y,
            Hp = player.Hp,
            MaxHp = player.MaxHp,
            Symbol = player.MapSymbol,
            Color = player.Color,
            Mana = player.Mana,
            MaxMana = player.MaxMana,
            Damage = player.Damage,
            Defense = player.Defense,
            Inventory = player.Inventory.Select(x => x.ToDto(0, 0)).ToList(),
            Equipment = player.Equipment
                .ToDictionary(
                    x => x.Key.ToString(),
                    x => x.Value == null ? null : x.Value.ToDto(0, 0)
                    ),
            Gold = player.Gold,
            Coins = player.Coins,
            CurrentWeight = player.CurrentWeight,
            MaxWeight = player.MaxWeight,
            Strength = player.GetTotalAttribute(AttributesType.Strength),
            Stamina = player.GetTotalAttribute(AttributesType.Stamina),
            Intelligence = player.GetTotalAttribute(AttributesType.Intelligence),
            Dexterity = player.GetTotalAttribute(AttributesType.Dexterity),
            Luck = player.GetTotalAttribute(AttributesType.Luck),
            Aggression = player.GetTotalAttribute(AttributesType.Aggression)
        };
    }

    public static UIContextDto ToDto(this UIContext uiContext)
    {
        return new UIContextDto
        {
            Message = uiContext.message,
            ShowBar = uiContext.showBar,
            BarCurrent = uiContext.barCurrent,
            BarMax = uiContext.barMax
        };
    }

    public static InventoryInfoDto ToDto(this InventoryInfo invInfo)
    {
        return new InventoryInfoDto
        {
            IsOpen = invInfo.isOpen,
            SelectedIdx = invInfo.selectedIdx,
            Offset = invInfo.Offset,
            WindowSize = invInfo.WindowSize
        };
    }
    
    public static GameUpdateDTO ToDto(this PlayerSession session, MapData map)
    {
        var uiContext = session.CurrentState.GetUIContext();
        var invInfo = session.CurrentState.GetInventoryInfo();

        return new GameUpdateDTO
        {
            Map = map.ToDto(),
            LocalPlayer = session.Player.ToDto(),
            AvailableActions = session.CurrentState.GetAvailableActions(),
            Logs = session.CurrentState.GetLogHistory()
                .Select(l => $"[{l.Time:mm:ss}] {l.Message}").ToList(),
            
            UIContext = new UIContextDto
            {
                Message = uiContext.message,
                ShowBar = uiContext.showBar,
                BarCurrent = uiContext.barCurrent,
                BarMax = uiContext.barMax
            },
            
            InventoryInfo = new InventoryInfoDto
            {
                IsOpen = invInfo.isOpen,
                SelectedIdx = invInfo.selectedIdx,
                Offset = invInfo.Offset,
                WindowSize = invInfo.WindowSize
            }
        };
    }
}