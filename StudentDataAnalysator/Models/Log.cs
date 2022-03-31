using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysator.Models
{
    public class Log
    {
        private DateTime time;
        private string eventContext;
        private string component;
        private string eventName;
        private string description;

        public DateTime Time { get => time; set => time = value; }
        public string EventContext { get => eventContext; set => eventContext = value; }
        public string Component { get => component; set => component = value; }
        public string EventName { get => eventName; set => eventName = value; }
        public string Description { get => description; set => description = value; }

        public Log(DateTime time, string eventContext, string component, string eventName, string description)
        {
            this.time = time;
            this.eventContext = eventContext;
            this.component = component;
            this.eventName = eventName;
            this.description = description;
        }

        public Log()
        {
        }


    }
}
