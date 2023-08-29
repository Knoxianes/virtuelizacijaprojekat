using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum MessageType
    {
        Info,Warning,Error
    }
    [DataContract]
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

        [DataMember]
        public uint Id { get => _id; set => _id = value; }
        [DataMember]
        public DateTime TimeStamp { get => _timeStamp; set => _timeStamp = value; }
        [DataMember]
        public string Message { get => _message; set => _message = value; }
        [DataMember]
        public MessageType MessageType { get => _messageType; set => _messageType = value; }
    }
}
