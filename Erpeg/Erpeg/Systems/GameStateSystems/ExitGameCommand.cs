using Erpeg.Core.Interfaces;
using Erpeg.Data.Models;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.GameStateSystems;

public class ExitGameCommand(MapData map, PlayerSession session) : ICommand
{
    public void Execute()
    {
        map.Characters.Remove(session.Player.Position);
        session.Initialize(new GameOverState(session));
    }
}