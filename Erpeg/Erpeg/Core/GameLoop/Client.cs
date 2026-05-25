using System.Net;

namespace Erpeg.Core.GameLoop;

public class Client(IPAddress? ip = null, int port = 5555)
{
    private readonly IPAddress _ip = ip ?? IPAddress.Loopback;
    private readonly int _port = port;

    public async Task RunAsync()
    {
        
    }
}