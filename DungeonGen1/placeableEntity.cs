using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGen1
{
    abstract class placeableEntity
    {
        public abstract void decidePlace();
        public abstract void place(int x, int y, char marker);
    }

}
