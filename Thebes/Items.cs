using System;
using System.Collections.Generic;
using System.Text;

namespace Thebes
{
    public abstract class Item
    {
        public string Id { get; set; }

        public Item(string id)
        {
            this.Id = id;
        }

        public abstract void UpdateStats(Player player);
    }

    public abstract class Card : Item
    {
        public Place Place { get; set; }
        public int Weeks { get; set; }

        public Card(string id, Place place, int weeks) : base(id)
        {
            this.Place = place;
            this.Weeks = weeks;
        }
    }

    public class SpecializedKnowledgeCard : Card
    {
        public int KnowledgeAmount { get; set; }
        public DigSite digSite { get; set; }

        public SpecializedKnowledgeCard(string id, Place place, int weeks, int knowledgeAmount, DigSite digSite) : base(id, place, weeks)
        {
            this.KnowledgeAmount = knowledgeAmount;
            this.digSite = digSite;
        }

        public override void UpdateStats(Player player)
        {
            player.SpecializedKnowledge[digSite] += KnowledgeAmount;
        }
    }

    public class GeneralKnowledgeCard : Card
    {
        public int KnowledgeAmount { get; set; }

        public GeneralKnowledgeCard(string id, Place place, int weeks, int knowledgeAmount) : base(id, place, weeks)
        {
            this.KnowledgeAmount = knowledgeAmount;
        }

        public override void UpdateStats(Player player)
        {
            player.GeneralKnowledge += KnowledgeAmount;
        }
    }

    public class RumorsCard : Card
    {
        public int KnowledgeAmount { get; set; }
        public DigSite digSite { get; set; }

        public RumorsCard(string id, Place place, int weeks, int knowledgeAmount, DigSite digSite) : base(id, place, weeks)
        {
            this.KnowledgeAmount = knowledgeAmount;
            this.digSite = digSite;
        }

        public override void UpdateStats(Player player)
        {
            player.SingleUseKnowledge[digSite] += KnowledgeAmount;
        }
    }

    public class ZeppelinCard : Card
    {
        public ZeppelinCard(string id, Place place, int weeks) : base(id, place, weeks) { }
        public override void UpdateStats(Player player)
        {
            player.Zeppelins++;
        }
    }

    public class CarCard : Card
    {
        public CarCard(string id, Place place, int weeks) : base(id, place, weeks) { }

        public override void UpdateStats(Player player)
        {
            player.Cars++;
        }
    }

    public class AssistentCard : Card
    {
        public AssistentCard(string id, Place place, int weeks) : base(id, place, weeks) { }
        public override void UpdateStats(Player player)
        {
            player.Assistents++;
        }
    }

    public class ShovelCard : Card
    {
        public ShovelCard(string id, Place place, int weeks) : base(id, place, weeks) { }

        public override void UpdateStats(Player player)
        {
            player.Shovels++;
        }
    }

    public class SpecialPermissionCard : Card
    {
        public SpecialPermissionCard(string id, Place place, int weeks) : base(id, place, weeks) { }

        public override void UpdateStats(Player player)
        {
            player.SpecialPermissions++;
        }
    }

    public class ExhibitionCard : Card
    {
        private int Points { get; set; }
        public Dictionary<DigSite, int> ArtifactsRequired { get; set; }

        public ExhibitionCard(string id, Place place, int weeks, int points, Dictionary<DigSite, int> artifactsRequired) : base(id, place, weeks)
        {
            this.Points = points;
            this.ArtifactsRequired = artifactsRequired;
        }

        public override void UpdateStats(Player player)
        {
            player.Points += Points;
        }

        public bool IsSmallExhibition()
        {
            return Points < 5;
        }

        public bool CheckRequiredTokens(Dictionary<DigSite, List<Token>> tokensObtained)
        {

            foreach (KeyValuePair<DigSite, int> requirement in ArtifactsRequired)
            {
                if (requirement.Value > tokensObtained[requirement.Key].Count)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public abstract class Token : Item
    {
        public DigSite digSite { get; set; }

        public Token(string id, DigSite digSite) : base(id)
        {
            this.digSite = digSite;
        }
    }

    public class SpecializedKnowledgeToken : Token
    {
        public int KnowledgeAmount { get; set; }
        public DigSite knowledgeDigSite { get; set; }

        public SpecializedKnowledgeToken(string id, DigSite digSite, int knowledgeAmount, DigSite knowledgeDigSite) : base(id, digSite)
        {
            this.KnowledgeAmount = knowledgeAmount;
            this.knowledgeDigSite = knowledgeDigSite;
        }

        public override void UpdateStats(Player player)
        {
            player.SpecializedKnowledge[knowledgeDigSite] += KnowledgeAmount;
        }
    }

    public class GeneralKnowledgeToken : Token
    {
        public int KnowledgeAmount { get; set; }

        public GeneralKnowledgeToken(string id, DigSite digSite, int knowledgeAmount) : base(id, digSite)
        {
            this.KnowledgeAmount = knowledgeAmount;
        }

        public override void UpdateStats(Player player)
        {
            player.GeneralKnowledge += KnowledgeAmount;
        }
    }

    public class ArtifactToken : Token
    {
        public int Points { get; set; }
        public string Name { get; set; }

        public ArtifactToken(string id, DigSite digSite, int points, string name) : base(id, digSite)
        {
            this.Points = points;
            this.Name = name;
        }

        public override void UpdateStats(Player player)
        {
            player.Points += Points;
        }
    }

    public class DirtToken : Token
    {    
        public DirtToken(string id, DigSite digSite) : base(id, digSite) { }
        public override void UpdateStats(Player player)
        {
            // player's stats don't change
        }
    }
}