using ChessLogic.Boardik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic.Moves
{
    public abstract class Move
    {
        public abstract MoveType Type { get; } 
        public abstract Position FromPos { get; }
        public abstract Position ToPos { get; }

        public abstract bool Execute(Board_Base board);

        public virtual bool IsLegal(Board_Base board)
        {
            Player player = board[FromPos].Color;
            Board_Base boardCopy = board.Copy();
            Execute(boardCopy);
            return !boardCopy.IsInCheck(player);
        }

    }
}
