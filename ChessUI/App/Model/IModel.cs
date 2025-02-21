using ChessLogic;
using ChessLogic.Boardik;
using ChessLogic.GameState;
using ChessLogic.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessUI.Model
{
    public interface IModel
    {
        public event EventHandler<HighlightEventArgs> PositionForHighlits;
        public event EventHandler<HighlightEventArgs> PositionForSwithOfHighlits;
        public event EventHandler<(Move, Player)> HandlePromotionMove;
        public event EventHandler<(Board_Base, Player)> MakeMove;
        public event Action GameOver;
        public Player CurrentPlayer { get; }
        public Board_Base GetBoard { get ; }

        public Result RestartGame();
        public void MouseCLickHandler(Position pos);
        public void OnFromPositionSelected(Position pos);
        public void OnToPositionSelected(Position pos);
        public void HandleMove(Move move);
    }
}
