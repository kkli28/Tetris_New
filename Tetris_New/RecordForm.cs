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
    public partial class RecordForm : Form {
        public RecordForm() {
            InitializeComponent();

            string[] records = new string[3] { "0", "0", "0" };
            int value = 0;
            try {
                records = File.ReadAllLines("record.txt");
                for(int i = 0; i < 3; ++i) {
                    Int32.TryParse(records[i], out value);
                }
            }catch(Exception e) {
                File.WriteAllLines("record.txt", new string[] { "0", "0", "0" });
                MessageBox.Show("游戏记录文件损坏，已重置所有记录！");
            }

            classicModeLB.Text = records[0];
            challengeModeLB.Text = records[1];
            fireModeLB.Text = records[2];
        }

        private void RecordForm_Load(object sender, EventArgs e) {

        }

        private void startBtn_Click(object sender, EventArgs e) {
            DialogResult result = MessageBox.Show("确定重置所有记录？", "提示",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes) {
                File.WriteAllLines("record.txt", new string[] { "0", "0", "0" });
                classicModeLB.Text = "0";
                challengeModeLB.Text = "0";
                fireModeLB.Text = "0";
            }
        }
    }
}
