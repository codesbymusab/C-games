using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Copter.models
{
    internal class Heart : PictureBox
    {
        [ToolboxItem(true)]
        public Heart()
        {

            this.DoubleBuffered = true;
            this.Anchor = AnchorStyles.Right;
            this.BackColor = Color.Transparent;
            this.BackgroundImage = Properties.Resources.health;
            this.BackgroundImageLayout = ImageLayout.Stretch;          
            this.Size = new Size(40, 40);
            this.TabIndex = 0;
            this.TabStop = false;


        }


    }
}
