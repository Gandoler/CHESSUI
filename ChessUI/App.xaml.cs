using ChessLogic;
using ChessLogic.Boardik;
using ChessLogic.GameState;
using ChessLogic.Moves;
using ChessUI.Code;
using ChessUI.Model;
using System.Diagnostics;
using System.Windows;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            GameState gameState = new GameState(ChessLogic.Player.White, Board_Base.initial());
                      Dictionary<Position, Move> moveCache = new();

        var chessWindow = new MainWindow();
            Modelka modelka = new Modelka(moveCache, gameState);
               Lazy<PromotionMenu> _promMenu =   new Lazy<PromotionMenu> (()=> new PromotionMenu(gameState.CurrentPlayer));
            Lazy<GameOverMenu> _lazyGameOverMenu = new Lazy<GameOverMenu>(() => new GameOverMenu(gameState));
         PauseMent _pauseMenu = new PauseMent();

            var presenter = new Presenter(chessWindow, gameState, modelka,_promMenu,_lazyGameOverMenu, _pauseMenu);
            
            chessWindow.Show();

        }
    }

}
