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
        private static uint auditrow_count = 0; // Broj  reda  za audit fajl koji je do sada ucitan
        private static uint importedfilerow_count = 0; // Broj datoteke koja se do sad ucitan
        private static uint loadrow_count = 0; // Broj uspesnih redova podataka ucitavnih iz csv fajla do sada
        private static DataBaseType dbtype; 
        public delegate void UpdateDBDelegate(List<Load> loads, DataBaseType dataBaseType);
        public event UpdateDBDelegate UpdateDbEvent;

        // Funkcija koja vrsi kalkulacije sa podacima iz baze
        public void Calculate()
        {
            ChannelFactory<IDataBase> factory = new ChannelFactory<IDataBase>("DataBase");
            IDataBase kanal = factory.CreateChannel();
            List<Load> loads = kanal.ReadLoad(dbtype); // Uzimanje podataka iz baze
            if (ConfigurationManager.AppSettings["calculation"].ToLower() == "apd")
            {
                loads = CalculateAPD(loads);
                UpdateDbEvent += kanal.UpdateLoads;
            }
            else if (ConfigurationManager.AppSettings["calculation"].ToLower() == "sd")
            {
                loads = CalculateSD(loads);
                UpdateDbEvent += kanal.UpdateLoads;
            }
            else
            {
                Console.WriteLine("Doslo je do greske u konfiguraciji aplikacije!!! ");
                throw new Exception("Doslo je do greske u konfiguraciji aplikacije!!!");
            }
            UpdateDbEvent?.Invoke(loads, dbtype);
            
        }

        // Pomocna funckija koja racuna Sqruare Deviation
        private  List<Load> CalculateSD(List<Load> loads)
        {
            foreach(Load load in loads)
            {
                if(load.MeasuredValue != -1 && load.ForecastValue != -1)
                {
                    load.SquareDeviation = Math.Pow((load.MeasuredValue - load.ForecastValue) / load.MeasuredValue,2);
                }
            }
            return loads;
        }

        // Pomocna funkcija koja racuna  ABSOLUTE PERCENTAGE DEVIATION
        private List<Load> CalculateAPD(List<Load> loads)
        {
            foreach (Load load in loads)
            {
                if (load.MeasuredValue != -1 && load.ForecastValue != -1)
                {
                    load.AbsolutePercentageDeviation = Math.Abs(load.MeasuredValue - load.ForecastValue) / load.MeasuredValue * 100;
                }
            }
            return loads;
           
        }

        public void Load(MemoryStream memoryStream, string fileName, FileType fileType)
        {

            // Ucitavanje podataka iz MemoryStreama
            StreamReader streamReader = new StreamReader(memoryStream);
            string file = streamReader.ReadToEnd();
           
            streamReader.Close();
            streamReader.Dispose();

            file = file.Trim();
            List<string> lines = new List<string>(file.Split('\n'));
            lines.RemoveAt(0); // Uklanjanje prvog reda iz csv fajla jer je prvi red String tipa DATE,MEASURED_VALUE
           

            ChannelFactory<IDataBase> factory = new ChannelFactory<IDataBase>("DataBase");
            IDataBase kanal = factory.CreateChannel();
            

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

            // Odbacivanje svih fajlova koji su veci od 24 reda jer dan ima 24 sata
            if (lines.Count > 25)
            {
                auditrow_count += 1;
                Audit error = new Audit(auditrow_count, DateTime.Now, String.Format("U datoteci {0} nalazi se neodgovarajući broj redova: {1}", fileName, lines.Count), MessageType.Error);
                kanal.AddAudit(error, dbtype); // Dodavanje u bazu obavestenje o gresci
                return;
            }

            // Algoritam ucitavanja podataka
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
            kanal.AddAudit(info, dbtype); // Dodavnje u bazu podataka obavestenje o uspesnosti ucitanog fajla
            kanal.AddImportedFile(importedFile, dbtype); // Dodavanje u bazu podatke o ucitanom fajlu

            memoryStream.Dispose();

        }
    }
        
}
