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
        public static MenuForm thisForm;

        public MenuForm() {
            InitializeComponent();

            thisForm = this;
        }

        private void classicBtn_Click(object sender, EventArgs e) {
            GameForm gf = new GameForm(Constant.CLASSIC_MODE,false,false,-1,null);
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

        private void button43_Click(object sender, EventArgs e) {
            MessageBox.Show("我是超级宇宙无敌大炸弹！嗷呜~  凸(艹皿艹 )");
        }

        private void challengeBtn_Click(object sender, EventArgs e) {
            ChooseHurdleForm chf = new ChooseHurdleForm();
            chf.Show();
            this.Hide();
        }

        private void helpBtn_MouseEnter(object sender, EventArgs e) {
            Button btn = (Button)sender;
            btn.FlatAppearance.BorderColor = Constant.BorderColor1;
            btn.ForeColor = Constant.TextColor1;
        }

        private void helpBtn_MouseLeave(object sender, EventArgs e) {
            Button btn = (Button)sender;
            btn.FlatAppearance.BorderColor = Constant.BorderColor0;
            btn.ForeColor = Constant.TextColor0;
        }

        private void customBtn_Click(object sender, EventArgs e) {
            CostomForm cf = new CostomForm();
            cf.Show();
            this.Hide();
        }

        private void fireBtn_Click(object sender, EventArgs e) {
            GameForm gf = new GameForm(Constant.FIRE_MODE, true, true, -1, null);
            gf.Show();
            this.Hide();
        }
        
    }
}
