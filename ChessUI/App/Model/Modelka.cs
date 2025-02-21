using ChessLogic;
using ChessLogic.Boardik;
using ChessLogic.GameState;
using ChessLogic.Moves;

namespace ChessUI.Model
{
    public class Modelka: IModel
    {
        private readonly Dictionary<Position, Move> _moveCache;
        private readonly IGameState _gameState; // надо будет через интерфейс
        private Position? _selectedPos = null;

        public Player CurrentPlayer { get { return _gameState.CurrentPlayer; } }
        public Board_Base GetBoard { get { return _gameState.Board; } }

        public Modelka(Dictionary<Position, Move> moveCache, IGameState gameState)
        {
            _moveCache = moveCache;
            _gameState = gameState;
        }


        public event EventHandler<HighlightEventArgs>? PositionForHighlits;
        public event EventHandler<HighlightEventArgs>? PositionForSwithOfHighlits;
        public event EventHandler<HandlePromotionMoveEventsArgs>? HandlePromotionMove;
        public event EventHandler<MakeMoveEventArgs>? MakeMove;
        public event Action? GameOver;



        // попытка перенести мув кеш

        public void  MouseCLickHandler(Position pos)
        {
            if (_selectedPos == null)
            {
                OnFromPositionSelected(pos);

            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        public Result RestartGame()
        {
            _selectedPos = null;
            PositionForSwithOfHighlits?.Invoke(this, new HighlightEventArgs(_moveCache));
            _moveCache.Clear();
            _gameState.Restart();
            MakeMove?.Invoke(this, new MakeMoveEventArgs(_gameState.Board, _gameState.CurrentPlayer));
            return _gameState.Result;
        }


        public void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = _gameState.LegalMovesForPiece(pos);

            if (moves.Any())
            {
                _selectedPos = pos;
                CacheMoves(moves);
                PositionForHighlits?.Invoke(this, new HighlightEventArgs(_moveCache));
                
            }

        }

        public void OnToPositionSelected(Position pos)
        {
            _selectedPos = null;
            PositionForSwithOfHighlits?.Invoke(this, new HighlightEventArgs(_moveCache));

            if (_moveCache.TryGetValue(pos, out var move))
            {
                if (move.Type == MoveType.PawnPromotion)
                {
                    HandlePromotionMove?.Invoke(this,new HandlePromotionMoveEventsArgs(move, _gameState.CurrentPlayer));
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
            MakeMove?.Invoke(this, new MakeMoveEventArgs(_gameState.Board, _gameState.CurrentPlayer));
           

            if (_gameState.isGameOver())
            {
                GameOver?.Invoke();
            }
        }


       
        private void CacheMoves(IEnumerable<Move> moves)
        {
            _moveCache.Clear();

            foreach (var VARIABLE in moves)
            {
                _moveCache[VARIABLE.ToPos] = VARIABLE;
            }
        }



    }
}
