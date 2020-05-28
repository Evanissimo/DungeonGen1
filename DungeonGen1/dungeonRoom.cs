using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGen1
{
    class dungeonRoom
    {
        public dungeonRoom()
        {
            
        }
        public dungeonRoom(int xOrig, int yOrig, int width, int height)
        {
            this.upperleftX = xOrig;
            this.upperleftY = yOrig;
            this.width = width;
            this.height = height;
        }
       public int upperleftX { get; set; }
       public int upperleftY { get; set; }
       public int width
        { get; set; }
       public int height
        { get; set; }
    }
}
