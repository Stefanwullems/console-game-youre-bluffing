using System;

namespace youre_bluffing_console
{

    class Game
    {
        private Player[] _players;
        private Bank _bank;
        private Animals _animals;
        private int turn = 37;

        public Game(Bank bank, Animals animals)
        {
            int numberOfPlayers = AskForNumberOfPlayers();
            _players = AddPlayers(numberOfPlayers);
            _animals = animals;
            _bank = bank;
        }

        public Player[] GetPlayers() { return _players; }

        public void StartGame()
        {
            GameLoop();
        }
        private void GameLoop()
        {
            Player currentPlayer = _players[turn % _players.Length];

            //  If turn is smaller than 40 it means that there are still animals left to draw from
            if (turn < 40) DrawCardSection(currentPlayer);
            //  Otherwise the trading sequence of the game starts
            else TradingSection(currentPlayer);

            //  Next turn
            Console.ReadLine();
            turn++;
            GameLoop();
        }

        private void TradingSection(Player currentPlayer)
        {
            Player playerToTradeWith = SelectPlayerToTradeWith(currentPlayer);

        }

        private Player SelectPlayerToTradeWith(Player currentPlayer)
        {
            while (true)
            {
                Console.WriteLine(currentPlayer.GetName() + " can pick a player to trade with");
                LogAnimalsAndQuartets(currentPlayer);

                while (true)
                {
                    Console.WriteLine("Enter a player's id to pick");
                    string idStr = Console.ReadLine();
                    int id;
                    if (int.TryParse(idStr, out id))
                    {
                        for (int i = 0; i < _players.Length; i++)
                        {
                            if (id == _players[i].GetPlayerId() && id != currentPlayer.GetPlayerId()) return _players[i];
                            else Console.WriteLine("Please enter one of the ids");
                        }
                    }
                    else Console.WriteLine("Please enter a valid id");
                }
            }
        }

        private void LogAnimalsAndQuartets(Player currentPlayer)
        {
            for (int i = 0; i < _players.Length; i++)
            {
                if (_players[i].GetPlayerId() != currentPlayer.GetPlayerId())
                {
                    Console.WriteLine("id: " + _players[i].GetPlayerId().ToString() + ", name: " + _players[i].GetName());
                    _players[i].LogAnimals();
                    _players[i].LogQuartets();
                }
            }
        }

        private void DrawCardSection(Player currentPlayer)
        {
            //  Gets next card in sequence out of a shuffled deck 
            string card = DealCard(currentPlayer);

            //  The players that didn't pick the card bid on the card
            Player highestBidder = BiddingLoop(card, currentPlayer, out int bid);

            //  Current player can choose to buy the card or take the money
            if (TakeMoneyOrBuy(currentPlayer, bid)) BuyAnimal(currentPlayer, highestBidder, card, bid);
            else BuyAnimal(highestBidder, currentPlayer, card, bid);
        }

        private void BuyAnimal(Player buyer, Player seller, string card, int bid)
        {
            buyer.AddAnimal(card);
            buyer.RemoveMoney(bid);
            seller.AddMoney(bid);
        }

        private Boolean TakeMoneyOrBuy(Player currentPlayer, int bid)
        {
            //  If the player has more os as much money as the bid he can choose to buy the card for the value of that bid
            if (currentPlayer.GetMoney() >= bid)
            {
                while (true)
                {
                    Console.WriteLine(currentPlayer.GetName() + ": buy = b, take the money = t");
                    string decision = Console.ReadLine();
                    if (decision == "b") return true;
                    if (decision == "t") return false;
                }
            }
            else return false;
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

            //  Player that drew the card gets removed from bidders
            Player[] bidders = RemoveFromBidders(_players, currentPlayer, out biddingTurn);
            int highestBid = 0;

            //  Start bidding
            while (true)
            {
                Player currentBidder = bidders[biddingTurn];
                Console.WriteLine("it's " + currentBidder.GetName() + "'s turn to bid");

                //  Bidder makes a bid or passes
                int money = currentBidder.GetMoney();
                bid = MakeBid(money, highestBid);

                //  Bidder passes
                if (bid == 0)
                {
                    //  Bidder gets removed so he can not bid anymore
                    bidders = RemoveFromBidders(bidders, currentBidder, out biddingTurn);

                    //  Return bidder and set bid to highest bid if there is only one bidder left
                    if (bidders.Length == 1)
                    {
                        bid = highestBid;
                        return bidders[0];
                    }
                }
                // Bidder makes a bid
                else highestBid = bid;

                //  Next turn
                biddingTurn = (biddingTurn + 1) % bidders.Length;
            }
        }


        private Player[] RemoveFromBidders(Player[] players, Player currentPlayer, out int biddingTurn)
        {
            //  Make an array that is one shorter than the previous one so that arr.Length is still useful
            Player[] bidders = new Player[players.Length - 1];

            //  Variables to be set when the player that needs to be removed is found
            Boolean playerToRemoveFound = false;
            biddingTurn = 0;

            for (int i = 0; i < players.Length; i++)
            {
                //  If player is found set the bidding turn to the player next to him
                if (players[i].GetPlayerId() == currentPlayer.GetPlayerId())
                {
                    playerToRemoveFound = true;
                    biddingTurn = i % bidders.Length;
                }
                //  If not add the player back into the bidders array
                else
                {
                    //  If player to remove is found, i is one bigger than it should be
                    //  because the player is not added to the array but i still got incremented
                    if (playerToRemoveFound) bidders[i - 1] = players[i];
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
                //  Ask a player for their bid
                Console.WriteLine("Please enter a bid that's divisible by 10. p = pass");
                string j = Console.ReadLine();

                //  If bid is a number check if it is a valid bid
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
                //  Player passes
                else if (j == "p") return 0;
                else Dialog("The computer doesn't know what to do with that input. Try something else");
            }
        }

        private static int AskForNumberOfPlayers()
        {
            while (true)
            {
                Console.WriteLine("How many players will play? Min 3, Max 5");
                int j;
                string num = Console.ReadLine();
                if (int.TryParse(num, out j)) { if (j >= 3 && j <= 5) return j; }
                Dialog("Please enter a number between 3 and 5");
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

        private static void Dialog(string dialog)
        {
            Console.WriteLine(dialog);
            Console.ReadLine();
        }
    }
}