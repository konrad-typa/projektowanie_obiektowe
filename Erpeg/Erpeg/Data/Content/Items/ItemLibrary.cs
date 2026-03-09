using Erpeg.Data.Models.Items;

namespace Erpeg.Data.Content.Items;

public static class ItemLibrary
{
    // weapons
    public static WeaponData GetOneHandSword() => 
        new WeaponData("One Handed Sword", 
            ItemType.Sword, 100, WeaponGripType.OneHanded, 50, 1, 1, 10,'/');
    public static WeaponData GetTwoHandSword() => 
        new WeaponData("Two Handed Sword", 
            ItemType.Sword, 250, WeaponGripType.TwoHanded, 90, 0.6, 1, 20,'!');
    public static WeaponData GetDaggers() => 
        new WeaponData("Daggers", 
            ItemType.Daggers, 75, WeaponGripType.TwoHanded, 30, 2, 1, 6,';');
    public static WeaponData GetBow() =>
        new WeaponData("Bow", 
            ItemType.Bow, 100, WeaponGripType.TwoHanded, 40, 0.7, 5, 10, '}');

    // eq
    public static EquipmentData GetArmor() => 
        new EquipmentData("Armor", 
            ItemType.Armor, 80, EquipmentSlotType.Chest, 100, 10, '&');
    public static EquipmentData GetShield() => 
        new EquipmentData("Shield", 
            ItemType.Shield, 50, EquipmentSlotType.OffHand, 60, 10, 'O');
    public static EquipmentData GetArtifact() => 
        new EquipmentData("Artifact", 
            ItemType.Shield, 20, EquipmentSlotType.Artifact, 60, 10, '~');

    // potions
    public static ConsumableData GetHealthPotion() => 
        new ConsumableData("Health Potion", 
            ItemType.HealthPotion, 20, 50, 2,'+');
    public static ConsumableData GetManaPotion() => 
        new ConsumableData("Mana Potion", 
            ItemType.HealthPotion, 20, 50, 2,'+');

    // money
    public static ItemData GetGold(int amount) => 
        new ItemData("Gold", 
            ItemType.Gold, amount, 0,'$');
    public static ItemData GetCoins(int amount) => 
        new ItemData("Coins", 
            ItemType.Coin, amount, 0,'◎');
    
    // ores
    public static ItemData GetWood() =>
        new ItemData("Wood", 
            ItemType.Wood, 5, 1,'=');
    
    // scrap
    public static ItemData GetDust() =>
        new ItemData("Dust", 
            ItemType.Scrap, 2, 1,'.');
}