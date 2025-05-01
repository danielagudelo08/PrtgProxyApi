using PrtgProxyApi.Domain.DTOs.Sensors;
using PrtgProxyApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrtgProxyApi.Domain.Contracts
{
    public interface ISensorsService
    {
        Task<List<SensorEntity>> GetSensorsByNameAsyncDomain(string name);
        Task<int> CreateHttpSensorAsync(CreateHttpSensorDTO request);
        Task<SensorEntity?> GetSensorByIdAsync(int sensorId);

    }
}
