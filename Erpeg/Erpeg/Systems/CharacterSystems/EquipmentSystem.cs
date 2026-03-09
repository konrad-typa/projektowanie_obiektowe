using System.Runtime.ExceptionServices;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;
using Erpeg.Services;
using Erpeg.Systems.CharacterSystems;

namespace Erpeg.Systems.CharacterSystems;

public class EquipmentSystem
{
    public static void Subscribe(PlayerData player)
    {
        InputService.OnInput += (action) =>
        {
            if (action == InputActionType.OpenInventory)
                TryOpen(player);
        };
    }

    private static void TryOpen(PlayerData player)
    {
        InputService.OnInput += (action) =>
        {
            if (action == InputActionType.PickUp)
                TryEquip(player);
        };
    }

    private static void TryEquip(PlayerData player)
    {
        
    }
}