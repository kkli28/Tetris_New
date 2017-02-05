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
    public partial class CostomForm : Form {
        int[] states;                           //标记按钮状态
        Color color0;                        //未选中时边框颜色
        Color color1;                        //选中时边框颜色
        bool boundaryAcc;              //是否跨越边界
        bool haveBomb;                  //是否启用炸弹

        bool startBtnClick;               //是否点击了开始按钮
        public CostomForm() {
            InitializeComponent();

            states = new int[Constant.MAX_INDEX + 1];
            for (int i = 0; i < Constant.MAX_INDEX + 1; ++i)
                states[i] = Constant.BUTTON_STATE0;

            color0 = Color.FromArgb(224, 224, 224);
            color1 = Color.Lime;
            boundaryAcc = Constant.NO_BOUNDARY_ACC;
            haveBomb = Constant.NO_BOMB;

            boundaryAccCB.SelectedIndex = 0;
            bombCB.SelectedIndex = 0;
            startBtnClick = false;
        }
        
        private void CostomForm_Load(object sender, EventArgs e) {

        }

        private void button0_Click(object sender, EventArgs e) {
            Button btn = (Button)sender;
            int index = -1;
            for (int i = 0; i < Constant.MAX_INDEX + 1; ++i) {
                if (btn.Name == "button" + i.ToString()) index = i;
            }
            
            if (states[index] == Constant.BUTTON_STATE0) {
                btn.BackColor = color1;
                states[index] = Constant.BUTTON_STATE1;
            }else {
                btn.BackColor = color0;
                states[index] = Constant.BUTTON_STATE0;
            }
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

        private void CostomForm_FormClosed(object sender, FormClosedEventArgs e) {
            if (!startBtnClick) MenuForm.thisForm.Show();
        }

        private void resetBtn_Click(object sender, EventArgs e) {
            for (int i = 0; i < Constant.MAX_INDEX + 1; ++i)
                states[i] = Constant.BUTTON_STATE0;
            
            foreach (Control c in this.Controls) {
                if(c is Button) {
                    try {
                        int index = Int32.Parse(c.Name[6].ToString());
                        c.BackColor = color0;
                    } catch (Exception) {
                        continue;
                    }
                }
            }

            int maxIndex = 6 * (Constant.MAX_Y + 1);
            foreach(Control c in this.Controls) {
                if(c is Button) {
                    for(int i = 0; i < maxIndex; ++i) {
                        if (c.Name == "button" + i.ToString()) c.BackColor = Constant.cantClickBtnColor;
                    }
                }
            }
        }

        private void startBtn_Click(object sender, EventArgs e) {
            startBtnClick = true;
            GameForm gf = new GameForm(Constant.CUSTOM_MODE, boundaryAcc, haveBomb, -1, states);
            gf.Show();
            this.Close();
        }
    }
}
