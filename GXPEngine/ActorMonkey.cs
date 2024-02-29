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

        Sound jump;
        Sound launch;
        Sound land;
        Sound collect;
        Sound hit;
        Sound rock;
        Sound bomb;
        Sound die;


        public ActorMonkey(int x, int y)
        {
            sprite = new Sprite("Monkey.png");
            sprite.SetOrigin(sprite.width, sprite.height);
            sprite.SetXY(x, y);
            isResting = true;
            leftToRight = true;
            jump = new Sound("Audio/Monkey Launched.mp3", false, false);
            launch = new Sound("Audio/Bongo Launch.mp3", false, false);
            land = new Sound("Audio/Bongo Landing.mp3", false, false);
            collect = new Sound("Audio/Fruit Collect.mp3", false, false);
            hit = new Sound("Audio/Monkey Hurt.mp3", false, false);
            rock = new Sound("Audio/Rock Hit.mp3", false, false);
            bomb = new Sound("Audio/Bomb Hit.mp3", false, false);
            die = new Sound("Audio/Death.mp3", false, false);
        }

        public void reset()
        {
            isResting = true;
            leftToRight = true;
        }

        public void getHunted()
        {
            die.Play().Volume = 0.5F;
            Constants.lives = 0;
            Constants.hud.hideLives();
        }

        public override void Update()
        {
            if (isResting)
            {
                if (leftToRight)
                {
                    if (Input.GetKeyUp(Key.D))
                    {
                        launch.Play().Volume = 0.5F;
                        sprite.Mirror(true, false);
                        isResting = false;
                        Constants.playerAtLeft = false;
                        Constants.hunter.hide();
                    }
                }
                else
                {
                    if (Input.GetKeyUp(Key.A))
                    {
                        launch.Play().Volume = 0.5F;
                        sprite.Mirror(false, false);
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
                        jump.Play().Volume = 0.5F;
                        sprite.Mirror(true, false);
                        leftToRight = true;
                        jumpsRemain--;
                    }
                    if (Input.GetKeyUp(Key.A))
                    {
                        jump.Play().Volume = 0.5F;
                        sprite.Mirror(false, false);
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
                            land.Play().Volume = 0.5F;
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
                            land.Play().Volume = 0.5F;
                            sprite.Move(64,0);
                            isResting = true;
                            Constants.playerAtRight = true;
                            Constants.hunter.moveRight();
                            leftToRight = false;
                            jumpsRemain = 2;
                        }
                        break;
                    case "fruit":
                        collect.Play().Volume = 0.5F;
                        Constants.score++;
                        collisions[i].LateDestroy();
                        break;
                    case "obstacle":
                        hit.Play().Volume = 0.5F;
                        Constants.lives--;
                        collisions[i].LateDestroy();
                        break;
                    case "Hunter":
                        die.Play().Volume = 0.5F;
                        Constants.lives = 0;
                        break;
                }

            }
        }
    }
}
