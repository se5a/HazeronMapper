using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazeronMapper
{
    [Serializable]
    public class Galaxy
    {
        List<SectorObj> Sectors_list = new List<SectorObj>();
        List<WormHoleObj> Wormholes_list = new List<WormHoleObj>();
        Dictionary<string, SectorObj> Sectors_dictionary = new Dictionary<string, SectorObj>();
        public Galaxy()
        {
    
        }
        public void add_sector(string location, SectorObj sector)
        {
            if (!this.Sectors_dictionary.Keys.Contains(location))
            {
                //this.Sectors_list.Add(sector);
                this.Sectors_dictionary.Add(location, sector);
            }
            else
            {
                //this.Sectors_dictionary[location] = sector;
            }
        }



        /// <summary>
        /// forces an update of the galaxy sectors list from the dictionary_list
        /// </summary>
        /// <param name="depth">depth of iteration, default 0, ie depth = 1 will update the sectors systemlists</param>
        public void listupdate_sectors(int depth = 0)
        {
            List<SectorObj> sectors_list = new List<SectorObj>();
            foreach (KeyValuePair<string, SectorObj> sector in this.Sectors_dictionary)
            {
                if (depth > 0)
                {
                    sector.Value.listupdate_systems(depth - 1);
                }
                sectors_list.Add(sector.Value);
            }
            this.Sectors_list = sectors_list;
        }

        public List<SectorObj> sectors_list
        {
            get { return this.Sectors_list; }
        }
        public Dictionary<string, SectorObj> sectors_dictionary
        {
            get { return this.Sectors_dictionary; }
        }

        /// <summary>
        /// creates and returns dictionary by iterating through the galaxy.sectors_dictonary and sectors.systems_dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, SystemObj> systems_dictionary()
        {
            Dictionary<string, SystemObj> systemdict = new Dictionary<string, SystemObj>();
            foreach (KeyValuePair<string, SectorObj> kvp_sector in this.sectors_dictionary)
            {
                foreach (KeyValuePair<string,SystemObj> kvp_system in kvp_sector.Value.systems_dictionary)
                {
                    if (!systemdict.ContainsKey(kvp_system.Key))
                    {
                        systemdict.Add(kvp_system.Key, kvp_system.Value);
                    }
                }
            }
            return systemdict;
        }

        /// <summary>
        /// creates a list of systems by itterating through the galaxy.sectors_list and sectors.systems_list - fix this to just add those lists together... or creating it from the dictionary.
        /// </summary>
        /// <returns></returns>
        /// <remarks>consider calling galaxy.listupdate_sectors(1) first. may be slow. or using galaxy.system_dictionary</remarks>
        public List<SystemObj> systems_list()
        {
            List<SystemObj> syslist = new List<SystemObj>();
            foreach (SectorObj sector in this.Sectors_list)
            {
                foreach (SystemObj system in sector.systems_list)
                {
                    syslist.Add(system);
                }
            }
            return syslist;
        }


        /// <summary>
        /// this should not be here in galaxy. move later.
        /// </summary>
        /// <param name="wormhole"></param>
        public void add_WH(WormHoleObj wormhole)
        {
            if (!this.Wormholes_list.Contains(wormhole))
            {
                this.Wormholes_list.Add(wormhole);
            }
        }
        public List<WormHoleObj> wormholes
        {
            get { return this.Wormholes_list; }
        }
    }

    [Serializable]
    public class SectorObj
    {
        string SectorName;
        //List<int> sectorLoc = new List<int>();
        string sectorlocS;
        List<SystemObj> Systems_list = new List<SystemObj>();
        Dictionary<string, SystemObj> Systems_dictionary = new Dictionary<string, SystemObj>();

        public SectorObj(string sectorloc, string SectorName, Galaxy galaxy)
        {
            this.sectorlocS = sectorloc;
            this.SectorName = SectorName;
            galaxy.add_sector(sectorlocS, this);
        }

        public void add_system(string systemloc, SystemObj system)
        {
            if (!system.sector.Systems_dictionary.Keys.Contains(systemloc))
            {
                system.sector.Systems_dictionary.Add(systemloc, system);                
            }
            else 
            {
                //system.sector.Systems_dictionary[systemloc] = system; 
            }
        }
        public string name
        {
            get { return this.SectorName; }
        }
        public string location
        {
            get { return this.sectorlocS; }
        }
        
        /// <summary>
        /// forces an update of this sectors system list
        /// </summary>
        public void listupdate_systems(int depth = 0)
        {                        
            List<SystemObj> systems_list = new List<SystemObj>();
            foreach (KeyValuePair<string, SystemObj> system  in this.Systems_dictionary)
            {
                systems_list.Add(system.Value);
            }
            this.Systems_list = systems_list;
        }

        /// <summary>
        /// returns list of systems for this sector, note: no garentee the list is complete, use listupdate_systems first if unsure.
        /// </summary>
        public List<SystemObj> systems_list
        {
            get { return this.Systems_list; }
        }
        public Dictionary<string, SystemObj> systems_dictionary
        {
            get { return this.Systems_dictionary; }
        }

        public override string ToString()
        {
            return this.SectorName;
        }
    }

    [Serializable]
    public class SystemObj
    {
        string sysName;
        List<int> sysLoc = new List<int>();
        string sysLocS;
        SectorObj parentSector;
        //UserControl_System systemicon;
        
        List<WormHoleObj> Wormholes_list = new List<WormHoleObj>();
        List<SystemBodyObj> systemBodys_list = new List<SystemBodyObj>();

        string sysNotes;

        System.Drawing.Point sysMapLoc;


        public SystemObj(string sysloc, string systemname, SectorObj sector)
        {         
            this.sysLocS = sysloc;
            this.sysName = systemname;
            this.parentSector = sector;
            sector.add_system(sysloc, this);
        }

        public void add_WH(WormHoleObj WHName)
        {
            if (!this.Wormholes_list.Contains(WHName))
            {
                this.Wormholes_list.Add(WHName);
            }
        }
     
        public string name
        {
            get { return this.sysName; }
        }

        //public string sector
        //{
        //    get { return this.sectorName.name; }
        //}
        public SectorObj sector
        {
            get { return this.parentSector; }
        }
        public string location
        {
            get { return this.sysLocS; }
        }
        //public UserControl_System usrctrl
        //{
        //    get { return this.systemicon; }
        //    set { this.systemicon = value; }
        //}
        //public List<int> loc
        //{
        //    get { return this.sysLoc; }
        //    set { this.sysLoc = value; }
        //}
        public string notes
        {
            get { return this.sysNotes; }
            set { this.sysNotes = value; }
        }

        public List<WormHoleObj> Wormholes
        {
            get { return this.Wormholes_list; }
        }



        public System.Drawing.Point maploc
        {
            get { return this.sysMapLoc; }
            set { this.sysMapLoc = value; }
        }

        public override string ToString()
        {
            return this.sysName;
        }
    }

    [Serializable]
    public class SystemBodyObj
    {
        string name;
        string orbzone;
        SystemObj system;
        int diameter;
        string atmosphere;
        string ocean;
        List<ResourceZoneObj> resourceZoneList = new List<ResourceZoneObj>();
        public SystemBodyObj(string name, string orbitzone, SystemObj system)
        {
            this.name = name;
            this.orbzone = orbitzone;
            this.system = system;
        }

        public void add_resourcezone(ResourceZoneObj zone)
        {
            if (!this.resourceZoneList.Contains(zone))// fix this. this needs to check the zone num.
            {
                this.resourceZoneList.Add(zone);
            }
        }

    }

    [Serializable]
    public class ResourceZoneObj
    {
        int zoneNum;
        SystemBodyObj parentBody;
        List<ResourceObj> resourcesList = new List<ResourceObj>();
        public ResourceZoneObj(int zoneNum, SystemBodyObj sysbody)
        {
            this.zoneNum = zoneNum;
            this.parentBody = sysbody;
        }
        public void add_resource(ResourceObj resource)
        {
            if (!this.resourcesList.Contains(resource))
            {
                this.resourcesList.Add(resource);
            }
        }
        public List<ResourceObj> resource_list
        {
            get { return this.resourcesList; }
        }
    }

    [Serializable]
    public class ResourceObj
    {
        string resource;
        int quality;
        int amount;
        ResourceZoneObj parentRzone;
        public ResourceObj(string resource, int quality, int amount, ResourceZoneObj rzone)
        {
            this.resource = resource;
            this.quality = quality;
            this.amount = amount;
            this.parentRzone = rzone;
        }
    }

    [Serializable]
    public class WormHoleObj
    {
        string WHName;
        bool WHPolarity = false; //false = negitive true = positive
        List<SystemObj> SystemsLinked = new List<SystemObj>(2);

        public WormHoleObj(string WHName, bool WHPolarity, SystemObj System, Galaxy galaxy)
        {
            
            this.WHName = WHName;
            this.WHPolarity = WHPolarity;
            makelink(System);
            //galaxy.add_WH(this);
            System.add_WH(this);
        }
        public void makelink(SystemObj System1, SystemObj System2 = null)
        {
            if (!this.SystemsLinked.Contains(System1))
            {
                this.SystemsLinked.Add(System1);
            }
            if (System2 != null)
            {
                if (!this.SystemsLinked.Contains(System2))
                {
                    this.SystemsLinked.Add(System2);
                }
            }
        }

        public bool polarity
        {
            get { return WHPolarity; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thissystem"></param>
        /// <returns>the sytem that 'thissystem' links to</returns>
        public SystemObj getlink(SystemObj thissystem)
        {
            SystemObj linkedsys; 
            if (this.SystemsLinked[0] == thissystem)
            {
                linkedsys = SystemsLinked[1];
            }
            else if (this.SystemsLinked[1] == thissystem)
            {
                linkedsys = SystemsLinked[0];
            }
            else
            {
                throw new System.ArgumentException("System does not exsist in this link");
            }
            return linkedsys;
        }
        public List<SystemObj> getlinks
        {
            get { return SystemsLinked; }
        }
        
    }
}
