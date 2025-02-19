using ChessLogic.Boardik;
using ChessLogic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic.Moves
{
    public class NormalMove : Move
    {
        public override MoveType Type => MoveType.Normal;

        public override Position FromPos { get; }

        public override Position ToPos { get; }

        public NormalMove(Position From, Position to)
        {
            FromPos = From;
            ToPos = to;
        }

        public override bool Execute(Board_Base board)
        {
            Piece piece = board[FromPos];
            bool capture = !board.isEmpty(ToPos);
            board[ToPos] = piece;
            board[FromPos] = null;
            piece.HasMoved = true;
            return capture || piece.Type == PieceType.Pawn;
        }
    }
}
