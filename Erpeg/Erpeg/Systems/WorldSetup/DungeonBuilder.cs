using System;
using System.Collections.Generic;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.WorldSetup.Spawners;

namespace Erpeg.Systems.WorldSetup;

public class DungeonBuilder
{
    private readonly MapData _map;
    private static readonly Random Random = new();
    
    private readonly List<(int x, int y)> _roomCenters = new();
    
    private DungeonBuilder(string name, int width, int height, TileType fillType)
    {
        _map = new MapData(name, width, height);
        for (int y = 0; y < _map.SizeY; y++)
        {
            for (int x = 0; x < _map.SizeX; x++)
            {
                _map.Layout[x, y] = fillType;
            }
        }
    }

    public static DungeonBuilder CreateEmpty(string name, int width, int height)
        => new DungeonBuilder(name, width, height, TileType.Empty);

    public static DungeonBuilder CreateFilled(string name, int width, int height)
        => new DungeonBuilder(name, width, height, TileType.Wall);
    
    public DungeonBuilder AddCentralHall(int roomWidth, int roomHeight)
    {
        int startX = Math.Max(1, (_map.SizeX - roomWidth) / 2);
        int startY = Math.Max(1, (_map.SizeY - roomHeight) / 2);
        
        DigArea(startX, startY, roomWidth, roomHeight);
        
        _roomCenters.Add((startX + roomWidth / 2, startY + roomHeight / 2));
        
        return this;
    }

    public DungeonBuilder AddRooms(int count, int minSize = 2, int maxSize = 4)
    {
        for (int i = 0; i < count; i++)
        {
            int attempts = 0;
            while (attempts < _map.SizeX * _map.SizeY)
            {
                int roomWidth = Random.Next(minSize, maxSize + 1) * 2;
                int roomHeight = Random.Next(minSize, maxSize + 1);
                
                int startX = Random.Next(1, _map.SizeX - roomWidth - 1);
                int startY = Random.Next(1, _map.SizeY - roomHeight - 1);

                if (IsSolid(startX, startY, roomWidth, roomHeight))
                {
                    DigArea(startX, startY, roomWidth, roomHeight);
                    _roomCenters.Add((startX + roomWidth / 2, startY + roomHeight / 2));
                    break;
                }
                attempts++;
            }
        }
        return this;
    }

    public DungeonBuilder AddCorridors()
    {
        if (_roomCenters.Count < 2) return this;
        
        for (int i = 1; i < _roomCenters.Count; i++)
        {
            var prevRoom = _roomCenters[i - 1];
            var currentRoom = _roomCenters[i];

            DigCorridor(prevRoom.x, prevRoom.y, currentRoom.x, currentRoom.y);
        }
        return this;
    }

    public DungeonBuilder AddItems(int count)
    {
        ItemSpawner.SpawnRandomItems(_map, count);
        return this;
    }

    public DungeonBuilder AddWeapons(int count)
    {
        ItemSpawner.SpawnRandomWeapons(_map, count);
        return this;
    }
    
    public DungeonBuilder AddEq(int count)
    {
        ItemSpawner.SpawnRandomEq(_map, count);
        return this;
    }

    public DungeonBuilder AddEnemies(int count)
    {
        EnemySpawner.SpawnRandomEnemies(_map, count);
        return this;
    }

    public MapData Build() => _map;
    
    // helpery
    private void DigArea(int startX, int startY, int width, int height)
    {
        for (int y = startY; y < startY + height; y++)
        {
            for (int x = startX; x < startX + width; x++)
            {
                _map.Layout[x, y] = TileType.Empty;
            }
        }
    }

    private bool IsSolid(int startX, int startY, int width, int height)
    {
        int checkStartX = Math.Max(0, startX - 1);
        int checkStartY = Math.Max(0, startY - 1);
        int checkEndX = Math.Min(_map.SizeX, startX + width + 2);
        int checkEndY = Math.Min(_map.SizeY, startY + height + 2);

        for (int y = checkStartY; y < checkEndY; y++)
        {
            for (int x = checkStartX; x < checkEndX; x++)
            {
                if (_map.Layout[x, y] != TileType.Wall) return false;
            }
        }
        return true;
    }

    private void DigCorridor(int x1, int y1, int x2, int y2)
    {
        int x = x1;
        int y = y1;
        
        while (x != x2)
        {
            _map.Layout[x, y] = TileType.Empty;
            x += Math.Sign(x2 - x1);
        }
        
        while (y != y2)
        {
            _map.Layout[x, y] = TileType.Empty;
            y += Math.Sign(y2 - y1);
        }
    }
}