using GuessingGame.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame
{
    public class DbFileHandling : IDbFileHandling
    {
        public IEnumerable<Player> GetStats(string path)
        {
            string inJson = File.ReadAllText(path);

            IEnumerable<Player> PlayersResults = JsonConvert.DeserializeObject<IEnumerable<Player>>(inJson);

            return PlayersResults;
        }

        public IEnumerable<string> GetWords(string path)
        {
            var words = new List<string>();
            using (StreamReader file = new StreamReader(path))
            {
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    words.Add(ln);
                }
                file.Close();
            }
            return words;
        }
        public void UpdateStats(Player player, string path)
        {
            string inJson = File.ReadAllText(path);

            IEnumerable<Player> PlayersResults = JsonConvert.DeserializeObject<IEnumerable<Player>>(inJson);

            if (PlayersResults == null)
            {
                PlayersResults = new List<Player>();
            }

            PlayersResults = PlayersResults.Append(player).ToList();
            string outJson = JsonConvert.SerializeObject(PlayersResults, Formatting.Indented);
            File.WriteAllText(path, outJson);
        }
    }
}
