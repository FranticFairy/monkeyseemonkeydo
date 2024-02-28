using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class ActorItem : Actor
    {

        public int lane;

        public ActorItem(int lane, string name)
        {
            this.lane = lane;
            if (name == "fruit")
            {
                sprite = new Sprite("colors.png");
            }
            else
            {
                sprite = new Sprite("square.png");
            }
            this.sprite.name = name;
            sprite.SetOrigin(sprite.width, sprite.height);

            int x = 0;

            switch (lane)
            {
                case 1:
                    x = 619 + 128;
                    break;

                case 2:
                    x = 841 + 128;
                    break;

                default:
                    x = 397 + 128;
                    break;
            }

            sprite.SetXY(x, 0);
        }

        public override void Update()
        {

            sprite.Move(0, 2);
        }
    }
}
