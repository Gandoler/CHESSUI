using ChessLogic;
using ChessLogic.GameState;
using System.Windows;
using System.Windows.Controls;

namespace ChessUI
{
    /// <summary>
    /// Логика взаимодействия для GameOverMenu.xaml
    /// </summary>
    public partial class GameOverMenu : UserControl
    {
        public event Action<Option>? OptionSelected;
        private readonly Result _result;
        public GameOverMenu(Result result, Player player)
        {
            InitializeComponent();


            _result = result;

            if (this._result == null) { throw new Exception("Пустой кент"); }
            WinnerText.Text = GetWinnerText(this._result.Winner);
            ReasonText.Text = GetReasonText(this._result.Reason, player);
        }

        


        private static string GetWinnerText(Player winner)
        {
            return winner switch
            {
                Player.White => "WHITE WINS!",
                Player.Black => "BLACK WINS!",
                _ => "IT'S A DRAW"
            };
        }

        private static string PlayerString(Player player)
        {
            return player switch
            {
                Player.White => "WHITE",
                Player.Black => "BLACK",
                _ => ""
            };
        }


        private static string GetReasonText(EndReason reason, Player currentPlayer)
        {
            return reason switch
            {
                EndReason.Stalemate => $"STALEMATE - {PlayerString(currentPlayer)} CAN'T MOVE",
                EndReason.Checkmate => $"CHECKMATE - {PlayerString(currentPlayer)} CAN'T MOVE",
                EndReason.FiftyMoveRule => "FIFTY-MOVE RULE",
                EndReason.InsufficientMaterial => "INSUFFICIENT MATERIAL",
                EndReason.ThreefoldRepetition => "THREEFOLD REPETITION",
                _ => ""
            };
        }
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Exit);
        }
    }
}
