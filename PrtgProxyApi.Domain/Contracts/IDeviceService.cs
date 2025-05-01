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
    public interface IDeviceService
    {
        Task<List<SelectDeviceDto>> GetDevicesForSelect();
        Task<DeviceEntity?> GetDeviceById(int id);
        Task<int> CreateDeviceAsync(CreateDeviceDTO request);
    }
}
