using System;
using System.Collections.Generic;

namespace youre_bluffing_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Animals animals = new Animals();
            for (int i = 0; i < animals.deck.Length; i++)
            {
                Console.WriteLine(i.ToString() + " " + animals.deck[i] + " is valued at " + Animals.cardType[animals.deck[i]]);
            }
        }
    }
}
