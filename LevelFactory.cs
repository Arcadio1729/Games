using GuessingGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame
{
    public class LevelFactory : ILevelFactory
    {
        public Level Get(string name)
        {
            if (name.Equals("hard"))
            {
                return new Level
                {
                    Name = "hard",
                    HowManyLifes = 15,
                    HowManyWords = 8
                };
            }
            if (name.Equals("easy"))
            {
                return new Level
                {
                    Name = "easy",
                    HowManyLifes = 10,
                    HowManyWords = 4
                };
            }

            return null;
        }
    }
}
