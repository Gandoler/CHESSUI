using ChessLogic;
using ChessLogic.Boardik;
using ChessLogic.GameState;
using ChessLogic.Moves;
using ChessUI.Code;
using ChessUI.Model;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

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
            Lazy<GameOverMenu> _lazyGameOverMenu = new Lazy<GameOverMenu>(() => new GameOverMenu(gameState.Result, gameState.CurrentPlayer));
         PauseMent _pauseMenu = new PauseMent();
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(150, 125, 255, 125));

            var presenter = new Presenter(chessWindow, gameState, modelka,_promMenu,_lazyGameOverMenu, _pauseMenu, brush);
            
            chessWindow.Show();

        }
    }

}
