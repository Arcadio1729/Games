using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using GuessingGame.Controller;

namespace GuessingGame.View
{
    public class ConsoleBoard : IBoard
    {
        public void WriteStart(GuessingGameController guessingGame, EventArgs e)
        {
            Console.WriteLine("Hi, what's your name: ");
            var PlayerName = Console.ReadLine();
            guessingGame.SetPlayer(PlayerName);

            Console.WriteLine("Please enter 'easy' or 'hard' level");

            var LevelEntered = false;
            while (!LevelEntered)
            {
                Console.WriteLine("Enter level: ");
                var Level = Console.ReadLine();
                LevelEntered = guessingGame.SetLevel(Level);
                if(!LevelEntered)
                    Console.WriteLine("Please enter 'easy' or 'hard' level");
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Let's go!");
        }
        public void DrawMap(GuessingGameController guessingGame, EventArgs e)
        {
            for(int i = 0; i < 32; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Level - {guessingGame.Guessing.Level.Name}");
            Console.WriteLine();
            Console.WriteLine();

            var temp = guessingGame.Guessing.Words.FirstOrDefault();
            var temps = guessingGame.Guessing.Words;

            var words = guessingGame.Guessing.Words
                .OrderBy(w => w.Coordinates.Column)
                .OrderBy(w => w.Coordinates.Row)
                .ToList();


            foreach(var w in guessingGame.Guessing.Words)
            {
                if (w.Coordinates.Row == "A")
                {
                    if (w.Revealed)
                    {
                        Console.Write(w.Content);
                    }
                    else
                    {
                        Console.Write("X");
                    }
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
            foreach (var w in guessingGame.Guessing.Words)
            {
                if (w.Coordinates.Row == "B")
                {
                    if (w.Revealed)
                    {
                        Console.Write(w.Content);
                    }
                    else
                    {
                        Console.Write("X");
                    }
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < 32; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
        public void ReadCoordinates(GuessingGameController guessingGame, EventArgs e)
        {
            Regex rgx = new Regex(@"^A\d{1}$|^B\d$");
            
            var Matched = false;
            while (!Matched)
            {
                Console.WriteLine("Enter Coordinates:");
                var Coordinates = Console.ReadLine();
                Matched = rgx.IsMatch(Coordinates);
                if (Matched)
                {
                    guessingGame.Guessing.CurrentCoordinates = Coordinates;
                }
                else
                {
                    Console.WriteLine("Wrong coordinates pattern.");
                }
            }
        }
        public void WriteEndGameMessage(GuessingGameController guessingGame, EventArgs e)
        {
            Console.WriteLine("Congratulations! You have won!");
            Console.WriteLine($"Your time: {guessingGame.Guessing.Player.GuessingTime}");
            Console.WriteLine($"Your tries: {guessingGame.Guessing.Player.GuessingTries}");

            Console.WriteLine();

            Console.WriteLine("Best results: ");
            Console.WriteLine("Name | Guessing Tries | Guessing Time | Date");
            foreach(var p in guessingGame.GetTop())
            {
                Console.WriteLine($"{p.Name} | {p.GuessingTries} | {p.GuessingTime} | {p.Date}");
            }
            Console.WriteLine();
            Console.WriteLine("Do you want to play again? y/n");

            char answer = Console.ReadKey().KeyChar;
            if (answer == 'y')
            {
                guessingGame.Guessing.KeepPlaying = true;
            }
            else
            {
                guessingGame.Guessing.KeepPlaying = false;
            }

        }
        public void WriteGameInfo(GuessingGameController guessingGame, EventArgs e)
        {
            Console.WriteLine("Waiting.... - Time for remember.");
            Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine($"You have {guessingGame.Guessing.GuessChances} left.");
            Console.WriteLine();
        }

    }
}
