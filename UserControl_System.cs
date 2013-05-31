using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HazeronMapper
{
    public partial class UserControl_System : UserControl
    {
        Point dragOffset;
        SystemObj system;
        bool showname = true;
        HazMap parenthazmapform;
        bool showNotes = false;



        public UserControl_System(SystemObj system, HazMap parenthazmapform)
        {
            InitializeComponent();
            this.system = system;
            this.Location = system.drawloc;
            this.parenthazmapform = parenthazmapform;
            this.textBox_sysnotes.Text = system.notes;
            label_SysName.Text = system.name;
            if (showname)
            {
                label_SysName.Enabled = true;
            }
            else
            {
                label_SysName.Enabled = false;
            }

        }

        private void shownotes()
        {
            if (this.showNotes)
            {
                textBox_sysnotes.Visible = false;
                //this.Size.Height = textBox_sysnotes.Size.Height + 10;
                //Size.Height = 10;
                this.showNotes = false;
            }
            else
            {
                textBox_sysnotes.Visible = true;
                this.showNotes = true;
                //this.Size.Height = 40;
                this.Size = new Size(this.Size.Width, this.textBox_sysnotes.Height + this.label_SysName.Height);
            }
        }

 

        private void UserControl_System_DoubleClick(object sender, EventArgs e)
        {

        }

        private void UserControl_System_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragOffset = this.PointToScreen(e.Location);
                var formLocation = this.Location;
                dragOffset.X -= formLocation.X;
                dragOffset.Y -= formLocation.Y;
            }

        }

        private void UserControl_System_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point newLocation = this.PointToScreen(e.Location);

                newLocation.X -= dragOffset.X;
                newLocation.Y -= dragOffset.Y;

                this.Location = newLocation;
                this.system.drawloc = newLocation;
                parenthazmapform.refreshlines();
                
            }
        }

        private void UserControl_System_MouseUp(object sender, MouseEventArgs e)
        {
            parenthazmapform.refreshlines();
        }

        private void label_SysName_Click(object sender, EventArgs e)
        {
            shownotes();
        }

        private void textBox_sysnotes_TextChanged(object sender, EventArgs e)
        {
            string thestring = this.textBox_sysnotes.Text;
            Font thefont = this.textBox_sysnotes.Font;
            
            Size size = TextRenderer.MeasureText(thestring, thefont);
            
            textBox_sysnotes.Size = size;
            this.Size = new Size(this.Size.Width, this.textBox_sysnotes.Height + this.label_SysName.Height);            
            system.notes = thestring;
        }

    }
}
