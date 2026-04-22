using System.Text.Json.Serialization;
using Erpeg.Core.Interfaces;

namespace Erpeg.Data.Models;

public record GameConfig( 
    string PlayerName, 
    [property: JsonPropertyName("DungeonTheme")] string ThemeClassName, 
    string LogFilePath)
{
    [JsonIgnore]
    public IDungeonGenStrategy Strategy 
    {
        get 
        {
            string fullTypeName = $"Erpeg.Systems.WorldSetup.{ThemeClassName}";
            
            Type? type = Type.GetType(fullTypeName);

            if (type != null && typeof(IDungeonGenStrategy).IsAssignableFrom(type))
            {
                return (IDungeonGenStrategy)Activator.CreateInstance(type)!;
            }

            // bezpiecznik
            return new Systems.WorldSetup.ClassicDungeon();
        }
    }
}