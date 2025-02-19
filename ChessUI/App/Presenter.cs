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

namespace ChessUI.Code
{
    internal class Presenter
    {
        private readonly IChessView _view;
        private readonly GameState _gameState;
        private readonly PauseMent pauseMent;
        private Lazy<GameOverMenu> _lazyGameOverMenu;
        public Presenter(IChessView chessView, GameState gameState)
        {
            // первый пупсик которого перенесли
            _gameState = gameState;
            pauseMent = new PauseMent();
            _lazyGameOverMenu = new Lazy<GameOverMenu>(() => new GameOverMenu(gameState));
            _view = chessView;
            Color color = Color.FromArgb(150, 125, 255, 125);
            SolidColorBrush brush = new SolidColorBrush(color);





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
