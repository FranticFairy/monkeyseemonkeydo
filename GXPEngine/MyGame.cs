using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;
using System.IO;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
    EasyDraw background;
    EasyDraw canvas;
    int tickTimer;
    string simonKey = "";
    string playerInput = " ";
    Minigame activeMinigame = null;
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
        tickTimer++;
        if(tickTimer == 30)
        {
            Constants.minigameTime--;
            tickTimer = 0;
        }

        if (Constants.minigameTime <= 0)
        {
            Constants.hud.updateScore();
            endMinigame();
            Constants.minigameTime = 60;
        } else
        {
            if (activeMinigame != null)
            {

                activeMinigame.update();
            } else
            {
                Random randGame = new Random();
                int randomOutput = randGame.Next(2);
                if(randomOutput == 0)
                {
                    startSimonSays();
                } else
                {
                    startWaterfall();
                }
            }
        }
        Constants.hud.updateScore();
    }

    void endMinigame()
    {
        if(activeMinigame != null)
        {
            Constants.hud.clearMinigame();
            activeMinigame.LateDestroy();
            activeMinigame = null;
            Constants.hud.updateScore();
        }
    }

    void startSimonSays()
    {
        activeMinigame = new MinigameSimonsays();
    }
    void startWaterfall()
    {
        activeMinigame = new MinigameWaterfall();
    }

    void startGame()
    {
        Constants.gameSpeed = 1;
        Constants.score = 0;
        Constants.playTime = 0;
        Constants.minigameOngoing = false;
        Constants.minigameTime = 0;
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}