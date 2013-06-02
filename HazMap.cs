﻿using System;
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
        List<UserControl_System> sys_usrCtrls = new List<UserControl_System>();
        Point zeropt;

        UserControl_System syslink_usrctrl;

        public HazMap()
        {
            InitializeComponent();
            zeropt = new Point((pictureBox1.Width / 2) * -1, pictureBox1.Height / 2);
            canvasdata = new Canvasdata(1, this.Width, this.Height, zeropt);

            Dictionary<string, testobj1> testdic = new Dictionary<string, testobj1>();

            testobj1 to1 = new testobj1("test1");
            testdic.Add("test1", to1);

            testobj2 to2 = new testobj2("testb", to1);
            //testdic.Add("testb", to2);

            to1.changeid2("test2");
            to1.changeid3 = "test3";

            
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
                    Readscan.readscan(scantext, galaxy);
                }
                catch (IOException)
                {
                }
            }
        }

        private void toolStripButton_drawmap_Click(object sender, EventArgs e)
        {           
            
            Point startmappoint = new Point(0,200);
           
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

                
                sys_usrCtrls = new List<UserControl_System>();  //new usercontrol list
                mappedsystems = new Dictionary<string, SystemObj>(); //new mappedsystems dictionary

                startSys.maploc = startmappoint; //set first system location 
                syslink_usrctrl = new UserControl_System(startSys, this, canvasdata); //first system usercontrol
                sys_usrCtrls.Add(syslink_usrctrl); //addthe user control to the usercontrol list
                 
                mappedsystems.Add(startSys.location, startSys);//add the system to the mappedsystems dictionary

                pictureBox1.Controls.Add(syslink_usrctrl); //add teh control to the picturebox.
                Point loc = syslink_usrctrl.Location;
                placesystems(startSys, 180, depth, resetlocs);
                
               
            }    
            refreshlines();        
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

        private void placesystems(SystemObj system, int angle, int depth, bool resetlocs)
        {
                       
            if (depth > 0)
            {
                //Dictionary<string, SystemObj> systemslinked = new Dictionary<string, SystemObj>();
                List<SystemObj> systemslinked = new List<SystemObj>();
                int icondistance = 100;

                Point sysiconloc = system.maploc;//new Point(0, 0);
                bool resetthisloc = false;

                foreach (WormHoleObj wh in system.Wormholes) //foreach wormhole from this system.
                {
                    SystemObj linkedsys = wh.getlink(system);
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
                foreach (SystemObj linkedsys in systemslinked)
                {
                    //SystemObj linkedsys = kvp_system.Value;

                    Point dloc = mappedsystems[linkedsys.location].maploc;
                    //if (dloc.X < 1 || dloc.Y < 1) { resetthisloc = true; }
                    int linkcount = systemslinked.Count;
                    //if (linkcount > 1) { linkcount -= 1; }
                    if (resetthisloc || resetlocs) //reset.
                    {
                        angle += (360 / linkcount);
                        sysiconloc = sides_ab(icondistance, angle, false);
                        sysiconloc.X += system.maploc.X;
                        sysiconloc.Y += system.maploc.Y;


                    }
                    mappedsystems[linkedsys.location].maploc = sysiconloc;
                    placesystems(mappedsystems[linkedsys.location], angle, depth - 1, resetlocs);
 
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



    public class testobj1
    {
        string id;
        string id2;
        string id3;
        testobj2 to2;
        public testobj1(string id)
        {
            this.id = id;
            id2 = id;
            id3 = id;
        }

        public void changeid2(string newidid)
        {
            this.id2 = newidid;
        }
        public string changeid3
        {
            set { this.id3 = value; }
        }
        
        public void set2link(testobj2 tobj2)
        {
            this.to2 = tobj2;
        }

    }
    public class testobj2
    {
        string id;
        testobj1 tobj1;
        testobj1 tobj2;
        public testobj2(string id, testobj1 tobj1)
        {
            this.id = id;
            this.tobj1 = tobj1;
            tobj1.set2link(this);
        }
    }


}
