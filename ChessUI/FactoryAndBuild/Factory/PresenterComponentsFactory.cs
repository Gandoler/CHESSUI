using ChessLogic;
using ChessLogic.Boardik;
using ChessLogic.GameState;
using ChessLogic.Moves;
using ChessUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessUI.FactoryAndBuild.Factory
{
    class PresenterComponentsFactory : IPresenterComponentsFactory
    {
        private readonly GameState _gameState = new(ChessLogic.Player.White, Board_Base.initial());
        private readonly Dictionary<Position, Move> moveCache = new();
        public PresenterComponentsFactory()
        {

        }

        public Lazy<GameOverMenu> CreateGameOverMenu()
        {
            return new Lazy<GameOverMenu>(() => new GameOverMenu(_gameState.Result, _gameState.CurrentPlayer));
        }

        public Modelka CreateModel()
        {
            return new Modelka(moveCache, _gameState);
        }

        public PauseMent CreatePauseMenu()
        {
            return new PauseMent();
        }

        public Lazy<PromotionMenu> CreatePromotionMenu()
        {
            return new Lazy<PromotionMenu>(() => new PromotionMenu(_gameState.CurrentPlayer));
        }

        public MainWindow CreateView()
        {
            return new MainWindow();
        }
    }
}
