using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
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

        public uint Id { get => _id; set => _id = value; }
        public DateTime Timestamp { get => _timestamp; set => _timestamp = value; }
        public double ForecastValue { get => _forecastValue; set => _forecastValue = value; }
        public double MeasuredValue { get => _measuredValue; set => _measuredValue = value; }
        public double AbsolutePercentageDeviation { get => _absolutePercentageDeviation; set => _absolutePercentageDeviation = value; }
        public double SquareDeviation { get => _squareDeviation; set => _squareDeviation = value; }
        public uint ImportedFileId { get => _importedFileId; set => _importedFileId = value; }
    }
}
