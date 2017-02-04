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
    public partial class MenuForm : Form {
        public MenuForm() {
            InitializeComponent();
        }

        private void classicBtn_Click(object sender, EventArgs e) {
            GameForm gf = new GameForm();
            gf.ShowDialog();
        }

        private void MenuForm_Load(object sender, EventArgs e) {

        }

        private void button29_Click(object sender, EventArgs e) {
            MessageBox.Show("我是方块啦！  (～￣▽￣)～");
        }

        private void button38_Click(object sender, EventArgs e) {
            MessageBox.Show("我是炸弹！  (〃｀ 3′〃)");
        }

        private void button42_Click(object sender, EventArgs e) {
            MessageBox.Show("俄罗斯方块的英文名，要不要这么无知！  -_-|||");
        }

        private void helpBtn_Click(object sender, EventArgs e) {
            HelpForm hf = new HelpForm();
            hf.ShowDialog();
        }

        private void button35_Click(object sender, EventArgs e) {

        }

        private void button36_Click(object sender, EventArgs e) {

        }

        private void button34_Click(object sender, EventArgs e) {

        }

        private void button33_Click(object sender, EventArgs e) {

        }

        private void button43_Click(object sender, EventArgs e) {
            MessageBox.Show("我是超级宇宙无敌大炸弹！嗷呜~  凸(艹皿艹 )");
        }
    }
}
