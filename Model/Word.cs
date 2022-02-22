using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame.Model
{
    public class Word
    {
        public string Content { get; set; }
        public bool Revealed { get; set; }
        public bool Guessed { get; set; }
        public BattleshipCoordinates Coordinates { get; set; } = new BattleshipCoordinates();
    }
}
