using ChessLogic.GameState;
using ChessUI.Code.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChessUI.Code
{
    internal class Presenter
    {
        private readonly IChessView _view;

        public Presenter(IChessView chessView)
        {
            _view = chessView;





            //подписка для обработчика клавиш
            _view.Window_KeyDownEsc += _view_Window_KeyDown;


            //подписка для рестарта игры
            _view.RestartGame_Click += _view_RestartGame_Click;


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
                _view.ShowPauseMenu(new PauseMent());
            }
        }




    }
}
