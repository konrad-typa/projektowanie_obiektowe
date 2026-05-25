using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Erpeg.Data.DTOs;
using Erpeg.Services;
using Erpeg.Services.RenderServices;
using Erpeg.Systems;

namespace Erpeg.Core.GameLoop;

public class Client(IPAddress? ip = null, int port = 5555)
{
    private readonly IPAddress _ip = ip ?? IPAddress.Loopback;
    private MapDTO? _cachedMap;

    public async Task RunAsync()
    {
        GameDiagnostics.Start();
        await Connect();
    }

    private async Task Connect()
    {
        var ipEndpoint = new IPEndPoint(_ip, port);
        using var client = new TcpClient();
        
        await client.ConnectAsync(ipEndpoint);
        Console.WriteLine($"Connected to {ipEndpoint}");
        
        await using var stream = client.GetStream();
        using var reader = new StreamReader(stream);
        await using var writer = new StreamWriter(stream);
        writer.AutoFlush = true;

        _ = Task.Run(() => HandleInput(writer));
        
        while (await reader.ReadLineAsync() is { } line)
        {
            try
            {
                var received = JsonSerializer.Deserialize<LocalGameStateDTO>(line);
                if (received is not null)
                {
                    _cachedMap ??= received.Map;
                    var state = new LocalGameStateDTO
                    {
                        Map = new MapDTO
                        {
                            Name = _cachedMap?.Name,
                            SizeX = _cachedMap?.SizeX ?? 0,
                            SizeY = _cachedMap?.SizeY ?? 0,
                            Tiles = _cachedMap?.Tiles,
                            Items = received.Map!.Items,
                            Characters = received.Map!.Characters
                        },
                        AvailableActions = received.AvailableActions,
                        InventoryInfo = received.InventoryInfo,
                        LocalPlayer = received.LocalPlayer,
                        Logs = received.Logs,
                        UIContext = received.UIContext
                    };
                    
                    var frame = RenderService.RenderFrame(state);
                    
                    Console.CursorVisible = false;
                    DisplayService.Write(frame);
                    
                    GameDiagnostics.Update();
                }
            }
            catch (JsonException)
            {
                // ignorowane
            }
        }
    }

    private async Task HandleInput(StreamWriter writer)
    {
        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                var inputDto = new PlayerInputDto {Key = key};
                
                var serializedInput = JsonSerializer.Serialize(inputDto);
                await writer.WriteLineAsync(serializedInput);
            }

            await Task.Delay(20);
        }
    }
}