﻿using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DataBase
{
    public class DataBaseServis : IDataBase
    {
        // In-memory baze podataka
        public static Dictionary<uint,Audit> AuditDataBase = new Dictionary<uint,Audit>();
        public static Dictionary<uint, Load> LoadDataBase = new Dictionary<uint, Load>();
        public static Dictionary<uint, ImportedFile> ImportedFileDataBase = new Dictionary<uint, ImportedFile>();
        private readonly string pathToXML = "C:\\Users\\strah\\OneDrive\\Dokumenti\\GitHub\\virtuelizacijaprojekat\\DataBase\\";
        public void AddAudit(Audit audit, DataBaseType dbtype)
        {
            switch (dbtype)
            {
                case DataBaseType.INMEMORY:
                    AddAuditInMemory(audit);
                    break;
                case DataBaseType.XML:
                    AddAuditXML(audit);
                    break;
                default: break;
            }
        }

        public void AddImportedFile(ImportedFile importedFile, DataBaseType dbtype)
        {
            switch (dbtype)
            {
                case DataBaseType.INMEMORY:
                    AddImportedFileInMemory(importedFile);
                    break;
                case DataBaseType.XML:
                    AddImportedFileXML(importedFile);
                    break;
                default: break;
            }
        }

        public void AddLoad(Load load, DataBaseType dbtype, FileType filetype)
        {
            switch (dbtype)
            {
                case DataBaseType.INMEMORY:
                    AddLoadInMemory(load,filetype);
                    break;
                case DataBaseType.XML:
                    AddLoadXML(load,filetype);
                    break;
                default: break;
            }
        }

        public List<Load> ReadLoad(DataBaseType dbtype)
        {
            switch (dbtype)
            {
                case DataBaseType.INMEMORY:
                    return ReadLoadInMemory();
                case DataBaseType.XML:
                    return ReadLoadXML();
                default:
                    return null;
            }
        }


        public void UpdateLoads(List<Load> loads, DataBaseType dbtype)
        {
            switch (dbtype)
            {
                case DataBaseType.INMEMORY:
                    UpdateLoadsInMemory(loads);
                    break;
                case DataBaseType.XML:
                    UpdateLoadsXML(loads);
                    break;
                default: break;
            }
        }

       



        //Funkcije za rad sa XML bazom
        private void AddAuditXML(Audit audit)
        {
            if(!File.Exists(pathToXML + "TBL_AUDIT.xml"))
            {
                XmlDocument doc =  new XmlDocument();
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);

                XmlElement stavke = doc.CreateElement("STAVKE");
                doc.AppendChild(stavke);

                XmlElement row = doc.CreateElement("row");

                XmlElement id = doc.CreateElement("ID");
                XmlText idValue = doc.CreateTextNode(audit.Id.ToString());
                id.AppendChild(idValue);
                row.AppendChild(id);

                XmlElement timeStamp = doc.CreateElement("TIME_STAMP");
                XmlText timeStampValue = doc.CreateTextNode(audit.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss"));
                timeStamp.AppendChild(timeStampValue);
                row.AppendChild(timeStamp);

                XmlElement messageType = doc.CreateElement("MESSAGE_TYPE");
                XmlText messageTypeValue = doc.CreateTextNode(audit.MessageType.ToString());
                messageType.AppendChild(messageTypeValue);
                row.AppendChild(messageType);

                XmlElement message = doc.CreateElement("MESSAGE_TYPE");
                XmlText messageValue = doc.CreateTextNode(audit.Message);
                message.AppendChild(messageValue);
                row.AppendChild(message);

                
                stavke.AppendChild(row);
                doc.Save(pathToXML + "TBL_AUDIT.xml");
                
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(pathToXML + "TBL_AUDIT.xml");
                XmlElement stavke = doc.DocumentElement;
               
                XmlElement row = doc.CreateElement("row");

                XmlElement id = doc.CreateElement("ID");
                XmlText idValue = doc.CreateTextNode(audit.Id.ToString());
                id.AppendChild(idValue);
                row.AppendChild(id);

                XmlElement timeStamp = doc.CreateElement("TIME_STAMP");
                XmlText timeStampValue = doc.CreateTextNode(audit.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss"));
                timeStamp.AppendChild(timeStampValue);
                row.AppendChild(timeStamp);

                XmlElement messageType = doc.CreateElement("MESSAGE_TYPE");
                XmlText messageTypeValue = doc.CreateTextNode(audit.MessageType.ToString());
                messageType.AppendChild(messageTypeValue);
                row.AppendChild(messageType);

                XmlElement message = doc.CreateElement("MESSAGE_TYPE");
                XmlText messageValue = doc.CreateTextNode(audit.Message);
                message.AppendChild(messageValue);
                row.AppendChild(message);

                stavke.AppendChild(row);
                doc.Save(pathToXML + "TBL_AUDIT.xml");
                


            }
            
        }
        private void AddImportedFileXML(ImportedFile importedFile)
        {
            if (!File.Exists(pathToXML + "TBL_IMPORTED_FILE.xml"))
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);

                XmlElement stavke = doc.CreateElement("STAVKE");
                doc.AppendChild(stavke);

                XmlElement row = doc.CreateElement("row");

                XmlElement id = doc.CreateElement("ID");
                XmlText idValue = doc.CreateTextNode(importedFile.Id.ToString());
                id.AppendChild(idValue);
                row.AppendChild(id);

                XmlElement fileName = doc.CreateElement("FILE_NAME");
                XmlText fileNameValue = doc.CreateTextNode(importedFile.FileName);
                fileName.AppendChild(fileNameValue);
                row.AppendChild(fileName);


                stavke.AppendChild(row);
                doc.Save(pathToXML + "TBL_IMPORTED_FILE.xml");

            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(pathToXML + "TBL_IMPORTED_FILE.xml");
                XmlElement stavke = doc.DocumentElement;

                XmlElement row = doc.CreateElement("row");

                XmlElement id = doc.CreateElement("ID");
                XmlText idValue = doc.CreateTextNode(importedFile.Id.ToString());
                id.AppendChild(idValue);
                row.AppendChild(id);

                XmlElement fileName = doc.CreateElement("FILE_NAME");
                XmlText fileNameValue = doc.CreateTextNode(importedFile.FileName);
                fileName.AppendChild(fileNameValue);
                row.AppendChild(fileName);

                stavke.AppendChild(row);
                doc.Save(pathToXML + "TBL_IMPORTED_FILE.xml");
            }
        }
        private void AddLoadXML(Load load, FileType filetype)
        {
            if (!File.Exists(pathToXML + "TBL_LOAD.xml"))
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);

                XmlElement stavke = doc.CreateElement("STAVKE");
                doc.AppendChild(stavke);

                XmlElement row = doc.CreateElement("row");

                XmlElement id = doc.CreateElement("ID");
                XmlText idValue = doc.CreateTextNode(load.Id.ToString());
                id.AppendChild(idValue);
                row.AppendChild(id);

                XmlElement timeStamp = doc.CreateElement("TIME_STAMP");
                XmlText timeStampValue = doc.CreateTextNode(load.Timestamp.ToString("yyyy-MM-dd HH:mm"));
                timeStamp.AppendChild(timeStampValue);
                row.AppendChild(timeStamp);

                XmlElement forecast = doc.CreateElement("FORECAST_VALUE");
                XmlText forecastValue = doc.CreateTextNode(load.ForecastValue.ToString());
                forecast.AppendChild(forecastValue);
                row.AppendChild(forecast);

                XmlElement measured = doc.CreateElement("MEASURED_VALUE");
                XmlText measuredValue = doc.CreateTextNode(load.MeasuredValue.ToString());
                measured.AppendChild(measuredValue);
                row.AppendChild(measured);

                XmlElement apd = doc.CreateElement("ABSOLUTE_PERCENTAGE_DEVIATION");
                XmlText apdValue = doc.CreateTextNode(load.AbsolutePercentageDeviation.ToString());
                apd.AppendChild(apdValue);
                row.AppendChild(apd);

                XmlElement squaredDeviation = doc.CreateElement("SQUARED_DEVIATION");
                XmlText squaredDeviationValue = doc.CreateTextNode(load.SquareDeviation.ToString());
                squaredDeviation.AppendChild(squaredDeviationValue);
                row.AppendChild(squaredDeviation);

                XmlElement ifID = doc.CreateElement("IMPORTED_FILE_ID");
                XmlText ifIDValue = doc.CreateTextNode(load.ImportedFileId.ToString());
                ifID.AppendChild(ifIDValue);
                row.AppendChild(ifID);


                stavke.AppendChild(row);
                doc.Save(pathToXML + "TBL_LOAD.xml");

            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(pathToXML + "TBL_LOAD.xml");
                XmlNode loadExists = LoadExistsXML(load,doc);
                XmlElement stavke = doc.DocumentElement;
                if (loadExists != null)
                {
                    
                    if (filetype == FileType.OSTVARENO)
                    {
                        XmlElement measured = doc.CreateElement("MEASURED_VALUE");
                        XmlText measuredValue = doc.CreateTextNode(load.MeasuredValue.ToString());
                        measured.AppendChild(measuredValue);
                        loadExists.ReplaceChild(measured, loadExists.ChildNodes[3]);
                        doc.Save(pathToXML + "TBL_LOAD.xml");
                    }
                    else
                    {
                        XmlElement forecast = doc.CreateElement("FORECAST_VALUE");
                        XmlText forecastValue = doc.CreateTextNode(load.ForecastValue.ToString());
                        forecast.AppendChild(forecastValue);
                        loadExists.ReplaceChild(forecast, loadExists.ChildNodes[2]);
                        doc.Save(pathToXML + "TBL_LOAD.xml");
                    }

                }
                else
                {
                    
                    XmlElement row = doc.CreateElement("row");

                    XmlElement id = doc.CreateElement("ID");
                    XmlText idValue = doc.CreateTextNode(load.Id.ToString());
                    id.AppendChild(idValue);
                    row.AppendChild(id);

                    XmlElement timeStamp = doc.CreateElement("TIME_STAMP");
                    XmlText timeStampValue = doc.CreateTextNode(load.Timestamp.ToString("yyyy-MM-dd HH:mm"));
                    timeStamp.AppendChild(timeStampValue);
                    row.AppendChild(timeStamp);

                    XmlElement forecast = doc.CreateElement("FORECAST_VALUE");
                    XmlText forecastValue = doc.CreateTextNode(load.ForecastValue.ToString());
                    forecast.AppendChild(forecastValue);
                    row.AppendChild(forecast);

                    XmlElement measured = doc.CreateElement("MEASURED_VALUE");
                    XmlText measuredValue = doc.CreateTextNode(load.MeasuredValue.ToString());
                    measured.AppendChild(measuredValue);
                    row.AppendChild(measured);

                    XmlElement apd = doc.CreateElement("ABSOLUTE_PERCENTAGE_DEVIATION");
                    XmlText apdValue = doc.CreateTextNode(load.AbsolutePercentageDeviation.ToString());
                    apd.AppendChild(apdValue);
                    row.AppendChild(apd);

                    XmlElement squaredDeviation = doc.CreateElement("SQUARED_DEVIATION");
                    XmlText squaredDeviationValue = doc.CreateTextNode(load.SquareDeviation.ToString());
                    squaredDeviation.AppendChild(squaredDeviationValue);
                    row.AppendChild(squaredDeviation);

                    XmlElement ifID = doc.CreateElement("IMPORTED_FILE_ID");
                    XmlText ifIDValue = doc.CreateTextNode(load.ImportedFileId.ToString());
                    ifID.AppendChild(ifIDValue);
                    row.AppendChild(ifID);


                    stavke.AppendChild(row);
                    doc.Save(pathToXML + "TBL_LOAD.xml");

                }
            }
        }

        // Pomocna funckija za proveru da li postoji vec podatak u xml bazi ako postoji vraca row sa tim podatkom
        private XmlNode  LoadExistsXML(Load load,XmlDocument doc)
        {
            XmlElement stavke = doc.DocumentElement;
            XmlNodeList stavkeChildren = stavke.ChildNodes;
            foreach(XmlNode child in stavkeChildren)
            {
                if (checkChild(child.ChildNodes, load))
                {
                    return child;
                }
            }
            return null;
        }

        // Pomocna funkcija za proveru dece od reda u kome se mozda nalazi podatak
        private bool checkChild(XmlNodeList parent,Load load)
        {
            foreach(XmlNode child in parent)
            {
                if(child.Name == "TIME_STAMP")
                {
                    DateTime dateTime = DateTime.Parse(child.InnerText);
                    if(dateTime == load.Timestamp)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Funckija za citanje xml baze Load podataka
        private List<Load> ReadLoadXML()
        {
            List<Load> loads = new List<Load>();
            XmlDocument doc = new XmlDocument();
            doc.Load(pathToXML + "TBL_LOAD.xml");
            XmlElement stavke = doc.DocumentElement;
            foreach(XmlNode row in stavke.ChildNodes)
            {
                uint id = uint.Parse(row.ChildNodes[0].InnerText);
                DateTime dateTime = DateTime.Parse(row.ChildNodes[1].InnerText);
                double forecastValue = double.Parse(row.ChildNodes[2].InnerText);
                double measuredValue  = double.Parse(row.ChildNodes[3].InnerText);
                double apd = double.Parse(row.ChildNodes[4].InnerText);
                double squareDeviation = double.Parse(row.ChildNodes[5].InnerText);
                uint importedFileId = uint.Parse(row.ChildNodes[6].InnerText);
                loads.Add(new Load(id, dateTime, forecastValue, measuredValue, apd, squareDeviation, importedFileId));
            }
            return loads;
        }

        // Funckija za azuriranje xml baze Load podataka
        private void UpdateLoadsXML(List<Load> loads)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(xmlDeclaration);
            XmlElement stavke = doc.CreateElement("STAVKE");
            doc.AppendChild(stavke);
            foreach(Load load in loads)
            {
                XmlElement row = doc.CreateElement("row");

                XmlElement id = doc.CreateElement("ID");
                XmlText idValue = doc.CreateTextNode(load.Id.ToString());
                id.AppendChild(idValue);
                row.AppendChild(id);

                XmlElement timeStamp = doc.CreateElement("TIME_STAMP");
                XmlText timeStampValue = doc.CreateTextNode(load.Timestamp.ToString("yyyy-MM-dd HH:mm"));
                timeStamp.AppendChild(timeStampValue);
                row.AppendChild(timeStamp);

                XmlElement forecast = doc.CreateElement("FORECAST_VALUE");
                XmlText forecastValue = doc.CreateTextNode(load.ForecastValue.ToString());
                forecast.AppendChild(forecastValue);
                row.AppendChild(forecast);

                XmlElement measured = doc.CreateElement("MEASURED_VALUE");
                XmlText measuredValue = doc.CreateTextNode(load.MeasuredValue.ToString());
                measured.AppendChild(measuredValue);
                row.AppendChild(measured);

                XmlElement apd = doc.CreateElement("ABSOLUTE_PERCENTAGE_DEVIATION");
                XmlText apdValue = doc.CreateTextNode(load.AbsolutePercentageDeviation.ToString());
                apd.AppendChild(apdValue);
                row.AppendChild(apd);

                XmlElement squaredDeviation = doc.CreateElement("SQUARED_DEVIATION");
                XmlText squaredDeviationValue = doc.CreateTextNode(load.SquareDeviation.ToString());
                squaredDeviation.AppendChild(squaredDeviationValue);
                row.AppendChild(squaredDeviation);

                XmlElement ifID = doc.CreateElement("IMPORTED_FILE_ID");
                XmlText ifIDValue = doc.CreateTextNode(load.ImportedFileId.ToString());
                ifID.AppendChild(ifIDValue);
                row.AppendChild(ifID);


                stavke.AppendChild(row);
            }
            doc.Save(pathToXML + "TBL_LOAD.xml");

        }


        //Private funkcije za rad sa In-Memory bazom
        private void AddAuditInMemory(Audit audit)
        {
            AuditDataBase.Add((uint)AuditDataBase.Count+1, audit);
        }
        private void AddImportedFileInMemory(ImportedFile importedFile)
        {
            ImportedFileDataBase.Add((uint)ImportedFileDataBase.Count + 1, importedFile);
        }
        private void AddLoadInMemory(Load load, FileType filetype)
        {
            int loadExists = LoadExistsInMemory(load);
            if(loadExists == -1)
            {
                LoadDataBase.Add((uint)LoadDataBase.Count + 1, load);
            }
            else
            {
                if(filetype == FileType.OSTVARENO)
                {
                    Load databaseLoad = LoadDataBase[(uint)loadExists];
                    databaseLoad.MeasuredValue = load.MeasuredValue;
                    LoadDataBase[(uint)loadExists] = databaseLoad;

                }
                else
                {
                    Load databaseLoad = LoadDataBase[(uint)loadExists];
                    databaseLoad.ForecastValue = load.ForecastValue;
                    LoadDataBase[(uint)loadExists] = databaseLoad;
                }
            }
        }

        // Funkcija za citanje cele in-memory baze load podataka
        private List<Load> ReadLoadInMemory()
        {
            List<Load> loads = new List<Load>();
            foreach(var key in LoadDataBase.Keys)
            {
                loads.Add(LoadDataBase[key]);
            }
            return loads;
        }

        // Pomocna funckija za proveru dali postoji Load podataka u in-memory bazi
        private int LoadExistsInMemory(Load load)
        {
            foreach (var key in LoadDataBase.Keys)
            {
                if(load.Timestamp == LoadDataBase[key].Timestamp)
                {
                    return (int)key;
                }
            }
            return -1;
        }

        // Funckija za azuriranje in-memory baze
        private void UpdateLoadsInMemory(List<Load> loads)
        {
            Dictionary<uint, Load> updateDataBase = new Dictionary<uint, Load>();
            foreach(Load load in loads)
            {
                updateDataBase.Add(load.Id, load);
            }
            LoadDataBase = updateDataBase;
        }
    }
}
