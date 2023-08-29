using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ImportedFile
    {
        private uint _id;
        private string _fileName;

        public ImportedFile(uint id, string fileName)
        {
            Id = id;
            FileName = fileName;
        }

        public uint Id { get => _id; set => _id = value; }
        public string FileName { get => _fileName; set => _fileName = value; }
    }
}
