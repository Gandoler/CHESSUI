using ChessLogic;
using ChessLogic.Moves;

namespace ChessUI.Model
{

    public class HighlightEventArgs : EventArgs
    {
        public Dictionary<Position, Move> Moves { get; }

        public HighlightEventArgs(Dictionary<Position, Move> moves)
        {
            Moves = moves;
        }
    }

}
