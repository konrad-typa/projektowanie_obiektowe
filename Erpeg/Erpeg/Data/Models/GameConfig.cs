using Erpeg.Core.Interfaces;

namespace Erpeg.Data.Models;

public record GameConfig(
    string PlayerName, 
    IDungeonGenStrategy DungeonTheme, 
    string LogFilePath
);