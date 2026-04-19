using Erpeg.Core.Interfaces;
using Erpeg.Core.StateMachine;
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
    private const bool IsRunning = true;
    private MapData _map;
    private PlayerData _player;
    private string _logPath = "";

    public void Run()
    {
        _map = MapSetup.SetupMap();
        
        _player = new("Tytus Bomba", (_map.SizeX/2, _map.SizeY/2));
        CharacterSpawner.SpawnPlayer(_map, _player);

        ILogger logger = new JournalLogger();
        logger = new FileLogger(_logPath, _player.Name, logger);
        GameLogger.Instance.Initialize(logger);
        
        GameStateManager.Initialize(new ExplorationState(_map, _player));
        DisplayService.Initialize();
        GameDiagnostics.Start();
        
        while (IsRunning)
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