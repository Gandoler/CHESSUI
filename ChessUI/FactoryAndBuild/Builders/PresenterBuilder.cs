using ChessLogic.GameState;
using ChessUI.Code;
using ChessUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChessUI.FactoryAndBuild.Builders
{
    public class PresenterBuilder
    {
        private MainWindow _chessWindow;
        private GameState _gameState;
        private Modelka _modelka;
        private Lazy<PromotionMenu> _promMenu;
        private Lazy<GameOverMenu> _lazyGameOverMenu;
        private PauseMent _pauseMenu;
        private SolidColorBrush _brush;

        public PresenterBuilder WithChessWindow(MainWindow chessWindow)
        {
            _chessWindow = chessWindow;
            return this;
        }

       

        public PresenterBuilder WithModelka(Modelka modelka)
        {
            _modelka = modelka;
            return this;
        }

        public PresenterBuilder WithPromotionMenu(Lazy<PromotionMenu> promMenu)
        {
            _promMenu = promMenu;
            return this;
        }

        public PresenterBuilder WithGameOverMenu(Lazy<GameOverMenu> lazyGameOverMenu)
        {
            _lazyGameOverMenu = lazyGameOverMenu;
            return this;
        }

        public PresenterBuilder WithPauseMenu(PauseMent pauseMenu)
        {
            _pauseMenu = pauseMenu;
            return this;
        }

        public PresenterBuilder WithBrush(SolidColorBrush brush)
        {
            _brush = brush;
            return this;
        }

        public Presenter Build()
        {
            return new Presenter(_chessWindow, _gameState, _modelka, _promMenu, _lazyGameOverMenu, _pauseMenu, _brush);
        }
    }
}
