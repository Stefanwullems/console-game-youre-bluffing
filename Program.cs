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
            for (int i = 0; i < animals.GetDeck().Length; i++)
            {
                Console.WriteLine(i.ToString() + " " + animals.GetDeck()[i] + " is valued at " + Animals.cardValues[animals.GetDeck()[i]]);
            }
            Console.WriteLine(bank.GetDonkeyBonus().ToString());
            Console.WriteLine(bank.GetDonkeyBonus().ToString());
            Console.WriteLine(bank.GetDonkeyBonus().ToString());
            Console.WriteLine(bank.GetDonkeyBonus().ToString());
            Player frank = new Player();

            for (int i = 0; i < frank.GetMoney().Length; i++)
            {
                Console.WriteLine(frank.GetMoney()[i].ToString());
            }
        }
    }
}
