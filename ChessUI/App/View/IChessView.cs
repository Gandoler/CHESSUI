using ChessLogic;
using ChessLogic.Boardik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChessUI.Code.View
{
    public interface IChessView
    {
        public event EventHandler<KeyEventArgs> Window_KeyDownEsc;
        public event Action RestartGame_Click;

        public bool isMenuOnScreeen();
        public void ShowPauseMenu(PauseMent pauseMenu);
    }
}
