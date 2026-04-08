using Erpeg.Data.Models.Items;

namespace Erpeg.Data.Content.Items;

public static class ItemLibrary
{
    // weapons
    public static WeaponItem GetOneHandSword() => 
        new WeaponItem("One Handed Sword", 
            100, WeaponGripType.OneHanded, 50, 1, 1, 10,'/');
    public static WeaponItem GetAxe() => 
        new WeaponItem("Axe", 
            100, WeaponGripType.OneHanded, 40, 1.3, 1, 10,'/');
    public static WeaponItem GetTwoHandSword() => 
        new WeaponItem("Two Handed Sword", 
            250, WeaponGripType.TwoHanded, 90, 0.6, 1, 20,'!');
    public static WeaponItem GetDaggers() => 
        new WeaponItem("Daggers", 
            75, WeaponGripType.TwoHanded, 30, 2, 1, 6,';');
    public static WeaponItem GetBow() =>
        new WeaponItem("Bow", 
            100, WeaponGripType.TwoHanded, 40, 0.7, 5, 10, '}');

    // eq
    public static EquipmentItem GetArmor() => 
        new EquipmentItem("Armor",
             80, EquipmentSlotType.Chest, 100, 10, '&');
    public static EquipmentItem GetShield() => 
        new EquipmentItem("Shield", 
             50, EquipmentSlotType.OffHand, 60, 10, 'O');
    public static EquipmentItem GetArtifact() => 
        new EquipmentItem("Artifact", 
             20, EquipmentSlotType.Artifact, 60, 10, '~');

    // potions
    public static HealthPotion GetHealthPotion() => 
        new HealthPotion( 50, 20);
    public static ConsumableItem GetManaPotion() => 
        new ManaPotion( 50, 20);

    // money
    public static GoldItem GetGold(int amount) => 
        new GoldItem(amount);
    public static CoinItem GetCoins(int amount) => 
        new CoinItem(amount);
        
    // ores
    public static MaterialItem GetWood() =>
        new MaterialItem("Wood", 5, 1,'=');
    public static MaterialItem GetIron() =>
        new MaterialItem("Iron", 5, 1.5,'*');
    
    // scrap
    public static MaterialItem GetDust() =>
        new MaterialItem("Dust", 2, 0,'.');
}