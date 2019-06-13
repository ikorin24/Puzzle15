using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle15
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if DEBUG
            Application.Run(new MainForm());
#else
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception)
            {
                MessageBox.Show("予期せぬエラーが発生したため、ゲームを終了します。", "Puzzle 15", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
#endif
        }
    }
}
