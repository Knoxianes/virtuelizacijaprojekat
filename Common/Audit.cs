using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum MessageType
    {
        Info,Warning,Error
    }
    public class Audit
    {
        private uint _id;
        private DateTime _timeStamp;
        private string _message;
        private MessageType _messageType;

        public Audit(uint id, DateTime timeStamp, string message, MessageType messageType)
        {
            Id = id;
            TimeStamp = timeStamp;
            Message = message;
            MessageType = messageType;
        }

        public uint Id { get => _id; set => _id = value; }
        public DateTime TimeStamp { get => _timeStamp; set => _timeStamp = value; }
        public string Message { get => _message; set => _message = value; }
        public MessageType MessageType { get => _messageType; set => _messageType = value; }
    }
}
