using Erpeg.Data.Models.Maps;

namespace Erpeg.Core.Interfaces;

public interface IDungeonGenStrategy
{
    MapData Generate();
}