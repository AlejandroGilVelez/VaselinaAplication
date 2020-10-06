using System;

namespace Framework.Models
{
    public class LogAudit : BaseModel
    {
        public string Message { get; set; }

        public string MessageTemplate { get; set; }

        public string Level { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Xml { get; set; }

        public string LogEvent { get; set; }       

    }
}
