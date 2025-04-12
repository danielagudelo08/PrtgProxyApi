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
                HttpRequestMethod = HttpRequestMethod.GET,
                SensorType = SensorType.Http,
                Tags = ["Informes"],
                Priority = Priority.Three,
                Timeout = request.Timeout.HasValue ? (int)request.Timeout : 60,
                Url = request.Url,

            };

            var sensors = _client.AddSensor(request.DeviceId, parameters);

            return sensors.FirstOrDefault()?.Id ?? 0;
        }

    }
}
