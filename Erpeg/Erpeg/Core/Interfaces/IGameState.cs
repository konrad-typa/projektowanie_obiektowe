using Erpeg.Data.Models.View;

namespace Erpeg.Core.Interfaces;

public interface IGameState
{
    void HandleInput(ConsoleKey key);
    void Update();
    List<string> GetAvailableActions();
    List<(DateTime Time,  string Message)> GetLogHistory();
    public UIContext GetUIContext();
    InventoryInfo GetInventoryInfo() => new InventoryInfo { isOpen = false };
}