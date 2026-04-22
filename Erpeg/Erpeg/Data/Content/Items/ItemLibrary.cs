using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Items.Decorators;
using Erpeg.Data.Models.Items.Weapons;

namespace Erpeg.Data.Content.Items;

public static class ItemLibrary
{
    // eq
    public static EquipmentItem GetArmor() => 
        new EquipmentItem("Armor",
             80, EquipmentSlotType.Chest, 100, 10, '&');
    public static EquipmentItem GetShield() => 
        new EquipmentItem("Shield", 
             50, EquipmentSlotType.OffHand, 60, 10, 'O');
    // decorated eq
    public static EquipmentItem GetStaminaArmor() =>
        new IntelligentEquipmentDecorator(GetArmor());
    public static EquipmentItem GetIntelligentStaminaShield() =>
        new IntelligentEquipmentDecorator(new StaminaEquipmentDecorator(GetShield()));

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
    
    
    /* ----------- THEME ITEMS ---------- */
    
    // CLASSIC THEME
    // weapons
    public static LightWeapon GetOneHandSword() => 
        new LightWeapon("Sword", 
            100, WeaponGripType.OneHanded, 50, 1, 1, 10,'/');
    public static HeavyWeapon GetTwoHandSword() => 
        new HeavyWeapon("2H Sword", 
            250, WeaponGripType.TwoHanded, 90, 0.6, 1, 20,'!');
    public static LightWeapon GetDaggers() => 
        new LightWeapon("Daggers", 
            75, WeaponGripType.TwoHanded, 30, 2, 1, 6,';');
    public static HeavyWeapon GetMagicStaff() =>
        new HeavyWeapon("Magic Staff", 
            100, WeaponGripType.TwoHanded, 40, 0.7, 1, 10, '}');
    // decorated weapons
    public static WeaponItem GetStrongOneHandSword() =>
        new StrongWeaponDecorator(GetOneHandSword());
    public static WeaponItem GetStrongTwoHandSword() =>
        new StrongWeaponDecorator(GetTwoHandSword());
    public static WeaponItem GetUnluckyTwoHandSword() =>
        new UnluckyWeaponDecorator(GetTwoHandSword());
    public static WeaponItem GetDexterityDaggers() =>
        new DexterityWeaponDecorator(GetDaggers());
    public static WeaponItem GetStrongDexterityOHSword() =>
        new StrongWeaponDecorator(new DexterityWeaponDecorator(GetOneHandSword()));
    // artifact
    public static EquipmentItem GetClassicArtifact() => 
        new EquipmentItem("Artifact", 
            20, EquipmentSlotType.Artifact, 60, 10, 'Ω');
    public static EquipmentItem GetDecoratedClassicArtifact() =>
        new ArtifactDecorator(GetClassicArtifact());
    
    // FUTURISTIC THEME
    public static LightWeapon GetBlaster() => 
        new LightWeapon("Blaster", 
            100, WeaponGripType.OneHanded, 50, 1, 1, 10,'/');
    public static HeavyWeapon GetLightsaber() => 
        new HeavyWeapon("Lightsaber", 
            250, WeaponGripType.TwoHanded, 90, 0.6, 1, 20,'!');
    public static HeavyWeapon GetMagicRing() =>
        new HeavyWeapon("Magic Ring", 
            100, WeaponGripType.TwoHanded, 40, 0.7, 1, 10, '}');
    // decorated weapons
    public static WeaponItem GetStrongBlaster() =>
        new StrongWeaponDecorator(GetBlaster());
    public static WeaponItem GetStrongLightsaber() =>
        new StrongWeaponDecorator(GetLightsaber());
    public static WeaponItem GetUnluckyLightsaber() =>
        new UnluckyWeaponDecorator(GetLightsaber());
    public static WeaponItem GetDexterityMagicRing() =>
        new DexterityWeaponDecorator(GetMagicRing());
    public static WeaponItem GetStrongDexterityBlaster() =>
        new StrongWeaponDecorator(new DexterityWeaponDecorator(GetBlaster()));
    // artifact
    public static EquipmentItem GetFuturisticArtifact() => 
        new EquipmentItem("Online Girlfriend", 
            20, EquipmentSlotType.Artifact, 60, 10, 'Ω');
    public static EquipmentItem GetDecoratedFuturisticArtifact() =>
        new ArtifactDecorator(GetFuturisticArtifact());
    
    // UNI THEME
    public static LightWeapon GetPencil() => 
        new LightWeapon("Pencil", 
            100, WeaponGripType.OneHanded, 50, 1, 1, 10,'/');
    public static HeavyWeapon GetPen() => 
        new HeavyWeapon("Pen", 
            250, WeaponGripType.OneHanded, 70, 0.8, 1, 20,'!');
    public static LightWeapon GetCrayons() => 
        new LightWeapon("Crayons", 
            75, WeaponGripType.TwoHanded, 30, 2, 1, 6,';');
    public static HeavyWeapon GetMagicIpad() =>
        new HeavyWeapon("Magic Ipad", 
            100, WeaponGripType.TwoHanded, 40, 0.7, 1, 10, '}');
    // decorated weapons
    public static WeaponItem GetStrongPencil() =>
        new StrongWeaponDecorator(GetPencil());
    public static WeaponItem GetStrongPen() =>
        new StrongWeaponDecorator(GetPen());
    public static WeaponItem GetUnluckyPen() =>
        new UnluckyWeaponDecorator(GetPen());
    public static WeaponItem GetDexterityCrayons() =>
        new DexterityWeaponDecorator(GetCrayons());
    public static WeaponItem GetStrongDexterityPencil() =>
        new StrongWeaponDecorator(new DexterityWeaponDecorator(GetPencil()));
    // artifact
    public static EquipmentItem GetUniArtifact() => 
        new EquipmentItem("Last year's exam", 
            20, EquipmentSlotType.Artifact, 60, 10, 'Ω');
    public static EquipmentItem GetDecoratedUniArtifact() =>
        new ArtifactDecorator(GetUniArtifact());
}