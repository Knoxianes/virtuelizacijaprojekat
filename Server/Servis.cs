using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        private static uint auditrow_count = 0;
        private static uint importedfilerow_count = 0;
        private static uint loadrow_count = 0;
        public void Load(MemoryStream memoryStream, string fileName, FileType fileType)
        {

            StreamReader streamReader = new StreamReader(memoryStream);
            string file = streamReader.ReadToEnd();
           
            streamReader.Close();
            streamReader.Dispose();

            file = file.Trim();
            List<string> lines = new List<string>(file.Split('\n'));
            lines.RemoveAt(0);
           

            ChannelFactory<IDataBase> factory = new ChannelFactory<IDataBase>("DataBase");
            IDataBase kanal = factory.CreateChannel();
            DataBaseType dbtype;

            if (ConfigurationManager.AppSettings["database"].ToLower() == "xml")
            {
                dbtype = DataBaseType.XML;
            }
            else if (ConfigurationManager.AppSettings["database"].ToLower() == "inmemory") {
                dbtype = DataBaseType.INMEMORY;
            }
            else
            {
                Console.WriteLine("Doslo je do greske u konfiguraciji aplikacije!!! ");
                throw new Exception("Doslo je do greske u konfiguraciji aplikacije!!!");
            }

            if (lines.Count > 25)
            {
                auditrow_count += 1;
                Audit error = new Audit(auditrow_count, DateTime.Now, String.Format("U datoteci {0} nalazi se neodgovarajući broj redova: {1}", fileName, lines.Count), MessageType.Error);
                kanal.AddAudit(error, dbtype);
                return;
            }
            foreach (var line in lines)
            {
                
                loadrow_count += 1;
                if(fileType == FileType.OSTVARENO)
                {

                    var splited = line.Split(',');
                    double value = double.Parse(splited[2]);
                    string date = splited[0];
                    string time = splited[1];
                    DateTime dateTime= DateTime.Parse(date+ " " + time);
                    Load load = new Load(loadrow_count, dateTime, -1, value, -1, -1, importedfilerow_count + 1);
                    kanal.AddLoad(load, dbtype, fileType);
                }
                else
                {
                    var splited = line.Split(',');
                    double value = double.Parse(splited[2]);
                    string date = splited[0];
                    string time = splited[1];
                    DateTime dateTime = DateTime.Parse(date + " " + time);
                    Load load = new Load(loadrow_count, dateTime, value, -1, -1, -1, importedfilerow_count + 1);
                    kanal.AddLoad(load, dbtype, fileType);
                }

            }
            auditrow_count += 1;
            importedfilerow_count += 1;
            Audit info = new Audit(auditrow_count, DateTime.Now, String.Format("Datoteka {0} je uspesno procitana", fileName), MessageType.Info);
            ImportedFile importedFile = new ImportedFile(importedfilerow_count, fileName);
            kanal.AddAudit(info, dbtype);
            kanal.AddImportedFile(importedFile, dbtype);

            memoryStream.Dispose();

        }
    }
        
}
