﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class HUD : GameObject
    {
        int score = 0;
        EasyDraw scoreCounter;
        EasyDraw simon;
        EasyDraw player;
        string playerText;

        public HUD()
        {
            scoreCounter = new EasyDraw(288, 32);
            //scoreCounter.Clear(Color.Black);
            scoreCounter.Text(("Score: " + score + " | High Score: " + Constants.highScore));
            AddChild(scoreCounter);

            simon = new EasyDraw(288, 32);
            player = new EasyDraw(288, 32);
            AddChild(simon);
            simon.SetXY(0, 40);
            AddChild(player);
            player.SetXY(0, 80);
        }

        public void writeSimon(string input)
        {
            simon.LateDestroy();

            simon = new EasyDraw(288, 32);
            simon.Clear(Color.Black);
            simon.Text(input);
            AddChild(simon);
            simon.SetXY(0, 40);
        }

        public void writePlayer(string input)
        {
            player.LateDestroy();

            //playerText = playerText+ input;

            player = new EasyDraw(288, 32);
            player.Clear(Color.Black);
            player.Text(input);
            AddChild(player);
            player.SetXY(0, 80);
        }

        public void updateScore()
        {
            if (Constants.highScore < Constants.score)
            {
                Constants.highScore = Constants.score;
            }

            if (score != Constants.score)
            {
                List<GameObject> children = GetChildren();
                foreach (GameObject child in children)
                {
                    child.LateDestroy();
                }

                scoreCounter = new EasyDraw(288, 32);
                score = Constants.score;
                scoreCounter.Clear(Color.Black);
                scoreCounter.Text(("Score: " + score + " | High Score: " + Constants.highScore));
                AddChild(scoreCounter);
            }
        }
    }
}