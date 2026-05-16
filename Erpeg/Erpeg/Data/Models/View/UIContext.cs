namespace Erpeg.Data.Models.View;

public class UIContext
{
    public string message { get; set; } = "";
    public bool showBar { get; set; } = false;
    public int barCurrent { get; set; } = 0;
    public int barMax { get; set; } = 0;
}