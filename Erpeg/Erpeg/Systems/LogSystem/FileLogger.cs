using Erpeg.Core.Interfaces;

namespace Erpeg.Systems.LogSystem;

public class FileLogger : ILogger
{
    private readonly string _filePath;
    private readonly ILogger _innerLogger;

    public FileLogger(string directoryPath, string playerName, ILogger innerLogger)
    {
        _innerLogger = innerLogger;
        string timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH:mm");
        string timeStamp2 = DateTime.Now.ToString("yyyy MM dd HH:mm");
        string fileName = $"{timeStamp}_{playerName}.log";
        
        _filePath = Path.Combine(directoryPath, fileName);
        Directory.CreateDirectory(directoryPath);
        File.WriteAllText(_filePath, $"--- {timeStamp2} session. Player: {playerName} ---\n");
    }

    public void Log(string message)
    {
        File.AppendAllText(_filePath, $"[{DateTime.Now:HH:mm::ss}] {message}\n");
        _innerLogger.Log(message);
    }

    public List<string> GetRecentLogs() => _innerLogger.GetRecentLogs();
    public List<string> GetFullHistory() => _innerLogger.GetFullHistory();
}