using System;

namespace youre_bluffing_console
{
    class Player
    {
        public int[] money = new int[40];
        public string[] animals = new string[40];
        public string[] quartets = new string[10];
        public Player()
        {
            money = Bank.InitialHand();
        }
    }
}