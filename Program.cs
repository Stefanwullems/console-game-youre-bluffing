using System;
using System.Collections.Generic;

namespace youre_bluffing_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Animals animals = new Animals();
            Bank bank = new Bank();
            for (int i = 0; i < animals.deck.Length; i++)
            {
                Console.WriteLine(i.ToString() + " " + animals.deck[i] + " is valued at " + Animals.cardValues[animals.deck[i]]);
            }
            Console.WriteLine(bank.GetDonkeyBonus().ToString());
            Console.WriteLine(bank.GetDonkeyBonus().ToString());
            Console.WriteLine(bank.GetDonkeyBonus().ToString());
            Console.WriteLine(bank.GetDonkeyBonus().ToString());
            Player frank = new Player();

            for (int i = 0; i < frank.money.Length; i++)
            {
                Console.WriteLine(frank.money[i].ToString());
            }
        }
    }
}
