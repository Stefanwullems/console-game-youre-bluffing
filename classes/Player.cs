using System;

namespace youre_bluffing_console
{
    class Player
    {
        private string _name;
        private int _playerId;
        private string[] _animals = new string[0];
        private int animalCount = 0;
        private string[] _quartets = new string[0];
        private int quartetCount = 0;
        private int _money = 0;

        public Player(string name, int playerId)
        {
            _name = name;
            _playerId = playerId;
            _money = Bank.InitialHand();
        }

        public string GetName() { return _name; }
        public string[] GetAnimals() { return _animals; }
        public string[] GetQuartets() { return _quartets; }
        public int GetMoney() { return _money; }
        public int GetPlayerId() { return _playerId; }

        public void LogAnimals()
        {
            Console.Write("Animals: ");
            for (int i = 0; i < animalCount; i++) Console.Write(_animals[i] + " ");
            Console.Write("\n");
        }

        public void LogQuartets()
        {
            Console.Write("Quartets: ");
            for (int i = 0; i < quartetCount; i++) Console.Write(_quartets[i] + " ");
            Console.Write("\n\n");
        }

        public void LogMoney() { Console.WriteLine(_money); }

        public void AddAnimal(string card)
        {
            Console.WriteLine(card + " added to " + _name + "'s hand");

            string[] animals = new string[animalCount + 1];
            for (int i = 0; i < _animals.Length; i++) animals[i] = _animals[i];

            animals[animalCount] = card;
            animalCount++;
            _animals = animals;
            int count = CountCardsOfType(card);
            if (count == 4)
            {
                for (int i = 0; i < count; i++) RemoveAnimal(card);
                AddQuartet(card);
            }
        }

        private void AddQuartet(string card)
        {
            Console.WriteLine(_name + " has a quartet of " + card + "s");

            string[] quartets = new string[quartetCount + 1];
            for (int i = 0; i < _quartets.Length; i++) quartets[i] = _quartets[i];
            quartets[quartetCount] = card;
            quartetCount++;
            _quartets = quartets;
        }

        public string HandOverAnimal(string card, int amount = 1)
        {
            if (amount == 1) Console.WriteLine("Removed " + card + " from " + _name + "'s hand");
            if (amount == 2) Console.WriteLine("Removed two " + card + "s from " + _name + "'s hand");
            try
            {
                int count = CountCardsOfType(card);
                if (count < amount) throw new Exception("Player does not have enough of those cards.\nPlayer has " + count + ". Player needs " + amount);
                return card;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace + "\nMessage: " + ex.Message);
                return null;
            }
        }

        private void RemoveAnimal(string card)
        {
            string[] animals = new string[animalCount];

            Boolean animalRemoved = false;
            for (int i = 0; i < animalCount; i++)
            {
                if (_animals[i] == card && !animalRemoved) animalRemoved = true;
                else
                {
                    if (animalRemoved) animals[i - 1] = _animals[i];
                    else animals[i] = _animals[i];
                }
            }
            animalCount--;
            _animals = animals;
        }

        private int CountCardsOfType(string card)
        {
            int count = 0;
            for (int i = 0; i < animalCount; i++) if (_animals[i] == card) count++;
            return count;
        }

        public void AddMoney(int money)
        {
            _money += money;
        }

        public void RemoveMoney(int money)
        {
            _money -= money;
        }
    }
}