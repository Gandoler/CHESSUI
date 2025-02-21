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

        
        SolidColorBrush brush= new SolidColorBrush(Color.FromArgb(150, 125, 255, 125));
        
        private readonly Lazy<PromotionMenu> _promMenu;
        private  Lazy<GameOverMenu> _lazyGameOverMenu;
        private readonly PauseMent _pauseMenu;

        public Presenter(IChessView chessView, GameState gameState, IModel model,
                             Lazy<PromotionMenu> promMenu, Lazy<GameOverMenu> lazyGameOverMenu,
                             PauseMent pauseMenu)
        {
            _view = chessView;
            _model = model;
            _pauseMenu = pauseMenu;
            _lazyGameOverMenu = lazyGameOverMenu;
            _promMenu = promMenu;



            #region model init sub
            _model.PositionForHighlits += (s, e) => _view.ShowHighlights(brush, e.Moves);
            _model.PositionForSwithOfHighlits += (s, e) => _view.HideHighlights(e.Moves);
            _model.HandlePromotionMove += (s, e) => _view.HandlePromotion(e.Item1.FromPos, e.Item1.ToPos, _promMenu.Value, e.Item2);
            _model.GameOver += _model_GameOver;
            _model.MakeMove += _model_MakeMove;

            #endregion


            #region view init sub
            //подписка для обработчика клавиш
            _view.Window_KeyDownEsc += _view_Window_KeyDown;

            //подписка для рестарта игры
            _view.RestartGame_Click += ()=>RestartGame();

            // Upate доски
            _view.ReDrawBord +=()=> _view.DrawBoard(_model.GetBoard);

            //изменение курсора 
            _view.ChangeCursor += () => _view.SetCursor(_model.CurrentPlayer);

            //при нажатии на клекту
            _view.BoardGrid_MouseDownEvent += _view_BoardGrid_MouseDownEvent;

            // временнннннная !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            _view.ShowChangeMenu += (Move v) => _model.HandleMove(v);
            #endregion

        }

        private void _model_MakeMove(object? sender, MakeMoveEventArgs e)
        {
            _view.DrawBoard(e.Board);
            _view.SetCursor(e.Player);

        }

        private void _model_GameOver()
        {
            _view.ShowGameOver(_lazyGameOverMenu.Value);
        }

        public void RestartGame()
        {
            Result res = _model.RestartGame();
            _lazyGameOverMenu = new Lazy<GameOverMenu>(() => new GameOverMenu(res,_model.CurrentPlayer));
     

        }


        // нажатие на esc
        private void _view_Window_KeyDown(object? sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!_view.isMenuOnScreeen() && e.Key == Key.Escape)
            {
                _view.ShowPauseMenu(_pauseMenu);
            }
        }


        #region MouseHandler
        // обратоька нгажатия мыши

        private void _view_BoardGrid_MouseDownEvent(object? sender, Point point)
        {
            Position pos = _view.ToSquarePosition(point);

            _model.MouseCLickHandler(pos);
        }

        #endregion 

    }
}
