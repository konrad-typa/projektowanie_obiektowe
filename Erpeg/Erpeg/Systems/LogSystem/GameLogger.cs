using Erpeg.Core.Interfaces;

namespace Erpeg.Systems.LogSystem;

public class GameLogger : ILogger
{
    private ILogger? _logStrategy;

    public void Initialize(ILogger logStrategy)
    {
        _logStrategy = logStrategy;
    }

    public void Log(string message)
    {
        _logStrategy?.Log(message);
    }

    public List<(DateTime, string)> GetRecentLogs() => _logStrategy?.GetRecentLogs() ?? new List<(DateTime, string)>();
    public List<(DateTime, string)> GetFullHistory() => _logStrategy?.GetFullHistory() ?? new List<(DateTime, string)>();
}