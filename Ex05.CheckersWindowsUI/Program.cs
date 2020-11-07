using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05.CheckersWindowsUI
{
    public class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            GameSettings gameSettings = new GameSettings();
            gameSettings.ShowDialog();
            GameManager gameManager = new GameManager(gameSettings);
        }
    }
}
