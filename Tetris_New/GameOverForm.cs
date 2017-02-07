using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tetris_New {
    public partial class GameOverForm : Form {
        public GameOverForm(int gameMode, int score,int record,string[] records) {
            InitializeComponent();
            
            if (score > record) {
                record = score;
                newRecordLB.Visible = true;
                newRecordBtn.Visible = true;
                try {
                    records[gameMode] = record.ToString();
                    File.WriteAllLines("record.txt", records);
                }catch(Exception e) {
                    File.WriteAllLines("record.txt", new string[3] { "0", "0", "0" });
                    MessageBox.Show("记录文件损坏，已重置所有记录！");
                }
            }
            string str = "";
            if (gameMode == Constant.CLASSIC_MODE) str = "经典模式";
            else if (gameMode == Constant.CHALLENGE_MODE) str = "挑战模式";
            else if (gameMode == Constant.FIRE_MODE) str = "无限火力";
            else str = "自定义模式";
            scoreLB.Text = score.ToString();
            recordLB.Text = record.ToString();
            gameModeLB.Text = str;
        }

        private void GameOverForm_Load(object sender, EventArgs e) {

        }
    }
}
