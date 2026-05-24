using Erpeg.Core.Interfaces;
using Erpeg.Data.DTOs;
using Erpeg.Data.Models;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Services;
using Erpeg.Services.RenderServices;
using Erpeg.Systems;
using Erpeg.Systems.EventSystems;
using Erpeg.Systems.GameStateSystems;
using Erpeg.Systems.LogSystem;
using Erpeg.Systems.WorldSetup;
using Erpeg.Systems.WorldSetup.Spawners;

namespace Erpeg.Core.GameLoop;

public class GameEngine
{
    private bool _isRunning = true;
    private MapData _map;
    private PlayerSession _playerSession;
    
    public void Run()
    {
        var configService = new GameConfigService();
        
        string filePath = "uni_config.json";
        /*
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
        */

        if (!configService.TryLoadConfig(filePath, out GameConfig? config, out var message))
        {
            Console.WriteLine($"Error loading config file: {message}");
            Console.WriteLine($"Press any key to exit.");
            Console.ReadKey();
            
            _isRunning = false;
            return;
        }
        Console.Clear();

        bool skipped = false;
        var mapIntro = config!.Strategy.StartMessage;
        foreach (char c in mapIntro)
        {
            Console.Write(c);
            if (Console.KeyAvailable)
                skipped = true;
            
            if (!skipped)
            {
                Thread.Sleep(40);
                if (c == '.' || c == '!')
                    Thread.Sleep(50);
            }
        }
        while (Console.KeyAvailable) Console.ReadKey(intercept: true);
        Console.WriteLine($"\n\nPress any key to continue.");
        Console.ReadKey(intercept: true);
        Console.Clear();
        
        EventManager.Initialize();
        
        _map = MapSetup.SetupMap(config.Strategy);
        var player = new PlayerData(config.PlayerName, (_map.SizeX/2, _map.SizeY/2));
        CharacterSpawner.SpawnPlayer(_map, player);
        player.RecalculateStats();

        ILogger logger = new JournalLogger();
        logger = new FileLogger(config!.LogFilePath, player.Name, logger);
        _playerSession = new PlayerSession(player, logger);
        
        _playerSession.Initialize(new ExplorationState(_map, _playerSession));
        
        DisplayService.Initialize();
        GameDiagnostics.Start();
        
        while (_isRunning)
        {
            Update();
            Draw();
            GameDiagnostics.Update();
            Thread.Sleep(50); // tickrate 20 
        }
    }
    private void Update()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            _playerSession.HandleInput(key);
        }
        _playerSession.Update();
        
        var enemies = _map.Characters.Values.OfType<EnemyData>().ToList();
        foreach (var e in enemies)
            if (e.IsMoving)
                e.MoveRandomly(_map);
    }

    private void Draw()
    {
        var gameStateDto = _playerSession.ToDto(_map);
        var frame = RenderService.RenderFrame(gameStateDto);
        DisplayService.Write(frame);
    }
}