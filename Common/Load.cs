using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public  class Load
    {
        private uint _id;
        private DateTime _timestamp;
        private double _forecastValue;
        private double _measuredValue;
        private double _absolutePercentageDeviation;
        private double _squareDeviation;
        private uint _importedFileId;

        public Load(uint id, DateTime timestamp, double forecastValue, double measuredValue, double absolutePercentageDeviation, double squareDeviation, uint importedFileId)
        {
            Id = id;
            Timestamp = timestamp;
            ForecastValue = forecastValue;
            MeasuredValue = measuredValue;
            AbsolutePercentageDeviation = absolutePercentageDeviation;
            SquareDeviation = squareDeviation;
            ImportedFileId = importedFileId;
        }

        [DataMember]
        public uint Id { get => _id; set => _id = value; }
        [DataMember]
        public DateTime Timestamp { get => _timestamp; set => _timestamp = value; }
        [DataMember]
        public double ForecastValue { get => _forecastValue; set => _forecastValue = value; }
        [DataMember]
        public double MeasuredValue { get => _measuredValue; set => _measuredValue = value; }
        [DataMember]
        public double AbsolutePercentageDeviation { get => _absolutePercentageDeviation; set => _absolutePercentageDeviation = value; }
        [DataMember]
        public double SquareDeviation { get => _squareDeviation; set => _squareDeviation = value; }
        [DataMember]
        public uint ImportedFileId { get => _importedFileId; set => _importedFileId = value; }
    }
}
