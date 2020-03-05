using System;
using System.Collections.Generic;
using System.Text;

namespace Thebes
{
    public static class GameSettings
    {
        public static List<Place> Places { get; set; }
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

        }
    }
}