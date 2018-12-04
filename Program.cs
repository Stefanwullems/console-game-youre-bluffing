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
            Player frank = new Player("frank");
            frank.AddAnimal("Horse");
            frank.AddAnimal("Horse");
            frank.AddAnimal("Horse");
            frank.AddAnimal("Horse");
            frank.AddAnimal(animals.DrawCard(0));
            frank.LogQuartets();
        }
    }
}
