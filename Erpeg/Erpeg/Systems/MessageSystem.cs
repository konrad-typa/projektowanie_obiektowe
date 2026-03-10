namespace Erpeg.Systems;

public static class MessageLogSystem
{
    private static string? _currentMessage = "";

    public static void Log(string message) => _currentMessage = message;
    public static string? GetCurrent() => _currentMessage;
    public static void Clear() => _currentMessage = "";
}