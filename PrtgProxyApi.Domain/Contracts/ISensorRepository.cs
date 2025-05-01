using PrtgProxyApi.Domain.DTOs.Sensors;
using PrtgProxyApi.PrtgAPISatrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrtgProxyApi.Domain.Contracts
{
    public interface ISensorRepository
    {
        Task<List<SensorEntity>> GetSensorsByNameAsync(string name);
        Task<int> CreateHttpSensorAsync(CreateSensorEntity request);
        Task<SensorEntity> GetSensorByIdAsync(int sensorId);
    }
}
