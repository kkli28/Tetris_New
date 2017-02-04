using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris_New {
    public partial class GameForm : Form {
        public GameForm() {
            InitializeComponent();
        }

        public Color intToColor(int i) {
            switch (i) {
                case 0: return Color.White;
                case 1: return Color.Green;
                case 2: return Color.Blue;
                case 3: return Color.Red;
                case 4: return Color.Orange;
                case 5: return Color.Pink;
                case 6: return Color.Purple;
                default: return Color.Black;
            }
        }

        private void GameForm_Load(object sender, EventArgs e) {

        }
    }
}
