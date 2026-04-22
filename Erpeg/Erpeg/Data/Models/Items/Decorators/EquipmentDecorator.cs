namespace Erpeg.Data.Models.Items.Decorators;

public abstract class EquipmentDecorator(EquipmentItem innerEquipment) : EquipmentItem(innerEquipment.Name,
    innerEquipment.Value, innerEquipment.SlotType, innerEquipment.Defense, innerEquipment.Weight,
    innerEquipment.MapSymbol)
{
    protected readonly EquipmentItem InnerEquipment = innerEquipment;

    public override string Name => InnerEquipment.Name;
    public override int Defense => InnerEquipment.Defense;
    public override Dictionary<AttributesType, int> Attributes => new(InnerEquipment.Attributes);
}

public class IntelligentEquipmentDecorator(EquipmentItem innerEquipment) : EquipmentDecorator(innerEquipment)
{
    public override string Name => $"{InnerEquipment.Name} (+int)";

    public override Dictionary<AttributesType, int> Attributes
    {
        get
        {
            var modifiedAttributes = new Dictionary<AttributesType, int>(InnerEquipment.Attributes);
            modifiedAttributes[AttributesType.Intelligence] += 5;
            return modifiedAttributes;
        }
    }
}

public class StaminaEquipmentDecorator(EquipmentItem innerEquipment) : EquipmentDecorator(innerEquipment)
{
    public override string Name => $"{InnerEquipment.Name} (+stm)";

    public override Dictionary<AttributesType, int> Attributes
    {
        get
        {
            var modifiedAttributes = new Dictionary<AttributesType, int>(InnerEquipment.Attributes);
            modifiedAttributes[AttributesType.Stamina] += 5;
            return modifiedAttributes;
        }
    }
}

public class ArtifactDecorator(EquipmentItem innerEquipment) : EquipmentDecorator(innerEquipment)
{
    public override string Name => $"*{InnerEquipment.Name}*";

    public override Dictionary<AttributesType, int> Attributes
    {
        get
        {
            var modifiedAttributes = new Dictionary<AttributesType, int>(InnerEquipment.Attributes);
            
            foreach (var attribute in InnerEquipment.Attributes)
                modifiedAttributes[attribute.Key] += 5;
            
            return modifiedAttributes;
        }
    }
}