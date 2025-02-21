using ChessLogic;
using ChessLogic.Boardik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessUI.App.Model
{
    class MakeMoveEventArgs:EventArgs
    {
        public Board_Base Board { get; }
        public Player Player { get; }

        MakeMoveEventArgs(Board_Base Board, Player Player)
        {
            this.Board = Board;
            this.Player = Player;
        }
    }
}
