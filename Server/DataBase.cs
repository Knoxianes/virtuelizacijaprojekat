using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class DataBase : IDatabase
    {
        public void Load(MemoryStream memoryStream)
        {
            StreamReader streamReader = new StreamReader(memoryStream);
            Console.WriteLine(streamReader.ReadToEnd());
            Console.WriteLine("\n");
            Console.WriteLine("\n");


        }
    }
}
