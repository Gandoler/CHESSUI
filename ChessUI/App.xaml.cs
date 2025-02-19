using ChessLogic.Boardik;
using ChessLogic.GameState;
using ChessUI.Code;
using System.Configuration;
using System.Data;
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

            var chessWindow = new MainWindow(gameState);
            

                var presenter = new Presenter(chessWindow, gameState);
            
            chessWindow.Show();

        }
    }

}
