using PrtgAPI;
using PrtgProxyApi.Domain.Request;

namespace PrtgProxyApi.Domain.Contracts
{
    public interface ISensorsService
    {
        Task<List<Sensor>> GetSensorsAsync();
        int CreateHttpSensor(CreateHttpSensorRequest request);
    }
}
