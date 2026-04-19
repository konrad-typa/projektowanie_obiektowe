using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Data.Models.Items;

public class CoinItem(int amount) : Item("Coins", amount, 0, '◎')
{
    public override void OnPickedUp(PlayerData player, MapData map)
    {
        player.AddCoins(this.Value);
        map.Items.Remove(player.Position);
        GameLogger.Instance.Log($"Picked up {Value} coins.");
    }
}

public class GoldItem(int amount) : Item("Gold", amount, 0, '$')
{
    public override void OnPickedUp(PlayerData player, MapData map)
    {
        player.AddGold(this.Value);
        map.Items.Remove(player.Position);
        GameLogger.Instance.Log($"Picked up {Value} gold.");
    }
}