using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuessingGame.Model;
using Newtonsoft.Json;

namespace GuessingGame.Controller
{
    public class GuessingGameController
    {
        public delegate void StateChangedEventHandler(GuessingGameController guessingGame, EventArgs e);

        public event StateChangedEventHandler GameStarted;
        public event StateChangedEventHandler StateChanged;

        public event StateChangedEventHandler GameEnded;

        public event StateChangedEventHandler WaitingForCoordinates;

        public event StateChangedEventHandler Revealed;

        private Guessing _guessing;

        private readonly IPlayerHandling _playerHandling;
        private readonly IDbFileHandling _dbFileHandling;
        private readonly ILevelFactory _levelFactory;

        private string _dbPath;
        private string _resultsPath;

        public GuessingGameController(
            IPlayerHandling playerHandling, 
            IDbFileHandling dbFileHandling, 
            ILevelFactory levelFactory,
            string dbPath, 
            string resultsPath)
        {
            this._playerHandling = playerHandling;
            this._dbFileHandling = dbFileHandling;
            this._levelFactory = levelFactory;

            this._dbPath = dbPath;
            this._resultsPath = resultsPath;

            this._guessing = new Guessing() 
                { 
                    Player = new Player() { Date = DateTime.Now },
                    KeepPlaying = true,
                    WordsBank = (List<string>)this._dbFileHandling.GetWords(this._dbPath)
                };
        }

        public void Init()
        {

            this._guessing.DbPath = this._dbPath;
            this._guessing.ResultsPath = this._resultsPath;
            this._guessing.KeepPlaying = true;
            this._guessing.GuessChances = this._guessing.Level.HowManyLifes;

            this.Guessing.StartDate = DateTime.Now;

            this.CreateMap(2, this.Guessing.Level.HowManyWords);
        }

