using Puzzle15.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle15
{
    public class Cell
    {
        protected int _position;
        /// <summary>番号</summary>
        public int Number { get; private set; }
        /// <summary>画像</summary>
        public Bitmap Image { get; private set; }
        /// <summary>位置</summary>
        public Point Position
        {
            get => new Point(PosX, PosY);
            set { _position = value.Y * Puzzle.ColumnCount + value.X; }
        }
        /// <summary>位置(X座標)</summary>
        public int PosX => _position % Puzzle.ColumnCount;
        /// <summary>位置(Y座標)</summary>
        public int PosY => _position / Puzzle.ColumnCount;

        /// <summary>パズルの一つのセルインスタンスを作成します</summary>
        /// <param name="number">番号</param>
        /// <param name="img">リソース画像名</param>
        public Cell(int number, Point position, string img)
        {
            Number = number;
            Position = position;
            try
            {
                var bmp = Resources.ResourceManager.GetObject(img, Resources.Culture) as Bitmap;
                Image = new Bitmap(bmp, new Size(Puzzle.CellPixelWidth, Puzzle.CellPixelHeight));
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected Cell(int number, Point position)
        {
            Number = number;
            Position = position;
        }
    }

    public class EmptyCell : Cell
    {
        private const int _emptyCellNumber = 0;

        public EmptyCell(Point position) : base(_emptyCellNumber, position) { }
    }
}
