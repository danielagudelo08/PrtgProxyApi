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
    public interface IDeviceRepository
    {
        Task<List<SelectDeviceDto>> GetDevicesForSelectAsync();
        Task<DeviceEntity?> GetDeviceByIdAsync(int id);
        Task<int> CreateDeviceAsync(CreateDeviceEntity request);
    }
}
