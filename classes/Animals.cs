using System;
using System.Collections.Generic;

namespace youre_bluffing_console
{
    class Animals
    {
        public static Dictionary<string, int> cardValues = new Dictionary<string, int>(){
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

        public static string[] cardTypes = new string[] { "Chicken", "Goose", "Cat", "Dog", "Sheep", "Goat", "Donkey", "Pig", "Cow", "Horse" };
        private string[] _deck = new string[40];

        public Animals()
        {
            _deck = GenerateDeck();
        }

        private static string[] GenerateDeck()
        {
            string[] deck = new string[40];
            for (int i = 0; i < cardTypes.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    deck[i * 4 + j] = cardTypes[i];
                }
            }
            return ShuffleDeck(deck);
        }

        private static string[] ShuffleDeck(string[] deck)
        {
            Random random = new Random();

            for (int i = deck.Length - 1; i > 1; i--)
            {
                int j = random.Next(0, i + 1);
                string key = deck[j];
                deck[j] = deck[i];
                deck[i] = key;
            }
            return deck;
        }

        public static int GetValueOfQuartets(string[] quartets)
        {
            int accumulator = 0;
            for (int i = 0; i < quartets.Length; i++)
            {
                accumulator += cardValues[quartets[i]];
            }
            return accumulator * quartets.Length;
        }

        public string DrawCard(int turn)
        {
            try
            {
                if (turn >= _deck.Length) throw new Exception("The deck is empty, move on to the trading section of the game");
                return _deck[turn];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace + "\nMessage: " + ex.Message);
                return null;
            };

        }

        public string[] GetDeck()
        {
            return _deck;
        }

        public static Boolean HasAnimalsInCommon(string[] p1, string[] p2, out Dictionary<String, int> animalsInCommon)
        {
            Dictionary<String, int> animalsP1 = new Dictionary<string, int>();
            Dictionary<String, int> animalsP2 = new Dictionary<string, int>();

            for (int i = 0; i < cardTypes.Length; i++) animalsP1.Add(cardTypes[i], 0);
            for (int i = 0; i < cardTypes.Length; i++) animalsP2.Add(cardTypes[i], 0);

            for (int i = 0; i < p1.Length; i++) animalsP1[p1[i]]++;
            for (int i = 0; i < p2.Length; i++) animalsP2[p2[i]]++;

            animalsInCommon = new Dictionary<string, int>();
            for (int i = 0; i < cardTypes.Length; i++)
            {
                string card = cardTypes[i];
                if (animalsP1[card] > 0 && animalsP2[card] > 0) animalsInCommon.Add(card, Math.Min(animalsP1[card], animalsP2[card]));
            }
            if (animalsInCommon.Count > 0) return true;
            else return false;
        }
    }
}