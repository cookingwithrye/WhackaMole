using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public static class ScoreTracker
    {
        private static Dictionary<string, int> _scores = new Dictionary<string, int>();

        public static Dictionary<string, int> Score { get { return _scores; } }

        /// <summary>
        /// Record a kill for the specified player
        /// </summary>
        /// <param name="name"></param>
        public static void AddKillFor(string name) 
        {
            if (!_scores.ContainsKey(name))
            {
                _scores.Add(name, 1); //this is the first kill for this player
            }
            else
            {
                _scores[name]++;
            }
        }
    }
    
    public class Mole
    {
        /// <summary>
        /// Rendered size in pixels
        /// </summary>
        public int Size { get; set; }

        //the location of the mole
        public Position Location;

        private static Random R = new Random();

        /// <summary>
        /// Mole generator with random size, position, and velocity
        /// </summary>
        /// <returns></returns>
        public static Mole GetRandomMole()
        {
            return new Mole()
            {
                Location = new Position(R.Next(0,500), R.Next(0, 400)),
                Size = R.Next(40, 120)
            };
        }
    }
}