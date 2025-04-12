using PrtgAPI;

namespace PrtgProxyApi.Domain.Contracts
{
    public interface IDevicesService
    {
        Task<List<Device>> GetDevicesAsync();
    }
}
