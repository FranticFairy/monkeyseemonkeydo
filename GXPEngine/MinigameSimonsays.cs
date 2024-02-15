using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class MinigameSimonsays : Minigame
    {
        private string playerInput = "";
        private string simonKey = "";

        public MinigameSimonsays()
        {
            Random rand = new Random();
            simonKey = Constants.morseValueList[rand.Next(Constants.morseValueList.Count)];
            Constants.hud.writeSimon(simonKey);
            Constants.hud.writePlayer("");
        }


        public override void update()
        {
            if (Input.GetKeyUp(Key.ENTER))
            {
                playerInput = playerInput + "-";
            }
            if (Input.GetKeyUp(Key.SPACE))
            {
                playerInput = playerInput + ".";
            }

            if (simonKey.StartsWith(playerInput))
            {
                if (simonKey == playerInput)
                {
                    Constants.minigameTime = 0;
                    playerInput = "";
                    Constants.score++;
                }
                else
                {
                    Constants.hud.writePlayer(playerInput);
                }
            }
            else
            {
                Constants.score--;
                Constants.minigameTime = 0;
                playerInput = "";
            }
        }
    }
}
