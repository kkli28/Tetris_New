namespace Tetris_New {
    partial class RecordForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.startBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.classicModeLB = new System.Windows.Forms.Label();
            this.challengeModeLB = new System.Windows.Forms.Label();
            this.fireModeLB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startBtn
            // 
            this.startBtn.BackColor = System.Drawing.Color.White;
            this.startBtn.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.startBtn.FlatAppearance.BorderSize = 2;
            this.startBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.startBtn.ForeColor = System.Drawing.Color.Red;
            this.startBtn.Location = new System.Drawing.Point(260, 273);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(72, 36);
            this.startBtn.TabIndex = 456;
            this.startBtn.Text = "重置";
            this.startBtn.UseVisualStyleBackColor = false;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(51, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 28);
            this.label1.TabIndex = 457;
            this.label1.Text = "经典模式 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(51, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 28);
            this.label2.TabIndex = 458;
            this.label2.Text = "挑战模式 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(51, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 28);
            this.label3.TabIndex = 459;
            this.label3.Text = "无限火力 :";
            // 
            // classicModeLB
            // 
            this.classicModeLB.AutoSize = true;
            this.classicModeLB.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.classicModeLB.ForeColor = System.Drawing.Color.Blue;
            this.classicModeLB.Location = new System.Drawing.Point(164, 45);
            this.classicModeLB.Name = "classicModeLB";
            this.classicModeLB.Size = new System.Drawing.Size(24, 28);
            this.classicModeLB.TabIndex = 460;
            this.classicModeLB.Text = "0";
            // 
            // challengeModeLB
            // 
            this.challengeModeLB.AutoSize = true;
            this.challengeModeLB.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.challengeModeLB.ForeColor = System.Drawing.Color.Blue;
            this.challengeModeLB.Location = new System.Drawing.Point(164, 115);
            this.challengeModeLB.Name = "challengeModeLB";
            this.challengeModeLB.Size = new System.Drawing.Size(24, 28);
            this.challengeModeLB.TabIndex = 461;
            this.challengeModeLB.Text = "0";
            // 
            // fireModeLB
            // 
            this.fireModeLB.AutoSize = true;
            this.fireModeLB.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fireModeLB.ForeColor = System.Drawing.Color.Blue;
            this.fireModeLB.Location = new System.Drawing.Point(164, 185);
            this.fireModeLB.Name = "fireModeLB";
            this.fireModeLB.Size = new System.Drawing.Size(24, 28);
            this.fireModeLB.TabIndex = 462;
            this.fireModeLB.Text = "0";
            // 
            // RecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(344, 321);
            this.Controls.Add(this.fireModeLB);
            this.Controls.Add(this.challengeModeLB);
            this.Controls.Add(this.classicModeLB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startBtn);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RecordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RecordForm";
            this.Load += new System.EventHandler(this.RecordForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label classicModeLB;
        private System.Windows.Forms.Label challengeModeLB;
        private System.Windows.Forms.Label fireModeLB;
    }
}