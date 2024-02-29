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
            if(name == "LBongo")
            {
                sprite = new Sprite("Drum_red.png");

            } else
            {
                sprite = new Sprite("Drum_blue.png");
            }
            sprite.SetOrigin(sprite.width, sprite.height);
            sprite.SetXY(x, y);
            this.sprite.name = name;

        }

    }
}
