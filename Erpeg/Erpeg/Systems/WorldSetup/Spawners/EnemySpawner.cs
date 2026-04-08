using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup.Spawners; // Lub Erpeg.Systems.WorldSetup.Spawners, zależnie od Twoich folderów

public static class EnemySpawner
{
    private static readonly Random Random = new Random();

    public static void SpawnRandomEnemies(MapData map, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var enemy = Random.Next(3) switch
            {
                0 => new EnemyData("Goblin", (0, 0), attack: 15, defense: 5, maxhp: 50, hp: 50),
                1 => new EnemyData("Orc", (0, 0), attack: 30, defense: 10, maxhp: 120, hp: 120),
                2 => new EnemyData("Troll", (0, 0), attack: 50, defense: 20, maxhp: 250, hp: 250),
                _ => new EnemyData("Goblin", (0, 0), attack: 15, defense: 5, maxhp: 50, hp: 50)
            };
            
            PlaceEnemyRandomly(map, enemy);
        }
    }

    private static void PlaceEnemyRandomly(MapData map, EnemyData enemy)
    {
        int attempts = 0;
        
        while (attempts < 100) 
        {
            int x = Random.Next(map.SizeX);
            int y = Random.Next(map.SizeY);
            var targetPos = (x, y);
            
            if (map.Layout[x, y] != TileType.Wall)
            {
                bool isCharacterHere = map.Characters.Any(c => c.Position == targetPos);
                
                bool isItemHere = map.Items.ContainsKey(targetPos);
                
                if (!isCharacterHere && !isItemHere)
                {
                    enemy.Position = targetPos;
                    map.Characters.Add(enemy);
                    return; 
                }
            }
            attempts++;
        }
    }
}