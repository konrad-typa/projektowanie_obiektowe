using System.Reflection.Metadata;
using Erpeg.Data.Content.Items;
using Erpeg.Data.Models.Items;

namespace Erpeg.Data.Models.Characters;

public class PlayerData(string name, (int x, int y) position, int maxhp = 500, int hp = 500, char symbol = '¶')
    : CharacterData(name, position, maxhp, hp, symbol)
{
    public List<ItemData> Inventory { get; set; } = new List<ItemData>();
    public Dictionary<EquipmentSlotType, ItemData?> Equipment { get; set; } = new Dictionary<EquipmentSlotType, ItemData?>()
    {
        { EquipmentSlotType.Head, null},
        { EquipmentSlotType.Chest, null},
        { EquipmentSlotType.Legs, null},
        { EquipmentSlotType.Feet, null},
        { EquipmentSlotType.MainHand, null},
        { EquipmentSlotType.OffHand, null},
        { EquipmentSlotType.Artifact, null},
    };
    public Dictionary<AttributesType, int> Attributes { get; set; } = new Dictionary<AttributesType, int>()
    {
        { AttributesType.Strength , 20 },
        { AttributesType.Stamina, 20 },
        { AttributesType.Dexterity , 20 },
        { AttributesType.Intelligence , 20 }
    };
    public double CurrentWeight { get; set; } = 0;
    public double MaxWeight { get; set; } = 100;

    private int _mana = 100;
    private int _maxMana = 100;
    public int MaxMana
    {
        get => _maxMana;
        set
        {
            _maxMana = value;
            _mana = Math.Clamp(_mana, 0, _maxMana);
        }
    }
    public int Mana
    {
        get => _mana;
        set => Math.Clamp(value, 0, _maxMana);
    }
    
    public int Damage { get; set; } = 0;
    public int Defense { get; set; } = 0;

    public Dictionary<ItemType, int> Money = new Dictionary<ItemType, int>()
    {
        { ItemType.Coin, 0 },
        { ItemType.Gold, 0 },
    };
}