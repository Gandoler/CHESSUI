using ChessLogic.Boardik;
using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
