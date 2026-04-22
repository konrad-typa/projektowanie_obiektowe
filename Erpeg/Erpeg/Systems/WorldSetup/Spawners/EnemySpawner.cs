using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup.Spawners;

public static class EnemySpawner
{
    private static readonly Random Random = new Random();

    public static void SpawnThemeEnemies(MapData map, int count, params Func<EnemyData>[] enemyGens)
    {
        for (int i = 0; i < count; i++)
        {
            var generator = enemyGens[Random.Next(enemyGens.Length)];
            var enemy = generator();
            
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