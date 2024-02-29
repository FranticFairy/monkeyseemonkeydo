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
        public int jumpsRemain = 2;

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
                    if (Input.GetKeyUp(Key.D))
                    {
                        isResting = false;
                        Constants.playerAtLeft = false;
                        Constants.hunter.hide();
                    }
                }
                else
                {
                    if (Input.GetKeyUp(Key.A))
                    {
                        isResting = false;
                        Constants.playerAtRight = false;
                        Constants.hunter.hide();
                    }
                }
            } 
            else
            {
                if (jumpsRemain > 0)
                {
                    if (Input.GetKeyUp(Key.D))
                    {
                        Console.WriteLine("D");
                        leftToRight = true;
                        jumpsRemain--;
                    }
                    if (Input.GetKeyUp(Key.A))
                    {
                        Console.WriteLine("A");
                        leftToRight = false;
                        jumpsRemain--;
                    }
                }

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
                            jumpsRemain = 2;
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
                            jumpsRemain = 2;
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
