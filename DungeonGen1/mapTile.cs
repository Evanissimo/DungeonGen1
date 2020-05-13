using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGen1
{
    public class mapTile
    {
        public bool exists
        { get; set; }
        public char contents
        {get; set;}
        public mapTile()
        {
            this.exists = false;
            this.contents = '\0';
        }
    }
}
