using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;
using System.IO;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
    EasyDraw background;
    EasyDraw canvas;
    public MyGame() : base(1366, 768, false)     // Create a window that's 800x600 and NOT fullscreen
    {

    }

    // For every game object, Update is called every frame, by the engine:
    void Update()
    {

    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}