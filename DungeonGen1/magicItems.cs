using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DungeonGen1
{
    class magicItems 
    {
        public magicItems(string name)
        {
            this.name = name;
        }
        string name;
        char marker;
        public Point DeterminePlace(dungeonRoom room, mapTile[,] currentMap)
        {
            for(int y = room.upperleftY; y< room.upperleftY + room.height; y++)
            {
                for(int x = room.upperleftX; x < room.upperleftX+ room.width; x++)
                {
                    if(currentMap[x,y].contents.Equals(' '))
                    {
                        Point toGo = new Point(x, y);
                        return toGo;
                    }
                }
            }
            return new Point (-1,-1);
        }
    }
}
