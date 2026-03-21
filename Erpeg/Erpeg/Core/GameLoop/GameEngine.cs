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
    private PlayerData _player = new("Tytus Bomba", (0, 0));

    public void Run()
    {
        _map = MazeGenerator.Generate();
        MapSetup.SetupMap(_map);
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
        var frame = RenderService.RenderFrame(_map, _player);
        DisplayService.Write(frame);
    }
}