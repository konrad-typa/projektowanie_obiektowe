namespace Erpeg.Core.Interfaces;

public interface ILogger
{
    void Log(string message);
    List<(DateTime Time,  string Message)> GetRecentLogs();
    List <(DateTime Time, string Message)> GetFullHistory();
}