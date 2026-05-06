using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.EventSystems;

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
            
            EventManager.SoundSystem.RegisterObserver(enemy);
            EventManager.SpeciesSystem.RegisterObserver(enemy);
            
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
                bool isCharacterHere = map.Characters.ContainsKey(targetPos);
                bool isItemHere = map.Items.ContainsKey(targetPos);
                
                if (!isCharacterHere && !isItemHere)
                {
                    enemy.Position = targetPos;
                    map.Characters[targetPos] = enemy;
                    return; 
                }
            }
            attempts++;
        }
    }
}