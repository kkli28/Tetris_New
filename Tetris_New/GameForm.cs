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
        Random random;
        int gameMode;
        int score;
        int record;
        bool bdAcc;                         //边界跨越选项
        bool haveBomb;                  //是否有炸弹

        //构造函数--需要参数：游戏模式、是否跨越边界、是否有炸弹、第几关
        public GameForm(int mode, bool bda, bool hb, int hd = -1, Point[] points =null) {
            InitializeComponent();

            //TODO: 添加参数判断
            gameMode = mode;
            bdAcc = bda;
            haveBomb = hb;
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

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e) {
            MenuForm.thisForm.Show();
        }
    }
}
