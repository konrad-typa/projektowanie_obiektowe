using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Data.Models.Items;

public class FistsItem() : Item("Fists", 0, 0.0, ' ')
{
    public override void OnPickedUp(PlayerData player, MapData map){}
}