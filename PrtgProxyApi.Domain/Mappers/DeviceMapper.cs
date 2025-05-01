using PrtgProxyApi.Domain.DTOs.Devices;
using PrtgProxyApi.Domain.Entities;

namespace PrtgProxyApi.Domain.Mappers
{
    public static class DeviceMapper
    {
        public static CreateDeviceEntity ToDomainRequest(CreateDeviceDTO request)
        {
            return new CreateDeviceEntity
            {
                Name = request.Name,
                Host = request.Host,
                ParentId = request.ParentId,
                Tags = request.Tags,
                Location = request.Location,
                Comments = request.Comments,
                Priority = request.Priority,
                AutoDiscoveryMode = request.AutoDiscoveryMode,
                AutoDiscoverySchedule = request.AutoDiscoverySchedule
            };
        }
    }
}
