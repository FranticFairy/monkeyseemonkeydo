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
    Sprite startScreen;
    AnimationSprite bgScreen;
    Sprite gameOverScreen;
    Sprite bushL;
    Sprite bushR;
    Sprite bushBL;
    Sprite bushBR;
    Sound menuMusic;
    Sound gameMusic;
    SoundChannel menuMusicChannel;
    SoundChannel gameMusicChannel;



    public MyGame() : base(1366, 768, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        startScreen = new Sprite("Start.png");
        bgScreen = new AnimationSprite("BG.png",3,1);
        gameOverScreen = new Sprite("GameOver.png");
        gameOverScreen.visible = false;
        bushL = new Sprite("Bush.png");
        bushR = new Sprite("Bush.png");
        bushR.Mirror(true, false);
        bushL.SetXY(167-128, 470);
        bushR.SetXY(1325 - 128, 470);
        bushBL = new Sprite("George.png");
        bushBR = new Sprite("George.png");
        bushBR.Mirror(true, false);
        bushBL.SetXY(167 - 128, 470);
        bushBR.SetXY(1325 - 128, 470);

        gameMusic = new Sound("Audio/In Game Music LOOP.mp3", true, true);
        gameMusicChannel = gameMusic.Play();
        gameMusicChannel.IsPaused = true;

        menuMusic = new Sound("Audio/Menu Music LOOP.mp3", true, true);
        menuMusicChannel = menuMusic.Play();

        try
        {
            StreamReader sr = new StreamReader("Scores.txt");
            String line = sr.ReadLine();
            while(line != null)
            {
                string[] scores = line.Split('|');
                Score newScore = new Score(int.Parse(scores[0]), scores[1]);
                Constants.scores.Add(newScore);
                line = sr.ReadLine();
            }
            sr.Close();
        } catch (Exception ex)
        {

        }


        AddChild(bgScreen);
        AddChild(startScreen);
        AddChild(gameOverScreen);
        Constants.hud = new HUD();
        AddChild(Constants.hud);
        Constants.hud.SetXY(0, 0);
        AddChild(bushBL);
        AddChild(bushBR);

        startGame();


    }

    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
        bgScreen.Animate(0.05F);

        if (Constants.lives <= 0)
        {
            Constants.hud.updateLives();
            playing = false;
            scoreInput = true;

            foreach (ActorItem item in objects)
            {
                item.sprite.LateDestroy();
                item.LateDestroy();
                item.sprite = null;
                item.Remove();
                RemoveChild(item);
            }

            Constants.hunter.hunterSprite.visible = false;
            Constants.player.monkeySprite.visible = false;
            Constants.leftBongo.sprite.visible = false;
            Constants.rightBongo.sprite.visible = false;

            //gameOverScreen.visible = true;

            objects.Clear();
            removeObjects.Clear();
            gameMusicChannel.IsPaused = true;
            menuMusicChannel.IsPaused = false;
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
                /*
                if(Constants.score > Constants.scores[0].score)
                {
                    Constants.highScore = Constants.score;
                    Constants.previousPlayer = " - " + Constants.playerName;
                }
                */
                Score playerScore = new Score(Constants.score, Constants.playerName);

                Constants.scores.Add(playerScore);
                Constants.scores.Sort();
                Constants.scores.Reverse();

                Constants.scoresExportable.Clear();
                foreach(Score score in Constants.scores)
                {
                    Constants.scoresExportable.Add(score.saveScore());
                }

                File.WriteAllLines("Scores.txt", Constants.scoresExportable);

                Constants.letterSel = 0;
                nameLetterCounter = 0;
                Constants.nameBuilder = new StringBuilder("___");
                gameOverScreen.visible = true;
                hasPlayed = true;
                Constants.lives = 1;
                Constants.hud.resetHUD();
                scoreInput = false;

            }
        }

        if (!playing && !scoreInput)
        {
            if (Input.GetKey(Key.SIX))
            {
                startScreen.Destroy();
                if(hasPlayed == true)
                {
                    gameOverScreen.visible = false;
                    resetGame();
                    buildGame();
                }
                buildGame();
            }
        }
        
        if(playing && !scoreInput)
        {
            Constants.hud.updateLives();
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
                tickTimer = 160 + randomOutput - (Constants.score*5);;
                if(tickTimer < 11)
                {
                    tickTimer = 12;
                }
                spawnItem();
            }

            Constants.hud.updateScore();
        }
    }

    void resetGame()
    {

        Constants.player.SetXY(315 + 32, 598);
        Constants.player.monkeySprite.SetXY(315 + 32, 598);
        Constants.player.reset();
        Constants.hunter.reset();

        Constants.player.monkeySprite.visible = true;
        Constants.leftBongo.sprite.visible = true;
        Constants.rightBongo.sprite.visible = true;

        Constants.playerAtLeft = false;
        Constants.playerAtRight = false;

        Constants.score = 0;

        Constants.lives = 3;
    }

    void buildGame()
    {
        menuMusicChannel.IsPaused = true;
        gameMusicChannel.IsPaused = false;
        if (!hasPlayed)
        {
            Constants.leftBongo = new ActorBongo(315, 598, "LBongo");
            Constants.rightBongo = new ActorBongo(1177, 598, "RBongo");
            Constants.hunter = new ActorHunter();
            Constants.player = new ActorMonkey(315 + 32, 598);

            //Items drop at (400+128), (600+128) and (800+128)

            AddChild(Constants.leftBongo.sprite);
            AddChild(Constants.rightBongo.sprite);
            AddChild(Constants.player.monkeySprite);
            AddChild(Constants.hunter.hunterSprite);
            AddChild(bushL);
            AddChild(bushR);
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
        menuMusicChannel.IsPaused = false;
        gameOverScreen.visible = false;
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