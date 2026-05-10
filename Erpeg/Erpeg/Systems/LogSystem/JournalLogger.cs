using Erpeg.Core.Interfaces;

namespace Erpeg.Systems.LogSystem;

public class JournalLogger : ILogger
{
    private readonly List<(DateTime, string)> _actionLogs = new();
    private readonly List<(DateTime Time, string Message)> _fullHistory = new();
    private readonly int _maxLogs = 3;

    public void Log(string message)
    {
        _fullHistory.Add((DateTime.Now, message));
        _actionLogs.Add((DateTime.Now, message));
        
        if (_actionLogs.Count > _maxLogs)
        {
            _actionLogs.RemoveAt(0);
        }
    }

    public List<(DateTime, string)> GetRecentLogs() => _actionLogs;
    public List<(DateTime, string)> GetFullHistory() => _fullHistory.ToList();
}