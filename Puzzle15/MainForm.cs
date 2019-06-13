using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle15
{
    public partial class MainForm : Form
    {
        private Puzzle _puzzle;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _puzzle = new Puzzle();
            _puzzle.Shuffle();
            _puzzle.ImageUpdated += (_, __) =>
            {
                PctPuzzle.Image = null;
                PctPuzzle.Image = _puzzle.Image;
            };
            PctPuzzle.Image = _puzzle.Image;
        }

        private void PctPuzzle_MouseDown(object sender, MouseEventArgs e)
        {
            var targetCell = new Point((int)(e.Location.X / (double)Puzzle.CellPixelWidth),
                          (int)(e.Location.Y / (double)Puzzle.CellPixelHeight));
            if (_puzzle.CanMove(targetCell))
            {
                _puzzle.MoveCell(targetCell);
            }
            if (_puzzle.IsCompleted())
            {
                PctPuzzle.Enabled = false;
                MessageBox.Show("おめでとうございます ！！！", "Puzzle 15", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnShuffle_Click(object sender, EventArgs e)
        {
            _puzzle.Shuffle();
            PctPuzzle.Enabled = true;
        }
    }
}
