using System.Collections.Generic;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Core.Utils
{
    public static class PathfindingHelper
    {
        public static bool CanHearSound(MapData map, (int x, int y) startPos, (int x, int y) targetPos, int maxRange, out int distance)
        {
            distance = -1;
            
            if (startPos == targetPos)
            {
                distance = 0;
                return true;
            }

            var queue = new Queue<((int x, int y) pos, int dist)>();
            bool[,] visited = new bool[map.SizeX, map.SizeY];

            queue.Enqueue((startPos, 0));
            visited[startPos.x, startPos.y] = true;
            
            var directions = new (int dx, int dy)[] 
            {
                (0, -1), (0, 1), (-1, 0), (1, 0)
            };

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                
                if (current.dist >= maxRange) 
                    continue;

                foreach (var dir in directions)
                {
                    int nextX = current.pos.x + dir.dx;
                    int nextY = current.pos.y + dir.dy;
                    var nextPos = (nextX, nextY);
                    
                    if (nextX >= 0 && nextX < map.SizeX && nextY >= 0 && nextY < map.SizeY)
                    {
                        if (map.Layout[nextX, nextY] != TileType.Wall && !visited[nextX, nextY])
                        {
                            int nextDist = current.dist + 1;
                            
                            if (nextPos == targetPos)
                            {
                                distance = nextDist;
                                return true;
                            }

                            visited[nextX, nextY] = true;
                            queue.Enqueue((nextPos, nextDist));
                        }
                    }
                }
            }
            
            return false;
        }
    }
}