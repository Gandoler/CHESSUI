using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ChessUI.Singletons
{
    public class ChessCursors
    {
        private static readonly ChessCursors _instance = new ChessCursors();

        public static Cursor WhiteCursor { get; private set; }
        public static Cursor BlackCursor { get; private set; }

        public static ChessCursors Instance => _instance;

        static ChessCursors()
        {
            try
            {
                WhiteCursor = LoadCursor("Assets/CursorW.cur");
                BlackCursor = LoadCursor("Assets/CursorB.cur");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки курсоров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                WhiteCursor = Cursors.Arrow;
                BlackCursor = Cursors.Arrow;
            }
        }

        private static Cursor LoadCursor(string filePath)
        {
            var resource = Application.GetResourceStream(new Uri(filePath, UriKind.Relative));
            if (resource == null || resource.Stream == null)
                throw new FileNotFoundException($"Не найден ресурс: {filePath}");

            return new Cursor(resource.Stream, true);
        }
    }
}
