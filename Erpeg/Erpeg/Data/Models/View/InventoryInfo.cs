namespace Erpeg.Data.Models.View;

public class InventoryInfo
{
    public bool isOpen { get; set; } = false;
    public int selectedIdx { get; set; } = 0;
    public int Offset { get; set; } = 0;
    public int WindowSize { get; set; } = 0;
}