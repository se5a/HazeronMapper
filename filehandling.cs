using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HazeronMapper
{
    public static class filehandling
    {

        public static void SerializeObject(object o, string fileName)
        {
            using (Stream s = File.OpenWrite(fileName))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, o);
            }
        }


        public static object DeserializeObject(string fileName)
        {
            using (Stream s = File.OpenRead(fileName))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return bf.Deserialize(s);
            }
        }
    }
}
