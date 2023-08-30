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
        public static void LoadData(string path, FileType fileType)
        {
            string[]files = Directory.GetFiles(path);
            if (files == null || files.Length <= 0)
            {
                return;
            }
            ChannelFactory<IServis> factory = new ChannelFactory<IServis>("Server");
            IServis kanal = factory.CreateChannel();

                foreach (string file in files)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {

                        using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                        {
                        
                            fileStream.CopyTo(memoryStream);
                            memoryStream.Position = 0;
                            kanal.Load(memoryStream, file.Split('\\')[file.Split('\\').Length - 1],fileType);
                        
                        
                        }
                    }
                }
        }
    }
}
