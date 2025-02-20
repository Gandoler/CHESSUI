using ChessLogic;
using ChessLogic.Pieces;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLogic.Moves;
using ChessUI.Singletons;
using ChessLogic.GameState;
using ChessLogic.Boardik;
using ChessUI.Code.View;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IChessView
    {

        #region initAndGo
        private readonly Image[,] pieceImages = new Image[8, 8]; //  это прост храниоище картинок тоже тута
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];  // это подсветка ее оставим тута


        
        private readonly GameState _gameState;
      
    
        public MainWindow(GameState gameState)
        {
            InitializeComponent();
            InitializeBoard();
            

           _gameState = gameState;

           
            this.MouseEnter += MainWindow_MouseEnter;
            
        }


        //при первом попадании мыши подгруз
        private void MainWindow_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeCursor?.Invoke();// было SetCursor(_gameState.CurrentPlayer);
            ReDrawBord?.Invoke();// было DrawBoard(_gameState.Board);
        }

        //нажатие клавиши  
        public event EventHandler<KeyEventArgs>? Window_KeyDownEsc;
        // нажатие рестарта игры
        public event Action? RestartGame_Click;

        // случился геймовер
        public event Action? Game_Over_event;
        //перерисовка доски
        public event Action? ReDrawBord;
        //включение подсветки
        public event Action? ShowHighLight;
        //выключенеи подсветки
        public event Action? UnShowHiighLight;
        
        // изменение курсора 
        public event Action? ChangeCursor;
        //при нажатии на клекту
        public event EventHandler<Point>? BoardGrid_MouseDownEvent;


        // эта тема просто заполняет контейнерами для картинок щахмат
        private void InitializeBoard()
        {
            for(int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    pieceImages[r, c] = image;
                    PieceGrid.Children.Add(image);


                    Rectangle highlight = new Rectangle();
                    highlights[r,c] = highlight;
                    HiglightGrid.Children.Add(highlight);
                }
            }
        }
        public void DrawBoard(Board_Base board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = board[r, c];
                    pieceImages[r,c].Source = Images.Instance.GetImage(piece);
                }
            }
        }

        #endregion

        #region EscMenu
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Window_KeyDownEsc?.Invoke(sender, e);
        }


        public void ShowPauseMenu(PauseMent pauseMenu)
        {
            MenuContainer.Content = pauseMenu;

            pauseMenu.OptionSelected += option =>
            {
                MenuContainer.Content = null;
                if (option == Option.Restart)
                {
                    RestartGame_Click?.Invoke();
                }
            };
        }

        public bool isMenuOnScreeen()
        {
            return MenuContainer.Content != null;
        }
        #endregion

        //s





        // обратоька нгажатия мыши
        public void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (isMenuOnScreeen())
            {
                return;
            }
            Point point = e.GetPosition(BoardGrid);
            BoardGrid_MouseDownEvent?.Invoke(sender, point);

        }

       
        public event Action<Move> Tempevent;
        public void HandlePromotion(Position from, Position to)
        {
            pieceImages[to.Row, to.Column].Source = Images.Instance.GetImage(_gameState.CurrentPlayer, PieceType.Pawn);
            pieceImages[from.Row, from.Column].Source = null;

            PromotionMenu promMenu = new PromotionMenu(_gameState.CurrentPlayer);
            MenuContainer.Content = promMenu;

            promMenu.PieceSelected += type =>
            {
                MenuContainer.Content = null;
                Move promMove = new PawnPromotion(from, to, type);
                Tempevent?.Invoke(promMove);
                
            };
        }

       

        public Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualHeight / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);

            return new Position(row, col);
        }

       

        #region design
        public void SetCursor(Player player)
        {
            if (player == Player.White)
            {
                Cursor = ChessCursors.WhiteCursor;
            }
            else
            {
                Cursor = ChessCursors.BlackCursor;
            }
        }
     


        public void ShowHighlights(SolidColorBrush brush, Dictionary<Position, Move> keyValuePairs) // включает подсветку
        {


            foreach (var pos in keyValuePairs.Keys)
            {
                highlights[pos.Row, pos.Column].Fill = brush;
            }
        }

        public void HideHighlights(Dictionary<Position, Move> keyValuePairs)// выключает подсветку
        {
            foreach (var pos in keyValuePairs.Keys)
            {
                highlights[pos.Row, pos.Column].Fill = Brushes.Transparent;
            }
        }

        #endregion


        #region gameend
        public void ShowGameOver(Lazy<GameOverMenu> gameOverMenu)
        {
            GameOverMenu foruse = gameOverMenu.Value;
            MenuContainer.Content = foruse;

            foruse.OptionSelected += option =>
            {
                if (option == Option.Restart)
                {
                    MenuContainer.Content = null;
                    RestartGame_Click?.Invoke();
                    
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }
        #endregion

       


    }
}