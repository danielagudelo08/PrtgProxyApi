using PrtgProxyApi.Domain.DTOs.Devices;
using PrtgProxyApi.Request.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
