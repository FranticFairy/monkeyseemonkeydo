using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class ActorHunter : Actor
    {

        public AnimationSprite hunterSprite = new AnimationSprite("Hunter.png", 5, 5);

        Sound appear;
        Sound shoot;
        bool appeared = false;
        bool shot = false;

        public ActorHunter()
        {
            appear = new Sound("Audio/Tiki Appear.mp3", false, false);
            shoot = new Sound("Audio/Blow Dart Shoot.mp3", false, false);
            sprite = new AnimationSprite("Hunter.png",5,5);
            hunterSprite.SetOrigin(hunterSprite.width, hunterSprite.height);
            SetXY(0, 0);
            hunterSprite.SetXY(0, 0);
            hunterSprite.visible = false;
            this.hunterSprite.name = "Hunter";
        }

        public override void Update()
        {
            if(Constants.playerAtLeft || Constants.playerAtRight)
            {
                hunterSprite.Animate(0.05F);
                if (hunterSprite.currentFrame == 12)
                {
                    if (appeared == false)
                    {
                        appeared = true;
                        appear.Play().Volume = 0.5F;
                    }
                }
                if (hunterSprite.currentFrame == 20)
                {
                    if (shot == false)
                    {
                        shot = true;
                        shoot.Play().Volume = 0.5F;
                    }
                }
                if (hunterSprite.currentFrame == 23)
                {
                    if (Constants.lives > 0)
                    {
                        Constants.player.getHunted();
                    }
                }
                //sprite.Move(0, 1);
            }

        }

        void playSound()
        {
        }

        public void reset()
        {
            SetXY(0, 0);
            hunterSprite.SetXY(0, 0);
            hunterSprite.visible = false;
            shot = false;
            appeared = false;
        }

        public void hide()
        {
            hunterSprite.visible = false;
        }
        public void moveLeft()
        {
            hunterSprite.Mirror(false, false);
            hunterSprite.SetFrame(0);
            SetXY(167, 698);
            hunterSprite.SetXY(167, 698);
            hunterSprite.visible = true;
        }

        public void moveRight()
        {
            hunterSprite.Mirror(true, false);
            hunterSprite.SetFrame(0);
            SetXY(1325, 698);
            hunterSprite.SetXY(1325, 698);
            hunterSprite.visible = true;
        }
    }
}
