using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum DataBaseType
    {
       XML,INMEMORY
    }
    public enum FileType
    {
        PROGNOZIRANO, OSTVARENO
    }
    [ServiceContract]
    public interface IDataBase
    {
        [OperationContract]
        void AddLoad(Load load, DataBaseType dbtype, FileType filetype);
        [OperationContract]
        void AddAudit(Audit audit, DataBaseType dbtype);
        [OperationContract]
        void AddImportedFile(ImportedFile importedFile, DataBaseType dbtype);

        List<Load> ReadLoad(DataBaseType dbtype);

    }
}
