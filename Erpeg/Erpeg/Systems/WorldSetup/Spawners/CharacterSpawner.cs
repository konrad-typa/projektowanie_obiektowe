using Erpeg.Data.Content.Items;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup.Spawners;

public static class CharacterSpawner
{
    private static readonly Random Random = new Random();
    public static void SpawnPlayer(MapData map, PlayerData player)
    {
        map.Characters.Add(player);
    }
}