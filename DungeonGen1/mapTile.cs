using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGen1
{
    public class mapTile
    {
        public int exists /// 1 = exists, 0 = does not exist, 2 = does not exist and is adjacent to an existing room as a wall
        { get; set; }
        
        public char contents
        {get; set;}
        public mapTile()
        {
            this.exists = 0;
            this.contents = '\0';
        }
    }
}
