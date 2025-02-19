using ChessLogic.Pieces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic.Boardik
{
    public class Board_Base
    {
        private readonly Piece[,] pieces = new Piece[8, 8];

        public Piece this[int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value; }
        }
        private readonly Dictionary<Player, Position> pawnSkipPositions = new Dictionary<Player, Position>
        {
            { Player.White, null },
            { Player.Black, null }
        };


        public Piece this[Position pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }

        public Position GetPawnSkipPosition(Player player)
        {
            return pawnSkipPositions[player];
        }

        public void SetPawnSkipPosition(Player player, Position pos)
        {
            pawnSkipPositions[player] = pos;
        }


        public static Board_Base initial()
        {
            Board_Base board = new Board_Base();
            board.AddStartPieses();
            return board;
        }

        private void AddStartPieses()
        {
            this[0, 0] = PieceFactory.CreatePiece(PieceType.Rook, Player.Black);
            this[0, 1] = PieceFactory.CreatePiece(PieceType.Knight, Player.Black);
            this[0, 2] = PieceFactory.CreatePiece(PieceType.Bishop, Player.Black);
            this[0, 3] = PieceFactory.CreatePiece(PieceType.Queen, Player.Black);
            this[0, 4] = PieceFactory.CreatePiece(PieceType.King, Player.Black);
            this[0, 5] = PieceFactory.CreatePiece(PieceType.Bishop, Player.Black);
            this[0, 6] = PieceFactory.CreatePiece(PieceType.Knight, Player.Black);
            this[0, 7] = PieceFactory.CreatePiece(PieceType.Rook, Player.Black);

            // Белые фигуры
            this[7, 0] = PieceFactory.CreatePiece(PieceType.Rook, Player.White);
            this[7, 1] = PieceFactory.CreatePiece(PieceType.Knight, Player.White);
            this[7, 2] = PieceFactory.CreatePiece(PieceType.Bishop, Player.White);
            this[7, 3] = PieceFactory.CreatePiece(PieceType.Queen, Player.White);
            this[7, 4] = PieceFactory.CreatePiece(PieceType.King, Player.White);
            this[7, 5] = PieceFactory.CreatePiece(PieceType.Bishop, Player.White);
            this[7, 6] = PieceFactory.CreatePiece(PieceType.Knight, Player.White);
            this[7, 7] = PieceFactory.CreatePiece(PieceType.Rook, Player.White);

            // Пешки
            for (int c = 0; c < 8; c++)
            {
                this[1, c] = PieceFactory.CreatePiece(PieceType.Pawn, Player.Black);
                this[6, c] = PieceFactory.CreatePiece(PieceType.Pawn, Player.White);
            }
        }


        public static bool IsInside(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
        }


        public bool isEmpty(Position pos)
        {
            return this[pos] == null;
        }

        public IEnumerable<Position> PiecePositions()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Position pos = new Position(r, c);

                    if (!isEmpty(pos))
                    {
                        yield return pos;
                    }
                }
            }
        }

        public IEnumerable<Position> PiecePositionsFor(Player player)
        {
            return PiecePositions().Where(pos => this[pos].Color == player);
        }

        public bool IsInCheck(Player player)
        {
            return PiecePositionsFor(player.Opponent()).Any(pos =>
            {
                Piece piece = this[pos];
                return piece.CanCaptureOpponentKing(pos, this);
            });
        }

        public Board_Base Copy()
        {
            Board_Base copy = new Board_Base();

            foreach (Position pos in PiecePositions())
            {
                copy[pos] = this[pos].Copy();
            }

            return copy;
        }


        public Counting CountPieces()
        {
            Counting counting = new Counting();
            foreach (Position pos in PiecePositions())
            {
                Piece piece = this[pos];
                counting.Increment(piece.Color, piece.Type);
            }
            return counting;

        }
        #region Check For Insuficient Material Situation
        public bool InsufficientMaterial()
        {
            Counting counting = CountPieces();

            return IsKingVKing(counting) || IsKingBishopVKing(counting) ||
                IsKingKnightVKing(counting) || IsKingBishopVKingBishop(counting);
        }


        private static bool IsKingVKing(Counting counting)
        {
            return counting.TotalCount == 2;
        }

        private static bool IsKingBishopVKing(Counting counting)
        {
            return counting.TotalCount == 3 && (counting.White(PieceType.Bishop) == 1 || counting.Black(PieceType.Bishop) == 1);
        }
        private static bool IsKingKnightVKing(Counting counting)
        {
            return counting.TotalCount == 3 && (counting.White(PieceType.Knight) == 1 || counting.Black(PieceType.Knight) == 1);
        }

        private bool IsKingBishopVKingBishop(Counting counting)
        {
            if (counting.TotalCount != 4)
            {
                return false;
            }

            if (counting.White(PieceType.Bishop) != 1 || counting.Black(PieceType.Bishop) != 1)
            {
                return false;
            }

            Position wBishopPos = FindPiece(Player.White, PieceType.Bishop);
            Position bBishopPos = FindPiece(Player.Black, PieceType.Bishop);

            return wBishopPos.SquareColor() == bBishopPos.SquareColor();
        }
        #endregion
        private Position FindPiece(Player color, PieceType type)
        {
            return PiecePositionsFor(color).First(pos => this[pos].Type == type);
        }

    }
}
