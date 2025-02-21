using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessUI.FactoryAndBuild.Factory
{
    interface IPresenterComponentsFactory
    {
        Lazy<PromotionMenu> CreatePromotionMenu(Player currentPlayer);
        Lazy<GameOverMenu> CreateGameOverMenu(GameResult result, Player currentPlayer);
        PauseMent CreatePauseMenu();
    }
}
