using System;

namespace youre_bluffing_console
{

    class Game
    {
        private Player[] _players;
        private Bank _bank;
        private Animals _animals;
        private int turn = 0;

        public Game(Bank bank, Animals animals)
        {
            int numberOfPlayers = AskForNumberOfPlayers();
            _players = AddPlayers(numberOfPlayers);
            _animals = animals;
            _bank = bank;
            AddInitialHandToPlayers();
        }

        public Player[] GetPlayers() { return _players; }

        public void StartGame()
        {
            GameLoop();
        }
        private void GameLoop()
        {
            Player currentPlayer = _players[turn % _players.Length];
            string card = DealCard(currentPlayer);
            Player highestBidder = BiddingLoop(card, currentPlayer, out int bid);
            if (PlayerBuysAnimal(currentPlayer))
            {
                currentPlayer.AddAnimal(card);
                currentPlayer.RemoveMoney(bid);
                highestBidder.AddMoney(bid);
            }
            else
            {
                currentPlayer.AddMoney(bid);
                highestBidder.AddAnimal(card);
                highestBidder.RemoveMoney(bid);
            }

            Console.ReadLine();
            turn++;
            GameLoop();
        }

        private Boolean PlayerBuysAnimal(Player currentPlayer)
        {
            while (true)
            {
                Console.WriteLine(currentPlayer.GetName() + ": buy = b, take the money = t");
                string decision = Console.ReadLine();
                if (decision == "b") return true;
                if (decision == "t") return false;
            }
        }

        private string DealCard(Player currentPlayer)
        {
            Dialog("It's " + currentPlayer.GetName() + "'s turn to draw a card");
            string card = _animals.DrawCard(turn);
            Dialog(currentPlayer.GetName() + " drew a " + card + " which is valued at " + Animals.cardValues[card]);
            return card;
        }

        private Player BiddingLoop(string card, Player currentPlayer, out int bid)
        {
            int biddingTurn;
            Player[] bidders = RemoveFromBidders(_players, currentPlayer, out biddingTurn);
            int highestBid = 0;
            while (true)
            {
                Player currentBidder = bidders[biddingTurn];
                Console.WriteLine("it's " + currentBidder.GetName() + "'s turn to bid");

                int money = currentBidder.GetMoney();
                bid = MakeBid(money, highestBid);
                if (bid == 0)
                {
                    bidders = RemoveFromBidders(bidders, currentBidder, out biddingTurn);
                    if (bidders.Length == 1)
                    {
                        bid = highestBid;
                        return bidders[0];
                    }
                }
                else highestBid = bid;
                biddingTurn = (biddingTurn + 1) % bidders.Length;
            }
        }


        private Player[] RemoveFromBidders(Player[] players, Player currentPlayer, out int biddingTurn)
        {
            Player[] bidders = new Player[players.Length - 1];
            Boolean currentPlayerPassed = false;
            biddingTurn = 0;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetPlayerId() == currentPlayer.GetPlayerId())
                {
                    currentPlayerPassed = true;
                    biddingTurn = i % bidders.Length;
                }
                else
                {
                    if (currentPlayerPassed) bidders[i - 1] = players[i];
                    else bidders[i] = players[i];
                }
            }
            return bidders;
        }

        private int MakeBid(int money, int highestBid)
        {
            int bid = 0;
            while (true)
            {
                Console.WriteLine("Please enter a bid that's divisible by 10. p = pass");
                string j = Console.ReadLine();
                if (int.TryParse(j, out bid))
                {
                    if (bid <= highestBid) Dialog("You can only bid higher than the highest bid\nHighest Bid: " + highestBid.ToString());
                    else if (money >= bid)
                    {
                        if (bid % 10 == 0) return bid;
                        else Dialog("Please enter a number that's a multiple of 10");
                    }
                    else Dialog("You don't have enough money for that");
                }
                else if (j == "p") return 0;
                else Dialog("The computer doesn't know what to do with that input. Try something else");
            }
        }

        private static int AskForNumberOfPlayers()
        {
            while (true)
            {
                Console.WriteLine("How many players will play? Min 2, Max 5");
                int j;
                string num = Console.ReadLine();
                if (int.TryParse(num, out j)) { if (j >= 2 && j <= 5) return j; }
                Dialog("Please enter a number between 2 and 5");
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
            for (int i = 0; i < _players.Length; i++) _players[i].AddMoney(Bank.InitialHand());
        }

        private static void Dialog(string dialog)
        {
            Console.WriteLine(dialog);
            Console.ReadLine();
        }
    }
}