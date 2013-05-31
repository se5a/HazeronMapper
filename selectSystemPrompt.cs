using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HazeronMapper
{
    public partial class selectSystemPrompt : Form
    {
        public selectSystemPrompt()
        { }
        public selectsys_returnObj selectSystemPromptdlg(Galaxy galaxy)
        {
            SystemObj selectedSystem = null;
            Dictionary<string, SystemObj> systemdict = new Dictionary<string, SystemObj>(galaxy.systems_dictionary());
            galaxy.listupdate_sectors(1);
            List<SystemObj> syslist = new List<SystemObj>(galaxy.systems_list());
            //selectedSystem = syslist[0];
            InitializeComponent();

            dataGridView_systems.DataSource = syslist;   

            dataGridView_systems.Columns["name"].DisplayIndex = 0;
            dataGridView_systems.Columns["location"].DisplayIndex = 1;
            dataGridView_systems.Columns["drawloc"].Visible = false;
            dataGridView_systems.Refresh();
            this.ShowDialog();

            //button_ok.Click += (sender, e) => { this.Close(); };
            //string key = dataGridView_systems.SelectedRows[0].Cells[1].Value.ToString();
            int keycolumnindex = dataGridView_systems.Columns["location"].Index;
            
            int keyrowindex = dataGridView_systems.Rows.IndexOf(dataGridView_systems.SelectedRows[0]);
            DataGridViewCell cell = dataGridView_systems[keycolumnindex, keyrowindex];
            string key = cell.Value.ToString();
            selectedSystem = systemdict[key];
            selectsys_returnObj returnobj = new selectsys_returnObj(selectedSystem, (int)numericUpDown_depth.Value, checkBox_resetloc.Checked);
            return returnobj;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
