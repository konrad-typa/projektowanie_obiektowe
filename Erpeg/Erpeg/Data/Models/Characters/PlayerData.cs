using System.Reflection.Metadata;
using Erpeg.Data.Content.Items;
using Erpeg.Data.Models.Items;

namespace Erpeg.Data.Models.Characters;

public class PlayerData(string name, (int x, int y) position, int maxhp = 500, int hp = 500, char symbol = '¶')
    : CharacterData(name, position, maxhp, hp, symbol)
{
    public List<Item> Inventory { get; set; } = new();
    public Dictionary<EquipmentSlotType, Item?> Equipment { get; set; } = new()
    {
        { EquipmentSlotType.Head, null},
        { EquipmentSlotType.Chest, null},
        { EquipmentSlotType.Legs, null},
        { EquipmentSlotType.Feet, null},
        { EquipmentSlotType.MainHand, null},
        { EquipmentSlotType.OffHand, null},
        { EquipmentSlotType.Artifact, null},
    };
    public Dictionary<AttributesType, int> Attributes { get; set; } = new()
    {
        { AttributesType.Strength , 20 },
        { AttributesType.Stamina, 20 },
        { AttributesType.Dexterity , 20 },
        { AttributesType.Intelligence , 20 }
    };
    public double CurrentWeight { get; private set; } = 0;
    public double MaxWeight { get; private set; } = 100;

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

    public int Gold { get; private set; } = 0;
    public int Coins { get; private set; } = 0;
    
    // metody
    public void AddGold(int amount) => Gold += amount;
    public void AddCoins(int amount) => Coins += amount;
    
    public bool TryAddWeight(double extraWeight)
    {
        if (CurrentWeight + extraWeight > MaxWeight) return false;
        CurrentWeight += extraWeight;
        return true;
    }

    public void RemoveItemFromInventory(Item item)
    {
        if (Inventory.Remove(item))
        {
            CurrentWeight -= item.Weight;
        }
    }

    public void Heal(int amount)
    {
        Hp += amount;
    }
    
    public void RestoreMana(int amount)
    {
        Mana += amount;
    }
    
    public void EquipWeapon(WeaponItem weapon)
    {
        UnequipSlot(EquipmentSlotType.MainHand);

        if (weapon.Grip == WeaponGripType.TwoHanded)
        {
            UnequipSlot(EquipmentSlotType.OffHand);
        }

        Inventory.Remove(weapon);
        Equipment[EquipmentSlotType.MainHand] = weapon;
    }

    public void EquipEq(EquipmentItem item)
    {
        UnequipSlot(item.SlotType);
        
        if (item.SlotType == EquipmentSlotType.OffHand)
        {
            var mainHandItem = Equipment[EquipmentSlotType.MainHand];
            
            if (mainHandItem != null && mainHandItem.BlocksOffHand)
            {
                UnequipSlot(EquipmentSlotType.MainHand);
            }
        }
        
        Inventory.Remove(item);
        Equipment[item.SlotType] = item;
    }

    public void UnequipSlot(EquipmentSlotType slot)
    {
        if (Equipment.TryGetValue(slot, out var currentItem) && currentItem != null)
        {
            Equipment[slot] = null;
            Inventory.Add(currentItem);
        }
    }
}