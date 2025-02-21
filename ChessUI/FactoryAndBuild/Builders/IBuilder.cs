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
    interface IBuilder
    {
        public PresenterBuilder WithChessWindow(MainWindow chessWindow);


        public PresenterBuilder WithGameState(GameState gameState);

        public PresenterBuilder WithModelka(Modelka modelka);

        public PresenterBuilder WithPromotionMenu(Lazy<PromotionMenu> promMenu);

        public PresenterBuilder WithGameOverMenu(Lazy<GameOverMenu> lazyGameOverMenu);

        public PresenterBuilder WithPauseMenu(PauseMent pauseMenu);

        public PresenterBuilder WithBrush(SolidColorBrush brush);

        public Presenter Build();
    }
}
