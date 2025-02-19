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

            var chessWindow = new MainWindow();

            if (chessWindow.IsLoaded)
            {

                var presenter = new Presenter(chessWindow);
            }
            chessWindow.Show();

        }
    }

}
