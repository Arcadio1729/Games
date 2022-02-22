using System;
using GuessingGame.Controller;
using GuessingGame.View;
namespace GuessingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string DbPath = @"words.txt";

            string ResultsPath = @"results.json";

            IDbFileHandling dbFileHandling = new DbFileHandling();
            IPlayerHandling playerHandling = new PlayerHandling();
            ILevelFactory levelFactory = new LevelFactory();

            GuessingGameController guessingGame = new GuessingGameController(
                playerHandling,
                dbFileHandling,
                levelFactory,
                DbPath, 
                ResultsPath);

            IBoard board = new ConsoleBoard();
            Simulator.Play(guessingGame, board);

            guessingGame.Play();
        }
    }
}
