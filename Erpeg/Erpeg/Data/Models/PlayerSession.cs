using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Data.Models;

public class PlayerSession
{
    public PlayerData Player { get; }
    public ILogger Logger { get; }
    public IGameState CurrentState { get; private set; }

    public PlayerSession(PlayerData player, ILogger logStrategy)
    {
        Player = player;
        
        var seshLogger = new GameLogger(); 
        seshLogger.Initialize(logStrategy);
        Logger = seshLogger;
    }
    
    public void Initialize(IGameState initialState)
    {
        CurrentState = initialState;
    }
    
    public void ChangeState(IGameState newState)
    {
        CurrentState = newState;
    }

    public void HandleInput(ConsoleKey key)
    {
        CurrentState?.HandleInput(key);
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}