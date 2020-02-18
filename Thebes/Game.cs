using System;
using System.Collections.Generic;
using System.Text;

namespace Thebes
{
    public class Game
    {
        public List<Player> Players { get; set; }
        public List<Place> Places { get; set; }
        public List<Card> Deck { get; set; }
        public Card[] AvailableCards { get; set; }
        public List<Card> DiscardPile { get; set; }
        public ExhibitionCard[] ActiveExhibitions { get; set; }

        public void Play()
        {
            while ( všichni hráči ještě neskončili) {

            }
        }


    }
}