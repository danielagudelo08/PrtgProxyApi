using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrtgProxyApi.Domain
{
    public class DeviceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Probe { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public bool Active { get; set; }
        public string Tags { get; set; }
        public string Comments { get; set; }
        public int UpSensors { get; set; }
        public int DownSensors { get; set; }
        public int WarningSensors { get; set; }
    }

}
