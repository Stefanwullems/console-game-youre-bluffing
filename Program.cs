using System;
using System.Collections.Generic;

namespace youre_bluffing_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new Bank(), new Animals());
            game.StartGame();
        }
    }
}
