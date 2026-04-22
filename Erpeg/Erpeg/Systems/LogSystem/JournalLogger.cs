using Erpeg.Core.Interfaces;

namespace Erpeg.Systems.LogSystem;

public class JournalLogger : ILogger
{
    private readonly List<string> _actionLogs = new();
    private readonly List<string> _fullHistory = new();
    private readonly int _maxLogs = 3;

    public void Log(string message)
    {
        _fullHistory.Add(message);
        _actionLogs.Add(message);
        
        if (_actionLogs.Count > _maxLogs)
        {
            _actionLogs.RemoveAt(0);
        }
    }

    public List<string> GetRecentLogs() => _actionLogs;
    public List<string> GetFullHistory() => _fullHistory;
}