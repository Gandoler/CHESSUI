using ChessLogic;
using ChessLogic.GameState;
using ChessLogic.Moves;

namespace ChessUI.Model
{
    class Model: IModel
    {
        private readonly Dictionary<Position, Move> _moveCache;
        private readonly GameState _gameState; // надо будет через интерфейс
        private Position? _selectedPos = null;

        public Model(Dictionary<Position, Move> moveCache, GameState gameState)
        {
            _moveCache = moveCache;
            _gameState = gameState;
        }


        public event EventHandler<HighlightEventArgs> PositionForHighlits;


        // попытка перенести мув кеш
        public void CacheMoves(IEnumerable<Move> moves)
        {
            _moveCache.Clear();

            foreach (var VARIABLE in moves)
            {
                _moveCache[VARIABLE.ToPos] = VARIABLE;
            }
        }

        public void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = _gameState.LegalMovesForPiece(pos);

            if (moves.Any())
            {
                _selectedPos = pos;
                CacheMoves(moves);
                PositionForHighlits?.Invoke(this, new HighlightEventArgs(_moveCache));
                _view.ShowHighlights(brush, _moveCache);
            }

        }

        public void OnToPositionSelected(Position pos)
        {
            _selectedPos = null;

            _view.HideHighlights(_moveCache);

            if (_moveCache.TryGetValue(pos, out var move))
            {
                if (move.Type == MoveType.PawnPromotion)
                {
                    _view.HandlePromotion(move.FromPos, move.ToPos, promMenu.Value, _gameState);
                }
                else
                {
                    HandleMove(move);
                }
            }
        }

        public void HandleMove(Move move)
        {
            _gameState.MakeMove(move);
            _view.DrawBoard(_gameState.Board);
            _view.SetCursor(_gameState.CurrentPlayer);


            if (_gameState.isGameOver())
            {
                _view.ShowGameOver(_lazyGameOverMenu.Value);

            }
        }



    }
}
