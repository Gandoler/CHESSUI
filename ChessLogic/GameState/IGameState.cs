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
    public interface IGameState
    {
        public Board_Base Board { get; set; }
        public Player CurrentPlayer { get; set; }
        public Result Result { get; set; } 




        public IEnumerable<Move> LegalMovesForPiece(Position pos);

        public void MakeMove(Move move);

        public IEnumerable<Move> AllLegalMovesFor(Player player);



        public bool isGameOver();


        public void Restart();
    }
}
