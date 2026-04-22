using Erpeg.Core.Interfaces;

namespace Erpeg.Systems.LogSystem;

public class FileLogger : ILogger
{
    private readonly string _filePath;
    private readonly ILogger _innerLogger;

    public FileLogger(string directoryPath, string playerName, ILogger innerLogger)
    {
        _innerLogger = innerLogger;
        string timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss");
        string timeStamp2 = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        string fileName = $"{timeStamp}_{playerName}.log";

        try
        {
            _filePath = Path.Combine(directoryPath, fileName);
            Directory.CreateDirectory(directoryPath);
            File.WriteAllText(_filePath, $"--- {timeStamp2} session. Player: {playerName} ---\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine("File save error:" + ex.Message);
        }
    }

    public void Log(string message)
    {
        _innerLogger.Log(message);
        
        try
        {
            File.AppendAllText(_filePath, $"[{DateTime.Now:HH:mm:ss}] {message}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine("File save error:" + ex.Message);
        }
    }

    public List<string> GetRecentLogs() => _innerLogger.GetRecentLogs();
    public List<string> GetFullHistory() => _innerLogger.GetFullHistory();
}