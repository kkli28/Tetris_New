namespace Tetris_New {
    partial class GameOverForm {
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.newRecordLB = new System.Windows.Forms.Label();
            this.newRecordBtn = new System.Windows.Forms.Button();
            this.recordLB = new System.Windows.Forms.Label();
            this.scoreLB = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.gameModeLB = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(53, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "Game Over";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(87, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "记录 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(87, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = "分数 :";
            // 
            // newRecordLB
            // 
            this.newRecordLB.AutoSize = true;
            this.newRecordLB.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.newRecordLB.ForeColor = System.Drawing.Color.Red;
            this.newRecordLB.Location = new System.Drawing.Point(116, 304);
            this.newRecordLB.Name = "newRecordLB";
            this.newRecordLB.Size = new System.Drawing.Size(58, 21);
            this.newRecordLB.TabIndex = 3;
            this.newRecordLB.Text = "新记录";
            this.newRecordLB.Visible = false;
            // 
            // newRecordBtn
            // 
            this.newRecordBtn.BackColor = System.Drawing.Color.White;
            this.newRecordBtn.Enabled = false;
            this.newRecordBtn.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.newRecordBtn.FlatAppearance.BorderSize = 2;
            this.newRecordBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newRecordBtn.Location = new System.Drawing.Point(103, 301);
            this.newRecordBtn.Name = "newRecordBtn";
            this.newRecordBtn.Size = new System.Drawing.Size(84, 28);
            this.newRecordBtn.TabIndex = 5;
            this.newRecordBtn.UseVisualStyleBackColor = false;
            this.newRecordBtn.Visible = false;
            // 
            // recordLB
            // 
            this.recordLB.AutoSize = true;
            this.recordLB.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.recordLB.ForeColor = System.Drawing.Color.Blue;
            this.recordLB.Location = new System.Drawing.Point(168, 158);
            this.recordLB.Name = "recordLB";
            this.recordLB.Size = new System.Drawing.Size(24, 28);
            this.recordLB.TabIndex = 6;
            this.recordLB.Text = "0";
            // 
            // scoreLB
            // 
            this.scoreLB.AutoSize = true;
            this.scoreLB.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.scoreLB.ForeColor = System.Drawing.Color.Blue;
            this.scoreLB.Location = new System.Drawing.Point(168, 208);
            this.scoreLB.Name = "scoreLB";
            this.scoreLB.Size = new System.Drawing.Size(24, 28);
            this.scoreLB.TabIndex = 7;
            this.scoreLB.Text = "0";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label.ForeColor = System.Drawing.Color.Blue;
            this.label.Location = new System.Drawing.Point(45, 108);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(107, 28);
            this.label.TabIndex = 8;
            this.label.Text = "游戏模式 :";
            // 
            // gameModeLB
            // 
            this.gameModeLB.AutoSize = true;
            this.gameModeLB.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gameModeLB.ForeColor = System.Drawing.Color.Blue;
            this.gameModeLB.Location = new System.Drawing.Point(158, 108);
            this.gameModeLB.Name = "gameModeLB";
            this.gameModeLB.Size = new System.Drawing.Size(96, 28);
            this.gameModeLB.TabIndex = 9;
            this.gameModeLB.Text = "经典模式";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Enabled = false;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(48, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 60);
            this.button1.TabIndex = 10;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // GameOverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(310, 361);
            this.Controls.Add(this.gameModeLB);
            this.Controls.Add(this.label);
            this.Controls.Add(this.scoreLB);
            this.Controls.Add(this.recordLB);
            this.Controls.Add(this.newRecordLB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newRecordBtn);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "GameOverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "游戏结束";
            this.Load += new System.EventHandler(this.GameOverForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label newRecordLB;
        private System.Windows.Forms.Button newRecordBtn;
        private System.Windows.Forms.Label recordLB;
        private System.Windows.Forms.Label scoreLB;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label gameModeLB;
        private System.Windows.Forms.Button button1;
    }
}