using Microsoft.Extensions.Logging;
using PrtgAPI;
using PrtgProxyApi.Domain.Contracts;
using PrtgProxyApi.Domain.Entities;
using PrtgProxyApi.PrtgAPISatrack.Mapper;


namespace PrtgProxyApi.PrtgAPISatrack.Repositories
{
    // Capa Infrastructure
    public class SensorRepository : ISensorRepository
    {
        private readonly PrtgClient _client;
        private readonly ILogger<SensorRepository> _logger;

        public SensorRepository(PrtgClient client, ILogger<SensorRepository> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<SensorEntity?> GetSensorByIdAsync(int sensorId)
        {
            _logger.LogInformation("Buscando sensor con ID: {sensorId}", sensorId);

            try
            {
                var sensor = await Task.Run(() => _client.GetSensor(sensorId));

                if (sensor == null)
                {
                    _logger.LogWarning("No se encontró el sensor con ID {sensorId}", sensorId);
                    return null;
                }

                _logger.LogInformation("Sensor encontrado: {sensor.Name}", sensor.Name);
                return MapperSensor.MapToDomainSensor(sensor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el sensor con ID {sensorId}", sensorId);
                throw; // Propaga la excepción para manejo posterior
            }
        }

        public async Task<List<SensorEntity>> GetSensorsByNameAsync(string name)
        {
            try
            {
                _logger.LogInformation("Obteniendo sensores de PRTG por nombre: {SensorName}", name);

                var sensorsFromPrtg = await Task.Run(() => _client.GetSensors()
                    .Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList());

                if (sensorsFromPrtg == null || sensorsFromPrtg.Count == 0)
                {
                    _logger.LogWarning("No se encontraron sensores con el nombre: {SensorName}", name);
                    return []; // Retorna una lista vacía si no hay resultados
                }

                return sensorsFromPrtg.Select(s => MapperSensor.MapToDomainSensor(s)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sensores de PRTG por nombre.");
                throw;
            }
        }

        public async Task<int> CreateHttpSensorAsync(CreateSensorEntity request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("El nombre del sensor es obligatorio.", nameof(request.Name));

            try
            {
                var parameters = MapperSensor.ToPrtgParameters(request);

                _logger.LogInformation("Creando sensor HTTP en dispositivo {DeviceId}", request.DeviceId);

                var sensors = await Task.Run(() => _client.AddSensor(request.DeviceId, parameters));
                var sensorId = sensors.FirstOrDefault()?.Id ?? 0;

                if (sensorId > 0 && !string.IsNullOrWhiteSpace(request.Comments))
                    await TryAddSensorCommentsAsync(sensorId, request.Comments);

                return sensorId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando el sensor HTTP.");
                throw;
            }
        }

        private async Task TryAddSensorCommentsAsync(int sensorId, string comments)
        {
            try
            {
                await Task.Run(() => _client.SetObjectProperty(sensorId, ObjectProperty.Comments, comments));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Sensor creado pero no se pudieron añadir comentarios.");
            }
        }
    }

}
