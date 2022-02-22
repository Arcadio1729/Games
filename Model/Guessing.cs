using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame.Model
{
    public class Guessing
    {
        public Level Level { get; set; }
        public string CurrentCoordinates { get; set; }
        public Dictionary<BattleshipCoordinates, Word> GameMap;
        public Word[] CurrentRevealedWords { get; set; } = new Word[2];
        public List<string> WordsBank { get; set; }
        public string CurrentEntry { get; set; }
        public string DbPath { get; set; }
        public List<Word> Words { get; set; }
        public int GuessChances { get; set; }
        public bool KeepPlaying { get; set; }
        public string ResultsPath { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Attempts { get; set; }
        public string PlayerName { get; set; }
        public Player Player { get; set; }
        public int GuessingTries { get; set; }
    }
}
