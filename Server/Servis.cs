using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Servis : IServis
    {
       
        public void Load(MemoryStream memoryStream, string fileName)
        {
            StreamReader streamReader = new StreamReader(memoryStream);
            string file = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader.Dispose();

            file.Trim();
            List<string> lines = new List<string>(file.Split('\n'));
            lines.RemoveAt(0);
            if(lines.Count > 25)
            {
                //treba daodati ovde kreiranje audita za odbacivanje datotek


                return;
            }
            FileType filetype;
            if (fileName.Split('_')[0] == "forecast")
            {
                filetype = FileType.PROGNOZIRANO;
            }
            else if(fileName.Split('_')[0] == "measured")
            {
                filetype = FileType.OSTVARENO;
            }
            else
            {
                Console.WriteLine(fileName.Split('_')[0]);
                Console.WriteLine("Doslo je do greske u nazivu fajla");
                throw new Exception("Doslo je do greske u nazivu fajla!");
            }
            switch (ConfigurationManager.AppSettings["database"].ToLower())
            {
                case "xml":
                    LoadXML(lines,filetype);
                    break;
                case "in-memory":
                    LoadInMemory(lines,filetype);
                    break;
                default:
                    Console.WriteLine("Nije lepo namesten config fajl vezan za bazu podataka!");
                    throw new Exception("Doslo je do greske u namestanju config fajla!");
            }
            memoryStream.Dispose();


        }

        private void LoadXML(List<string> lines, FileType filetype )
        {
            ChannelFactory<IDataBase> factory = new ChannelFactory<IDataBase>("DataBase");
            IDataBase kanal = factory.CreateChannel();
            foreach(var line in lines)
            {
                //Implementirati ucitavanje u bazu podataka
            }
        }

        private void LoadInMemory(List<string> lines, FileType filetype)
        {
            //Implementirati ucitavanje u bazu podataka
            throw new NotImplementedException();
        }
    }
}
