using System.Reflection.Metadata;
using Erpeg.Data.Content.Items;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Items.Weapons;

namespace Erpeg.Data.Models.Characters;

public class PlayerData(string name, (int x, int y) position, int maxhp = 200, int hp = 200, char symbol = '¶')
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
        { AttributesType.Intelligence , 20 },
        { AttributesType.Aggression, 20},
        { AttributesType.Luck, 20}
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
            RecalculateStats();
        }

        Inventory.Remove(weapon);
        Equipment[EquipmentSlotType.MainHand] = weapon;
        RecalculateStats();
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
                RecalculateStats();
            }
        }
        
        Inventory.Remove(item);
        Equipment[item.SlotType] = item;
        RecalculateStats();
    }

    public void UnequipSlot(EquipmentSlotType slot)
    {
        if (Equipment.TryGetValue(slot, out var currentItem) && currentItem != null)
        {
            Equipment[slot] = null;
            Inventory.Add(currentItem);
            RecalculateStats();
        }
    }
    
    public int GetTotalAttribute(AttributesType type)
    {
        int total = Attributes.GetValueOrDefault(type, 0);
        
        foreach (var item in Equipment.Values)
        {
            if (item != null && item.Attributes.TryGetValue(type, out var itemBonus))
            {
                total += itemBonus;
            }
        }

        return total;
    }
    
    public void RecalculateStats()
    {
        // bonus do hp ze staminy
        int totalStamina = GetTotalAttribute(AttributesType.Stamina);
        MaxHp = 200 + (totalStamina * 5); 
        
        // bonus do dmg, obrony z itemow
        int totalDamage = 0;
        int totalDefense = 0;
        foreach (var item in Equipment.Values)
        {
            if (item != null)
            {
                totalDamage += item.Damage;
                totalDefense += item.Defense;
            }
        }
        int totalStrength = GetTotalAttribute(AttributesType.Strength);
        int totalDexterity = GetTotalAttribute(AttributesType.Dexterity);
        
        Damage = totalDamage + totalStrength;
        Defense = totalDefense +  totalDexterity;
    }
}