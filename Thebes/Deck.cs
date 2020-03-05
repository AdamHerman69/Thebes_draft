using System;
using System.Collections.Generic;
using System.Text;

namespace Thebes
{
    public class Deck
    {
        private Queue<Card> CardDeck { get; set; }
        public List<Card> DiscardPile { get; set; }
        Random random;

        public Deck(List<Card> cards, int playerCount)
        {
            DiscardPile = new List<Card>();
            random = new Random();
            CardDeck = BuildDeck(cards, playerCount);
        }
        
        /// <summary>
        /// Builds a properly shuffled deck from given card with regards to player count and exhibition positioning.
        /// </summary>
        /// <param name="cards">List of cards to build the deck from</param>
        /// <param name="playerCount">Number of players participating in the game</param>
        /// <returns>Queue representation of the final deck</returns>
        private Queue<Card> BuildDeck(List<Card> cards, int playerCount)
        {
            if (playerCount < 2 || playerCount > 4)
            {
                throw new InvalidOperationException("Invalid number of players. This deck implemetation only works for 2 to 4 players");
            }

            List<Card> newDeck = new List<Card>();
            List<ExhibitionCard> smallExhibitions = new List<ExhibitionCard>();
            List<ExhibitionCard> largeExhibitions = new List<ExhibitionCard>();

            while (cards.Count != 0)
            {
                int randomIndex = random.Next(cards.Count);
                Card randomCard = cards[randomIndex];

                if (randomCard is ExhibitionCard)
                {
                    if (((ExhibitionCard)randomCard).IsSmallExhibition())
                    {
                        smallExhibitions.Add((ExhibitionCard)randomCard);
                    }
                    else
                    {
                        largeExhibitions.Add((ExhibitionCard)randomCard);
                    }
                }
                else
                {
                    newDeck.Add(randomCard);
                }

                cards.RemoveAt(randomIndex);
            }

            // Positioning exhibition cards in the deck according to the rules.
            // Small exhibitions go between 1/3 and 2/3 of the deck.
            // Large exhibitions between 2/3 and the end. In case of two players, same as small exhibitions

            int smallExhibitionLowBoundPosition = newDeck.Count / 3 + 4;
            int smallExhibitionUpperBoundPostition = smallExhibitionLowBoundPosition * 2;
            int largeExhibitionLowBoundPosition, largeExhibitionUpperBoundPosition;
            
            if (playerCount == 2)
            {
                largeExhibitionLowBoundPosition = smallExhibitionLowBoundPosition;
                largeExhibitionUpperBoundPosition = smallExhibitionUpperBoundPostition;
            }
            else
            {
                largeExhibitionLowBoundPosition = smallExhibitionUpperBoundPostition + 1;
                largeExhibitionUpperBoundPosition = newDeck.Count;
            }

            foreach (ExhibitionCard smallExhibition in smallExhibitions)
            {
                newDeck.Insert(random.Next(smallExhibitionLowBoundPosition, smallExhibitionUpperBoundPostition), smallExhibition);
            }

            foreach (ExhibitionCard largeExhibition in largeExhibitions)
            {
                newDeck.Insert(random.Next(largeExhibitionLowBoundPosition, largeExhibitionUpperBoundPosition), largeExhibition);
            }

            return new Queue<Card>(newDeck);
        }

        /// <summary>
        /// Draws a card from <see cref="CardDeck"/>. If empty, recycles <see cref="DiscardPile"/> using  <see cref="recycleDeck"/>
        /// </summary>
        /// <returns>Top card from <see cref="CardDeck"/></returns>
        public Card DrawCard()
        {
            if (CardDeck.Count == 0)
            {
                recycleDeck();
            }
            return CardDeck.Dequeue();
        }

        /// <summary>
        /// Shuffles discarted cards and builds a new deck from them.
        /// </summary>
        public void recycleDeck()
        {
            if (CardDeck.Count != 0)
            {
                throw new InvalidOperationException("Deck is not empty yet. No reason to recycle.");
            }
            if (DiscardPile.Count == 0)
            {
                throw new InvalidOperationException("Discard pile is empty while deck recycling.");
            }

            Shuffle(DiscardPile);
            CardDeck = new Queue<Card>(DiscardPile);
            DiscardPile.Clear();
        }

        /// <summary>
        /// Shuffles a given card deck using Fisher-Yates shuffle algorithm.
        /// </summary>
        /// <param name="deck">Deck to shuffle</param>
        private void Shuffle(List<Card> deck)
        {
            int index = deck.Count;
            while (index > 1)
            {
                index--;
                int swapPosition = random.Next(index + 1);
                Card card = deck[swapPosition];
                deck[swapPosition] = deck[index];
                deck[index] = card;
            }
        }

        /// <summary>
        /// Adds a card to the discard pile.
        /// </summary>
        /// <param name="card">Card to discard</param>
        public void Discard(Card card)
        {
            DiscardPile.Add(card);
        }
    }
}
