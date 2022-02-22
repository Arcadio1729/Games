using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame.Model
{
    public class Level
    {
        public string Name { get; set; }
        public int HowManyWords { get; set; }
        public int HowManyLifes { get; set; }
    }
}
