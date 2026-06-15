namespace Flappy_Copter
{
    partial class GameOver
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btn_menu = new Button();
            btn_tryAgain = new Button();
            Score = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 36.288F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(52, 42);
            label1.Name = "label1";
            label1.Size = new Size(385, 79);
            label1.TabIndex = 7;
            label1.Text = "GAME OVER";
            // 
            // btn_menu
            // 
            btn_menu.BackColor = Color.Orange;
            btn_menu.Font = new Font("Showcard Gothic", 12.096F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_menu.Location = new Point(112, 279);
            btn_menu.Name = "btn_menu";
            btn_menu.Size = new Size(234, 47);
            btn_menu.TabIndex = 5;
            btn_menu.Text = "MAIN MENU";
            btn_menu.UseVisualStyleBackColor = false;
            // 
            // btn_tryAgain
            // 
            btn_tryAgain.BackColor = Color.SpringGreen;
            btn_tryAgain.Font = new Font("Showcard Gothic", 12.096F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_tryAgain.Location = new Point(112, 213);
            btn_tryAgain.Name = "btn_tryAgain";
            btn_tryAgain.Padding = new Padding(3);
            btn_tryAgain.Size = new Size(234, 45);
            btn_tryAgain.TabIndex = 4;
            btn_tryAgain.Text = "TRY AGAIN";
            btn_tryAgain.UseVisualStyleBackColor = false;
            // 
            // Score
            // 
            Score.AutoSize = true;
            Score.Font = new Font("Showcard Gothic", 17.855999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Score.ForeColor = Color.FromArgb(128, 64, 64);
            Score.Location = new Point(154, 152);
            Score.Name = "Score";
            Score.Size = new Size(155, 38);
            Score.TabIndex = 8;
            Score.Text = "Score: 0";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FloralWhite;
            panel1.Location = new Point(65, 134);
            panel1.Name = "panel1";
            panel1.Size = new Size(360, 5);
            panel1.TabIndex = 9;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top;
            panel2.BackColor = Color.SaddleBrown;
            panel2.Location = new Point(-17, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(517, 36);
            panel2.TabIndex = 10;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Bottom;
            panel3.BackColor = Color.SaddleBrown;
            panel3.Location = new Point(-30, 347);
            panel3.Name = "panel3";
            panel3.Size = new Size(530, 36);
            panel3.TabIndex = 11;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FloralWhite;
            panel4.Location = new Point(65, 202);
            panel4.Name = "panel4";
            panel4.Size = new Size(360, 5);
            panel4.TabIndex = 12;
            // 
            // GameOver
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gold;
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(Score);
            Controls.Add(label1);
            Controls.Add(btn_menu);
            Controls.Add(btn_tryAgain);
            Name = "GameOver";
            Size = new Size(475, 383);
            Load += GameOver_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        public Button btn_menu;
        public Button btn_tryAgain;
        public Label Score;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
    }
}
