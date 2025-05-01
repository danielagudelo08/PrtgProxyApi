using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrtgProxyApi.Domain
{
    public class SensorEntity
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Device { get; set; }
        public string Group { get; set; }
        public string Probe { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public bool Active { get; set; }
        public double Uptime { get; set; }
        public double Downtime { get; set; }
        public DateTime? LastCheck { get; set; }
        public DateTime? LastUp { get; set; }
        public DateTime? LastDown { get; set; }
        public string Comments { get; set; }
        public List<string> Tags { get; set; }

        public string DisplayType { get; set; }
    }
}
