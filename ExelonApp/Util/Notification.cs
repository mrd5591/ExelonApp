using Newtonsoft.Json;
using System;

namespace ExelonApp.Util
{
    public class Notification
    {
        private string ts;
        public string timestamp
        { 
            get
            {
                return ts;
            }
            
            set
            {
                ts = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(double.Parse(value)).ToLocalTime().ToString();  
            }
        }
        public string message { get; set; }
        public string notificationId { get; set; }
        public string confirm { get; set; }
    }
}
