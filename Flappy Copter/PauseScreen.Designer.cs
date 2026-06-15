namespace Flappy_Copter
{
    partial class PauseScreen
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
            btn_resume = new Button();
            btn_sound = new Button();
            btn_music = new Button();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            SuspendLayout();
            // 
            // btn_resume
            // 
            btn_resume.BackColor = Color.SpringGreen;
            btn_resume.Font = new Font("Showcard Gothic", 12.096F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_resume.Location = new Point(124, 127);
            btn_resume.Name = "btn_resume";
            btn_resume.Padding = new Padding(3);
            btn_resume.Size = new Size(234, 45);
            btn_resume.TabIndex = 0;
            btn_resume.Text = "RESUME";
            btn_resume.UseVisualStyleBackColor = false;
            btn_resume.Click += btn_resume_Click;
            // 
            // btn_sound
            // 
            btn_sound.BackColor = Color.Orange;
            btn_sound.Font = new Font("Showcard Gothic", 12.096F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_sound.Location = new Point(124, 204);
            btn_sound.Name = "btn_sound";
            btn_sound.Size = new Size(234, 47);
            btn_sound.TabIndex = 1;
            btn_sound.Text = "TURN SOUND OFF";
            btn_sound.UseVisualStyleBackColor = false;
            // 
            // btn_music
            // 
            btn_music.BackColor = Color.Red;
            btn_music.Font = new Font("Showcard Gothic", 12.096F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_music.Location = new Point(124, 277);
            btn_music.Name = "btn_music";
            btn_music.Size = new Size(234, 46);
            btn_music.TabIndex = 2;
            btn_music.Text = "TURN MUSIC OFF";
            btn_music.UseVisualStyleBackColor = false;
            btn_music.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 36.288F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(112, 26);
            label1.Name = "label1";
            label1.Size = new Size(274, 79);
            label1.TabIndex = 3;
            label1.Text = "Paused";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FloralWhite;
            panel1.Location = new Point(87, 108);
            panel1.Name = "panel1";
            panel1.Size = new Size(316, 5);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.BackColor = Color.SaddleBrown;
            panel2.Location = new Point(-2, -2);
            panel2.Name = "panel2";
            panel2.Size = new Size(484, 36);
            panel2.TabIndex = 11;
            // 
            // panel3
            // 
            panel3.BackColor = Color.SaddleBrown;
            panel3.Location = new Point(-2, 340);
            panel3.Name = "panel3";
            panel3.Size = new Size(489, 36);
            panel3.TabIndex = 12;
            // 
            // PauseScreen
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gold;
            BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(btn_music);
            Controls.Add(btn_sound);
            Controls.Add(btn_resume);
            Name = "PauseScreen";
            Size = new Size(480, 374);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Button btn_resume;
        public Button btn_sound;
        public Button btn_music;
        private Label label1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
    }
}
