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
    public partial class HelpForm : Form {
        public HelpForm() {
            InitializeComponent();
        }

        private void HelpForm_Load(object sender, EventArgs e) {

        }

        private void label3_MouseEnter(object sender, EventArgs e) {
            Label label = (Label)sender;
            label.ForeColor = Color.Blue;
        }

        private void label3_MouseLeave(object sender, EventArgs e) {
            Label label = (Label)sender;
            label.ForeColor = Color.FromArgb(224,224,224);
        }
    }
}
