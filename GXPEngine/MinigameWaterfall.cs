using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class MinigameWaterfall : Minigame
    {
        private static List<List<string>> vines = new List<List<string>>();
        private static List<WaterfallObject> objects = new List<WaterfallObject>();

        private int vineHeight = 9; //4 top, 4 bottom, middle row = 9.
        private int vineCount = 5; //2 left, 2 right, middle row = 5

        private int monkeyHeight = 4; //9 Levels. Count from 0. 4 is the middle.
        private int monkeyVine = 2; //5 Vines. Count from 0. 2 is the middle.

        private int oldMonkeyVine = 0;
        private int spawnTicker = 10;
        private int moveTicker = 30;

        public MinigameWaterfall()
        {
            vines = new List<List<string>>();
            objects = new List<WaterfallObject>();

            for(int i = 0; i < vineHeight; i++)
            {
                vines.Add(new List<string>());
            }
            foreach(List<string> vine in vines)
            {
                for(int x = 0; x < vineCount; x++)
                {
                    vine.Add("|");
                }
            }

            vines[monkeyHeight][monkeyVine] = "M";
        }

        public override void update()
        {
            spawnTicker--;
            moveTicker--;
            oldMonkeyVine = monkeyVine;

            /*
            foreach (List<string> vine in vines)
            {
                for(int y = 0; y < vineCount; y++)
                {
                    vine[y] = "|";
                }
            }
            */

            if (Input.GetKeyUp(Key.ENTER))
            {
                if(monkeyVine < vineCount-1)
                {
                    monkeyVine++;
                }
            }
            if (Input.GetKeyUp(Key.SPACE))
            {
                if (monkeyVine > 0)
                {
                    monkeyVine--;
                }
            }


            if(oldMonkeyVine != monkeyVine)
            {
                vines[monkeyHeight][oldMonkeyVine] = "|";
            }

            vines[monkeyHeight][monkeyVine] = "M";
            foreach (WaterfallObject waterfallObject in objects.ToList())
            {
                int objectvine = waterfallObject.vine;
                if (waterfallObject.height == monkeyHeight && objectvine == monkeyVine)
                {
                    switch (waterfallObject.getType())
                    {
                        case "F":
                            waterfallObject.LateDestroy();
                            objects.Remove(waterfallObject);
                            Constants.score++;
                            break;
                        default:
                            waterfallObject.LateDestroy();
                            objects.Remove(waterfallObject);
                            Constants.score--;
                            Constants.minigameTime = 0;
                            break;

                    }
                }
            }

            if (moveTicker == 0)
            {
                foreach (WaterfallObject waterfallObject in objects.ToList())
                {
                    int oldheight = waterfallObject.height;
                    int objectvine = waterfallObject.vine;
                    moveObject(waterfallObject);
                    if (waterfallObject.height < 0 || waterfallObject.height >= vineHeight)
                    {
                        waterfallObject.LateDestroy();
                        objects.Remove(waterfallObject);
                    }
                    else
                    {
                        if(waterfallObject.height == monkeyHeight && objectvine == monkeyVine)
                        {
                            switch(waterfallObject.getType())
                            {
                                case "F":
                                    waterfallObject.LateDestroy();
                                    objects.Remove(waterfallObject);
                                    Constants.score++;
                                    break;
                                default:
                                    waterfallObject.LateDestroy();
                                    objects.Remove(waterfallObject);
                                    Constants.score--;
                                    Constants.minigameTime = 0;
                                    break;

                            }
                        } else
                        {
                            vines[waterfallObject.height][objectvine] = waterfallObject.getType();
                        }
                    }
                    if (oldheight >= 0 && oldheight < vineHeight)
                    {
                        vines[oldheight][objectvine] = "|";
                    }
                }
                moveTicker = 30;
            }

            if (spawnTicker == 0)
            {
                spawnTicker = 30;
                createObject();
            }

            string output = "";

            foreach (List<string> vine in vines)
            {
                foreach (string x in vine)
                {
                    output = output + x;
                }
                output = output + Environment.NewLine;
            }
            Constants.hud.writeVine(output);
        }

        public void moveObject(WaterfallObject item)
        {
            if(item.fromBelow == true)
            {
                item.height--;
            } else
            {
                item.height++;
            }
        }

        public void createObject()
        {
            Random rand = new Random();
            int randomOutput = rand.Next(10)+1;

            if(randomOutput == 1)
            {
                //Dart
                Random rand2 = new Random();
                WaterfallObject dart = new WaterfallObject(true, true, rand2.Next(5), 8);
                objects.Add(dart);
                vines[dart.height][dart.vine] = "D";
            } else if (randomOutput % 2 == 1)
            {
                //Obstacle
                Random rand2 = new Random();
                WaterfallObject obstacle = new WaterfallObject(true, false, rand2.Next(5), 0);
                objects.Add(obstacle);
                vines[obstacle.height][obstacle.vine] = "O";
            }
            else
            {
                //Fruit
                Random rand2 = new Random();
                WaterfallObject fruit = new WaterfallObject(false, false, rand2.Next(5), 0);
                objects.Add(fruit);
                vines[fruit.height][fruit.vine] = "F";
            }

        }
    }
}
