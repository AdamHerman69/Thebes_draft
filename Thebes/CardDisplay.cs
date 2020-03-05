using System;
using System.Collections.Generic;
using System.Text;

namespace Thebes
{
    public class CardDisplay
    {
        public delegate Card DrawCard();
        private Card[] AvailableCards { get; set; }
        DrawCard drawCardMethod;
        public static int timeToChangeCards = 1;

        public CardDisplay(DrawCard drawCard)
        {
            AvailableCards = new Card[4];
            drawCardMethod = drawCard;

            for (int i = 0; i < AvailableCards.Length; i++)
            {
                AvailableCards[i] = drawCardMethod();
            }
        }

        public void ChangeDisplayedCards(Deck deck)
        {
            for (int i = 0; i < AvailableCards.Length; i++)
            {
                deck.Discard(AvailableCards[i]);
            }

            for (int i = 0; i < AvailableCards.Length; i++)
            {
                AvailableCards[i] = drawCardMethod();
            }
        }

        public Card GiveCard(Card card)
        {
            int cardIndex = Array.IndexOf(this.AvailableCards, card);
            if (cardIndex < 0)
            {
                throw new InvalidOperationException("Desired card is not od display.");
            }

            this.AvailableCards[cardIndex] = drawCardMethod();

            return card;
        }
    }

    public class AvailableExhibitions
    {
        private ExhibitionCard[] Exhibitions { get; set; }
        Action<Card> DiscardCard;

        public AvailableExhibitions(Action<Card> DiscardCardMethod)
        {
            Exhibitions = new ExhibitionCard[3];
            DiscardCard = DiscardCardMethod;
        }

        public void DisplayExhibition(ExhibitionCard exhibition)
        {
            if (Exhibitions[Exhibitions.Length - 1] != null)
            {
                DiscardCard(Exhibitions[Exhibitions.Length - 1]);
            }

            for (int i = Exhibitions.Length - 1; i > 0; i--)
            {
                Exhibitions[i] = Exhibitions[i - 1];
            }
            Exhibitions[0] = exhibition;
        }

        public ExhibitionCard GiveExhibition(ExhibitionCard exhibition)
        {
            int cardIndex = Array.IndexOf(Exhibitions, exhibition);
            if (cardIndex < 0)
            {
                throw new InvalidOperationException("Exhibition is not active.");
            }

            Exhibitions[cardIndex] = null;

            return exhibition;
        }

    }
}
