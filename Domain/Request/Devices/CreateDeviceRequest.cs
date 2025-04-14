using PrtgAPI;

namespace PrtgProxyApi.Domain.Request.Devices
{
    public class CreateDeviceRequest
    {
        // Obligatorios
        public required string Name { get; set; }
        public required string Host { get; set; }
        public required int ParentId { get; set; }

        // Opcionales de creación
        public string[]? Tags { get; set; }
        public AutoDiscoveryMode? AutoDiscoveryMode { get; set; }
        public AutoDiscoverySchedule? AutoDiscoverySchedule { get; set; }

        // Opcionales posteriores vía SetObjectProperty
        public string? Location { get; set; }
        public string? Comments { get; set; }
        public int? Priority { get; set; } // 0 a 5
    }


}
