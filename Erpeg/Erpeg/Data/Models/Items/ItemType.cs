namespace Erpeg.Data.Models.Items;

public enum ItemType
{
    // weapons - !
    Sword, Bow, Daggers,
    
    // eq - &
    Armor, Trousers, Boots, Gloves, Hat, Shield,
    
    // potions - +
    HealthPotion, ManaPotion,
    
    // ores - =
    Wood, Iron, Leather,
    
    // scrap - ? (default)
    Scrap,
    
    // money - $
    Coin, Gold
}