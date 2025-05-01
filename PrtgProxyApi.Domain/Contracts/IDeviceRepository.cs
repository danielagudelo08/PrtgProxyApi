using PrtgProxyApi.Domain.DTOs.Devices;
using PrtgProxyApi.Domain.Entities;
using PrtgProxyApi.DTOs.Devices;
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
