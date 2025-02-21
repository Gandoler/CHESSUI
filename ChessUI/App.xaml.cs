using ChessLogic;
using ChessLogic.Boardik;
using ChessLogic.GameState;
using ChessLogic.Moves;
using ChessUI.Code;
using ChessUI.FactoryAndBuild.Builders;
using ChessUI.FactoryAndBuild.Factory;
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


            PresenterComponentsFactory factory = new();
            PresenterBuilder builder = new();
            Director direction = new(builder, factory);

        }
    }

}
