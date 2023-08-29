using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class ImportedFile
    {
        private uint _id;
        private string _fileName;

        public ImportedFile(uint id, string fileName)
        {
            Id = id;
            FileName = fileName;
        }

        [DataMember]
        public uint Id { get => _id; set => _id = value; }
        [DataMember]
        public string FileName { get => _fileName; set => _fileName = value; }
    }
}
