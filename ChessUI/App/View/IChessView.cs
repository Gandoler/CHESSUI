using ChessLogic;
using ChessLogic.Boardik;
using ChessLogic.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public event Action Game_Over_event;
        public event Action ReDrawBord;

        public event Action ShowHighLight;
        public event Action UnShowHiighLight;


        public event Action ChangeCursor;
        // для отслеживания что-то типо обсервера
        public event Action<Position?>? SelectedPosChanged;




        public bool isMenuOnScreeen();
        public void ShowPauseMenu(PauseMent pauseMenu);


        public void RestartGame();
        public void RestartGame(Dictionary<Position, Move> keyValuePairs);
        public void ShowGameOver(Lazy<GameOverMenu> gameOverMenu);

        public void DrawBoard(Board_Base board);


        public void ShowHighlights(SolidColorBrush brush); // включает подсветку
        public void ShowHighlights(SolidColorBrush brush, Dictionary<Position, Move> keyValuePairs);
        public void HideHighlights();// выключает подсветку
        public void HideHighlights(Dictionary<Position, Move> keyValuePairs);

        public void SetCursor(Player player);



        // группа по нажатию на клетку
        public void OnToPositionSelected(Position pos);
        public void HandlePromotion(Position from, Position to);
        public void HandleMove(Move move);
        public void OnFromPositionSelected(Position pos);
        public Position ToSquarePosition(Point point);
    }
}
