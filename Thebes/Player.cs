using System;
using System.Collections.Generic;
using System.Text;

namespace Thebes
{
    public class Player : IComparable<Player>
    {
        Action notEnoughTimeDialog;
        public string Name { get; private set; }
        public Game Game { get; set; }
        public Time Time { get; set; }
        public Dictionary<DigSite, bool> Permissions { get; set; }
        public Dictionary<DigSite, int> SpecializedKnowledge { get; set; }
        public Dictionary<DigSite, int> SingleUseKnowledge { get; set; }
        public int GeneralKnowledge { get; set; }
        public int Zeppelins { get; set; }

        private bool useZappelin;
        public int SpecialPermissions { get; set; }
        public int Congresses { get; set; }
        public int Assistents { get; set; }
        public int Shovels { get; set; }
        public int Cars { get; set; }
        public int Points { get; set; }
        public List<Card> Cards { get; set; }
        public Dictionary<DigSite, List<Token>> Tokens { get; set; }
        
        public Place CurrentPlace { get; set; }

        public Player(string name, List<DigSite> digSites, Place startingPlace, Action notEnoughTimeDialog)
        {
            this.Name = name;
            this.CurrentPlace = startingPlace;
            this.notEnoughTimeDialog = notEnoughTimeDialog;

            // add all valid permissions
            foreach (DigSite digSite in digSites)
            {
                Permissions.Add(digSite, true);
            }

            // add specialized knowledge values (each player starts with 0)
            foreach (DigSite digSite in digSites)
            {
                SpecializedKnowledge.Add(digSite, 0);
            }

            // add single use knowledge values (each player starts with 0)
            foreach (DigSite digSite in digSites)
            {
                SingleUseKnowledge.Add(digSite, 0);
            }

            // create token bags for each dig site
            foreach (DigSite digSite in digSites)
            {
                Tokens.Add(digSite, new List<Token>());
            }

            // all other fields are 0
        }

        int IComparable<Player>.CompareTo(Player other)
        {
            return Time.CompareTo(other.Time);
        }

        private int GetAssistentKnowledge()
        {
            if (Assistents >= 3)
            {
                return 2;
            }
            else if (Assistents >= 2)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        private int CountKnowledge(DigSite digSite, List<Card> singleUseCards)
        {
            // TODO single use cards
            int knowledge = 0;

            knowledge += SpecializedKnowledge[digSite];
            knowledge += GeneralKnowledge;
            knowledge += GetAssistentKnowledge();

            return knowledge;
        }
        
        
        /// <summary>
        /// Moves a player to the desired place by spending weeks.
        /// </summary>
        /// <param name="destination"></param>
        public void MoveTo(Place destination)
        {
            if (useZappelin)
            {
                if (this.Zeppelins > 0)
                {
                    this.Zeppelins--;
                }
                else
                {
                    throw new InvalidOperationException("Player doesn't have a zeppelin");
                }
            }
            else
            {
                Time.SpendWeeks(GameSettings.GetDistance(CurrentPlace, destination));
            }

            CurrentPlace = destination;
        }

        private void TakeCard(Card card)
        {
            Time.SpendWeeks(card.Weeks);
            if (card is ExhibitionCard)
            {
                Cards.Add(Game.ActiveExhibitions.GiveExhibition((ExhibitionCard)card));
            }
            else
            {
                Cards.Add(Game.AvailableCards.GiveCard(card));
            }
            
            card.UpdateStats(this);
        }

        /// <summary>
        /// Moves a player to the dig site and proceeds to dig according to the amount of knowledge and weeks spend.
        /// </summary>
        /// <param name="digSite">Where to dig</param>
        /// <param name="weeks">How long to dig</param>
        /// <param name="singleUseCards">Single use cards to use. NOT WORKING ATM</param>
        private void Dig(DigSite digSite, int weeks, List<Card> singleUseCards)
        {
            // TODO different dialog for invalid permission
            if (Time.RemainingWeeks() < weeks + GameSettings.GetDistance(CurrentPlace, digSite) || !Permissions[digSite])
            {
                notEnoughTimeDialog();
                return;
            }

            MoveTo(digSite);

            Time.SpendWeeks(weeks);
            List<Token> dugTokens = digSite.DrawTokens(GameSettings.DugTokenCount(CountKnowledge(digSite, singleUseCards), weeks));
            foreach (Token token in dugTokens)
            {
                token.UpdateStats(this);
                Tokens[digSite].Add(token);
            }
        }

        /// <summary>
        /// Moves a player to the destination of the desired card and takes it. Spending weeks.
        /// </summary>
        /// <param name="card">Card to take</param>
        public void MoveAndTakeCard(Card card)
        {
            if (card is ExhibitionCard && !((ExhibitionCard)card).CheckRequiredTokens(Tokens))
            {
                notEnoughTimeDialog(); // TODO can't execute exhibition dialog
                return;
            }
            if (Time.RemainingWeeks() < card.Weeks + GameSettings.GetDistance(CurrentPlace, card.Place))
            {
                notEnoughTimeDialog();
                return;
            }
            
            MoveTo(card.Place);
            TakeCard(card);
        }

        /// <summary>
        /// Ends the year for a player by waiting for another. Used when he doesn't want to do anything.
        /// </summary>
        private void EndYear()
        {
            Time.EndYear();
        }

        /// <summary>
        /// Moves player to the desired card changing place and changes the cards
        /// </summary>
        /// <param name="cardChangePlace">Place where to change cards</param>
        private void MoveAndChangeDisplayCards(CardChangePlace cardChangePlace)
        {
            if (Time.RemainingWeeks() < GameSettings.GetDistance(CurrentPlace, cardChangePlace) + CardDisplay.timeToChangeCards)
            {
                notEnoughTimeDialog();
                return;
            }

            MoveTo(cardChangePlace);
            Time.SpendWeeks(CardDisplay.timeToChangeCards);
            Game.AvailableCards.ChangeDisplayedCards(Game.Deck);
        }

        public void TakeAction()
        {

        }
    }
}