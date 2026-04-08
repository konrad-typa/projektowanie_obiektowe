using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;

namespace Erpeg.Data.Models.Items;

public class CoinItem : Item
{
    public CoinItem(int amount) : base("Coins", amount, 0, '◎') { }

    public override void OnPickedUp(PlayerData player, MapData map)
    {
        player.AddCoins(this.Value);
        map.Items.Remove(player.Position);
        MessageLogSystem.Log($"Picked up {Value} coins.");
    }
}

public class GoldItem : Item
{
    public GoldItem(int amount) : base("Gold", amount, 0, '$') { }

    public override void OnPickedUp(PlayerData player, MapData map)
    {
        player.AddGold(this.Value);
        map.Items.Remove(player.Position);
        MessageLogSystem.Log($"Picked up {Value} gold.");
    }
}