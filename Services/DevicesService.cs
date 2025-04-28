using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using PrtgAPI;
using PrtgProxyApi.Settings;
using PrtgAPI.Parameters;
using PrtgProxyApi.Request.Devices;
using PrtgProxyApi.DTOs.Devices;
using PrtgProxyApi.Contracts.Services;

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

        public async Task<List<DeviceSelectDto>> GetDevicesForSelectAsync()
        {
            try
            {
                _logger.LogInformation("Obteniendo dispositivos (solo id y nombre) desde PRTG.");

                var devices = await Task.Run(() => _client
                    .GetDevices()
                    .Select(d => new DeviceSelectDto
                    {
                        Id = d.Id,
                        Name = d.Name
                    })
                    .ToList());

                _logger.LogInformation($"Se encontraron {devices.Count} dispositivos.");
                return devices;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener dispositivos desde PRTG.");
                throw;
            }
        }


        public async Task<Device?> GetDeviceByIdAsync(int id)
        {
            try
            {
                var device = await Task.Run(() => _client.GetDevice(id));

                if (device == null)
                {
                    _logger.LogWarning($"No se encontró un dispositivo con ID {id}.");
                }

                return device;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el dispositivo con ID {id}.");
                throw;
            }
        }



        public async Task<int> CreateDeviceAsync(CreateDeviceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Host))
                throw new ArgumentException("El nombre y el host del dispositivo son obligatorios.");

            try
            {
                var parameters = new NewDeviceParameters(request.Name, request.Host)
                {
                    Tags = request.Tags ?? [],
                    AutoDiscoveryMode = request.AutoDiscoveryMode ?? AutoDiscoveryMode.Manual,
                    AutoDiscoverySchedule = request.AutoDiscoverySchedule ?? AutoDiscoverySchedule.Once
                };

                var device = await Task.Run(() => _client.AddDevice(request.ParentId, parameters)) ?? throw new InvalidOperationException("No se pudo crear el dispositivo. El objeto resultante es nulo.");

                // Propiedades opcionales via SetObjectProperty
                if (!string.IsNullOrWhiteSpace(request.Location))
                {
                    _client.SetObjectProperty(device, ObjectProperty.Location, request.Location);
                }

                if (!string.IsNullOrWhiteSpace(request.Comments))
                {
                    _client.SetObjectProperty(device, ObjectProperty.Comments, request.Comments);
                }

                if (request.Priority.HasValue)
                {
                    _client.SetObjectProperty(device, ObjectProperty.Priority, (Priority)request.Priority.Value);
                }

                _logger.LogInformation($"Dispositivo '{request.Name}' creado exitosamente con ID: {device.Id}");

                return device.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear el dispositivo '{request?.Name}'.");
                throw;
            }
        }


    }
}
