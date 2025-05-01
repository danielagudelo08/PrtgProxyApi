using Microsoft.Extensions.Logging;
using PrtgProxyApi.Domain.Contracts;
using PrtgProxyApi.Domain.DTOs.Sensors;
using PrtgProxyApi.Domain.Helpers;
using PrtgProxyApi.PrtgAPISatrack;

namespace PrtgProxyApi.Domain
{
    // Capa Domain
    public class SensorsServiceDomain: ISensorsServiceDomain
    {
        private readonly ISensorRepository _sensorRepository;
        private readonly ILogger<SensorsServiceDomain> _logger;

        public SensorsServiceDomain(ISensorRepository sensorRepository, ILogger<SensorsServiceDomain> logger)
        {
            _sensorRepository = sensorRepository ?? throw new ArgumentNullException(nameof(sensorRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<SensorEntity> GetSensorByIdAsync(int sensorId)
        {
            try
            {
                var sensor = await _sensorRepository.GetSensorByIdAsync(sensorId);

                if (sensor == null)
                {
                    _logger.LogWarning($"No se encontró el sensor con ID {sensorId}");
                }

                return sensor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el sensor con ID {sensorId}");
                throw; // Propaga la excepción para manejo posterior
            }
        }

        public async Task<List<SensorEntity>> GetSensorsByNameAsyncDomain(string name)
            {
                try
                {
                    _logger.LogInformation("Buscando sensores que contengan el nombre: {SensorName}", name);
                    var normalizedName = TextNormalizer.Normalize(name);
                    var sensors = await _sensorRepository.GetSensorsByNameAsync(name);
                    var filteredSensors = sensors
                        .Where(s => !string.IsNullOrWhiteSpace(s.Name) &&
                                    TextNormalizer.Normalize(s.Name).Contains(normalizedName))
                        .ToList();
                    _logger.LogInformation("Se encontraron {Count} sensores con nombre similar a '{SensorName}'", filteredSensors.Count, name);
                    return filteredSensors;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al buscar sensores con nombre '{SensorName}'", name);
                    throw;
                }
            }

            public async Task<int> CreateHttpSensorAsync(CreateSensorEntity request)
            {
                return await _sensorRepository.CreateHttpSensorAsync(request);
            }
        }

}
