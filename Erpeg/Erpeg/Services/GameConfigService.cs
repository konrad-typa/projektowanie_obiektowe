using System.Text.Json;
using Erpeg.Data.Models;

namespace Erpeg.Services;

public class GameConfigService
{
    public bool TryLoadConfig(string fileName, out GameConfig? config, out string? message)
    {
        if (!File.Exists(fileName))
        {
            config = null;
            message = "Config file doesn't exist!";
            return false;
        }
        
        try
        {
            var options = new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            };
            
            config = JsonSerializer.Deserialize<GameConfig>(File.ReadAllText(fileName), options);
            message = null;
            return config != null;
        }
        catch (Exception ex)
        {
            config = null;
            message = ("Config file serializing error: " + ex.Message);
            return false;
        }
    }
}