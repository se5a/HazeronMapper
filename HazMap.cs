using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace HazeronMapper
{

    public partial class HazMap : Form
    {
        Galaxy galaxy = new Galaxy();

        Canvasdata canvasdata;

        List<Point> points = new List<Point>();

        Dictionary<string, SystemObj> mappedsystems = new Dictionary<string, SystemObj>();
        List<WormHoleObj> mappedwormholes = new List<WormHoleObj>();
        List<SystemObj> systemPlaced = new List<SystemObj>();
        List<UserControl_System> sys_usrCtrls = new List<UserControl_System>();
        Point canvasloc;

        UserControl_System syslink_usrctrl;

        bool leftmousegrab = false;
        Point mousedownOffset;

        public HazMap()
        {
            InitializeComponent();
            canvasloc = new Point((pictureBox1.Width / 2) * -1, pictureBox1.Height / 2);
            canvasdata = new Canvasdata(1, pictureBox1.Width, pictureBox1.Height, canvasloc);            
        }

        public void canvasupdate()
        {           
            canvasdata = new Canvasdata(1, pictureBox1.Width, pictureBox1.Height, canvasloc); 
        }
        public void refreshlines()
        {
            //pictureBox1.Invalidate();
            if (mappedsystems.Count > 0)
            {
                pictureBox1.Paint += new PaintEventHandler(this.drawlines);
                pictureBox1.Invalidate();
            }
        }
        public void refresh()
        { pictureBox1.Invalidate(); }

        public Galaxy thegalaxy
        {
            get { return this.galaxy; }
        }

        private void toolStripButton_drawmap_Click(object sender, EventArgs e)
        {           
            
            //Point startmappoint = new Point(0,200);
            canvasupdate();

            if (galaxy.sectors_dictionary.Count > 0)
            {
                selectSystemPrompt promptwindow = new selectSystemPrompt();
                selectsys_returnObj retobj = promptwindow.selectSystemPromptdlg(galaxy);
            
                SystemObj startSys = retobj.system;
                int depth = retobj.depth;
                bool resetlocs = retobj.resetloc;

                foreach (UserControl_System control in sys_usrCtrls)//clear usercontrols
                { 
                    pictureBox1.Controls.Remove(control);
                    control.Dispose();
                }

                if (resetlocs)
                {
                    startSys.maploc = new Point(0,200);
                }
                
                sys_usrCtrls = new List<UserControl_System>();  //new usercontrol list
                mappedsystems = new Dictionary<string, SystemObj>(); //new mappedsystems dictionary
                mappedwormholes = new List<WormHoleObj>();
                systemPlaced = new List<SystemObj>();

                //startSys.maploc = startmappoint; //set first system location
                systemPlaced.Add(startSys);
                syslink_usrctrl = new UserControl_System(startSys, this, canvasdata); //first system usercontrol
                sys_usrCtrls.Add(syslink_usrctrl); //addthe user control to the usercontrol list
                 
                mappedsystems.Add(startSys.location, startSys);//add the system to the mappedsystems dictionary

                pictureBox1.Controls.Add(syslink_usrctrl); //add teh control to the picturebox.
                Point loc = syslink_usrctrl.Location;
                placesystems(startSys, 180, depth, resetlocs);
                
               
            }    
            refreshlines();        
        }


        private void placesystems(SystemObj parentsystem, int angle, int depth, bool resetlocs)
        {                       
            if (depth > 0)
            {
                //Dictionary<string, SystemObj> systemslinked = new Dictionary<string, SystemObj>();
                List<SystemObj> systemslinked = new List<SystemObj>();
                int icondistance = 100;
               // Point parentMaploc = parentsystem.maploc;
                bool resetthisloc = false;

                foreach (WormHoleObj wh in parentsystem.Wormholes) //foreach wormhole from this system.
                {
                    
                    SystemObj linkedsys = wh.getlink(parentsystem);
                    
                    //if (!systemslinked.Keys.Contains(linkedsys.location))
                    if (!systemslinked.Contains(linkedsys))
                    {
                        //systemslinked.Add(linkedsys.location, linkedsys);
                        systemslinked.Add(linkedsys);
                    }

                    if (!mappedwormholes.Contains(wh))
                    {
                        mappedwormholes.Add(wh);
                    }
                    if (!mappedsystems.Keys.Contains(linkedsys.location))
                    {
                                              
                        syslink_usrctrl = new UserControl_System(linkedsys, this, canvasdata);
                        sys_usrCtrls.Add(syslink_usrctrl);
                        mappedsystems.Add(linkedsys.location, linkedsys);
                        pictureBox1.Controls.Add(syslink_usrctrl);                       
                    }                             
                }
                //foreach (KeyValuePair<string, SystemObj> kvp_system in systemslinked)
                foreach (SystemObj linkedsys in systemslinked) //this list should not include the parent...
                {
                    if (!systemPlaced.Contains(linkedsys))
                    {
                        //SystemObj linkedsys = kvp_system.Value;
                        Point thismaploc = new Point();
                        
                        //Point dloc = mappedsystems[linkedsys.location].maploc;
                        //if (dloc.X < 1 || dloc.Y < 1) { resetthisloc = true; }
                        int linkcount = systemslinked.Count;
                        //if (linkcount > 1) { linkcount -= 1; }
                        if (resetthisloc || resetlocs) //reset.
                        {
                            thismaploc = parentsystem.maploc;
                            angle += (360 / linkcount);
                            thismaploc = sides_ab(icondistance, angle, false);
                            thismaploc.X += parentsystem.maploc.X;
                            thismaploc.Y += parentsystem.maploc.Y;
                            linkedsys.maploc = thismaploc;
                            //

                        }
                        //mappedsystems[linkedsys.location].maploc = thismaploc;
                        
                        systemPlaced.Add(linkedsys);
                    }
                    //placesystems(mappedsystems[linkedsys.location], angle, depth - 1, resetlocs);
                    placesystems(linkedsys, angle, depth - 1, resetlocs);
 
                }
            }
        }

        public void drawlines(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //if (mappedsystems.Count > 1)
            foreach (UserControl_System sysctrl in sys_usrCtrls)
            {
                sysctrl.location(canvasdata);
            }
            {
                foreach (WormHoleObj wh in mappedwormholes)
                {
                    Pen linkline = new Pen(Color.Blue, 2);
                    List<SystemObj> systemslinked = new List<SystemObj>(wh.getlinks);

                    //mappedsystems[systemslinked[0].location].usrctrl.updateloc(canvasdata);
                    //mappedsystems[systemslinked[1].location].usrctrl.updateloc(canvasdata);

                    //Point pt1 = canvasdata.canvasLocation(mappedsystems[systemslinked[0].location].maploc);
                    Point pt1 = canvasdata.canvasLocation(systemslinked[0].maploc);
                   // Point pt2 = canvasdata.canvasLocation(mappedsystems[systemslinked[1].location].maploc);
                    Point pt2 = canvasdata.canvasLocation(systemslinked[1].maploc);

                    Point pt3 = canvasdata.zeroedpoint(pt1, pt2);
                    if (pt3.Y == 0)
                    {
                        pt3.Y = 1;
                    }
                    int offangle = (int)Math.Atan(pt3.X / pt3.Y);
                    

                    if (wh.polarity)
                    {
                        offangle += 90;
                        int offset1 = 4;
                        pt3 = sides_ab(offset1, offangle);
                        linkline.Color = Color.Red;
                        pt1 = canvasdata.add(pt1, pt3);
                        pt2 = canvasdata.add(pt2, pt3);
                    }
                    else
                    {
                        offangle -= 90;
                        int offset1 = 4;
                        pt3 = sides_ab(offset1, offangle);
                        linkline.Color = Color.Blue;
                        pt1 = canvasdata.add(pt1, pt3);
                        pt2 = canvasdata.add(pt2, pt3);
                    }

                    g.DrawLine(linkline, pt1, pt2);
                }
            }
        }



        public Point sides_ab(int hypotinuse, int angle_A, bool radians = true )
        {   
            double angle_a = angle_A;
            if (!radians)
                { angle_a = angle_A * Math.PI / 180; }
            double side_a = 0;
            double side_b = 0;

            side_b = Math.Sin(angle_a) * hypotinuse;
            side_a = Math.Cos(angle_a) * hypotinuse;

            Point sides = new Point((int)side_b, (int)side_a);
            return sides;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = savefilediag(".hzm");           
            filehandling.SerializeObject(galaxy, filename);
        }

        private string savefilediag(string extension)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = extension;
            string filename = null;
            DialogResult ofd_result = sfd.ShowDialog();
            if (ofd_result == DialogResult.OK)
            {
                filename = sfd.FileName;
            }
            return filename;
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = openfilediag(".hzm");
            if (filename != null)
            {
                galaxy = (Galaxy)filehandling.DeserializeObject(filename);
            }
        }

        private void clipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string hazscan = Clipboard.GetText();
            string pastemesage = Readscan.readscan(hazscan, galaxy);
            if (pastemesage != null)
            {
                this.toolStripStatusLabel1.Text = pastemesage;
            }
            else
            {
                this.toolStripStatusLabel1.Text = "Paste failed";
            }
        }

        private void textFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = openfilediag(".txt");
            string scanfile = filename;
            try
            {
                string scantext = File.ReadAllText(scanfile);
                Readscan.readscan(scantext, galaxy);
            }
            catch (IOException)
            {
            }
        }

        private string openfilediag(string extension)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "*" + extension;
            ofd.DefaultExt = extension;
            string filename = null;
            DialogResult ofd_result = ofd.ShowDialog();
            if (ofd_result == DialogResult.OK)
            {
                filename = ofd.FileName;
            }
            return filename;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                leftmousegrab = true;
                mousedownOffset = e.Location;
                //this.Cursor = Cursors. fuuuuu, no good cursors for this in default. 
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {          
            canvasupdate();
            pictureBox1.Invalidate();
            leftmousegrab = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>this could be cleaned up, instead of getting delta from mousedownoffset and new mousemove loc
        /// get it from the canvaslocation itself and the mouse move loc.</remarks>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftmousegrab)
            {
                canvasupdate();
                pictureBox1.Invalidate();
                Point mousemove = e.Location;
                Point offset = new Point();
                offset.X = mousedownOffset.X - mousemove.X;
                offset.Y = mousedownOffset.Y - mousemove.Y;
                mousedownOffset = mousemove;
                canvasloc.X += offset.X;
                canvasloc.Y -= offset.Y;

            }
        }

        private void HazMap_SizeChanged(object sender, EventArgs e)
        {
            int offsetx = canvasdata.width / 2 - pictureBox1.Width / 2;
            int offsety = canvasdata.height / 2 - pictureBox1.Height / 2;
            canvasloc.X += offsetx;
            canvasloc.Y -= offsety;
            canvasupdate();
            pictureBox1.Invalidate();
        } 
    }

    public class selectsys_returnObj
    {
        public SystemObj system;
        public int depth;
        public bool resetloc;
        public selectsys_returnObj(SystemObj system, int depth, bool resetloc)
        {
            this.system = system;
            this.depth = depth;
            this.resetloc = resetloc;
        }
    }
}
