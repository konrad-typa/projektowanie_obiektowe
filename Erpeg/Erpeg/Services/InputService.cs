using Erpeg.Core.Interfaces;

namespace Erpeg.Services;
public class InputService : IService
{
    public void Initialize() { }

    public static Action<InputActionType> OnInput;
    
    public static void ReadInput()
    {
        if (!Console.KeyAvailable)
        {
            return;
        }
        var key = Console.ReadKey(true).Key;

        var action = key switch
        {
            ConsoleKey.A => InputActionType.MoveLeft,
            ConsoleKey.W => InputActionType.MoveUp,
            ConsoleKey.S => InputActionType.MoveDown,
            ConsoleKey.D => InputActionType.MoveRight,
            ConsoleKey.E => InputActionType.PickUp,
            ConsoleKey.Escape => InputActionType.Exit,
            ConsoleKey.I => InputActionType.OpenInventory,
            ConsoleKey.G => InputActionType.Drop,
            _ => InputActionType.None
        };

        if (action != InputActionType.None)
        {
            OnInput?.Invoke(action);
        }
    }
}