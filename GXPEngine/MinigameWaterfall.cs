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

        private int vineHeight = 9; //4 top, 4 bottom, middle row = 9.
        private int vineCount = 5; //2 left, 2 right, middle row = 5

        private int monkeyHeight = 4; //9 Levels. Count from 0. 4 is the middle.
        private int monkeyVine = 2; //5 Vines. Count from 0. 2 is the middle.

        private int oldMonkeyVine = 0;

        public MinigameWaterfall()
        {
            vines = new List<List<string>>();

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
            oldMonkeyVine = monkeyVine;

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
                vines[monkeyHeight][monkeyVine] = "M";
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
            Console.WriteLine(output);
            Console.WriteLine("-----");
        }
    }
}
