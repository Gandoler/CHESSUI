using ChessLogic.Boardik;
using ChessLogic.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic.Pieces
{
    public class Knight: Piece
    {
        public override PieceType Type => PieceType.Knight;

        public override Player Color { get; }

        public Knight(Player Color)
        {
            this.Color = Color;
        }

        public override Piece Copy()
        {
            Knight copy = new Knight(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }


        private static IEnumerable<Position> PotentialToPositions(Position from)
        {
            foreach (Direction vDir in new Direction[] { Direction.North, Direction.South })
            {
                foreach (Direction hDir in new Direction[] { Direction.West, Direction.East })
                {
                    yield return from + 2 * vDir + hDir;
                    yield return from + 2 * hDir + vDir;
                }
            }
        }

        private IEnumerable<Position> MovePositions(Position from, Board_Base board)
        {
            return PotentialToPositions(from).Where(pos => Board_Base.IsInside(pos)
                && (board.isEmpty(pos) || board[pos].Color != Color));
        }

        public override IEnumerable<Move> GetMoves(Position from, Board_Base board)
        {
            return MovePositions(from, board).Select(to => new NormalMove(from, to));
        }


    }

}
