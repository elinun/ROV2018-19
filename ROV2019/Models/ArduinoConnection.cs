using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROV2019.Models
{
    public class ArduinoConnections
    {
        public List<ArduinoConnection> connections { get; set; }
    }

    public class ArduinoConnection
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string FriendlyName { get; set; }
        public int Latency { get; set; }
        public Trim Trim { get; set; }
    }

    public class Trim
    {
        
    }
}
