using ChessLogic.Moves;
using ChessLogic.Boardik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic.Pieces
{
    internal class King: Piece
    {
        public override PieceType Type => PieceType.King;

        public override Player Color { get; }

        private static readonly Direction[] dirs = new Direction[]
       {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
            Direction.NorthEast,
            Direction.SouthEast,         
            Direction.NorthWest,
            Direction.SouthWest
      };

        public King(Player Color)
        {
            this.Color = Color;
        }

        private static bool IsUnmovedRook(Position pos, Board_Base board)
        {
            if (board.isEmpty(pos))
            {
                return false;
            }

            Piece piece = board[pos];
            return piece.Type == PieceType.Rook && !piece.HasMoved;
        }

        private static bool AllEmpty(IEnumerable<Position> positions, Board_Base board)
        {
            return positions.All(pos => board.isEmpty(pos));
        }

        private bool CanCastleKingSide(Position from, Board_Base board)
        {
            if (HasMoved)
            {
                return false;
            }
            Position rookPos = new Position(from.Row, 7);
            Position[] betweenPositions = new Position[] { new Position(from.Row, 5), new Position(from.Row, 6) };

            return IsUnmovedRook(rookPos, board) && AllEmpty(betweenPositions, board);
        }


        private bool CanCastleQueenSide(Position from, Board_Base board)
        {
            if (HasMoved)
            {
                return false;
            }

            Position rookPos = new Position(from.Row, 0);
            Position[] betweenPositions = new Position[] { new Position(from.Row, 1), new Position(from.Row, 2), new Position(from.Row, 3) };

            return IsUnmovedRook(rookPos, board) && AllEmpty(betweenPositions, board);
        }

        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private IEnumerable<Position> MovePositions(Position from, Board_Base board)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;

                if (!Board_Base.IsInside(to))
                {
                    continue;
                }

                if (board.isEmpty(to) || board[to].Color != Color)
                {
                    yield return to;
                }
            }
        }


        public override IEnumerable<Move> GetMoves(Position from, Board_Base board)
        {
            foreach (Position to in MovePositions(from, board))
            {
                yield return new NormalMove(from, to);
            }

            if(CanCastleKingSide(from, board))
            {
                yield return new Castle(MoveType.CastleKS, from);
            }
            if(CanCastleQueenSide(from, board))
            {
                yield return new Castle(MoveType.CastleQS, from);
            }
        }

        public override bool CanCaptureOpponentKing(Position from, Board_Base board)
        {
            return MovePositions(from, board)
                .Select(to => board[to])
                .Any(piece => piece?.Type == PieceType.King);
        }


    }
}
