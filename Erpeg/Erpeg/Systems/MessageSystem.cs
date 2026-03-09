namespace Erpeg.Systems;

using System.Collections.Generic;

public static class MessageLogSystem
{
    private static readonly Queue<string> _messages = new();
    private const int MaxMessages = 3;

    public static void Log(string message)
    {
        _messages.Enqueue(message);
        if (_messages.Count > MaxMessages)
            _messages.Dequeue();
    }

    public static List<string> GetMessages() => new(_messages);
}