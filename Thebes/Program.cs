using System;
using System.Collections.Generic;

namespace Thebes
{
    class Program
    {
        static void Main(string[] args)
        {
            GameSettings.LoadFromFile();
            Game game = new Game(new List<Player>());
            game.Play();
        }
    }
}
