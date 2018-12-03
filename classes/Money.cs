using System;

namespace youre_bluffing_console
{
    class Money
    {
        private static int[] donkeyBonus = new int[] { 50, 100, 200, 500 };
        private int donkeyCount = 0;

        public static int[] InitialHand()
        {
            int[] hand = new int[] { 10, 10, 10, 20, 50 };
            return hand;
        }

        public int GetDonkeyBonus()
        {
            int bonus = donkeyBonus[donkeyCount];
            donkeyCount++;
            return bonus;
        }
    }
}