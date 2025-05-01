using Microsoft.Extensions.Logging;
using PrtgAPI;
using PrtgProxyApi.Domain.Contracts;
using PrtgProxyApi.Domain.Entities;
using PrtgProxyApi.DTOs.Devices;
using PrtgProxyApi.PrtgAPISatrack.Mapper;


namespace PrtgProxyApi.PrtgAPISatrack.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly PrtgClient _client;
        private readonly ILogger<DeviceRepository> _logger;

        public DeviceRepository(PrtgClient client, ILogger<DeviceRepository> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<SelectDeviceDto>> GetDevicesForSelectAsync()
        {
            return await Task.Run(() => _client
                .GetDevices()
                .Select(d => new SelectDeviceDto
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToList());
        }

        public async Task<DeviceEntity?> GetDeviceByIdAsync(int id)
        {
            try
            {
                var prtgDevice = await Task.Run(() => _client.GetDevice(id));

                if (prtgDevice == null)
                {
                    _logger.LogWarning("No se encontró un dispositivo con ID {DeviceId}.", id);
                    return null;
                }

                return MapperDevice.MapToDomainDevice(prtgDevice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el dispositivo con ID {DeviceId}.", id);
                throw;
            }
        }

        public async Task<int> CreateDeviceAsync(CreateDeviceEntity request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Host))
                throw new ArgumentException("El nombre y el host del dispositivo son obligatorios.");

            try
            {
                var parameters = MapperDevice.ToPrtgParameters(request);

                var device = await Task.Run(() => _client.AddDevice(request.ParentId, parameters))
                    ?? throw new InvalidOperationException("No se pudo crear el dispositivo.");

                SetOptionalProperties(device, request);

                _logger.LogInformation("Dispositivo {Name} creado exitosamente con ID: {Id}", request.Name, device.Id);
                return device.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el dispositivo {Name}.", request.Name);
                throw;
            }
        }

        private void SetOptionalProperties(Device device, CreateDeviceEntity request)
        {
            if (!string.IsNullOrWhiteSpace(request.Location) && request.Location?.ToLowerInvariant() != "string")
                _client.SetObjectProperty(device, ObjectProperty.Location, request.Location);

            if (!string.IsNullOrWhiteSpace(request.Comments) && request.Comments?.ToLowerInvariant() != "string")
                _client.SetObjectProperty(device, ObjectProperty.Comments, request.Comments);

            if (request.Priority.HasValue)
                _client.SetObjectProperty(device, ObjectProperty.Priority, (Priority)request.Priority.Value);
        }

    }
}
