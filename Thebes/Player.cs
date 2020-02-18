using System;
using System.Collections.Generic;
using System.Text;

namespace Thebes
{
    public class Player : IComparer<Player>
    {
        public string Name { get; private set; }

        public Dictionary<DigSite, bool> Permissions { get; set; }
        public Dictionary<DigSite, int> SpecializedKnowledge { get; set; }
        public Dictionary<DigSite, int> SingleUseKnowledge { get; set; }
        public int GeneralKnowledge { get; set; }
        public int Zeppelins { get; set; }
        public int SpecialPermissions { get; set; }
        public int Congresses { get; set; }
        public int Assistents { get; set; }
        public int Shovels { get; set; }
        public int Cars { get; set; }
        public int Points { get; set; }
        public List<Card> Cards { get; set; }
        public List<Token> Tokens { get; set; }
        public int CurrentWeek { get; set; }
        public Place CurrentPlace { get; set; }

        public Player(string name, List<DigSite> digSites, Place startingPlace, int startingWeek)
        {
            this.Name = name;
            this.CurrentPlace = startingPlace;
            this.CurrentWeek = startingWeek;

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

    }
}