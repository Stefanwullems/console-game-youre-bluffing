using System;
using System.Collections.Generic;

namespace youre_bluffing_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            for (int i = 0; i < deck.deck.Length; i++)
            {
                Console.WriteLine(i.ToString() + " " + deck.deck[i]);
            }
        }
    }

    class Deck
    {
        public Dictionary<string, int> typeValuePairs = new Dictionary<string, int>();
        public static string[] cardTypesSortedByValue = new string[] { "Chicken", "Goose", "Cat", "Dog", "Sheep", "Goat", "Donkey", "Pig", "Cow", "Horse" };
        public static int[] cardValuesSortedAsc = new int[] { 10, 40, 90, 160, 250, 350, 500, 650, 800, 1000 };
        public string[] deck = new string[40];

        public Deck()
        {
            deck = GenerateDeck();
        }

        private static string[] GenerateDeck()
        {
            string[] deck = new string[40];
            for (int i = 0; i < cardTypesSortedByValue.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    deck[i * 4 + j] = cardTypesSortedByValue[i];
                }
            }
            return deck;
        }


    }

}
