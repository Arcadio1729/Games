using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuessingGame.Controller;

namespace GuessingGame
{
    public interface IBoard
    {
        void WriteStart(GuessingGameController guessingGame, EventArgs e);
        void DrawMap(GuessingGameController guessingGame, EventArgs e);
        void ReadCoordinates(GuessingGameController guessingGame, EventArgs e);
        void WriteGameInfo(GuessingGameController guessingGame, EventArgs e);
        void WriteEndGameMessage(GuessingGameController guessingGame, EventArgs e);
    }
}
