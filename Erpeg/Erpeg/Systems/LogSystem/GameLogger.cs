using Erpeg.Core.Interfaces;

namespace Erpeg.Systems.LogSystem;

public class GameLogger : ILogger
{
    private ILogger? _logStrategy;
    private string _contextMessage ="";

    private GameLogger() {
    }

    public static GameLogger Instance { get; } = new GameLogger();

    public void Initialize(ILogger logStrategy)
    {
        _logStrategy = logStrategy;
    }

    public void Log(string message)
    {
        _logStrategy?.Log(message);
    }

    public void SetContext(string message)
    {
        _contextMessage = message;
    }
    public string GetContext() => _contextMessage;

    public List<(DateTime, string)> GetRecentLogs() => _logStrategy?.GetRecentLogs() ?? new List<(DateTime, string)>();
    public List<(DateTime, string)> GetFullHistory() => _logStrategy?.GetFullHistory() ?? new List<(DateTime, string)>();
}