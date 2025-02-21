using ChessLogic;
using ChessLogic.Boardik;
using ChessLogic.Moves;

namespace ChessUI.Model
{
    public interface IModel
    {
        public event EventHandler<HighlightEventArgs> PositionForHighlits;
        public event EventHandler<HighlightEventArgs> PositionForSwithOfHighlits;
        public event EventHandler<HandlePromotionMoveEventsArgs> HandlePromotionMove;
        public event EventHandler<MakeMoveEventArgs> MakeMove;
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
