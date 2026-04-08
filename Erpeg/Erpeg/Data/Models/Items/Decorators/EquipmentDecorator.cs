namespace Erpeg.Data.Models.Items.Decorators;

public abstract class EquipmentDecorator : EquipmentItem
{
    protected readonly EquipmentItem _innerEquipment;

    protected EquipmentDecorator(EquipmentItem innerEquipment) 
        : base(innerEquipment.Name, innerEquipment.Value, innerEquipment.SlotType, innerEquipment.Defense, innerEquipment.Weight, innerEquipment.MapSymbol)
    {
        _innerEquipment = innerEquipment;
    }
    
    public override string Name => _innerEquipment.Name;
    public override int Defense => _innerEquipment.Defense;
    public override Dictionary<AttributesType, int> Attributes => _innerEquipment.Attributes;
}

public class IntelligentEquipmentDecorator(EquipmentItem innerEquipment) : EquipmentDecorator(innerEquipment)
{
    public override string Name => $"{_innerEquipment.Name} (+int)";

    public override Dictionary<AttributesType, int> Attributes
    {
        get
        {
            var modifiedAttributes = new Dictionary<AttributesType, int>(_innerEquipment.Attributes);
            modifiedAttributes[AttributesType.Intelligence] += 5;
            return modifiedAttributes;
        }
    }
}

public class StaminaEquipmentDecorator(EquipmentItem innerEquipment) : EquipmentDecorator(innerEquipment)
{
    public override string Name => $"{_innerEquipment.Name} (+stm)";

    public override Dictionary<AttributesType, int> Attributes
    {
        get
        {
            var modifiedAttributes = new Dictionary<AttributesType, int>(_innerEquipment.Attributes);
            modifiedAttributes[AttributesType.Stamina] += 5;
            return modifiedAttributes;
        }
    }
}