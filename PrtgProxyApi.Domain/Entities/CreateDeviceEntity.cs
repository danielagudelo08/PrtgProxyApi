using PrtgProxyApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrtgProxyApi.Domain.Entities
{
    public class CreateDeviceEntity
    {
        public required string Name { get; set; }
        public required string Host { get; set; }
        public required int ParentId { get; set; }

        // Opcionales de creación
        public string[]? Tags { get; set; }
        public DeviceDiscoveryMode? AutoDiscoveryMode { get; set; }
        public DeviceDiscoverySchedule? AutoDiscoverySchedule { get; set; }

        // Opcionales posteriores vía SetObjectProperty
        public string? Location { get; set; }
        public string? Comments { get; set; }
        public int? Priority { get; set; } // 0 a 5
    }
}
