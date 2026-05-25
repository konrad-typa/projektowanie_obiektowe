using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Erpeg.Core.Interfaces;
using Erpeg.Data.DTOs;
using Erpeg.Data.Models;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Services;
using Erpeg.Systems.EventSystems;
using Erpeg.Systems.GameStateSystems;
using Erpeg.Systems.LogSystem;
using Erpeg.Systems.WorldSetup;
using Erpeg.Systems.WorldSetup.Spawners;

namespace Erpeg.Core.GameLoop;

public class Server (int port = 5555)
{
    private readonly List<Task> _clients = new ();
    private MapData _sharedMap;
    private GameConfig? _config;
    private bool _isServerRunning = true;
    private readonly ConcurrentDictionary<int, PlayerSession> _activeSessions = new ();
    private readonly ConcurrentDictionary<int, StreamWriter> _writers = new ();
    private static readonly ConcurrentQueue<PlayerInputDto> CommandQueue = new();
    
    private static readonly JsonSerializerOptions JsonOptions = new() 
    { 
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull 
    };
    
    public async Task RunAsync()
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}]" +
                          $" Starting server on port {port}");
        // 
        if (!TryLoadConfig())
            return;

        _ = Task.Run(StartServerLoop);

        var ipEndPoint = new IPEndPoint(IPAddress.Any, port);
        using var listener = new TcpListener(ipEndPoint);
        var cts = new CancellationTokenSource();
        await AcceptClients(listener, cts.Token);
        
        //
        _isServerRunning = false;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}]" +
                          $" Server finished on port {port}");
    }

    private async Task StartServerLoop()
    {
        while (_isServerRunning)
        {
            while (CommandQueue.TryDequeue(out var command))
            {
                if (_activeSessions.TryGetValue(command.Id, out var playerSession))
                {
                    playerSession.HandleInput(command.Key);
                }
            }

            foreach (var session in _activeSessions.Values)
            {
                session.Update();
            }
            
            var enemies = _sharedMap.Characters.Values.OfType<EnemyData>().ToList();
            foreach (var e in enemies)
                if (e.IsMoving)
                    e.MoveRandomly(_sharedMap);

            foreach (var session in _activeSessions)
            {
                int id = session.Key;
                var playerSession = session.Value;

                if (_writers.TryGetValue(id, out var writer))
                {
                    GameUpdateDTO updateDto = playerSession.ToDto(_sharedMap);
                    var cachedMap = _sharedMap.ToStaticDto();
                    LocalGameStateDTO stateDto = new LocalGameStateDTO
                    {
                        Map = new MapDTO
                        {
                            Name = cachedMap.Name,
                            SizeX = cachedMap.SizeX,
                            SizeY = cachedMap.SizeY,
                            Tiles = null,
                            Items = updateDto.Map!.Items,
                            Characters = updateDto.Map.Characters,
                        },
                        AvailableActions = updateDto.AvailableActions,
                        InventoryInfo = updateDto.InventoryInfo,
                        LocalPlayer = updateDto.LocalPlayer,
                        Logs = updateDto.Logs,
                        UIContext = updateDto.UIContext,
                    };

                    try
                    {
                        var serializedDto = JsonSerializer.Serialize(stateDto, JsonOptions);
                        await writer.WriteLineAsync(serializedDto);
                    }
                    catch (Exception)
                    {
                        // obslugiwane w handleclient
                    }
                }
            }

            await Task.Delay(30);
        }
    }
    
    private async Task AcceptClients(TcpListener tcpListener, CancellationToken token = default)
    {
        tcpListener.Start(9);
        int idx = 0;
        
        while (true)
        {
            try
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync(token);
                
                var clientTask = Task.Run(() => HandleClient(client, ++idx), token);
                _clients.Add(clientTask);
                
                _clients.RemoveAll(t => t.IsCompleted);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Token cancelled");
                break;
            }
        }
        
        tcpListener.Dispose();
        await Task.WhenAll(_clients);
    }
    
    private async Task HandleClient(TcpClient client, int idx)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] New Client connected. Id: {idx}");

        try
        {
            await using var stream = client.GetStream();
            await using var writer = new StreamWriter(stream);
            writer.AutoFlush = true;
            using var reader = new StreamReader(stream);
            
            // odebranie nazwy gracza (!!!)
            string name = "Tytus Bomba";
            
            // inicjalizacja gracza
            var player = new PlayerData(name, (_sharedMap.SizeX/2, _sharedMap.SizeY/2));
            CharacterSpawner.SpawnPlayer(_sharedMap, player);
            player.RecalculateStats();

            ILogger logger = new JournalLogger();
            logger = new FileLogger(_config!.LogFilePath, name, logger);
            
            var playerSession = new PlayerSession(player, logger);
            playerSession.Initialize(new ExplorationState(_sharedMap, playerSession));

            /*--- dane dla klienta ---*/
            GameUpdateDTO updateDto = playerSession.ToDto(_sharedMap);
            var cachedMap = _sharedMap.ToStaticDto();
            LocalGameStateDTO initGameStateDto = new LocalGameStateDTO
            {
                Map = new MapDTO
                {
                    Name = cachedMap.Name,
                    SizeX = cachedMap.SizeX,
                    SizeY = cachedMap.SizeY,
                    Tiles = cachedMap.Tiles,
                    Items = updateDto.Map!.Items,
                    Characters = updateDto.Map.Characters,
                },
                AvailableActions = updateDto.AvailableActions,
                InventoryInfo = updateDto.InventoryInfo,
                LocalPlayer = updateDto.LocalPlayer,
                Logs = updateDto.Logs,
                UIContext = updateDto.UIContext,
            };
            
            // serializacja
            var serializedInitDto =  JsonSerializer.Serialize(initGameStateDto, JsonOptions);
            
            // wysłanie pierwszych danych
            await writer.WriteLineAsync(serializedInitDto);
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Sent Data to Client {idx}");
            
            _writers.TryAdd(idx, writer);
            _activeSessions.TryAdd(idx, playerSession);
            
            // update
            while (client.Connected)
            {
                var received = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(received))
                    break;

                var input = JsonSerializer.Deserialize<PlayerInputDto>(received);
                if (input != null)
                    CommandQueue.Enqueue(new PlayerInputDto {Id = idx, Key = input.Key});
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Token cancelled");
        }
        catch (IOException)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Client {idx} disconnected abruptly");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Error handling client {idx}: {ex.Message}");
        }
        finally
        {
            _writers.TryRemove(idx, out _);
            if (_activeSessions.TryRemove(idx, out var session))
            {
                _sharedMap.Characters.Remove(session.Player.Position);
            }
            client.Dispose();
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Client {idx} disconnected");
        }
    }
    
    private bool TryLoadConfig()
    {
        var configService = new GameConfigService();
        
        string? filePath = "uni_config.json";
        
        /*
        while (true)
        {
            Console.WriteLine("Config File name/path:");
            filePath = Console.ReadLine();
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Config file not found. Try again.");
            }
            else break;
        }
        */
        
        if (!configService.TryLoadConfig(filePath, out _config, out var message))
        {
            Console.WriteLine($"Error loading config file: {message}");
            Console.WriteLine($"Press any key to exit.");
            Console.ReadKey();
            return false;
        }
        
        EventManager.Initialize();
        _sharedMap = MapSetup.SetupMap(_config!.Strategy);
        
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Config loaded successfully");
        return true;
    }
}