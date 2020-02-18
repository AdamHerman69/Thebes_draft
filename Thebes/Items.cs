using System;
using System.Collections.Generic;
using System.Text;

namespace Thebes
{
    public abstract class Item
    {
        public string Id { get; set; }

        public abstract void UpdateStats(Player player);
    }

    public abstract class Card : Item
    {
        public Place Place { get; set; }
        public int Weeks { get; set; }
    }

    public class SpecializedKnowledgeCard : Card
    {
        public int KnowledgeAmount { get; set; }
        public DigSite digSite { get; set; }

        public override void UpdateStats(Player player)
        {
            player.SpecializedKnowledge[digSite] += KnowledgeAmount;
        }
    }

    public class GeneralKnowledgeCard : Card
    {
        public int KnowledgeAmount { get; set; }

        public override void UpdateStats(Player player)
        {
            player.GeneralKnowledge += KnowledgeAmount;
        }
    }

    public class RumorsCard : Card
    {
        public int KnowledgeAmount { get; set; }
        public DigSite digSite { get; set; }

        public override void UpdateStats(Player player)
        {
            player.SingleUseKnowledge[digSite] += KnowledgeAmount;
        }
    }

    public class ZeppelinCard : Card
    {
        public override void UpdateStats(Player player)
        {
            player.Zeppelins++;
        }
    }

    public class CarCard : Card
    {
        public override void UpdateStats(Player player)
        {
            player.Cars++;
        }
    }

    public class AssistentCard : Card
    {
        public override void UpdateStats(Player player)
        {
            player.Assistents++;
        }
    }

    public class ShovelCard : Card
    {
        public override void UpdateStats(Player player)
        {
            player.Shovels++;
        }
    }

    public class SpecialPermissionCard : Card
    {
        public override void UpdateStats(Player player)
        {
            player.SpecialPermissions++;
        }
    }

    public class ExhibitionCard : Card
    {
        public int Points { get; set; }
        public int MyProperty { get; set; }
        public Dictionary<DigSite, int> ArtifactsRequired { get; set; }

        public override void UpdateStats(Player player)
        {
            player.Points += Points;
        }

    }

    public abstract class Token : Item
    {
        public DigSite digSite { get; set; }
    }

    public class SpecializedKnowledgeToken : Token
    {
        public int KnowledgeAmount { get; set; }
        public DigSite knowledgeDigSite { get; set; }

        public override void UpdateStats(Player player)
        {
            player.SpecializedKnowledge[knowledgeDigSite] += KnowledgeAmount;
        }
    }

    public class GeneralKnowledgeToken : Token
    {
        public int KnowledgeAmount { get; set; }

        public override void UpdateStats(Player player)
        {
            player.GeneralKnowledge += KnowledgeAmount;
        }
    }

    public class ArtifactToken : Token
    {
        public int Points { get; set; }

        public override void UpdateStats(Player player)
        {
            player.Points += Points;
        }
    }

    public class DirtToken : Token
    {
        public override void UpdateStats(Player player)
        {
            // player's stats don't change
        }
    }
}