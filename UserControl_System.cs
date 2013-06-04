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
        Canvasdata canvasdata;
        Point offset = new Point(10, 10);
        //Point warplinesloc;
        bool leftmousegrab = false;

        public UserControl_System(SystemObj system, HazMap parenthazmapform, Canvasdata canvasdata)
        {
            InitializeComponent();
            this.system = system;
            //updateloc(canvasdata);
            this.parenthazmapform = parenthazmapform;
            this.textBox_sysnotes.Text = system.notes;
            this.canvasdata = canvasdata;
            //system.usrctrl = this;
            //this.Location = canvasdata.sub(canvasdata.canvasLocation(system.maploc), offset);
            location();
            
            this.Size = this.MinimumSize;
            setminsize();
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

        private void setminsize()
        {
            string thestring = this.label_SysName.Text;
            Font thefont = this.label_SysName.Font;
            
            Size size = TextRenderer.MeasureText(thestring, thefont);
            size.Width += this.button1.Width + 2;
            size.Height = this.button1.Height + 3;
            this.MinimumSize = size;
        }

        private void location()
        {
            this.Location = canvasdata.sub(canvasdata.canvasLocation(system.maploc), offset);
        }
        public void location(Canvasdata canvasdata)
        {
            this.canvasdata = canvasdata;
            location();
        }
      
        private void shownotes_switch()
        {
            if (this.showNotes)
            {
                textBox_sysnotes.Visible = false;
                this.showNotes = false;
                setminsize();
                this.Size = this.MinimumSize;
            }
            else
            {
                textBox_sysnotes.Visible = true;
                this.showNotes = true;
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
                leftmousegrab = true;
                dragOffset = this.PointToScreen(e.Location);
                var formLocation = this.Location;
                dragOffset.X -= formLocation.X += offset.X;
                dragOffset.Y -= formLocation.Y += offset.X;
            }
        }

        private void UserControl_System_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftmousegrab)
            {
                Point newloc = this.PointToScreen(e.Location);
                newloc = canvasdata.sub(newloc, dragOffset);
                this.system.maploc = canvasdata.reversecanvasLocation(newloc);
                location();
                //parenthazmapform.refreshlines();
                parenthazmapform.refresh();             
            }
        }

        private void UserControl_System_MouseUp(object sender, MouseEventArgs e)
        {
            leftmousegrab = false;
            //parenthazmapform.refreshlines();
            parenthazmapform.refresh();
        }

        private void label_SysName_Click(object sender, EventArgs e)
        {
            shownotes_switch();
        }

        private void textBox_sysnotes_TextChanged(object sender, EventArgs e)
        {
            string thestring = this.textBox_sysnotes.Text;
            Font thefont = this.textBox_sysnotes.Font;
            
            Size size = TextRenderer.MeasureText(thestring, thefont);
            
            textBox_sysnotes.Size = size;
            this.Size = new Size(this.textBox_sysnotes.Width + 2, this.textBox_sysnotes.Height + this.label_SysName.Height);            
            system.notes = thestring;
        }

    }
}
