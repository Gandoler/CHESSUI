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

namespace ChessUI.Code
{
    internal class Presenter
    {
        private readonly IChessView _view;
        private GameState gameState;
        private readonly PauseMent pauseMent;
        private readonly GameOverMenu gameOverMenu;

        public Presenter(IChessView chessView)
        {
            // первый пупсик которого перенесли
            gameState = new GameState(Player.White, Board_Base.initial());
            pauseMent = new PauseMent();
            gameOverMenu = new GameOverMenu(gameState);

            _view = chessView;





            //подписка для обработчика клавиш
            _view.Window_KeyDownEsc += _view_Window_KeyDown;


            //подписка для рестарта игры
            _view.RestartGame_Click += _view_RestartGame_Click;

            // gameOver 
            _view.Game_Over_event += _view_Game_Over_event;


        }


        // случился gameover
        private void _view_Game_Over_event()
        {
            _view.ShowGameOver(gameOverMenu);
        }

        //перезапуск игры
        private void _view_RestartGame_Click()
        {
            _view.RestartGame();
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
