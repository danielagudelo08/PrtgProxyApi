using PrtgAPI;
using PrtgProxyApi.Domain.Request.Devices;

namespace PrtgProxyApi.Domain.Contracts
{
    public interface IDevicesService
    {
        Task<List<Device>> GetDevicesAsync();
        Task<int> CreateDeviceAsync(CreateDeviceRequest request);
        Task<Device?> GetDeviceByIdAsync(int id);
    }
}
