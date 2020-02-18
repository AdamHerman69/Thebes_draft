using System;
using System.Collections.Generic;
using System.Text;

namespace Thebes
{
    public abstract class Place
    {
        public string Name { get; private set; }

    }

    public class DigSite : Place
    {
        private List<Token> tokens { get; set; }
        private static Random random = new Random();

        public List<Token> Dig(int tokenAmount)
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

    public class CardChanePlace : Place
    {

    }

    public class University : Place
    {

    }
}
