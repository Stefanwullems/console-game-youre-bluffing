using System;

namespace youre_bluffing_console
{

    class Game
    {
        private Player[] _players;
        private Bank _bank;
        private Animals _animals;

        public Game(Bank bank, Animals animals)
        {
            int numberOfPlayers = AskForNumberOfPlayers();
            _players = AddPlayers(numberOfPlayers);
            AddInitialHandToPlayers();
        }

        public Player[] GetPlayers() { return _players; }

        private static int AskForNumberOfPlayers()
        {
            while (true)
            {
                Console.WriteLine("How many players will play? Min 2, Max 5");
                int j;
                string num = Console.ReadLine();
                if (int.TryParse(num, out j)) { if (j >= 2 && j <= 5) return j; }
                Console.WriteLine("Please enter a number between 2 and 5");
            }
        }

        private static Player[] AddPlayers(int number)
        {
            Player[] players = new Player[number];
            for (int i = 0; i < number; i++)
            {
                Console.Write("Player " + (i + 1).ToString() + " - ");
                string name = Console.ReadLine();
                players[i] = new Player(name, i + 1);
            }
            return players;
        }

        private void AddInitialHandToPlayers()
        {
            for (int i = 0; i < _players.Length; i++)
            {
                int[] initialHand = Bank.InitialHand();
                for (int j = 0; j < initialHand.Length; j++) _players[i].AddMoney(initialHand[j]);
            }
        }
    }
}