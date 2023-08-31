using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
  


    [ServiceContract]
    public interface IServis
    {
        [OperationContract]
        void Load(MemoryStream memoryStream, string fileName, FileType fileType);
        [OperationContract]
        void Calculate();
    }
}
