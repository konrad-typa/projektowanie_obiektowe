using System.Diagnostics;

namespace Erpeg.Systems;

public static class GameDiagnostics
{
    public static int FPS { get; private set; } = 0;

    private static Stopwatch _timer = new Stopwatch();
    private static int _frameCount = 0;
    private static double _timeSinceLastUpdate = 0;
    
    public static void Start()
    {
        _timer.Start();
    }
    
    public static void Update()
    {
        _frameCount++;
        _timeSinceLastUpdate += _timer.Elapsed.TotalSeconds;
        _timer.Restart();
        
        if (_timeSinceLastUpdate >= 1.0)
        {
            FPS = (int)(_frameCount / _timeSinceLastUpdate);
            
            _frameCount = 0;
            _timeSinceLastUpdate = 0;
        }
    }
}