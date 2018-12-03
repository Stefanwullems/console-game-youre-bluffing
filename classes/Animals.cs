using System;
using System.Collections.Generic;

namespace youre_bluffing_console
{
    class Animals
    {
        public static Dictionary<string, int> cardType = new Dictionary<string, int>(){
            {"Chicken", 10},
            {"Goose", 40},
            {"Cat", 90},
            {"Dog", 160},
            {"Sheep", 250},
            {"Goat", 350},
            {"Donkey", 500},
            {"Pig", 650},
            {"Cow", 800},
            {"Horse", 1000}
        };

        private static string[] cardTypesSortedByValue = new string[] { "Chicken", "Goose", "Cat", "Dog", "Sheep", "Goat", "Donkey", "Pig", "Cow", "Horse" };
        public string[] deck = new string[40];

        public Animals()
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