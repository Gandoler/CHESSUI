using ChessLogic;
using ChessLogic.Boardik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace ChessUI.Code.View
{
    public interface IChessView
    {
        public event EventHandler<KeyEventArgs> Window_KeyDownEsc;
        public event Action RestartGame_Click;


        public event Action Game_Over_event;
        public event Action ReDrawBord;

        public event Action ShowHighLight;
        public event Action UnShowHiighLight;


        public event Action<Player> ChangeCursor;

        public bool isMenuOnScreeen();
        public void ShowPauseMenu(PauseMent pauseMenu);


        public void RestartGame();
        public void ShowGameOver(Lazy<GameOverMenu> gameOverMenu);

        public void DrawBoard(Board_Base board);


        public void ShowHighlights(SolidColorBrush brush); // включает подсветку
        public void HideHighlights();// выключает подсветку

        public void SetCursor(Player player);
    }
}
