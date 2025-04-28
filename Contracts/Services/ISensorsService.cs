using PrtgAPI;
using PrtgProxyApi.Request;

namespace PrtgProxyApi.Contracts.Services
{
    public interface ISensorsService
    {
        Task<List<Sensor>> GetSensorsAsync();
        Task<Sensor> GetSensorByIdAsync(int sensorId);
        Task<List<Sensor>> GetSensorsByNameAsync(string name);
        int CreateHttpSensor(CreateHttpSensorRequest request);


    }
}
