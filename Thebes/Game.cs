using System;
using System.Collections.Generic;
using System.Text;

namespace Thebes
{
    public class Game
    {
        private Random random;
        public List<Player> Players { get; set; }
        public Deck Deck { get; set; }
        public CardDisplay AvailableCards { get; set; }
        public AvailableExhibitions ActiveExhibitions { get; set; }
                
        public Game(List<Player> players)
        {
            this.random = new Random();
            this.Players = players;
            this.Deck = new Deck(GameSettings.Cards, players.Count);

            AvailableCards = new CardDisplay(DrawCard);
        }

        public int PlayersOnWeek(Time time)
        {
            int count = 0;
            foreach (Player player in Players)
            {
                if (player.Time.Equals(time))
                {
                    count++;
                }
            }
            return count;
        }
        
        private bool AreAllPlayersDone()
        {
            foreach (Player player in Players)
            {
                if (player.Time.RemainingWeeks() >= 0)
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// Draws cards from <see cref="Deck"/> until a non-exhibition card is found. Exhibitions drawn are added using <see cref="AddNewExhibitionCard(ExhibitionCard)"/> method.
        /// </summary>
        /// <returns>First non-exhibition card drawn from <see cref="Deck"/></returns>
        public Card DrawCard()
        {
            Card drawnCard = Deck.DrawCard();
            while (drawnCard is ExhibitionCard)
            {
                ActiveExhibitions.DisplayExhibition((ExhibitionCard)drawnCard);
            }
            return drawnCard;
        }


        public void Play()
        {
            while (!AreAllPlayersDone()) 
            {
                Players.Sort();
                Players[0].TakeAction();
            }
        }


    }
}