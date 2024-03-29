﻿using System;
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
        public List<Token> Tokens { get; set; }
        private static Random random = new Random();

        public DigSite(string name, int index) : base(name, index)
        {
           // init tokens
        }

        public List<Token> DrawTokens(int tokenAmount)
        {
            List<Token> tokensDrawn = new List<Token>();
            for (int i = 0; i < tokenAmount; i++)
            {
                Token tokenDrawn = Tokens[random.Next(0, Tokens.Count)];
                Tokens.Remove(tokenDrawn);
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
