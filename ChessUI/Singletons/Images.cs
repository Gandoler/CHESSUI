using ChessLogic;
using ChessLogic.Pieces;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessUI.Singletons
{
    public sealed class Images
    {
        // Статическое поле для хранения единственного экземпляра класс а
        private static readonly Images instance = new Images();
        public static Images Instance => instance;
        private static Dictionary<PieceType, ImageSource> WhiteSources { get; } = new();
        private static Dictionary<PieceType, ImageSource> BlackSources { get; } = new();
        private readonly ImageSource EmptyCellImage = new WriteableBitmap(1, 1, 96, 96, PixelFormats.Pbgra32, null);

        static Images()
        {
            // Инициализация словарей с изображениями белых и черных фигур
            Instance.InitializeSources();
        }

        // подгрузочка в словарики
        private void InitializeSources()
        {
            WhiteSources.Clear();
            BlackSources.Clear();
            WhiteSources[PieceType.Pawn] = LoadImage("Assets/PawnW.png");
            WhiteSources[PieceType.Bishop] = LoadImage("Assets/BishopW.png");
            WhiteSources[PieceType.Knight] = LoadImage("Assets/KnightW.png");
            WhiteSources[PieceType.Rook] = LoadImage("Assets/RookW.png");
            WhiteSources[PieceType.Queen] = LoadImage("Assets/QueenW.png");
            WhiteSources[PieceType.King] = LoadImage("Assets/KingW.png");

            BlackSources[PieceType.Pawn] = LoadImage("Assets/PawnB.png");
            BlackSources[PieceType.Bishop] = LoadImage("Assets/BishopB.png");
            BlackSources[PieceType.Knight] = LoadImage("Assets/KnightB.png");
            BlackSources[PieceType.Rook] = LoadImage("Assets/RookB.png");
            BlackSources[PieceType.Queen] = LoadImage("Assets/QueenB.png");
            BlackSources[PieceType.King] = LoadImage("Assets/KingB.png");
        }


        // скрытый загрузчик
        private ImageSource LoadImage(string filePath)
        {
            Uri resourceUri = new Uri($"pack://application:,,,/{filePath}");
            return new BitmapImage(resourceUri);
        }

        // получить картинки по цвету и типу фигуры
        public ImageSource GetImage(Player color, PieceType type)
        {
            return color switch
            {
                Player.White => WhiteSources[type],
                Player.Black => BlackSources[type],
                _ => EmptyCellImage
            };
        }
        // получить изображение по фигуре
        public ImageSource GetImage(Piece piece)
        {
            if (piece == null) return EmptyCellImage;
            return GetImage(piece.Color, piece.Type);

        }
    }
}
