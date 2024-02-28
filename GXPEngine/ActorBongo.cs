using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class ActorBongo : Actor
    {

        public ActorBongo(int x, int y, string name)
        {
            sprite = new Sprite("triangle.png");
            sprite.SetOrigin(sprite.width, sprite.height);
            sprite.SetXY(x, y);
            this.sprite.name = name;

        }

    }
}
