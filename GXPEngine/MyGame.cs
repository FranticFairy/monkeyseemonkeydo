using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Text;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
    EasyDraw background;
    EasyDraw canvas;
    int tickTimer = 30;
    bool spawningItem = false;
    bool playing = false;
    bool scoreInput = false;
    string simonKey = "";
    string playerInput = " ";
    Minigame activeMinigame = null;
    int letterSel = 0;
    bool hasPlayed = false;
    private static List<ActorItem> objects = new List<ActorItem>();
    private static List<ActorItem> removeObjects = new List<ActorItem>();
    private int nameLetterCounter = 0;


    public MyGame() : base(1366, 768, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        Constants.hud = new HUD();
        AddChild(Constants.hud);
        Constants.hud.SetXY(0, 0);

        startGame();


    }

    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
        if(Constants.lives <= 0)
        {
            playing = false;
            scoreInput = true;
        }

        if (!playing && scoreInput)
        {
            if (Input.GetKeyDown(Key.A))
            {
                if(Constants.letterSel > 0)
                {
                    Constants.letterSel--;
                } else
                {
                    Constants.letterSel = 35;
                }
            }
            else if (Input.GetKeyDown(Key.D))
            {
                if (Constants.letterSel < 35)
                {
                    Constants.letterSel++;
                }
                else
                {
                    Constants.letterSel = 0;
                }
            }

            Constants.nameBuilder[nameLetterCounter] = Constants.letters[Constants.letterSel];
            Constants.playerName = Constants.nameBuilder.ToString();
            Constants.hud.registerScore();

            //36 Chars
            if (Input.GetKeyUp(Key.SIX) && nameLetterCounter < 2)
            {
                Constants.letterSel = 0;
                nameLetterCounter++;
            } else if (Input.GetKeyUp(Key.SIX) && nameLetterCounter == 2)
            {
                if(Constants.score > Constants.highScore)
                {
                    Constants.highScore = Constants.score;
                    Constants.previousPlayer = " - " + Constants.playerName;
                }

                Constants.letterSel = 0;
                nameLetterCounter = 0;
                Constants.nameBuilder = new StringBuilder("___");
                resetGame();
            }
        }

        if (!playing && !scoreInput)
        {
            if (Input.GetKey(Key.SIX))
            {
                buildGame();
            }
        }
        
        if(playing && !scoreInput)
        {

            Constants.player.Update();
            Constants.hunter.Update();
            foreach (ActorItem item in objects)
            {
                item.Update();

                if (item.sprite.y >= 768)
                {
                    removeObjects.Add(item);
                    break;
                }
            }

            foreach (ActorItem item in removeObjects)
            {
                item.sprite.LateDestroy();
                item.LateDestroy();
                item.sprite = null;
                item.Remove();
                RemoveChild(item);
                objects.Remove(item);
            }
            removeObjects.Clear();

            tickTimer--;
            if (tickTimer == 0)
            {
                spawningItem = false;
                Random rand = new Random();
                int randomOutput = rand.Next(60) + 1;
                tickTimer = 160 + randomOutput - (Constants.score*2);
                spawnItem();
            }

            Constants.hud.updateScore();
        }
    }

    void resetGame()
    {

        foreach (ActorItem item in objects)
        {
            item.sprite.LateDestroy();
            item.LateDestroy();
            item.sprite = null;
            item.Remove();
            RemoveChild(item);
        }


        objects.Clear();
        removeObjects.Clear();

        Constants.player.SetXY(395 + 32, 698);
        Constants.player.sprite.SetXY(395 + 32, 698);
        Constants.player.reset();
        Constants.hunter.reset();

        Constants.playerAtLeft = false;
        Constants.playerAtRight = false;

        Constants.score = 0;

        scoreInput = false;
        Constants.lives = 3;
        Constants.hud.resetHUD();
    }

    void buildGame()
    {
        if(!hasPlayed)
        {
            Constants.leftBongo = new ActorBongo(395, 698, "LBongo");
            Constants.rightBongo = new ActorBongo(1097, 698, "RBongo");
            Constants.hunter = new ActorHunter();
            Constants.player = new ActorMonkey(395 + 32, 698);

            //Items drop at (400+128), (600+128) and (800+128)

            AddChild(Constants.leftBongo.sprite);
            AddChild(Constants.rightBongo.sprite);
            AddChild(Constants.player.sprite);
            AddChild(Constants.hunter.hunterSprite);
            hasPlayed = true;
        }


        playing = true;
        Constants.lives = 3;
        Constants.hud.updateScore();
    }

    void spawnItem()
    {
        if(spawningItem == false )
        {
            spawningItem = true;
            Random rand = new Random();
            int randomOutput = rand.Next(10) + 1;
            Random rand2 = new Random();
            int randomLane = rand2.Next(3);

            if (randomOutput == 1)
            {

            }
            else if (randomOutput % 2 == 1)
            {
                //Obstacle
                ActorItem obstacle = new ActorItem(randomLane, "obstacle");
                objects.Add(obstacle);
                AddChild(obstacle.sprite);

            }
            else
            {
                //Fruit
                ActorItem fruit = new ActorItem(randomLane, "fruit");
                objects.Add(fruit);
                AddChild(fruit.sprite);
            }
        }
    }

    void startGame()
    {
        Constants.gameSpeed = 1;
        Constants.score = 0;
        Constants.playTime = 0;
        Constants.lives = 3;
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}