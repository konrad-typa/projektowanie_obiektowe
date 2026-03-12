using System.Reflection.PortableExecutable;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;


namespace Erpeg.Systems.CharacterSystems;

public class InventorySystem
{
    public static void TryPickUp(MapData map, PlayerData player)
    {
        if (map.Items.TryGetValue(player.Position, out ItemData item))
        {
            if (item.Type == ItemType.Coin || item.Type == ItemType.Gold)
            {
                player.Money[item.Type] += item.Value;
                map.Items.Remove(player.Position);
            }
            else if (player.CurrentWeight + item.Weight <= player.MaxWeight)
            {
                player.Inventory.Add(item);
                player.CurrentWeight += item.Weight;
                map.Items.Remove(player.Position);
            }
            else
            {
                // za ciężkie 
            }
        }
    }

    public static void TryDrop(MapData map, PlayerData player, ItemData item)
    {
        if (AvailableTile(map, player.Position, out var dropTile)
            && player.Inventory.Contains(item))
        {
            player.Inventory.Remove(item);
            player.CurrentWeight -= item.Weight;
            map.Items[dropTile] = item;
        }
    }

    public static void TryEquip(PlayerData player, ItemData item)
    {
        if (!player.Inventory.Contains(item))
            return;
        
        if (item.Type == ItemType.Weapon)
            TryEquipWeapon(player, (WeaponData)item);
        
        else if (item.Type == ItemType.Eq)
            TryEquipEq(player, (EquipmentData)item);
    }
    
    private static void TryEquipWeapon(PlayerData player, WeaponData weapon)
    {
        Unequip(player, EquipmentSlotType.MainHand);

        if (weapon.Grip == WeaponGripType.TwoHanded)
            Unequip(player, EquipmentSlotType.OffHand);
        
        player.Inventory.Remove(weapon);
        player.CurrentWeight -= weapon.Weight;
        
        player.Equipment[EquipmentSlotType.MainHand] = weapon;
    }
    
    private static void TryEquipEq(PlayerData player, EquipmentData equipment)
    {
        var slot = equipment.SlotType;
        Unequip(player, slot);

        if (slot ==  EquipmentSlotType.OffHand && IsOffHandBlocked(player))
            Unequip(player, EquipmentSlotType.MainHand);
        
        player.Inventory.Remove(equipment);
        player.CurrentWeight -= equipment.Weight;
        
        player.Equipment[slot] = equipment;
    }
    
    // helpery
    private static void Unequip(PlayerData player, EquipmentSlotType slot)
    {
        if (player.Equipment.TryGetValue(slot, out var item) && item != null)
        {
            player.Equipment[slot] = null;
            player.Inventory.Add(item);
            player.CurrentWeight += item.Weight;
        }
    }
    
    private static bool IsOffHandBlocked(PlayerData player)
    {
        if (player.Equipment.TryGetValue(EquipmentSlotType.MainHand, out var item)
            && item.Type == ItemType.Weapon)
        {
            WeaponData weapon = (WeaponData)item;
            if (weapon.Grip == WeaponGripType.TwoHanded)
                return true;
        }

        return false;
    }

    private static bool AvailableTile(MapData map, (int x, int y) position, out (int x, int y) pos)
    {
        if (!map.Items.ContainsKey(position))
        {
            pos = position;
            return true;
        }
        
        for (int i = -1; i < 2; i ++)
        {
            for (int j = -1; j < 2; j++)
            {
                int dx = Math.Clamp(position.x + i, 0, map.SizeX - 1);
                int dy = Math.Clamp(position.y + j, 0, map.SizeY - 1);

                var check = (dx, dy);
                if (!map.Items.ContainsKey(check) && map.Layout[dx, dy] != TileType.Wall)
                {
                    pos = check;
                    return true;
                }
            }
        }

        pos = default;
        return false;
    }
}