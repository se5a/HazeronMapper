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
        

        List<Point> points = new List<Point>();

        Dictionary<string, SystemObj> mappedsystems = new Dictionary<string, SystemObj>();
        List<WormHoleObj> mappedwormholes = new List<WormHoleObj>();
        
        public HazMap()
        {
            InitializeComponent();   
        }

        //public SystemObj startSystem
        //{
        //    set { this.startSys = value; }
        //}
        public Galaxy thegalaxy
        {
            get { return this.galaxy; }
        }

        private void textFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult ofd_result = ofd.ShowDialog();
            if (ofd_result == DialogResult.OK)
            {
                string scanfile = ofd.FileName;
                try
                {
                    string scantext = File.ReadAllText(scanfile);
                    //Readscan.readscan(scanfile, galaxy);
                    Readscan.readscan(scantext, galaxy);
                }
                catch (IOException)
                {
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {           
            
            Point startmappoint = new Point(pictureBox1.Size.Width / 2, pictureBox1.Size.Height / 2);
            //selectsys_returnObj retobj = selectSysPrompt.selectsystemprompt(galaxy);
            if (galaxy.sectors_dictionary.Count > 0)
            {
                selectSystemPrompt promptwindow = new selectSystemPrompt();
                selectsys_returnObj retobj = promptwindow.selectSystemPromptdlg(galaxy);
            
                SystemObj startSys = retobj.system;
                int depth = retobj.depth;
                bool resetlocs = retobj.resetloc;
            
                UserControl_System sys_usrctrl = new UserControl_System(startSys, this);
                sys_usrctrl.Location = startmappoint;
                pictureBox1.Invalidate();
                pictureBox1.Controls.Clear();
                pictureBox1.Controls.Add(sys_usrctrl);

                //SystemObj currentsystem = startSys;

                if (!mappedsystems.ContainsKey(startSys.location))
                {
                    mappedsystems.Add(startSys.location, startSys);
                }
                //mappedsystems = new Dictionary<string, SystemObj>(startSys.location, startSys);
                mappedsystems[startSys.location].drawloc = startmappoint;
                placesystems(mappedsystems[startSys.location], 0, depth, resetlocs);

                refreshlines();
            }
            
        }

        public void refreshlines()
        {
            pictureBox1.Invalidate();
            pictureBox1.Paint += new PaintEventHandler(this.drawlines);
        }

        private void placesystems(SystemObj system, int angle, int depth, bool resetlocs)
        {
            
            
            if (depth > 0)
            {
                Dictionary<string, SystemObj> systemslinked = new Dictionary<string, SystemObj>();
                int icondistance = 150;
                //int angle = 0;

                foreach (WormHoleObj wh in system.Wormholes) //foreach wormhole from this system.
                {
                    SystemObj linkedsys = wh.getlink(system);
                    if (!systemslinked.Keys.Contains(linkedsys.location))
                    {
                        systemslinked.Add(linkedsys.location, linkedsys);
                    }

                    if (!mappedwormholes.Contains(wh))
                    {
                        mappedwormholes.Add(wh);
                    }
                    if (!mappedsystems.Keys.Contains(linkedsys.location))
                    {
                        mappedsystems.Add(linkedsys.location, linkedsys);

                        UserControl_System syslink_usrctrl; 
                        Point sysiconloc = new Point();
                        if (mappedsystems[linkedsys.location].drawloc == sysiconloc || resetlocs == true) //|| reset.
                        {
                            angle += 270 / systemslinked.Count;
                            sysiconloc = sides_ab(icondistance, angle, false);
                            sysiconloc.X += system.drawloc.X;
                            sysiconloc.Y += system.drawloc.Y;
                            //kvp_linkedsys.Value.drawloc = sysiconloc;
                            mappedsystems[linkedsys.location].drawloc = sysiconloc;
                        }
                        //galaxy.add_WH(wh);
                        syslink_usrctrl = new UserControl_System(mappedsystems[linkedsys.location], this);
                        pictureBox1.Controls.Add(syslink_usrctrl);
                        //syslink_usrctrl.Location = sysiconloc;
                        
                        placesystems(mappedsystems[linkedsys.location], angle, depth-1, resetlocs);

                    }
                }         
            }
        }

        public void drawlines(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (WormHoleObj wh in mappedwormholes)
            { 
                Pen linkline = new Pen(Color.Blue, 2);
                List<SystemObj> systemslinked = new List<SystemObj>(wh.getlinks);
                Point iconoffset = new Point(10,10);
                
                Point pt1 = mappedsystems[systemslinked[0].location].drawloc;
                Point pt2 = mappedsystems[systemslinked[1].location].drawloc;
                pt1 = add(pt1, iconoffset);
                pt2 = add(pt2, iconoffset);

                Point pt3 = zeroedpoint(pt1, pt2);
                if (pt3.Y == 0)
                {
                    pt3.Y = 1;
                }
                int offangle = (int)Math.Atan(pt3.X / pt3.Y);
                offangle += 90;
                int offset1 = 4;
                pt3 = sides_ab(offset1, offangle);

                if (wh.polarity)
                {
                    linkline.Color = Color.Red;
                    pt1 = add(pt1, pt3);
                    pt2 = add(pt2, pt3);
                }
                else
                {
                    linkline.Color = Color.Blue;
                    pt1 = sub(pt1, pt3);
                    pt2 = sub(pt2, pt3);
                }
                
                g.DrawLine(linkline, pt1, pt2);
            }
        }


        public Point zeroedpoint(Point thispoint, Point otherpoint)
        {
            int zerox = otherpoint.X - thispoint.X;
            int zeroy = otherpoint.Y - thispoint.Y;

            return new Point(zerox, zeroy);            
        }
        public Point add(Point pointA, Point pointB)
        {
            pointA.X += pointB.X;
            pointA.Y += pointB.X;
            return pointA;
        }
        public Point sub(Point pointA, Point pointB)
        {
            pointA.X -= pointB.X;
            pointA.Y -= pointB.X;
            return pointA;
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

            Point sides = new Point((int)side_a, (int)side_b);
            return sides;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filehandling.SerializeObject(galaxy, "galaxy.hzm");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            galaxy = (Galaxy)filehandling.DeserializeObject("galaxy.hzm");
        }

        private void clipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string hazscan = Clipboard.GetText();
            Readscan.readscan(hazscan, galaxy);
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
