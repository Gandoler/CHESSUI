using ChessLogic;
using ChessLogic.Moves;

namespace ChessUI.Model
{
    public class HandlePromotionMoveEventsArgs: EventArgs
    {
        public Move Move { get; }
        public Player Player { get; }

        public HandlePromotionMoveEventsArgs(Move move, Player Player)
        {
            this.Move = move;
            this.Player = Player;
        }
    }
}
