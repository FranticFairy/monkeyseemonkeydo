using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class ActorHunter : Actor
    {
        public ActorHunter()
        {
            sprite = new Sprite("checkers.png");
            sprite.SetOrigin(sprite.width, sprite.height);
            SetXY(0, 0);
            sprite.SetXY(0, 0);
            sprite.visible = false;
            this.sprite.name = "Hunter";
        }

        public override void Update()
        {
            if(Constants.playerAtLeft || Constants.playerAtRight)
            {
                sprite.Move(0, 1);
            }
        }

        public void reset()
        {
            SetXY(0, 0);
            sprite.SetXY(0, 0);
            sprite.visible = false;
        }

        public void hide()
        {
            sprite.visible = false;
        }
        public void moveLeft()
        {
            SetXY(320, 200);
            sprite.SetXY(320, 200);
            sprite.visible = true;
        }

        public void moveRight()
        {
            SetXY(1184 - 64, 200);
            sprite.SetXY(1184 - 64, 200);
            sprite.visible = true;
        }
    }
}
