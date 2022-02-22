using GuessingGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame
{
    public class PlayerHandling : IPlayerHandling
    {
        public void SetName(Player player, string name)
        {
            player.Name = name;
        }
        public void SetTime(Player player, TimeSpan time)
        {
            player.GuessingTime = time;
        }
        public void SetTries(Player player, int tries)
        {
            player.GuessingTries = tries;
        }
    }
}
