using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puzzle15.Properties;

namespace Puzzle15
{
    public class Puzzle
    {
        public const int ColumnCount = 4;
        public const int RowCount = 4;

        public const int BitmapWidth = 400;
        public const int BitmapHeight = 400;

        public const int CellPixelWidth = BitmapWidth / ColumnCount;
        public const int CellPixelHeight = BitmapHeight / RowCount;

        private readonly Color _backColor = Color.Black;
        private readonly Cell[] _cells;
        private readonly EmptyCell _emptyCell;

        public event EventHandler ImageUpdated;

        public Bitmap Image { get; private set; }

        public Puzzle()
        {
            Image = new Bitmap(BitmapWidth, BitmapHeight);
            _emptyCell = new EmptyCell(new Point(ColumnCount - 1, RowCount - 1));
            _cells = Enumerable.Range(0, ColumnCount * RowCount - 1)
                              .Select(i => new Cell(i + 1, new Point(i % ColumnCount, i / ColumnCount), $"img{i + 1}"))
                              .ToArray();
            UpdateVisual();
        }

        public void Shuffle() => Shuffle((int)DateTime.Now.Ticks);

        public void ToComplete()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].Position = new Point(i % ColumnCount, i / ColumnCount);
            }
            _emptyCell.Position = new Point(ColumnCount - 1, RowCount - 1);
            UpdateVisual();
        }

        public void Shuffle(int seed)
        {
            var random = new Random(seed);
            const int swapCount = 50 * 2;


            var nums = Enumerable.Range(0, _cells.Length).ToArray();

            for (int i = 0; i < swapCount; i++)
            {
                var a = random.Next() % nums.Length;
                var b = random.Next() % (nums.Length - 1);
                if (b >= a) b++;

                (nums[a], nums[b]) = (nums[b], nums[a]);
            }



            foreach (var (x, i) in nums.Select((p, i) => (p, i)))
            {
                _cells[x].Position = new Point(i % ColumnCount, i / RowCount);
            }
            _emptyCell.Position = new Point(ColumnCount - 1, RowCount - 1);
            if (IsCompleted()) { Shuffle(seed + 1); }
            UpdateVisual();
        }

        /// <summary>セルを移動させます</summary>
        /// <param name="clickedPixel">クリックされたセル</param>
        public void MoveCell(Point targetCell)
        {
            var target = _cells.FirstOrDefault(cell => cell.Position == targetCell);
            target.Position = _emptyCell.Position;
            _emptyCell.Position = targetCell;
            UpdateVisual();
        }

        public bool IsCompleted()
        {
            return _cells.All(cell => cell.Number - 1 == cell.PosY * ColumnCount + cell.PosX);
        }

        public bool CanMove(Point targetCell)
        {
            var target = _cells.FirstOrDefault(cell => cell.Position == targetCell);
            if(target == null) { return false; }
            return Math.Abs(_emptyCell.PosX - target.PosX) + Math.Abs(_emptyCell.PosY - target.PosY) == 1;
        }

        private void UpdateVisual()
        {
            using (var g = Graphics.FromImage(Image))
            {
                g.Clear(_backColor);
                foreach (var cell in _cells)
                {
                    g.DrawImage(cell.Image, new Point(cell.PosX * CellPixelWidth, cell.PosY * CellPixelHeight));
                }
            }
            ImageUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
