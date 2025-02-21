using ChessLogic.Boardik;
using ChessLogic;
using ChessLogic.GameState;
using ChessUI.Code.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Http.Headers;
using System.Windows.Media;
using System.Windows;
using ChessLogic.Moves;
using ChessUI.Model;

namespace ChessUI.Code
{
    internal class Presenter
    {
        private readonly IModel _model;
        private readonly IChessView _view;

        
        private readonly GameState _gameState;
        private readonly PauseMent pauseMent;
        private Lazy<GameOverMenu> _lazyGameOverMenu;
        private Position? _selectedPos = null;
        SolidColorBrush brush;
        private readonly Dictionary<Position, Move> moveCache = new();
        private readonly Lazy<PromotionMenu> promMenu;


        public Presenter(IChessView chessView, GameState gameState, IModel model)
        {
            _view = chessView;
            _model = model;


            // первый пупсик которого перенесли
            _gameState = gameState;
            pauseMent = new PauseMent();
            _lazyGameOverMenu = new Lazy<GameOverMenu>(() => new GameOverMenu(gameState));
            promMenu = new Lazy<PromotionMenu> (()=> new PromotionMenu(_gameState.CurrentPlayer));
            Color color = Color.FromArgb(150, 125, 255, 125);
            brush = new SolidColorBrush(color);


            #region view init sub
            //подписка для обработчика клавиш
            _view.Window_KeyDownEsc += _view_Window_KeyDown;

            //подписка для рестарта игры
            _view.RestartGame_Click += ()=>RestartGame();

            // Upate доски
            _view.ReDrawBord +=()=> _view.DrawBoard(_gameState.Board);

            //изменение курсора 
            _view.ChangeCursor += () => _view.SetCursor(_gameState.CurrentPlayer);

            //при нажатии на клекту
            _view.BoardGrid_MouseDownEvent += _view_BoardGrid_MouseDownEvent;

            // временнннннная !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            _view.ShowChangeMenu += (Move v) => HandleMove(v);
            #endregion

        }



        public void RestartGame()
        {
            _lazyGameOverMenu = new Lazy<GameOverMenu>(() => new GameOverMenu(_gameState));
            _selectedPos = null;

            moveCache.Clear();
            _view.HideHighlights(moveCache);
            _gameState.Restart();
            _view.DrawBoard(_gameState.Board);
            _view.SetCursor(_gameState.CurrentPlayer);

        }


        // нажатие на esc
        private void _view_Window_KeyDown(object? sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!_view.isMenuOnScreeen() && e.Key == Key.Escape)
            {
                _view.ShowPauseMenu(pauseMent);
            }
        }


        #region MouseHandler
        // обратоька нгажатия мыши

        private void _view_BoardGrid_MouseDownEvent(object? sender, Point point)
        {
            Position pos = _view.ToSquarePosition(point);

            if (_selectedPos == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }


        // попытка перенести мув кеш
        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();

            foreach (var VARIABLE in moves)
            {
                moveCache[VARIABLE.ToPos] = VARIABLE;
            }
        }

        public void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = _gameState.LegalMovesForPiece(pos);

            if (moves.Any())
            {
                _selectedPos = pos;
                CacheMoves(moves);
                _view.ShowHighlights(brush, moveCache);
            }

        }

        public void OnToPositionSelected(Position pos)
        {
            _selectedPos = null;

            _view.HideHighlights(moveCache);

            if (moveCache.TryGetValue(pos, out var move))
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



        #endregion 

    }
}
