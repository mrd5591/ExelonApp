using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace ExelonApp.Util
{
    public class Notification : BaseViewModel
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
        public bool confirm { get; set; }
    }
}
