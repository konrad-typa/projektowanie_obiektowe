using Erpeg.Core.Interfaces;
using Erpeg.Core.StateMachine;
using Erpeg.Data.Models;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Services;
using Erpeg.Systems;
using Erpeg.Systems.GameStateSystems;
using Erpeg.Systems.LogSystem;
using Erpeg.Systems.WorldSetup;
using Erpeg.Systems.WorldSetup.Spawners;

namespace Erpeg.Core.GameLoop;

public class GameEngine
{
    private bool _isRunning = true;
    private MapData _map;
    private PlayerData _player;

    public void Run()
    {
        var configService = new GameConfigService();
        string filePath;
        while (true)
        {
            Console.WriteLine("Config File name/path:");
            filePath = Console.ReadLine()!;
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Config file not found. Try again.");
            }
            else break;
        }

        if (!configService.TryLoadConfig(filePath, out GameConfig? config, out var message))
        {
            Console.WriteLine($"Error loading config file: {message}");
            Console.WriteLine($"Press any key to exit.");
            Console.ReadKey();
            
            _isRunning = false;
            return;
        }
        Console.Clear();

        var mapIntro = config!.Strategy.StartMessage;
        foreach (char c in mapIntro)
        {
            Console.Write(c);
            Thread.Sleep(40);
            if (c == '.' || c == '!')
                Thread.Sleep(50);
        }
        Console.WriteLine($"\n\nPress any key to continue.");
        Console.ReadKey();
        Console.Clear();
        
        _map = MapSetup.SetupMap(config!.Strategy);
        _player = new(config!.PlayerName, (_map.SizeX/2, _map.SizeY/2));
        CharacterSpawner.SpawnPlayer(_map, _player);
        _player.RecalculateStats();

        ILogger logger = new JournalLogger();
        logger = new FileLogger(config!.LogFilePath, _player.Name, logger);
        GameLogger.Instance.Initialize(logger);
        
        GameStateManager.Initialize(new ExplorationState(_map, _player));
        DisplayService.Initialize();
        GameDiagnostics.Start();
        
        while (_isRunning)
        {
            Update();
            Draw();
            GameDiagnostics.Update();
            // Thread.Sleep(16); 
        }
    }
    private void Update()
    {
        InputService.ReadInput();
        GameStateManager.Update();
    }

    private void Draw()
    {
        var currentState = GameStateManager.CurrentState;
        var frame = RenderService.RenderFrame(_map, _player, currentState);
        DisplayService.Write(frame);
    }
}