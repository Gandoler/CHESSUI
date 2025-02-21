using ChessLogic;
using ChessLogic.Pieces;
using ChessUI.Singletons;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChessUI
{
    /// <summary>
    /// Логика взаимодействия для PromotionMenu.xaml
    /// </summary>
    public partial class PromotionMenu : UserControl
    {
        public event Action<PieceType> PieceSelected;
        public PromotionMenu(Player player)
        {
            InitializeComponent();



            QueenImg.Source = Images.Instance.GetImage(player, PieceType.Queen);
            BishopImg.Source = Images.Instance.GetImage(player, PieceType.Bishop);
            RookImg.Source = Images.Instance.GetImage(player, PieceType.Rook);
            KnightImg.Source = Images.Instance.GetImage(player, PieceType.Knight);
        }

        private void QueenImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Queen);
        }

        private void BishopImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Bishop);

        }

        private void RookImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Rook);

        }

        private void KnightImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Knight);

        }
    }
}
