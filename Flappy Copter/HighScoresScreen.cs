using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Copter
{
    public partial class HighScoresScreen : UserControl
    {
        private const string ScoresFilePath = "scores.txt";
        public HighScoresScreen()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Visible = false;

        }

        private void HighScoresScreen_Load(object sender, EventArgs e)
        {
            

            var lines = File.ReadAllLines(ScoresFilePath);
            foreach (var line in lines)
            {
                var parts = line.Split('/');
                var name = parts[0];
                var val = int.Parse(parts[1]);
                if (name == "EASY") easy_lbl.Text = val.ToString();
                else if (name == "MEDIUM") med_lbl.Text = val.ToString();
                else if (name == "HARD") hard_lbl.Text = val.ToString();
            }
            

        }
    }
}
