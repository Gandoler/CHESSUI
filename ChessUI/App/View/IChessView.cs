using ChessLogic;
using ChessLogic.Boardik;
using ChessLogic.Moves;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ChessUI.Code.View
{
    public interface IChessView
    {
        public event EventHandler<KeyEventArgs> Window_KeyDownEsc;
        public event EventHandler<Point> BoardGrid_MouseDownEvent;
        public event Action RestartGame_Click;
        public event Action ReDrawBord;
        public event Action ChangeCursor;
        public event Action<Move> ShowChangeMenu;


        public bool isMenuOnScreeen();
        public void ShowPauseMenu(PauseMent pauseMenu);
        public void ShowGameOver(GameOverMenu gameOverMenu);
        public void DrawBoard(Board_Base board);
        public void ShowHighlights(SolidColorBrush brush, Dictionary<Position, Move> keyValuePairs);
        public void HideHighlights(Dictionary<Position, Move> keyValuePairs);
        public void SetCursor(Player player);
        public void HandlePromotion(Position from, Position to, PromotionMenu promMenu, Player player);
        public Position ToSquarePosition(Point point);
    }
}
