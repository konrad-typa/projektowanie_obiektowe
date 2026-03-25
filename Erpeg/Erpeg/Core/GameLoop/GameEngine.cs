using Erpeg.Core.StateMachine;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Services;
using Erpeg.Systems;
using Erpeg.Systems.GameStates;
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
        _map = MapSetup.SetupMap();
        _player = new("Tytus Bomba", (_map.SizeX/2, _map.SizeY/2));
        
        CharacterSpawner.SpawnPlayer(_map, _player);
        
        GameStateManager.Initialize(new ExplorationState(_map, _player));
        DisplayService.Initialize();
        GameDiagnostics.Start();
        
        while (_isRunning)
        {
            Update();
            Draw();
            GameDiagnostics.Update();
            //Thread.Sleep(16);
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