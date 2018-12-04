using System;
using System.Collections.Generic;

namespace youre_bluffing_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new Bank(), new Animals());
            Player[] players = game.GetPlayers();
            for (int i = 0; i < players.Length; i++)
            {
                players[i].LogMoney();
            }
        }
    }
}
