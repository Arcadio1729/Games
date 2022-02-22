using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuessingGame.Controller;

namespace GuessingGame
{
    public class Simulator
    {
        public static void Play(GuessingGameController guessingController, IBoard board)
        {
            guessingController.GameStarted += board.WriteStart;
            guessingController.StateChanged += board.WriteGameInfo;
            guessingController.StateChanged += board.DrawMap;
            guessingController.WaitingForCoordinates += board.ReadCoordinates;
            guessingController.Revealed += board.DrawMap;
            guessingController.GameEnded += board.WriteEndGameMessage;
        }
    }
}
