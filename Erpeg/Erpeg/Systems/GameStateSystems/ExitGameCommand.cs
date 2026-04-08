using Erpeg.Core.Interfaces;

namespace Erpeg.Systems.GameStateSystems;

public class ExitGameCommand : ICommand
{
    public void Execute()
    {
        Environment.Exit(0);
    }
}