using ChessLogic.Moves;
using ChessLogic.Boardik;

namespace ChessLogic.Pieces
{
    public abstract class Piece
    {
        public abstract PieceType Type { get; }
        public abstract Player Color { get; }

        public bool HasMoved { get; set; } = false;

        public abstract Piece Copy();
        public abstract IEnumerable<Move> GetMoves(Position from, Board_Base board);

        protected IEnumerable<Position> MovePositionsInDir(Position from, Board_Base board, Direction direction)
        {
            for(Position pos = from + direction; Board_Base.IsInside(pos); pos += direction)
            {
                if (board.isEmpty(pos)) {
                    yield return pos;
                    continue;
                }
                Piece piece = board[pos];

                if(piece.Color != Color)
                {
                    yield return pos;
                }
                yield break;
            }
        }


        protected IEnumerable<Position> MovePositionsInDirs(Position from, Board_Base board, Direction[] directions)
        {
            return directions.SelectMany(dir => MovePositionsInDir(from, board, dir));
        }

        public virtual bool CanCaptureOpponentKing(Position from, Board_Base board)
        {
            return GetMoves(from, board)
                .Select(move => board[move.ToPos])
                .Any(piece => piece?.Type == PieceType.King);
        }
    }
}
