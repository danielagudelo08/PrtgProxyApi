using PrtgAPI;
using PrtgProxyApi.Request.Devices;
using PrtgProxyApi.DTOs.Devices;
namespace PrtgProxyApi.Contracts.Services
{
    public interface IDevicesService
    {
        Task<List<Device>> GetDevicesAsync();
        Task<List<DeviceSelectDto>> GetDevicesForSelectAsync();
        Task<int> CreateDeviceAsync(CreateDeviceRequest request);
        Task<Device?> GetDeviceByIdAsync(int id);
    }
}
