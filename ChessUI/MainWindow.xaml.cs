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
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move> moveCache = new();
        
        private GameState gameState;
        private Position selectedPos = null;
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            if (gameState == null)
            {
                gameState = new GameState(Player.White, Board_Base.initial());
            }

            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);

            
        }
        #region EscMenu
        //нажатие клавиши  
        public event EventHandler<KeyEventArgs>? Window_KeyDownEsc;
        public event Action? RestartGame_Click;

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


        private void DrawBoard(Board_Base board)
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


        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isMenuOnScreeen())
            {
                return;
            }

            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);

            if(selectedPos == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        private void OnToPositionSelected(Position pos)
        {
            selectedPos = null;
            
            HideHighlights();

            if(moveCache.TryGetValue(pos, out Move move))
            {
                if (move.Type == MoveType.PawnPromotion)
                {
                    HandlePromotion(move.FromPos, move.ToPos);
                }
                else 
                {
                    HandleMove(move);
                }
            }
        }

        private void HandlePromotion(Position from, Position to)
        {
            pieceImages[to.Row, to.Column].Source = Images.Instance.GetImage(gameState.CurrentPlayer, PieceType.Pawn);
            pieceImages[from.Row, from.Column].Source = null;

            PromotionMenu promMenu = new PromotionMenu(gameState.CurrentPlayer);
            MenuContainer.Content = promMenu;

            promMenu.PieceSelected += type =>
            {
                MenuContainer.Content = null;
                Move promMove = new PawnPromotion(from, to, type);
                HandleMove(promMove);
            };
        }

        private void HandleMove(Move move)
        {
           gameState.MakeMove(move);
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);


            if (gameState.isGameOver())
            {
                ShowGameOver();
            }
        }

        private void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = gameState.LegalMovesForPiece(pos);

            if (moves.Any())
            {
                selectedPos = pos;
                CacheMoves(moves);  
                ShowHighlights();
            }

        }

        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualHeight / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);

            return new Position(row, col);
        }

        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();

            foreach (var VARIABLE in moves)
            {
                moveCache[VARIABLE.ToPos] = VARIABLE;
            }
        }

        private void ShowHighlights() // включает подсветку
        {
            Color color = Color.FromArgb(150, 125, 255, 125);
            SolidColorBrush brush = new SolidColorBrush(color);

            foreach (var pos in moveCache.Keys)
            {
                highlights[pos.Row, pos.Column].Fill = new SolidColorBrush(color);
            }
        }

        private void HideHighlights()// выключает подсветку
        {
            foreach (var pos in moveCache.Keys)
            {
                highlights[pos.Row, pos.Column].Fill = Brushes.Transparent;
            }
        }


        private void SetCursor(Player player)
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




        private void ShowGameOver()
        {
            GameOverMenu gameOverMenu = new GameOverMenu(gameState);
            MenuContainer.Content = gameOverMenu;

            gameOverMenu.OptionSelected += option =>
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

        #region RestatrGame

        public void RestartGame()
        {
            selectedPos = null;
            HideHighlights();
            moveCache.Clear();
            gameState = new GameState(Player.White, Board_Base.initial());
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);
        }

        #endregion restartgame




    }
}