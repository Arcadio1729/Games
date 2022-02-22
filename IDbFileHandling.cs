using GuessingGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame
{
    public interface IDbFileHandling
    {
        IEnumerable<string> GetWords(string path);
        IEnumerable<Player> GetStats(string path);
        void UpdateStats(Player player, string path);   

    }
}
