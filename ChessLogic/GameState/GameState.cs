using ChessLogic.Boardik;
using ChessLogic.Moves;
using ChessLogic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic.GameState
{
    public class GameState
    {
        public Board_Base Board { get; set; }
        public Player CurrentPlayer { get; private set; }
        public Result Result { get;  set; } = null;

        private int noCapptureOrPawnMoves = 0;

        public GameState(Player player, Board_Base board)
        {
            CurrentPlayer = player;
            Board = board;
        }


        public IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if (Board.isEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];
            IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
            return moveCandidates.Where(move => move.IsLegal(Board));
        }

        public void MakeMove(Move move)
        {
            Board.SetPawnSkipPosition(CurrentPlayer, null);
            bool captureOrPawn = move.Execute(Board);

            if (captureOrPawn)
            {
                noCapptureOrPawnMoves = 0;
            }
            else
            {
                noCapptureOrPawnMoves++;
            }

            CurrentPlayer = CurrentPlayer.Opponent();
            CheckForGameOver();
        }

        public IEnumerable<Move> AllLegalMovesFor(Player player)
        {
            IEnumerable<Move> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
            {
                Piece piece = Board[pos];
                return piece.GetMoves(pos, Board);
            });

            return moveCandidates.Where(move => move.IsLegal(Board));
        }

        private void CheckForGameOver()
        {
            if (!AllLegalMovesFor(CurrentPlayer).Any())
            {
                if (Board.IsInCheck(CurrentPlayer))
                {
                    Result = Result.Win(CurrentPlayer.Opponent());
                }
                else
                {
                    Result = Result.Draw(EndReason.Stalemate);
                }
            }
            else if (Board.InsufficientMaterial())
            {
                Result = Result.Draw(EndReason.InsufficientMaterial);
            }
            else if (FiftyMoveRool())
            {
                Result = Result.Draw(EndReason.FiftyMoveRule);

            }
        }

        public bool isGameOver()
        {
            return Result != null;
        }


        private bool FiftyMoveRool()
        {
            int fullMoves = noCapptureOrPawnMoves / 2;
            return fullMoves == 50;
        }
    }
}
