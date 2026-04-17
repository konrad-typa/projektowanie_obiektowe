namespace Erpeg.Systems;

public static class MessageLogSystem
{
    private static readonly List<string> _actionLogs = new();
    private static readonly List<string> _fullHistory = new();
    private static readonly int _maxLogs = 3;
    
    private static string _contextMessage = "";

    public static void Log(string message)
    {
        _actionLogs.Add(message);
        _fullHistory.Add(message);
        if (_actionLogs.Count > _maxLogs)
        {
            _actionLogs.RemoveAt(0);
        }
    }
    public static void SetContext(string context) => _contextMessage = context;

    public static List<string> GetLogs() => _actionLogs;
    public static string GetContext() => _contextMessage;
    public static List<string> GetFullHistory() => _fullHistory;
}