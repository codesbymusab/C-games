namespace Flappy_Copter
{
    partial class DifficulityScreen
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
            Choose = new Label();
            label1 = new Label();
            btn_Medium = new Button();
            btn_Easy = new Button();
            btn_Hard = new Button();
            panel3 = new Panel();
            panel1 = new Panel();
            panel2 = new Panel();
            SuspendLayout();
            // 
            // Choose
            // 
            Choose.AutoSize = true;
            Choose.Font = new Font("Showcard Gothic", 17.855999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Choose.ForeColor = Color.WhiteSmoke;
            Choose.Location = new Point(162, 146);
            Choose.Name = "Choose";
            Choose.Size = new Size(155, 38);
            Choose.TabIndex = 12;
            Choose.Text = "Choose:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Showcard Gothic", 36.288F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(32, 43);
            label1.Name = "label1";
            label1.Size = new Size(419, 79);
            label1.TabIndex = 11;
            label1.Text = "Difficulity";
            // 
            // btn_Medium
            // 
            btn_Medium.BackColor = Color.Orange;
            btn_Medium.Font = new Font("Showcard Gothic", 12.096F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Medium.Location = new Point(118, 284);
            btn_Medium.Name = "btn_Medium";
            btn_Medium.Size = new Size(234, 47);
            btn_Medium.TabIndex = 10;
            btn_Medium.Text = "MEDIUM";
            btn_Medium.UseVisualStyleBackColor = false;
            // 
            // btn_Easy
            // 
            btn_Easy.BackColor = Color.SpringGreen;
            btn_Easy.Font = new Font("Showcard Gothic", 12.096F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Easy.Location = new Point(118, 211);
            btn_Easy.Name = "btn_Easy";
            btn_Easy.Padding = new Padding(3);
            btn_Easy.Size = new Size(234, 45);
            btn_Easy.TabIndex = 9;
            btn_Easy.Text = "EASY";
            btn_Easy.UseVisualStyleBackColor = false;
            // 
            // btn_Hard
            // 
            btn_Hard.BackColor = Color.Red;
            btn_Hard.Font = new Font("Showcard Gothic", 12.096F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Hard.Location = new Point(118, 363);
            btn_Hard.Name = "btn_Hard";
            btn_Hard.Size = new Size(234, 47);
            btn_Hard.TabIndex = 13;
            btn_Hard.Text = "HARD";
            btn_Hard.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Gold;
            panel3.Location = new Point(-2, 427);
            panel3.Name = "panel3";
            panel3.Size = new Size(474, 51);
            panel3.TabIndex = 14;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Gold;
            panel1.ForeColor = Color.Snow;
            panel1.Location = new Point(-2, -2);
            panel1.Name = "panel1";
            panel1.Size = new Size(484, 41);
            panel1.TabIndex = 15;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FloralWhite;
            panel2.Location = new Point(83, 125);
            panel2.Name = "panel2";
            panel2.Size = new Size(316, 5);
            panel2.TabIndex = 16;
            // 
            // DifficulityScreen
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SaddleBrown;
            BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(btn_Hard);
            Controls.Add(Choose);
            Controls.Add(label1);
            Controls.Add(btn_Medium);
            Controls.Add(btn_Easy);
            Name = "DifficulityScreen";
            Size = new Size(470, 476);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Label Choose;
        private Label label1;
        public Button btn_Medium;
        public Button btn_Easy;
        public Button btn_Hard;
        private Panel panel3;
        private Panel panel1;
        private Panel panel2;
    }
}
