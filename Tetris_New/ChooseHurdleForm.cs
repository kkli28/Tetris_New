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
    public partial class ChooseHurdleForm : Form {
        int pbIndex;            //用于标记之前选中的PictureBox
        bool selectPB;            //是否选择了一个PictureBox

        bool startBtnClick;                 //开始按钮是否按下。当界面关闭时，如果按下则不回到菜单界面
        bool boundaryAcc;                //边界跨越
        bool haveBomb;                    //是否有炸弹

        public ChooseHurdleForm() {
            InitializeComponent();

            boundaryAccCB.SelectedIndex = 0;
            bombCB.SelectedIndex = 0;

            pbIndex = -1;
            selectPB = false;
            startBtnClick = false;
            boundaryAcc = Constant.NO_BOUNDARY_ACC;
            
        }
        
        private void ChooseHurdleForm_Load(object sender, EventArgs e) {

        }

        private void hurdle0PB_Click(object sender, EventArgs e) {
            PictureBox pb = (PictureBox)sender;
            string name = pb.Name;

            if (pbIndex != -1) {                    //清除之前点击的PictureBox边框（Button）的颜色
                foreach (Control c in this.Controls) {   //清除边框颜色
                    if (c is Button) {
                        if ("hurdle" + pbIndex == "hurdle" + c.Name[6]) c.BackColor = Constant.BorderColor0;
                    }
                }
            }

            int index = Int32.Parse(name[6].ToString());
            if (pbIndex == index) {                         //选择了之前点击的PictureBox，则取消选中
                selectPB = false;
                pbIndex = -1;
                startBtn.Enabled = false;
                startBtn.FlatAppearance.BorderColor = Constant.BorderColor0;
                startBtn.ForeColor = Constant.BorderColor0;
                
                return;                             //此处返回
            } else {
                selectPB = true;
                pbIndex = index;
                startBtn.Enabled = true;

                startBtn.FlatAppearance.BorderColor = Constant.BorderColor1;
                startBtn.ForeColor = Constant.BorderColor1;

                string btnName = "hurdle" + name[6];
                foreach (Control c in this.Controls) {
                    if (c is Button) {
                        if (btnName == "hurdle" + c.Name[6]) c.BackColor = Constant.BorderColor1;
                    }
                }
                
            }
            
        }

        private void startBtn_Click(object sender, EventArgs e) {
            startBtnClick = true;

            GameForm gf = new GameForm(Constant.CHALLENGE_MODE, boundaryAcc, haveBomb,pbIndex,null); //关卡信息
            gf.Show();
            this.Close();
        }

        private void boundaryAccCB_SelectedIndexChanged(object sender, EventArgs e) {
            switch (boundaryAccCB.SelectedIndex) {
                case 0:
                    boundaryAcc = Constant.NO_BOUNDARY_ACC;
                    break;
                default:
                    boundaryAcc = Constant.BOUNDARY_ACC;
                    break;
            }
            
        }

        private void bombCB_SelectedIndexChanged(object sender, EventArgs e) {
            switch (bombCB.SelectedIndex) {
                case 0:
                    haveBomb = Constant.NO_BOMB;
                    break;
                default:
                    haveBomb = Constant.HAVE_BOMB;
                    break;
            }
        }

        private void ChooseHurdleForm_FormClosed(object sender, FormClosedEventArgs e) {
            if (!startBtnClick) MenuForm.thisForm.Show();
        }

        private void ChooseHurdleForm_Load_1(object sender, EventArgs e) {

        }
    }
}
