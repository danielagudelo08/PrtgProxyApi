using Microsoft.Extensions.Options;
using PrtgAPI;
using PrtgAPI.Parameters;
using PrtgProxyApi.Domain.Contracts;
using PrtgProxyApi.Domain.Request;
using PrtgProxyApi.Settings;
using System.Runtime;

namespace PrtgProxyApi.Services
{
    public class SensorsService : ISensorsService
    {
        private readonly PrtgClient _client;
        private readonly ILogger<SensorsService> _logger;
        public SensorsService(IOptions<PrtgSettings> settings, ILogger<SensorsService> logger) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            var prtgSettings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrEmpty(prtgSettings.Server) ||
                string.IsNullOrEmpty(prtgSettings.Username) ||
                string.IsNullOrEmpty(prtgSettings.Password))
            {
                throw new InvalidOperationException("Faltan valores en la configuración de PRTG.");
            }

            _client = new PrtgClient(
                prtgSettings.Server,
                prtgSettings.Username,
                prtgSettings.Password,
                AuthMode.Password,
                prtgSettings.IgnoreSSL
            );
        }

        public async Task<List<Sensor>> GetSensorsAsync()
        {
            try
            {
                _logger.LogInformation("Obteniendo sensores desde PRTG.");

                List<Sensor> sensors = await Task.Run(() => _client.GetSensorsAsync());

                _logger.LogInformation($"Se encontraron {sensors.Count} sensores.");
                return sensors;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sensores desde PRTG.");
                throw;
            }
        }

        public int CreateHttpSensor(CreateHttpSensorRequest request)
        {
            var parameters = new HttpSensorParameters
            {
                Name = request.Name,
                Url = request.Url,
                HttpRequestMethod = request.HttpRequestMethod,
                SensorType = SensorType.Http,
                Tags = ["Informes"],
                Priority = request.Priority,
                Timeout = request.Timeout.HasValue ? (int)request.Timeout : 60,
                InheritInterval = false,
                Interval = request.IntervalSeconds switch
                {
                    30 => ScanningInterval.ThirtySeconds,
                    60 => ScanningInterval.SixtySeconds,
                    300 => ScanningInterval.FiveMinutes,
                    600 => ScanningInterval.TenMinutes,
                    _ => ScanningInterval.FiveMinutes // Valor por defecto
                }
            };

            // Si se han proporcionado comentarios, establécelos después de crear el sensor
            var sensors = _client.AddSensor(request.DeviceId, parameters);
            var sensorId = sensors.FirstOrDefault()?.Id ?? 0;

            // Si se creó el sensor y hay comentarios para agregar
            if (sensorId > 0 && !string.IsNullOrEmpty(request.Comments))
            {
                try
                {
                    // Usar SetObjectProperty para establecer comentarios
                    _client.SetObjectProperty(sensorId, ObjectProperty.Comments, request.Comments);
                }
                catch (Exception ex)
                {
                    // Opcional: manejar excepciones si la actualización de comentarios falla
                    // pero el sensor ya fue creado
                    Console.WriteLine($"Sensor creado pero no se pudieron añadir comentarios: {ex.Message}");
                }
            }

            return sensorId;
        }

    }
}
