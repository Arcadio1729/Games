using GuessingGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame
{
    public interface IPlayerHandling
    {
        void SetName(Player player, string name);
        void SetTime(Player player, TimeSpan time);
        void SetTries(Player player, int tries);
    }
}
