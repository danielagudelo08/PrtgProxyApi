using PrtgProxyApi.Domain.DTOs.Devices;
using PrtgProxyApi.DTOs.Devices;
using PrtgProxyApi.Request.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrtgProxyApi.Domain.Contracts
{
    public interface IDeviceServiceDomain
    {
        Task<List<SelectDeviceDto>> GetDevicesForSelect();
        Task<DeviceEntity?> GetDeviceById(int id);
        Task<int> CreateDeviceAsync(CreateDeviceEntity request);
    }
}
