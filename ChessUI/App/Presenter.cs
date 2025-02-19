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

namespace ChessUI.Code
{
    internal class Presenter
    {
        private readonly IChessView _view;
        private readonly GameState _gameState;
        private readonly PauseMent pauseMent;
        private Lazy<GameOverMenu> _lazyGameOverMenu;
        private Position? _selectedPos = null;
        public Presenter(IChessView chessView, GameState gameState)
        {
            // первый пупсик которого перенесли
            _gameState = gameState;
            pauseMent = new PauseMent();
            _lazyGameOverMenu = new Lazy<GameOverMenu>(() => new GameOverMenu(gameState));
            _view = chessView;
            Color color = Color.FromArgb(150, 125, 255, 125);
            SolidColorBrush brush = new SolidColorBrush(color);


            // отслеживание обсерверок
            _view.SelectedPosChanged += _view_SelectedPosChanged;


            //подписка для обработчика клавиш
            _view.Window_KeyDownEsc += _view_Window_KeyDown;


            //подписка для рестарта игры
            _view.RestartGame_Click += () => _view.RestartGame(); 

            // gameOver 
            _view.Game_Over_event += () => _view.ShowGameOver(_lazyGameOverMenu);


            // Upate доски
            _view.ReDrawBord +=()=> _view.DrawBoard(_gameState.Board);


            //включение подсветки
            _view.ShowHighLight +=()=> _view.ShowHighlights(brush);


            //выключенеи подсветки
            _view.UnShowHiighLight += () => _view.HideHighlights();

            //изменение курсора 
            _view.ChangeCursor += () => _view.SetCursor(_gameState.CurrentPlayer);

            //при нажатии на клекту
            _view.BoardGrid_MouseDownEvent += _view_BoardGrid_MouseDownEvent;



        }

        // отслеживание обсерверок
        private void _view_SelectedPosChanged(Position? position)
        {
            _selectedPos = position;
        }

        private void _view_BoardGrid_MouseDownEvent(object? sender, Point point)
        {
            Position pos = _view.ToSquarePosition(point);

            if (_selectedPos == null)
            {
                _view.OnFromPositionSelected(pos);
            }
            else
            {
                _view.OnToPositionSelected(pos);
            }
        }









        // нажатие на esc
        private void _view_Window_KeyDown(object? sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!_view.isMenuOnScreeen() && e.Key == Key.Escape)
            {
                _view.ShowPauseMenu(pauseMent);
            }
        }




    }
}
