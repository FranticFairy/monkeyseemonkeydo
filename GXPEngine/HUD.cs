using System;
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
        EasyDraw vine;
        string playerText;
        Sprite life1;
        Sprite life2;
        Sprite life3;
        List<EasyDraw> scoreList;

        public HUD()
        {
            scoreList = new List<EasyDraw>();

            life1 = new Sprite("Life.png");
            life2 = new Sprite("Life.png");
            life3 = new Sprite("Life.png");

            life1.visible = false;
            life2.visible = false;
            life3.visible = false;

            life1.SetXY(1205 - 64, 83);
            life2.SetXY(1275 - 64, 83);
            life3.SetXY(1345 - 64, 83);

            AddChild(life1);
            AddChild(life2);
            AddChild(life3);

            scoreCounter = new EasyDraw(512, 32);
            scoreCounter.x = scoreCounter.x + 8;
            scoreCounter.y = scoreCounter.y + 8;
            //scoreCounter.Clear(Color.Black);
            //scoreCounter.Text(("Press 6 to Start!"));
            AddChild(scoreCounter);

            /*
            simon = new EasyDraw(288, 32);
            player = new EasyDraw(288, 32);
            AddChild(simon);
            simon.SetXY(0, 40);
            AddChild(player);
            player.SetXY(0, 80);
            */
        }

        public void writeSimon(string input)
        {
            if(simon != null)
            {
                simon.LateDestroy();
            }

            simon = new EasyDraw(288, 32);
            simon.Clear(Color.Black);
            simon.Text(input);
            AddChild(simon);
            simon.SetXY(0, 40);
        }

        public void writeVine(string input)
        {
            if (vine != null)
            {
                vine.LateDestroy();
            }


            vine = new EasyDraw(400, 400);
            vine.Clear(Color.Black);
            vine.Text(input);
            AddChild(vine);
            vine.SetXY(0, 40);
        }

        public void writePlayer(string input)
        {
            if (player != null)
            {
                player.LateDestroy();
            }

            //playerText = playerText+ input;

            player = new EasyDraw(288, 32);
            player.Clear(Color.Black);
            player.Text(input);
            AddChild(player);
            player.SetXY(0, 80);
        }

        public void clearMinigame()
        {
            if (simon != null)
            {
                simon.LateDestroy();
            }
            if (player != null)
            {
                player.LateDestroy();
            }
            if (vine != null)
            {
                vine.LateDestroy();
            }
            List<GameObject> children = GetChildren();
            foreach (GameObject child in children)
            {
                child.LateDestroy();
            }
        }

        public void hideLives()
        {
            life1.visible = false;
            life2.visible = false;
            life3.visible = false;
        }

        public void updateLives()
        {
            switch(Constants.lives)
            {
                case 0:
                    life3.visible = false;
                    break;
                case 1:
                    life2.visible = false;
                    break;
                case 2:
                    life1.visible = false;
                    break;
                case 3:
                    life1.visible = true;
                    life2.visible = true;
                    life3.visible = true;
                    break;
            }
        }

        public void resetHUD()
        {

            scoreCounter.Destroy();

            scoreCounter = new EasyDraw(512, 32);
            score = Constants.score;
            scoreCounter.x = scoreCounter.x + 8;
            scoreCounter.y = scoreCounter.y + 8;
            scoreCounter.TextFont("Daydream",10);
            //scoreCounter.Clear(Color.Black);
            //scoreCounter.Fill(Color.Black);
            scoreCounter.Text((""));
            AddChild(scoreCounter);
        }

        public void updateScore()
        {
            /*
            if (Constants.highScore < Constants.score)
            {
                Constants.highScore = Constants.score;
            }
            */

            /*
            List<GameObject> children = GetChildren();
            foreach (GameObject child in children)
            {
                child.LateDestroy();
            }
            */

            scoreCounter.Destroy();

            scoreCounter = new EasyDraw(512, 32);
            score = Constants.score;
            scoreCounter.x = scoreCounter.x + 8;
            scoreCounter.y = scoreCounter.y + 8;
            scoreCounter.TextFont("Daydream", 10);
            //scoreCounter.Clear(Color.Black);
            scoreCounter.Text(("Score: " + score + " | High Score: " + Constants.scores[0].writeScore()));
            AddChild(scoreCounter);
        }

        public void registerScore()
        {
            List<GameObject> children = GetChildren();
            foreach (GameObject child in children)
            {
                child.LateDestroy();
            }

            scoreCounter.Destroy();

            scoreCounter = new EasyDraw(512, 32);
            score = Constants.score;
            scoreCounter.x = scoreCounter.x + 8;
            scoreCounter.y = scoreCounter.y + 8;
            scoreCounter.TextFont("Daydream", 10);
            //scoreCounter.Clear(Color.Black);
            scoreCounter.Text(("High Score: " + score + " | Your Name: " + Constants.playerName));
            AddChild(scoreCounter);

            scoreList.Clear();

            foreach(Score score in Constants.scores)
            {
                EasyDraw scoreDraw = new EasyDraw(512, 32);
                scoreDraw.SetXY(1300 - 256, 16 + (32 * scoreList.Count()));
                scoreDraw.TextFont("Daydream", 10);
                scoreDraw.Text(score.writeScore());
                scoreList.Add(scoreDraw);
                AddChild(scoreDraw);
            }
        }
    }
}
