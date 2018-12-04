using System;

namespace youre_bluffing_console
{
    class Bank
    {
        private static int[] donkeyBonus = new int[] { 50, 100, 200, 500 };
        private int donkeyCount = 0;

        public static int InitialHand()
        {
            return 80;
        }

        public int GetDonkeyBonus()
        {
            try
            {
                if (donkeyCount + 1 > donkeyBonus.Length) throw new Exception("There should only be 4 donkeys in the game.");
                int bonus = donkeyBonus[donkeyCount];
                donkeyCount++;
                return bonus;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace + " \nMessage: " + ex.Message);
                return 0;
            }
        }
    }
}