        public void Play()
        {
            while (this.Guessing.KeepPlaying)
            {
                this.OnGameStarted();
                this.Init();
                this.DrawWords(this._guessing.Level.HowManyWords);
                while (this.Guessing.KeepPlaying)
                {
                    this.OnStateChanged();
                    
                    this.OnWaitingForCoordinates();
                    this.Reveal(this.Guessing.CurrentCoordinates);
                    this.OnRevealed();

                    this.OnWaitingForCoordinates();
                    this.Reveal(this.Guessing.CurrentCoordinates);
                    this.OnRevealed();

                    this.CheckGuessed();
                    this.Guessing.KeepPlaying = this.EndGame();
                }

                this._playerHandling.SetTime(this.Guessing.Player,DateTime.Now - this.Guessing.StartDate);
                this._playerHandling.SetTries(this.Guessing.Player,this.Guessing.GuessingTries);
                this._dbFileHandling.UpdateStats(this.Guessing.Player, this._resultsPath);
                this.OnGameEnded();
                this.Guessing.Player = new Player() { Date = DateTime.Now };
            }
        }
        public Guessing Guessing
        {
            get
            {
                return this._guessing;
            }
            set
            {
                this._guessing = value;
            }
        }
        private void DrawWords(int amount)
        {
            this._guessing.Words = new List<Word>();
            Random rnd = new Random();
            var randomIndexes = Enumerable.Range(0, rnd.Next(this._guessing.WordsBank.Count()))
                                .OrderBy(x => System.Guid.NewGuid())
                                .ToList()
                                .Take(amount);

            var indexes = Enumerable.Range(0, 2*amount).ToList();
         

            foreach (var ri in randomIndexes)
            {
                this._guessing.Words.Add(new Word() 
                { 
                    Content=this._guessing.WordsBank[ri],
                    Guessed=false,
                    Coordinates=null,
                    Revealed=false
                });
                this._guessing.Words.Add(new Word()
                {
                    Content = this._guessing.WordsBank[ri],
                    Guessed = false,
                    Coordinates = null,
                    Revealed = false
                });
            }

            this._guessing.Words = this._guessing.Words.OrderBy(a => Guid.NewGuid()).ToList();
            int counter = 0;
            for (int i = 0; i < 2; i++)
            {
                for(int j = 0; j < amount; j++)
                {
                    int column = j + 1;
                    string row = Convert.ToChar(i+65).ToString();

                    this._guessing.Words[counter].Coordinates = new BattleshipCoordinates 
                    { 
                        Column = column, 
                        Row = row 
                    };
                    counter++;
                }
            }
        }
        private void CreateMap(int rowsAmount,int columnsAmount)
        {
            this._guessing.GameMap = new Dictionary<BattleshipCoordinates, Word>();

            for(int i = 0; i < rowsAmount; i++)
            {
                for(int j = 0; j < columnsAmount; j++)
                {
                    string rowLetter = ((char)(i + 65)).ToString();
                    BattleshipCoordinates bc = new BattleshipCoordinates()
                    {
                        Row = rowLetter,
                        Column = j + 1
                    };

                    this._guessing.GameMap.Add(bc, new Word
                    {
                        Content = "",
                        Coordinates = bc,
                        Guessed = false,
                        Revealed = false
                    });
                }
            }
        }
        public void Reveal(string coordinates)
        {
            BattleshipCoordinates bc = new BattleshipCoordinates()
            {
                Row = coordinates.Substring(0, 1),
                Column = Convert.ToInt32(coordinates.Substring(1, 1))
            };

            var temps = this.Guessing.Words;

            var word = this.Guessing.Words
                .Where
                (w =>
                    w.Coordinates.Row == bc.Row &&
                    w.Coordinates.Column == bc.Column
                )
                .FirstOrDefault();

            word.Revealed = true;

            if (this._guessing.CurrentRevealedWords[0] == null)
            {
                this._guessing.CurrentRevealedWords[0] = word;
            }
            else
            {
                this._guessing.CurrentRevealedWords[1] = word;
            }
        }
        public void CheckGuessed()
        {
            this.Guessing.GuessingTries++;
            if (this._guessing.CurrentRevealedWords[0].Content == this._guessing.CurrentRevealedWords[1].Content)
            {
                this._guessing.CurrentRevealedWords[0].Guessed = true;
                this._guessing.CurrentRevealedWords[1].Guessed = true;
            }
            else
            {
                this._guessing.CurrentRevealedWords[0].Revealed = false;
                this._guessing.CurrentRevealedWords[1].Revealed = false;
                this.Guessing.GuessChances--;
            }
            this._guessing.CurrentRevealedWords = new Word[2];
        }
        public bool EndGame()
        {
            var points = this.Guessing.GuessChances;

            return !CheckWon() || (points <= 0);
        }
        private bool CheckWon()
        {
            var results = this.Guessing.Words
                .FirstOrDefault(w => w.Guessed==false);

            return results == null;
        }

        public void SetPlayer(string name)
        {
            this._playerHandling.SetName(this.Guessing.Player, name);
        }
        public bool SetLevel(string name)
        {
            if(name=="hard" || name == "easy")
            {
                this._guessing.Level = this._levelFactory.Get(name);
                return true;
            }
            return false;
        }
        public IEnumerable<Player> GetTop()
        {
            return this._dbFileHandling.GetStats(this._resultsPath)
                .OrderBy(p => p.GuessingTries)
                .Take(10);
        }
        protected virtual void OnGameStarted()
        {
            if (this.GameStarted != null)
                this.GameStarted(this, EventArgs.Empty);
        }
        protected virtual void OnStateChanged()
        {
            if (this.StateChanged != null)
                this.StateChanged(this, EventArgs.Empty);
        }

        protected virtual void OnRevealed()
        {
            if (this.Revealed != null)
                this.Revealed(this, EventArgs.Empty);
        }

        protected virtual void OnWaitingForCoordinates()
        {
            if (this.WaitingForCoordinates != null)
                this.WaitingForCoordinates(this, EventArgs.Empty);
        }

        protected virtual void OnGameEnded()
        {
            if (this.GameEnded != null)
                this.GameEnded(this, EventArgs.Empty);
        }


    }
}
