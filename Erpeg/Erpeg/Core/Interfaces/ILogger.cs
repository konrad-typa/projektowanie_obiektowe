namespace Erpeg.Core.Interfaces;

public interface ILogger
{
    void Log(string message);
    List<string> GetRecentLogs();
    List <string> GetFullHistory();
}