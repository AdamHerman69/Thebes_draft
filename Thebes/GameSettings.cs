using System;
using System.Collections.Generic;
using System.Text;

namespace Thebes
{
    public static class GameSettings
    {
        public static List<Place> Places { get; set; }
        public static Place StartingPlace { get; set; }
        private static int[][] Distances { get; set; }
        private static Dictionary<int, Dictionary<int, int>> TimeWheel { get; set; }
        public static List<Card> Cards { get; set; }

        public static int GetDistance(Place from, Place to)
        {
            return Distances[from.Index][to.Index];
        }

        public static int DugTokenCount(int knowledge, int weeks)
        {
            return TimeWheel[knowledge][weeks];
        }
        public static void LoadFromFile()
        {
            throw new NotImplementedException();
        }

        public static void Initialize()
        {
            List<Token> CreateTokenList(int one, int two, int three, int four, int five, int six, int seven)
            {

            }
            List<Token> TokenList = new List<Token>();
            for (int i = 0; i < 16; i++)
            {
                TokenList.Add(new DirtToken());
            }

            for (int i = 0; i < 1; i++)
            {
                TokenList.Add(new ArtifactToken())
            }
            
            Places = new List<Place>();
            Places.Add(new University("London", 0));
            Places.Add(new University("Paris", 1));
            Places.Add(new University("Berlin", 2));
            Places.Add(new University("Wien", 3));
            Places.Add(new University("Moscow", 4));
            Places.Add(new University("Rome", 5));

            Places.Add(new CardChangePlace("Warsaw", 6));

            Places.Add(new DigSite("Greece", 7,  ));
        }
    }
}