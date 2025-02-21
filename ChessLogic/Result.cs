using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Result
    {
        public Player Winner { get; }
        public EndReason Reason { get; }

        public event Action<Result>? NewResult;
        public Result(Player winner, EndReason reason)
        {
            Winner = winner;
            Reason = reason;
            NewResult?.Invoke(this);
        }

        public static Result Win(Player winner)
        {
            return new Result(winner, EndReason.Checkmate);
        }

        public static Result Draw(EndReason reason)
        {
            return new Result(Player.None, reason);
        }
    }
}
