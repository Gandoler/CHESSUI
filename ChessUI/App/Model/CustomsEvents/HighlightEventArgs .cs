using ChessLogic.Moves;
using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessUI.Model
{

    public class HighlightEventArgs : EventArgs
    {
        public Dictionary<Position, Move> Moves { get; }

        public HighlightEventArgs(Dictionary<Position, Move> moves)
        {
            Moves = moves;
        }
    }

}
