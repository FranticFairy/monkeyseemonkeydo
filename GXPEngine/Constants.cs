using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    static class Constants
    {
        public static HUD hud;
        //highScore will be reworked later.
        public static int highScore;

        //how many points have you scored
        public static int score;

        //how long has this session gone on
        public static int playTime;

        //Rate of speed of the game
        public static int gameSpeed;

        //How much time is there left in this minigame if Ongoing is true, or how much time until the next minigame if not
        public static int minigameTime;

        //Is a minigame active now?
        public static bool minigameOngoing;

        public static int boostTime;


        public static Dictionary<char, String> charToMorse = new Dictionary<char, String>()
            {
                {'A' , ".-"},
                {'B' , "-..."},
                {'C' , "-.-."},
                {'D' , "-.."},
                {'E' , "."},
                {'F' , "..-."},
                {'G' , "--."},
                {'H' , "...."},
                {'I' , ".."},
                {'J' , ".---"},
                {'K' , "-.-"},
                {'L' , ".-.."},
                {'M' , "--"},
                {'N' , "-."},
                {'O' , "---"},
                {'P' , ".--."},
                {'Q' , "--.-"},
                {'R' , ".-."},
                {'S' , "..."},
                {'T' , "-"},
                {'U' , "..-"},
                {'V' , "...-"},
                {'W' , ".--"},
                {'X' , "-..-"},
                {'Y' , "-.--"},
                {'Z' , "--.."},
                {'0' , "-----"},
                {'1' , ".----"},
                {'2' , "..---"},
                {'3' , "...--"},
                {'4' , "....-"},
                {'5' , "....."},
                {'6' , "-...."},
                {'7' , "--..."},
                {'8' , "---.."},
                {'9' , "----."},
            };
        public static List<string> morseValueList = new List<string>(charToMorse.Values);
        public static List<char> morseKeyList = new List<char>(charToMorse.Keys);

        public static Dictionary<String, char> morseToChar = new Dictionary<String, char>()
            {
                {".-" , 'A'},
                {"-..." , 'B'},
                {"-.-." , 'C'},
                {"-.." , 'D'},
                {"." , 'E'},
                {"..-." , 'F'},
                {"--." , 'G'},
                {"...." , 'H'},
                {".." , 'I'},
                {".---" , 'J'},
                {"-.-" , 'K'},
                {".-.." , 'L'},
                {"--" , 'M'},
                {"-." , 'N'},
                {"---" , 'O'},
                {".--." , 'P'},
                {"--.-" , 'Q'},
                {".-." , 'R'},
                {"..." , 'S'},
                {"-" , 'T'},
                {"..-" , 'U'},
                {"...-" , 'V'},
                {".--" , 'W'},
                {"-..-" , 'X'},
                {"-.--" , 'Y'},
                {"--.." , 'Z'},
                {"-----" , '0'},
                {".----" , '1'},
                {"..---" , '2'},
                {"...--" , '3'},
                {"....-" , '4'},
                {"....." , '5'},
                {"-...." , '6'},
                {"--..." , '7'},
                {"---.." , '8'},
                {"----." , '9'},
            };
    }
}
