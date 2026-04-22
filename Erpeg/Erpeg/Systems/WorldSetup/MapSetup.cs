using Erpeg.Core.Interfaces;
using Erpeg.Data.Content.Items;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.WorldSetup.Spawners;

namespace Erpeg.Systems.WorldSetup;

public static class MapSetup
{
    public static MapData SetupMap(IDungeonGenStrategy strategy)
    {
        var generated = strategy.Generate();

        return generated;
    }
}