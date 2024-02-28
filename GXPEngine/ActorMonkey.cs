using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class ActorMonkey : Actor
    {
        public bool isResting;
        public bool leftToRight;

        public ActorMonkey(int x, int y)
        {
            sprite = new Sprite("circle.png");
            sprite.SetOrigin(sprite.width, sprite.height);
            sprite.SetXY(x, y);
            isResting = true;
            leftToRight = true;
        }

        public void reset()
        {
            isResting = true;
            leftToRight = true;
        }

        public override void Update()
        {
            if (isResting)
            {
                if (leftToRight)
                {
                    if (Input.GetKey(Key.D))
                    {
                        isResting = false;
                        Constants.playerAtLeft = false;
                        Constants.hunter.hide();
                    }
                }
                else
                {
                    if (Input.GetKey(Key.A))
                    {
                        isResting = false;
                        Constants.playerAtRight = false;
                        Constants.hunter.hide();
                    }
                }
            }
            else
            {
                if (leftToRight)
                {
                    sprite.Move(4, 0);
                }
                else
                {
                    sprite.Move(-4, 0);
                }
            }

            GameObject[] collisions = sprite.GetCollisions();
            for (int i = 0; i < collisions.Length; i++)
            {
                switch (collisions[i].name)
                {
                    case "LBongo":
                        if (!leftToRight)
                        {
                            sprite.Move(-64, 0);
                            isResting = true;
                            Constants.playerAtLeft = true;
                            Constants.hunter.moveLeft();
                            leftToRight = true;
                        }
                        break;
                    case "RBongo":
                        if (leftToRight)
                        {
                            sprite.Move(64,0);
                            isResting = true;
                            Constants.playerAtRight = true;
                            Constants.hunter.moveRight();
                            leftToRight = false;
                        }
                        break;
                    case "fruit":
                        Constants.score++;
                        collisions[i].LateDestroy();
                        break;
                    case "obstacle":
                        Constants.lives--;
                        collisions[i].LateDestroy();
                        break;
                    case "Hunter":
                        Constants.lives = 0;
                        break;
                }

            }
        }
    }
}
