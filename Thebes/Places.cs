using System;
using System.Collections.Generic;
using System.Text;

namespace Thebes
{
    public abstract class Place
    {
        public string Name { get; private set; }
        public int Index { get; set; }

        public Place(string name, int index)
        {
            Name = name;
            Index = index;
        }
    }

    public class DigSite : Place
    {
        private List<Token> Tokens { get; set; }
        private static Random random = new Random();

        public DigSite(string name, int index, List<Token> tokens) : base(name, index)
        {
            Tokens = tokens;
        }

        public List<Token> DrawTokens(int tokenAmount)
        {
            List<Token> tokensDrawn = new List<Token>();
            for (int i = 0; i < tokenAmount; i++)
            {
                Token tokenDrawn = tokens[random.Next(0, tokens.Count)];
                tokens.Remove(tokenDrawn);
                tokensDrawn.Add(tokenDrawn);
            }
            return tokensDrawn;
        }
    }

    public class CardChangePlace : Place
    {
        public CardChangePlace(string name, int index) : base(name, index) { }
    }

    public class University : Place
    {
        public University(string name, int index) : base(name, index) { }
    }
}
