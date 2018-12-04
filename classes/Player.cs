using System;

namespace youre_bluffing_console
{
    class Player
    {
        private int[] _money = new int[40];
        private int moneyCount = 0;
        private string[] _animals = new string[40];
        private int animalCount = 0;
        private string[] _quartets = new string[10];
        private int quartetCount = 0;
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

        public void AddAnimal(string card)
        {
            _animals[animalCount] = card;
            int count = 0;
            // Counts amount of animals of the same type in players hand
            for (int i = 0; i < animalCount; i++) if (_animals[i] == card) count++;
            //  If player has a quartet the animals get removed from the game and added to his quartets
            if (count == 4)
            {
                for (int i = 0; i < count; i++) RemoveAnimal(card);
                AddQuartet(card);
            }

        }

        private void AddQuartet(string card)
        {
            _quartets[quartetCount] = card;
        }

        public void RemoveAnimal(string card)
        {
            animalCount--;
            for (int i = 0; i < animalCount; i++)
            {
                if (_animals[i] == card)
                {
                    _animals[i] = _animals[animalCount];
                    _animals[animalCount] = null;
                }
            }
        }
    }
}