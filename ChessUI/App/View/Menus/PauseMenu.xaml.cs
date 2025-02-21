using System.Windows;
using System.Windows.Controls;

namespace ChessUI
{
    /// <summary>
    /// Логика взаимодействия для PauseMent.xaml
    /// </summary>
    public partial class PauseMent : UserControl
    {
        public event Action<Option> OptionSelected;
        public PauseMent()
        {
            InitializeComponent();
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Continue);
        }
    }
}
