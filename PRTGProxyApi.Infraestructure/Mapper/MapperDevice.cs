using PrtgAPI;
using PrtgAPI.Parameters;
using PrtgProxyApi.Domain;
using PrtgProxyApi.Domain.DTOs.Devices;
using PrtgProxyApi.Domain.Enums;
using PrtgProxyApi.Request.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrtgProxyApi.PrtgAPISatrack.Mapper
{
    public static class MapperDevice
    {
        public static DeviceEntity MapToDomainDevice(Device prtgDevice)
        {
            return new DeviceEntity
            {
                Id = prtgDevice.Id, // int, no necesita control
                Name = !string.IsNullOrWhiteSpace(prtgDevice.Name) ? prtgDevice.Name : "Sin nombre",
                Group = !string.IsNullOrWhiteSpace(prtgDevice.Group) ? prtgDevice.Group : "Sin grupo",
                Probe = !string.IsNullOrWhiteSpace(prtgDevice.Probe) ? prtgDevice.Probe : "Sin probe",
                Status = prtgDevice.Status != null ? prtgDevice.Status.ToString() : "Desconocido",
                Message = !string.IsNullOrWhiteSpace(prtgDevice.Message) ? prtgDevice.Message : null,
                Active = prtgDevice.Active, // bool
                Tags = prtgDevice.Tags != null && prtgDevice.Tags.Any() ? string.Join(",", prtgDevice.Tags) : null,
                Comments = !string.IsNullOrWhiteSpace(prtgDevice.Comments) ? prtgDevice.Comments : null,
                UpSensors = prtgDevice.UpSensors >= 0 ? prtgDevice.UpSensors : 0,
                DownSensors = prtgDevice.DownSensors >= 0 ? prtgDevice.DownSensors : 0,
                WarningSensors = prtgDevice.WarningSensors >= 0 ? prtgDevice.WarningSensors : 0
            };

        }

        public static NewDeviceParameters ToPrtgParameters(CreateDeviceEntity request)
        {
            var parameters = new NewDeviceParameters(request.Name, request.Host)
            {
                Tags = request.Tags ?? [],
                AutoDiscoveryMode = request.AutoDiscoveryMode.HasValue
                    ? MapToPrtgDiscoveryMode(request.AutoDiscoveryMode.Value)
                    : AutoDiscoveryMode.Manual,

                AutoDiscoverySchedule = request.AutoDiscoverySchedule.HasValue
                    ? MapToPrtgDiscoverySchedule(request.AutoDiscoverySchedule.Value)
                    : AutoDiscoverySchedule.Once
            };

            return parameters;
        }

        private static AutoDiscoveryMode MapToPrtgDiscoveryMode(DeviceDiscoveryMode mode)
        {
            return mode switch
            {
                DeviceDiscoveryMode.Manual => AutoDiscoveryMode.Manual,
                DeviceDiscoveryMode.Automatic => AutoDiscoveryMode.Automatic,
                _ => throw new ArgumentOutOfRangeException(nameof(mode))
            };
        }

        private static AutoDiscoverySchedule MapToPrtgDiscoverySchedule(DeviceDiscoverySchedule schedule)
        {
            return schedule switch
            {
                DeviceDiscoverySchedule.Once => AutoDiscoverySchedule.Once,
                DeviceDiscoverySchedule.Hourly => AutoDiscoverySchedule.Hourly,
                DeviceDiscoverySchedule.Daily => AutoDiscoverySchedule.Daily,
                DeviceDiscoverySchedule.Weekly => AutoDiscoverySchedule.Weekly,
                _ => throw new ArgumentOutOfRangeException(nameof(schedule))
            };
        }
    }

}
