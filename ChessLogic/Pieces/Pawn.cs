using ChessLogic.Boardik;
using ChessLogic.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic.Pieces
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;

        public override Player Color { get; }

        private  readonly Direction forward;

        public Pawn(Player Color)
        {
            this.Color = Color;
            if (Color == Player.White)
            {
                forward = Direction.North;
            }
            else if (Color == Player.Black)
            {
                forward = Direction.South;
            }
        }

        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }


        private static bool CanMoveTo(Position pos, Board_Base board)
        {
            return Board_Base.IsInside(pos) && board.isEmpty(pos);
        }


        private bool CanCaptureAt(Position pos, Board_Base board)
        {
            if (!Board_Base.IsInside(pos) || board.isEmpty(pos))
            {
                return false;
            }

            return board[pos].Color != Color;
        }


        private static IEnumerable<Move> PromotionMoves(Position from, Position to)
        {
            yield return new PawnPromotion(from, to, PieceType.Knight);
            yield return new PawnPromotion(from, to, PieceType.Bishop);
            yield return new PawnPromotion(from, to, PieceType.Rook);
            yield return new PawnPromotion(from, to, PieceType.Queen);
        }


        private IEnumerable<Move> ForwardMoves(Position from, Board_Base board)
        {
            Position oneMovePos = from + forward;

            if (CanMoveTo(oneMovePos, board))
            {
                if (oneMovePos.Row == 0 || oneMovePos.Row == 7)
                {
                    foreach (Move promMove in PromotionMoves(from, oneMovePos))
                    {
                        yield return promMove;
                    }
                }
                else
                {
                    yield return new NormalMove(from, oneMovePos);
                }
            }

            Position twoMovesPos = oneMovePos + forward;

            if (!HasMoved && CanMoveTo(twoMovesPos, board))
            {
                yield return new DoublePawn(from, twoMovesPos);
            }
        }

        private IEnumerable<Move> DiagonalMoves(Position from, Board_Base board)
        {
            foreach (Direction dir in new Direction[] { Direction.West, Direction.East })
            {
                Position to = from + forward + dir;

                if (to == board.GetPawnSkipPosition(Color.Opponent()))
                {
                    yield return new EnPassant(from, to);
                }
                else if (CanCaptureAt(to, board))
                {
                    if (to.Row == 0 || to.Row == 7)
                    {
                        foreach (Move promMove in PromotionMoves(from, to))
                        {
                            yield return promMove;
                        }
                    }
                    else
                    {
                        yield return new NormalMove(from, to);
                    }
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position from, Board_Base board)
        {
            return ForwardMoves(from, board).Concat(DiagonalMoves(from, board));
        }

        public override bool CanCaptureOpponentKing(Position from, Board_Base board)
        {
            return DiagonalMoves(from, board)
                .Select(move => board[move.ToPos])
                .Any(piece => piece?.Type == PieceType.King);
        }
    }
}
