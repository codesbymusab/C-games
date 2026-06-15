namespace Flappy_Copter
{
    partial class HighScoresScreen
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
            panel1 = new Panel();
            btn_back = new Button();
            panel2 = new Panel();
            easy_lbl = new Label();
            label5 = new Label();
            panel3 = new Panel();
            med_lbl = new Label();
            label6 = new Label();
            panel4 = new Panel();
            hard_lbl = new Label();
            label7 = new Label();
            label2 = new Label();
            label3 = new Label();
            panel7 = new Panel();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 36.288F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(38, 33);
            label1.Name = "label1";
            label1.Size = new Size(442, 79);
            label1.TabIndex = 0;
            label1.Text = "HIGH SCORES";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Snow;
            panel1.Location = new Point(38, 124);
            panel1.Name = "panel1";
            panel1.Size = new Size(454, 5);
            panel1.TabIndex = 1;
            // 
            // btn_back
            // 
            btn_back.BackColor = Color.GreenYellow;
            btn_back.Font = new Font("Showcard Gothic", 12.096F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_back.ForeColor = SystemColors.ActiveCaptionText;
            btn_back.Location = new Point(211, 520);
            btn_back.Name = "btn_back";
            btn_back.Size = new Size(110, 43);
            btn_back.TabIndex = 2;
            btn_back.Text = "BACK";
            btn_back.UseVisualStyleBackColor = false;
            btn_back.Click += btn_back_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Chocolate;
            panel2.Controls.Add(easy_lbl);
            panel2.Controls.Add(label5);
            panel2.Location = new Point(38, 212);
            panel2.Name = "panel2";
            panel2.Size = new Size(454, 83);
            panel2.TabIndex = 3;
            // 
            // easy_lbl
            // 
            easy_lbl.AutoSize = true;
            easy_lbl.BackColor = Color.Transparent;
            easy_lbl.Font = new Font("Showcard Gothic", 16.128F, FontStyle.Bold, GraphicsUnit.Point, 0);
            easy_lbl.ForeColor = Color.Gold;
            easy_lbl.Location = new Point(335, 25);
            easy_lbl.Name = "easy_lbl";
            easy_lbl.Size = new Size(64, 35);
            easy_lbl.TabIndex = 8;
            easy_lbl.Text = "360";
            easy_lbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Showcard Gothic", 16.128F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Gold;
            label5.Location = new Point(28, 25);
            label5.Name = "label5";
            label5.Size = new Size(85, 35);
            label5.TabIndex = 7;
            label5.Text = "EASY";
            // 
            // panel3
            // 
            panel3.BackColor = Color.Chocolate;
            panel3.Controls.Add(med_lbl);
            panel3.Controls.Add(label6);
            panel3.Location = new Point(38, 319);
            panel3.Name = "panel3";
            panel3.Size = new Size(454, 83);
            panel3.TabIndex = 4;
            panel3.Paint += panel3_Paint;
            // 
            // med_lbl
            // 
            med_lbl.AutoSize = true;
            med_lbl.BackColor = Color.Transparent;
            med_lbl.Font = new Font("Showcard Gothic", 16.128F, FontStyle.Bold, GraphicsUnit.Point, 0);
            med_lbl.ForeColor = Color.Gold;
            med_lbl.Location = new Point(337, 24);
            med_lbl.Name = "med_lbl";
            med_lbl.Size = new Size(62, 35);
            med_lbl.TabIndex = 9;
            med_lbl.Text = "230";
            med_lbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Showcard Gothic", 16.128F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Gold;
            label6.Location = new Point(3, 24);
            label6.Name = "label6";
            label6.Size = new Size(135, 35);
            label6.TabIndex = 8;
            label6.Text = "MEDIUM";
            // 
            // panel4
            // 
            panel4.BackColor = Color.Chocolate;
            panel4.Controls.Add(hard_lbl);
            panel4.Controls.Add(label7);
            panel4.Location = new Point(38, 425);
            panel4.Name = "panel4";
            panel4.Size = new Size(454, 83);
            panel4.TabIndex = 5;
            // 
            // hard_lbl
            // 
            hard_lbl.AutoSize = true;
            hard_lbl.BackColor = Color.Transparent;
            hard_lbl.Font = new Font("Showcard Gothic", 16.128F, FontStyle.Bold, GraphicsUnit.Point, 0);
            hard_lbl.ForeColor = Color.Gold;
            hard_lbl.Location = new Point(335, 20);
            hard_lbl.Name = "hard_lbl";
            hard_lbl.Size = new Size(64, 35);
            hard_lbl.TabIndex = 10;
            hard_lbl.Text = "100";
            hard_lbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Showcard Gothic", 16.128F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Gold;
            label7.Location = new Point(25, 20);
            label7.Name = "label7";
            label7.Size = new Size(98, 35);
            label7.TabIndex = 9;
            label7.Text = "HARD";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Showcard Gothic", 13.8239994F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Gold;
            label2.Location = new Point(66, 141);
            label2.Name = "label2";
            label2.Size = new Size(85, 30);
            label2.TabIndex = 6;
            label2.Text = "MODE";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Showcard Gothic", 13.8239994F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Gold;
            label3.Location = new Point(364, 141);
            label3.Name = "label3";
            label3.Size = new Size(90, 30);
            label3.TabIndex = 7;
            label3.Text = "SCORE";
            // 
            // panel7
            // 
            panel7.BackColor = Color.Snow;
            panel7.Location = new Point(41, 187);
            panel7.Name = "panel7";
            panel7.Size = new Size(454, 5);
            panel7.TabIndex = 2;
            // 
            // HighScoresScreen
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SaddleBrown;
            Controls.Add(panel7);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(btn_back);
            Controls.Add(panel1);
            Controls.Add(label1);
            Name = "HighScoresScreen";
            Size = new Size(521, 577);
            Load += HighScoresScreen_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        public Button btn_back;
        private Panel panel2;
        private Panel panel3;
        private Label label5;
        private Label label6;
        private Panel panel4;
        private Label label7;
        private Label label2;
        private Label label3;
        private Label easy_lbl;
        private Label med_lbl;
        private Label hard_lbl;
        private Panel panel7;
    }
}
