using System.Data;
using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Maps;
using Erpeg.Services;
using Erpeg.Systems.WorldSetup;

namespace Erpeg.Core.GameLoop;

public class GameEngine
{
    private bool _isRunning = true;
    private MapData _map;

    public void Run()
    {
        _map = MazeGenerator.Generate();
        MapInitializer.SetupMap(_map);
        
        while (_isRunning)
        {
            //Update();
            Draw();
        }
    }
    private void Update()
    {
        
    }

    private void Draw()
    {
        var frame = RenderService.RenderFrame(_map);
        DisplayService.Write(frame);
        Thread.Sleep(16);
    }
}