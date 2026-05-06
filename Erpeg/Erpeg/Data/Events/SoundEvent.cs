namespace Erpeg.Data.Events;

public class SoundEvent
{
    public int SourceX { get; set; }
    public int SourceY { get; set; }
    public int Range { get; set; }
    public string? SourceName { get; set; }
}