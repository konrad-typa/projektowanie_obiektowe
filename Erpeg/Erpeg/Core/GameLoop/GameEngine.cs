using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Services;
using Erpeg.Systems.MovementSystem;
using Erpeg.Systems.WorldSetup;
using Erpeg.Systems.WorldSetup.Spawners;

namespace Erpeg.Core.GameLoop;

public class GameEngine
{
    private readonly bool _isRunning = true;
    private MapData _map;
    private PlayerData _player = new PlayerData("Tytus Bomba", (0, 0));

    public void Run()
    {
        _map = MazeGenerator.Generate();
        MapSetup.SetupMap(_map);
        CharacterSpawner.SpawnPlayer(_map, _player);
        
        while (_isRunning)
        {
            Update();
            Draw();
        }
    }
    private void Update()
    {
        InputService.ReadInput();
        if (InputService.LastKey != null)
        {
            PlayerMovement.TryMovePlayer(_map, _player,  InputService.LastKey.Value);
        }
    }

    private void Draw()
    {
        var frame = RenderService.RenderFrame(_map);
        DisplayService.Write(frame);
        Thread.Sleep(16);
    }
}