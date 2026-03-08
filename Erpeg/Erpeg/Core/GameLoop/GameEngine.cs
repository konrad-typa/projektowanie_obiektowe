using System.Data;
using Erpeg.Core.Interfaces;
using Erpeg.Services;

namespace Erpeg.Core.GameLoop;

public class GameEngine
{
    private bool _isRunning = true;

    private List<IService> _services = new List<IService>
    {
        new RenderService()
    };

    public void Run()
    {
        foreach (var service in _services)
        {
            service.Initialize();
        }

        while (_isRunning)
        {
            Update();
            Draw();
        }
    }
    private void Update()
    {
        throw new NotImplementedException();
    }

    private void Draw()
    {
        throw new  NotImplementedException();
    }
}