using Erpeg.Data.Content.Items;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.WorldSetup.Spawners;

namespace Erpeg.Systems.WorldSetup;

public static class MapSetup
{
    public static MapData SetupMap()
    {
        var strategy = new ClassicDungeonGenerator();
        var generated = strategy.Generate();

        return generated;
    }
}