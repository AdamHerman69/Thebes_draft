using System;
using System.Collections.Generic;
using System.Linq;

namespace Thebes
{
    class Program
    {
        static void Main(string[] args)
        {
            GameSettings.Initialize();
            Game game = new Game(2);

            Player adam = new Player("Adam", GameSettings.Places.OfType<DigSite>().ToList(), GameSettings.StartingPlace, notEnoughTimeDialog, game.AvailableCards.ChangeDisplayedCards, game.AvailableCards.GiveCard, game.ActiveExhibitions.GiveExhibition);
            Player vitek = new Player("Adam", GameSettings.Places.OfType<DigSite>().ToList(), GameSettings.StartingPlace, notEnoughTimeDialog, game.AvailableCards.ChangeDisplayedCards, game.AvailableCards.GiveCard, game.ActiveExhibitions.GiveExhibition);

            game.Players = new List<Player>() { adam, vitek };

            game.Play();
        }

        public static void notEnoughTimeDialog()
        {
            Console.WriteLine("You don't have enough time for that action");
        }
    }
}
