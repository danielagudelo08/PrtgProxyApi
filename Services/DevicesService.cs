using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using PrtgAPI;
using PrtgProxyApi.Settings;
using PrtgProxyApi.Domain.Contracts;

namespace PrtgProxyApi.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly PrtgClient _client;
        private readonly ILogger<DevicesService> _logger;

        public DevicesService(IOptions<PrtgSettings> settings, ILogger<DevicesService> logger)
        {
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

        public async Task<List<Device>> GetDevicesAsync()
        {
            try
            {
                _logger.LogInformation("Obteniendo dispositivos desde PRTG.");

                var devices = await Task.Run(() => _client.GetDevices().ToList());

                _logger.LogInformation($"Se encontraron {devices.Count} dispositivos.");
                return devices;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener dispositivos desde PRTG.");
                throw;
            }
        }
    }
}
