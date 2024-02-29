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
                Random randomFruit = new Random();
                int fruitNumber = randomFruit.Next(3);
                switch (fruitNumber)
                {
                    case 1:
                        sprite = new Sprite("Coconut_Halves.png");
                        break;
                    case 2:
                        sprite = new Sprite("Pineapple.png");
                        break;
                    default:
                        sprite = new Sprite("Banana.png");
                        break;
                }
            }
            else
            {
                Random randomObstacle = new Random();
                int obstacleNumber = randomObstacle.Next(4);
                switch (obstacleNumber)
                {
                    case 1:
                        sprite = new Sprite("Bomb_Barrel.png");
                        break;
                    case 2:
                        sprite = new Sprite("Bomb_Bomb.png");
                        break;
                    case 3:
                        sprite = new Sprite("Bomb_dynamite.png");
                        break;
                    default:
                        sprite = new Sprite("Rock.png");
                        break;
                }
            }

            this.sprite.name = name;
            sprite.SetOrigin(sprite.width, sprite.height);

            int x = 0;

            switch (lane)
            {
                case 1:
                    x = 714 + 128;
                    break;

                case 2:
                    x = 878 - 32;
                    break;

                default:
                    x = 550 + 32;
                    break;
            }

            sprite.SetXY(x, 0);
        }

        public override void Update()
        {
            int moveSpeed = 2 + (int)Math.Floor((Constants.score / 10.0));
            sprite.Move(0, moveSpeed);
        }
    }
}
