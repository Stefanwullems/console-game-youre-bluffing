using System;

namespace youre_bluffing_console
{
    class Player
    {
        private int[] _money = new int[40];
        private string[] _animals = new string[40];
        public string[] _quartets = new string[10];
        public Player()
        {
            _money = Bank.InitialHand();
        }

        public string[] GetAnimals()
        {
            return _animals;
        }

        public string[] GetQuartets()
        {
            return _quartets;
        }

        public int[] GetMoney()
        {
            return _money;
        }
    }
}