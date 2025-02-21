using ChessLogic;
using ChessLogic.GameState;
using ChessUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessUI.FactoryAndBuild.Factory
{
    interface IPresenterComponentsFactory
    {
        
        Lazy<PromotionMenu> CreatePromotionMenu();
        Lazy<GameOverMenu> CreateGameOverMenu();
        PauseMent CreatePauseMenu();

        Modelka CreateModel();
        MainWindow CreateView();
    }
}
