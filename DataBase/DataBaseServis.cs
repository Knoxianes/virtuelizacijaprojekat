using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class DataBaseServis : IDataBase
    {
        public void AddAudit(Audit load, DataBaseType dbtype)
        {
            throw new NotImplementedException();
        }

        public void AddImportedFile(ImportedFile load, DataBaseType dbtype)
        {
            throw new NotImplementedException();
        }

        public void AddLoad(Load load, DataBaseType dbtype, FileType filetype)
        {
            throw new NotImplementedException();
        }

        public List<Load> ReadLoad(DataBaseType dbtype)
        {
            throw new NotImplementedException();
        }
    }
}
