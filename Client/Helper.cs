using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static  class Helper
    {
        //Pomocna funckija za ucitavanje podataka iz direktorijuma csv fajlova  u bazu podataka
        public static bool LoadData(string path, FileType fileType)
        {
            string[]files = Directory.GetFiles(path);
            if (files == null || files.Length <= 0)
            {
                return false;
            }
            ChannelFactory<IServis> factory = new ChannelFactory<IServis>("Server");
            IServis kanal = factory.CreateChannel();

            foreach (string file in files)
            {
                string fileName = file.Split('\\')[file.Split('\\').Length - 1];
                fileName = fileName.Trim();
                string typeOfFile = fileName.Split('_')[0];
                if( fileType == FileType.OSTVARENO)
                {
                    if(typeOfFile != "ostv")
                    {
                        return false;
                    }
                }
                else
                {
                    if (typeOfFile != "prog")
                    {
                        return false;
                    }
                }
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                    
                        fileStream.CopyTo(memoryStream);
                        memoryStream.Position = 0;
                        kanal.Load(memoryStream,fileName ,fileType);
                    
                    
                    }
                }
            }
            return true;
        }
        public static void Calculate()
        {
            ChannelFactory<IServis> factory = new ChannelFactory<IServis>("Server");
            IServis kanal = factory.CreateChannel();
            kanal.Calculate();
        }
    }
}
